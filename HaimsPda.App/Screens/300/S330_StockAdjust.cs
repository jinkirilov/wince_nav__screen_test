using System;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [330] 재고조정 : [320]/[321]/[324]/[410] 에서 선택한 LOC 한 건의 재고를 +/- 조정한다.
    // 원본 웹화면 : /ui/ws/plus/PL330_W01.xml (서버 메뉴 P190)
    //
    //   OnEnter(LEP/PTNO/PTNM/CLASS/LOCNO/WHSCD/QTY)
    //     -> 조정처리수량 입력 -> fn_CheckQty (권한 / 수량 검증, 조정후수량 계산)
    //     -> fn_Search (PL330_W01_S01 : 단가 / 차종 / 증표번호)
    //     -> MP631 확인 -> fn_Save (PL330_W01_I01, I02, U01, U02)
    //
    // 부번/LOC/LOC수량은 넘어온 값만 보여주고 입력은 조정처리수량뿐이다.
    //
    // 저장 성공 : 결과 메시지창 -> 호출 화면으로 복귀(NavResult.Saved) -> 호출 화면이 재조회.
    // 저장 실패 : 메시지창만 띄우고 이 화면에 남는다.
    //
    // 원본과 다르게 한 부분
    //   - 조정후수량은 계산 결과 표시용이라 입력할 수 없다(원본은 입력칸이지만 늘 덮어쓴다).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S330_StockAdjust.Designer.cs 에서 관리한다.
    public sealed partial class S330_StockAdjust : ScreenBase
    {
        private const string MP_OK = "MP102";      // 정상 처리되었습니다
        private const string MP_NOINFO = "MP303";  // 부품번호를 확인하세요 (원본 S01 결과 없음)
        private const string MP_CONFIRM = "MP631"; // 재고를 조정하시겠습니까
        private const string MP_ZERO = "MP613";    // 수량에 공백이나 0을 입력할 수 없습니다
        private const string MP_CLEAR = "MP503";   // 지우시겠습니까

        private string _lep = "H";
        private string _ptno = "";
        private string _whscd = "";
        private bool _busy;
        private bool _saved;      // 저장 후에는 LOC수량이 바뀌므로 재진입 전까지 저장 차단

        public override int ScreenNo { get { return ScreenId.StockAdjust; } }
        public override string ScreenName { get { return "재고조정"; } }

        public S330_StockAdjust()
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
            if (_whscd.Length == 0) _whscd = "M";     // 원본 fn_init : selWHS "M"

            txtPart.Text = PartNo.Display(_ptno);
            lblClass.Text = Arg(args, "CLASS");
            lblPartName.Text = Arg(args, "PTNM");
            txtLoc.Text = Loc.Display(Arg(args, "LOCNO"));
            lblWh.Text = _whscd;
            txtLocQty.Text = Num(Arg(args, "QTY"));

            if (_ptno.Length == 0)
            {
                SetButtons(false);
                btnStock.Enabled = true;
                Msg("[320]/[321]/[324] 에서 LOC 를 선택해 진입하세요.", MsgLevel.Warn);
                return;
            }

            if (!StockAdjustService.HasPermission)
            {
                // 원본은 저장 시점에 막는다. 입력부터 헛수고하지 않게 들어올 때 알린다.
                Msg("재고를 조정할 수 있는 권한이 없습니다.", MsgLevel.Warn);
            }
            else
            {
                Msg("조정처리수량을 입력하세요. (감소는 -)", MsgLevel.Info);
            }

            txtAdjQty.Focus();
            txtAdjQty.SelectAll();
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 입력
        // ------------------------------------------------------------------
        private void OnAdjKeyPress(object sender, KeyPressEventArgs e)
        {
            // 숫자, 맨 앞의 '-', BackSpace 만 허용
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar == '-')
            {
                if (txtAdjQty.SelectionStart != 0 || txtAdjQty.Text.IndexOf('-') >= 0) e.Handled = true;
                return;
            }
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void OnAdjTextChanged(object sender, EventArgs e)
        {
            // 입력 중에는 경고 없이 조정후수량만 미리 보여 준다
            int adj;
            if (!TryInt(txtAdjQty.Text, out adj) || adj == 0) { txtAfterQty.Text = ""; return; }
            txtAfterQty.Text = (ToInt(txtLocQty.Text) + adj).ToString();
        }

        // 원본 inptOSD_QTY Enter -> fn_CheckQty("0"), 한 번 더 Enter(inptQTY) -> fn_CheckQty("1")
        // 여기서는 조정후수량을 입력칸으로 두지 않으므로 Enter 한 번에 저장까지 간다.
        private void OnAdjKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Save();
        }

        private void OnSave(object sender, EventArgs e) { Save(); }

        /// <summary>
        /// 원본 fn_CheckQty. 통과하면 INV_ZERO_PLUS 값을 돌려주고, 아니면 null.
        /// </summary>
        private string CheckQty()
        {
            if (!StockAdjustService.HasPermission)
            {
                ReportText("조정할 수 있는 권한이 없습니다.", MsgLevel.Warn);
                return null;
            }

            int adj;
            if (!TryInt(txtAdjQty.Text, out adj) || adj == 0)
            {
                Report(MP_ZERO, "수량에 공백이나 0을 입력할 수 없습니다.", MsgLevel.Warn);
                FocusQty();
                return null;
            }

            int loc = ToInt(txtLocQty.Text);
            if (adj < 0 && adj + loc < 0)
            {
                ReportText("LOC 가용재고보다 적게 조정할 수 없습니다.", MsgLevel.Warn);
                FocusQty();
                return null;
            }

            string zeroPlus;
            if (loc == 0)
            {
                if (adj <= 0)
                {
                    ReportText("LOC 가용재고보다 적게 조정할 수 없습니다.", MsgLevel.Warn);
                    FocusQty();
                    return null;
                }
                zeroPlus = "Y";
            }
            else zeroPlus = "N";

            txtAfterQty.Text = (loc + adj).ToString();
            return zeroPlus;
        }

        // ------------------------------------------------------------------
        // 저장 : fn_CheckQty("1") -> fn_Search -> MP631 -> fn_Save
        // ------------------------------------------------------------------
        private void Save()
        {
            if (_busy) return;
            if (_saved) { Msg("이미 저장했습니다. [재고]로 가서 다시 조회하세요.", MsgLevel.Warn); return; }
            if (_ptno.Length == 0)
            {
                Report(MP_NOINFO, "부품번호를 확인하세요.", MsgLevel.Warn);
                return;
            }

            string zeroPlus = CheckQty();
            if (zeroPlus == null) return;

            string lep = _lep, ptno = _ptno;

            Begin("확인중...");
            Async.Run(this,
                delegate { return StockAdjustService.Search(lep, ptno, zeroPlus); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    AdjustInfo info = (AdjustInfo)r;
                    if (!info.Found)
                    {
                        Report(MP_NOINFO, "부품번호를 확인하세요.", MsgLevel.Warn);
                        return;
                    }
                    End("", MsgLevel.Info);

                    string adj = txtAdjQty.Text.Trim();
                    string q = CommonCache.Msg(MP_CONFIRM, "재고를 조정하시겠습니까?")
                             + "\r\nLOC " + txtLoc.Text + "\r\n"
                             + txtLocQty.Text + " -> " + txtAfterQty.Text + " (" + Signed(adj) + ")";
                    if (!Confirm(q)) { FocusQty(); return; }

                    Commit(info);
                });
        }

        private void Commit(AdjustInfo info)
        {
            AdjustInput i = new AdjustInput();
            i.Lep = _lep;
            i.Ptno = _ptno;
            i.Whscd = _whscd;
            i.Locno = txtLoc.Text;
            i.AdjQty = txtAdjQty.Text.Trim();

            Begin("저장중...");
            Async.Run(this,
                delegate { StockAdjustService.Save(i, info); return null; },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    // 성공 : 결과 메시지창 -> 호출 화면으로 복귀. 호출 화면은 OnReturn 에서 다시 조회한다.
                    _saved = true;
                    string msg = CommonCache.Msg(MP_OK, "정상 처리되었습니다.");
                    End(msg, MsgLevel.Success);
                    MessageBox.Show(msg + "\r\nLOC수량 " + txtLocQty.Text + " -> " + txtAfterQty.Text,
                                    "[330] " + ScreenName);

                    NavArgs res = NavResult.Saved(ScreenNo);
                    res.Set(NavResult.KeyLocno, Loc.Key(i.Locno));
                    res.Set(NavResult.KeyLocQty, txtAfterQty.Text);
                    Shell.GoBack(res);
                });
        }

        // 원본에는 없지만 저장 후 갈 곳이 필요하다 ([322] 와 같음) : [321] 파트별재고
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

        // 원본 fn_clear 는 화면 전체를 지우지만 부번/LOC 를 다시 넣을 방법이 없으므로 입력값만 지운다.
        private void OnClear(object sender, EventArgs e)
        {
            if (_busy || _saved) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "화면을 지우시겠습니까?"))) { FocusQty(); return; }
            txtAdjQty.Text = "0";
            txtAfterQty.Text = "";
            FocusQty();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void FocusQty()
        {
            txtAdjQty.Focus();
            txtAdjQty.SelectAll();
        }

        private static string Signed(string s)
        {
            return (s.StartsWith("-")) ? s : "+" + s;
        }

        /// <summary>표시용. "3.00" 이나 "1,234" 도 정수로 보여 준다.</summary>
        private static string Num(string s)
        {
            return ToInt(s).ToString();
        }

        private static bool TryInt(string s, out int v)
        {
            v = 0;
            if (s == null) return false;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0 || s == "-") return false;
            try { v = int.Parse(s); return true; }
            catch { return false; }
        }

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
            return MessageBox.Show(text, "[330] " + ScreenName,
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
            if (msg.Length > 0) Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[330] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[330] " + ScreenName);
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
            btnClear.Enabled = on && !_saved;
        }

        private void ClearAll()
        {
            _lep = "H";
            _ptno = "";
            _whscd = "";
            _saved = false;
            lblPrefix.Text = "H";
            txtLoc.Text = "";
            lblWh.Text = "";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            txtLocQty.Text = "0";
            txtAdjQty.Text = "0";
            txtAfterQty.Text = "";
            txtAdjQty.Enabled = true;
            SetButtons(true);
        }
    }
}
