using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [441] 실시간창고이관(입고) : [440] 에서 이관 출고한 부품을 다른 창고에 입고한다.
    // 원본 웹화면 : /ui/ws/plus/PL441_W01.xml (메뉴 1D07 / P181), 매뉴얼 LOC관리 199~202p
    //
    //   부번 스캔(S01, TO_WHSCD = 입고 창고) -> [FROM] 출고창고/LOC/수량 표시
    //   -> [TO] 창고 선택(바꾸면 재조회) -> LOC 스캔(S02 확인. 입고 창고에 LOC 가 없으면 I01 로 등록)
    //   -> 수량 Enter (이관수량과 같아야 함, MP304) -> 저장(U01 ~ U04) -> 초기화
    //
    // 원본과 다르게 한 부분
    //   - 입고 창고에 LOC 가 없을 때 원본은 확인 없이 I01 로 LOC 를 등록한다. 여기서는 한 번 묻는다.
    //   - LOC 확인(등록)이 끝나야 저장한다 (원본은 수량 Enter 만으로 저장 가능).
    //   - 원본 fn_SaveLoc_After 는 성공 여부를 보지 않고 초기화한다. 여기서는 실패면 남겨 둔다.
    //   - 계열 팝업으로 골랐을 때도 TO_LOCNO 를 채운다 (원본 fn_popPart 는 누락).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S441_MigrateIn.Designer.cs 에서 관리한다.
    public sealed partial class S441_MigrateIn : ScreenBase
    {
        private const string MP_OK = "MP101";
        private const string MP_SAVED = "MP102";
        private const string MP_SAVEERR = "MP108";
        private const string MP_NOPART = "MP303";
        private const string MP_QTYDIFF = "MP304";  // 수량이 다릅니다
        private const string MP_NOLOC = "MP310";    // 로케이션을 확인하세요
        private const string MP_NODATA = "MP339";   // 이관 정보가 없습니다
        private const string MP_CLEAR = "MP503";
        private const string MP_WHSCHK = "MP589";   // 입고할 수 없는 창고

        private bool _busy;
        private bool _loading;
        private string _focus = "";      // PTNO / LOCNO
        private MigrateInItem _cur;
        private bool _locOk;

        public override int ScreenNo { get { return ScreenId.WhMigrateIn; } }
        public override string ScreenName { get { return "실시간창고이관(입고)"; } }

        public S441_MigrateIn()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadWarehouses(Arg(args, "PTNO"));
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        private void LoadWarehouses(string linkPtno)
        {
            Begin("창고 조회중...");
            Async.Run(this,
                delegate { return LocStockService.GetWarehouses(); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    _loading = true;
                    try
                    {
                        cboWh.Items.Clear();
                        for (int i = 0; i < list.Count; i++) cboWh.Items.Add(list[i]);
                        if (cboWh.Items.Count > 0) cboWh.SelectedIndex = 0;
                        SelectByText(cboWh, "M");
                    }
                    finally { _loading = false; }

                    End("부번을 스캔하세요.", MsgLevel.Info);

                    if (linkPtno.Length > 0) { txtPart.Text = PartNo.Display(linkPtno); Search(); }
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

        // 원본 selWHS onchange : 다시 조회
        private void OnWhChanged(object sender, EventArgs e)
        {
            if (_loading || _busy) return;
            if (PartNo.Key(txtPart.Text).Length > 0) Search();
        }

        // ------------------------------------------------------------------
        // 스캔 / 입력
        // ------------------------------------------------------------------
        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;

            if (_focus == "LOCNO" || (_focus.Length == 0 && _cur != null && !_locOk))
            {
                txtLocTo.Text = Loc.Display(data.Text);
                LocEnter();
            }
            else
            {
                txtPart.Text = PartNo.Display(data.Text);
                Search();
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

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Save();
        }

        private void OnQtyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        // ------------------------------------------------------------------
        // 조회 (원본 fn_SearchFromPTNO / fn_popPart)
        // ------------------------------------------------------------------
        private void Search()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            txtPart.Text = PartNo.Display(ptno);

            string wh = CurWh;

            Begin("조회중...");
            Async.Run(this,
                delegate { return MigrateInService.Search(wh, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    ClearResult();

                    if (list.Count == 0)
                    {
                        Report(MP_NODATA, "이관 입고할 정보가 없습니다.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }

                    int idx = HaimsPda.Controls.LepSelect.Pick(list, "계열 선택 - " + PartNo.Display(ptno));
                    if (idx < 0) { End("취소했습니다.", MsgLevel.Info); FocusSel(txtPart); return; }

                    MigrateInItem it = (MigrateInItem)list[idx];
                    _cur = it;
                    lblPrefix.Text = it.Lep;
                    lblClass.Text = it.Grade;
                    lblPartName.Text = it.PartName;
                    txtFromWh.Text = it.FromWh;
                    txtFromLoc.Text = Loc.Display(it.FromLoc);
                    txtExpQty.Text = it.Qty;
                    txtQty.Text = it.Qty;

                    // 원본 : 출고창고와 입고창고가 다르면 입고창고에 등록된 LOC 를 채워 준다
                    if (it.FromWh != wh) txtLocTo.Text = Loc.Display(it.ToLoc);

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다.") + " 입고할 LOC 를 스캔하세요.", MsgLevel.Success);
                    FocusSel(txtLocTo);
                });
        }

        // ------------------------------------------------------------------
        // TO LOC 확인 (원본 fn_SearchLoc / fn_InsertLoc)
        // ------------------------------------------------------------------
        private void LocEnter()
        {
            if (_busy) return;
            if (_cur == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            string loc = Loc.Key(txtLocTo.Text);
            if (loc.Length == 0) { Report(MP_NOLOC, "로케이션을 입력해주십시오.", MsgLevel.Warn); FocusSel(txtLocTo); return; }
            txtLocTo.Text = Loc.Display(loc);

            MigrateInItem it = _cur;
            string wh = CurWh;
            _locOk = false;

            Begin("LOC 확인중...");
            Async.Run(this,
                delegate { return MigrateInService.CheckLoc(wh, it); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    MigrateInLocCheck c = (MigrateInLocCheck)r;

                    // 이 창고에 이미 LOC 가 등록된 부품 : 같은 LOC 여야 한다
                    if (c.Locno.Length > 0)
                    {
                        if (c.Locno != loc)
                        {
                            ReportText(CommonCache.Msg(MP_NOLOC, "로케이션을 확인하세요.")
                                       + "\r\n등록된 LOC : " + Loc.Display(c.Locno), MsgLevel.Warn);
                            FocusSel(txtLocTo);
                            return;
                        }
                        _locOk = true;
                        End("LOC 확인. 수량을 확인하고 Enter 를 누르세요.", MsgLevel.Success);
                        FocusSel(txtQty);
                        return;
                    }

                    if (c.WhscdCheck == "N")
                    {
                        Report(MP_WHSCHK, "입고할 수 없는 창고입니다.", MsgLevel.Warn);
                        FocusSel(txtLocTo);
                        return;
                    }

                    End("", MsgLevel.Info);
                    if (!Confirm("입고 창고(" + wh + ")에 LOC 가 없습니다.\r\n" + txtLocTo.Text + " 로 등록하시겠습니까?"))
                    {
                        FocusSel(txtLocTo);
                        return;
                    }
                    InsertLoc(loc);
                });
        }

        private void InsertLoc(string loc)
        {
            MigrateInItem it = _cur;
            string wh = CurWh;

            Begin("LOC 등록중...");
            Async.Run(this,
                delegate { MigrateInService.InsertLoc(wh, it, loc); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_NOLOC, "로케이션을 확인하세요.") + "\r\n" + ex.Message, MsgLevel.Error);
                        FocusSel(txtLocTo);
                        return;
                    }

                    _locOk = true;
                    End("LOC 등록 완료. 수량을 확인하고 Enter 를 누르세요.", MsgLevel.Success);
                    FocusSel(txtQty);
                });
        }

        // ------------------------------------------------------------------
        // 저장 (원본 fn_SaveLoc)
        // ------------------------------------------------------------------
        private void Save()
        {
            if (_busy) return;
            if (_cur == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            if (!_locOk) { Msg("LOC 를 먼저 스캔하세요.", MsgLevel.Warn); FocusSel(txtLocTo); return; }

            string qty = txtQty.Text.Trim();
            if (qty.Length == 0) { FocusSel(txtQty); return; }
            if (qty != _cur.Qty.Trim()) { Report(MP_QTYDIFF, "이관수량과 다릅니다.", MsgLevel.Warn); FocusSel(txtQty); return; }

            MigrateInItem it = _cur;
            string wh = CurWh, loc = Loc.Key(txtLocTo.Text);

            Begin("저장중...");
            Async.Run(this,
                delegate { MigrateInService.Save(wh, it, loc, qty); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.") + "\r\n" + ex.Message, MsgLevel.Error);
                        FocusSel(txtQty);
                        return;
                    }

                    Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);
                    MessageBox.Show(CommonCache.Msg("CO000", "정상 처리되었습니다."), "[441] " + ScreenName);
                    txtPart.Text = "";
                    ClearResult();
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        // LOC : [410] LOC등록 (원본 OnBtnLoc - 출고창고 코드를 넘김)
        private void OnLoc(object sender, EventArgs e)
        {
            if (_busy) return;
            NavArgs a = LinkArgs();
            if (_cur != null) a.Set("WHSCD", _cur.FromWh);
            Shell.Navigate(ScreenId.LocRegister, a);
        }

        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;
            Shell.Navigate(ScreenId.StockByPart, LinkArgs());
        }

        private NavArgs LinkArgs()
        {
            NavArgs a = new NavArgs();
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length > 0)
            {
                a.Set("LEP", lblPrefix.Text);
                a.Set("PTNO", ptno);
                a.Set("PTNM", lblPartName.Text);
                a.Set("CLASS", lblClass.Text);
            }
            return a;
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "입력한 내용을 지우시겠습니까?"))) return;
            ClearAll();
            _loading = true;
            try { SelectByText(cboWh, "M"); }
            finally { _loading = false; }
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void ClearResult()
        {
            _cur = null;
            _locOk = false;
            lblPrefix.Text = "H";
            lblClass.Text = "";
            lblPartName.Text = "";
            txtFromWh.Text = "";
            txtFromLoc.Text = "";
            txtExpQty.Text = "";
            txtLocTo.Text = "";
            txtQty.Text = "";
        }

        private void ClearAll()
        {
            txtPart.Text = "";
            _focus = "";
            ClearResult();
        }

        private void FocusSel(TextBox t) { t.Focus(); t.SelectAll(); }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[441] " + ScreenName,
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

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK && code != MP_SAVED) MessageBox.Show(text, "[441] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[441] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnLoc.Enabled = on;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
            cboWh.Enabled = on;
        }
    }
}
