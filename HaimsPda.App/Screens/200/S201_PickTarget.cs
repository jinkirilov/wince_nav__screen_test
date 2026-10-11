using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [201] 피킹대상조회 : 일자범위 + 창고 + 판매구분 -> 업체별 피킹 대상 건수.
    // 원본 웹화면 : /ui/ws/plus/PL201_W01.xml (서버 메뉴 P126)
    //
    //   진입 : gfn_SearchWHS_Plus + 판매구분(MT/07). 판매구분 기본은 두 번째 항목(계약, HAR 2026-09-20)
    //   조회 : PL201_W01_S01 -> ds_List (업체 / 건수 / 업체명 / 선점여부)
    //   업체 하나를 골라  세부 -> [202] / 피킹 -> [210] / 리스트피킹 -> [280]
    //
    // 원본과 다르게 한 부분
    //   - 원본 그리드는 체크박스로 고르고 "2건 이상이면 MP558" 로 막는다. 여기서는 행 하나 선택.
    //   - 원본은 피킹 버튼에서 PL210_W01_S02 를 한 번 부르고(결과 미사용) 넘어간다. 그 호출은 뺐다.
    //   - [210]/[280] 에서 저장이 있었으면 돌아올 때 다시 조회한다 (원본은 RETURN 링크로 항상 재조회).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S201_PickTarget.Designer.cs 에서 관리한다.
    public sealed partial class S201_PickTarget : WorkScreen
    {
        private bool _searched;

        public override int ScreenNo { get { return ScreenId.PickTarget; } }
        public override string ScreenName { get { return "피킹대상조회"; } }

        public S201_PickTarget()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstTarget.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            lstTarget.Items.Clear();
            lblTotal.Text = "0";
            LoadCombos();
        }

        public override void OnReturn(NavArgs result)
        {
            if (NavResult.IsSaved(result) && _searched) Search();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        private void LoadCombos()
        {
            Run("조회중...",
                delegate
                {
                    return new object[] { OutboundService.GetWarehouses(OutboundService.Pgm201),
                                          OutboundService.GetSalgbCodes(OutboundService.Pgm201) };
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    object[] a = (object[])r;
                    ArrayList whs = (ArrayList)a[0];
                    if (whs.Count == 0) whs.Add("M");
                    FillCombo(cboWh, whs, "M");

                    ArrayList codes = new ArrayList();
                    CodeItem all = new CodeItem();
                    all.Name = "전체";
                    codes.Add(all);
                    codes.AddRange((ArrayList)a[1]);
                    FillCombo(cboSalgb, codes, null);
                    if (cboSalgb.Items.Count > 1) cboSalgb.SelectedIndex = 1;   // 원본 setSelectedIndex(1)

                    End("일자/판매구분을 고르고 [조회] 하세요.", MsgLevel.Info);
                    btnSearch.Focus();
                });
        }

        // ------------------------------------------------------------------
        private void OnSearch(object sender, EventArgs e) { Search(); }

        private void Search()
        {
            if (Busy) return;
            if (DayGap(dtpFrom.Value, dtpTo.Value) > 9)
            {
                Report("MP612", "조회기간은 10일을 넘을 수 없습니다.", MsgLevel.Warn);
                return;
            }
            if (cboSalgb.SelectedIndex <= 0)
            {
                ReportText("판매구분을 선택하세요.", MsgLevel.Warn);
                return;
            }

            string wh = ComboText(cboWh, "M"), salgb = ComboCode(cboSalgb);
            string from = Ymd(dtpFrom.Value), to = Ymd(dtpTo.Value);

            lstTarget.Items.Clear();
            lblTotal.Text = "0";

            Run("조회중...",
                delegate { return OutboundService.SearchPickTargets(wh, salgb, from, to); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _searched = true;
                    ArrayList rows = (ArrayList)r;

                    int total = 0;
                    lstTarget.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(o["TRS_VNDMN"]);
                            it.SubItems.Add(Num(o["PIC_CNT"]));
                            it.SubItems.Add(o["VEN_VNDNM"]);
                            it.SubItems.Add(o["PIC_VND_YN"] == "A" ? "타인" : "");
                            it.Tag = o;
                            lstTarget.Items.Add(it);
                            total += ToInt(o["PIC_CNT"]);
                        }
                    }
                    finally { lstTarget.EndUpdate(); }
                    lblTotal.Text = total.ToString();

                    if (rows.Count == 0)
                    {
                        Report("MP322", "피킹 대상이 없습니다.", MsgLevel.Warn);
                        return;
                    }
                    lstTarget.Items[0].Selected = true;
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                    lstTarget.Focus();
                });
        }

        private OutRow Selected
        {
            get
            {
                if (lstTarget.SelectedIndices.Count == 0) return null;
                return lstTarget.Items[lstTarget.SelectedIndices[0]].Tag as OutRow;
            }
        }

        // ------------------------------------------------------------------
        // 버튼 (원본 fn_BottomButton / ds_LinkInfo 1B06)
        // ------------------------------------------------------------------
        private void OnDetail(object sender, EventArgs e) { Go(ScreenId.PickDetail); }
        private void OnPicking(object sender, EventArgs e) { Go(ScreenId.Picking); }
        private void OnListPicking(object sender, EventArgs e) { Go(ScreenId.ListPicking); }

        private void Go(int screen)
        {
            if (Busy) return;
            OutRow o = Selected;
            if (o == null) { Report("MP545", "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            Shell.Navigate(screen, LinkArgs(o));
        }

        private NavArgs LinkArgs(OutRow o)
        {
            NavArgs a = new NavArgs();
            a.Set("TRS_SALGB", ComboCode(cboSalgb));
            a.Set("TRS_SALGBNM", ComboText(cboSalgb, ""));
            a.Set("TRS_WHSFL", ComboText(cboWh, "M"));
            a.Set("CUSTCODE", o["TRS_VNDMN"]);
            a.Set("CUSTNAME", o["VEN_VNDNM"]);
            a.Set("CNT", o["PIC_CNT"]);
            a.Set("FROMDT", Ymd(dtpFrom.Value));
            a.Set("TODT", Ymd(dtpTo.Value));
            return a;
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!Confirm("화면을 CLEAR 하시겠습니까?")) return;
            lstTarget.Items.Clear();
            lblTotal.Text = "0";
            SelectByText(cboWh, "M");
            if (cboSalgb.Items.Count > 1) cboSalgb.SelectedIndex = 1;
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            _searched = false;
            Msg("초기화", MsgLevel.Info);
        }

        protected override void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnDetail.Enabled = on;
            btnPicking.Enabled = on;
            btnListPicking.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
