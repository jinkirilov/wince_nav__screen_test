using System;
using System.Drawing;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;

namespace HaimsPda
{
    /// <summary>
    /// Login.xml 의 dimmed 팝업(비밀번호 변경) 대응.
    /// 현재 비밀번호 확인 -> 변경 비밀번호 2회 입력 -> 변경.
    /// </summary>
    public class PwdChangeForm : Form
    {
        private readonly string _userId;
        private bool _checked;

        private Label _lbTitle, _lb1, _lb2, _lb3, _lbHint;
        private TextBox _txtCur, _txtNew, _txtCfm;
        private GradButton _btnCheck, _btnOk, _btnCancel;
        private Font _fTitle, _fBody, _fBtn;

        private static readonly Color BoxBg = Color.FromArgb(0xd5, 0xda, 0xdd);
        private static readonly Color TitleBg = Color.FromArgb(0xbf, 0xc5, 0xc9);
        private static readonly Color TitleFg = Color.FromArgb(0x42, 0x45, 0x42);
        private static readonly Color LabelFg = Color.FromArgb(0x3e, 0x33, 0x20);

        public PwdChangeForm(string userId)
        {
            _userId = userId;
            ScreenScale.Init();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackColor = BoxBg;

            _fTitle = ScreenScale.Fnt(11f, FontStyle.Bold);
            _fBody = ScreenScale.Fnt(9f, FontStyle.Regular);
            _fBtn = ScreenScale.Fnt(9f, FontStyle.Bold);

            Build();
            Layout_();
        }

        private static int S(int n) { return ScreenScale.S(n); }

        private void Build()
        {
            _lbTitle = new Label();
            _lbTitle.Text = "비밀번호 변경";
            _lbTitle.Font = _fTitle;
            _lbTitle.ForeColor = TitleFg;
            _lbTitle.BackColor = TitleBg;

            _lb1 = MakeLabel("현재 비밀번호");
            _lb2 = MakeLabel("변경 비밀번호");
            _lb3 = MakeLabel("변경 비밀번호 확인");

            _lbHint = MakeLabel("* 비밀번호는 8자이상입니다.");

            _txtCur = MakeInput();
            _txtNew = MakeInput();
            _txtCfm = MakeInput();

            _txtCur.KeyDown += new KeyEventHandler(TxtCur_KeyDown);
            _txtNew.KeyDown += new KeyEventHandler(TxtNew_KeyDown);
            _txtCfm.KeyDown += new KeyEventHandler(TxtCfm_KeyDown);

            _btnCheck = new GradButton();
            _btnCheck.Text = "확인";
            _btnCheck.Font = _fBtn;
            _btnCheck.Click += new EventHandler(BtnCheck_Click);

            _btnOk = new GradButton();
            _btnOk.Text = "변경";
            _btnOk.Font = _fBtn;
            _btnOk.Click += new EventHandler(BtnOk_Click);

            _btnCancel = new GradButton();
            _btnCancel.Text = "취소";
            _btnCancel.Font = _fBtn;
            _btnCancel.Click += new EventHandler(BtnCancel_Click);

            // CF 의 ControlCollection 에는 AddRange 가 없다
            Controls.Add(_lbTitle);
            Controls.Add(_lb1);
            Controls.Add(_lb2);
            Controls.Add(_lb3);
            Controls.Add(_lbHint);
            Controls.Add(_txtCur);
            Controls.Add(_txtNew);
            Controls.Add(_txtCfm);
            Controls.Add(_btnCheck);
            Controls.Add(_btnOk);
            Controls.Add(_btnCancel);
        }

        private Label MakeLabel(string t)
        {
            Label l = new Label();
            l.Text = t;
            l.Font = _fBody;
            l.ForeColor = LabelFg;
            l.BackColor = BoxBg;
            return l;
        }

        private TextBox MakeInput()
        {
            TextBox t = new TextBox();
            t.BorderStyle = BorderStyle.FixedSingle;
            t.BackColor = Color.White;
            t.PasswordChar = '*';
            t.MaxLength = 20;
            t.Font = _fBody;
            return t;
        }

