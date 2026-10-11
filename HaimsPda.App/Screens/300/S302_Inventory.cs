using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [302] 재물조사 : [310] 에서 고른 재물조사번호의 품목을 한 건씩 받아 실셈수량을 입력한다.
    // 원본 웹화면 : /ui/ws/plus/PL302_W01.xml (서버 메뉴 P192)
    //
    //   OnEnter(DATE/SALENO/WHSCD/SELECT/ISC_ZONE) -> fn_Search(false) : PL302_W01_S31 -> dsOutPDA_310_S01 첫 건
    //   실셈수량 입력 -> fn_CheckQty : 차이 = 실셈 - LOC수량
    //       차이 0   -> fn_Save(RESULT=Y)
    //       차이 있음 -> "이상내역을 등록하시겠습니까?"
    //                    예   -> 이상코드 선택(PL301_W01_S04) -> fn_Save(RESULT=N, RESCD)
    //                    아니오 -> 실셈수량을 LOC수량으로 되돌리고 대기 (원본 그대로)
    //   fn_Save : PL302_W01_U21 + U02 -> 남은 품목(ITEM_CNT)이 1 이면 끝, 아니면 다음 품목 조회
    //
    //   부번 스캔/입력 -> fn_Search(true) : 그 부번 품목으로 이동
    //   SKIP (연간/등급만) -> 다음 품목 (직전 ISR_LOCNO_SEQ / LOC_LOCNO 를 보낸다)
    //   파트 -> [321]
    //
    // 재고수량/LOC수량은 원본도 화면에서 주석 처리해 보여 주지 않는다(블라인드 실사). 그대로 둔다.
    //
    // 원본과 다르게 한 부분
    //   - 이상코드 팝업은 공통 목록 팝업(LepSelect.Choose)을 쓴다.
    //   - 마지막 품목 저장 후 원본은 화면 전체를 지운다. 여기서는 헤더(재물조사번호 등)는 남기고 품목만 지운다.
    //   - 저장이 한 번이라도 있었으면 [310] 으로 돌아갈 때 재조회시킨다 (BackResult).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S302_Inventory.Designer.cs 에서 관리한다.
    public sealed partial class S302_Inventory : ScreenBase
    {
        private const string MP_OK = "MP102";       // 정상 처리되었습니다
        private const string MP_SEARCH = "MP101";   // 정상 조회되었습니다
        private const string MP_NODATA = "MP626";   // 조회대상 데이터가 존재하지 않습니다
        private const string MP_PTNO = "MP303";     // 부품번호를 확인하세요
        private const string MP_DIFF = "MP518";     // 입력한 수량과 상이합니다. 이상내역을 등록하시겠습니까?

        private InvJob _job;
        private InvItem _item;
        private string _locnoSeq;    // 원본 SearChYn=="Y" 일 때의 ISR_LOCNO_SEQN
        private string _locno;       // 원본 LOC_LOCNON
        private bool _busy;
        private bool _saved;         // [310] 재조회용
        private bool _done;          // 마지막 품목까지 저장함

        public override int ScreenNo { get { return ScreenId.Inventory; } }
        public override string ScreenName { get { return "재물조사"; } }

        public override NavArgs BackResult
        {
            get { return _saved ? NavResult.Saved(ScreenNo) : null; }
        }

        public S302_Inventory()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            _job = new InvJob();
            _job.Date = Arg(args, "DATE");
            _job.Seq = Arg(args, "SALENO");
            _job.Whscd = Arg(args, "WHSCD");
            _job.Gubun = InvGubun.Code(Arg(args, "SELECT"));
            _job.Zone = Arg(args, "ISC_ZONE");

            lblSeq.Text = _job.Seq;
            lblWh.Text = _job.Whscd;
            lblZone.Text = _job.Zone;
            lblGubun.Text = InvGubun.Name(_job.Gubun);

            // 원본 : 연간/등급이 아니면 SKIP 버튼(group135)을 숨긴다
            btnSkip.Visible = (_job.Gubun == InvGubun.Annual || _job.Gubun == InvGubun.Grade);

            if (_job.Seq.Length == 0)
            {
                _job = null;
                SetButtons(false);
                btnPart.Enabled = true;
                Msg("[310] 재물조사대상에서 골라 진입하세요.", MsgLevel.Warn);
                return;
            }

            Search(null);
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy || _job == null) return;
            txtPart.Text = PartNo.Display(data.Text);
            SearchPart();
        }

        // ------------------------------------------------------------------
        // 조회 (fn_Search)
        // ------------------------------------------------------------------
        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            SearchPart();
        }

        private void OnSearch(object sender, EventArgs e) { SearchPart(); }

        private void SearchPart()
        {
            if (_busy || _job == null) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                FocusPart();
                return;
            }
            txtPart.Text = PartNo.Display(ptno);
            Search(ptno);
        }

        // 원본 btn6 SKIP -> fn_Search(false)
        private void OnSkip(object sender, EventArgs e)
        {
            if (_busy || _job == null) return;
            Search(null);
        }

        /// <summary>ptno 가 null 이면 다음 품목 (fn_Search(false))</summary>
        private void Search(string ptno)
        {
            if (_busy) return;

            InvJob job = _job;
            string seq = _locnoSeq, loc = _locno;

            Begin("조회중...");
            Async.Run(this,
                delegate { return InventoryService.SearchItem(job, ptno, seq, loc); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    InvItem it = (InvItem)r;
                    if (!it.Found)
                    {
                        // 원본은 이전 품목 표시를 그대로 둔다
                        Report(MP_NODATA, "조회대상 데이터가 존재하지 않습니다.", MsgLevel.Warn);
                        FocusPart();
                        return;
                    }

                    ShowItem(it);
                    End(CommonCache.Msg(MP_SEARCH, "정상 조회되었습니다."), MsgLevel.Success);
                    FocusCount();
                });
        }

        private void ShowItem(InvItem it)
        {
            _item = it;
            _done = false;

            lblPrefix.Text = (it.Lep.Length > 0) ? it.Lep : "H";
            txtPart.Text = PartNo.Display(it.Ptno);
            lblPartName.Text = it.PartName;
            lblClass.Text = it.Grade;
            lblCnt.Text = it.ItemCnt;
            lblPrice.Text = it.Amt;
            if (it.Whscd.Length > 0) lblWh.Text = it.Whscd;
            if (it.Zone.Length > 0) lblZone.Text = it.Zone;
            lblLoc.Text = Loc.Display(it.Locno);
            lblOutQty.Text = Num(it.SalQt);
            txtCount.Text = "0";
            lblDiff.Text = "0";

            // 원본 SearChYn : 연간/등급만 다음 조회에 위치키를 싣는다
            if (_job.Gubun == InvGubun.Annual || _job.Gubun == InvGubun.Grade)
            {
                _locnoSeq = it.LocnoSeq;
                _locno = it.Locno;
            }
        }

        // ------------------------------------------------------------------
        // 실셈수량 입력
        // ------------------------------------------------------------------
        private void OnCountKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void OnCountKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            CheckQty();
        }

        private void OnPlus1(object sender, EventArgs e) { AddCount(1); }
        private void OnPlus10(object sender, EventArgs e) { AddCount(10); }

        private void OnMinus1(object sender, EventArgs e)
        {
            if (ToInt(txtCount.Text) <= 0)
            {
                MessageBox.Show("-값은 입력할수 없습니다.", "[302] " + ScreenName);
                FocusCount();
                return;
            }
            AddCount(-1);
        }

        private void OnZero(object sender, EventArgs e)
        {
            txtCount.Text = "0";
            FocusCount();
        }

        private void AddCount(int n)
        {
            txtCount.Text = (ToInt(txtCount.Text) + n).ToString();
            FocusCount();
        }

        private void OnConfirm(object sender, EventArgs e) { CheckQty(); }

        // ------------------------------------------------------------------
        // fn_CheckQty
        // ------------------------------------------------------------------
        private void CheckQty()
        {
            if (_busy) return;
            if (_job == null || _item == null || _done)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                FocusPart();
                return;
            }

            int cnt = ToInt(txtCount.Text);
            int diff = cnt - ToInt(_item.LocAvlQt);
            txtCount.Text = cnt.ToString();
            lblDiff.Text = diff.ToString();

            if (diff == 0)
            {
                Save(cnt.ToString(), "0", "", "Y");
                return;
            }

            string q = CommonCache.Msg(MP_DIFF, "입력한 수량과 상이합니다. 이상내역을 등록 하시겠습니까?");
            if (Confirm(q))
            {
                PickReasonAndSave(cnt.ToString(), diff.ToString());
            }
            else
            {
                // 원본 : 실셈수량을 LOC수량으로 되돌리고 저장하지 않는다
                txtCount.Text = Num(_item.LocAvlQt);
                lblDiff.Text = "0";
                FocusCount();
            }
        }

        private void PickReasonAndSave(string doQt, string osdQt)
        {
            Begin("이상코드 조회중...");
            Async.Run(this,
                delegate { return InventoryService.GetReasons(InventoryService.Pgm302); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList codes = (ArrayList)r;
                    if (codes.Count == 0)
                    {
                        ReportText("이상코드가 등록되어있지 않습니다.", MsgLevel.Warn);
                        FocusCount();
                        return;
                    }
                    End("", MsgLevel.Info);

                    int idx = HaimsPda.Controls.LepSelect.Choose(codes, "이상코드 선택", "이상코드");
                    if (idx < 0) { Msg("취소했습니다.", MsgLevel.Info); FocusCount(); return; }

                    Save(doQt, osdQt, ((InvReason)codes[idx]).Code, "N");
                });
        }

        // ------------------------------------------------------------------
        // fn_Save
        // ------------------------------------------------------------------
        private void Save(string doQt, string osdQt, string rescd, string result)
        {
            InvJob job = _job;
            InvItem it = _item;

            Begin("저장중...");
            Async.Run(this,
                delegate { InventoryService.SaveItem(job, it, doQt, osdQt, rescd, result); return null; },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    _saved = true;
                    string ok = CommonCache.Msg(MP_OK, "정상 처리되었습니다.");

                    if (it.ItemCnt.Trim() == "1")
                    {
                        // 원본 fn_Init : 마지막 품목
                        ClearItem();
                        _done = true;
                        End(ok + " 재물조사 품목을 모두 처리했습니다.", MsgLevel.Success);
                        MessageBox.Show(ok + "\r\n재물조사 품목을 모두 처리했습니다.", "[302] " + ScreenName);
                        FocusPart();
                        return;
                    }

                    End(ok, MsgLevel.Success);
                    Search(null);
                });
        }

        // ------------------------------------------------------------------
        // 파트 -> [321] (원본 OnBtnStock, 메뉴 1C03)
        // ------------------------------------------------------------------
        private void OnPart(object sender, EventArgs e)
        {
            if (_busy) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                FocusPart();
                return;
            }

            NavArgs a = new NavArgs();
            a.Set("LEP", lblPrefix.Text);
            a.Set("PTNO", ptno);
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            a.Set("LOCNO", Loc.Key(lblLoc.Text));
            a.Set("WHSCD", lblWh.Text);
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        // ------------------------------------------------------------------
        private void FocusPart()
        {
            txtPart.Focus();
            txtPart.SelectAll();
        }

        private void FocusCount()
        {
            txtCount.Focus();
            txtCount.SelectAll();
        }

        /// <summary>표시용. "3.00" 이나 "1,234" 도 정수로 보여 준다.</summary>
        private static string Num(string s) { return ToInt(s).ToString(); }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try
            {
                return (int)double.Parse(s, System.Globalization.NumberStyles.Float,
                                         System.Globalization.CultureInfo.InvariantCulture);
            }
            catch { return 0; }
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[302] " + ScreenName,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        private void Begin(string msg)
        {
            _busy = true;
            Cursor.Current = Cursors.WaitCursor;
            SetButtons(false);
            Msg(msg, MsgLevel.Info);
        }

        private void End(string msg, MsgLevel lv)
        {
            _busy = false;
            Cursor.Current = Cursors.Default;
            SetButtons(_job != null);
            btnPart.Enabled = true;
            if (msg.Length > 0) Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            MessageBox.Show(text, "[302] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[302] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnPlus1.Enabled = on;
            btnMinus1.Enabled = on;
            btnPlus10.Enabled = on;
            btnZero.Enabled = on;
            btnSkip.Enabled = on;
            btnConfirm.Enabled = on;
            btnPart.Enabled = on;
        }

        private void ClearItem()
        {
            _item = null;
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblPartName.Text = "";
            lblClass.Text = "";
            lblCnt.Text = "";
            lblLoc.Text = "";
            lblOutQty.Text = "0";
            lblPrice.Text = "0";
            txtCount.Text = "0";
            lblDiff.Text = "0";
        }

        private void ClearAll()
        {
            _job = null;
            _locnoSeq = null;
            _locno = null;
            _saved = false;
            _done = false;
            lblSeq.Text = "";
            lblWh.Text = "";
            lblZone.Text = "";
            lblGubun.Text = "";
            ClearItem();
            SetButtons(true);
        }
    }
}
