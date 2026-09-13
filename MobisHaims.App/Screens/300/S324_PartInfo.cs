using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [324] 부품정보조회 : 계열/차종/창고 조건 + 부번으로 목록 조회, 이전/다음 페이징
    // 원본 웹화면 : /ui/ws/plus/PL324_W01.xml
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S324_PartInfo.Designer.cs 에서 관리한다.
    //
    // [주의] 대문자 변환은 KeyPress 에서 하지 않는다.
    //        CF 는 KeyPressEventArgs.KeyChar 대입을 무시한다.
    //        조회 시점에 PartNo.Display / PartNo.Key 가 ToUpper 한다.
    public sealed partial class S324_PartInfo : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_NOSTOCK = "MP333";  // 해당부품에 대한 재고정보가 없습니다
        private const string MP_NOSEL = "MP545";    // 선택된 데이터가 없습니다

        private bool _busy;
        private bool _loading;   // 콤보 초기 세팅 중에는 조회하지 않는다

        public override int ScreenNo { get { return ScreenId.PartInfo; } }
        public override string ScreenName { get { return "부품정보조회"; } }

        public S324_PartInfo()
        {
            InitializeComponent();
            if (IsDesignMode) 
                return;
            WinApi.GridLines(this.lstPart.Handle, true);
            WinApi.DoubleBuffering(this.lstPart.Handle, true);
            //for (int c = 0; c < lstPart.Columns.Count; c++)
            //    lstPart.Columns[1].Width = -2;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadCombos(args);
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
        }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            txtPart.Text = PartNo.Display(data.Text);
            Search(PartInfoService.Dir.First, PartNo.Key(txtPart.Text));
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            txtPart.Text = PartNo.Display(txtPart.Text);
            Search(PartInfoService.Dir.First, PartNo.Key(txtPart.Text));
        }

        private void OnLepChanged(object sender, EventArgs e) { Requery(); }
        private void OnCarChanged(object sender, EventArgs e) { Requery(); }
        private void OnWhChanged(object sender, EventArgs e) { Requery(); }

        private void Requery()
        {
            if (_loading || _busy) 
                return;
            if (txtPart.Text.Trim().Length == 0) 
                return;
            Search(PartInfoService.Dir.First, PartNo.Key(txtPart.Text));
        }

        // ------------------------------------------------------------------
        // 콤보 3개 (계열 / 창고 / 차종)
        // ------------------------------------------------------------------
        private void LoadCombos(NavArgs args)
        {
            _loading = true;

            string linkLep = (args == null) ? null : args.GetString("LEP");
            string linkWh = (args == null) ? null : args.GetString("WHSCD");
            string linkPtno = (args == null) ? null : args.GetString("PTNO");

            Begin("코드 조회중...");
            Async.Run(this,
                delegate
                {
                    ArrayList[] all = new ArrayList[3];
                    all[0] = PartInfoService.GetLeps();
                    all[1] = PartInfoService.GetWarehouses();
                    all[2] = PartInfoService.GetCarCodes();
                    return all;
                },
                delegate(object r, Exception ex)
                {
                    _loading = true;
                    try
                    {
                        if (Fail(ex)) return;

                        ArrayList[] all = (ArrayList[])r;

                        FillCombo(cboLep, all[0]);
                        FillCombo(cboWh, all[1]);

                        // 차종은 "전체"(빈 값)를 맨 앞에 둔다 (원본 selCarcode.addItem("","",1))
                        cboCar.Items.Clear();
                        CodeItem blank = new CodeItem();
                        cboCar.Items.Add(blank);
                        for (int i = 0; i < all[2].Count; i++) 
                            cboCar.Items.Add(all[2][i]);
                        cboCar.SelectedIndex = 0;

                        // 기본값 : 창고 M, 계열은 사용자 HK (C 면 H)
                        SelectByText(cboWh, "M");
                        SelectByText(cboLep, DefaultLep());

                        if (linkWh != null) 
                            SelectByText(cboWh, linkWh);
                        if (linkLep != null) 
                            SelectByText(cboLep, linkLep);
                    }
                    finally { _loading = false; }

                    End("조회 조건을 선택하세요.", MsgLevel.Info);

                    if (linkPtno != null && linkPtno.Length > 0)
                    {
                        txtPart.Text = PartNo.Display(linkPtno);
                        Search(PartInfoService.Dir.First, PartNo.Key(linkPtno));
                    }
                    else txtPart.Focus();
                });
        }

        /// <summary>원본 FV_HK_VALUE : USR_HK 가 C 면 H, 아니면 그대로</summary>
        private static string DefaultLep()
        {
            UserInfo u = Session.User;
            string hk = (u == null) ? "" : u["USR_HK"];
            return (hk == "C") ? "H" : hk;
        }

        private static void FillCombo(ComboBox cbo, ArrayList items)
        {
            cbo.Items.Clear();
            for (int i = 0; i < items.Count; i++) cbo.Items.Add(items[i]);
            if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
        }

        private static void SelectByText(ComboBox cbo, string text)
        {
            if (text == null || text.Length == 0) return;
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (cbo.Items[i].ToString() == text) { cbo.SelectedIndex = i; return; }
            }
        }

        private string CurLep
        {
            get { return (cboLep.SelectedIndex < 0) ? "" : cboLep.Items[cboLep.SelectedIndex].ToString(); }
        }

        private string CurWh
        {
            get { return (cboWh.SelectedIndex < 0) ? "M" : cboWh.Items[cboWh.SelectedIndex].ToString(); }
        }

        private string CurCar
        {
            get
            {
                if (cboCar.SelectedIndex < 0) return "";
                CodeItem c = cboCar.Items[cboCar.SelectedIndex] as CodeItem;
                return (c == null) ? "" : c.Code;
            }
        }

        // ------------------------------------------------------------------
        // 목록 조회
        // ------------------------------------------------------------------
        private void Search(PartInfoService.Dir dir, string ptno)
        {
            if (_busy) return;

            string lep = CurLep;
            string wh = CurWh;
            string car = CurCar;

            Begin("조회중...");
            Async.Run(this,
                delegate { return PartInfoService.Search(lep, ptno, wh, car, dir); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList rows = (ArrayList)r;
                    if (rows.Count == 0)
                    {
                        Report(MP_NOSTOCK, "해당 부품에 대한 재고정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }

                    FillGrid(rows);

                    // 원본 : 부번을 입력한 조회일 때만 품명/수불코드를 채운다
                    if (txtPart.Text.Trim().Length > 0)
                    {
                        PartInfoRow first = (PartInfoRow)rows[0];
                        lblPartName.Text = first.PartName;
                        lblClass.Text = first.Class_;
                    }

                    Report(MP_OK, "정상 조회되었습니다.", MsgLevel.Success);
                });
        }

        private void OnPrev(object sender, EventArgs e)
        {
            string anchor = AnchorPtno(true);
            if (anchor == null) { Msg("먼저 조회하세요.", MsgLevel.Warn); return; }
            Search(PartInfoService.Dir.Prev, anchor);
        }

        private void OnNext(object sender, EventArgs e)
        {
            string anchor = AnchorPtno(false);
            if (anchor == null) { Msg("먼저 조회하세요.", MsgLevel.Warn); return; }
            Search(PartInfoService.Dir.Next, anchor);
        }

        /// <summary>페이징 기준 부번. 이전은 첫 행, 다음은 마지막 행.</summary>
        private string AnchorPtno(bool first)
        {
            if (lstPart.Items.Count == 0) 
                return null;
            int idx = first ? 0 : lstPart.Items.Count - 1;
            PartInfoRow p = lstPart.Items[idx].Tag as PartInfoRow;
            return (p == null) ? null : p.Ptno;
        }

        private void FillGrid(ArrayList rows)
        {
            lstPart.BeginUpdate();
            try
            {
                lstPart.Items.Clear();
                for (int n = 0; n < rows.Count; n++)
                {
                    PartInfoRow p = (PartInfoRow)rows[n];
                    ListViewItem it = new ListViewItem(PartNo.Display(p.Ptno));
                    it.SubItems.Add(Loc.Display(p.Locno));
                    it.SubItems.Add(p.AvlQty);
                    it.SubItems.Add(p.Price);
                    it.SubItems.Add(p.PartName);
                    it.SubItems.Add(p.Grade);
                    it.SubItems.Add(p.VhcKind);
                    it.SubItems.Add(p.InvQty);
                    it.SubItems.Add(p.Lep);
                    it.Tag = p;
                    lstPart.Items.Add(it);
                }
  
            }
            finally 
            { 
                lstPart.EndUpdate();
       
            }

            //if (lstPart.Items.Count > 0)
            //{
            //    for (int c = 0; c < lstPart.Columns.Count; c++)
            //        lstPart.Columns[1].Width = -1;
            //}
        }

        private PartInfoRow Selected
        {
            get
            {
                if (lstPart.SelectedIndices.Count == 0) return null;
                return lstPart.Items[lstPart.SelectedIndices[0]].Tag as PartInfoRow;
            }
        }

        // ------------------------------------------------------------------
        // 선택 행을 들고 다른 화면으로 (원본 gfn_SetLinkInfo "1C05" + gfn_GoToMenu)
        // ------------------------------------------------------------------
        private void OnPart(object sender, EventArgs e) { GoWith(ScreenId.StockByPart); }
        private void OnLoc(object sender, EventArgs e) { GoWith(ScreenId.LocRegister); }
        private void OnAdjust(object sender, EventArgs e) { GoWith(ScreenId.StockAdjust); }

        private void GoWith(int screenId)
        {
            PartInfoRow p = Selected;
            if (p == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            NavArgs a = new NavArgs();
            a.Set("LEP", p.Lep);
            a.Set("PTNO", PartNo.Key(p.Ptno));
            a.Set("PTNM", p.PartName);
            a.Set("CLASS", p.Grade);
            a.Set("LOCNO", Loc.Key(p.Locno));
            a.Set("WHSCD", CurWh);
            a.Set("QTY", p.AvlQty);
            a.Set("DETAIL_INFO", CurCar);

            Shell.Navigate(screenId, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
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

        /// <summary>MP101 이 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[324] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[324] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnPrev.Enabled = on;
            btnNext.Enabled = on;
            btnPart.Enabled = on;
            btnLoc.Enabled = on;
            btnAdjust.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearAll()
        {
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            lstPart.Items.Clear();
        }

        private void txtPart_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
