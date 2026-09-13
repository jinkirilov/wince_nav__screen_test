using System;
using System.Drawing;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Ui;


namespace MobisHaims.Controls
{
    public delegate void JumpEventHandler(int screenNo);

    // 하단 상시 노출: [JUMP] [상태점] 처리결과/작업자(소속) 메시지
    public sealed partial class FooterControl : UserControl
    {
        public event JumpEventHandler JumpRequested;

        // 로그인 직후 셸이 넣어 주는 상시 문구 : "대리점명 사용자명 yyyy-MM-dd HH:mm"
        private string _defaultText = "";

        public FooterControl()
        {
            InitializeComponent();

            // Theme 의존 값은 디자이너가 직렬화 못 하므로 여기서 적용
            // (InitializeComponent에 넣으면 디자이너 로드/재생성 시 깨짐)
            this.BackColor = Theme.FooterBack;
            lbMsg.Font = Theme.FooterMsgFont;
            lbMsg.ForeColor = Theme.FooterFore;

            // JUMP 아이콘. 없으면 배경색만 남고 클릭은 그대로 동작한다.
            System.Drawing.Bitmap ico = HaimsPda.Ui.Res.Get("b_ic_jump.png");
            if (ico != null) btnJump.Image = ico;
        }

        /// <summary>로그인 정보 문구를 등록한다. 서버 메시지가 없을 때 이게 보인다.</summary>
        public void SetDefaultText(string text)
        {
            _defaultText = (text == null) ? "" : text;
            ShowDefault();
        }

        /// <summary>서버 메시지를 지우고 로그인 정보 문구로 되돌린다.</summary>
        public void ShowDefault()
        {
            SetMessage(_defaultText, MsgLevel.Info);
        }

        /// <summary>서버 응답 결과 메시지 표시.</summary>
        public void SetMessage(string text, MsgLevel lv)
        {
            // CF 에는 OnForeColorChanged 가 없다. 색을 바꾼 뒤 직접 Invalidate 한다.
            lbMsg.ForeColor = ColorFor(lv);
            lbMsg.Text = " " + ((text == null) ? "" : text);
            lbMsg.Invalidate();
        }

        private static Color ColorFor(MsgLevel lv)
        {
            // 푸터 배경이 어두우므로 밝은 계열로 쓴다.
            switch (lv)
            {
                case MsgLevel.Success: return Color.FromArgb(144, 238, 144);
                case MsgLevel.Warn: return Color.FromArgb(255, 190, 80);
                case MsgLevel.Error: return Color.FromArgb(255, 110, 110);
                default: return Color.White;
            }
        }

        private void OnJumpClick(object sender, EventArgs e)
        {
            using (JumpForm f = new JumpForm())
            {
                if (f.ShowDialog() == DialogResult.OK && JumpRequested != null)
                    JumpRequested(f.ScreenNo);
            }
        }
    }
}
