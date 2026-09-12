using System;
using System.Drawing;
using System.Windows.Forms;
using MobisHaims.Ui;

namespace MobisHaims.Controls
{
    // 상단 크롬: [햄버거] [화면번호]화면명 [닫기]
    public sealed partial class HeaderControl : UserControl
    {
        public event EventHandler MenuClicked;
        public event EventHandler CloseClicked;

        public HeaderControl()
        {
            InitializeComponent();

            // Theme 의존 값은 디자이너가 직렬화 못 하므로 여기서 적용
            // (InitializeComponent에 넣으면 디자이너 로드/재생성 시 깨짐)
            this.BackColor = Theme.HeaderBack;
            lblTitle.ForeColor = Theme.HeaderFore;
            lblTitle.BackColor = Theme.HeaderBack;
            lblTitle.Font = Theme.TitleFont;
            lblTitle.Align = VAlign.MiddleLeft;
            btnMenu.BackColor = Theme.HeaderBack;
            btnMenu.ForeColor = Theme.HeaderFore;
            btnClose.BackColor = Theme.HeaderBack;
            btnClose.ForeColor = Theme.HeaderFore;

        }

        public void SetTitle(int no, string name)
        {
            lblTitle.Text = " [" + no.ToString("D3") + "]" + name;
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            if (MenuClicked != null) MenuClicked(this, EventArgs.Empty);
        }

        private void OnCloseClick(object sender, EventArgs e)
        {
            if (CloseClicked != null) CloseClicked(this, EventArgs.Empty);
        }

    }
}
