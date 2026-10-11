using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [221] 판매증표선택 : 출력일자 + 창고로 업체/정비업체를 고르고 판매증표를 골라 [220] 으로 출고한다.
    // 원본 웹화면 : /ui/ws/plus/PL221_W01.xml (서버 메뉴 P184)
    //
    //   업체 콤보 : PL221_W01_S02 -> ds_List    (TRS_VNDMN / VEN_VNDNM)
    //   정비 콤보 : PL221_W01_S03 -> ds_List_WM (TRF_VNDCD_WM / VEN_VNDNM)
    //   조회      : PL221_W01_S01 -> ds_List    (TRS_VCHNO / TRS_CNT / VEN_VNDNM) — 전부 체크된 채로 나온다
    //   출고      : 체크한 증표들 -> [220]
    //   [220] 의 "멀티" 로 들어온 경우(MULTI=Y)는 [220] 으로 되돌아가며 증표 목록을 넘긴다 (원본 LINK2)
    //
    // 원본과 다르게 한 부분
    //   - 업체 목록이 비었을 때 원본은 MP322 창을 띄운다. 여기서는 푸터에만 알린다(진입마다 뜨는 것 방지).
    //   - 전체 선택/해제 [ALL] 버튼을 더했다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S221_SlipSelect.Designer.cs 에서 관리한다.
    public sealed partial class S221_SlipSelect : WorkScreen
    {
        private bool _multi;
        private bool _loading;
        private bool _searched;

        public override int ScreenNo { get { return ScreenId.SlipSelect; } }
        public override string ScreenName { get { return "판매증표선택"; } }

        public S221_SlipSelect()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstSlips.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            _multi = Arg(args, "MULTI") == "Y";
            _loading = true;
            dtpDate.Value = DateTime.Today;
            _loading = false;
            lstSlips.Items.Clear();

            Run("조회중...",
                delegate { return OutboundService.GetWarehouses(OutboundService.Pgm221); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    ArrayList whs = (ArrayList)r;
                    if (whs.Count == 0) whs.Add("M");
                    _loading = true;
                    FillCombo(cboWh, whs, "M");
                    _loading = false;
                    End("", MsgLevel.Info);
                    LoadVendors();
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

        private void OnCondChanged(object sender, EventArgs e)
        {
            if (_loading || Busy || cboWh.Items.Count == 0) return;
            LoadVendors();
        }

        /// <summary>원본 fn_Search_VenList + fn_Search_VenList2</summary>
        private void LoadVendors()
        {
            string wh = ComboText(cboWh, "M"), dt = Ymd(dtpDate.Value);
            lstSlips.Items.Clear();
            Run("업체 조회중...",
                delegate
                {
                    return new object[] { OutboundService.SearchSlipVendors(wh, dt, false),
                                          OutboundService.SearchSlipVendors(wh, dt, true) };
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    object[] a = (object[])r;
                    FillVendor(cboCust, (ArrayList)a[0], "TRS_VNDMN");
                    FillVendor(cboCust2, (ArrayList)a[1], "TRF_VNDCD_WM");
                    if (cboCust.Items.Count <= 1 && cboCust2.Items.Count <= 1)
                        End(CommonCache.Msg("MP322", "조회 대상이 없습니다."), MsgLevel.Warn);
                    else
                        End("업체를 고르고 [조회] 하세요.", MsgLevel.Info);
                });
        }

        private static void FillVendor(ComboBox cbo, ArrayList rows, string codeCol)
        {
            cbo.Items.Clear();
            CodeItem blank = new CodeItem();
            blank.Name = " ";
            cbo.Items.Add(blank);
            for (int i = 0; i < rows.Count; i++)
            {
                OutRow o = (OutRow)rows[i];
                if (o[codeCol].Length == 0) continue;
                CodeItem c = new CodeItem();
                c.Code = o[codeCol];
                c.Name = c.Code + " " + o["VEN_VNDNM"];
                cbo.Items.Add(c);
            }
            cbo.SelectedIndex = 0;
        }

        // ------------------------------------------------------------------
        private void OnSearch(object sender, EventArgs e) { Search(); }

        private void Search()
        {
            if (Busy) return;
            string c1 = ComboCode(cboCust).Trim(), c2 = ComboCode(cboCust2).Trim();
            if (c1.Length == 0 && c2.Length == 0)
            {
                Report("MP305", "업체를 선택하십시오.", MsgLevel.Warn);
                return;
            }
            string wh = ComboText(cboWh, "M"), dt = Ymd(dtpDate.Value);
            lstSlips.Items.Clear();
            Run("조회중...",
                delegate { return OutboundService.SearchSlips(wh, dt, c1, c2); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _searched = true;
                    ArrayList rows = (ArrayList)r;
                    lstSlips.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(o["TRS_VCHNO"]);
                            it.SubItems.Add(Num(o["TRS_CNT"]));
                            it.SubItems.Add(o["VEN_VNDNM"]);
                            it.Checked = true;      // 원본 : column5 를 전부 1 로
                            lstSlips.Items.Add(it);
                        }
                    }
                    finally { lstSlips.EndUpdate(); }

                    if (rows.Count == 0) { Report("MP322", "조회 대상이 없습니다.", MsgLevel.Warn); return; }
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                });
        }

        // ------------------------------------------------------------------
        // 출고 (OnBtnOutput)
        // ------------------------------------------------------------------
        private void OnOutput(object sender, EventArgs e)
        {
            if (Busy) return;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < lstSlips.Items.Count; i++)
            {
                if (!lstSlips.Items[i].Checked) continue;
                if (sb.Length > 0) sb.Append(",");
                sb.Append(lstSlips.Items[i].Text);
            }
            if (sb.Length == 0) { Report("MP545", "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            NavArgs a = new NavArgs();
            a.Set("VCHNOS", sb.ToString());
            if (_multi) Shell.GoBack(a);
            else Shell.Navigate(ScreenId.SlipPicking, a);
        }

        private void OnAll(object sender, EventArgs e)
        {
            bool any = false;
            for (int i = 0; i < lstSlips.Items.Count; i++) if (!lstSlips.Items[i].Checked) { any = true; break; }
            for (int i = 0; i < lstSlips.Items.Count; i++) lstSlips.Items[i].Checked = any;
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!ConfirmMsg("MP503", "화면을 지우시겠습니까?")) return;
            lstSlips.Items.Clear();
            _loading = true;
            SelectByText(cboWh, "M");
            dtpDate.Value = DateTime.Today;
            _loading = false;
            cboCust.Items.Clear();
            cboCust2.Items.Clear();
            _searched = false;
            LoadVendors();
        }

        protected override void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnOutput.Enabled = on;
            btnAll.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
