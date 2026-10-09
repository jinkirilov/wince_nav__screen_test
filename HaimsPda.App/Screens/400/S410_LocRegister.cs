using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [410] LOC등록 : 부품의 LOC 를 신규 등록하거나 변경한다.
    // 원본 웹화면 : /ui/ws/plus/PL410_W01.xml (메뉴 1D01 / P141), 매뉴얼 LOC관리 177~181p
    //
    //   창고/계열 선택 -> 부번 스캔(fn_Search) -> 등록 LOC 스캔 -> "등록하시겠습니까?" -> fn_Save
    //
    // 모드 버튼(원본 textbox30) : 등록용 -> 조회용 -> 확인용 -> 등록용
    //   등록용 : 부번 조회 후 커서가 등록LOC 로. 저장 후에는 부번으로.
    //   조회용 : 부번 조회 후 커서가 부번에 남는다 (연속 조회).
    //   확인용 : 등록LOC 에 스캔한 값이 현재 LOC 와 같은지 소리로만 알려 준다. 저장하지 않는다.
    //
    // 원본과 다르게 한 부분
    //   - 출고대기(D/O)가 있으면 저장 전에 막는다 (매뉴얼 181p 의 "출고예정 수량이 있어..." 를 클라이언트에서도 확인).
    //   - 현재 LOC 와 같은 LOC 를 스캔하면 저장하지 않고 알려만 준다.
    //   - 계열을 바꾸면 조회 결과를 버리고 부번이 있으면 다시 조회한다 (원본은 콤보만 바뀌어 다른 계열로 저장될 수 있음).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S410_LocRegister.Designer.cs 에서 관리한다.
    public sealed partial class S410_LocRegister : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";    // 정상 저장되었습니다
        private const string MP_SAVEERR = "MP108";  // 저장중 에러가 발생하였습니다
        private const string MP_NOPART = "MP303";   // 부품번호를 확인하세요
        private const string MP_NOLOC = "MP310";    // 로케이션을 확인하세요
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까
        private const string MP_REGLOC = "MP598";   // 로케이션을 등록하시겠습니까

        private const string ModeReg = "등록용";
        private const string ModeView = "조회용";
        private const string ModeCheck = "확인용";

        [DllImport("coredll.dll")]
        private static extern bool MessageBeep(uint type);

        private bool _busy;
        private bool _loading;              // 콤보 채우는 중 (변경 이벤트 무시)
        private string _focus = "";         // 원본 GV_FocusGbn : PTNO / LOCNO
        private string _mode = ModeReg;

        private LocRegInfo _info;           // 마지막 조회 결과
        private string _searchedPtno = "";  // 원본 GV_SelectSave : 조회한 부번과 저장할 부번이 같아야 한다
        private string _searchedLep = "";
        private bool _afterSave;            // 원본 GV_SAVEYN
        private bool _returnAfterSave;      // 다른 화면에서 RETURN=Y 로 들어오면 저장 후 돌아간다 (원본 SCRNO=1A04)

        public override int ScreenNo { get { return ScreenId.LocRegister; } }
        public override string ScreenName { get { return "LOC등록"; } }

        public S410_LocRegister()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            string mode = Arg(args, "DETAIL_INFO");
            _mode = (mode == ModeView || mode == ModeCheck) ? mode : ModeReg;
            _returnAfterSave = (Arg(args, "RETURN") == "Y");

            ClearAll();
            btnMode.Text = _mode;

            string ptno = Arg(args, "PTNO");
            if (ptno.Length > 0)
            {
                txtPart.Text = PartNo.Display(ptno);
                lblClass.Text = Arg(args, "CLASS");
                lblPartName.Text = Arg(args, "PTNM");
            }

            LoadCombos(Arg(args, "WHSCD"), Arg(args, "LEP"), ptno);
        }

        // [330] 에서 저장하고 돌아오면 같은 부번을 다시 조회한다
        public override void OnReturn(NavArgs result)
        {
            if (!NavResult.IsSaved(result) || _busy) return;
            if (PartNo.Key(txtPart.Text).Length == 0) return;
            Search();
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        /// <summary>원본 FV_HK_VALUE : USR_HK 가 C 면 H</summary>
        private static string DefaultLep()
        {
            UserInfo u = Session.User;
            string hk = (u == null) ? null : u["USR_HK"];
            hk = (hk == null) ? "" : hk.Trim();
            if (hk.Length == 0 || hk == "C") return "H";
            return hk;
        }

        // ------------------------------------------------------------------
        // 창고 / 계열 콤보 (원본 gfn_SearchWHS_Plus / gfn_SearchLEP_Plus)
        // ------------------------------------------------------------------
        private void LoadCombos(string linkWh, string linkLep, string linkPtno)
        {
            Begin("조회중...");
            Async.Run(this,
                delegate
                {
                    ArrayList[] r = new ArrayList[2];
                    r[0] = LocStockService.GetWarehouses();
                    r[1] = PartInfoService.GetLeps();
                    return r;
                },
                delegate(object o, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList[] r = (ArrayList[])o;
                    _loading = true;
                    try
                    {
                        cboWh.Items.Clear();
                        for (int i = 0; i < r[0].Count; i++) cboWh.Items.Add(r[0][i]);
                        if (cboWh.Items.Count > 0) cboWh.SelectedIndex = 0;
                        SelectByText(cboWh, "M");
                        SelectByText(cboWh, linkWh);

                        cboLep.Items.Clear();
                        for (int i = 0; i < r[1].Count; i++) cboLep.Items.Add(r[1][i]);
                        if (cboLep.Items.Count > 0) cboLep.SelectedIndex = 0;
                        SelectByText(cboLep, DefaultLep());
                        SelectByText(cboLep, linkLep);
                    }
                    finally { _loading = false; }

                    End("부번을 스캔/입력하세요.", MsgLevel.Info);

                    if (linkPtno.Length > 0) Search();
                    else txtPart.Focus();
                });
        }

        private static void SelectByText(ComboBox cbo, string text)
        {
            if (text == null || text.Length == 0) return;
            for (int i = 0; i < cbo.Items.Count; i++)
                if (cbo.Items[i].ToString() == text) { cbo.SelectedIndex = i; return; }
        }

        private string CurWh
        {
            get { return (cboWh.SelectedIndex < 0) ? "M" : cboWh.Items[cboWh.SelectedIndex].ToString(); }
        }

        private string CurLep
        {
            get { return (cboLep.SelectedIndex < 0) ? DefaultLep() : cboLep.Items[cboLep.SelectedIndex].ToString(); }
        }

        // 원본 selWHS onchange : 등록LOC 를 비우고 다시 조회
        private void OnWhChanged(object sender, EventArgs e)
        {
            if (_loading || _busy) return;
            txtLoc.Text = "";
            if (PartNo.Key(txtPart.Text).Length > 0) Search();
        }

        private void OnLepChanged(object sender, EventArgs e)
        {
            if (_loading || _busy) return;
            ClearResult();
            if (PartNo.Key(txtPart.Text).Length > 0) Search();
            else txtPart.Focus();
        }

        // 등록용 -> 조회용 -> 확인용
        private void OnMode(object sender, EventArgs e)
        {
            if (_mode == ModeReg) _mode = ModeView;
            else if (_mode == ModeView) _mode = ModeCheck;
            else _mode = ModeReg;
            btnMode.Text = _mode;
            txtPart.Focus();
        }

        // ------------------------------------------------------------------
        // 스캔 / 입력 (원본 fn_Barcode : 부번 포커스거나 포커스가 없으면 부번)
        // ------------------------------------------------------------------
        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;

            if (_focus == "LOCNO")
            {
                txtLoc.Text = Loc.Display(data.Text);
                LocEnter();
            }
            else
            {
                txtPart.Text = PartNo.Display(data.Text);
                if (PartNo.Key(txtPart.Text).Length > 0) Search();
            }
        }

        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }
        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnOtherFocus(object sender, EventArgs e) { _focus = ""; }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Search();
        }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocEnter();
        }

        // ------------------------------------------------------------------
        // 조회 (원본 fn_Search / fn_AfterSearch)
        // ------------------------------------------------------------------
        private void Search()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            txtPart.Text = PartNo.Display(ptno);

            string wh = CurWh, lep = CurLep;

            Begin("조회중...");
            Async.Run(this,
                delegate { return LocRegisterService.Search(wh, lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    LocRegInfo info = (LocRegInfo)r;
                    if (!info.Found)
                    {
                        ClearResult();
                        Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }

                    ShowInfo(info);
                    _searchedPtno = ptno;
                    _searchedLep = lep;
                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);

                    if (_mode == ModeView || (_mode == ModeReg && _afterSave))
                    {
                        _afterSave = false;
                        FocusSel(txtPart);
                    }
                    else
                        txtLoc.Focus();
                });
        }

        private void ShowInfo(LocRegInfo i)
        {
            _info = i;
            lblClass.Text = i.Grade;
            lblPartName.Text = i.PartName;
            txtAvl.Text = i.AvlQty;
            txtIn.Text = i.InQty;
            txtOut.Text = i.OutQty;

            // 원본 : 첫 줄은 조회 계열의 LOC, 둘째 줄은 반대 계열(H<->K). 그 밖의 계열은 둘째 줄 비움
            lblLoc1Cap.Text = "LOC_" + i.Lep;
            txtLoc1.Text = (i.Locno.Length == 0) ? "NONE" : Loc.Display(i.Locno);
            txtLocQty1.Text = i.AvlQty;

            if (i.Lep == "H" || i.Lep == "K")
            {
                lblLoc2Cap.Text = (i.Lep == "H") ? "LOC_K" : "LOC_H";
                txtLoc2.Text = (i.Locno2.Length == 0) ? "NONE" : Loc.Display(i.Locno2);
                txtLocQty2.Text = i.AvlQty2;
            }
            else
            {
                lblLoc2Cap.Text = "";
                txtLoc2.Text = "";
                txtLocQty2.Text = "";
            }
        }

        // ------------------------------------------------------------------
        // 등록 LOC (원본 inptLOC onkeydown)
        // ------------------------------------------------------------------
        private void LocEnter()
        {
            if (_busy) return;

            if (PartNo.Key(txtPart.Text).Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            string loc = Loc.Key(txtLoc.Text);
            if (loc.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLoc); return; }
            txtLoc.Text = Loc.Display(loc);

            if (_mode == ModeCheck) { CheckOnly(loc); return; }

            if (!CanSave(loc)) return;

            string q = CommonCache.Msg(MP_REGLOC, "${} 로케이션을 등록하시겠습니까?", new string[] { txtLoc.Text });
            if (!Confirm(q)) { FocusSel(txtLoc); return; }

            Save(loc);
        }

        // 확인용 : 현재 LOC 와 같으면 성공음, 다르면 실패음 (원본 gfn_AppCall("sound", "s"/"f"))
        private void CheckOnly(string loc)
        {
            string cur = (_info == null) ? "" : _info.Locno;
            if (cur.Length > 0 && cur == loc)
            {
                MessageBeep(0x00000000);
                Msg("LOC 일치", MsgLevel.Success);
                txtLoc.Text = "";
                FocusSel(txtPart);
            }
            else
            {
                MessageBeep(0x00000010);
                Msg("LOC 불일치 - 현재 " + ((cur.Length == 0) ? "NONE" : Loc.Display(cur)), MsgLevel.Error);
                FocusSel(txtLoc);
            }
        }

        private bool CanSave(string loc)
        {
            string ptno = PartNo.Key(txtPart.Text);
            if (_info == null || ptno != _searchedPtno || CurLep != _searchedLep)
            {
                Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn);
                FocusSel(txtPart);
                return false;
            }

            if (_info.DoFlag != "I" && _info.DoFlag != "U")
            {
                ReportText("등록할 수 없는 부품입니다. (DO_FLAG=" + _info.DoFlag + ")", MsgLevel.Warn);
                return false;
            }

            if (ToInt(_info.OutQty) > 0)
            {
                ReportText("출고예정 수량이 있어 로케이션 등록을 할 수 없습니다.", MsgLevel.Warn);
                FocusSel(txtLoc);
                return false;
            }

            if (_info.Locno.Length > 0 && _info.Locno == loc)
            {
                Msg("이미 등록된 LOC 입니다.", MsgLevel.Info);
                txtLoc.Text = "";
                FocusSel(txtPart);
                return false;
            }
            return true;
        }

        // 원본 fn_Save / fn_Save_After
        private void Save(string loc)
        {
            LocRegInfo i = _info;
            string wh = CurWh, lep = _searchedLep, ptno = _searchedPtno;

            Begin("저장중...");
            Async.Run(this,
                delegate { LocRegisterService.Save(i.DoFlag, wh, lep, ptno, loc, i.Locno, i.AvlQty); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.") + "\r\n" + ex.Message, MsgLevel.Error);
                        FocusSel(txtLoc);
                        return;
                    }

                    Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);
                    txtLoc.Text = "";

                    if (_returnAfterSave && Shell != null) { Shell.GoBack(); return; }

                    _afterSave = true;
                    Search();      // 바뀐 LOC 를 다시 보여 준다
                });
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        // 재고 : [321] 파트별재고 (원본 fn_OnBtnStock)
        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;
            if (PartNo.Key(txtPart.Text).Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            Shell.Navigate(ScreenId.StockByPart, LinkArgs());
        }

        // 조정 : [330] 재고조정 (원본 fn_OnBtnAdjust)
        private void OnAdjust(object sender, EventArgs e)
        {
            if (_busy) return;
            if (PartNo.Key(txtPart.Text).Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            NavArgs a = LinkArgs();
            a.Set("LOCNO", (_info == null) ? "" : _info.Locno);
            a.Set("QTY", txtAvl.Text);
            a.Set("WHSCD", CurWh);
            Shell.Navigate(ScreenId.StockAdjust, a);
        }

        private NavArgs LinkArgs()
        {
            NavArgs a = new NavArgs();
            a.Set("LEP", CurLep);
            a.Set("PTNO", PartNo.Key(txtPart.Text));
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            a.Set("DETAIL_INFO", _mode);
            return a;
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "입력한 내용을 지우시겠습니까?"))) return;

            ClearAll();
            _loading = true;
            try { SelectByText(cboWh, "M"); SelectByText(cboLep, DefaultLep()); }
            finally { _loading = false; }
            _mode = ModeReg;
            btnMode.Text = _mode;
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void ClearResult()
        {
            _info = null;
            _searchedPtno = "";
            _searchedLep = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            lblLoc1Cap.Text = "LOC_H";
            lblLoc2Cap.Text = "LOC_K";
            txtLoc1.Text = ""; txtLocQty1.Text = "";
            txtLoc2.Text = ""; txtLocQty2.Text = "";
            txtAvl.Text = "";
            txtLoc.Text = "";
            txtIn.Text = "";
            txtOut.Text = "";
        }

        private void ClearAll()
        {
            txtPart.Text = "";
            _afterSave = false;
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

        private void FocusSel(TextBox t)
        {
            t.Focus();
            t.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[410] " + ScreenName,
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
            if (msg != null && msg.Length > 0) Msg(msg, lv);
        }

        /// <summary>MP101/MP102 가 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK && code != MP_SAVED) MessageBox.Show(text, "[410] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[410] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnMode.Enabled = on;
            btnStock.Enabled = on;
            btnAdjust.Enabled = on;
            btnClear.Enabled = on;
            cboWh.Enabled = on;
            cboLep.Enabled = on;
        }
    }
}
