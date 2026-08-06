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

        public FooterControl()
        {
            InitializeComponent();

            // Theme 의존 값은 디자이너가 직렬화 못 하므로 여기서 적용
            // (InitializeComponent에 넣으면 디자이너 로드/재생성 시 깨짐)
            this.BackColor = Theme.FooterBack;
            lblMsg.Font = Theme.BodyFont;
            lblMsg.ForeColor = Theme.FooterFore;
        }

        public void SetMessage(string text, MsgLevel lv)
        {
            lblMsg.Text = " " + text;
            pnlDot.BackColor = ColorFor(lv);
        }

        private static Color ColorFor(MsgLevel lv)
        {
            switch (lv)
            {
                case MsgLevel.Success: return Color.Green;
                case MsgLevel.Warn: return Color.Orange;
                case MsgLevel.Error: return Color.Red;
                default: return Color.RoyalBlue;
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
