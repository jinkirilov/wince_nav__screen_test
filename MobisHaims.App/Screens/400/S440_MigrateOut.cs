using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [440] 실시간창고이관(출고) : 다른 창고로 이관할 부품을 출고한다 (같은 창고 안이면 [430] 창고이전).
    // 원본 웹화면 : /ui/ws/plus/PL440_W01.xml (메뉴 1D06 / P180), 매뉴얼 LOC관리 195~198p
    //
    //   창고 선택 -> LOC 스캔(S01 확인) -> 부번 스캔(S02) -> 대상수량 Enter
    //   -> 증표 채번(S03) -> 저장(I01 + I02 + U01) -> 처리 목록에 추가
    //
    // 원본과 다르게 한 부분
    //   - 부품의 LOC 가 스캔한 LOC 와 다르면 알려 준다 (원본은 조회 결과 LOC 로 말없이 덮어씀).
    //   - 대상수량 0 이하를 막는다 (원본은 초과만 MP582).
    //   - 저장 후 창고를 M 으로 되돌리지 않는다 (원본 fn_SetData 는 매번 selWHS = M).
    //   - S02 0건일 때 원본은 S04 를 S02 에 덧붙여 다시 보낸다(tit_ClearActionInfo 누락). 여기서는 S04 만.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S440_MigrateOut.Designer.cs 에서 관리한다.
    public sealed partial class S440_MigrateOut : ScreenBase
    {
        private const string MP_OK = "MP101";
        private const string MP_SAVED = "MP102";
        private const string MP_SAVEERR = "MP108";
        private const string MP_NOPART = "MP303";
        private const string MP_BADLOC = "MP311";   // 정확한 로케이션 코드가 아닙니다
        private const string MP_CLEAR = "MP503";
        private const string MP_DEL = "MP512";
        private const string MP_NOSEL = "MP545";
        private const string MP_OVERQT = "MP582";   // 이관수량이 재고수량보다 많습니다
        private const string MP_SALQT = "MP583";    // 출고대기 수량 존재
        private const string MP_LOCN = "MP592";     // 사용할 수 없는 LOC

        private bool _busy;
        private bool _loading;
        private string _focus = "";      // LOCNO / PTNO
        private string _scanLoc = "";    // 확인된 LOC
        private MigrateOutItem _cur;
        private bool _blocked;
        private bool _sortDesc;

        public override int ScreenNo { get { return ScreenId.WhMigrateOut; } }
        public override string ScreenName { get { return "실시간창고이관(출고)"; } }

        public S440_MigrateOut()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            WinApi.GridLines(this.lstList.Handle, true);
            WinApi.DoubleBuffering(this.lstList.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadWarehouses(Arg(args, "WHSCD"), Arg(args, "PTNO"));
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        private void LoadWarehouses(string linkWh, string linkPtno)
        {
            Begin("창고 조회중...");
            Async.Run(this,
                delegate { return LocStockService.GetWarehouses(); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    _loading = true;
                    try
                    {
                        cboWh.Items.Clear();
                        for (int i = 0; i < list.Count; i++) cboWh.Items.Add(list[i]);
                        if (cboWh.Items.Count > 0) cboWh.SelectedIndex = 0;
                        SelectByText(cboWh, "M");
                        SelectByText(cboWh, linkWh);
                    }
                    finally { _loading = false; }

                    End("LOC 를 스캔하세요.", MsgLevel.Info);

                    if (linkPtno.Length > 0) { txtPart.Text = PartNo.Display(linkPtno); PartEnter(); }
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

        private void OnWhChanged(object sender, EventArgs e)
        {
            if (_loading || _busy) return;
            _scanLoc = "";
            ClearCurrent();
            txtLoc.Text = "";
            txtLoc.Focus();
        }

        // ------------------------------------------------------------------
        // 스캔 / 입력
        // ------------------------------------------------------------------
        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            if (_busy) return;

            if (_focus == "LOCNO")
            {
                txtLoc.Text = Loc.Display(data.Text);
                LocEnter();
            }
            else
            {
                txtPart.Text = PartNo.Display(data.Text);
                PartEnter();
            }
        }

        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }
        private void OnOtherFocus(object sender, EventArgs e) { _focus = ""; }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocEnter();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            PartEnter();
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Save();
        }

        private void OnQtyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        // ------------------------------------------------------------------
        // LOC 확인 (원본 fn_SearchLoc)
        // ------------------------------------------------------------------
        private void LocEnter()
        {
            if (_busy) return;

            string loc = Loc.Key(txtLoc.Text);
            if (loc.Length == 0) { txtLoc.Focus(); return; }
            txtLoc.Text = Loc.Display(loc);

            string wh = CurWh;

            Begin("LOC 확인중...");
            Async.Run(this,
                delegate { return MigrateOutService.CheckLoc(wh, loc); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    string yn = (string)r;
                    if (yn == null)
                    {
                        _scanLoc = "";
                        Report(MP_BADLOC, "정확한 로케이션 코드가 아닙니다.", MsgLevel.Warn);
                        FocusSel(txtLoc);
                        return;
                    }
                    if (yn == "N")
                    {
                        _scanLoc = "";
                        Report(MP_LOCN, "사용할 수 없는 로케이션입니다.", MsgLevel.Warn);
                        FocusSel(txtLoc);
                        return;
                    }

                    _scanLoc = loc;
                    ClearCurrent();
                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다.") + " 부번을 스캔하세요.", MsgLevel.Success);
                    FocusSel(txtPart);
                });
        }

        // ------------------------------------------------------------------
        // 부품 조회 (원본 fn_Search / fn_Out_Search / fn_popPart)
        // ------------------------------------------------------------------
        private void PartEnter()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            txtPart.Text = PartNo.Display(ptno);

            string wh = CurWh;

            Begin("조회중...");
            Async.Run(this,
                delegate
                {
                    object[] o = new object[2];
                    ArrayList list = MigrateOutService.Search(wh, ptno);
                    o[0] = list;
                    o[1] = (list.Count == 0) ? MigrateOutService.CountPendingOut(ptno) : 0;
                    return o;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    object[] o = (object[])r;
                    ArrayList list = (ArrayList)o[0];
                    int pending = (int)o[1];
                    ClearCurrent();

                    if (list.Count == 0)
                    {
                        if (pending > 0)
                            ReportText("출고 부품정보가 " + pending + "건 존재합니다.\r\n입고를 잡아주셔야 합니다.", MsgLevel.Warn);
                        else
                            ReportText("해당 창고에 재고가 없는 부품입니다.", MsgLevel.Warn);
                        txtPart.Text = "";
                        txtPart.Focus();
                        return;
                    }

                    int idx = MobisHaims.Controls.LepSelect.Pick(list, "계열 선택 - " + PartNo.Display(ptno));
                    if (idx < 0) { End("취소했습니다.", MsgLevel.Info); FocusSel(txtPart); return; }

                    MigrateOutItem it = (MigrateOutItem)list[idx];
                    _cur = it;
                    lblPrefix.Text = it.Lep;
                    txtPart.Text = PartNo.Display(it.Ptno);
                    txtLoc.Text = Loc.Display(it.Locno);
                    txtQty.Text = it.AvlQty;
                    txtOut.Text = it.SalQty;

                    if (ToInt(it.SalQty) > 0)
                    {
                        _blocked = true;
                        Report(MP_SALQT, "출고대기 수량이 있어 이관 출고할 수 없습니다.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }

                    string note = "";
                    if (_scanLoc.Length > 0 && _scanLoc != it.Locno)
                        note = " (스캔한 LOC 와 다름 - 부품 LOC " + Loc.Display(it.Locno) + ")";

                    End("수량을 확인하고 Enter 를 누르세요." + note, note.Length > 0 ? MsgLevel.Warn : MsgLevel.Success);
                    FocusSel(txtQty);
                });
        }

        // ------------------------------------------------------------------
        // 저장 (원본 fn_SearchInvoice -> fn_ConfirmQt -> fn_SetData)
        // ------------------------------------------------------------------
        private void Save()
        {
            if (_busy) return;
            if (_cur == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            if (_blocked) { Msg("출고대기가 있는 부품입니다.", MsgLevel.Warn); return; }

            int qty = ToInt(txtQty.Text);
            int avl = ToInt(_cur.AvlQty);
            if (qty > avl) { Report(MP_OVERQT, "이관수량이 재고수량보다 많습니다.", MsgLevel.Warn); FocusSel(txtQty); return; }
            if (qty <= 0) { ReportText("수량을 입력하세요.", MsgLevel.Warn); FocusSel(txtQty); return; }

            MigrateOutItem it = _cur;
            string wh = CurWh, q = qty.ToString();

            Begin("저장중...");
            Async.Run(this,
                delegate { MigrateOutService.Save(wh, it, q); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.") + "\r\n" + ex.Message, MsgLevel.Error);
                        FocusSel(txtQty);
                        return;
                    }

                    Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);

                    ListViewItem li = new ListViewItem(it.Lep);
                    li.SubItems.Add(PartNo.Display(it.Ptno));
                    li.SubItems.Add(q);
                    li.SubItems.Add(Loc.Display(it.Locno));
                    li.Tag = it.Locno;
                    lstList.Items.Add(li);

                    _scanLoc = "";
                    txtLoc.Text = "";
                    ClearCurrent();
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        private void OnSort(object sender, EventArgs e)
        {
            if (_busy || lstList.Items.Count < 2) return;

            ArrayList rows = new ArrayList();
            for (int i = 0; i < lstList.Items.Count; i++) rows.Add(lstList.Items[i]);
            rows.Sort(new LocComparer(_sortDesc));
            _sortDesc = !_sortDesc;

            lstList.BeginUpdate();
            try
            {
                lstList.Items.Clear();
                for (int i = 0; i < rows.Count; i++) lstList.Items.Add((ListViewItem)rows[i]);
            }
            finally { lstList.EndUpdate(); }
        }

        private sealed class LocComparer : IComparer
        {
            private readonly bool _desc;
            public LocComparer(bool desc) { _desc = desc; }
            public int Compare(object x, object y)
            {
                int c = string.Compare((string)((ListViewItem)x).Tag, (string)((ListViewItem)y).Tag);
                return _desc ? -c : c;
            }
        }

        // 삭제 : 처리 목록에서 행만 지운다 (원본도 저장 취소가 아님)
        private void OnDelete(object sender, EventArgs e)
        {
            if (_busy) return;
            if (lstList.SelectedIndices.Count == 0) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            if (!Confirm(CommonCache.Msg(MP_DEL, "선택한 부품을 삭제하시겠습니까?"))) return;
            lstList.Items.RemoveAt(lstList.SelectedIndices[0]);
        }

        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;
            NavArgs a = new NavArgs();
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length > 0) { a.Set("LEP", lblPrefix.Text); a.Set("PTNO", ptno); }
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "입력한 내용을 지우시겠습니까?"))) return;
            ClearAll();
            _loading = true;
            try { SelectByText(cboWh, "M"); }
            finally { _loading = false; }
            txtLoc.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void ClearCurrent()
        {
            _cur = null;
            _blocked = false;
            lblPrefix.Text = "H";
            txtPart.Text = "";
            txtQty.Text = "";
            txtOut.Text = "";
        }

        private void ClearAll()
        {
            _scanLoc = "";
            txtLoc.Text = "";
            lstList.Items.Clear();
            _sortDesc = false;
            _focus = "";
            ClearCurrent();
        }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }

        private void FocusSel(TextBox t) { t.Focus(); t.SelectAll(); }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[440] " + ScreenName,
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
            if (msg != null && msg.Length > 0) Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK && code != MP_SAVED) MessageBox.Show(text, "[440] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[440] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSort.Enabled = on;
            btnDel.Enabled = on;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
            cboWh.Enabled = on;
        }
    }
}
