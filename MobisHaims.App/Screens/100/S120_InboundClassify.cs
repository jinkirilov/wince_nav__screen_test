using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Controls;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [120] 일반입고분류 / [121] 전문점입고분류
    // 원본 웹화면 : /ui/ws/plus/PL120_W01.xml , PL121_W01.xml
    //
    // 두 원본은 거의 같은 소스다. 갈리는 부분은 ClassifyMode 에 모아 두고
    // 화면 클래스는 하나만 쓴다. 파생 클래스 두 개가 각각 모드를 지정한다.
    //
    //   fn_SearchLep -> fn_SearchClass -> fn_Search -> (fn_SaveChk -> fn_Save -> fn_SaveAfter)
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S120_InboundClassify.Designer.cs 에서 관리한다.
    public partial class S120_InboundClassify : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";   // 정상 처리되었습니다
        private const string MP_PTNO = "MP303";    // 부품번호를 확인하세요
        private const string MP_NOLEP = "MP540";   // 부품번호를 입력하십시오
        private const string MP_NOALLOC = "MP532"; // 할당정보가 없는 품목입니다
        private const string MP_NOQTY = "MP528";   // 수량을 확인하세요
        private const string MP_NOLOC = "MP527";   // LOC 를 확인하세요
        private const string MP_NEEDLOC = "MP521"; // 로케이션을 등록해야 합니다

        private readonly ClassifyMode _mode;

        private string _lep = "H";
        private string _searchedPtno = "";
        private ClassifySearchResult _search;
        private ClassInfo _class;
        private bool _busy;

        public override int ScreenNo { get { return _mode.ScreenNo; } }
        public override string ScreenName { get { return _mode.Name; } }

        protected S120_InboundClassify(ClassifyMode mode)
        {
            _mode = mode;
            InitializeComponent();
            if (IsDesignMode) return;

            // 업체 표시는 전문점(121)에만 있다
            lblVenCap.Visible = _mode.ShowVendor;
            lblVen.Visible = _mode.ShowVendor;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string ptno = (args == null) ? null : args.GetString("PTNO");
            if (ptno != null && ptno.Length > 0)
            {
                string lep = args.GetString("LEP");
                if (lep != null && lep.Length > 0) { _lep = lep; lblPrefix.Text = lep; }
                txtPart.Text = PartNo.Display(ptno);
                SearchLep();
                return;
            }

            txtPart.Focus();
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
        }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            txtPart.Text = PartNo.Display(data.Text);
            SearchLep();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchLep(); }
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            SyncSaveQty();
            OnSave(null, null);
        }

        /// <summary>할당수량을 고치면 저장수량도 따라간다 (원본 inptSAVE).</summary>
        private void SyncSaveQty()
        {
            txtSaveQty.Text = txtWsfQty.Text.Trim();
        }

        // ------------------------------------------------------------------
        // 1단계 : 부번 -> 계열 (fn_SearchLep / PL100_W01_S01)
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

            txtPart.Text = PartNo.Display(txtPart.Text);
            ClearResult();

            ClassifyMode m = _mode;

            Begin("조회중...");
            Async.Run(this,
                delegate { return ClassifyService.SearchLep(m, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;
                    if (leps.Count == 0)
                    {
                        _lep = "H";
                        lblPrefix.Text = "H";
                        Report("MP307", "할당 정보가 없는 품목입니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    int idx = LepSelect.Pick(leps, "계열 선택 - " + PartNo.Display(txtPart.Text));
                    if (idx < 0)
                    {
                        End("취소했습니다.", MsgLevel.Info);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    _lep = (string)leps[idx];
                    lblPrefix.Text = _lep;

                    SearchClass(ptno);
                });
        }

        // ------------------------------------------------------------------
        // 2단계 : 품명 / 등급 (fn_SearchClass)
        //
        // 결과가 없으면 원본은 재고를 만들고(fn_SaveInv) 다시 이 단계로 돌아온다.
        // ------------------------------------------------------------------
        private void SearchClass(string ptno)
        {
            SearchClass(ptno, true);
        }

        private void SearchClass(string ptno, bool allowSaveInv)
        {
            ClassifyMode m = _mode;
            string lep = _lep;

            Async.Run(this,
                delegate { return ClassifyService.SearchClass(m, lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassInfo info = (ClassInfo)r;
                    if (!info.Found)
                    {
                        if (!allowSaveInv)
                        {
                            Report(MP_NOALLOC, "할당 정보가 없는 품목입니다.", MsgLevel.Warn);
                            txtPart.Focus();
                            return;
                        }
                        SaveInv(ptno);
                        return;
                    }

                    _class = info;
                    lblPartName.Text = info.PartName;
                    lblClass.Text = info.Grade;

                    SearchDetail(ptno);
                });
        }

        /// <summary>재고 등록 후 등급 조회를 한 번 더 한다 (fn_SaveInv_After).</summary>
        private void SaveInv(string ptno)
        {
            ClassifyMode m = _mode;
            string lep = _lep;

            Msg("재고 등록중...", MsgLevel.Info);
            Async.Run(this,
                delegate { ClassifyService.SaveInv(m, lep, ptno); return null; },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    SearchClass(ptno, false);   // 무한 반복 방지
                });
        }

        // ------------------------------------------------------------------
        // 3단계 : 화면정보 + 할당목록 (fn_Search)
        // ------------------------------------------------------------------
        private void SearchDetail(string ptno)
        {
            ClassifyMode m = _mode;
            string lep = _lep;

            Async.Run(this,
                delegate { return ClassifyService.Search(m, lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassifySearchResult sr = (ClassifySearchResult)r;
                    if (!sr.HasScreen)
                    {
                        Report(MP_NOALLOC, "조회대상 데이터가 존재하지 않습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    _search = sr;
                    _searchedPtno = ptno;

                    txtCurInv.Text = sr.S("CURINV");
                    txtNar.Text = sr.S("NARQT");
                    txtCnt.Text = sr.S("WSFCNT");
                    txtWsfQty.Text = sr.S("WSFQT");
                    SyncSaveQty();

                    string rsv = sr.S("RSVQT");
                    txtRsv.Text = rsv;

                    // 예약수량이 있으면 원본은 빨강으로 표시하고 예약조회 체크를 열어 준다
                    bool hasRsv = ToInt(rsv) > 0;
                    txtRsv.ForeColor = hasRsv ? System.Drawing.Color.Red
                                              : System.Drawing.Color.Black;
                    chkSale.Enabled = hasRsv;
                    chkSale.Checked = false;

                    // 121 은 업체를 함께 보여 준다
                    if (_mode.ShowVendor && sr.HasAlloc)
                    {
                        Row a = sr.FirstAlloc;
                        lblVen.Text = a["WSF_VNDMN"] + "  " + a["VEN_VNDNM"];
                    }

                    string loc = sr.S("LOC_LOCNO");
                    if (loc.Length == 0)
                    {
                        // 원본 : LOC 가 없으면 등록 화면으로 보낸다
                        lblLoc.Text = "NONE";
                        lblLoc.ForeColor = System.Drawing.Color.Green;
                        End(CommonCache.Msg(MP_NEEDLOC,
                            "할당된 부품의 로케이션이 없습니다. 로케이션을 등록해야 합니다."),
                            MsgLevel.Warn);

                        if (Confirm(CommonCache.Msg(MP_NEEDLOC,
                                "할당된 부품의 로케이션이 없습니다.\r\n로케이션을 등록하시겠습니까?")))
                            GoLoc();
                        return;
                    }

                    lblLoc.Text = Loc.Display(loc);
                    lblLoc.ForeColor = System.Drawing.Color.Black;

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);
                    txtWsfQty.Focus();
                    txtWsfQty.SelectAll();
                });
        }

        // ------------------------------------------------------------------
        // 분류 저장 (fn_SaveChk -> fn_Save -> fn_SaveAfter)
        // ------------------------------------------------------------------
        private void OnSave(object sender, EventArgs e)
        {
            if (_busy) return;

            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (txtWsfQty.Text.Trim().Length == 0)
            {
                Report(MP_NOQTY, "수량을 확인하세요.", MsgLevel.Warn);
                txtWsfQty.Focus();
                return;
            }
            if (lblLoc.Text.Trim().Length == 0 || lblLoc.Text == "NONE")
            {
                Report(MP_NOLOC, "LOC 를 확인하세요.", MsgLevel.Warn);
                return;
            }
            if (_search == null || !_search.HasAlloc)
            {
                ReportText("재조회 후 저장하세요.", MsgLevel.Warn);
                return;
            }
            if (PartNo.Key(txtPart.Text) != _searchedPtno)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                txtPart.Focus();
                txtPart.SelectAll();
                return;
            }

            // 다건 할당은 원본이 대기품목 팝업(PL130_P01)이 넘겨준 dsInput 을 쓴다.
            // 그 팝업이 없으므로 막는다. 되돌릴 수 없는 쓰기라 추측으로 보내지 않는다.
            if (_search.Allocs.Count > 1)
            {
                ReportText("할당내역이 " + _search.Allocs.Count + "건입니다.\r\n"
                         + "다건 분류는 할당내역 선택 화면이 있어야 합니다.", MsgLevel.Warn);
                return;
            }

            SyncSaveQty();

            ClassifyMode m = _mode;
            string lep = _lep;
            string ptno = _searchedPtno;
            ArrayList allocs = _search.Allocs;

            Begin("확인중...");
            Async.Run(this,
                delegate { return ClassifyService.SaveCheck(m, lep, ptno, allocs); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassifyCheckResult c = (ClassifyCheckResult)r;

                    if (!c.Found || c.TotCnt <= 0)
                    {
                        ClearAll();
                        ReportText("입고 또는 분류되었거나 할당정보가 없는 부번입니다.", MsgLevel.Error);
                        return;
                    }
                    if (allocs.Count != c.TotCnt)
                    {
                        ClearAll();
                        ReportText("입고 또는 분류된 할당내역이 있습니다. 다시 조회 후 저장하세요.", MsgLevel.Error);
                        return;
                    }
                    if (c.VchnoCnt > 0)
                    {
                        ClearAll();
                        ReportText("입고 또는 분류된 할당내역입니다. 헬프데스크에 문의하세요.", MsgLevel.Error);
                        return;
                    }

                    Commit();
                });
        }

        private void Commit()
        {
            Row a = _search.FirstAlloc;

            CommitInput c = CommitInput.From(a);
            c.Lep = _lep;
            c.Ptno = _searchedPtno;
            c.WsfQt = txtWsfQty.Text.Trim();     // 원본도 화면 입력값을 쓴다
            c.Whscd = a["LOC_WHSCD"];            // 화면 콤보가 아니라 할당의 창고코드
            c.VapSysdt = (_class == null) ? "" : _class.VapSysdt;
            c.VapSysdtL = (_class == null) ? "" : _class.VapSysdtL;
            c.Vchym = (_class == null) ? "" : _class.Vchym;
            c.CtlQty = 0;      // 미수령 화면 미구현
            c.CtlCd = "";

            if (!Confirm("분류 저장하시겠습니까?\r\n"
                       + "부번 " + PartNo.Display(c.Ptno) + " / 수량 " + c.WsfQt
                       + " / LOC " + lblLoc.Text))
                return;

            ClassifyMode m = _mode;
            bool wantRsv = chkSale.Enabled && chkSale.Checked;

            Begin("저장중...");
            Async.Run(this,
                delegate
                {
                    CommitPrepare p = ClassifyService.CommitPrepareCall(m, c);
                    ClassifyService.Commit(m, c, p);
                    return p;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    CommitPrepare p = (CommitPrepare)r;

                    string msg = CommonCache.Msg(MP_SAVED, "정상 처리되었습니다.");
                    if (p.Vchno.Length > 0) msg += "  (증표 " + p.Vchno + ")";

                    string ptno = _searchedPtno;
                    string lep = _lep;

                    ClearAll();
                    End(msg, MsgLevel.Success);
                    MessageBox.Show(msg, "[" + _mode.ScreenNo + "] " + ScreenName);

                    // fn_SaveAfter2 : 예약조회 체크가 켜져 있으면 예약내역으로 넘어간다
                    if (wantRsv && Shell != null)
                    {
                        NavArgs na = new NavArgs();
                        na.Set("PTNO", ptno);
                        na.Set("LEP", lep);
                        Shell.Navigate(ScreenId.ReserveList, na);
                        return;
                    }

                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        private void OnLoc(object sender, EventArgs e)
        {
            if (_busy) return;

            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            GoLoc();
        }

        /// <summary>원본 fn_MoveLoc : [410] LOC등록으로 부번을 들고 이동</summary>
        private void GoLoc()
        {
            if (Shell == null) return;

            NavArgs a = new NavArgs();
            a.Set("PTNO", _searchedPtno.Length > 0 ? _searchedPtno : PartNo.Key(txtPart.Text));
            a.Set("LEP", _lep);
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            Shell.Navigate(ScreenId.LocRegister, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm("화면을 CLEAR 하시겠습니까?")) return;

            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        private void ClearResult()
        {
            lblClass.Text = "";
            lblPartName.Text = "";
            lblVen.Text = "";
            lblLoc.Text = "";
            lblLoc.ForeColor = System.Drawing.Color.Black;
            txtCurInv.Text = "";
            txtRsv.Text = "";
            txtRsv.ForeColor = System.Drawing.Color.Black;
            txtNar.Text = "";
            txtCnt.Text = "";
            txtWsfQty.Text = "";
            txtSaveQty.Text = "";
            chkSale.Checked = false;
            chkSale.Enabled = false;
            _search = null;
            _class = null;
            _searchedPtno = "";
        }

        private void ClearAll()
        {
            _lep = "H";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            ClearResult();
        }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[" + _mode.ScreenNo + "] " + ScreenName,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2) == DialogResult.Yes;
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
            if (code != MP_OK) MessageBox.Show(text, "[" + _mode.ScreenNo + "] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[" + _mode.ScreenNo + "] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSave.Enabled = on;
            btnLoc.Enabled = on;
            btnClear.Enabled = on;
        }
    }

    /// <summary>[120] 일반입고분류</summary>
    public sealed class S120_SiteInboundClassify : S120_InboundClassify
    {
        public S120_SiteInboundClassify() : base(ClassifyMode.Site) { }
    }

    /// <summary>[121] 전문점입고분류</summary>
    public sealed class S121_ShopInboundClassify : S120_InboundClassify
    {
        public S121_ShopInboundClassify() : base(ClassifyMode.Shop) { }
    }
}
