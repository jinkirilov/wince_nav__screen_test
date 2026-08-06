using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MobisHaims.Core;

namespace MobisHaims.Nav
{
    // 모든 업무화면의 베이스. Form이 아닌 UserControl로 만들어 셸의 ContentPanel에 스왑한다.
    //
    // [해상도 대응]
    //  - 디자이너 작업 기준은 ShellForm._content @ VGA = 480 x 528 (DesignSize).
    //  - 메뉴형 화면 : LayoutGrid()  - 실행 시점 W/H를 균등분할 (디자이너 좌표 무시)
    //  - 업무형 화면 : ScaleToClient() - 디자이너 좌표/폰트를 실행 해상도에 비례 스케일
    //  - QVGA(240x320) / VGA(480x640) 양쪽 모두 동일 코드로 동작한다.
    public class ScreenBase : UserControl
    {
        // 디자이너 기준 해상도(ShellForm._content @ VGA)
        public static readonly Size DesignSize = new Size(480, 528);

        protected IShellContext Shell;

        public virtual int ScreenNo { get { return 0; } }
        public virtual string ScreenName { get { return ""; } }
        public virtual ProcessMenuItem[] ProcessMenu { get { return null; } }

        public void Attach(IShellContext shell) { Shell = shell; OnAttached(); }
        protected virtual void OnAttached() { }

        public virtual void OnEnter(NavArgs args) { }   // 화면 진입
        public virtual void OnLeave() { }               // 화면 이탈
        public virtual bool OnBack() { return true; }   // false 반환 시 뒤로가기 취소

        protected void Msg(string t, MsgLevel lv) { if (Shell != null) Shell.ShowMessage(t, lv); }

        // 생성자에서는 Component.DesignMode를 신뢰할 수 없으므로 Site로 판정한다.
        protected bool IsDesignMode
        {
            get { return this.Site != null && this.Site.DesignMode; }
        }

        #region 메뉴형 레이아웃 : 화면 전체를 cols x rows 로 균등분할

