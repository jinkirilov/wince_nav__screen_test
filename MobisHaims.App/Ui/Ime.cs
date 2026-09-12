using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HaimsPda.Ui
{
    /// <summary>
    /// 입력기(IME) 강제 영문 모드.
    ///
    /// 이 프로그램은 한글 입력이 없다. 부번/사원번호/LOC/수량은 모두 영숫자이고
    /// 바코드 스캐너 입력도 영숫자로 들어온다. 한글 모드로 남아 있으면
    /// 스캔값이 조합 문자로 깨지므로 시작 시점과 입력창 포커스 시점에 영문으로 되돌린다.
    ///
    /// Windows CE 의 IMM API 는 coredll.dll 에 있다. 장비/이미지에 따라
    /// export 되지 않는 경우가 있어 모든 호출을 try/catch 로 감싼다.
    /// (실패해도 프로그램은 그대로 동작한다)
    /// </summary>
    public static class Ime
    {
        #region native

        [DllImport("coredll.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("coredll.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("coredll.dll")]
        private static extern bool ImmSetConversionStatus(IntPtr hIMC, uint fdwConversion, uint fdwSentence);

        [DllImport("coredll.dll")]
        private static extern bool ImmGetConversionStatus(IntPtr hIMC, out uint fdwConversion, out uint fdwSentence);

        [DllImport("coredll.dll")]
        private static extern bool ImmSetOpenStatus(IntPtr hIMC, bool fOpen);

        [DllImport("coredll.dll")]
        private static extern bool ImmSimulateHotKey(IntPtr hWnd, uint dwHotKeyID);

        // 변환 모드
        private const uint IME_CMODE_ALPHANUMERIC = 0x0000;   // 영문
        private const uint IME_CMODE_NATIVE = 0x0001;         // 한글
        private const uint IME_CMODE_FULLSHAPE = 0x0008;      // 전각
        private const uint IME_SMODE_NONE = 0x0000;

        // 한국어 IME 핫키 (ImmSimulateHotKey)
        private const uint IME_KHOTKEY_ENGLISH = 0x0B;        // 한/영 전환

        #endregion

        /// <summary>coredll 에 IMM 이 없는 장비면 false 로 굳어져 이후 호출을 건너뛴다.</summary>
        private static bool _available = true;

        public static bool Available { get { return _available; } }

        /// <summary>
        /// 지정한 창의 입력 모드를 영문으로 되돌린다.
        /// IMM 컨텍스트는 창(HWND) 단위라 폼과 입력창 각각에 적용해야 확실하다.
        /// </summary>
        public static void SetEnglish(Control c)
        {
            if (!_available || c == null) return;

            IntPtr hWnd;
            try
            {
                hWnd = c.Handle;
            }
            catch { return; }

            if (hWnd == IntPtr.Zero) return;
            SetEnglish(hWnd);
        }

        public static void SetEnglish(IntPtr hWnd)
        {
            if (!_available || hWnd == IntPtr.Zero) return;

            IntPtr hIMC = IntPtr.Zero;
            try
            {
                hIMC = ImmGetContext(hWnd);
                if (hIMC == IntPtr.Zero)
                {
                    // 컨텍스트가 없으면 IME 자체가 붙지 않은 창이다. 정상.
                    return;
                }

                uint conv, sent;
                if (ImmGetConversionStatus(hIMC, out conv, out sent))
                {
                    // 한글/전각 비트만 떨어뜨리고 나머지 상태는 보존
                    uint want = conv & ~(IME_CMODE_NATIVE | IME_CMODE_FULLSHAPE);
                    if (want != conv) ImmSetConversionStatus(hIMC, want, sent);
                }
                else
                {
                    ImmSetConversionStatus(hIMC, IME_CMODE_ALPHANUMERIC, IME_SMODE_NONE);
                }

                ImmSetOpenStatus(hIMC, false);
            }
            catch (MissingMethodException) { _available = false; }
            catch (EntryPointNotFoundException) { _available = false; }
            catch { }
            finally
            {
                if (hIMC != IntPtr.Zero)
                {
                    try { ImmReleaseContext(hWnd, hIMC); }
                    catch { }
                }
            }
        }

        /// <summary>
        /// ImmSetConversionStatus 가 먹지 않는 장비용 보조 수단.
        /// 한/영 핫키를 시뮬레이션한다. 현재 상태가 영문이면 오히려 한글로 바뀌므로
        /// 상태 확인이 불가능할 때만 쓴다.
        /// </summary>
        public static void ToggleHanEng(Control c)
        {
            if (!_available || c == null) return;
            try
            {
                if (c.Handle == IntPtr.Zero) return;
                ImmSimulateHotKey(c.Handle, IME_KHOTKEY_ENGLISH);
            }
            catch (MissingMethodException) { _available = false; }
            catch (EntryPointNotFoundException) { _available = false; }
            catch { }
        }

        /// <summary>
        /// 컨트롤 트리를 훑어 TextBox 전부에 GotFocus 핸들러를 건다.
        /// 포커스가 들어올 때마다 영문으로 되돌리므로, 사용자가 중간에
        /// 한/영 키를 눌렀더라도 다음 입력창에서 복구된다.
        ///
        /// 폼/화면의 Load(또는 Attach) 시점에 1회 호출한다.
        /// </summary>
        public static void AttachAll(Control root)
        {
            if (root == null) return;
            SetEnglish(root);
            AttachRec(root);
        }

        private static void AttachRec(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    // 중복 등록 방지: 먼저 떼고 다시 건다
                    c.GotFocus -= new EventHandler(OnInputGotFocus);
                    c.GotFocus += new EventHandler(OnInputGotFocus);
                }
                if (c.Controls.Count > 0) AttachRec(c);
            }
        }

        private static void OnInputGotFocus(object sender, EventArgs e)
        {
            SetEnglish(sender as Control);
        }
    }
}
