using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HaimsPda.Ui
{
    /// <summary>
    /// 실행 시 화면 해상도와 DPI를 읽어 레이아웃 배율을 결정한다.
    ///
    /// 설계 기준(디자인 단위) = QVGA 240 x 320.
    ///   - 240x320 (96dpi)  -> Factor 1, FontScale 1.0
    ///   - 480x640 (192dpi) -> Factor 2, FontScale 1.0  (OS가 폰트를 이미 2배로 그림)
    ///   - 480x640 (96dpi)  -> Factor 2, FontScale 2.0  (직접 2배로 키워야 함)
    ///
    /// 레이아웃 좌표는 전부 S(n) 을 거쳐 픽셀로 환산하고,
    /// 폰트는 Fnt() 로 만들어 쓴다.
    /// </summary>
    public static class ScreenScale
    {
        public const int DesignWidth = 240;
        public const int DesignHeight = 320;

        // 한글이 포함된 문자열용 / 영문 전용 폰트
        public const string FamilyKo = "Gulim";
        public const string FamilyEn = "Tahoma";

        private static bool _ready;
        private static int _factor = 1;
        private static float _fontScale = 1f;
        private static int _dpi = 96;
        private static Size _screen = new Size(DesignWidth, DesignHeight);

        #region native
        [DllImport("coredll.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("coredll.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("coredll.dll")]
        private static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
        private const int LOGPIXELSX = 88;
        #endregion

        /// <summary>Main() 최초에 한 번 호출.</summary>
        public static void Init()
        {
            if (_ready) return;

            try
            {
                Rectangle b = Screen.PrimaryScreen.Bounds;
                if (b.Width > 0 && b.Height > 0) _screen = new Size(b.Width, b.Height);
            }
            catch { }

            // 가로 400px 이상이면 VGA 급으로 보고 2배.
            // 240x400, 240x320 같은 QVGA 변형은 1배 유지.
            _factor = (_screen.Width >= 400) ? 2 : 1;

            _dpi = ReadDpi();
            float dpiFactor = _dpi / 96f;
            if (dpiFactor <= 0.1f) dpiFactor = 1f;

            // OS가 DPI로 이미 키워 그리는 만큼은 빼고 나머지만 폰트에 반영
            _fontScale = _factor / dpiFactor;
            if (_fontScale < 0.5f) _fontScale = 0.5f;
            if (_fontScale > 4f) _fontScale = 4f;

            _ready = true;
        }

        private static int ReadDpi()
        {
            IntPtr hdc = IntPtr.Zero;
            try
            {
                hdc = GetDC(IntPtr.Zero);
                if (hdc == IntPtr.Zero) return 96;
                int v = GetDeviceCaps(hdc, LOGPIXELSX);
                return (v > 0) ? v : 96;
            }
            catch
            {
                // 데스크톱 테스트 빌드 등 coredll 이 없는 환경
                return 96;
            }
            finally
            {
                if (hdc != IntPtr.Zero)
                {
                    try { ReleaseDC(IntPtr.Zero, hdc); }
                    catch { }
                }
            }
        }

        public static Size Screen1 { get { Init(); return _screen; } }
        public static int Factor { get { Init(); return _factor; } }
        public static int Dpi { get { Init(); return _dpi; } }
        public static float FontScale { get { Init(); return _fontScale; } }
        public static bool IsVga { get { return Factor >= 2; } }

        /// <summary>디자인 단위 -> 실제 픽셀</summary>
        public static int S(int designPx)
        {
            Init();
            return designPx * _factor;
        }

        public static Size S(int w, int h) { return new Size(S(w), S(h)); }

        public static Rectangle S(int x, int y, int w, int h)
        {
            return new Rectangle(S(x), S(y), S(w), S(h));
        }

        /// <summary>디자인 기준 pt -> 실제 Font</summary>
        public static Font Fnt(float designPt, FontStyle style)
        {
            return Fnt(FamilyKo, designPt, style);
        }

        public static Font Fnt(string family, float designPt, FontStyle style)
        {
            Init();
            float pt = designPt * _fontScale;
            if (pt < 5f) pt = 5f;
            try { return new Font(family, pt, style); }
            catch { return new Font(FamilyEn, pt, style); }
        }

        /// <summary>진단용 문자열 (로그/디버그 표시)</summary>
        public static string Describe()
        {
            Init();
            return _screen.Width + "x" + _screen.Height
                 + " dpi=" + _dpi
                 + " factor=" + _factor
                 + " fontScale=" + _fontScale.ToString("0.00");
        }
    }

    public static class WinApi
    {
        [DllImport("coredll.dll")]
        public static extern uint SendMessage(IntPtr hwnd, uint msg, uint wparam, uint lparam);

        private static void SetListViewStyle(IntPtr hwnd, uint style, bool enable)
        {
            uint currentStyle = SendMessage(hwnd, LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);

            if (enable)
            {
                SendMessage(hwnd, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, currentStyle | style);
            }
            else
            {
                SendMessage(hwnd, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, currentStyle & ~style);
            }
        }

        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        private const uint LVM_GETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 55;

        private const uint LVS_EX_GRIDLINES = 0x00000001;
        private const uint LVS_EX_DOUBLEBUFFER = 0x00010000;

        public static void DoubleBuffering(IntPtr hwnd, bool enable)
        {
            SetListViewStyle(hwnd, LVS_EX_DOUBLEBUFFER, enable);
        }

        public static void GridLines(IntPtr hwnd, bool enable)
        {
            SetListViewStyle(hwnd, LVS_EX_GRIDLINES, enable);
        }

    }

}
