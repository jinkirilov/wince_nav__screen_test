using System;
using System.Drawing;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Nav;
using MobisHaims.Ui;

namespace MobisHaims.Screens
{
    // [100] 입고메뉴 : 사업소입고분류/전문점입고분류/입고저장/정렬입고저장/예약내역/입고대기품목조회
    // 컨트롤 선언은 S100_InboundMenu.Designer.cs (VS2008 디자이너 편집 가능).
    // 배치는 LayoutMenu()가 실행 시점 ClientSize 를 균등분할한다.
    public sealed partial class S100_InboundMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.InboundMenu; } }
        public override string ScreenName { get { return "입고메뉴"; } }

        // 미구현 화면 ID(ScreenId 상수 추가 시 교체)
        private const int ShopInboundClassify = 121;
        private const int ReserveList = 142;
        private const int InboundWaitInquiry = 143;

        private Button[] _btns;

        public S100_InboundMenu()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            _btns = new Button[]
            {
                btnSiteClassify, btnShopClassify, btnInboundSave,
                btnSortInboundSave, btnReserveList, btnWaitInquiry
            };

            btnSiteClassify.Tag = ScreenId.SiteInboundClassify;
            btnShopClassify.Tag = ShopInboundClassify;
            btnInboundSave.Tag = ScreenId.InboundSave;
            btnSortInboundSave.Tag = ScreenId.SortInboundSave;
            btnReserveList.Tag = ReserveList;
            btnWaitInquiry.Tag = InboundWaitInquiry;

            ApplyTheme();
            LayoutMenu();
        }

        private void ApplyTheme()
        {
            this.BackColor = Theme.MainBack;
            for (int i = 0; i < _btns.Length; i++)
            {
                _btns[i].BackColor = Theme.MenuBtnBack;
                _btns[i].ForeColor = Theme.MenuBtnFore;
                _btns[i].Font = Theme.BtnFont;
            }
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null || c.Tag == null || Shell == null) return;
            Shell.Navigate(Convert.ToInt32(c.Tag), NavArgs.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutMenu();
        }

        // 버튼 6개를 화면 전체에 균등 배치.
        //  VGA (480x528) : 2열 x 3행 -> 약 228 x 168
        //  QVGA(240x260) : 1열 x 6행 -> 약 232 x  36 (긴 메뉴명 잘림 방지)
        private void LayoutMenu()
        {
            if (_btns == null) return;
            LayoutGrid(_btns, IsNarrow ? 1 : 2, GridGap);
        }

        public override void OnEnter(NavArgs args)
        {
            Msg("입고 업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
