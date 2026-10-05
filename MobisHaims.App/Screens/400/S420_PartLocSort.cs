using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [420] 부품LOC정렬 : 입고장에서 저장 처리한 부품을 창고 셀에 넣기 위한 체크리스트.
    // 원본 웹화면 : /ui/ws/plus/PL420_W01.xml (메뉴 1D02 / P142), 매뉴얼 LOC관리 182~185p
    //
    //   [입력 단계] 부번 스캔 -> fn_Search(S01) -> 목록에 추가 (반복)
    //   [정렬]      창고, LOC 순으로 정렬
    //   [확인 단계] 부번 스캔 -> 목록에서 찾아 아래 칸에 표시 -> LOC 스캔 -> 일치하면 목록에서 삭제
    //   목록이 비면 작업 완료. 서버 저장은 없다.
    //
    // 버튼 [확인]/[입력] 은 원본 textbox247 과 같은 토글이다.
    //   라벨 "확인" = 지금 입력 단계 (누르면 확인 단계로), 라벨 "입력" = 지금 확인 단계 (누르면 입력 단계로)
    //
    // 원본과 다르게 한 부분
    //   - 같은 부번/계열을 두 번 넣지 않는다 (원본의 MP316 검사 함수 fn_SetData 는 호출되지 않는 죽은 코드).
    //   - 확인 단계에서 목록에 없는 부번, 맞지 않는 LOC 는 알려 준다 (원본은 아무 반응 없음).
    //   - 확인 단계에서 같은 부번이 계열별로 둘 이상이면 고르게 한다 (원본은 첫 행).
    //   - 정렬은 창고 -> LOC 순 (원본은 LOC 만, 매뉴얼은 "창고별, LOC별").
    //   - 매뉴얼 그림의 SKIP 버튼은 현재 원본 XML 에 없어서 넣지 않았다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S420_PartLocSort.Designer.cs 에서 관리한다.
    public sealed partial class S420_PartLocSort : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_NOPINFO = "MP302";  // 부품정보를 확인하세요
        private const string MP_NOPART = "MP303";   // 부품번호를 확인하세요
        private const string MP_NOLOC = "MP310";    // 로케이션을 확인하세요
        private const string MP_DUP = "MP316";      // 이미 불출된 품목입니다
        private const string MP_NOSTOCK = "MP333";  // 해당부품에 대한 재고정보가 없습니다
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까

        [DllImport("coredll.dll")]
        private static extern bool MessageBeep(uint type);

        private bool _busy;
        private bool _loading;
        private bool _verify;                 // false = 입력 단계, true = 확인 단계
        private string _focus = "";           // PTNO / PTNO2 / LOCNO
        private readonly ArrayList _rows = new ArrayList();   // LocSortRow
        private LocSortRow _cur;              // 확인 단계에서 고른 행

        public override int ScreenNo { get { return ScreenId.PartLocSort; } }
        public override string ScreenName { get { return "부품LOC정렬"; } }

        public S420_PartLocSort()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            WinApi.GridLines(this.lstList.Handle, true);
            WinApi.DoubleBuffering(this.lstList.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string ptno = Arg(args, "PTNO");
            if (ptno.Length > 0) txtPart.Text = PartNo.Display(ptno);

            LoadWarehouses(ptno);
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        // ------------------------------------------------------------------
        // 창고 콤보 (원본 gfn_SearchWHS_Plus)
        // ------------------------------------------------------------------
        private void LoadWarehouses(string linkPtno)
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
                    }
                    finally { _loading = false; }

                    End("부번을 스캔하세요.", MsgLevel.Info);

                    if (linkPtno.Length > 0) Search();
                    else txtPart.Focus();
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
        // 스캔 / 입력 (원본 fn_Barcode)
        // ------------------------------------------------------------------
        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            if (_busy) return;

            string target = _focus;
            if (target.Length == 0) target = _verify ? (_cur == null ? "PTNO2" : "LOCNO") : "PTNO";

            if (target == "LOCNO")
            {
                txtLocTo.Text = Loc.Display(data.Text);
                LocToEnter();
            }
            else if (target == "PTNO2")
            {
                txtPartTo.Text = PartNo.Display(data.Text);
                PartToEnter();
            }
            else
            {
                if (_verify) { Msg("확인 단계입니다. 아래 부번 칸에 스캔하세요.", MsgLevel.Warn); return; }
                txtPart.Text = PartNo.Display(data.Text);
                Search();
            }
        }

        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }
        private void OnPartToFocus(object sender, EventArgs e) { _focus = "PTNO2"; }
        private void OnLocToFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnOtherFocus(object sender, EventArgs e) { _focus = ""; }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Search();
        }

        private void OnPartToKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            PartToEnter();
        }

        private void OnLocToKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocToEnter();
        }

        // ------------------------------------------------------------------
        // 입력 단계 : 부번 조회 -> 목록 추가 (원본 fn_Search / fn_popPart)
        // ------------------------------------------------------------------
        private void Search()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            txtPart.Text = PartNo.Display(ptno);

            string wh = CurWh;

            Begin("조회중...");
            Async.Run(this,
                delegate { return LocSortService.Search(wh, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList hits = (ArrayList)r;
                    if (hits.Count == 0)
                    {
                        Report(MP_NOSTOCK, "해당 부품에 대한 재고정보가 없습니다.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }

                    // 계열이 둘 이상이면 고르게 한다 (원본 lep_popup)
                    int idx = MobisHaims.Controls.LepSelect.Pick(hits, "계열 선택 - " + PartNo.Display(ptno));
                    if (idx < 0) { End("취소했습니다.", MsgLevel.Info); FocusSel(txtPart); return; }

                    LocSortRow s = (LocSortRow)hits[idx];
                    ShowHeader(s);

                    if (FindRow(s.Lep, s.Ptno, s.Whscd) != null)
                    {
                        Report(MP_DUP, "이미 목록에 있는 품목입니다.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }

                    _rows.Add(s);
                    FillGrid();
                    End("목록 " + _rows.Count + "건. 계속 스캔하거나 [확인]을 누르세요.", MsgLevel.Success);
                    FocusSel(txtPart);
                });
        }

        private void ShowHeader(LocSortRow s)
        {
            lblPrefix.Text = s.Lep;
            lblClass.Text = s.Grade;
            lblPartName.Text = s.PartName;
            txtLoc.Text = Loc.Display(s.Locno);
        }

        private LocSortRow FindRow(string lep, string ptno, string whscd)
        {
            for (int i = 0; i < _rows.Count; i++)
            {
                LocSortRow s = (LocSortRow)_rows[i];
                if (s.Lep == lep && s.Ptno == ptno && s.Whscd == whscd) return s;
            }
            return null;
        }

        // ------------------------------------------------------------------
        // 확인 단계 : 부번 -> LOC (원본 fn_SearchListPartNo / fn_SearchListLocPartNo)
        // ------------------------------------------------------------------
        private void PartToEnter()
        {
            if (_busy) return;
            if (_rows.Count == 0) { Msg("목록이 비어 있습니다.", MsgLevel.Info); return; }

            string ptno = PartNo.Key(txtPartTo.Text);
            if (ptno.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPartTo); return; }
            txtPartTo.Text = PartNo.Display(ptno);

            ArrayList hits = new ArrayList();
            for (int i = 0; i < _rows.Count; i++)
            {
                LocSortRow s = (LocSortRow)_rows[i];
                if (s.Ptno == ptno) hits.Add(s);
            }

            if (hits.Count == 0)
            {
                MessageBeep(0x00000010);
                Report(MP_NOPINFO, "목록에 없는 부품입니다.", MsgLevel.Warn);
                FocusSel(txtPartTo);
                return;
            }

            int idx = MobisHaims.Controls.LepSelect.Pick(hits, "계열 선택 - " + PartNo.Display(ptno));
            if (idx < 0) { FocusSel(txtPartTo); return; }

            SetCurrent((LocSortRow)hits[idx]);
            txtLocTo.Text = "";
            txtLocTo.Focus();
            Msg(Loc.Display(_cur.Locno) + " 에 넣고 LOC 를 스캔하세요.", MsgLevel.Info);
        }

        private void LocToEnter()
        {
            if (_busy) return;
            if (_cur == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPartTo); return; }

            string loc = Loc.Key(txtLocTo.Text);
            if (loc.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocTo); return; }
            txtLocTo.Text = Loc.Display(loc);

            if (loc != _cur.Locno)
            {
                MessageBeep(0x00000010);
                Msg("LOC 불일치 - " + Loc.Display(_cur.Locno) + " 에 넣어야 합니다.", MsgLevel.Error);
                FocusSel(txtLocTo);
                return;
            }

            MessageBeep(0x00000000);
            _rows.Remove(_cur);
            FillGrid();
            ClearTo();

            if (_rows.Count == 0) Msg("작업이 완료되었습니다.", MsgLevel.Success);
            else Msg("완료. 남은 " + _rows.Count + "건", MsgLevel.Success);
            txtPartTo.Focus();
        }

        // 목록 행을 누르면 확인 칸에 채운다 (원본 fn_getCellClick)
        private void OnListSelected(object sender, EventArgs e)
        {
            if (_busy || lstList.SelectedIndices.Count == 0) return;
            LocSortRow s = lstList.Items[lstList.SelectedIndices[0]].Tag as LocSortRow;
            if (s == null || s == _cur) return;
            SetCurrent(s);
            txtPartTo.Text = PartNo.Display(s.Ptno);
            if (_verify) { txtLocTo.Text = ""; txtLocTo.Focus(); }
        }

        private void SetCurrent(LocSortRow s)
        {
            _cur = s;
            lblPrefixTo.Text = s.Lep;
            lblClassTo.Text = s.Grade;
            txtQty.Text = s.AvlQty;
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        // 확인 <-> 입력 (원본 OnBtnConfirm)
        private void OnConfirm(object sender, EventArgs e)
        {
            if (_busy) return;
            SetVerify(!_verify);
        }

        private void SetVerify(bool on)
        {
            _verify = on;
            btnConfirm.Text = on ? "입력" : "확인";
            txtPart.Enabled = !on;
            txtPartTo.Enabled = on;
            txtLocTo.Enabled = on;

            if (on)
            {
                ClearTo();
                txtPartTo.Focus();
                Msg("확인 단계 - 부번을 스캔하세요.", MsgLevel.Info);
            }
            else
            {
                FocusSel(txtPart);
                Msg("입력 단계 - 부번을 스캔하세요.", MsgLevel.Info);
            }
        }

        // 정렬 : 창고 -> LOC (원본 OnBtnSort)
        private void OnSort(object sender, EventArgs e)
        {
            if (_busy || _rows.Count == 0) return;
            _rows.Sort(new LocComparer());
            FillGrid();
            Msg("창고/LOC 순으로 정렬했습니다.", MsgLevel.Info);
        }

        private sealed class LocComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                LocSortRow a = (LocSortRow)x, b = (LocSortRow)y;
                int c = string.Compare(a.Whscd, b.Whscd);
                if (c != 0) return c;
                c = string.Compare(a.Locno, b.Locno);
                if (c != 0) return c;
                return string.Compare(a.Ptno, b.Ptno);
            }
        }

        // 재고 : [321] 파트별재고 (원본 OnBtnPart)
        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;

            NavArgs a = new NavArgs();
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length > 0)
            {
                a.Set("LEP", lblPrefix.Text);
                a.Set("PTNO", ptno);
                a.Set("PTNM", lblPartName.Text);
                a.Set("CLASS", lblClass.Text);
            }
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
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void FillGrid()
        {
            lstList.BeginUpdate();
            try
            {
                lstList.Items.Clear();
                for (int n = 0; n < _rows.Count; n++)
                {
                    LocSortRow s = (LocSortRow)_rows[n];
                    ListViewItem li = new ListViewItem(s.Lep);
                    li.SubItems.Add(PartNo.Display(s.Ptno));
                    li.SubItems.Add(s.Whscd);
                    li.SubItems.Add(s.AvlQty);
                    li.SubItems.Add(Loc.Display(s.Locno));
                    li.Tag = s;
                    lstList.Items.Add(li);
                }
            }
            finally { lstList.EndUpdate(); }
        }

        private void ClearTo()
        {
            _cur = null;
            lblPrefixTo.Text = "H";
            txtPartTo.Text = "";
            lblClassTo.Text = "";
            txtLocTo.Text = "";
            txtQty.Text = "";
        }

        private void ClearAll()
        {
            _rows.Clear();
            lstList.Items.Clear();
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            txtLoc.Text = "";
            ClearTo();
            _focus = "";
            _verify = true;      // SetVerify(false) 가 입력 단계로 맞춘다
            SetVerify(false);
        }

        private void FocusSel(TextBox t)
        {
            if (!t.Enabled) return;
            t.Focus();
            t.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[420] " + ScreenName,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2) == DialogResult.Yes;
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
            if (code != MP_OK) MessageBox.Show(text, "[420] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[420] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnConfirm.Enabled = on;
            btnSort.Enabled = on;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
            cboWh.Enabled = on;
        }
    }
}
