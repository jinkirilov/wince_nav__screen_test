using System;
using System.Drawing;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;

namespace HaimsPda
{
    public partial class LoginForm : Form
    {
        #region 디자인 단위 상수 (QVGA 240x320 기준, login.css 실측값)

        private const int LogoX = 12, LogoY = 10, LogoW = 141, LogoH = 22;
        private const int ColW = 168;            // .in_loc width
        private const int LockW = 24, LockH = 34;
        private const int RowH = 32;             // 체크박스 줄
        private const int ChgW = 80, ChgH = 31;  // .btn_jp
        private const int InpW = 158, InpH = 32; // .Uinput / .Pinput
        private const int MgrpPad = 2;           // .Mgrp padding
        private const int LoginBtnW = 96;        // 금색 버튼 (나머지는 서버명 표시)

        private const int GapAfterLock = 12;     // .mt12
        private const int GapAfterRow = 6;       // .mt7 (실제 6px)
        private const int GapAfterId = 5;        // .mt5
        private const int GapAfterPw = 5;        // .mt5

        private const int CopyH = 24;
        private const int BottomPad = 6;

        #endregion

        #region 색상 (login.css)

        private static readonly Color BgFallback = Color.FromArgb(27, 67, 109);
        private static readonly Color TxtLogin = Color.FromArgb(0xd4, 0xda, 0xe0);
        private static readonly Color CopyText = Color.FromArgb(0xd4, 0xda, 0xe0);
        private static readonly Color CopyLink = Color.FromArgb(0x34, 0x95, 0xbd);
        private static readonly Color CopyLine = Color.FromArgb(0x26, 0x4d, 0x77);
        private static readonly Color MgrpBg = Color.FromArgb(0x21, 0x27, 0x2a);
        private static readonly Color MgrpBorder = Color.FromArgb(0x0a, 0x18, 0x27);

        #endregion

        #region 상태

        // 화면 요소는 전부 디자이너(LoginForm.Designer.cs)가 잡는다.
        // 직접 그리던 시절의 필드(_buffer/_rcLogo/_fLogin …)는 쓰이지 않아 제거했다.
        private string _serverName = "";
        private bool _busy;

        #endregion

        public LoginForm()
        {
            ScreenScale.Init();
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }




        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 이 프로그램은 한글 입력이 없다. 입력창 포커스 때마다 영문으로 되돌린다.
            Ime.AttachAll(this);

            chkSave.Checked = true;

            if (Prefs.Get(Prefs.KeySaveYn) == "Y" && Prefs.Get(Prefs.KeyUserId).Length > 0)
            {
                txtId.Text = Prefs.Get(Prefs.KeyUserId);
                txtPw.Focus();
            }
            else
            {
                txtId.Focus();
            }

            FetchServerNameAsync();
        }

        private void FetchServerNameAsync()
        {
            Async.Run(this,
                delegate
                {
                    return AuthService.GetServerName();
                },
                delegate(object result, Exception error)
                {
                    if (error != null) return;          // 서버명은 실패해도 무시
                    _serverName = (result == null) ? "" : ((string)result).Trim();
                    Session.ServerName = _serverName;
                    lblServer.Text = _serverName;
                });
        }


