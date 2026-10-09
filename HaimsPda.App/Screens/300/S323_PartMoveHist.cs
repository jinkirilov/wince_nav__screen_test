using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [323] 부품수불이력조회 : 부번 + 조회일 -> 전월/현재고/단가 등 헤더 + 수불내역 목록
    // 원본 웹화면 : /ui/ws/plus/PL323_W01.xml (서버 메뉴 P134)
    //
    //   부번 스캔 -> fn_SearchLep(PL300_W01_S01) -> (계열 선택) -> fn_Search(PL323_W01_S01/S04/S06)
    //   조회 전용. 버튼 : 파트([321]) / LOC([410]) / 지움
    //
    // 원본과 다르게 한 부분
    //   - 조회일은 원본 DatePicker 대신 날짜 선택 컨트롤 + ◀▶(한 달씩) 버튼으로 바꾼다.
    //     조회일을 바꾸면 부번이 있을 때 바로 다시 조회한다.
    //   - 수불내역이 없을 때 원본은 아무 안내 없이 빈 목록을 보여 준다. 여기서는 푸터에 알린다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S323_PartMoveHist.Designer.cs 에서 관리한다.
    public sealed partial class S323_PartMoveHist : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_NOSTOCK = "MP333"; // 해당부품에 대한 재고정보가 없습니다
        private const string MP_NOLEP = "MP540";   // 부품번호를 입력하십시오(계열)
        private const string MP_PTNO = "MP303";    // 부품번호를 확인하세요
        private const string MP_CLEAR = "MP503";   // 지우시겠습니까

        private string _lep = "H";
        private string _searchedPtno = "";
        private bool _busy;
        private bool _dateLock;      // 코드에서 날짜를 바꿀 때 재조회 막기

        public override int ScreenNo { get { return ScreenId.PartMoveHist; } }
        public override string ScreenName { get { return "부품수불이력"; } }

        public S323_PartMoveHist()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            // 다른 화면에서 부번을 들고 오면 계열 조회 없이 바로 조회한다 (원본 ds_LinkInfo)
            string ptno = Arg(args, "PTNO");
            if (ptno.Length > 0)
            {
                string lep = Arg(args, "LEP");
                SetLep(lep.Length > 0 ? lep : "H");
                txtPart.Text = PartNo.Display(ptno);
                lblClass.Text = Arg(args, "CLASS");
                lblPartName.Text = Arg(args, "PTNM");

                DateTime d;
                if (TryDate(Arg(args, "DATE"), out d)) SetDate(d);

                Search(PartNo.Key(ptno));
                return;
            }

            txtPart.Focus();
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;
            txtPart.Text = PartNo.Display(data.Text);
            SearchLep();
        }

        // ------------------------------------------------------------------
        // 입력
        // ------------------------------------------------------------------
        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchLep(); }
        }

        private void OnSearch(object sender, EventArgs e) { SearchLep(); }

        private void OnPrevMonth(object sender, EventArgs e) { ShiftMonth(-1); }
        private void OnNextMonth(object sender, EventArgs e) { ShiftMonth(1); }

        private void ShiftMonth(int m)
        {
            if (_busy) return;
            DateTime d = dtpDate.Value;
            DateTime t = new DateTime(d.Year, d.Month, 1).AddMonths(m);
            int last = DateTime.DaysInMonth(t.Year, t.Month);
            SetDate(new DateTime(t.Year, t.Month, Math.Min(d.Day, last)));
            Requery();
        }

        private void OnDateChanged(object sender, EventArgs e)
        {
            if (_dateLock || _busy) return;
            Requery();
        }

        /// <summary>이미 조회한 부번이 있으면 같은 계열로 다시 조회한다.</summary>
        private void Requery()
        {
            if (_searchedPtno.Length > 0 && PartNo.Key(txtPart.Text) == _searchedPtno)
                Search(_searchedPtno);
        }

        private void SetDate(DateTime d)
        {
            _dateLock = true;
            try { dtpDate.Value = d.Date; }
            finally { _dateLock = false; }
        }

        private static bool TryDate(string s, out DateTime d)
        {
            d = DateTime.Today;
            if (s == null) return false;
            s = s.Replace("-", "").Trim();
            if (s.Length < 8) return false;
            try
            {
                d = new DateTime(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(4, 2)),
                                 int.Parse(s.Substring(6, 2)));
                return true;
            }
            catch { return false; }
        }

        // ------------------------------------------------------------------
        // fn_SearchLep : 부번 -> 계열
        // ------------------------------------------------------------------
        private void SearchLep()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_NOLEP, "부품번호를 입력하십시오.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            txtPart.Text = PartNo.Display(ptno);
            ClearResult();

            Begin("조회중...");
            Async.Run(this,
                delegate { return PartHistService.SearchLep(ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;
                    if (leps.Count == 0)
                    {
                        SetLep("H");
                        Report(MP_NOSTOCK, "해당 부품에 대한 재고정보가 없습니다.", MsgLevel.Warn);
                        FocusPart();
                        return;
                    }

                    int idx = HaimsPda.Controls.LepSelect.Pick(
                                  leps, "계열 선택 - " + PartNo.Display(ptno));
                    if (idx < 0) { End("취소했습니다.", MsgLevel.Info); FocusPart(); return; }

                    SetLep((string)leps[idx]);
                    _busy = false;      // Search 가 다시 Begin 한다
                    Search(ptno);
                });
        }

        // ------------------------------------------------------------------
        // fn_Search : 헤더 + 수불내역
        // ------------------------------------------------------------------
        private void Search(string ptno)
        {
            if (_busy) return;

            string lep = _lep;
            DateTime d = dtpDate.Value.Date;

            Begin("조회중...");
            Async.Run(this,
                delegate { return PartHistService.Search(lep, ptno, d); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    PartHistResult sr = (PartHistResult)r;
                    _searchedPtno = ptno;

                    // 원본은 결과 유무와 상관없이 그리드와 헤더를 채운다
                    if (sr.HasInfo)
                    {
                        if (sr.PartName.Length > 0) lblPartName.Text = sr.PartName;
                        if (sr.Grade.Length > 0) lblClass.Text = sr.Grade;
                    }
                    lblStdWh.Text = sr.StdWh;
                    lblStdLoc.Text = Loc.Display(sr.StdLoc);
                    lblPrevQty.Text = sr.PrevQty;
                    lblRegWhs.Text = sr.RegWhs;
                    lblTrsCnt.Text = sr.TrsCnt;
                    lblCurQty.Text = sr.CurQty;
                    lblCl.Text = sr.Cl;
                    lblPrice.Text = sr.Price;

                    FillGrid(sr.Rows);

                    if (sr.Rows.Count == 0)
                        End("수불 내역이 없습니다. (" + dtpDate.Value.ToString("yyyy-MM-dd") + ")", MsgLevel.Info);
                    else
                        End(CommonCache.Msg(MP_OK, "정상 조회되었습니다.") + " (" + sr.Rows.Count + "건)",
                            MsgLevel.Success);
                    FocusPart();
                });
        }

        private void FillGrid(ArrayList rows)
        {
            lstHist.BeginUpdate();
            try
            {
                lstHist.Items.Clear();
                for (int i = 0; i < rows.Count; i++)
                {
                    PartHistRow h = (PartHistRow)rows[i];
                    ListViewItem it = new ListViewItem(h.Gubun);
                    it.SubItems.Add(h.Date);
                    it.SubItems.Add(h.Whscd);
                    it.SubItems.Add(h.Qty);
                    it.SubItems.Add(h.Vendor);
                    lstHist.Items.Add(it);
                }
            }
            finally { lstHist.EndUpdate(); }
        }

        // ------------------------------------------------------------------
        // 버튼 (원본 OnBtnPart / fn_MoveLoc : 부번과 조회일을 들고 이동)
        // ------------------------------------------------------------------
        private void OnPart(object sender, EventArgs e) { GoWith(ScreenId.StockByPart); }   // [321]
        private void OnLoc(object sender, EventArgs e) { GoWith(ScreenId.LocRegister); }    // [410]

        private void GoWith(int screenId)
        {
            if (_busy) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            NavArgs a = new NavArgs();
            a.Set("LEP", _lep);
            a.Set("PTNO", ptno);
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            a.Set("DATE", dtpDate.Value.ToString("yyyyMMdd"));
            Shell.Navigate(screenId, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "화면을 지우시겠습니까?"))) { FocusPart(); return; }
            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void SetLep(string lep) { _lep = lep; lblPrefix.Text = lep; }

        private void FocusPart()
        {
            txtPart.Focus();
            txtPart.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[323] " + ScreenName,
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
            SetButtons(true);
            Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[323] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[323] " + ScreenName);
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
            btnPrev.Enabled = on;
            btnNext.Enabled = on;
            btnPart.Enabled = on;
            btnLoc.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearResult()
        {
            _searchedPtno = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            lblStdWh.Text = "";
            lblStdLoc.Text = "";
            lblPrevQty.Text = "";
            lblRegWhs.Text = "";
            lblTrsCnt.Text = "";
            lblCurQty.Text = "";
            lblCl.Text = "";
            lblPrice.Text = "";
            lstHist.Items.Clear();
        }

        /// <summary>원본 fn_Init : 조회일은 오늘, 계열 H</summary>
        private void ClearAll()
        {
            SetLep("H");
            txtPart.Text = "";
            SetDate(DateTime.Today);
            ClearResult();
        }
    }
}
