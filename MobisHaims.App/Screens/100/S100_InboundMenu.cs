using System;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [100] 입고메뉴 : 일반입고분류/전문점입고분류/입고저장/정렬입고저장/예약내역/대기품목조회
    //
    // 좌표와 크기, 색, 폰트는 전부 S100_InboundMenu.Designer.cs (VS2008 디자이너)에서 관리한다.
    // QVGA 축소는 ShellForm 의 AutoScaleMode.Dpi 가 처리한다.
    public sealed partial class S100_InboundMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.InboundMenu; } }
        public override string ScreenName { get { return "입고메뉴"; } }

        public S100_InboundMenu()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            btnSiteClassify.Tag = ScreenId.SiteInboundClassify;
            btnShopClassify.Tag = ScreenId.ShopInboundClassify;
            btnInboundSave.Tag = ScreenId.InboundSave;
            // 메뉴의 "정렬입고 저장" 버튼은 141 이다. 142(직입고저장)와 다른 화면이다.
            btnSortInboundSave.Tag = ScreenId.SortInboundSave;
            btnReserveList.Tag = ScreenId.ReserveList;
            btnWaitInquiry.Tag = ScreenId.WaitPartInquiry;
            btn142.Tag = ScreenId.DirectInboundSave;
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            
            if (c == null || c.Tag == null || Shell == null) 
                return;
            
            Shell.Navigate(Convert.ToInt32(c.Tag), NavArgs.Empty);
        }

        public override void OnEnter(NavArgs args)
        {
            Msg("입고 업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
