using System;
using System.Drawing;
using System.Windows.Forms;

namespace HaimsPda.Controls
{
    /// <summary>
    /// 포커스를 받으면 배경색이 바뀌는 TextBox.
    ///
    /// 배경색은 "저장했다가 복원"하지 않고 상태(포커스 여부, ReadOnly)로 매번 계산한다.
    /// - BackColor        : 평상시 색. 포커스 중에 바꿔도 유지된다(디자이너 값 그대로 사용).
    /// - FocusedBackColor : 포커스 중 색. ReadOnly 이면 적용하지 않는다.
    ///
    /// KeyDown / KeyUp / KeyPress 는 건드리지 않는다.
    /// ReadOnly 라도 화면의 Enter 처리 핸들러는 그대로 호출된다.
    /// (입력 차단은 TextBox.ReadOnly 가 처리한다)
    /// </summary>
    public class CustTextBox : TextBox
    {
        private Color _normalBackColor = Color.White;
        private Color _focusedBackColor = Color.Yellow;
        private bool _selectAllOnFocus = true;
        private bool _hasFocus;

        public CustTextBox()
        {
            ApplyColor();
        }

        /// <summary>평상시 배경색. 실제 표시색은 포커스/ReadOnly 상태로 결정된다.</summary>
        public new Color BackColor
        {
            get { return _normalBackColor; }
            set { _normalBackColor = value; ApplyColor(); }
        }

        /// <summary>포커스 중 배경색. 기본값 Yellow.</summary>
        public Color FocusedBackColor
        {
            get { return _focusedBackColor; }
            set { _focusedBackColor = value; ApplyColor(); }
        }

        /// <summary>포커스를 받을 때 전체 선택 여부. 기본값 true. (ReadOnly 면 하지 않음)</summary>
        public bool SelectAllOnFocus
        {
            get { return _selectAllOnFocus; }
            set { _selectAllOnFocus = value; }
        }

        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; ApplyColor(); }
        }

        private void ApplyColor()
        {
            Color c = (_hasFocus && !base.ReadOnly) ? _focusedBackColor : _normalBackColor;
            if (base.BackColor != c)
                base.BackColor = c;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _hasFocus = true;
            ApplyColor();
            base.OnGotFocus(e);   // 화면의 GotFocus 핸들러는 색이 바뀐 뒤 호출

            // 터치로 포커스를 받으면 직후 탭 처리로 선택이 풀리는 경우가 있어 한 박자 늦춘다
            if (_selectAllOnFocus && !base.ReadOnly)
                BeginInvoke(new EventHandler(DelayedSelectAll));
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _hasFocus = false;
            ApplyColor();
            base.OnLostFocus(e);
        }

        private void DelayedSelectAll(object sender, EventArgs e)
        {
            if (!_hasFocus || base.ReadOnly) return;
            SelectAll();
        }
    }
}
