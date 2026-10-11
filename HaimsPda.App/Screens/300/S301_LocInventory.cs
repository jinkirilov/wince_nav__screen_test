using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [301] 재물조사(LOC) : LOC 를 스캔해 그 안의 부품을 한 건씩 실셈하고 저장한다. 다 끝나면 다음 LOC 로 넘어간다.
    // 원본 웹화면 : /ui/ws/plus/PL301_W01.xml (서버 메뉴 P188)
    //
    //   LOC 스캔 -> fn_getList : PL301_W01_S01 (LOC 안 부품) + S06 (이어서 셀 LOC 목록, 원본 hidgrid)
    //   부번 스캔 -> 목록에서 매칭 (서버 호출 없음, 계열 2건 이상이면 계열 선택)
    //   수량 입력 -> SaveProcess : 재고수량과 다르면 MP518 확인 -> 이상코드 선택
    //            -> fn_SaveChk : PL301_W01_S05 (CCH_ADJFG=Y 또는 없음 -> I01, 그 외 -> U01)
    //            -> fn_Save    : PL301_W01_I01 / U01
    //   저장 성공 -> 목록에서 그 행을 지우고 다음 행. 목록이 비면 다음 LOC 로 자동 이동.
    //   SKIP -> 확인 후 다음 LOC
    //   파트 -> [321]
    //
    // 원본과 다르게 한 부분
    //   - 수량 비교를 문자열이 아니라 숫자로 한다 ("3" 과 "3.00" 을 같게 본다).
    //   - 이상코드 팝업은 공통 목록 팝업(LepSelect.Choose)을 쓴다.
    //   - 원본 ↕ 버튼(버튼줄 접기)은 두지 않고 저장/SKIP/파트/지움을 한 줄에 다 둔다.
    //
    // [주의] CCH_DT 는 원본 전역변수 GV_CCH_INS_DT 를 그대로 따른다.
    //        S05 결과가 없으면 값을 지우지 않고 직전 값을 다시 보낸다.
    //        HAR(2026-10-11 14:13)에서 실제로 그렇게 나가는 것을 확인함 : 첫 I01 은 CCH_DT 공백,
    //        U01 이후 다른 부번의 I01 은 직전 CCH_INS_DTM 을 그대로 보냄.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S301_LocInventory.Designer.cs 에서 관리한다.
    public sealed partial class S301_LocInventory : ScreenBase
    {
        private const string MP_OK = "MP102";       // 정상 처리되었습니다
        private const string MP_SEARCH = "MP101";   // 정상 조회되었습니다
        private const string MP_NOLOC = "MP310";    // LOC 를 입력하십시오
        private const string MP_BADLOC = "MP313";   // 해당 LOC 는 사용할 수 없는 LOC 입니다
        private const string MP_PTNO = "MP303";     // 부품번호를 확인하세요
        private const string MP_NOLEP = "MP570";    // 부품에 해당하는 계열이 없습니다
        private const string MP_DIFF = "MP518";     // 입력한 수량과 상이합니다. 이상내역을 등록하시겠습니까?
        private const string MP_SAVEFAIL = "MP108"; // 저장 실패
        private const string MP_NOSEL = "MP545";    // 선택된 데이터가 없습니다
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까

        private bool _busy;
        private string _focus = "";      // 원본 GV_FocusGbn : "LOCNO" / "PTNO"
        private ArrayList _locs = new ArrayList();   // 원본 hidgrid (LOC_LOCNO)
        private string _cchDt = "";      // 원본 GV_CCH_INS_DT (화면 수명 동안 유지)

        public override int ScreenNo { get { return ScreenId.LocInventory; } }
        public override string ScreenName { get { return "재물조사(LOC)"; } }

        public S301_LocInventory()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            WinApi.GridLines(this.lstLoc.Handle, true);
            WinApi.DoubleBuffering(this.lstLoc.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadWarehouses(args);
        }

        // ------------------------------------------------------------------
        // 창고 콤보 + 링크로 넘어온 LOC 자동조회 (원본 ds_LinkInfo LOCNO/WHSCD)
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
                    if (cboWh.Items.Count == 0) cboWh.Items.Add("M");
                    cboWh.SelectedIndex = 0;
                    SelectByText(cboWh, "M");
                    if (linkWh != null) SelectByText(cboWh, linkWh);

                    End("LOC 을 스캔/입력하세요.", MsgLevel.Info);

                    if (linkLoc != null && linkLoc.Trim().Length > 0)
                    {
                        txtLoc.Text = Loc.Display(linkLoc);
                        LocSearch(true);
                    }
                    else txtLoc.Focus();
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

        // ------------------------------------------------------------------
        // 입력 이벤트
        // ------------------------------------------------------------------
        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocSearch(true);
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            ListSearch();
        }

        private void OnQtyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            SaveProcess();
        }

        // 원본 fn_Barcode
        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;
            if (_focus == "LOCNO" || (_focus.Length == 0 && lstLoc.Items.Count == 0))
            {
                txtLoc.Text = Loc.Display(data.Text);
                LocSearch(true);
            }
            else
            {
                txtPart.Text = PartNo.Display(data.Text);
                ListSearch();
            }
        }

        // ------------------------------------------------------------------
        // LOC 조회 (fn_getList). withLocList : 원본 GV_SearchChk=="false" -> S06 도 함께
        // ------------------------------------------------------------------
        private void LocSearch(bool withLocList)
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
            lstLoc.Items.Clear();
            ClearPart();

            string wh = CurWh;

            Begin("조회중...");
            Async.Run(this,
                delegate { return InventoryService.SearchLoc(wh, loc, withLocList); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    InvLocResult sr = (InvLocResult)r;
                    if (sr.Rows.Count == 0)
                    {
                        Report(MP_BADLOC, "해당 LOC 는 사용할 수 없는 LOC 입니다.", MsgLevel.Warn);
                        txtLoc.Focus();
                        txtLoc.SelectAll();
                        return;
                    }

                    if (sr.Locs != null) _locs = sr.Locs;
                    FillGrid(sr.Rows);

                    End(CommonCache.Msg(MP_SEARCH, "정상 조회되었습니다.") + " (" + sr.Rows.Count + "건"
                        + (_locs.Count > 1 ? ", 다음 LOC " + (_locs.Count - 1) : "") + ")", MsgLevel.Success);
                    SelectRow(0);   // 원본 fn_getClickCell("0")
                });
        }

        private void FillGrid(ArrayList rows)
        {
            lstLoc.BeginUpdate();
            try
            {
                lstLoc.Items.Clear();
                for (int i = 0; i < rows.Count; i++)
                {
                    InvLocRow p = (InvLocRow)rows[i];
                    ListViewItem it = new ListViewItem(p.Lep);
                    it.SubItems.Add(PartNo.Display(p.Ptno));
                    it.SubItems.Add(Num(p.AvlQt));
                    it.SubItems.Add(p.LocFl);
                    it.Tag = p;
                    lstLoc.Items.Add(it);
                }
            }
            finally { lstLoc.EndUpdate(); }
        }

        // ------------------------------------------------------------------
        // 부번 -> 목록 매칭 (fn_validatePTNO / GetLep / ListSearchData)
        // ------------------------------------------------------------------
        private void ListSearch()
        {
            if (_busy) return;
            if (lstLoc.Items.Count == 0) { txtLoc.Focus(); return; }

            string key = PartNo.Key(txtPart.Text);
            if (key.Length == 0) { Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn); FocusPart(); return; }
            txtPart.Text = PartNo.Display(key);

            ArrayList hit = new ArrayList();
            ArrayList labels = new ArrayList();
            for (int i = 0; i < lstLoc.Items.Count; i++)
            {
                InvLocRow p = lstLoc.Items[i].Tag as InvLocRow;
                if (p == null || PartNo.Key(p.Ptno) != key) continue;
                hit.Add(i);
                labels.Add(p.Lep.Length > 0 ? p.Lep : "H");
            }

            if (hit.Count == 0)
            {
                ClearPart();
                txtPart.Text = PartNo.Display(key);
                Report(MP_NOLEP, "부품에 해당하는 계열이 없습니다.", MsgLevel.Warn);
                FocusPart();
                return;
            }

            int idx = HaimsPda.Controls.LepSelect.Pick(labels, "계열 선택 - " + PartNo.Display(key));
            if (idx < 0) { Msg("취소했습니다.", MsgLevel.Info); FocusPart(); return; }

            SelectRow((int)hit[idx]);
        }

        private bool _syncLock;

        private void SelectRow(int index)
        {
            if (index < 0 || index >= lstLoc.Items.Count) return;

            _syncLock = true;
            try
            {
                for (int i = 0; i < lstLoc.Items.Count; i++)
                    lstLoc.Items[i].Selected = (i == index);
                lstLoc.EnsureVisible(index);
            }
            finally { _syncLock = false; }

            ShowRow(lstLoc.Items[index].Tag as InvLocRow);
        }

        private void OnRowSelected(object sender, EventArgs e)
        {
            if (_syncLock || lstLoc.SelectedIndices.Count == 0) return;
            ShowRow(lstLoc.Items[lstLoc.SelectedIndices[0]].Tag as InvLocRow);
        }

        /// <summary>원본 fn_getClickCell : 수량은 재고수량으로 채워 둔다</summary>
        private void ShowRow(InvLocRow p)
        {
            if (p == null) return;
            lblPrefix.Text = (p.Lep.Length > 0) ? p.Lep : "H";
            txtPart.Text = PartNo.Display(p.Ptno);
            lblClass.Text = p.Class_;
            lblPartName.Text = p.PartName;
            lblOutQty.Text = Num(p.SalQt);
            lblPrice.Text = p.Price;
            lblAvl.Text = Num(p.AvlQt);
            txtQty.Text = Num(p.AvlQt);
            lblCnt.Text = lstLoc.Items.Count + "건";
            txtQty.Focus();
            txtQty.SelectAll();
        }

        private int SelectedIndex
        {
            get { return (lstLoc.SelectedIndices.Count == 0) ? -1 : lstLoc.SelectedIndices[0]; }
        }

        private InvLocRow Selected
        {
            get
            {
                int i = SelectedIndex;
                return (i < 0) ? null : lstLoc.Items[i].Tag as InvLocRow;
            }
        }

        // ------------------------------------------------------------------
        // 저장 (SaveProcess -> 이상코드 -> fn_SaveChk -> fn_Save)
        // ------------------------------------------------------------------
        private void OnSave(object sender, EventArgs e) { SaveProcess(); }

        private void SaveProcess()
        {
            if (_busy) return;

            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                FocusPart();
                return;
            }
            if (lstLoc.Items.Count == 0)
            {
                ReportText("LOC 조회를 해주세요.", MsgLevel.Warn);
                txtLoc.Focus();
                return;
            }
            InvLocRow p = Selected;
            if (p == null || PartNo.Key(p.Ptno) != PartNo.Key(txtPart.Text))
            {
                ReportText("부품 조회를 해주세요.", MsgLevel.Warn);
                FocusPart();
                return;
            }
            string qty = txtQty.Text.Trim();
            if (qty.Length == 0)
            {
                ReportText("수량을 입력해 주세요.", MsgLevel.Warn);
                txtQty.Focus();
                return;
            }
            qty = ToInt(qty).ToString();

            if (ToInt(qty) != ToInt(p.AvlQt))
            {
                if (!Confirm(CommonCache.Msg(MP_DIFF, "입력한 수량과 상이합니다. 이상내역을 등록하시겠습니까?")))
                {
                    txtQty.Focus();
                    txtQty.SelectAll();
                    return;
                }
                PickReasonAndSave(p, qty);
            }
            else
            {
                Save(p, qty, "");
            }
        }

        private void PickReasonAndSave(InvLocRow p, string qty)
        {
            Begin("이상코드 조회중...");
            Async.Run(this,
                delegate { return InventoryService.GetReasons(InventoryService.Pgm301); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList codes = (ArrayList)r;
                    if (codes.Count == 0)
                    {
                        ReportText("이상코드가 등록되어있지 않습니다.", MsgLevel.Warn);
                        return;
                    }
                    End("", MsgLevel.Info);

                    int idx = HaimsPda.Controls.LepSelect.Choose(codes, "이상코드 선택", "이상코드");
                    if (idx < 0) { Msg("취소했습니다.", MsgLevel.Info); txtQty.Focus(); return; }

                    Save(p, qty, ((InvReason)codes[idx]).Code);
                });
        }

        private void Save(InvLocRow p, string qty, string reason)
        {
            string wh = CurWh;
            string loc = Loc.Key(txtLoc.Text);
            string prevDt = _cchDt;
            int rowIdx = SelectedIndex;
            string failMsg = CommonCache.Msg(MP_SAVEFAIL, "저장에 실패하였습니다.");

            Begin("저장중...");
            Async.Run(this,
                delegate
                {
                    // fn_SaveChk
                    InvSaveChk c = InventoryService.SaveCheck(wh, p.Lep, p.Ptno);
                    bool update = c.Found && c.AdjFg != "Y";
                    string dt = c.Found ? c.InsDtm : prevDt;

                    // fn_Save
                    try { InventoryService.SaveLoc(update, wh, loc, p, qty, dt, reason); }
                    catch (Exception) { throw new HaimsException(failMsg); }
                    return dt;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    _cchDt = (string)r;
                    End(CommonCache.Msg(MP_OK, "정상 처리되었습니다."), MsgLevel.Success);
                    AfterSave(rowIdx);
                });
        }

        /// <summary>원본 fn_Save_After</summary>
        private void AfterSave(int rowIdx)
        {
            if (rowIdx >= 0 && rowIdx < lstLoc.Items.Count) lstLoc.Items.RemoveAt(rowIdx);

            if (lstLoc.Items.Count > 0)
            {
                SelectRow(Math.Min(rowIdx, lstLoc.Items.Count - 1));
                return;
            }

            // 이 LOC 는 끝
            ClearPart();
            if (_locs.Count > 1)
            {
                NextLoc();
            }
            else
            {
                _locs.Clear();
                txtLoc.Text = "";
                Msg("마지막 LOC 까지 처리했습니다. 다음 LOC 을 스캔하세요.", MsgLevel.Success);
                txtLoc.Focus();
            }
        }

        // ------------------------------------------------------------------
        // SKIP / 다음 LOC (fn_Next_Loc)
        // ------------------------------------------------------------------
        private void OnSkip(object sender, EventArgs e)
        {
            if (_busy) return;
            if (_locs.Count == 0)
            {
                ReportText("로케이션을 조회하십시오.", MsgLevel.Warn);
                txtLoc.Focus();
                return;
            }
            if (_locs.Count == 1)
            {
                ReportText("마지막 로케이션입니다.", MsgLevel.Warn);
                return;
            }
            if (!Confirm("다음 로케이션이 존재합니다. 계속 작업하시겠습니까?")) return;
            NextLoc();
        }

        private void NextLoc()
        {
            // 원본 : hidgrid 가 2건이면 다음 LOC 에서 S06 을 다시 받는다
            bool withLocList = (_locs.Count == 2);
            _locs.RemoveAt(0);
            txtLoc.Text = Loc.Display((string)_locs[0]);
            ClearPart();
            LocSearch(withLocList);
        }

        // ------------------------------------------------------------------
        // 파트 -> [321] (원본 OnBtnStock, 메뉴 1C03)
        // ------------------------------------------------------------------
        private void OnPart(object sender, EventArgs e)
        {
            if (_busy) return;
            InvLocRow p = Selected;
            if (p == null) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            NavArgs a = new NavArgs();
            a.Set("LEP", (p.Lep.Length > 0) ? p.Lep : "H");
            a.Set("PTNO", PartNo.Key(p.Ptno));
            a.Set("PTNM", p.PartName);
            a.Set("CLASS", p.Class_);
            a.Set("LOCNO", Loc.Key(txtLoc.Text));
            a.Set("WHSCD", CurWh);
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "화면을 지우시겠습니까?"))) return;
            ClearAll();
            SelectByText(cboWh, "M");
            txtLoc.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void FocusPart()
        {
            txtPart.Focus();
            txtPart.SelectAll();
        }

        private static string Num(string s) { return ToInt(s).ToString(); }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try
            {
                return (int)double.Parse(s, System.Globalization.NumberStyles.Float,
                                         System.Globalization.CultureInfo.InvariantCulture);
            }
            catch { return 0; }
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[301] " + ScreenName,
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
            if (msg.Length > 0) Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            MessageBox.Show(text, "[301] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[301] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSave.Enabled = on;
            btnSkip.Enabled = on;
            btnPart.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearPart()
        {
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            lblCnt.Text = "";
            lblAvl.Text = "";
            lblOutQty.Text = "";
            lblPrice.Text = "";
            txtQty.Text = "";
        }

        /// <summary>원본 fn_Init</summary>
        private void ClearAll()
        {
            _focus = "";
            _locs = new ArrayList();
            txtLoc.Text = "";
            lstLoc.Items.Clear();
            ClearPart();
        }
    }
}
