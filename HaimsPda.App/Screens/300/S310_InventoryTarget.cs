using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [310] 재물조사대상조회 : 창고/생성일자/구분/완료구분으로 재물조사 목록을 조회하고 한 건을 골라 [302] 로 간다.
    // 원본 웹화면 : /ui/ws/plus/PL310_W01.xml (서버 메뉴 P131)
    //
    //   fn_Search : 특별/등급 -> PL310_W01_S21, 그 외 -> PL310_W01_S11 -> ds_LocInfo
    //   OnBtnStock : 건수 0 이면 MP627, 아니면 DATE/SALENO/WHSCD/SELECT/ISC_ZONE 을 들고 [302]
    //
    // 원본과 다르게 한 부분
    //   - 생성일자는 DatePicker 팝업 대신 날짜 선택 컨트롤 + ◀▶(하루씩) 버튼.
    //   - 목록 행을 Enter/더블탭해도 [302] 로 간다 (원본은 행 선택 후 버튼만).
    //   - [302] 에서 저장하고 돌아오면 같은 조건으로 다시 조회하고 보던 행을 다시 고른다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S310_InventoryTarget.Designer.cs 에서 관리한다.
    public sealed partial class S310_InventoryTarget : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_NODATA = "MP567";  // 조회 대상 정보가 없습니다
        private const string MP_ZERO = "MP627";    // 재물조사 대상 건수가 없습니다
        private const string MP_NOSEL = "MP545";   // 선택된 데이터가 없습니다
        private const string MP_CLEAR = "MP503";   // 지우시겠습니까

        private bool _busy;
        private bool _searched;
        private string _reselSeq;      // 재조회 후 다시 고를 행 (OnReturn)

        public override int ScreenNo { get { return ScreenId.InventoryTarget; } }
        public override string ScreenName { get { return "재물조사대상"; } }

        public S310_InventoryTarget()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            cboGubun.Items.Clear();
            for (int i = 0; i < InvGubun.Names.Length; i++) cboGubun.Items.Add(InvGubun.Names[i]);
            cboStat.Items.Clear();
            for (int i = 0; i < InvStat.Names.Length; i++) cboStat.Items.Add(InvStat.Names[i]);

            WinApi.GridLines(this.lstTarget.Handle, true);
            WinApi.DoubleBuffering(this.lstTarget.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadWarehouses();
        }

        // [302] 에서 저장하고 돌아오면 같은 조건으로 다시 조회한다 (건수/완료 상태가 바뀐다)
        public override void OnReturn(NavArgs result)
        {
            if (!NavResult.IsSaved(result) || !_searched) return;
            InvTargetRow t = Selected;
            _reselSeq = (t == null) ? null : t.Seq;
            Search();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 창고 콤보 (gfn_SearchWHS_Plus)
        // ------------------------------------------------------------------
        private void LoadWarehouses()
        {
            Begin("창고 조회중...");
            Async.Run(this,
                delegate { return LocStockService.GetWarehouses(); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    cboWh.Items.Clear();
                    for (int i = 0; i < list.Count; i++) cboWh.Items.Add(list[i]);
                    if (cboWh.Items.Count == 0) cboWh.Items.Add("M");
                    cboWh.SelectedIndex = 0;
                    SelectByText(cboWh, "M");

                    End("조건을 고르고 [조회] 하세요.", MsgLevel.Info);
                    btnSearch.Focus();
                });
        }

        private static void SelectByText(ComboBox cbo, string text)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
                if (cbo.Items[i].ToString() == text) { cbo.SelectedIndex = i; return; }
        }

        private string CurWh
        {
            get { return (cboWh.SelectedIndex < 0) ? "M" : cboWh.Items[cboWh.SelectedIndex].ToString(); }
        }

        private string CurGubun
        {
            get { int i = cboGubun.SelectedIndex; return (i < 0) ? "" : InvGubun.Codes[i]; }
        }

        private string CurStat
        {
            get { int i = cboStat.SelectedIndex; return (i < 0) ? "" : InvStat.Codes[i]; }
        }

        // ------------------------------------------------------------------
        // 조회 (fn_Search)
        // ------------------------------------------------------------------
        private void OnSearch(object sender, EventArgs e) { Search(); }

        private void OnPrevDay(object sender, EventArgs e) { if (!_busy) dtpDate.Value = dtpDate.Value.AddDays(-1); }
        private void OnNextDay(object sender, EventArgs e) { if (!_busy) dtpDate.Value = dtpDate.Value.AddDays(1); }

        private void Search()
        {
            if (_busy) return;

            string wh = CurWh, gubun = CurGubun, stat = CurStat;
            DateTime d = dtpDate.Value.Date;

            lstTarget.Items.Clear();
            lblCnt.Text = "";

            Begin("조회중...");
            Async.Run(this,
                delegate { return InventoryService.SearchTargets(wh, d, gubun, stat); },
                delegate(object r, Exception ex)
                {
                    string resel = _reselSeq;
                    _reselSeq = null;

                    if (Fail(ex)) return;

                    _searched = true;
                    ArrayList rows = (ArrayList)r;
                    if (rows.Count == 0)
                    {
                        Report(MP_NODATA, "조회 대상 정보가 없습니다.", MsgLevel.Warn);
                        return;
                    }

                    FillGrid(rows);
                    lblCnt.Text = rows.Count.ToString();

                    int sel = 0;
                    if (resel != null)
                    {
                        for (int i = 0; i < lstTarget.Items.Count; i++)
                        {
                            InvTargetRow t = lstTarget.Items[i].Tag as InvTargetRow;
                            if (t != null && t.Seq == resel) { sel = i; break; }
                        }
                    }
                    lstTarget.Items[sel].Selected = true;
                    lstTarget.EnsureVisible(sel);

                    Report(MP_OK, "정상 조회되었습니다.", MsgLevel.Success);
                    lstTarget.Focus();
                });
        }

        private void FillGrid(ArrayList rows)
        {
            lstTarget.BeginUpdate();
            try
            {
                lstTarget.Items.Clear();
                for (int i = 0; i < rows.Count; i++)
                {
                    InvTargetRow t = (InvTargetRow)rows[i];
                    ListViewItem it = new ListViewItem(t.Seq);
                    it.SubItems.Add(t.Cnt);
                    it.SubItems.Add(InvStat.Name(t.Stat));
                    it.SubItems.Add(InvGubun.Name(t.Gubun));
                    it.SubItems.Add(t.Zone);
                    it.Tag = t;
                    lstTarget.Items.Add(it);
                }
            }
            finally { lstTarget.EndUpdate(); }
        }

        private InvTargetRow Selected
        {
            get
            {
                if (lstTarget.SelectedIndices.Count == 0) return null;
                return lstTarget.Items[lstTarget.SelectedIndices[0]].Tag as InvTargetRow;
            }
        }

        // ------------------------------------------------------------------
        // [302] 재물조사로 (OnBtnStock)
        // ------------------------------------------------------------------
        private void OnInventory(object sender, EventArgs e) { GoInventory(); }
        private void OnRowActivate(object sender, EventArgs e) { GoInventory(); }

        private void OnListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            GoInventory();
        }

        private void GoInventory()
        {
            if (_busy) return;

            InvTargetRow t = Selected;
            if (t == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            if (t.Cnt.Trim() == "0")
            {
                Report(MP_ZERO, "재물조사 대상 건수가 없습니다.", MsgLevel.Warn);
                return;
            }

            NavArgs a = new NavArgs();
            a.Set("DATE", dtpDate.Value.ToString("yyyyMMdd"));
            a.Set("SALENO", t.Seq);
            a.Set("WHSCD", CurWh);
            a.Set("SELECT", t.Gubun);          // 원본은 표시명(수시/정기..)을 넘긴다. 여기서는 코드
            a.Set("DETAIL_INFO", CurStat);
            a.Set("ISC_ZONE", t.Zone);
            Shell.Navigate(ScreenId.Inventory, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "화면을 지우시겠습니까?"))) return;
            ClearAll();
            SelectByText(cboWh, "M");
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[310] " + ScreenName,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        private void Begin(string msg)
        {
            _busy = true;
            Cursor.Current = Cursors.WaitCursor;
            SetButtons(false);
            Msg(msg, MsgLevel.Info);
        }

        private void End(string msg, MsgLevel lv)
        {
            _busy = false;
            Cursor.Current = Cursors.Default;
            SetButtons(true);
            Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[310] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[310] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnPrev.Enabled = on;
            btnNext.Enabled = on;
            btnInventory.Enabled = on;
            btnClear.Enabled = on;
        }

        /// <summary>원본 fn_Init : 구분/완료구분 공백, 생성일자 오늘, 창고 M</summary>
        private void ClearAll()
        {
            _searched = false;
            _reselSeq = null;
            dtpDate.Value = DateTime.Today;
            if (cboGubun.Items.Count > 0) cboGubun.SelectedIndex = 0;
            if (cboStat.Items.Count > 0) cboStat.SelectedIndex = 0;
            lblCnt.Text = "";
            lstTarget.Items.Clear();
        }
    }
}
