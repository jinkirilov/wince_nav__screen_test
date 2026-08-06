using System;
using System.Drawing;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Nav;
using MobisHaims.Ui;

namespace MobisHaims.Screens
{
    // [000] 메인메뉴 : 입고/출고/재고/LOC/조회 진입
    // 컨트롤 선언은 S000_MainMenu.Designer.cs (VS2008 디자이너 편집 가능).
    // 배치는 LayoutMenu()가 실행 시점 ClientSize 를 균등분할하므로 디자이너 좌표는 미리보기용이다.
    public sealed partial class S000_MainMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.Main; } }
        public override string ScreenName { get { return "메인메뉴"; } }

        private Button[] _tiles;

        public S000_MainMenu()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            _tiles = new Button[] { btnInbound, btnOutbound, btnStock, btnLoc, btnInquiry };

            btnInbound.Tag = ScreenId.InboundMenu;
            btnOutbound.Tag = ScreenId.OutboundMenu;
            btnStock.Tag = ScreenId.StockMenu;
            btnLoc.Tag = ScreenId.LocMenu;
            btnInquiry.Tag = ScreenId.InquiryMenu;

            ApplyTheme();
            LayoutMenu();
        }

        // Designer.cs 의 리터럴 색상은 미리보기용. 런타임 기준값은 Theme.
        private void ApplyTheme()
        {
            this.BackColor = Theme.MainBack;
            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i].BackColor = Theme.TileBack;
                _tiles[i].ForeColor = Theme.TileFore;
                _tiles[i].Font = Theme.BtnFont;
            }
        }

        private void OnTileClick(object sender, EventArgs e)
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

        // 타일 5개를 화면 전체에 균등 배치.
        //  VGA (480x528) : 3열 x 2행 -> 약 149 x 252
        //  QVGA(240x260) : 2열 x 3행 -> 약 114 x  81
        private void LayoutMenu()
        {
            if (_tiles == null) return;
            LayoutGrid(_tiles, IsNarrow ? 2 : 3, GridGap);
        }

        public override void OnEnter(NavArgs args)
        {
            Msg("업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
