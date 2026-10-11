using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [250] 피킹분류(TAG) : TAG 와 부번을 스캔해 피킹한 부품을 TAG 에 분류한다.
    // 원본 웹화면 : /ui/ws/plus/PL250_W01.xml (서버 메뉴 P125, 1B05) — 전문점 전용
    //
    //   TAG  스캔 -> PL250_W01_S03 -> ds_Output03 (USE_YN / TAG_PTNO_ICNT / TAG_PTNO_SCNT)
    //               없으면 MP576, USE_YN=N 이거나 남은 건수 0 이면 MP577. 건수 "처리/전체"
    //   부번 스캔 -> PL250_W01_S01 -> ds_Output01 (업체별 행) — 계열이 여럿이면 계열 선택
    //               수량 = 모든 행 TRS_SALQT 합
    //   저장      -> PL250_W01_U01 + U02 (+ TAG 마지막 건이면 U02 한 번 더, TAG_STAT=N)
    //               TRS_SALQT = 수량 합, TRS_CNT = 행 TRS_CNT 합
    //               처리 = 전체가 되면 화면 초기화 확인(MP503)
    //   세부      -> [604] TAG조회 (조회메뉴 미구현)
    //
    // [251] 배송분류(CASE) 의 원형(2013)이다. CASE 단계가 없다.
    // 실측 없음(테스트 계정이 대리점). 원본 소스 기준.
    //
    // 원본과 다르게 한 부분
    //   - TRS_CNT : 원본은 "record[" + i+1 + "]" 문자열 연결로 2행부터 NaN 이 된다. 모든 행 합을 보낸다(1행이면 같다).
    //   - 저장 후 원본은 부품 칸을 그대로 둔다(같은 부번을 또 저장할 수 있다). 여기서는 비우고 다음 부번을 기다린다.
    //   - 계열 팝업은 원본처럼 행 수가 아니라 서로 다른 계열이 둘 이상일 때만 띄운다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S250_TagClassify.Designer.cs 에서 관리한다.
    public sealed partial class S250_TagClassify : WorkScreen
    {
        private int _iCnt, _sCnt, _qty, _cnt;
        private string _focus = "";
        private string _foundPtno = "";

        public override int ScreenNo { get { return ScreenId.TagClassify; } }
        public override string ScreenName { get { return "피킹분류(TAG)"; } }

        public S250_TagClassify()
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
            lblQty.Text = "";
            lstVnd.Items.Clear();
            _qty = 0; _cnt = 0;
            _foundPtno = "";
        }

        private void OnTagFocus(object sender, EventArgs e) { _focus = "TAG"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (Busy) return;
            if (_focus == "TAG") { txtTag.Text = DispTag(data.Text); SearchTag(); }
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
                delegate { return OutboundService.SearchClassTag(tag); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    OutRow o = (OutRow)r;
                    if (o == null) { Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn); txtTag.Focus(); return; }
                    int ic = ToInt(o["TAG_PTNO_ICNT"]), sc = ToInt(o["TAG_PTNO_SCNT"]);
                    if (o["USE_YN"] == "N" || ic - sc == 0)
                    {
                        Report("MP577", "사용할 수 없는 TAG 입니다.", MsgLevel.Warn);
                        txtTag.Focus();
                        return;
                    }
                    _iCnt = ic;
                    _sCnt = sc;
                    lblCnt.Text = _sCnt + "/" + _iCnt;
                    End("TAG 확인. 부품을 스캔하세요.", MsgLevel.Success);
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 부번 (fn_Search)
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
            if (ptno.Length == 0) return;
            txtPart.Text = PartNo.Display(ptno);
            string dt = Ymd(dtpDate.Value), tag = TagNo;

            Run("조회중...",
                delegate { return OutboundService.SearchClassPart(ptno, dt, tag); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    ArrayList rows = (ArrayList)r;
                    lstVnd.Items.Clear();
                    _qty = 0; _cnt = 0;
                    _foundPtno = "";
                    lblQty.Text = "";
                    if (rows.Count == 0)
                    {
                        lblPrefix.Text = "H";
                        Report("MP578", "분류 대상이 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }

                    ArrayList leps = new ArrayList();
                    lstVnd.BeginUpdate();
                    try
                    {
                        for (int i = 0; i < rows.Count; i++)
                        {
                            OutRow o = (OutRow)rows[i];
                            ListViewItem it = new ListViewItem(o["TRS_VNDMN"]);
                            it.SubItems.Add(Num(o["TRS_SALQT"]));
                            it.SubItems.Add(o["TRS_VCHNO"]);
                            it.SubItems.Add(Num(o["TRS_SALQT_SUM"]));
                            lstVnd.Items.Add(it);
                            _qty += ToInt(o["TRS_SALQT"]);
                            _cnt += ToInt(o["TRS_CNT"]);
                            if (!leps.Contains(o["TRS_LEP"])) leps.Add(o["TRS_LEP"]);
                        }
                    }
                    finally { lstVnd.EndUpdate(); }

                    OutRow first = (OutRow)rows[0];
                    lblPartName.Text = first["INV_PTNM_A"];
                    lblClass.Text = first["UWMG_GRADE"];

                    // 원본 fn_LepShow : 계열을 고르게 한다 (계열만 바뀌고 수량은 전체 합 그대로)
                    string lep = (string)leps[0];
                    if (leps.Count > 1)
                    {
                        int k = HaimsPda.Controls.LepSelect.Pick(leps, "계열 선택 - " + txtPart.Text);
                        if (k >= 0) lep = (string)leps[k];
                    }
                    lblPrefix.Text = lep.Length > 0 ? lep : "H";
                    lblQty.Text = _qty.ToString();
                    _foundPtno = ptno;
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                    btnSave.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 저장 (fn_Save)
        // ------------------------------------------------------------------
        private void OnSave(object sender, EventArgs e)
        {
            if (Busy) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0 || ptno != _foundPtno)
            {
                Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (TagNo.Length == 0) { Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn); txtTag.Focus(); return; }

            string lep = lblPrefix.Text, tag = TagNo;
            int qty = _qty, cnt = _cnt;
            bool close = (_iCnt - _sCnt == 1) || _iCnt == 1;

            Run("저장중...",
                delegate { OutboundService.SaveClassTag(lep, ptno, tag, qty, cnt, close); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null) { Report("MP108", "저장에 실패하였습니다.", MsgLevel.Error); return; }
                    _sCnt += cnt;
                    lblCnt.Text = _sCnt + "/" + _iCnt;
                    Report("MP102", "정상 처리되었습니다.", MsgLevel.Success);

                    // 원본 : 처리 = 전체면 fn_Clear (MP503 확인 후 초기화)
                    if (_sCnt == _iCnt && ConfirmMsg("MP503", "화면을 지우시겠습니까?")) { Init(); return; }
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
