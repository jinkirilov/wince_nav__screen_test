using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [430] 실시간창고이전(출고) : 신축/새로 임차한 창고로 옮기기 위해 구창고 LOC 에서 부품을 출고한다.
    // 원본 웹화면 : /ui/ws/plus/PL430_W01.xml (메뉴 1D03 / P143), 매뉴얼 LOC관리 186~188p
    //
    //   창고 / 수불등급([선택] 버튼으로 순환) / 시작지역(ZONE 3 + AISLE 2 = 5자리) 입력
    //   -> fn_Search(S01) 가 "가장 빠른 LOC 의 한 건" 을 지시
    //   -> LOC 스캔(일치 확인) -> 부번 스캔(일치 확인) -> 대상수량 Enter -> 저장(I01 + U01)
    //   -> 처리 목록에 추가하고 다음 건 지시 (NEXT_PTNO=Y)
    //
    // 원본과 다르게 한 부분
    //   - LOC 와 부번을 둘 다 맞게 스캔해야 저장한다 (원본은 수량 Enter 만으로 저장 가능).
    //   - 대상수량은 1 ~ 가능수량 범위만 허용한다.
    //   - SKIP : 원본은 fn_Search 를 다시 불러서 첫 저장 전에는 같은 건이 다시 나온다(m_bNext=false).
    //            여기서는 NEXT_PTNO=Y 에 현재 건의 LOC/부번을 넘겨 다음 건을 요청한다.
    //   - 저장 후 다음 건 요청은 원본 그대로 LOCNO 에 시작지역(5자리), PTNO 에 방금 저장한 부번을 넘긴다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S430_TransferOut.Designer.cs 에서 관리한다.
    public sealed partial class S430_TransferOut : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";    // 정상 저장되었습니다
        private const string MP_SAVEERR = "MP108";  // 저장중 에러가 발생하였습니다
        private const string MP_NOPART = "MP303";   // 부품번호를 확인하세요
        private const string MP_BADLOC = "MP311";   // 정확한 로케이션 코드가 아닙니다 / LOC 를 확인하세요
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까
        private const string MP_SKIP = "MP509";     // SKIP 하시겠습니까
        private const string MP_DEL = "MP512";      // 삭제하시겠습니까
        private const string MP_NOSEL = "MP545";    // 선택된 데이터가 없습니다
        private const string MP_SALQT = "MP583";    // 출고대기 수량 존재. 이관 출고 불가
        private const string MP_START = "MP623";    // 시작지역 형식 오류

        private bool _busy;
        private string _focus = "";              // START / LOCNO / PTNO
        private ArrayList _grades = new ArrayList();   // CodeItem (MP/03)
        private int _gradeIdx;
        private string _startKey = "";           // 조회에 쓴 시작지역 5자리
        private TransferOutItem _cur;            // 현재 지시 건
        private bool _blocked;                   // 출고대기가 있어 처리 불가 (SKIP 만 가능)
        private bool _locOk, _partOk;
        private bool _sortDesc;

        public override int ScreenNo { get { return ScreenId.WhTransferOut; } }
        public override string ScreenName { get { return "실시간창고이전(출고)"; } }

        public S430_TransferOut()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            WinApi.GridLines(this.lstList.Handle, true);
            WinApi.DoubleBuffering(this.lstList.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadCombos(Arg(args, "WHSCD"), Arg(args, "LOCNO"), Arg(args, "DETAIL_INFO"));
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        // ------------------------------------------------------------------
        // 창고 / 수불등급 (원본 gfn_SearchWHS_Plus / gfn_SearchComCode MP03)
        // ------------------------------------------------------------------
        private void LoadCombos(string linkWh, string linkStart, string linkGradeIdx)
        {
            Begin("조회중...");
            Async.Run(this,
                delegate
                {
                    ArrayList[] r = new ArrayList[2];
                    r[0] = LocStockService.GetWarehouses();
                    r[1] = TransferOutService.GetGrades();
                    return r;
                },
                delegate(object o, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList[] r = (ArrayList[])o;
                    cboWh.Items.Clear();
                    for (int i = 0; i < r[0].Count; i++) cboWh.Items.Add(r[0][i]);
                    if (cboWh.Items.Count > 0) cboWh.SelectedIndex = 0;
                    SelectByText(cboWh, "M");
                    SelectByText(cboWh, linkWh);

                    _grades = r[1];
                    _gradeIdx = 0;
                    int gi = ToInt(linkGradeIdx);
                    if (gi > 0 && gi < _grades.Count) _gradeIdx = gi;
                    ShowGrade();

                    End("시작지역(5자리)을 입력하세요.", MsgLevel.Info);

                    string key = Loc.Key(linkStart);
                    if (key.Length >= 5)
                    {
                        txtStart.Text = StartDisplay(key.Substring(0, 5));
                        StartEnter();
                    }
                    else
                        txtStart.Focus();
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

        private string CurGrade
        {
            get { return (_gradeIdx < 0 || _gradeIdx >= _grades.Count) ? "" : ((CodeItem)_grades[_gradeIdx]).Code; }
        }

        private void ShowGrade()
        {
            lblGrade.Text = (_gradeIdx < 0 || _gradeIdx >= _grades.Count) ? "" : ((CodeItem)_grades[_gradeIdx]).Name;
        }

        private static string StartDisplay(string key5)
        {
            return key5.Substring(0, 3) + "-" + key5.Substring(3, 2);
        }

        // ------------------------------------------------------------------
        // 스캔 / 입력 (원본 fn_Barcode)
        // ------------------------------------------------------------------
        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            if (_busy) return;

            string target = _focus;
            if (target.Length == 0)
                target = (_cur == null) ? "START" : (!_locOk ? "LOCNO" : "PTNO");

            if (target == "START")
            {
                txtStart.Text = data.Text;
                StartEnter();
            }
            else if (target == "LOCNO")
            {
                txtLoc.Text = Loc.Display(data.Text);
                LocEnter();
            }
            else if (target == "PTNO")
            {
                txtPart.Text = PartNo.Display(data.Text);
                PartEnter();
            }
        }

        private void OnStartFocus(object sender, EventArgs e) { _focus = "START"; }
        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }
        private void OnOtherFocus(object sender, EventArgs e) { _focus = ""; }

        private void OnStartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            StartEnter();
        }

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
        // 시작지역 (원본 inptStartLoc onkeydown : 5자리만)
        // ------------------------------------------------------------------
        private void StartEnter()
        {
            if (_busy) return;

            string key = Loc.Key(txtStart.Text);
            if (key.Length != 5)
            {
                Report(MP_START, "시작지역은 5자리(ZONE 3 + AISLE 2)로 입력하세요.", MsgLevel.Warn);
                FocusSel(txtStart);
                return;
            }

            txtStart.Text = StartDisplay(key);
            _startKey = key;
            Search(false, "", "", "");
        }

        // ------------------------------------------------------------------
        // 지시 조회 (원본 fn_Search / fn_Search_After)
        // ------------------------------------------------------------------
        private void Search(bool next, string lep, string afterLoc, string afterPtno)
        {
            if (_startKey.Length != 5) { FocusSel(txtStart); return; }

            string wh = CurWh, start = _startKey, grade = CurGrade;

            Begin("조회중...");
            Async.Run(this,
                delegate { return TransferOutService.Search(wh, start, grade, next, lep, afterLoc, afterPtno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    TransferOutItem it = (TransferOutItem)r;
                    ClearCurrent();

                    if (!it.Found)
                    {
                        Report(MP_BADLOC, "출고할 품목이 없습니다.", MsgLevel.Warn);
                        FocusSel(txtStart);
                        return;
                    }

                    _cur = it;
                    txtLoc.Text = Loc.Display(it.Locno);
                    lblPrefix.Text = it.Lep;
                    txtPart.Text = PartNo.Display(it.Ptno);
                    txtQty.Text = it.AvlQty;
                    txtOut.Text = it.OutQty;

                    if (ToInt(it.OutQty) > 0)
                    {
                        // 원본 : 부번/수량을 지우고 MP583. 이 건은 SKIP 해야 한다
                        _blocked = true;
                        txtPart.Text = "";
                        txtQty.Text = "";
                        Report(MP_SALQT, "출고대기 수량이 있어 출고할 수 없습니다. SKIP 하세요.", MsgLevel.Warn);
                        return;
                    }

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다.") + " LOC 를 스캔하세요.", MsgLevel.Success);
                    FocusSel(txtLoc);
                });
        }

        // 원본 fn_ConfirmLoc
        private void LocEnter()
        {
            if (_busy || _cur == null) return;
            if (_blocked) { Msg("출고대기가 있는 품목입니다. SKIP 하세요.", MsgLevel.Warn); return; }

            string loc = Loc.Key(txtLoc.Text);
            txtLoc.Text = Loc.Display(loc);

            if (loc != _cur.Locno)
            {
                _locOk = false;
                Report(MP_BADLOC, "LOC 를 확인하세요.", MsgLevel.Warn);
                FocusSel(txtLoc);
                return;
            }

            _locOk = true;
            Msg("LOC 확인. 부번을 스캔하세요.", MsgLevel.Info);
            FocusSel(txtPart);
        }

        // 원본 fn_ConfirmPart
        private void PartEnter()
        {
            if (_busy || _cur == null) return;
            if (_blocked) { Msg("출고대기가 있는 품목입니다. SKIP 하세요.", MsgLevel.Warn); return; }

            string ptno = PartNo.Key(txtPart.Text);
            txtPart.Text = PartNo.Display(ptno);

            if (ptno != _cur.Ptno)
            {
                _partOk = false;
                Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn);
                FocusSel(txtPart);
                return;
            }

            _partOk = true;
            txtQty.Text = _cur.AvlQty;
            txtOut.Text = _cur.OutQty;
            Msg("수량을 확인하고 Enter 를 누르세요.", MsgLevel.Info);
            FocusSel(txtQty);
        }

        // ------------------------------------------------------------------
        // 저장 (원본 fn_Save / fn_Save_After / fn_SetData)
        // ------------------------------------------------------------------
        private void Save()
        {
            if (_busy || _cur == null) return;
            if (_blocked) { Msg("출고대기가 있는 품목입니다. SKIP 하세요.", MsgLevel.Warn); return; }
            if (!_locOk) { Msg("LOC 를 먼저 스캔하세요.", MsgLevel.Warn); FocusSel(txtLoc); return; }
            if (!_partOk) { Msg("부번을 먼저 스캔하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            int qty = ToInt(txtQty.Text);
            int avl = ToInt(_cur.AvlQty);
            if (qty <= 0 || qty > avl)
            {
                ReportText("수량은 1 ~ " + avl + " 사이로 입력하세요.", MsgLevel.Warn);
                FocusSel(txtQty);
                return;
            }

            TransferOutItem it = _cur;
            string wh = CurWh, q = qty.ToString();

            Begin("저장중...");
            Async.Run(this,
                delegate { TransferOutService.Save(wh, it, q); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.") + "\r\n" + ex.Message, MsgLevel.Error);
                        FocusSel(txtQty);
                        return;
                    }

                    Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);
                    AddDone(it, q);

                    // 원본 : NEXT_PTNO=Y, LOCNO = 시작지역, PTNO = 방금 저장한 부번
                    Search(true, it.Lep, _startKey, it.Ptno);
                });
        }

        private void AddDone(TransferOutItem it, string qty)
        {
            ListViewItem li = new ListViewItem(it.Lep);
            li.SubItems.Add(PartNo.Display(it.Ptno));
            li.SubItems.Add(qty);
            li.SubItems.Add(Loc.Display(it.Locno));
            li.Tag = it.Locno;
            lstList.Items.Add(li);
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        // 수불등급 순환 (원본 OnBtnSelect)
        private void OnGrade(object sender, EventArgs e)
        {
            if (_busy || _grades.Count == 0) return;
            _gradeIdx = (_gradeIdx + 1) % _grades.Count;
            ShowGrade();
            if (_startKey.Length == 5) Search(false, "", "", "");
        }

        // SKIP : 현재 건 다음 지시 (원본 OnBtnSkip)
        private void OnSkip(object sender, EventArgs e)
        {
            if (_busy) return;
            if (_startKey.Length != 5) { FocusSel(txtStart); return; }
            if (!Confirm(CommonCache.Msg(MP_SKIP, "SKIP 하시겠습니까?"))) return;

            if (_cur == null) Search(false, "", "", "");
            else Search(true, _cur.Lep, _cur.Locno, _cur.Ptno);
        }

        // 정렬 : LOC 오름/내림 토글 (원본 OnBtnAlign)
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
                string a = (string)((ListViewItem)x).Tag, b = (string)((ListViewItem)y).Tag;
                int c = string.Compare(a, b);
                return _desc ? -c : c;
            }
        }

        // 삭제 : 처리 목록에서 행만 지운다 (원본 OnBtnDel - 저장 취소가 아님)
        private void OnDelete(object sender, EventArgs e)
        {
            if (_busy) return;
            if (lstList.SelectedIndices.Count == 0) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }
            if (!Confirm(CommonCache.Msg(MP_DEL, "선택한 부품을 삭제하시겠습니까?"))) return;
            lstList.Items.RemoveAt(lstList.SelectedIndices[0]);
        }

        // 재고 : [321] 파트별재고 (원본 OnBtnStock)
        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;

            NavArgs a = new NavArgs();
            if (_cur != null)
            {
                a.Set("LEP", _cur.Lep);
                a.Set("PTNO", _cur.Ptno);
            }
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "입력한 내용을 지우시겠습니까?"))) return;
            ClearAll();
            SelectByText(cboWh, "M");
            _gradeIdx = 0;
            ShowGrade();
            txtStart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void ClearCurrent()
        {
            _cur = null;
            _blocked = false;
            _locOk = false;
            _partOk = false;
            txtLoc.Text = "";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            txtQty.Text = "";
            txtOut.Text = "";
        }

        private void ClearAll()
        {
            _startKey = "";
            txtStart.Text = "";
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

        private void FocusSel(TextBox t)
        {
            t.Focus();
            t.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[430] " + ScreenName,
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

        /// <summary>MP101/MP102 가 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK && code != MP_SAVED) MessageBox.Show(text, "[430] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[430] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnGrade.Enabled = on;
            btnSkip.Enabled = on;
            btnSort.Enabled = on;
            btnDel.Enabled = on;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
            cboWh.Enabled = on;
        }
    }
}
