using System;
using System.Drawing;
using System.Windows.Forms;
using HaimsPda.Nav;

namespace HaimsPda.Ui
{
    /// <summary>
    /// ScreenBase를 타이틀바가 있는 모달 다이얼로그로 띄우는 호스트 폼.
    /// - 동시에 하나만 열림 (다이얼로그 위에서 다시 다이얼로그를 열 수 없음)
    /// - 표시 중에는 ShellForm.Navigate 차단 (IsOpen 확인)
    /// - 닫힐 때 화면(ScreenBase)도 Dispose됨 → 결과값은 닫기 전에 필드로 보관할 것
    /// </summary>
    public class ScreenDialog : Form
    {
        // ── 정적 상태 ─────────────────────────────
        static ScreenBase _current;

        /// <summary>다이얼로그 표시 중 여부</summary>
        public static bool IsOpen { get { return _current != null; } }

        /// <summary>현재 다이얼로그 화면 (스캐너 라우팅용)</summary>
        public static ScreenBase Current { get { return _current; } }

        // ── 상수 (192 DPI 기준) ───────────────────
        const float BASE_DPI = 192f;
        const int BORDER = 1;
        const int TITLE_H = 52;
        const int TITLE_PAD_X = 12;
        const int CLOSE_W = 100;
        const int CLOSE_PAD = 6;

        static readonly Color BorderColor = Color.FromArgb(0xE0, 0xE0, 0xE0);
        static readonly Color TitleBack = Color.FromArgb(0x19, 0x40, 0x6A);
        static readonly Color TitleFore = Color.FromArgb(0xE0, 0xE0, 0xE0);
        static readonly Color CloseBack = Color.FromArgb(0x22, 0x57, 0x90);

        // ── 인스턴스 ─────────────────────────────
        readonly ScreenBase _screen;
        readonly string _title;
        readonly bool _closeOnEscape;

        Panel _titleBar;
        Button _btnClose;
        Font _titleFont;
        SolidBrush _titleBrush;
        StringFormat _titleFormat;
        float _scale = 1f;

        ScreenDialog(ScreenBase screen, string title, bool closeOnEscape)
        {
            _screen = screen;
            _title = (title != null) ? title : (screen.Text ?? "");
            _closeOnEscape = closeOnEscape;

            AutoScaleMode = AutoScaleMode.Dpi;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            Menu = null;              // WM: MainMenu가 있으면 전체화면 강제
            Text = _title;
            BackColor = BorderColor;  // 바깥 1px이 테두리로 보임
            KeyPreview = true;

            // 타이틀바
            _titleBar = new Panel();
            _titleBar.BackColor = TitleBack;
            _titleBar.Paint += new PaintEventHandler(TitleBar_Paint);

            _btnClose = new Button();
            _btnClose.Text = "닫기";
            _btnClose.BackColor = CloseBack;
            _btnClose.ForeColor = TitleFore;
            _btnClose.Font = new Font("굴림", 10f, FontStyle.Bold);
            _btnClose.Click += new EventHandler(BtnClose_Click);

            _titleFont = new Font("굴림", 10f, FontStyle.Bold);
            _titleBrush = new SolidBrush(TitleFore);
            _titleFormat = new StringFormat();
            _titleFormat.Alignment = StringAlignment.Near;
            _titleFormat.LineAlignment = StringAlignment.Center;
            _titleFormat.FormatFlags = StringFormatFlags.NoWrap;

            _titleBar.Controls.Add(_btnClose);
            Controls.Add(_titleBar);
            Controls.Add(screen);
        }

        int S(int v192)
        {
            return (int)(v192 * _scale + 0.5f);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            using (Graphics g = CreateGraphics())
                _scale = g.DpiY / BASE_DPI;

            int titleH = S(TITLE_H);
            int pad = S(CLOSE_PAD);
            int closeW = S(CLOSE_W);

            // 타이틀바
            _titleBar.Bounds = new Rectangle(BORDER, BORDER, _screen.Width, titleH);
            _btnClose.Bounds = new Rectangle(
                _titleBar.Width - closeW - pad, pad,
                closeW, titleH - pad * 2);

            // 본문 화면
            _screen.Location = new Point(BORDER, BORDER + titleH);

            // 폼 크기·위치 (WM은 생성자에서 지정한 크기를 덮어쓰므로 여기서 지정)
            int w = _screen.Width + BORDER * 2;
            int h = _screen.Height + titleH + BORDER * 2;

            Rectangle wa = Screen.PrimaryScreen.WorkingArea;
            if (w > wa.Width) w = wa.Width;
            if (h > wa.Height) h = wa.Height;

            Bounds = new Rectangle(
                wa.X + (wa.Width - w) / 2,
                wa.Y + (wa.Height - h) / 2,
                w, h);

            _screen.OnDialogShown();
        }

        void TitleBar_Paint(object sender, PaintEventArgs e)
        {
            int x = S(TITLE_PAD_X);
            RectangleF r = new RectangleF(
                x, 0,
                _btnClose.Left - x, _titleBar.Height);
            e.Graphics.DrawString(_title, _titleFont, _titleBrush, r, _titleFormat);
        }

        void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_closeOnEscape && e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
                return;
            }
            base.OnKeyDown(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_titleFont != null) { _titleFont.Dispose(); _titleFont = null; }
                if (_titleBrush != null) { _titleBrush.Dispose(); _titleBrush = null; }
                if (_btnClose != null && _btnClose.Font != null) _btnClose.Font.Dispose();
            }
            base.Dispose(disposing);
        }

        // ── 진입점 ───────────────────────────────

        public static DialogResult Show(ScreenBase screen)
        {
            return Show(screen, null, true);
        }

        public static DialogResult Show(ScreenBase screen, string title)
        {
            return Show(screen, title, true);
        }

        /// <param name="title">null이면 screen.Text 사용</param>
        /// <param name="closeOnEscape">ESC/Back 키로 닫기 허용</param>
        public static DialogResult Show(ScreenBase screen, string title, bool closeOnEscape)
        {
            if (screen == null) throw new ArgumentNullException("screen");

            if (IsOpen)
            {
                screen.Dispose();
                return DialogResult.None;   // 중첩 호출 차단
            }

            _current = screen;
            ScreenDialog dlg = null;
            try
            {
                dlg = new ScreenDialog(screen, title, closeOnEscape);
                return dlg.ShowDialog();
            }
            finally
            {
                _current = null;
                if (dlg != null)
                {
                    dlg.Controls.Remove(screen);
                    dlg.Dispose();
                }
                screen.Dispose();
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
