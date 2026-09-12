using System;
using System.Text;
using System.Windows.Forms;

namespace MobisHaims.Devices
{
    /// <summary>
    /// 키보드 웨지 스캐너.
    ///
    /// 대부분의 산업용 PDA 는 스캔값을 키 입력으로 흘려보내도록 설정할 수 있다.
    /// 벤더 SDK 가 없어도, 에뮬레이터에서도 동작하므로 기본 구현이자 최후 수단이다.
    ///
    /// [동작]
    ///  - 폼의 KeyPreview 를 켜고 KeyPress 를 엿본다.
    ///  - 키 간격이 GapMs 보다 벌어지면 사람이 친 것으로 보고 버퍼를 버린다.
    ///  - Enter 가 오면 버퍼 길이가 MinLength 이상일 때만 Scanned 를 올린다.
    ///
    /// [중요] 키를 가로채지 않는다(e.Handled 를 건드리지 않는다).
    /// 포커스가 있는 TextBox 도 같은 입력을 그대로 받는다. 즉 기존 화면의
    /// "TextBox + Enter" 방식과 Scanned 이벤트 방식을 함께 쓸 수 있다.
    /// </summary>
    public sealed class WedgeScanner : IScanner
    {
        /// <summary>이 간격(ms)보다 키가 늦게 오면 사람 입력으로 간주하고 버퍼를 버린다.</summary>
        public int GapMs = 120;

        /// <summary>이 길이 미만이면 스캔으로 인정하지 않는다.</summary>
        public int MinLength = 3;

        private readonly Form _form;
        private readonly StringBuilder _buf = new StringBuilder(64);
        private int _lastTick;
        private bool _open;
        private bool _enabled = true;

        public WedgeScanner(Form form)
        {
            if (form == null) throw new ArgumentNullException("form");
            _form = form;
        }

        public string Name { get { return "KeyboardWedge"; } }
        public bool IsOpen { get { return _open; } }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; if (!value) _buf.Length = 0; }
        }

        public event ScanEventHandler Scanned;

        public bool Open()
        {
            if (_open) return true;
            try
            {
                _form.KeyPreview = true;
                _form.KeyPress += new KeyPressEventHandler(OnKeyPress);
                _open = true;
                return true;
            }
            catch { return false; }
        }

        public void Close()
        {
            if (!_open) return;
            try { _form.KeyPress -= new KeyPressEventHandler(OnKeyPress); }
            catch { }
            _buf.Length = 0;
            _open = false;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_enabled) return;

            int now = Environment.TickCount;
            // TickCount 는 49.7일마다 한 바퀴 돈다. 음수 차이는 새 입력으로 본다.
            int gap = now - _lastTick;
            _lastTick = now;
            if (gap < 0 || gap > GapMs) _buf.Length = 0;

            char c = e.KeyChar;

            if (c == '\r' || c == '\n')
            {
                string s = _buf.ToString().Trim();
                _buf.Length = 0;
                if (s.Length >= MinLength) Raise(s);
                return;
            }

            if (c < ' ') return;          // 제어문자 무시
            if (_buf.Length > 512) _buf.Length = 0;   // 폭주 방지
            _buf.Append(c);
        }

        private void Raise(string text)
        {
            ScanEventHandler h = Scanned;
            if (h == null) return;
            try { h(this, new ScanData(text, "")); }
            catch { }   // 화면 핸들러의 예외로 입력 경로가 죽지 않게 한다
        }

        public void Dispose() { Close(); }
    }
}
