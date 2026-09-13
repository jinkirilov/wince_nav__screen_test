using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [320] LOC별재고 : LOC 를 조회해 그 안의 부품 목록을 보고, 부번으로 행을 찾아 세부/통제/조정/LOC 로 넘어간다.
    // 원본 웹화면 : /ui/ws/plus/PL320_W01.xml
    //
    //   fn_LocSearch(S01) -> 목록  ->  fn_ListSearch(부번 매칭) -> OnBtnDetail
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S320_LocStock.Designer.cs 에서 관리한다.
    //
    // [주의] 대문자 변환은 KeyPress 에서 하지 않는다. CF 는 KeyChar 대입을 무시한다.
    //        조회 시점에 PartNo.Key / Loc.Key 가 ToUpper 한다.
    public sealed partial class S320_LocStock : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_NOLOC = "MP310";    // LOC 를 입력하십시오
        private const string MP_BADLOC = "MP313";   // 해당 LOC 는 사용할 수 없는 LOC 입니다
        private const string MP_NOPART = "MP303";   // 부품번호를 입력하십시오
        private const string MP_NOSEL = "MP545";    // 선택된 데이터가 없습니다
        private const string MP_NOLEP = "MP570";    // 부품에 해당하는 계열이 없습니다

        private bool _busy;
        private bool _hasStockOnly = true;   // btnGubun  : true="재고유"(P) / false="재고무"("")
        private bool _stdOnly = true;        // btnGubun2 : true="표준유"    / false="표준무"
        private string _lep = "H";
        private string _focus = "";          // 원본 GV_FocusGbn : "LOCNO" / "PTNO"

        public override int ScreenNo { get { return ScreenId.StockByLoc; } }
        public override string ScreenName { get { return "LOC별재고"; } }

        public S320_LocStock()
        {
            InitializeComponent();
            if (IsDesignMode) 
                return;

            WinApi.GridLines(this.lstLoc.Handle, true);
            WinApi.DoubleBuffering(this.lstLoc.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadWarehouses(args);
        }

        // ------------------------------------------------------------------
        // 창고 콤보 (원본 gfn_SearchWHS_Plus) + 링크로 넘어온 LOC 자동조회
        // ------------------------------------------------------------------
        private void LoadWarehouses(NavArgs args)
        {
            string linkWh = (args == null) ? null : args.GetString("WHSCD");
            string linkLoc = (args == null) ? null : args.GetString("LOCNO");

            Begin("창고 조회중...");
            Async.Run(this,
                delegate { return LocStockService.GetWarehouses(); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    cboWh.Items.Clear();
                    for (int i = 0; i < list.Count; i++) cboWh.Items.Add(list[i]);
                    if (cboWh.Items.Count > 0) cboWh.SelectedIndex = 0;
                    SelectByText(cboWh, "M");
                    if (linkWh != null) SelectByText(cboWh, linkWh);

                    End("LOC 을 스캔/입력하세요.", MsgLevel.Info);

                    if (linkLoc != null && linkLoc.Length > 0)
                    {
                        txtLoc.Text = Loc.Display(linkLoc);
                        LocSearch();
                    }
                    else 
                        txtLoc.Focus();
                });
        }

        private static void SelectByText(ComboBox cbo, string text)
        {
            if (text == null || text.Length == 0) return;
            for (int i = 0; i < cbo.Items.Count; i++)
                if (cbo.Items[i].ToString() == text) { cbo.SelectedIndex = i; return; }
        }

        private string CurWh
        {
            get { return (cboWh.SelectedIndex < 0) ? "M" : cboWh.Items[cboWh.SelectedIndex].ToString(); }
        }

        private string Gubun
        {
            get { return _hasStockOnly ? LocStockService.GubunHasStock : LocStockService.GubunAll; }
        }

        // ------------------------------------------------------------------
        // 입력 이벤트
        // ------------------------------------------------------------------
        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) 
                return;
            e.Handled = true;
            LocSearch();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            ListSearch();
        }

        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            if (_focus == "PTNO")
            {
                txtPart.Text = PartNo.Display(data.Text);
                ListSearch();
            }
            else
            {
                txtLoc.Text = Loc.Display(data.Text);
                LocSearch();
            }
        }

        private void OnGubunToggle(object sender, EventArgs e)
        {
            _hasStockOnly = !_hasStockOnly;
            btnGubun.Text = _hasStockOnly ? "재고유" : "재고무";
            txtLoc.Focus();
        }

        private void OnStdToggle(object sender, EventArgs e)
        {
            _stdOnly = !_stdOnly;
            btnGubun2.Text = _stdOnly ? "표준유" : "표준무";
            txtLoc.Focus();
        }

        // ------------------------------------------------------------------
        // LOC 조회 (fn_LocSearch)
        // ------------------------------------------------------------------
        private void LocSearch()
        {
            if (_busy) return;

            string loc = Loc.Key(txtLoc.Text);
            if (loc.Length == 0)
            {
                Report(MP_NOLOC, "LOC 을 입력하십시오.", MsgLevel.Warn);
                txtLoc.Focus();
                return;
            }

            txtLoc.Text = Loc.Display(loc);
            ClearResult();

            // POS 판정 : 원본과 동일
            string pos;
            string send = loc;
            bool showPos;

            if (_stdOnly)
            {
                pos = LocStockService.PosStd;
                if (send.Length > 9) send = send.Substring(0, 9);
                showPos = true;
            }
            else if (loc.Length == 9)
            {
                pos = LocStockService.PosStd;
                showPos = true;
            }
            else
            {
                pos = LocStockService.PosPick;
                showPos = false;
            }

            colPos.Width = showPos ? 60 : 0;

            string wh = CurWh;
            string gubun = Gubun;

            Begin("조회중...");
            Async.Run(this,
                delegate { return LocStockService.Search(wh, send, pos, gubun); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList rows = (ArrayList)r;
                    if (rows.Count == 0)
                    {
                        Report(MP_BADLOC, "해당 LOC 는 사용할 수 없는 LOC 입니다.", MsgLevel.Warn);
                        txtLoc.Focus();
                        return;
                    }

                    FillGrid(rows);
                    txtCnt.Text = rows.Count.ToString();

                    Report(MP_OK, "정상 조회되었습니다.", MsgLevel.Success);
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 부번으로 목록에서 찾기 (fn_ListSearch) — 서버를 다시 부르지 않는다
        // ------------------------------------------------------------------
        private void ListSearch()
        {
            if (lstLoc.Items.Count == 0) 
            { 
                txtLoc.Focus(); 
                return; 
            }

            string key = PartNo.Key(txtPart.Text);
            if (key.Length == 0) { txtPart.Focus(); return; }

            txtPart.Text = PartNo.Display(key);

            ArrayList hit = new ArrayList();
            for (int i = 0; i < lstLoc.Items.Count; i++)
            {
                LocPartRow p = lstLoc.Items[i].Tag as LocPartRow;
                if (p != null && PartNo.Key(p.Ptno) == key) hit.Add(i);
            }

            if (hit.Count == 0)
            {
                _lep = "H";
                lblPrefix.Text = "H";
                Report(MP_NOLEP, "부품에 해당하는 계열이 없습니다.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            // 계열이 2건 이상이면 원본은 계열선택 팝업을 띄운다. 여기서는 첫 건으로 진행한다.
            SelectRow((int)hit[0]);
            if (hit.Count > 1)
                Msg("계열 " + hit.Count + "건 - 첫 건(" + _lep + ")으로 진행", MsgLevel.Info);

            GoDetail();
        }

        private void SelectRow(int index)
        {
            if (index < 0 || index >= lstLoc.Items.Count) return;

            lstLoc.Items[index].Selected = true;
            lstLoc.EnsureVisible(index);
            SyncFromRow(lstLoc.Items[index].Tag as LocPartRow);
        }

        private void SyncFromRow(LocPartRow p)
        {
            if (p == null) return;
            _lep = (p.Lep.Length > 0) ? p.Lep : "H";
            lblPrefix.Text = _lep;
            txtPart.Text = PartNo.Display(p.Ptno);
            lblClass.Text = p.Class_;
        }

        private void OnRowSelected(object sender, EventArgs e)
        {
            if (lstLoc.SelectedIndices.Count == 0) return;
            SyncFromRow(lstLoc.Items[lstLoc.SelectedIndices[0]].Tag as LocPartRow);
        }

        private void FillGrid(ArrayList rows)
        {
            lstLoc.BeginUpdate();
            try
            {
                lstLoc.Items.Clear();
                for (int n = 0; n < rows.Count; n++)
                {
                    LocPartRow p = (LocPartRow)rows[n];
                    ListViewItem it = new ListViewItem(p.Pos);
                    it.SubItems.Add(p.Lep);
                    it.SubItems.Add(PartNo.Display(p.Ptno));
                    it.SubItems.Add(p.AvlQty);
                    it.Tag = p;
                    lstLoc.Items.Add(it);
                }
            }
            finally { lstLoc.EndUpdate(); }
        }

        private LocPartRow Selected
        {
            get
            {
                if (lstLoc.SelectedIndices.Count == 0) return null;
                return lstLoc.Items[lstLoc.SelectedIndices[0]].Tag as LocPartRow;
            }
        }

        // ------------------------------------------------------------------
        // 버튼 (원본 gfn_SetLinkInfo "1C02" + gfn_GoToMenu)
        // ------------------------------------------------------------------
        private void OnDetail(object sender, EventArgs e) { GoDetail(); }

        private void GoDetail()
        {
            LocPartRow p = Selected;
            if (p == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            Shell.Navigate(ScreenId.StockDetail, LinkArgs(p));   // [322] 상세내역
        }

        private void OnControl(object sender, EventArgs e)
        {
            LocPartRow p = Selected;
            if (p == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            Msg("OS&D(212) 연결 예정 - " + PartNo.Display(p.Ptno) + " " + p.AvlQty, MsgLevel.Info);
        }

        private void OnAdjust(object sender, EventArgs e)
        {
            LocPartRow p = Selected;
            if (p == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            NavArgs a = LinkArgs(p);
            a.Set("QTY", p.AvlQty);
            Shell.Navigate(ScreenId.StockAdjust, a);             // [330] 재고조정
        }

        private void OnLocMove(object sender, EventArgs e)
        {
            if (Loc.Key(txtLoc.Text).Length == 0)
            {
                Report(MP_NOLOC, "LOC 을 입력하십시오.", MsgLevel.Warn);
                return;
            }
            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_NOPART, "부품번호를 입력하십시오.", MsgLevel.Warn);
                return;
            }

            LocPartRow p = Selected;
            NavArgs a = (p != null) ? LinkArgs(p) : new NavArgs();
            a.Set("LEP", _lep);
            a.Set("PTNO", PartNo.Key(txtPart.Text));
            a.Set("LOCNO", Loc.Key(txtLoc.Text));
            a.Set("WHSCD", CurWh);
            Shell.Navigate(ScreenId.LocMove, a);                 // [401] LOC재고이동
        }

        private NavArgs LinkArgs(LocPartRow p)
        {
            NavArgs a = new NavArgs();
            a.Set("LEP", (p.Lep.Length > 0) ? p.Lep : _lep);
            a.Set("PTNO", PartNo.Key(p.Ptno));
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", p.Class_);
            a.Set("LOCNO", Loc.Key(txtLoc.Text));
            a.Set("WHSCD", CurWh);
            a.Set("EXPECTQTY", p.AvlQty);
            return a;
        }

        private void OnClear(object sender, EventArgs e)
        {
            ClearAll();
            txtLoc.Focus();
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
            if (code != MP_OK) MessageBox.Show(text, "[320] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[320] " + ScreenName);
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
            btnGubun2.Enabled = on;
            btnDetail.Enabled = on;
            btnControl.Enabled = on;
            btnAdjust.Enabled = on;
            btnLoc.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearResult()
        {
            lstLoc.Items.Clear();
            txtCnt.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
        }

        private void ClearAll()
        {
            _lep = "H";
            lblPrefix.Text = "H";
            txtLoc.Text = "";
            txtPart.Text = "";
            ClearResult();
        }
    }
}
