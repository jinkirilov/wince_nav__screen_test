using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [321] 파트별재고 : 부번 조회 -> 헤더 + LOC별 재고 목록
    // 원본 웹화면 : /ui/ws/plus/PL321_W01.xml
    //
    //   fn_SearchLep(S03) -> fn_SearchPTNO(S01 + S02)
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S321_PartStock.Designer.cs 에서 관리한다.
    public sealed partial class S321_PartStock : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_NOSTOCK = "MP333"; // 해당부품에 대한 재고정보가 없습니다
        private const string MP_NOSEL = "MP545";   // 선택된 데이터가 없습니다

        private string _lep = "H";
        private bool _hasStockOnly = true;   // btnGubun : true="재고유"(P), false="재고무"("")
        private bool _busy;

        public override int ScreenNo { get { return ScreenId.StockByPart; } }
        public override string ScreenName { get { return "파트별재고"; } }

        public S321_PartStock()
        {
            InitializeComponent();
            if (IsDesignMode) 
                return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            // [140] 등에서 부번을 들고 넘어온 경우 바로 조회한다 (원본 ds_LinkInfo 대응)
            string ptno = (args == null) ? null : args.GetString("PTNO");
            if (ptno != null && ptno.Length > 0)
            {
                string lep = args.GetString("LEP");
                if (lep != null && lep.Length > 0) { _lep = lep; lblPrefix.Text = lep; }

                txtPart.Text = PartNo.Display(ptno);
                lblPartName.Text = Str(args.GetString("PTNM"));
                lblClass.Text = Str(args.GetString("CLASS"));

                SearchPart(PartNo.Key(ptno));
                return;
            }

            txtPart.Focus();
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
        }

        private static string Str(string s) 
        { 
            return (s == null) ? "" : s; 
        }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            txtPart.Text = PartNo.Display(data.Text);
            SearchLep();
        }


        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchLep(); }
        }

        private void OnGubunToggle(object sender, EventArgs e)
        {
            _hasStockOnly = !_hasStockOnly;
            btnGubun.Text = _hasStockOnly ? "재고유" : "재고무";
        }

        private string Gubun
        {
            get { return _hasStockOnly ? StockService.GubunHasStock : StockService.GubunAll; }
        }

        // ------------------------------------------------------------------
        // 1단계 : 부번 -> 계열
        // ------------------------------------------------------------------
        private void SearchLep()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { txtPart.Focus(); return; }

            txtPart.Text = PartNo.Display(txtPart.Text);
            ClearResult();

            Begin("조회중...");
            Async.Run(this,
                delegate { return StockService.SearchLep(ptno, Gubun); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;
                    if (leps.Count == 0)
                    {
                        _lep = "H";
                        lblPrefix.Text = "H";
                        Report(MP_NOSTOCK, "해당 부품에 대한 재고정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }

                    // 계열이 2건 이상이면 선택 팝업을 띄운다 (원본 lep_popup)
                    int idx = MobisHaims.Controls.LepSelect.Pick(
                                  leps, "계열 선택 - " + PartNo.Display(txtPart.Text));
                    if (idx < 0)
                    {
                        End("취소했습니다.", MsgLevel.Info);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    _lep = (string)leps[idx];
                    lblPrefix.Text = _lep;

                    SearchPart(ptno);
                });
        }

        // ------------------------------------------------------------------
        // 2단계 : 헤더 + LOC 목록
        // ------------------------------------------------------------------
        private void SearchPart(string ptno)
        {
            Async.Run(this,
                delegate { return StockService.SearchPart(_lep, ptno, Gubun); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    PartStockResult sr = (PartStockResult)r;
                    if (!sr.HasAny)
                    {
                        Report(MP_NOSTOCK, "해당 부품에 대한 재고정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }

                    PartStockInfo i = sr.Info;
                    lblClass.Text = i.Grade;
                    lblPartName.Text = i.PartName;
                    txtAvlQty.Text = i.AvlQty;
                    txtPrice.Text = i.Price;
                    txtDefQty.Text = i.DefQty;
                    txtDoQty.Text = i.DoQty;
                    txtAms.Text = i.Ams;
                    txtSftQty.Text = i.SftQty;
                    txtVhc.Text = i.VhcKind;
                    txtStdIn.Text = i.StdInQty;

                    FillGrid(sr.Locs);

                    Report(MP_OK, "정상 조회되었습니다.", MsgLevel.Success);
                    txtPart.Focus();
                    txtPart.SelectAll();
                });
        }

        private void FillGrid(ArrayList locs)
        {
            lstLoc.BeginUpdate();
            try
            {
                lstLoc.Items.Clear();
                for (int n = 0; n < locs.Count; n++)
                {
                    LocStockRow l = (LocStockRow)locs[n];
                    ListViewItem it = new ListViewItem(l.Whscd);
                    it.SubItems.Add(Loc.Display(l.Locno));
                    it.SubItems.Add(l.AvlQty);
                    it.Tag = l;
                    lstLoc.Items.Add(it);
                }
            }
            finally { lstLoc.EndUpdate(); }
        }

        /// <summary>선택된 LOC 행. 없으면 null.</summary>
        private LocStockRow Selected
        {
            get
            {
                if (lstLoc.SelectedIndices.Count == 0) return null;
                return (LocStockRow)lstLoc.Items[lstLoc.SelectedIndices[0]].Tag;
            }
        }

        // ------------------------------------------------------------------
        // 버튼 : 선택 행을 들고 다른 화면으로 넘어간다 (원본 gfn_SetLinkInfo + gfn_GoToMenu)
        // ------------------------------------------------------------------
        private void OnWealth(object sender, EventArgs e) { GoWith(ScreenId.LocInventory, "재물조사(LOC)"); }
        private void OnControl(object sender, EventArgs e) { GoWith(0, "OS&D(212)"); }
        private void OnAdjust(object sender, EventArgs e) { GoWith(0, "재고조정(330)"); }

        private void GoWith(int screenId, string name)
        {
            LocStockRow l = Selected;
            if (l == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            // TODO: NavArgs 로 LEP/PTNO/PTNM/CLASS/LOCNO/WHSCD/AVLQT 전달 (ds_LinkInfo 대응)
            Msg(name + " 연결 예정 - " + l.Whscd + " " + l.Locno + " " + l.AvlQty, MsgLevel.Info);
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
            if (code != MP_OK) MessageBox.Show(text, "[321] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[321] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnGubun.Enabled = on;
            btnWealth.Enabled = on;
            btnControl.Enabled = on;
            btnAdjust.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearResult()
        {
            lblClass.Text = "";
            lblPartName.Text = "";
            txtAvlQty.Text = ""; txtPrice.Text = "";
            txtDefQty.Text = ""; txtDoQty.Text = "";
            txtAms.Text = ""; txtSftQty.Text = "";
            txtVhc.Text = ""; txtStdIn.Text = "";
            lstLoc.Items.Clear();
        }

        private void ClearAll()
        {
            _lep = "H";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            ClearResult();
        }

        private void txtPart_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
