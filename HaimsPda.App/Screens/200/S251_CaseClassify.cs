using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [251] 배송분류(CASE) : TAG 와 부번을 스캔해 업체별로 CASE 에 담는다.
    // 원본 웹화면 : /ui/ws/plus/PL251_W01.xml (서버 메뉴 P198, 2025.01 신규, XML title 은 "배송분류(TAG)")
    //
    //   TAG  스캔 -> PL251_W01_S03 -> ds_Output03 (USE_YN / TAG_PTNO_ICNT / TAG_PTNO_SCNT) -> 건수 "처리/전체"
    //   부번 스캔 -> PL251_W01_S01 -> ds_Output01 (업체별 행) — 계열이 여럿이면 계열 선택
    //   수량 Enter -> PL251_W01_S05 -> 업체의 다음 CASE 번호를 채운다
    //   CASE Enter -> PL251_W01_S06 -> CASE 검증 (없으면 MP641, USE_YN=N 이면 MP577)
    //   저장      -> CASE 검증 후 PL251_W01_U01_PROC + I01_PROC (+ TAG 마지막 건이면 U02_PROC, TAG_STAT=N)
    //   세부      -> [604] TAG조회 (조회메뉴는 아직 미구현)
    //
    // 실측 없음(2026-09-19 라이브에서 조회 미발생). 저장 전문은 원본 소스 기준.
    //
    // 원본과 다르게 한 부분
    //   - TRS_SALQT 를 두 번 넣는데 두 번째(m_nCntCust)가 남는다. 원본은 TRS_CNT 합을 "record[" + i+1 + "]"
    //     (문자열 연결)로 읽어 2행 이상이면 NaN 이 된다. 여기서는 고른 업체 행의 TRS_CNT 를 보낸다(1행이면 원본과 같다).
    //   - 계열 팝업에서 계열을 골라도 원본은 첫 행 수량을 쓴다. 여기서는 고른 계열의 첫 행을 잡는다.
    //   - CSD_USE_YMDTMS 는 원본이 서버 시각을 쓴다. 단말 시각으로 보낸다.
    //   - TAG_PTNO_CNT=1 확인창(MP556)은 원본이 저장 요청과 동시에 띄운다. 여기서는 저장 성공 후 띄운다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S251_CaseClassify.Designer.cs 에서 관리한다.
    public sealed partial class S251_CaseClassify : WorkScreen
    {
        private int _iCnt, _sCnt;
        private ArrayList _rows = new ArrayList();
        private string _focus = "";

        public override int ScreenNo { get { return ScreenId.CaseClassify; } }
        public override string ScreenName { get { return "배송분류(CASE)"; } }

        public S251_CaseClassify()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstVnd.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            dtpDate.Value = DateTime.Today;
            Init();
        }

        /// <summary>원본 fn_Init</summary>
        private void Init()
        {
            txtTag.Text = "";
            lblCnt.Text = "";
            _iCnt = 0; _sCnt = 0;
            ClearPart();
            Msg("TAG 를 스캔하세요.", MsgLevel.Info);
            txtTag.Focus();
        }

        private void ClearPart()
        {
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            txtQty.Text = "";
            txtCase.Text = "";
            _rows = new ArrayList();
            lstVnd.Items.Clear();
        }

        private void OnTagFocus(object sender, EventArgs e) { _focus = "TAG"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }
        private void OnCaseFocus(object sender, EventArgs e) { _focus = "CASE"; }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (Busy) return;
            if (_focus == "TAG") { txtTag.Text = DispTag(data.Text); SearchTag(); }
            else if (_focus == "CASE") { txtCase.Text = data.Text.Trim().ToUpper(); CheckCase(false); }
            else { txtPart.Text = PartNo.Display(data.Text); SearchPart(); }
        }

        /// <summary>plus.js gfn_DispTag_Plus : 16자리면 6-4-1-나머지</summary>
        private static string DispTag(string v)
        {
            v = (v == null) ? "" : v.Trim().Replace("-", "");
            if (v.Length == 16) v = v.Substring(0, 6) + "-" + v.Substring(6, 4) + "-" + v.Substring(10, 1) + "-" + v.Substring(11);
            return v.ToUpper();
        }

        private string TagNo { get { return txtTag.Text.Trim(); } }

        // ------------------------------------------------------------------
        // TAG (fn_Search_Tag)
        // ------------------------------------------------------------------
        private void OnTagKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            txtTag.Text = DispTag(txtTag.Text);
            SearchTag();
        }

        private void SearchTag()
        {
            if (Busy) return;
            string tag = TagNo;
            if (tag.Length == 0) { Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn); return; }
            Run("조회중...",
                delegate { return OutboundService.SearchTag(tag); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    OutRow o = (OutRow)r;
                    if (o == null) { Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn); txtTag.Focus(); return; }
                    if (o["USE_YN"] == "N") { Report("MP577", "사용할 수 없는 TAG 입니다.", MsgLevel.Warn); txtTag.Focus(); return; }
                    _iCnt = ToInt(o["TAG_PTNO_ICNT"]);
                    _sCnt = ToInt(o["TAG_PTNO_SCNT"]);
                    lblCnt.Text = _sCnt + "/" + _iCnt;
                    End("TAG 확인. 부품을 스캔하세요.", MsgLevel.Success);
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 부번 (fn_SearchPtno)
        // ------------------------------------------------------------------
        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            SearchPart();
        }

        private void SearchPart()
        {
            if (Busy) return;
            string ptno = PartNo.Key(txtPart.Text);
            txtQty.Text = "";
            txtCase.Text = "";
            if (ptno.Length == 0) return;
            txtPart.Text = PartNo.Display(ptno);
            string dt = Ymd(dtpDate.Value), tag = TagNo;

            Run("조회중...",
                delegate { return OutboundService.SearchTagPart(ptno, dt, tag); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _rows = (ArrayList)r;
                    lstVnd.Items.Clear();
                    if (_rows.Count == 0)
                    {
                        lblPrefix.Text = "H";
                        Report("MP578", "분류 대상이 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }
                    lstVnd.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < _rows.Count; i++)
                        {
                            OutRow o = (OutRow)_rows[i];
                            ListViewItem it = new ListViewItem(o["TRS_VNDMN"]);
                            it.SubItems.Add(Num(o["TRS_SALQT"]));
                            it.SubItems.Add(o["TRS_VCHNO"]);
                            it.SubItems.Add(Num(o["TRS_SALQT_SUM"]));
                            lstVnd.Items.Add(it);
                        }
                    }
                    finally { lstVnd.EndUpdate(); }

                    OutRow first = (OutRow)_rows[0];
                    lblPartName.Text = first["INV_PTNM_A"];
                    lblClass.Text = first["UWMG_GRADE"];

                    // 계열이 둘 이상이면 고르게 한다 (원본 lep_popup)
                    ArrayList leps = new ArrayList();
                    for (int i = 0; i < _rows.Count; i++)
                    {
                        string l = ((OutRow)_rows[i])["TRS_LEP"];
                        if (!leps.Contains(l)) leps.Add(l);
                    }
                    int sel = 0;
                    if (leps.Count > 1)
                    {
                        int k = HaimsPda.Controls.LepSelect.Pick(leps, "계열 선택 - " + txtPart.Text);
                        if (k >= 0)
                            for (int i = 0; i < _rows.Count; i++)
                                if (((OutRow)_rows[i])["TRS_LEP"] == (string)leps[k]) { sel = i; break; }
                    }
                    SelectRow(sel);
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                    txtQty.Focus();
                    txtQty.SelectAll();
                });
        }

        private void SelectRow(int i)
        {
            if (i < 0 || i >= lstVnd.Items.Count) return;
            lstVnd.Items[i].Selected = true;
            lstVnd.EnsureVisible(i);
            ShowRow(i);
        }

        private void OnRowSelected(object sender, EventArgs e)
        {
            if (lstVnd.SelectedIndices.Count == 0) return;
            ShowRow(lstVnd.SelectedIndices[0]);
        }

        private void ShowRow(int i)
        {
            OutRow o = (OutRow)_rows[i];
            lblPrefix.Text = o["TRS_LEP"].Length > 0 ? o["TRS_LEP"] : "H";
            txtQty.Text = Num(o["TRS_SALQT"]);
            txtCase.Text = "";
        }

        private OutRow Selected
        {
            get
            {
                if (lstVnd.SelectedIndices.Count == 0) return _rows.Count > 0 ? (OutRow)_rows[0] : null;
                return (OutRow)_rows[lstVnd.SelectedIndices[0]];
            }
        }

        // ------------------------------------------------------------------
        // 수량 Enter -> 다음 CASE 번호 (fn_Search_CaseNo)
        // ------------------------------------------------------------------
        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            if (Busy) return;
            if (PartNo.Key(txtPart.Text).Length == 0) { Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn); return; }
            OutRow o = Selected;
            if (o == null) return;
            string vnd = o["TRS_VNDMN"];
            Run("CASE 조회중...",
                delegate { return OutboundService.SearchCaseNo(vnd); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    txtCase.Text = (string)r;
                    End("CASE 를 확인(스캔)하세요.", MsgLevel.Info);
                    txtCase.Focus();
                    txtCase.SelectAll();
                });
        }

        // ------------------------------------------------------------------
        // CASE 검증 (fn_Search_Case) / 저장 (fn_Save)
        // ------------------------------------------------------------------
        private void OnCaseKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            CheckCase(false);
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (Busy) return;
            if (PartNo.Key(txtPart.Text).Length == 0) { Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn); return; }
            CheckCase(true);
        }

        private void CheckCase(bool save)
        {
            if (Busy) return;
            string cas = txtCase.Text.Trim();
            OutRow o = Selected;
            if (cas.Length == 0 || o == null)
            {
                if (save) ReportText("CASE 번호를 입력하세요.", MsgLevel.Warn);
                return;
            }
            string vnd = o["TRS_VNDMN"], tag = TagNo;
            Run("CASE 확인중...",
                delegate { return OutboundService.SearchCase(vnd, tag, cas); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    OutRow c = (OutRow)r;
                    if (c == null) { Report("MP641", "CASE 번호를 확인하세요.", MsgLevel.Warn); txtCase.Focus(); return; }
                    if (c["USE_YN"] == "N") { Report("MP577", "사용할 수 없는 CASE 입니다.", MsgLevel.Warn); txtCase.Focus(); return; }
                    if (!save) { End("CASE 확인. [저장] 하세요.", MsgLevel.Success); return; }
                    End("", MsgLevel.Info);
                    Save(o, cas, c["TAG_PTNO_CNT"] == "1");
                });
        }

        private void Save(OutRow o, string cas, bool lastOfTag)
        {
            OutboundService.CaseSave s = new OutboundService.CaseSave();
            s.Lep = lblPrefix.Text;
            s.Ptno = txtPart.Text;
            s.Tag = TagNo;
            s.Vndmn = o["TRS_VNDMN"];
            s.SalQt = o["TRS_SALQT"];
            s.Cnt = o["TRS_CNT"];
            s.CaseNo = cas;
            s.CloseTag = (_iCnt - _sCnt == 1) || _iCnt == 1;
            int cnt = ToInt(o["TRS_CNT"]);
            int rows = _rows.Count;

            Run("저장중...",
                delegate { OutboundService.SaveCase(s); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null) { Report("MP108", "저장에 실패하였습니다.", MsgLevel.Error); return; }
                    _sCnt += cnt;
                    lblCnt.Text = _sCnt + "/" + _iCnt;
                    Report("MP102", "정상 처리되었습니다.", MsgLevel.Success);

                    if (lastOfTag && ConfirmMsg("MP556", "분류가 완료되었습니다. 화면을 초기화하시겠습니까?"))
                    {
                        Init();
                        return;
                    }
                    if (rows > 1) { SearchPart(); return; }
                    ClearPart();
                    Msg("다음 부품을 스캔하세요.", MsgLevel.Info);
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        private void OnDetail(object sender, EventArgs e)
        {
            if (TagNo.Length == 0) { Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn); return; }
            ReportText("[604] TAG조회 화면은 아직 만들지 않았습니다.", MsgLevel.Warn);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!ConfirmMsg("MP503", "화면을 지우시겠습니까?")) return;
            Init();
        }

        protected override void SetButtons(bool on)
        {
            btnSave.Enabled = on;
            btnDetail.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
