using System;
using System.Windows.Forms;
using HaimsPda.Core;
using HaimsPda.Net;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [200] 출고메뉴
    //
    // 버튼과 화면번호는 서버 메뉴(1B00 하위)를 따른다.
    //   P126 피킹대상/처리 201 / P182 피킹대상조회(전문점) 203 / P183 피킹대상(전문점) 204
    //   P122 피킹(판매증표출고) 220 / P184 판매증표선택 221 / P125 피킹분류(TAG) 250
    //   P198 배송분류(CASE) 251 / P186 피킹분류(증표) 261
    //
    // 203 / 204 / 250 은 전문점 전용이다 (원본 Sub.xml : USR_USRTY == "A" 면 대리점이라 막는다).
    // 판정은 UserKind 에 모아 두었고, 개발 중(UserKind.Enforce = false)에는 구분 없이 모두 연다.
    //
    // 좌표와 크기, 색, 폰트는 전부 S200_OutboundMenu.Designer.cs 에서 관리한다.
    public sealed partial class S200_OutboundMenu : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.OutboundMenu; } }
        public override string ScreenName { get { return "출고메뉴"; } }

        public S200_OutboundMenu()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            btnPickTarget.Tag = ScreenId.PickTarget;
            btnShopTarget.Tag = ScreenId.PickTargetShop;
            btnShopTarget2.Tag = ScreenId.PickTargetShop2;
            btnSlipPicking.Tag = ScreenId.SlipPicking;
            btnSlipSelect.Tag = ScreenId.SlipSelect;
            btnTagClassify.Tag = ScreenId.TagClassify;
            btnCaseClassify.Tag = ScreenId.CaseClassify;
            btnSlipClassify.Tag = ScreenId.SlipClassifySelect;
        }

        private static bool IsShopOnly(int id)
        {
            return id == ScreenId.PickTargetShop || id == ScreenId.PickTargetShop2 || id == ScreenId.TagClassify;
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null || c.Tag == null || Shell == null) return;

            int id = Convert.ToInt32(c.Tag);
            if (IsShopOnly(id))
            {
                string block = UserKind.ShopOnlyBlock();
                if (block != null) { MessageBox.Show(block, "[200] " + ScreenName); return; }
            }
            Shell.Navigate(id, NavArgs.Empty);
        }

        public override void OnEnter(NavArgs args)
        {
            Msg("출고 업무를 선택하세요.", MsgLevel.Info);
        }
    }
}
