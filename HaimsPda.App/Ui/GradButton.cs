using System;
using System.Drawing;
using System.Windows.Forms;

namespace HaimsPda.Ui
{
    public struct GradStop
    {
        public float Pos;   // 0.0 ~ 1.0
        public Color Color;
        public GradStop(float pos, Color c) { Pos = pos; Color = c; }
    }

    /// <summary>
    /// login.css 의 세로 그라데이션 버튼을 재현한다.
    /// CF 3.5 에는 LinearGradientBrush 가 없어 라인 단위로 직접 그린 뒤
    /// 결과를 비트맵에 캐시한다 (크기가 바뀔 때만 재생성).
    /// </summary>
    public class GradButton : Control
    {
        private GradStop[] _normal;
        private GradStop[] _pressed;
        private Color _border = Color.FromArgb(0x7f, 0x80, 0x81);
        private Color _borderTop = Color.FromArgb(0xa6, 0xa8, 0xa9);
        private Color _textShadow = Color.Empty;

        private Bitmap _cacheNormal;
        private Bitmap _cachePressed;
        private bool _down;

        public GradButton()
        {
            // btn_jp 기본값 (회색)
            _normal = new GradStop[] {
                new GradStop(0.00f, Color.FromArgb(0xf4,0xf4,0xf4)),
                new GradStop(0.05f, Color.FromArgb(0xe2,0xe2,0xe2)),
                new GradStop(0.97f, Color.FromArgb(0xc2,0xc2,0xc2)),
                new GradStop(1.00f, Color.FromArgb(0xd0,0xd0,0xd0))
            };
            _pressed = new GradStop[] {
                new GradStop(0.00f, Color.FromArgb(0xab,0xab,0xab)),
                new GradStop(0.10f, Color.FromArgb(0xb8,0xb8,0xb8)),
                new GradStop(0.65f, Color.FromArgb(0xc4,0xc4,0xc4)),
                new GradStop(1.00f, Color.FromArgb(0xc6,0xc6,0xc6))
            };
            ForeColor = Color.Black;
            _textShadow = Color.FromArgb(0xff, 0xff, 0xff);
        }

        /// <summary>x-button-confirm (로그인 버튼, 금색) 스타일로 전환</summary>
        public void UseConfirmStyle()
        {
            _normal = new GradStop[] {
                new GradStop(0.00f, Color.FromArgb(0xd2,0x8e,0x19)),
                new GradStop(0.90f, Color.FromArgb(0xaa,0x73,0x14)),
                new GradStop(1.00f, Color.FromArgb(0x88,0x5c,0x10))
            };
            _pressed = new GradStop[] {
                new GradStop(0.00f, Color.FromArgb(0x88,0x5c,0x10)),
                new GradStop(0.90f, Color.FromArgb(0x77,0x50,0x0e)),
                new GradStop(1.00f, Color.FromArgb(0x66,0x47,0x11))
            };
            _border = Color.FromArgb(0x66, 0x47, 0x11);
            _borderTop = Color.FromArgb(0x73, 0x56, 0x20);
            ForeColor = Color.White;
            _textShadow = Color.FromArgb(0x00, 0x00, 0x00);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            DisposeCache();
            base.OnResize(e);
        }

        private void DisposeCache()
        {
            if (_cacheNormal != null) { _cacheNormal.Dispose(); _cacheNormal = null; }
            if (_cachePressed != null) { _cachePressed.Dispose(); _cachePressed = null; }
        }

        private Bitmap Build(GradStop[] stops)
        {
            int w = Width, h = Height;
            if (w <= 0 || h <= 0) return null;

            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int y = 0; y < h; y++)
                {
                    float t = (h == 1) ? 0f : (float)y / (h - 1);
                    Color c = Lerp(stops, t);
                    using (Pen p = new Pen(c))
                        g.DrawLine(p, 0, y, w, y);
                }

                using (Pen pb = new Pen(_border))
                    g.DrawRectangle(pb, 0, 0, w - 1, h - 1);
                using (Pen pt = new Pen(_borderTop))
                    g.DrawLine(pt, 1, 0, w - 2, 0);
            }
            return bmp;
        }

        private static Color Lerp(GradStop[] s, float t)
        {
            if (s.Length == 0) return Color.Gray;
            if (t <= s[0].Pos) return s[0].Color;
            for (int i = 1; i < s.Length; i++)
            {
                if (t <= s[i].Pos)
                {
                    float span = s[i].Pos - s[i - 1].Pos;
                    float k = (span <= 0f) ? 0f : (t - s[i - 1].Pos) / span;
                    Color a = s[i - 1].Color, b = s[i].Color;
                    return Color.FromArgb(
                        (int)(a.R + (b.R - a.R) * k),
                        (int)(a.G + (b.G - a.G) * k),
                        (int)(a.B + (b.B - a.B) * k));
                }
            }
            return s[s.Length - 1].Color;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp;
            if (_down)
            {
                if (_cachePressed == null) _cachePressed = Build(_pressed);
                bmp = _cachePressed;
            }
            else
            {
                if (_cacheNormal == null) _cacheNormal = Build(_normal);
                bmp = _cacheNormal;
            }
            if (bmp != null) e.Graphics.DrawImage(bmp, 0, 0);

            if (Text != null && Text.Length > 0)
            {
                SizeF sz = e.Graphics.MeasureString(Text, Font);
                float x = (Width - sz.Width) / 2f;
                float y = (Height - sz.Height) / 2f;
                if (_down) { x += 1; y += 1; }

                if (_textShadow != Color.Empty)
                    using (SolidBrush sb = new SolidBrush(_textShadow))
                        e.Graphics.DrawString(Text, Font, sb, x, y + 1);

                using (SolidBrush fb = new SolidBrush(ForeColor))
                    e.Graphics.DrawString(Text, Font, fb, x, y);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { /* 전체를 직접 그림 */ }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _down = true; Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            bool wasDown = _down;
            _down = false; Invalidate();
            base.OnMouseUp(e);
            if (wasDown && e.X >= 0 && e.Y >= 0 && e.X < Width && e.Y < Height)
                OnClick(EventArgs.Empty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) DisposeCache();
            base.Dispose(disposing);
        }
    }
}