        // items 를 cols 열 그리드로 배치한다.
        // 실행 화면의 ClientSize 를 기준으로 계산하므로 QVGA/VGA 모두 여백 없이 꽉 찬다.
        // 나머지 픽셀은 앞쪽 행/열에 1px씩 분배하여 오차를 없앤다.
        protected void LayoutGrid(Control[] items, int cols, int gap)
        {
            if (items == null || items.Length < 1 || cols < 1) return;

            int W = this.ClientSize.Width;
            int H = this.ClientSize.Height;
            if (W < 40 || H < 40) return;

            int rows = (items.Length + cols - 1) / cols;

            int availW = W - gap * (cols + 1);
            int availH = H - gap * (rows + 1);
            if (availW < cols || availH < rows) return;

            int baseW = availW / cols, extraW = availW % cols;
            int baseH = availH / rows, extraH = availH % rows;

            int[] colX = new int[cols];
            int[] colW = new int[cols];
            int x = gap;
            for (int c = 0; c < cols; c++)
            {
                colW[c] = baseW + (c < extraW ? 1 : 0);
                colX[c] = x;
                x += colW[c] + gap;
            }

            int[] rowY = new int[rows];
            int[] rowH = new int[rows];
            int y = gap;
            for (int r = 0; r < rows; r++)
            {
                rowH[r] = baseH + (r < extraH ? 1 : 0);
                rowY[r] = y;
                y += rowH[r] + gap;
            }

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) continue;
                int r = i / cols, c = i % cols;
                items[i].Bounds = new Rectangle(colX[c], rowY[r], colW[c], rowH[r]);
            }
        }

        // 화면 폭에 따른 권장 열 수/간격 (QVGA 240 / VGA 480)
        protected bool IsNarrow { get { return this.ClientSize.Width < 320; } }
        protected int GridGap { get { return IsNarrow ? 4 : 8; } }

        #endregion

        #region 업무형 레이아웃 : 디자이너 좌표/폰트를 실행 해상도에 비례 스케일

        private Dictionary<Control, Rectangle> _designBounds;
        private Dictionary<Control, Font> _designFonts;
        private List<Font> _madeFonts;
        private float _lastFontScale;

        // 생성자 끝(테마 적용 후)에서 1회 호출한다. 이 시점의 좌표/폰트가 기준값이 된다.
        protected void CaptureDesignLayout()
        {
            _designBounds = new Dictionary<Control, Rectangle>();
            _designFonts = new Dictionary<Control, Font>();
            _madeFonts = new List<Font>();
            _lastFontScale = 1f;
            CaptureRec(this);
        }

        private void CaptureRec(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                _designBounds[c] = c.Bounds;

                // .NET CF 의 Panel 등 텍스트를 그리지 않는 컨트롤은 Font 를 지원하지 않고
                // NotSupportedException 을 던진다. 이런 컨트롤은 폰트 스케일 대상에서 제외한다.
                try { _designFonts[c] = c.Font; }
                catch (NotSupportedException) { }

                if (c.Controls.Count > 0) CaptureRec(c);
            }
        }

        // OnResize 에서 호출. CaptureDesignLayout() 이전이면 아무것도 하지 않는다.
        protected void ScaleToClient()
        {
            if (_designBounds == null) return;

            int W = this.ClientSize.Width;
            int H = this.ClientSize.Height;
            if (W < 40 || H < 40) return;

            float sx = (float)W / (float)DesignSize.Width;
            float sy = (float)H / (float)DesignSize.Height;

            ScaleBoundsRec(this, sx, sy);

            // 폰트는 가로/세로 중 작은 배율로 통일(글자 잘림 방지)
            float sf = sx < sy ? sx : sy;
            if (Math.Abs(sf - _lastFontScale) > 0.01f)
            {
                _lastFontScale = sf;
                ScaleFonts(sf);
            }
        }

        private void ScaleBoundsRec(Control parent, float sx, float sy)
        {
            foreach (Control c in parent.Controls)
            {
                Rectangle d;
                if (_designBounds.TryGetValue(c, out d))
                {
                    if (c.Dock == DockStyle.None)
                    {
                        c.Bounds = new Rectangle(
                            (int)(d.X * sx + 0.5f), (int)(d.Y * sy + 0.5f),
                            (int)(d.Width * sx + 0.5f), (int)(d.Height * sy + 0.5f));
                    }
                    else if (c.Dock == DockStyle.Top || c.Dock == DockStyle.Bottom)
                    {
                        c.Height = (int)(d.Height * sy + 0.5f);   // Dock 높이만 스케일
                    }
                    else if (c.Dock == DockStyle.Left || c.Dock == DockStyle.Right)
                    {
                        c.Width = (int)(d.Width * sx + 0.5f);
                    }
                }
                if (c.Controls.Count > 0) ScaleBoundsRec(c, sx, sy);
            }
        }

        // 새 Font 를 먼저 할당한 뒤 이전 Font 를 Dispose (WM GDI 핸들 누수 방지).
        // Theme 의 static Font 는 _madeFonts 에 담기지 않으므로 Dispose 대상이 아니다.
        private void ScaleFonts(float s)
        {
            List<Font> old = _madeFonts;
            _madeFonts = new List<Font>();

            ScaleFontsRec(this, s);

            for (int i = 0; i < old.Count; i++)
            {
                try { old[i].Dispose(); }
                catch { }
            }
        }

        private void ScaleFontsRec(Control parent, float s)
        {
            foreach (Control c in parent.Controls)
            {
                Font d;
                if (_designFonts.TryGetValue(c, out d) && d != null)
                {
                    try
                    {
                        if (Math.Abs(s - 1f) < 0.01f)
                        {
                            c.Font = d;                       // 원본(Theme) 폰트로 복귀
                        }
                        else
                        {
                            float size = d.Size * s;
                            if (size < 5f) size = 5f;         // WM 최소 가독 크기
                            Font nf = new Font(d.Name, size, d.Style);
                            _madeFonts.Add(nf);
                            c.Font = nf;
                        }
                    }
                    catch (NotSupportedException) { }
                }
                if (c.Controls.Count > 0) ScaleFontsRec(c, s);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _madeFonts != null)
            {
                for (int i = 0; i < _madeFonts.Count; i++)
                {
                    try { _madeFonts[i].Dispose(); }
                    catch { }
                }
                _madeFonts.Clear();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
