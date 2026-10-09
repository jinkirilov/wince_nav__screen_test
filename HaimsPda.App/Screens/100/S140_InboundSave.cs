using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
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
        private const string MP_PREINPUT = "MP504";// 사업소 미발송 상태입니다. 선입고처리 하겠습니까?

        private string _lep = "H";
        private InboundSearchResult _search;
        private ArrayList _picked;           // [1401] 에서 고른 할당 레코드 (원본 dsInput). null = 선택 안 함
        private NotRecvResult _ctl;          // [132] 미수령 (원본 GV_Control / GV_NoInputQty / GV_NoInputRescd)
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

        public override void OnScan(HaimsPda.Devices.ScanData data)
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

        // 원본 fn_OnBtnAlloc : 할당내역 팝업(PL140_P01)을 연다
        private void OnAssignList(object sender, EventArgs e)
        {
            if (_busy) return;
            if (_search == null || !_search.HasAlloc)
            {
                Report(MP_NOLEP, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            // 원본 fn_OnBtnAlloc : 미수령이 있으면 할당을 다시 고를 때 초기화된다고 묻는다(MP561)
            if (_ctl != null)
            {
                if (!Confirm(CommonCache.Msg("MP561",
                        "미수령데이터가 등록되었습니다. 할당내역을 선택하면 미수령등록 내역이 초기화 됩니다. 확인하시겠습니까?")))
                    return;
                _ctl = null;
            }
            OpenAllocPopup();
        }

        // ------------------------------------------------------------------
        // [1401] 할당내역 선택 (PL140_P01)
        //
        // 팝업이 자체로 PL140_W01_S02 를 다시 부른다(WSFID=M). 여기서 고른 행이
        // 그대로 커밋 입력이 되고, 수량란에는 선택 합계를 넣는다(원본 GV_Qty).
        // 미수령(GV_Control)은 미구현이라 원본의 MP561 확인은 생략한다.
        // ------------------------------------------------------------------
        private void OpenAllocPopup()
        {
            string ptno = _searchedPtno;
            string whscd = CurrentWh();
            string lep = _lep;

            Begin("할당내역 조회중...");
            Async.Run(this,
                delegate { return InboundService.SearchAllocs(lep, ptno, whscd); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList rows = (ArrayList)r;
                    if (rows.Count == 0)
                    {
                        Report(MP_NOALLOC, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        return;
                    }
                    End("할당내역을 선택하세요.", MsgLevel.Info);

                    string title = "[" + ScreenId.AllocSelect + "] 할당내역  " + _lep + " "
                                 + PartNo.Display(ptno) + " " + lblClass.Text + "\r\n" + lblPartName.Text;
                    ArrayList sel = AllocSelect.Pick(rows, title);
                    if (sel == null)
                    {
                        Msg("할당내역 선택을 취소했습니다.", MsgLevel.Info);
                        txtQty.Focus();
                        return;
                    }

                    ApplyPicked(sel);
                });
        }

        private void ApplyPicked(ArrayList sel)
        {
            _picked = sel;
            int qty = InboundService.SumQty(sel);
            txtQty.Text = qty.ToString();

            Msg("할당 " + sel.Count + "건 선택 / 수량 " + qty, MsgLevel.Success);

            // 원본 : 선택한 행 중 하나라도 PRE_INPUT_YN=Y 면 MP504 안내(확인 버튼만)
            for (int i = 0; i < sel.Count; i++)
            {
                if (((Row)sel[i])["PRE_INPUT_YN"] == "Y")
                {
                    MessageBox.Show(CommonCache.Msg(MP_PREINPUT,
                        "사업소 미발송 상태입니다. 선입고처리 하겠습니까?"), "[140] " + ScreenName);
                    break;
                }
            }

            txtQty.Focus();
            txtQty.SelectAll();
        }

        /// <summary>
        /// 커밋 대상. 원본 fn_SaveChk 와 같은 순서로 고른다.
        ///   팝업에서 고른 것이 있으면 그것, 없고 할당이 1건이면 그 1건, 그 외는 null.
        /// </summary>
        private ArrayList Targets()
        {
            if (_picked != null && _picked.Count > 0) return _picked;
            if (_search != null && _search.Allocs.Count == 1) return _search.Allocs;
            return null;
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


            //Msg(" (fn_Loc) 준비중", MsgLevel.Info); 
            GoLoc();
        }

        private void GoLoc()
        {
            NavArgs a = new NavArgs();
            a.Set("LEP", _lep);
            a.Set("PTNO", PartNo.Key(txtPart.Text));
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            a.Set("WHSCD", CurrentWh());
            a.Set("RETURN", "Y");
            Shell.Navigate(ScreenId.LocRegister, a);
        }

        // ------------------------------------------------------------------
        // 원본 fn_MoveMisuryung -> [132] 미수령등록 (P119)
        //
        // 할당 1건에 대해서만 된다. 돌아오면 수량란에 입고수량(CAL_QT)을 넣고,
        // 커밋 때 CTLQT/CTLCD 와 함께 PL100_W01_I05 + I06 이 붙는다.
        // 입고대상수량은 할당 레코드의 수량을 쓴다(커밋의 WSF_WSFQT 와 같은 값).
        // 원본은 화면 수량란을 넘겨 두 번 등록하면 줄어든 수량이 기준이 된다.
        // ------------------------------------------------------------------
        private void OnNotRecv(object sender, EventArgs e)
        {
            if (_busy) return;

            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (_search == null || !_search.HasAlloc)
            {
                Report(MP_NOALLOC, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (_picked != null && _picked.Count > 1)
            {
                Report("MP539", "복수건 할당을 선택하면 미수령을 등록할 수 없습니다.", MsgLevel.Warn);
                return;
            }

            ArrayList t = Targets();
            if (t == null)
            {
                ReportText("할당내역이 " + _search.Allocs.Count + "건입니다.\r\n미수령할 할당을 먼저 1건 선택하세요.", MsgLevel.Warn);
                return;
            }

            Row a = (Row)t[0];
            string st = a["WSF_STAT"];
            if (st == "1" || st == "2")
            {
                Report("MP618", "입고분류가 완료된 부품은 미수령을 등록할 수 없습니다.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            int wsfQty = InboundService.QtyOf(a);
            string vchno = a["WSF_VCHNO"];
            string title = "[" + ScreenId.NotRecv + "] 미수령등록  " + _lep + " "
                         + PartNo.Display(_searchedPtno) + " " + lblClass.Text + "\r\n" + lblPartName.Text;

            Begin("미수령 사유 조회중...");
            Async.Run(this,
                delegate { return NotRecvService.GetReasons(); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList reasons = (ArrayList)r;
                    if (reasons.Count == 0)
                    {
                        ReportText("미수령 사유 코드를 가져오지 못했습니다.", MsgLevel.Error);
                        return;
                    }
                    End("미수령 수량과 사유를 입력하세요.", MsgLevel.Info);

                    NotRecvResult res = NotRecv.Show(title, vchno, wsfQty, reasons);
                    if (res == null)
                    {
                        Msg("미수령 등록을 취소했습니다.", MsgLevel.Info);
                        txtQty.Focus();
                        return;
                    }

                    _ctl = res;
                    txtQty.Text = res.InQty.ToString();
                    Msg("미수령 " + res.NarQty + " (" + res.ReasonNm + ") / 입고 " + res.InQty, MsgLevel.Success);
                    txtQty.Focus();
                    txtQty.SelectAll();
                });
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
                    int idx = HaimsPda.Controls.LepSelect.Pick(
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

                    // 할당이 여러 건이면 고르지 않고는 저장할 수 없으므로 바로 팝업을 띄운다.
                    // (원본은 GV_LinkChk 로 최초 조회 때는 건너뛰고 버튼으로만 열게 돼 있다)
                    if (sr.Allocs.Count > 1) OpenAllocPopup();
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

            ArrayList targets = Targets();
            if (targets == null)
            {
                ReportText("할당내역이 " + _search.Allocs.Count + "건입니다.\r\n입고할 할당내역을 선택하세요.", MsgLevel.Warn);
                OpenAllocPopup();
                return;
            }

            string ptno = PartNo.Key(txtPart.Text);
            Row a = _search.FirstAlloc;     // 거래처는 원본도 ds_Output01 첫 레코드에서 읽는다
            string vchnoList = InboundService.QuoteVchnoList(targets);
            int allocCnt = targets.Count;

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
                    Commit(targets);
                });
        }

        // ------------------------------------------------------------------
        // 입고 커밋 (fn_Save -> fn_Save_After -> fn_SaveAfter2)
        //
        // 서버 호출은 건당 두 번. 1) 증표채번 + 업체집계 상태  2) 실제 쓰기.
        //
        // 다건(팝업 선택)은 원본 fn_Save 의 for 루프처럼 레코드마다 같은 두 호출을 반복한다.
        // 원본 tit_CallService(..., true) 는 동기 호출이라 한 건씩 끝내고 다음 건으로 간다.
        // 여기서도 한 작업 스레드에서 순서대로 돌리고, 중간에 실패하면 거기서 멈춘다.
        // 건마다 서버 트랜잭션이 따로라 앞서 끝난 건은 되돌리지 못한다.
        //
        // 수량은 각 레코드의 WSF_WSFQT 를 그대로 쓴다. 화면 수량란은 표시용이다(원본 동일).
        // ------------------------------------------------------------------
        private void Commit(ArrayList targets)
        {
            ArrayList inputs = new ArrayList();
            for (int i = 0; i < targets.Count; i++)
            {
                CommitInput c = CommitInput.From((Row)targets[i]);
                c.Lep = _lep;
                c.Ptno = PartNo.Key(txtPart.Text);
                c.Whscd = CurrentWh();
                c.VapSysdt = _vapSysdt;
                c.VapSysdtL = _vapSysdtL;
                c.Vchym = _vchym;
                // 미수령은 할당 1건일 때만 등록된다(MP539)
                if (_ctl != null && targets.Count == 1)
                {
                    c.CtlQty = _ctl.NarQty;
                    c.CtlCd = _ctl.ReasonCd;
                }
                inputs.Add(c);
            }

            CommitInput first = (CommitInput)inputs[0];
            string what = (inputs.Count == 1)
                ? "수량 " + first.WsfQt + " / LOC " + Loc.Display(first.Locno)
                : "할당 " + inputs.Count + "건 / 수량 " + InboundService.SumQty(targets);
            if (first.HasControl)
                what += "\r\n미수령 " + first.CtlQty + " (" + _ctl.ReasonNm + ")";

            if (!Confirm("입고 저장하시겠습니까?\r\n"
                       + "부번 " + PartNo.Display(first.Ptno) + " / " + what
                       + " / 창고 " + first.Whscd))
                return;

            int[] done = new int[1];
            ArrayList vchnos = new ArrayList();

            Begin("저장중...");
            Async.Run(this,
                delegate
                {
                    for (int i = 0; i < inputs.Count; i++)
                    {
                        CommitInput c = (CommitInput)inputs[i];

                        // 이미 분류(2)된 건은 증표를 새로 따지 않는다. 원본도 1단계를 건너뛴다.
                        CommitPrepare p = c.IsClassified ? null
                                                         : InboundService.CommitPrepareCall(c);
                        InboundService.Commit(c, p);

                        string v = (p == null) ? c.WsfVchno1 : p.Vchno;
                        if (v != null && v.Length > 0) vchnos.Add(v);
                        done[0]++;
                    }
                    return null;
                },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        if (done[0] == 0) { Fail(ex); return; }

                        // 일부만 들어갔다. 화면 값은 더 이상 맞지 않으므로 비우고 재조회하게 한다.
                        ClearAll();
                        ReportText(inputs.Count + "건 중 " + done[0] + "건 저장 후 오류가 났습니다.\r\n"
                                 + ex.Message + "\r\n부번을 다시 조회하세요.", MsgLevel.Error);
                        txtPart.Focus();
                        return;
                    }

                    string msg = CommonCache.Msg("MP102", "정상 처리되었습니다.");
                    if (inputs.Count > 1) msg += "  (" + inputs.Count + "건)";
                    if (vchnos.Count > 0) msg += "\r\n증표 " + Join(vchnos);

                    // fn_SaveAfter2 : 성공하면 화면을 초기화하고 다음 부번을 받는다
                    ClearAll();
                    End(msg, MsgLevel.Success);
                    MessageBox.Show(msg, "[140] " + ScreenName);
                    txtPart.Focus();
                });
        }

        private static string Join(ArrayList list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(list[i]);
            }
            return sb.ToString();
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
            _picked = null;
            _ctl = null;
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
