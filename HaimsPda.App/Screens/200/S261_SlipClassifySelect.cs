using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [261] 피킹분류(증표) : 일자범위 + 창고 + 업체로 미분류 증표를 골라 [260] 배송분류로 넘긴다.
    // 원본 웹화면 : /ui/ws/plus/PL261_W01.xml (서버 메뉴 P186, XML title 은 "판매증표선택")
    //
    //   업체 콤보 : PL260_W01_S01 -> ds_List (TRS_VNDMN / VEN_VNDNM)  — 일자/창고가 바뀌면 다시 받는다
    //   조회      : PL260_W01_S02 -> ds_List (TRS_VCHNO / TRS_CNT / TRS_SALGB)
    //   확인      : 체크한 증표 'yyyyMMsssss',... + 업체/일자/건수 -> [260]
    //   기간은 10일 이내 (MP612)
    //
    // 원본과 다르게 한 부분
    //   - 원본은 [ALL] 을 누를 때만 선택 건수를 다시 센다. 여기서는 체크할 때마다 센다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S261_SlipClassifySelect.Designer.cs 에서 관리한다.
    public sealed partial class S261_SlipClassifySelect : WorkScreen
    {
        private bool _loading;
        private bool _searched;
        private bool _allOn;

        public override int ScreenNo { get { return ScreenId.SlipClassifySelect; } }
        public override string ScreenName { get { return "피킹분류(증표)"; } }

        public S261_SlipClassifySelect()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstSlips.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            _loading = true;
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            _loading = false;
            lstSlips.Items.Clear();
            lblCnt.Text = "0";

            Run("조회중...",
                delegate { return OutboundService.GetWarehouses(OutboundService.Pgm261); },
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

        private bool RangeOk()
        {
            if (DayGap(dtpFrom.Value, dtpTo.Value) <= 9) return true;
            Report("MP612", "조회기간은 10일을 넘을 수 없습니다.", MsgLevel.Warn);
            return false;
        }

        private void LoadVendors()
        {
            if (!RangeOk()) return;
            string wh = ComboText(cboWh, "M"), from = Ymd(dtpFrom.Value), to = Ymd(dtpTo.Value);
            lstSlips.Items.Clear();
            lblCnt.Text = "0";
            Run("업체 조회중...",
                delegate { return OutboundService.SearchClassifyVendors(wh, from, to); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    ArrayList rows = (ArrayList)r;
                    cboCust.Items.Clear();
                    for (int i = 0; i < rows.Count; i++)
                    {
                        OutRow o = (OutRow)rows[i];
                        if (o["TRS_VNDMN"].Length == 0) continue;
                        CodeItem c = new CodeItem();
                        c.Code = o["TRS_VNDMN"];
                        c.Name = c.Code + "-" + o["VEN_VNDNM"];
                        cboCust.Items.Add(c);
                    }
                    if (cboCust.Items.Count > 0) cboCust.SelectedIndex = 0;

                    if (cboCust.Items.Count == 0) Report("MP601", "분류 대상 업체가 없습니다.", MsgLevel.Warn);
                    else End("업체를 고르고 [조회] 하세요.", MsgLevel.Info);
                });
        }

        // ------------------------------------------------------------------
        private void OnSearch(object sender, EventArgs e) { Search(); }

        private void Search()
        {
            if (Busy || !RangeOk()) return;
            string vnd = ComboCode(cboCust);
            if (vnd.Length == 0) { Report("MP305", "업체를 선택하십시오.", MsgLevel.Warn); return; }
            string wh = ComboText(cboWh, "M"), from = Ymd(dtpFrom.Value), to = Ymd(dtpTo.Value);

            lstSlips.Items.Clear();
            lblCnt.Text = "0";
            _allOn = false;
            Run("조회중...",
                delegate { return OutboundService.SearchClassifySlips(wh, from, to, vnd); },
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
                            it.SubItems.Add(S210_Picking.SalgbName(o["TRS_SALGB"], ""));
                            it.Tag = o;
                            lstSlips.Items.Add(it);
                        }
                    }
                    finally { lstSlips.EndUpdate(); }

                    if (rows.Count == 0) { Report("MP578", "분류 대상이 없습니다.", MsgLevel.Warn); return; }
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                });
        }

        private void OnItemCheck(object sender, ItemCheckEventArgs e)
        {
            CountChecked(e.Index, e.NewValue == CheckState.Checked);
        }

        /// <summary>원본 fn_CheckList : 체크한 증표의 TRS_CNT 합</summary>
        private void CountChecked(int changing, bool newState)
        {
            int sum = 0;
            for (int i = 0; i < lstSlips.Items.Count; i++)
            {
                bool on = (i == changing) ? newState : lstSlips.Items[i].Checked;
                if (on) sum += ToInt(lstSlips.Items[i].SubItems[1].Text);
            }
            lblCnt.Text = sum.ToString();
        }

        private void OnAll(object sender, EventArgs e)
        {
            _allOn = !_allOn;
            for (int i = 0; i < lstSlips.Items.Count; i++) lstSlips.Items[i].Checked = _allOn;
            CountChecked(-1, false);
        }

        // ------------------------------------------------------------------
        // 확인 -> [260] (OnBtnConfirm, ds_LinkInfo 1B14)
        // ------------------------------------------------------------------
        private void OnConfirm(object sender, EventArgs e)
        {
            if (Busy) return;
            if (ComboCode(cboCust).Length == 0) { Report("MP305", "업체를 선택하십시오.", MsgLevel.Warn); return; }

            ArrayList keys = new ArrayList();
            OutRow first = null;
            for (int i = 0; i < lstSlips.Items.Count; i++)
            {
                if (!lstSlips.Items[i].Checked) continue;
                OutRow o = (OutRow)lstSlips.Items[i].Tag;
                if (first == null) first = o;
                keys.Add(OutboundService.VchKey(o["TRS_VCHNO"]));
            }
            if (keys.Count == 0) { Report("MP545", "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            CountChecked(-1, false);

            NavArgs a = new NavArgs();
            a.Set("CUSTCODE", ComboCode(cboCust));
            a.Set("CUSTNAME", ComboText(cboCust, ""));
            a.Set("FROMDT", Ymd(dtpFrom.Value));
            a.Set("TODT", Ymd(dtpTo.Value));
            a.Set("CNT", lblCnt.Text);
            a.Set("TRS_SALGB", first["TRS_SALGB"]);
            a.Set("WHSCD", ComboText(cboWh, "M"));
            a.Set("DETAIL_INFO", OutboundService.InList(keys));
            Shell.Navigate(ScreenId.SlipClassify, a);
        }

        protected override void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnConfirm.Enabled = on;
            btnAll.Enabled = on;
        }
    }
}
