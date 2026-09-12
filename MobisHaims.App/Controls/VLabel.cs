using System;
using System.Drawing;
using System.Windows.Forms;

namespace MobisHaims.Controls
{
    /// <summary>
    /// 9방향 정렬.
    ///
    /// CF 의 System.Drawing.ContentAlignment 에는 TopLeft / TopCenter / TopRight
    /// 세 개뿐이라 Middle*, Bottom* 를 쓸 수 없다. 그래서 별도로 정의한다.
    /// </summary>
    public enum VAlign
    {
        TopLeft, TopCenter, TopRight,
        MiddleLeft, MiddleCenter, MiddleRight,
        BottomLeft, BottomCenter, BottomRight
    }

    /// <summary>
    /// 수직 가운데 정렬이 되는 라벨.
    ///
    /// CF 의 Label.TextAlign 은 TopLeft / TopCenter / TopRight 만 지원한다.
    /// MiddleLeft 를 지정해도 위쪽에 그려지므로, 좌표를 옮기는 대신
    /// 컨트롤 안에서 직접 가운데에 그린다.
    ///
    /// 좌표/크기는 디자이너가 그대로 잡으면 되고, 이 컨트롤은 그 안에서
    /// 텍스트 위치만 책임진다. AutoScaleMode.Dpi 로 크기가 바뀌어도
    /// 그릴 때마다 다시 계산하므로 어긋나지 않는다.
    /// </summary>
    public class VLabel : Control
    {
        private VAlign _align = VAlign.MiddleLeft;

        public VLabel()
        {
            ForeColor = Color.White;
        }

        /// <summary>9방향 전부 지원한다. 기본값 MiddleLeft.</summary>
        public VAlign Align
        {
            get { return _align; }
            set { _align = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (SolidBrush bb = new SolidBrush(BackColor))
                g.FillRectangle(bb, 0, 0, Width, Height);

            string t = Text;
            if (t == null || t.Length == 0) return;

            SizeF sz = g.MeasureString(t, Font);

            float x;
            switch (_align)
            {
                case VAlign.TopCenter:
                case VAlign.MiddleCenter:
                case VAlign.BottomCenter:
                    x = (Width - sz.Width) / 2f; break;

                case VAlign.TopRight:
                case VAlign.MiddleRight:
                case VAlign.BottomRight:
                    x = Width - sz.Width; break;

                default:
                    x = 0; break;
            }

            float y;
            switch (_align)
            {
                case VAlign.TopLeft:
                case VAlign.TopCenter:
                case VAlign.TopRight:
                    y = 0; break;

                case VAlign.BottomLeft:
                case VAlign.BottomCenter:
                case VAlign.BottomRight:
                    y = Height - sz.Height; break;

                default:
                    y = (Height - sz.Height) / 2f; break;
            }

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            using (SolidBrush fb = new SolidBrush(ForeColor))
                g.DrawString(t, Font, fb, x, y);
        }

        // 배경까지 OnPaint 에서 직접 칠한다(깜빡임 감소)
        protected override void OnPaintBackground(PaintEventArgs e) { }

        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        // CF 에는 OnForeColorChanged / OnBackColorChanged 가 없다.
        // 실행 중에 색을 바꾼다면 호출부에서 Invalidate() 를 직접 부른다.
    }
}
