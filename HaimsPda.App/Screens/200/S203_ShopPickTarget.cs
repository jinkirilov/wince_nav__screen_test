using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [203] 피킹대상조회(전문점) : 일자범위 + 창고 + 노선 + 지역 -> 노선·지역별 피킹 대상 건수.
    // 원본 웹화면 : /ui/ws/plus/PL203_W01.xml (서버 메뉴 P182, 1B10) — 전문점 전용
    //
    //   진입 : 창고 콤보 + 노선/지역 콤보 (PL203_W01_S02, GUBUN R=노선 / A=지역)
    //          노선 콤보 = ALL, NONE, 노선들 / 지역 콤보 = (공백), 지역들
    //   조회 : PL203_W01_S01 -> ds_List (SCD_RUTCD / LOC_PIC_AREA / PIC_CNT)
    //          SEL_NO 규칙 (원본 그대로)
    //            노선 ALL + 지역 공백         -> SEL_NO=Y
    //            노선 선택 + 지역 선택        -> SEL_NO=T, RUTCD, AREA
    //            노선 선택 + 지역 공백        -> SEL_NO=R, RUTCD, AREA
    //            노선 NONE                    -> SEL_NO=R, RUTCD="  "(공백 두 칸), AREA
    //            그 외(노선 ALL + 지역 선택)  -> SEL_NO=A, RUTCD="", AREA
    //   세부 : 고른 행의 노선/지역을 들고 [204]
    //
    // 실측 없음(테스트 계정이 대리점). 원본 소스 기준.
    //
    // 원본과 다르게 한 부분
    //   - 원본은 세부에서 PL204_W01_S01 을 한 번 불러 결과를 확인만 하고 [204] 가 같은 조회를 다시 한다.
    //     앞의 호출은 뺐다 (결과 없으면 [204] 가 MP322 를 띄운다).
    //   - 원본 그리드는 체크박스로 1건만 고르게 한다. 여기서는 행 하나 선택.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S203_ShopPickTarget.Designer.cs 에서 관리한다.
    public sealed partial class S203_ShopPickTarget : WorkScreen
    {
        private ArrayList _lineArea = new ArrayList();
        private bool _searched;

        public override int ScreenNo { get { return ScreenId.PickTargetShop; } }
        public override string ScreenName { get { return "피킹대상조회(전문점)"; } }

        public S203_ShopPickTarget()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstLines.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            lstLines.Items.Clear();
            lblCnt.Text = "";
            lblRutNm.Text = "";

            Run("조회중...",
                delegate
                {
                    return new object[] { OutboundService.GetWarehouses(OutboundService.Pgm203),
                                          OutboundService.SearchLineArea(OutboundService.Pgm203) };
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    object[] a = (object[])r;
                    ArrayList whs = (ArrayList)a[0];
                    if (whs.Count == 0) whs.Add("M");
                    FillCombo(cboWh, whs, "M");
                    _lineArea = (ArrayList)a[1];
                    LineArea.Fill(cboRut, cboArea, _lineArea, true);
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

        private void OnRutChanged(object sender, EventArgs e)
        {
            lblRutNm.Text = LineArea.RouteName(_lineArea, ComboCode(cboRut));
        }

        // ------------------------------------------------------------------
        private void OnSearch(object sender, EventArgs e) { Search(); }

        /// <summary>원본 fn_Search 의 SEL_NO 분기</summary>
        private OutboundService.LineCond Cond()
        {
            OutboundService.LineCond c = new OutboundService.LineCond();
            string rut = ComboCode(cboRut), area = ComboCode(cboArea);
            int ri = cboRut.SelectedIndex, ai = cboArea.SelectedIndex;

            if (rut == "ALL" && area.Length == 0) c.SelNo = "Y";
            else if (ri > 1 && ai > 0) { c.SelNo = "T"; c.Rutcd = rut; c.Area = area; }
            else if (ri > 1 && area.Length == 0) { c.SelNo = "R"; c.Rutcd = rut; c.Area = area; }
            else if (rut == "NONE") { c.SelNo = "R"; c.Rutcd = "  "; c.Area = area; }
            else { c.SelNo = "A"; c.Rutcd = ""; c.Area = area; }
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

            lstLines.Items.Clear();
            lblCnt.Text = "";
            Run("조회중...",
                delegate { return OutboundService.SearchShopLines(wh, from, to, c); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _searched = true;
                    ArrayList rows = (ArrayList)r;
                    lstLines.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(o["SCD_RUTCD"]);
                            it.SubItems.Add(o["LOC_PIC_AREA"]);
                            it.SubItems.Add(Num(o["PIC_CNT"]));
                            it.Tag = o;
                            lstLines.Items.Add(it);
                        }
                    }
                    finally { lstLines.EndUpdate(); }
                    lblCnt.Text = rows.Count.ToString();

                    if (rows.Count == 0) { Report("MP322", "피킹 대상이 없습니다.", MsgLevel.Warn); return; }
                    lstLines.Items[0].Selected = true;
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                    lstLines.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 세부 -> [204] (원본 fn_BtnDetail, ds_LinkInfo 1B10)
        // ------------------------------------------------------------------
        private void OnDetail(object sender, EventArgs e)
        {
            if (Busy) return;
            if (lstLines.SelectedIndices.Count == 0) { Report("MP545", "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            OutRow o = (OutRow)lstLines.Items[lstLines.SelectedIndices[0]].Tag;

            NavArgs a = new NavArgs();
            a.Set("FROMDT", Ymd(dtpFrom.Value));
            a.Set("TODT", Ymd(dtpTo.Value));
            a.Set("LINE", o["SCD_RUTCD"]);
            a.Set("AREA", o["LOC_PIC_AREA"]);
            a.Set("TRS_WHSFL", ComboText(cboWh, "M"));
            Shell.Navigate(ScreenId.PickTargetShop2, a);
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
            lblRutNm.Text = "";
            lblCnt.Text = "";
            lstLines.Items.Clear();
            _searched = false;
        }

        protected override void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnDetail.Enabled = on;
            btnClear.Enabled = on;
        }
    }

    /// <summary>[203]/[204] 노선·지역 콤보 공용 (원본 fn_SearchLineArea_After)</summary>
    internal static class LineArea
    {
        /// <summary>
        /// withAll=true  (203) : 노선 = ALL, NONE, 노선들
        /// withAll=false (204) : 노선 = NONE, 노선들
        /// 지역 = (공백), 지역들
        /// </summary>
        public static void Fill(ComboBox rut, ComboBox area, ArrayList rows, bool withAll)
        {
            rut.Items.Clear();
            area.Items.Clear();
            if (withAll) rut.Items.Add(Item("ALL", "ALL"));
            rut.Items.Add(Item("NONE", "NONE"));
            area.Items.Add(Item("", " "));
            for (int i = 0; i < rows.Count; i++)
            {
                OutRow o = (OutRow)rows[i];
                string v = o["RUT_RUTCD"];
                if (v.Length == 0) continue;
                if (o["GUBUN"] == "A") area.Items.Add(Item(v, v));
                else if (o["GUBUN"] == "R") rut.Items.Add(Item(v, v));
            }
            rut.SelectedIndex = 0;
            area.SelectedIndex = 0;
        }

        public static void Select(ComboBox cbo, string code)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                CodeItem c = cbo.Items[i] as CodeItem;
                if (c != null && c.Code == code) { cbo.SelectedIndex = i; return; }
            }
        }

        public static string RouteName(ArrayList rows, string code)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                OutRow o = (OutRow)rows[i];
                if (o["GUBUN"] == "R" && o["RUT_RUTCD"] == code) return o["RUT_RUTNM"];
            }
            return "";
        }

        private static CodeItem Item(string code, string name)
        {
            CodeItem c = new CodeItem();
            c.Code = code;
            c.Name = name;
            return c;
        }
    }
}
