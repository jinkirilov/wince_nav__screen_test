using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [140] 입고저장 : 부번 조회 -> LOC/수량 입력 -> 저장
    // 원본 웹화면 : /ui/ws/plus/PL140_W01.xml
    //
    // 조회 흐름은 원본과 같다.
    //   fn_SearchLep(S01) -> fn_SearchClass(PL100_W01_S02) -> fn_Search(S02/S03/S05)
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S140_InboundSave.Designer.cs 에서 관리한다.
    public sealed partial class S140_InboundSave : ScreenBase
    {
        // 서버 코드 테이블(ds_Message)에서 가져오는 메시지. 없으면 뒤의 기본문구를 쓴다.
        private const string MP_OK = "MP101";     // 정상 조회되었습니다
        private const string MP_PTNO = "MP303";   // 부품번호를 확인하세요
        private const string MP_NOLEP = "MP540";  // 할당정보가 없는 부품번호입니다(계열)
        private const string MP_NOALLOC = "MP532";// 할당정보가 없는 부품번호입니다

        private string _lep = "H";
        private InboundSearchResult _search;
        private string _searchedPtno = "";   // 원본 GV_SelectSave
        private string _vchym = "";
        private string _vapSysdt = "";
        private string _vapSysdtL = "";
        private bool _busy;

        public override int ScreenNo { get { return ScreenId.InboundSave; } }
        public override string ScreenName { get { return "입고저장"; } }

        public S140_InboundSave()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadWarehouse();
            txtPart.Focus();
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
        }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            // 포커스 위치에 따라 부번 / LOC 로 분기 (웹의 GV_FocusGbn 대응)
            if (txtLoc.Focused) txtLoc.Text = Loc.Display(data.Text);
            else { txtPart.Text = PartNo.Display(data.Text); SearchPart(); }
        }


        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchPart(); }
        }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            txtLoc.Text = Loc.Display(txtLoc.Text);   // 하이픈 넣어 정규화
            txtQty.Focus();
            txtQty.SelectAll();
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; Save(); }
        }

        private void OnSearch(object sender, EventArgs e) { SearchPart(); }
        private void OnSave(object sender, EventArgs e) { Save(); }

        private void OnWhChanged(object sender, EventArgs e)
        {
            if (_busy) return;
            if (txtPart.Text.Trim().Length > 0) SearchPart();
        }

        private void OnAssignList(object sender, EventArgs e)
        {
            if (_search == null || !_search.HasAlloc) { Msg("먼저 부번을 조회하세요.", MsgLevel.Warn); return; }
            Msg("할당내역 " + _search.Allocs.Count + "건 (목록화면 준비중)", MsgLevel.Info);
        }

        // 원본 fn_OnBtnStock : 부번을 들고 [321] 파트별재고로 이동한다 (gfn_GoToMenu 'S','1C03')
        private void OnStock(object sender, EventArgs e)
        {
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) 
            { 
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn); 
                return; 
            }

            NavArgs a = new NavArgs();
            a.Set("LEP", _lep);
            a.Set("PTNO", ptno);
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        /// <summary>
        /// 재고 등록. 원본은 버튼이 아니라 등급조회(fn_SearchClass)에 결과가 없을 때
        /// fn_SaveInv 로 자동 호출된다. 쓰기 호출이므로 버튼에 걸지 않는다.
        /// </summary>
        private void SaveInv(string ptno)
        {
            Begin("재고 등록중...");
            Async.Run(this,
                delegate { InboundService.SaveInv(_lep, ptno); return null; },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    Report(MP_OK, "재고가 등록되었습니다.", MsgLevel.Success);
                });
        }

        private void OnLoc(object sender, EventArgs e) 
        { 
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                return;
            }


            Msg(" (fn_Loc) 준비중", MsgLevel.Info); 
        }

        private void OnNotRecv(object sender, EventArgs e) 
        { 
            Msg("미수령 (fn_MoveMisuryung) 준비중", MsgLevel.Info); 
        }

        private void OnClear(object sender, EventArgs e)
        {
            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 창고 콤보 (plus:PL000_W01_S01)
        // ------------------------------------------------------------------
        private void LoadWarehouse()
        {
            Cursor.Current = Cursors.WaitCursor;
            Async.Run(this,
                delegate { return InboundService.GetWarehouses(); },
                delegate(object r, Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    if (ex != null) { Msg("창고 조회 실패 : " + ex.Message, MsgLevel.Error); return; }

                    ArrayList list = (ArrayList)r;
                    cboWh.Items.Clear();
                    for (int i = 0; i < list.Count; i++) cboWh.Items.Add(list[i]);
                    if (cboWh.Items.Count == 0) cboWh.Items.Add("M");
                    cboWh.SelectedIndex = 0;
                });
        }

        // ------------------------------------------------------------------
        // 1단계 : 부번 -> 계열(LEP)  (fn_SearchLep / PL140_W01_S01)
        // ------------------------------------------------------------------
        private void SearchPart()
        {
            if (_busy) return;
            txtPart.Text = txtPart.Text.ToUpper();
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            txtPart.Text = PartNo.Display(txtPart.Text);
            ClearResult();

            Begin("조회중...");
            Async.Run(this,
                delegate { return InboundService.SearchLep(ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;
                    if (leps.Count == 0)
                    {
                        _lep = "H";
                        lblPrefix.Text = "H";
                        Report(MP_NOLEP, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
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

                    SearchClass(ptno);
                });
        }

        // ------------------------------------------------------------------
        // 2단계 : 등급 / 품명  (fn_SearchClass / PL100_W01_S02)
        // ------------------------------------------------------------------
        private void SearchClass(string ptno)
        {
            Async.Run(this,
                delegate { return InboundService.SearchClass(_lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassInfo info = (ClassInfo)r;
                    if (!info.Found)
                    {
                        // 원본과 동일하게 재고 등록(I02)으로 넘어간다.
                        ReportText("재고정보가 없어 재고를 등록합니다.", MsgLevel.Warn);
                        SaveInv(ptno);
                        return;
                    }

                    lblClass.Text = info.Grade;
                    lblPartName.Text = info.PartName;
                    _vapSysdt = info.VapSysdt;
                    _vapSysdtL = info.VapSysdtL;
                    _vchym = info.Vchym;

                    SearchDetail(ptno);
                });
        }

        // ------------------------------------------------------------------
        // 3단계 : 화면 정보  (fn_Search / PL140_W01_S02, S03, S05)
        // ------------------------------------------------------------------
        private void SearchDetail(string ptno)
        {
            string whscd = CurrentWh();

            Async.Run(this,
                delegate { return InboundService.Search(_lep, ptno, whscd); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    InboundSearchResult sr = (InboundSearchResult)r;
                    _search = sr;
                    _searchedPtno = ptno;

                    if (!sr.HasAlloc)
                    {
                        Report(MP_NOALLOC, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }

                    Row a = sr.FirstAlloc;
                    if (_vchym.Length == 0 && a["WSF_SYSDT"].Length >= 6)
                        _vchym = a["WSF_SYSDT"].Substring(0, 6);

                    txtLoc.Text = Loc.Display(a["LOC_LOCNO"]);
                    txtNoStock.Text = sr.MinusQty;

                    txtAssignCnt.Text = sr.Screen_("WSF_CNT");
                    txtCurStock.Text = sr.Screen_("CURINV");
                    txtSched.Text = sr.Screen_("WSF_SNDDT_X");
                    txtReserve.Text = sr.Screen_("RSVQT");
                    txtAssignQty.Text = sr.Screen_("WSF_WSFQT");
                    txtQty.Text = sr.Screen_("WSF_WSFQT");

                    Report(MP_OK, "정상 조회되었습니다.", MsgLevel.Success);
                    txtQty.Focus();
                    txtQty.SelectAll();
                });
        }

        // ------------------------------------------------------------------
        // 저장 전 검증  (fn_SaveChk / PL140_W01_S07, S08)
        // ------------------------------------------------------------------
        private void Save()
        {
            if (_busy) return;

            if (_search == null || !_search.HasAlloc)
            {
                ReportText("재조회 후 입고하세요.", MsgLevel.Warn);
                return;
            }

            // 원본 fn_Save 첫 줄 : 조회했던 부번과 지금 입력된 부번이 달라지면 막는다
            if (PartNo.Key(txtPart.Text) != _searchedPtno)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart.Focus();
                txtPart.SelectAll();
                return;
            }

            string ptno = PartNo.Key(txtPart.Text);
            Row a = _search.FirstAlloc;
            string vchnoList = InboundService.QuoteVchnoList(_search.Allocs);
            int allocCnt = _search.Allocs.Count;

            Begin("확인중...");
            Async.Run(this,
                delegate
                {
                    return InboundService.SaveCheck(ptno, vchnoList, _vchym,
                                                    a["WSF_VNDMN"], a["WSF_VNDSB"]);
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    SaveCheckResult c = (SaveCheckResult)r;

                    if (c.TotalCnt > 0) 
                    { 
                        ClearAll(); ReportText("이미 입고된 부번입니다.", MsgLevel.Error);
                        return; 
                    }
                    if (!c.HasOutput07 || c.TotCnt <= 0) { ClearAll(); ReportText("입고되었거나 할당정보가 없는 부번입니다.", MsgLevel.Error); return; }
                    if (allocCnt != c.TotCnt) { ClearAll(); ReportText("입고된 할당내역이 있습니다. 다시 조회 후 입고하세요.", MsgLevel.Error); return; }
                    if (c.VchnoCnt > 0) { ClearAll(); ReportText("입고된 할당내역입니다. 헬프데스크에 문의하세요.", MsgLevel.Error); return; }

                    // 검증 통과. 이제 실제 커밋으로 넘어간다 (원본 fn_SaveChkAfter -> fn_Save).
                    Commit();
                });
        }

        // ------------------------------------------------------------------
        // 입고 커밋 (fn_Save -> fn_Save_After -> fn_SaveAfter2)
        //
        // 서버 호출 두 번. 1) 증표채번 + 업체집계 상태  2) 실제 쓰기.
        // 두 호출을 한 작업 스레드에서 이어서 돌린다.
        // ------------------------------------------------------------------
        private void Commit()
        {
            // 다건 할당은 원본이 할당내역 팝업(PL140_P01)이 넘겨준 dsInput 을 쓴다.
            // 그 팝업이 아직 없으므로 여기서 막는다. 잘못 커밋하면 되돌릴 수 없다.
            if (_search.Allocs.Count > 1)
            {
                ReportText("할당내역이 " + _search.Allocs.Count + "건입니다.\r\n"
                         + "다건 입고는 할당내역 선택 화면이 있어야 합니다.", MsgLevel.Warn);
                return;
            }

            Row a = _search.FirstAlloc;

            CommitInput c = CommitInput.From(a);
            c.Lep = _lep;
            c.Ptno = PartNo.Key(txtPart.Text);
            c.Whscd = CurrentWh();
            c.VapSysdt = _vapSysdt;
            c.VapSysdtL = _vapSysdtL;
            c.Vchym = _vchym;
            c.CtlQty = 0;      // 미수령 화면 미구현
            c.CtlCd = "";

            if (!Confirm("입고 저장하시겠습니까?\r\n"
                       + "부번 " + PartNo.Display(c.Ptno) + " / 수량 " + c.WsfQt
                       + " / 창고 " + c.Whscd + " / LOC " + Loc.Display(c.Locno)))
                return;

            Begin("저장중...");
            Async.Run(this,
                delegate
                {
                    // 이미 분류(2)된 건은 증표를 새로 따지 않는다. 원본도 1단계를 건너뛴다.
                    CommitPrepare p = c.IsClassified ? null
                                                     : InboundService.CommitPrepareCall(c);
                    InboundService.Commit(c, p);
                    return p;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    CommitPrepare p = r as CommitPrepare;
                    string vchno = (p == null) ? c.WsfVchno1 : p.Vchno;

                    string msg = CommonCache.Msg("MP102", "정상 처리되었습니다.");
                    if (vchno != null && vchno.Length > 0) msg += "  (증표 " + vchno + ")";

                    // fn_SaveAfter2 : 성공하면 화면을 초기화하고 다음 부번을 받는다
                    ClearAll();
                    End(msg, MsgLevel.Success);
                    MessageBox.Show(msg, "[140] " + ScreenName);
                    txtPart.Focus();
                });
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[140] " + ScreenName,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        // ------------------------------------------------------------------
        private string CurrentWh()
        {
            if (cboWh.SelectedIndex < 0) return "M";
            string v = cboWh.Items[cboWh.SelectedIndex].ToString();
            return (v.Length == 0) ? "M" : v;
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

        /// <summary>
        /// 메시지 코드로 결과를 알린다.
        /// 코드 문구는 서버 코드 테이블(ds_Message)에서 가져오고, 없으면 fallback 을 쓴다.
        /// MP101(정상) 이 아니면 푸터에 더해 MessageBox 도 띄운다.
        /// </summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) 
                MessageBox.Show(text, "[140] " + ScreenName);
        }

        /// <summary>코드가 없는(원본 JS 에 하드코딩된) 문구용. 항상 MessageBox 를 띄운다.</summary>
        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[140] " + ScreenName);
        }

        /// <summary>예외면 메시지 출력 후 true.</summary>
        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnAssignList.Enabled = on;
            btnStock.Enabled = on;
            btnLoc.Enabled = on;
            btnNotRecv.Enabled = on;
            btnSave.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearResult()
        {
            _search = null;
            lblClass.Text = "";
            lblPartName.Text = "";
            txtAssignCnt.Text = "";
            txtLoc.Text = "";
            txtCurStock.Text = "";
            txtSched.Text = "";
            txtNoStock.Text = "";
            txtReserve.Text = "";
            txtAssignQty.Text = "";
            txtQty.Text = "";
        }

        private void ClearAll()
        {
            _lep = "H";
            _vchym = ""; 
            _vapSysdt = ""; 
            _vapSysdtL = "";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            ClearResult();
        }

        private void txtPart_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
