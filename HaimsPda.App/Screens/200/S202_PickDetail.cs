using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [202] 피킹세부내역 : [201]/[204] 에서 고른 업체의 피킹 라인 목록 (조회 전용)
    //   [204](전문점)에서 오면 SHOP=Y 와 LINE/AREA 를 받아 PL230_W01_S01 로 조회하고, 피킹/리스트피킹은 230/281 로 간다.
    // 원본 웹화면 : /ui/ws/plus/PL202_W01.xml (서버 메뉴 P171)
    //
    //   원본은 201 이 PL210_W01_S02 를 불러 sessionStorage(DETAIL_INFO)에 넣고 202 는 표시만 한다.
    //   여기서는 같은 조건을 넘겨받아 202 가 같은 SQL 을 직접 부른다 (결과 동일).
    //   피킹 -> [210] / 리스트피킹 -> [280] (같은 조건 그대로)
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S202_PickDetail.Designer.cs 에서 관리한다.
    public sealed partial class S202_PickDetail : WorkScreen
    {
        private NavArgs _link;

        public override int ScreenNo { get { return ScreenId.PickDetail; } }
        public override string ScreenName { get { return "피킹세부내역"; } }

        public S202_PickDetail()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstLines.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            _link = args;
            lblVndmn.Text = Arg(args, "CUSTCODE");
            lblVndnm.Text = Arg(args, "CUSTNAME");
            lblCnt.Text = Arg(args, "CNT");
            lstLines.Items.Clear();

            if (lblVndmn.Text.Length == 0)
            {
                SetButtons(false);
                Msg("[201] 피킹대상에서 업체를 골라 진입하세요.", MsgLevel.Warn);
                return;
            }
            Search();
        }

        // [210]/[280] 에서 저장하고 돌아오면 남은 라인으로 다시 조회하고, [201] 에도 알린다
        private bool _saved;
        public override void OnReturn(NavArgs result)
        {
            if (!NavResult.IsSaved(result)) return;
            _saved = true;
            Search();
        }
        public override NavArgs BackResult { get { return _saved ? NavResult.Saved(ScreenNo) : null; } }

        private void Search()
        {
            string wh = Arg(_link, "TRS_WHSFL"), salgb = Arg(_link, "TRS_SALGB");
            string from = Arg(_link, "FROMDT"), to = Arg(_link, "TODT"), vnd = Arg(_link, "CUSTCODE");

            bool shop = IsShop;
            string area = Arg(_link, "AREA"), line = Arg(_link, "LINE");

            lstLines.Items.Clear();
            Run("조회중...",
                delegate
                {
                    // [204] 에서 온 경우 : PL230_W01_S01 + AREA/RUTCD (원본 204 fn_SearchDetail, TRS_SALGB 없음)
                    if (shop)
                        return OutboundService.SearchShopPickLines(OutboundService.Pgm204, "fn_SearchDetail",
                                                                   wh, null, from, to, vnd, area, line);
                    return OutboundService.SearchPickLines(OutboundService.Pgm201, "fn_SearchDetail",
                                                           wh, salgb, from, to, vnd);
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    ArrayList rows = (ArrayList)r;
                    lstLines.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(Loc.Display(o["TRS_LOCNO"]));
                            it.SubItems.Add(o["TRS_LEP"]);
                            it.SubItems.Add(PartNo.Display(o["TRS_PTNO"]));
                            it.SubItems.Add(o["UWMG_GRADE"]);
                            it.SubItems.Add(Num(o["TRS_SALQT"]));
                            it.SubItems.Add(o["TRS_VCHNO"]);
                            it.SubItems.Add(o["TRS_GRTNO"]);
                            lstLines.Items.Add(it);
                        }
                    }
                    finally { lstLines.EndUpdate(); }
                    lblCnt.Text = rows.Count.ToString();

                    if (rows.Count == 0) { Report("MP322", "피킹 대상이 없습니다.", MsgLevel.Warn); return; }
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                });
        }

        /// <summary>[204] 에서 들어왔다 (전문점 조건)</summary>
        private bool IsShop { get { return Arg(_link, "SHOP") == "Y"; } }

        // 원본 202 는 어디서 왔든 P121(210) / P147(280) 으로 간다. 전문점에서 왔으면 230 / 281 로 보낸다.
        private void OnPicking(object sender, EventArgs e) { Go(IsShop ? ScreenId.ShopPicking : ScreenId.Picking); }
        private void OnListPicking(object sender, EventArgs e) { Go(IsShop ? ScreenId.ShopListPicking : ScreenId.ListPicking); }

        private void Go(int screen)
        {
            if (Busy || _link == null || lblVndmn.Text.Length == 0) return;
            NavArgs a = new NavArgs();
            string[] keys = { "TRS_SALGB", "TRS_SALGBNM", "TRS_WHSFL", "CUSTCODE", "CUSTNAME", "CNT", "FROMDT", "TODT",
                              "SHOP", "LINE", "AREA" };
            for (int i = 0; i < keys.Length; i++) a.Set(keys[i], Arg(_link, keys[i]));
            Shell.Navigate(screen, a);
        }

        protected override void SetButtons(bool on)
        {
            btnPicking.Enabled = on;
            btnListPicking.Enabled = on;
        }
    }
}
