using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [262] 증표분류세부 : [260] 의 증표들 안 분류 대상 라인 목록 (조회 전용)
    // 원본 웹화면 : /ui/ws/plus/PL262_W01.xml (서버 메뉴 P191)
    //
    //   PL260_W01_S05 -> ds_List_260_S05 (TRS_LEP / TRS_PTNO / TRS_SALQT / VCHNO)
    //   총건수 = 행 수, 총수량 = TRS_SALQT 합
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S262_SlipClassifyDetail.Designer.cs 에서 관리한다.
    public sealed partial class S262_SlipClassifyDetail : WorkScreen
    {
        public override int ScreenNo { get { return ScreenId.SlipClassifyDetail; } }
        public override string ScreenName { get { return "증표분류세부"; } }

        public S262_SlipClassifyDetail()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstLines.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            string wh = Arg(args, "WHSCD"), from = Arg(args, "FROM_SALDT"), to = Arg(args, "TO_SALDT");
            string vnd = Arg(args, "SELECT"), vch = Arg(args, "DETAIL_INFO");

            lblVndmn.Text = vnd;
            lblWh.Text = wh;
            lblDates.Text = Dash(from) + " ~ " + Dash(to);
            lblCnt.Text = "0";
            lblQty.Text = "0";
            lstLines.Items.Clear();

            if (vch.Length == 0) { Msg("[260] 에서 진입하세요.", MsgLevel.Warn); return; }

            Run("조회중...",
                delegate { return OutboundService.SearchClassifyDetail(wh, from, to, vnd, vch); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    ArrayList rows = (ArrayList)r;
                    int qty = 0;
                    lstLines.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(o["TRS_LEP"]);
                            it.SubItems.Add(PartNo.Display(o["TRS_PTNO"]));
                            it.SubItems.Add(Num(o["TRS_SALQT"]));
                            it.SubItems.Add(o["VCHNO"]);
                            lstLines.Items.Add(it);
                            qty += ToInt(o["TRS_SALQT"]);
                        }
                    }
                    finally { lstLines.EndUpdate(); }
                    lblCnt.Text = rows.Count.ToString();
                    lblQty.Text = qty.ToString();

                    if (rows.Count == 0) { Report("MP578", "분류 대상이 없습니다.", MsgLevel.Warn); return; }
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                });
        }

        private static string Dash(string d)
        {
            return (d != null && d.Length == 8) ? d.Substring(0, 4) + "-" + d.Substring(4, 2) + "-" + d.Substring(6) : d;
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        private void OnClose(object sender, EventArgs e)
        {
            if (Busy) return;
            Shell.GoBack();
        }

        protected override void SetButtons(bool on)
        {
            btnClose.Enabled = on;
        }
    }
}