        private void Layout_()
        {
            int W = ClientSize.Width, H = ClientSize.Height;
            int pad = S(8);
            int labW = S(74);
            int inpH = S(28);
            int rowGap = S(6);
            int btnH = S(30);

            _lbTitle.Bounds = new Rectangle(0, 0, W, S(28));

            int x = pad;
            int inpX = pad + labW + S(4);
            int inpW = W - inpX - pad - S(48);
            int y = _lbTitle.Bottom + S(10);

            _lb1.Bounds = new Rectangle(x, y + S(6), labW, inpH);
            _txtCur.Bounds = new Rectangle(inpX, y, inpW, inpH);
            _btnCheck.Bounds = new Rectangle(_txtCur.Right + S(4), y,
                                             W - _txtCur.Right - S(4) - pad, inpH);
            y += inpH + rowGap;

            _lb2.Bounds = new Rectangle(x, y + S(6), labW, inpH);
            _txtNew.Bounds = new Rectangle(inpX, y, inpW, inpH);
            y += inpH + rowGap;

            _lb3.Bounds = new Rectangle(x, y + S(6), labW, inpH);
            _txtCfm.Bounds = new Rectangle(inpX, y, inpW, inpH);
            y += inpH + rowGap;

            _lbHint.Bounds = new Rectangle(x, y, W - pad * 2, S(18));
            y += S(24);

            int bw = (W - pad * 2 - S(6)) / 2;
            _btnOk.Bounds = new Rectangle(pad, y, bw, btnH);
            _btnCancel.Bounds = new Rectangle(pad + bw + S(6), y, bw, btnH);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Ime.AttachAll(this);   // 한글 입력 없음 — 영문 모드 고정
            _txtCur.Focus();
        }

        #region 이벤트

        private void TxtCur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            VerifyCurrent();
        }

        private void TxtNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            if (_txtNew.Text.Length < 8) MessageBox.Show("비밀번호길이는 8자 이상입니다.");
            else _txtCfm.Focus();
        }

        private void TxtCfm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            if (_txtNew.Text == _txtCfm.Text) Apply();
            else MessageBox.Show("비밀번호가 일치하지 않습니다.");
        }

        private void BtnCheck_Click(object sender, EventArgs e) { VerifyCurrent(); }

        private void BtnOk_Click(object sender, EventArgs e) { Apply(); }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        private void VerifyCurrent()
        {
            string cur = _txtCur.Text.Trim();
            if (cur.Length == 0) { MessageBox.Show("비밀번호를 입력하세요."); _txtCur.Focus(); return; }

            // 최초접속 비밀번호는 서버 확인 없이 통과
            if (cur == "M" + _userId) { _checked = true; _txtNew.Focus(); return; }

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!AuthService.CheckPassword(_userId, cur))
                {
                    MessageBox.Show("비밀번호가 틀립니다.");
                    _txtCur.Focus();
                    return;
                }
                _checked = true;
                _txtNew.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        private void Apply()
        {
            if (_txtNew.Text.Trim().Length == 0) { MessageBox.Show("변경비밀번호를 입력하세요."); return; }
            if (_txtCfm.Text.Trim().Length == 0) { MessageBox.Show("변경비밀번호확인을 입력하세요."); return; }
            if (_txtNew.Text.Length < 8) { MessageBox.Show("비밀번호길이는 8자 이상입니다."); return; }
            if (_txtNew.Text != _txtCfm.Text) { MessageBox.Show("비밀번호가 일치하지 않습니다."); return; }
            if (!_checked)
            {
                MessageBox.Show("현재 비밀번호가 확인되지 않았습니다. 현재 비밀번호를 입력 후 확인버튼 또는 엔터키로 확인하십시요.");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                AuthService.ChangePassword(_userId, _txtCfm.Text.Trim());
                MessageBox.Show("비밀번호가 변경되었습니다. 다시 로그인하십시요.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_fTitle != null) _fTitle.Dispose();
                if (_fBody != null) _fBody.Dispose();
                if (_fBtn != null) _fBtn.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
