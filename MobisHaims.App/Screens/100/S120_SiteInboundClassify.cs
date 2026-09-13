using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Data;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [120] 사업소입고분류 : 부번 스캔 -> 조회 -> 수량 입력 -> 저장
    //
    // 좌표와 크기, 색, 폰트는 전부 S120_SiteInboundClassify.Designer.cs (VS2008 디자이너)에서 관리한다.
    // QVGA 축소는 ShellForm 의 AutoScaleMode.Dpi 가 처리하므로
    // 이 화면에서는 좌표를 계산하지 않는다.
    public sealed partial class S120_SiteInboundClassify : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.SiteInboundClassify; } }
        public override string ScreenName { get { return "사업소입고분류"; } }

        public S120_SiteInboundClassify()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            txtPart.Focus();
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; LookupPart(); }
        }

        private void LookupPart()
        {
            string part = txtPart.Text.Trim();
            if (part.Length == 0) { Msg("부번을 입력하세요.", MsgLevel.Warn); return; }

            Dictionary<string, object> item = new Dictionary<string, object>();
            item["partNo"] = part;
            item["whCode"] = Shell.Session.WhCode;

            IfMessage res = Shell.If.Send("IF_INB_PART", item);
            if (!res.IsSuccess) { Msg(res.header.ifFailMsg, MsgLevel.Error); return; }

            lblReserve.Text = res.ItemStr("reserveQty");
            lblAssign.Text = res.ItemStr("assignQty");
            lblAssignCnt.Text = res.ItemStr("assignCnt");
            lblLoc.Text = res.ItemStr("loc");
            lblCurStock.Text = res.ItemStr("curStock");
            lblNotRecv.Text = res.ItemStr("notRecv");
            txtQty.Text = res.ItemStr("assignQty");

            Msg("정상조회되었습니다.", MsgLevel.Success);
            txtQty.Focus();
            txtQty.SelectAll();
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (txtPart.Text.Trim().Length == 0) { Msg("부번을 먼저 조회하세요.", MsgLevel.Warn); return; }

            Dictionary<string, object> item = new Dictionary<string, object>();
            item["partNo"] = txtPart.Text.Trim();
            item["loc"] = lblLoc.Text;
            item["qty"] = txtQty.Text.Trim();
            item["whCode"] = Shell.Session.WhCode;

            IfMessage res = Shell.If.Send("IF_INB_CLASSIFY_SAVE", item);
            if (!res.IsSuccess) { Msg(res.header.ifFailMsg, MsgLevel.Error); return; }

            Msg("저장되었습니다. (수량 " + res.ItemStr("savedQty") + ")", MsgLevel.Success);
            ClearAll();
            txtPart.Focus();
        }

        private void OnSort(object sender, EventArgs e)
        {
            Shell.Navigate(ScreenId.SortInboundSave, NavArgs.Empty);
        }

        private void OnLoc(object sender, EventArgs e)
        {
            Msg("LOC 등록 화면 연결 예정", MsgLevel.Info);
        }

        private void OnNotRecv(object sender, EventArgs e)
        {
            Msg("미수령 등록 처리 예정", MsgLevel.Info);
        }

        private void OnClear(object sender, EventArgs e)
        {
            ClearAll();
            Msg("초기화", MsgLevel.Info);
        }

        private void ClearAll()
        {
            txtPart.Text = "";
            lblReserve.Text = "0"; lblAssign.Text = "0"; lblAssignCnt.Text = "0";
            lblLoc.Text = ""; lblCurStock.Text = "0"; lblNotRecv.Text = "0";
            txtQty.Text = "0";
        }
    }
}
