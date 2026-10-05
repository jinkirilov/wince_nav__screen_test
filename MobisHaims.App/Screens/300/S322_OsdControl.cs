using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [322] 통제등록(OS&D) : [320]/[321]/[3201] 에서 선택한 LOC 한 건에 통제(결함) 수량을 등록한다.
    // 원본 웹화면 : /ui/ws/plus/PL212_W01.xml (서버 메뉴 P138, 원본 화면번호 212)
    //
    //   OnEnter(LEP/PTNO/PTNM/CLASS/LOCNO/WHSCD/EXPECTQTY)
    //     -> 사유 콤보 (common:CODESEARCH MP/41)
    //     -> 처리수량 입력 -> fn_CheckQty -> fn_SaveDEFQT (PL211_W01_I01/U01/U02)
    //
    // 부번/LOC/LOC수량은 넘어온 값만 보여주고 입력은 처리수량과 사유뿐이다.
    // 좌표/크기/색/폰트/TabIndex 는 전부 S322_OsdControl.Designer.cs 에서 관리한다.
    public sealed partial class S322_OsdControl : ScreenBase
    {
        private const string MP_OK = "CO000";      // 정상 처리되었습니다
        private const string MP_SAVEERR = "MP108"; // 저장중 에러가 발생하였습니다
        private const string MP_CONFIRM = "MP516"; // 저장 확인 (인자 : LOC수량, 처리수량)
        private const string MP_OVER = "MP559";    // OS&D수량이 출고대상수량보다 많습니다
        private const string MP_ZERO = "MP613";    // 수량에 공백이나 0을 입력할 수 없습니다

        private string _lep = "H";
        private string _ptno = "";
        private string _whscd = "";
        private bool _busy;
        private bool _saved;      // 저장 후에는 LOC수량이 바뀌므로 재진입 전까지 저장 차단

        public override int ScreenNo { get { return ScreenId.OsdControl; } }
        public override string ScreenName { get { return "통제등록"; } }

        public S322_OsdControl()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string lep = Arg(args, "LEP");
            if (lep.Length > 0) _lep = lep;
            lblPrefix.Text = _lep;

            _ptno = PartNo.Key(Arg(args, "PTNO"));
            _whscd = Arg(args, "WHSCD");

            txtPart.Text = PartNo.Display(_ptno);
            lblClass.Text = Arg(args, "CLASS");
            lblPartName.Text = Arg(args, "PTNM");
            txtLoc.Text = Loc.Display(Arg(args, "LOCNO"));
            txtObjQty.Text = Arg(args, "EXPECTQTY");

            if (_ptno.Length == 0)
            {
                SetButtons(false);
                btnClear.Enabled = true;
                Msg("[320]/[321] 에서 LOC 를 선택해 진입하세요.", MsgLevel.Warn);
                return;
            }

            LoadReasons();
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            // 원본도 이 화면에서 스캔 입력을 받지 않는다.
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 사유 콤보 : 원본 fn_SearchCDM
        // ------------------------------------------------------------------
        private void LoadReasons()
        {
            Begin("조회중...");
            Async.Run(this,
                delegate { return OsdService.GetReasons(); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    cboReason.Items.Clear();
                    for (int n = 0; n < list.Count; n++) cboReason.Items.Add(list[n]);
                    if (cboReason.Items.Count > 0) cboReason.SelectedIndex = 0;

                    End("처리수량을 입력하세요.", MsgLevel.Info);
                    txtOsdQty.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 입력 이벤트
        // ------------------------------------------------------------------
        private void OnOsdKeyPress(object sender, KeyPressEventArgs e)
        {
            // 숫자와 BackSpace 만 허용 (KeyChar 대입은 CF 에서 안 먹지만 Handled 는 동작한다)
            if (e.KeyChar == (char)Keys.Back) return;
            if (e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void OnOsdKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            if (CheckQty(true)) cboReason.Focus();
        }

        private void OnOsdTextChanged(object sender, EventArgs e)
        {
            // 입력 중에는 경고 없이 수정후수량만 미리 계산한다
            CheckQty(false);
        }

        private void OnReasonKeyDown(object sender, KeyEventArgs e)
        {
            // 원본 : 사유 Enter -> 수정후수량 Enter -> fn_CheckQty("SAVE")
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Save();
        }

        /// <summary>원본 fn_CheckQty : 검증 + 수정후수량 계산. warn=false 면 메시지 없이 계산만.</summary>
        private bool CheckQty(bool warn)
        {
            int obj = ToInt(txtObjQty.Text);
            int osd = ToInt(txtOsdQty.Text);

            if (osd <= 0)
            {
                txtDoQty.Text = "";
                if (warn) { Report(MP_ZERO, "수량에 공백이나 0을 입력할 수 없습니다.", MsgLevel.Warn); FocusQty(); }
                return false;
            }
            if (osd > obj)
            {
                txtDoQty.Text = "";
                if (warn) { Report(MP_OVER, "OS&D수량이 출고대상수량보다 많습니다.", MsgLevel.Warn); FocusQty(); }
                return false;
            }

            txtDoQty.Text = (obj - osd).ToString();
            return true;
        }

        private void FocusQty()
        {
            txtOsdQty.Focus();
            txtOsdQty.SelectAll();
        }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        private void OnSave(object sender, EventArgs e) { Save(); }

        // 원본 fn_SaveDEFQT
        private void Save()
        {
            if (_busy) return;
            if (_saved) { Msg("이미 저장했습니다. [재고]로 돌아가 다시 조회하세요.", MsgLevel.Warn); return; }
            if (_ptno.Length == 0) { Msg("부품 정보가 없습니다.", MsgLevel.Warn); return; }
            if (!CheckQty(true)) return;

            CodeItem rc = cboReason.SelectedItem as CodeItem;
            if (rc == null) { Msg("OS&D 사유를 선택하세요.", MsgLevel.Warn); cboReason.Focus(); return; }

            string q = CommonCache.Msg(MP_CONFIRM,
                           "LOC수량 ${} 중 ${} 개를 통제 처리하시겠습니까?",
                           new string[] { txtObjQty.Text.Trim(), txtOsdQty.Text.Trim() });
            if (!Confirm(q)) { FocusQty(); return; }

            OsdInput i = new OsdInput();
            i.Lep = _lep;
            i.Ptno = _ptno;
            i.Whscd = _whscd;
            i.Locno = txtLoc.Text;
            i.ObjQty = txtObjQty.Text.Trim();
            i.OsdQty = txtOsdQty.Text.Trim();
            i.DoQty = txtDoQty.Text.Trim();
            i.ResCd = rc.Code;

            Begin("저장중...");
            Async.Run(this,
                delegate { OsdService.Save(i); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        string text = CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.")
                                    + "\r\n" + ex.Message;
                        ReportText(text, MsgLevel.Error);
                        return;
                    }

                    _saved = true;
                    Report(MP_OK, "정상 처리되었습니다.", MsgLevel.Success);
                    btnSave.Enabled = false;
                    txtOsdQty.Enabled = false;
                    cboReason.Enabled = false;
                    btnStock.Focus();
                });
        }

        // 원본 fn_BottomButton('qty') : [321] 파트별재고로 (부번을 들고 가서 바로 재조회)
        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;

            NavArgs a = new NavArgs();
            if (_ptno.Length > 0)
            {
                a.Set("LEP", _lep);
                a.Set("PTNO", _ptno);
                a.Set("PTNM", lblPartName.Text);
                a.Set("CLASS", lblClass.Text);
            }
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        // 원본 'clear' 는 화면 전체를 지우지만, 여기서는 부번/LOC 를 다시 입력할 방법이 없으므로
        // 입력값(처리수량/사유)만 초기화한다.
        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            txtOsdQty.Text = "";
            txtDoQty.Text = "";
            if (cboReason.Items.Count > 0) cboReason.SelectedIndex = 0;
            if (!_saved) FocusQty();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[322] " + ScreenName,
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

        /// <summary>CO000 이 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[322] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[322] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSave.Enabled = on && !_saved;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearAll()
        {
            _lep = "H";
            _ptno = "";
            _whscd = "";
            _saved = false;
            lblPrefix.Text = "H";
            txtLoc.Text = "";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            txtObjQty.Text = "";
            txtOsdQty.Text = "";
            txtDoQty.Text = "";
            txtOsdQty.Enabled = true;
            cboReason.Enabled = true;
            cboReason.Items.Clear();
            SetButtons(true);
        }
    }
}