        #region 입력 처리


        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            // 원본(Login.xml)은 길이 조건 없이 바로 비밀번호로 넘어간다.
            if (e.KeyCode == Keys.Enter)
            {
                txtId.Text = txtId.Text.ToUpper();
                txtId.SelectionStart = txtId.TextLength;
                txtPw.Focus();
                e.Handled = true;
            }
        }

        private void txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPw.Text = txtPw.Text.ToUpper();   // gfn_ConvUpper
                e.Handled = true;
                DoLogin();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e) { DoLogin(); }

        private void btnEnv_Click(object sender, EventArgs e)
        {
            if (_busy) return;

            using (HostForm f = new HostForm())
            {
                if (f.ShowDialog() != DialogResult.OK) return;
            }

            // 주소가 바뀌었으니 서버명을 다시 받아 온다
            lblServer.Text = "";
            _serverName = "";
            Session.ServerName = "";
            FetchServerNameAsync();

            txtId.Focus();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            if (id.Length == 0)
            {
                MessageBox.Show("사용자 아이디를 넣어주세요.");
                return;
            }
            if (id == "DA19900" || id == "D285000")
            {
                MessageBox.Show("비밀번호를 변경할 수 없는 아이디 입니다.");
                return;
            }

            // 비밀번호 변경 화면 (Login.xml 의 dimmed 팝업 대응)
            using (PwdChangeForm f = new PwdChangeForm(id))
                f.ShowDialog();
        }

        #endregion

        #region 로그인 (fn_Login)

        private void DoLogin()
        {
            if (_busy) return;

            string id = txtId.Text.Trim().ToUpper();
            string pw = txtPw.Text.Trim();

            // 최초접속 비밀번호 = "M" + 아이디
            if (pw == "M" + id)
            {
                if (id == "DA19900" || id == "D285000")
                {
                    MessageBox.Show("비밀번호를 변경할 수 없는 아이디 입니다.");
                    return;
                }
                MessageBox.Show("최초접속비밀번호입니다. 비밀번호를 변경하십시요");
                using (PwdChangeForm f = new PwdChangeForm(id))
                    f.ShowDialog();
                return;
            }

            if (id.Length == 0) { MessageBox.Show("사용자 아이디를 넣어주세요."); txtId.Focus(); return; }
            if (pw.Length == 0) { MessageBox.Show("비밀번호를 넣어주세요."); txtPw.Focus(); return; }

            SetBusy(true);

            Async.Run(this,
                delegate
                {
                    return AuthService.Login(id, pw);
                },
                delegate(object result, Exception error)
                {
                    SetBusy(false);

                    if (error != null)
                    {
                        MessageBox.Show(FriendlyError(error));
                        txtPw.Focus();
                        txtPw.SelectAll();
                        return;
                    }

                    Session.User = (UserInfo)result;

                    if (chkSave.Checked)
                    {
                        Prefs.Set(Prefs.KeySaveYn, "Y");
                        Prefs.Set(Prefs.KeyUserId, id);
                    }
                    else
                    {
                        Prefs.Set(Prefs.KeySaveYn, "");
                        Prefs.Set(Prefs.KeyUserId, "");
                    }

                    // 로그 기록은 결과를 기다릴 필요가 없다 (UI 블로킹 방지)
                    UserInfo u = Session.User;
                    Async.Run(this, delegate { AuthService.SaveLoginLog(u); return null; }, null);

                    // 메뉴/메시지/공통코드 (웹 getMenuAndMessageAndCommonCode).
                    // 메시지 코드(MP***)를 화면에서 쓰려면 이게 먼저 채워져 있어야 한다.
                    SetBusy(true);
                    Async.Run(this,
                        delegate { AuthService.LoadCommonData(); return null; },
                        delegate(object r2, Exception e2)
                        {
                            SetBusy(false);
                            if (e2 != null)
                                MessageBox.Show("공통 데이터를 불러오지 못했습니다.\r\n" + e2.Message);

                            DialogResult = DialogResult.OK;
                            Close();
                        });
                });
        }

        private static string FriendlyError(Exception ex)
        {
            if (ex is HaimsException) return ex.Message;
            if (ex is System.Net.WebException)
                return "서버에 연결할 수 없습니다.\r\n통신 상태를 확인하십시오.";
            return "오류가 발생했습니다.\r\n" + ex.Message;
        }

        private void SetBusy(bool busy)
        {
            _busy = busy;
            txtId.Enabled = !busy;
            txtPw.Enabled = !busy;
            chkSave.Enabled = !busy;
            btnChange.Enabled = !busy;
            btnLogin.Enabled = !busy;
            btnLogin.Text = busy ? "처리중" : "로그인";
            btnLogin.Invalidate();
            Cursor.Current = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
