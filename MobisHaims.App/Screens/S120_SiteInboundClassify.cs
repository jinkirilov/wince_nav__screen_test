using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MobisHaims.Core;
using MobisHaims.Data;
using MobisHaims.Nav;
using MobisHaims.Ui;

namespace MobisHaims.Screens
{
    // [120] 사업소입고분류 : 부번 스캔 -> 조회 -> 수량 입력 -> 저장
    //
    // 좌표를 코드로 계산하던 LayoutFields()/LayoutButtons()를 제거했다.
    // 배치는 전적으로 S120_SiteInboundClassify.Designer.cs (VS2008 디자이너)에서 결정한다.
    // ScaleToClient()가 디자이너 좌표/폰트를 실행 해상도에 비례 변환하므로
    // VGA(480x640)에서는 디자인 그대로, QVGA(240x320)에서는 축소되어 동일 배치로 보인다.
    public sealed partial class S120_SiteInboundClassify : ScreenBase
    {
        public override int ScreenNo { get { return ScreenId.SiteInboundClassify; } }
        public override string ScreenName { get { return "사업소입고분류"; } }

        public S120_SiteInboundClassify()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            ApplyTheme();
            CaptureDesignLayout();   // 이 시점의 좌표/폰트가 스케일 기준값
            ScaleToClient();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ScaleToClient();
        }

        // Designer.cs 의 리터럴 색상은 미리보기용. 런타임 기준값은 Theme.
        private void ApplyTheme()
        {
            this.BackColor = Theme.WorkBack;
            _fields.BackColor = Theme.WorkBack;
            _buttons.BackColor = Theme.WorkBack;

            lblPrefix.BackColor = Theme.HeaderBack;
            lblPrefix.ForeColor = Color.White;
            txtPart.BackColor = Theme.ScanBack;
            txtPart.Font = Theme.BodyFont;

            lblReserveCap.ForeColor = Theme.Accent;
            lblReserve.ForeColor = Theme.Accent;
            lblReserve.Font = Theme.BigFont;
            lblAssign.Font = Theme.BigFont;

            txtQty.BackColor = Theme.QtyBack;
            txtQty.Font = Theme.BigFont;

            Label[] body = { lblPartCap, lblPrefix, lblReserveCap, lblAssignCap, lblLocCap, lblLoc,
                             lblAssignCntCap, lblAssignCnt, lblCurStockCap, lblCurStock,
                             lblQtyCap, lblNotRecvCap, lblNotRecv };
            for (int i = 0; i < body.Length; i++) body[i].Font = Theme.BodyFont;

            foreach (Control c in _buttons.Controls)
            {
                c.Font = Theme.BtnFont;
                c.BackColor = Theme.MenuBtnBack;
                c.ForeColor = Color.White;
            }
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
