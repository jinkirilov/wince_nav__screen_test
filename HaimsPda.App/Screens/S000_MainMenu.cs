using System;
using System.Windows.Forms;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [000] 메인메뉴 : 입고 / 출고 / 재고 / LOC / 배송
    // (조회메뉴는 메인에서 뺐다. 화면번호 600대는 JUMP 용으로 ScreenId 에만 남김)
    //
    // 좌표와 크기, 색, 폰트는 전부 S000_MainMenu.Designer.cs (VS2008 디자이너)에서 관리한다.
    // 디자이너 기준 해상도는 ShellForm._content @ VGA = 480 x 528 (192dpi).
    // QVGA(96dpi) 축소는 ShellForm 의 AutoScaleMode.Dpi 가 처리하므로
    // 이 화면에서는 좌표를 계산하지 않는다.
    public sealed partial class S000_MainMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.Main; } }
        public override string ScreenName { get { return "메인메뉴"; } }

        public S000_MainMenu()
        {
            InitializeComponent();
            if (IsDesignMode) 
                return;

            btnInbound.Tag = ScreenId.InboundMenu;
            btnOutbound.Tag = ScreenId.OutboundMenu;
            btnStock.Tag = ScreenId.StockMenu;
            btnLoc.Tag = ScreenId.LocMenu;
            btnDelivery.Tag = ScreenId.DeliveryMenu;
            btnPallet.Tag = ScreenId.PalletMenu;
        }

        private void OnTileClick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null || c.Tag == null || Shell == null) 
                return;

            Shell.Navigate(Convert.ToInt32(c.Tag), NavArgs.Empty);
        }

        public override void OnEnter(NavArgs args)
        {
            Msg("업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
