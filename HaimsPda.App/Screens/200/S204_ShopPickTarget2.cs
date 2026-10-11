using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [204] 피킹대상(전문점) : 일자범위 + 창고 + 노선 + 지역 -> 업체별 피킹 대상. 피킹 처리 화면으로 넘긴다.
    // 원본 웹화면 : /ui/ws/plus/PL204_W01.xml (서버 메뉴 P183, 1B11) — 전문점 전용
    //
    //   진입 : 창고 + 노선/지역 콤보(PL203_W01_S02). 노선 = NONE, 노선들 / 지역 = (공백), 지역들
    //          [203] 에서 오면 그 노선/지역으로 바로 조회한다.
    //   조회 : PL204_W01_S01 -> ds_List (TRS_VNDMN / PIC_CNT / VEN_VNDNM / TRS_SALDT / SCD_RUT_SEQ / PIC_VND_YN)
    //          [203] 에서 온 첫 조회 : LINE NONE -> RUTCD="" / ALL -> SEL_NO=Y / 그 외 RUTCD=LINE, 그리고 AREA
    //          그 다음부터          : 노선 NONE -> RUTCD="" / 그 외 RUTCD=노선, 지역이 있으면 AREA,
    //                                 노선·지역 모두 비면 SEL_NO=Y
    //   세부       -> [202] (전문점 조건 : PL230_W01_S01 + AREA/RUTCD)
    //   피킹       -> [230] 피킹실적처리(전문점)
    //   LOC피킹    -> [240] LOC별 피킹실적처리 — 여러 업체를 체크해 함께 넘길 수 있다 ('a','b')
    //   리스트피킹 -> [281] 리스트피킹(전문점)
    //   세부/피킹/리스트피킹은 1건만 (0건 MP545 / 2건 이상 MP558)
    //
    // 실측 없음(테스트 계정이 대리점). 원본 소스 기준.
    //
    // 원본과 다르게 한 부분
    //   - 원본 세부는 PL230_W01_S01 결과를 sessionStorage 로 202 에 넘긴다. 여기서는 202 가 같은 조건으로 직접 조회한다.
    //   - 체크한 행이 없으면 지금 선택된(하이라이트) 행 1건을 대상으로 본다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S204_ShopPickTarget2.Designer.cs 에서 관리한다.
    public sealed partial class S204_ShopPickTarget2 : WorkScreen
    {
        private bool _subDlg;           // 원본 m_bSubDlg : [203] 에서 넘어온 첫 조회
        private string _linkLine = "", _linkArea = "";
        private bool _searched;

        public override int ScreenNo { get { return ScreenId.PickTargetShop2; } }
        public override string ScreenName { get { return "피킹대상(전문점)"; } }

        public S204_ShopPickTarget2()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstTarget.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            lstTarget.Items.Clear();
            lblCnt.Text = "";

            _linkLine = Arg(args, "LINE");
            _linkArea = Arg(args, "AREA");
            string linkWh = Arg(args, "TRS_WHSFL");
            DateTime f, t;
            _subDlg = TryDate(Arg(args, "FROMDT"), out f) & TryDate(Arg(args, "TODT"), out t);
            dtpFrom.Value = _subDlg ? f : DateTime.Today;
            dtpTo.Value = _subDlg ? t : DateTime.Today;

            Run("조회중...",
                delegate
                {
                    return new object[] { OutboundService.GetWarehouses(OutboundService.Pgm204),
                                          OutboundService.SearchLineArea(OutboundService.Pgm204) };
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    object[] a = (object[])r;
                    ArrayList whs = (ArrayList)a[0];
                    if (whs.Count == 0) whs.Add("M");
                    FillCombo(cboWh, whs, "M");
                    if (linkWh.Length > 0) SelectByText(cboWh, linkWh);
                    LineArea.Fill(cboRut, cboArea, (ArrayList)a[1], false);
                    if (_subDlg)
                    {
                        LineArea.Select(cboRut, _linkLine);
                        LineArea.Select(cboArea, _linkArea);
                        End("", MsgLevel.Info);
                        Search();
                        return;
                    }
                    End("일자/노선/지역을 고르고 [조회] 하세요.", MsgLevel.Info);
                    btnSearch.Focus();
                });
        }

        public override void OnReturn(NavArgs result)
        {
            if (NavResult.IsSaved(result) && _searched) Search();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void OnSearch(object sender, EventArgs e) { Search(); }

        private OutboundService.LineCond Cond()
        {
            OutboundService.LineCond c = new OutboundService.LineCond();
            if (_subDlg)
            {
                if (_linkLine == "NONE") c.Rutcd = "";
                else if (_linkLine == "ALL") c.SelNo = "Y";
                else c.Rutcd = _linkLine;
                c.Area = _linkArea;
                _subDlg = false;
                return c;
            }
            string rut = ComboCode(cboRut), area = ComboCode(cboArea);
            c.Rutcd = (rut == "NONE") ? "" : rut;
            if (area.Length > 0) c.Area = area;
            if (rut.Length == 0 && area.Length == 0) c.SelNo = "Y";
            return c;
        }

        private void Search()
        {
            if (Busy) return;
            if (DayGap(dtpFrom.Value, dtpTo.Value) > 9)
            {
                Report("MP612", "조회기간은 10일을 넘을 수 없습니다.", MsgLevel.Warn);
                return;
            }
            string wh = ComboText(cboWh, "M"), from = Ymd(dtpFrom.Value), to = Ymd(dtpTo.Value);
            OutboundService.LineCond c = Cond();

            lstTarget.Items.Clear();
            lblCnt.Text = "";
            Run("조회중...",
                delegate { return OutboundService.SearchShopTargets(wh, from, to, c); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _searched = true;
                    ArrayList rows = (ArrayList)r;
                    lstTarget.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(o["TRS_VNDMN"]);
                            it.SubItems.Add(Num(o["PIC_CNT"]));
                            it.SubItems.Add(o["VEN_VNDNM"]);
                            it.SubItems.Add(o["TRS_SALDT"]);
                            it.SubItems.Add(o["SCD_RUT_SEQ"]);
                            it.SubItems.Add(o["PIC_VND_YN"] == "A" ? "타인" : "");
                            it.Tag = o;
                            lstTarget.Items.Add(it);
                        }
                    }
                    finally { lstTarget.EndUpdate(); }
                    lblCnt.Text = rows.Count.ToString();

                    if (rows.Count == 0) { Report("MP322", "피킹 대상이 없습니다.", MsgLevel.Warn); return; }
                    lstTarget.Items[0].Selected = true;
                    Report("MP102", "정상 처리되었습니다.", MsgLevel.Success);   // 원본도 MP102
                    lstTarget.Focus();
                });
        }

        /// <summary>체크된 행. 체크가 없으면 선택된 행 1건</summary>
        private ArrayList Picked()
        {
            ArrayList l = new ArrayList();
            for (int i = 0; i < lstTarget.Items.Count; i++)
                if (lstTarget.Items[i].Checked) l.Add(lstTarget.Items[i].Tag);
            if (l.Count == 0 && lstTarget.SelectedIndices.Count > 0)
                l.Add(lstTarget.Items[lstTarget.SelectedIndices[0]].Tag);
            return l;
        }

        private OutRow PickOne()
        {
            ArrayList l = Picked();
            if (l.Count == 0) { Report("MP545", "선택된 데이터가 없습니다.", MsgLevel.Warn); return null; }
            if (l.Count > 1) { Report("MP558", "한 건만 선택하십시오.", MsgLevel.Warn); return null; }
            return (OutRow)l[0];
        }

        private NavArgs BaseArgs()
        {
            NavArgs a = new NavArgs();
            a.Set("SHOP", "Y");
            a.Set("TRS_WHSFL", ComboText(cboWh, "M"));
            a.Set("TRS_SALGB", "");
            a.Set("TRS_SALGBNM", "");
            a.Set("FROMDT", Ymd(dtpFrom.Value));
            a.Set("TODT", Ymd(dtpTo.Value));
            a.Set("LINE", ComboCode(cboRut));
            a.Set("AREA", ComboCode(cboArea));
            return a;
        }

        private void GoOne(int screen)
        {
            if (Busy) return;
            OutRow o = PickOne();
            if (o == null) return;
            NavArgs a = BaseArgs();
            a.Set("CUSTCODE", o["TRS_VNDMN"]);
            a.Set("CUSTNAME", o["VEN_VNDNM"]);
            a.Set("CNT", o["PIC_CNT"]);
            Shell.Navigate(screen, a);
        }

        private void OnDetail(object sender, EventArgs e) { GoOne(ScreenId.PickDetail); }
        private void OnPicking(object sender, EventArgs e) { GoOne(ScreenId.ShopPicking); }
        private void OnListPicking(object sender, EventArgs e) { GoOne(ScreenId.ShopListPicking); }

        // 원본 OnBtnLocPick : 체크한 업체 전부를 'a','b' 로 넘긴다
        private void OnLocPicking(object sender, EventArgs e)
        {
            if (Busy) return;
            ArrayList l = Picked();
            if (l.Count == 0) { Report("MP545", "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            ArrayList codes = new ArrayList(), names = new ArrayList();
            for (int i = 0; i < l.Count; i++)
            {
                codes.Add(((OutRow)l[i])["TRS_VNDMN"]);
                names.Add(((OutRow)l[i])["VEN_VNDNM"]);
            }
            NavArgs a = BaseArgs();
            a.Set("CUSTCODE", OutboundService.InList(codes));
            a.Set("CUSTNAME", OutboundService.InList(names));
            a.Set("SELECT", l.Count.ToString());
            Shell.Navigate(ScreenId.LocPicking, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!ConfirmMsg("MP503", "화면을 지우시겠습니까?")) return;
            SelectByText(cboWh, "M");
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            if (cboRut.Items.Count > 0) cboRut.SelectedIndex = 0;
            if (cboArea.Items.Count > 0) cboArea.SelectedIndex = 0;
            lblCnt.Text = "";
            lstTarget.Items.Clear();
            _searched = false;
        }

        protected override void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnDetail.Enabled = on;
            btnPicking.Enabled = on;
            btnLocPicking.Enabled = on;
            btnListPicking.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
