using System;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [400] LOC메뉴 (원본 화면 제목 "로케이션메뉴")
    //
    // 항목과 화면번호는 서버 메뉴(ds_Menu 1D00/P140)와 HAR(2026-10-05 10:44)의 실제 값을 따랐다.
    //   P145 LOC재고이동 401 / P141 LOC등록 410 / P142 부품LOC정렬 420
    //   P143 창고이전(출고) 430 / P144 창고이전(입고) 431
    //   P180 창고이관(출고) 440 / P181 창고이관(입고) 441
    // 배치는 매뉴얼(LOC관리 161p) 화면과 같다 : 1행 LOC재고이동 단독, 이후 2열.
    //
    // 좌표와 크기, 색, 폰트는 전부 S400_LocMenu.Designer.cs 에서 관리한다.
    public sealed partial class S400_LocMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.LocMenu; } }
        public override string ScreenName { get { return "로케이션메뉴"; } }

        public S400_LocMenu()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            btnLocMove.Tag = ScreenId.LocMove;
            btnLocRegister.Tag = ScreenId.LocRegister;
            btnLocSort.Tag = ScreenId.PartLocSort;
            btnTransferOut.Tag = ScreenId.WhTransferOut;
            btnTransferIn.Tag = ScreenId.WhTransferIn;
            btnMigrateOut.Tag = ScreenId.WhMigrateOut;
            btnMigrateIn.Tag = ScreenId.WhMigrateIn;
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
            Msg("LOC 업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
