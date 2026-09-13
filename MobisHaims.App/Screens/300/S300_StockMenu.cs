using System;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [300] 재고메뉴
    //
    // 항목과 화면번호는 서버 메뉴(ds_Menu)의 실제 값을 따랐다.
    //   P133 파트별재고 321 / P132 LOC별재고 320 / P135 부품정보조회 324 / P134 부품수불이력 323
    //   P192 재물조사 302 / P188 재물조사(LOC) 301 / P131 재물조사대상 310 / P190 재고조정 330
    //
    // 좌표와 크기, 색, 폰트는 전부 S300_StockMenu.Designer.cs 에서 관리한다.
    public sealed partial class S300_StockMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.StockMenu; } }
        public override string ScreenName { get { return "재고메뉴"; } }

        public S300_StockMenu()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            btnPartStock.Tag = ScreenId.StockByPart;
            btnLocStock.Tag = ScreenId.StockByLoc;
            btnPartInfo.Tag = ScreenId.PartInfo;
            btnMoveHist.Tag = ScreenId.PartMoveHist;
            btnInventory.Tag = ScreenId.Inventory;
            btnLocInventory.Tag = ScreenId.LocInventory;
            btnInvTarget.Tag = ScreenId.InventoryTarget;
            btnAdjust.Tag = ScreenId.StockAdjust;
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
            Msg("재고 업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
