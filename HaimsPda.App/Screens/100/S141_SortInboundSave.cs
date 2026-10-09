using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [141] 정렬입고저장 : 입고할 부번을 먼저 모아 LOC 순으로 정렬해 두고,
    // 현장을 돌며 LOC -> 부번 순으로 스캔해 한 줄씩 입고한다.
    // 원본 웹화면 : /ui/ws/plus/PL141_W01.xml (메뉴 1A07)
    //
    // 화면 위쪽 : 수집. 부번 스캔 -> 계열 -> 등급 -> 할당조회 -> 작업목록에 한 줄 추가
    //   할당이 2건 이상이면 [1401] 할당내역 팝업에서 고른 건만 담는다.
    //   LOC 가 없으면(MP521) 예 = [410] LOC등록으로 이동, 아니오 = 기본 LOC "M" 자동등록.
    // 화면 아래쪽 : 작업. LOC 스캔(목록에서 찾기) -> 부번 스캔(일치 확인) -> 수량 Enter = 저장
    //   SORT 는 LOC 순 정렬 토글, SKIP 은 다음 줄로.
    //
    // 원본과 다르게 한 부분
    //   - ↕ 버튼(버튼줄 토글)은 쓰지 않고 6개 버튼을 한 줄에 둔다(140 과 같은 결정).
    //   - 이미 목록에 있는 부번에서 할당내역 버튼을 누르면 그 줄의 선택을 바꾼다.
    //     (원본은 팝업에서 돌아온 뒤 중복검사에 걸려 아무 것도 바뀌지 않는다)
    //   - 저장할 때 LOC 가 비어 있으면 막는다(원본은 빈 LOC 로 저장된다).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S141_SortInboundSave.Designer.cs 에서 관리한다.
    public sealed partial class S141_SortInboundSave : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";    // 정상 처리되었습니다
        private const string MP_PTNO = "MP303";     // 부품번호를 확인하세요
        private const string MP_NOLEP = "MP540";    // 할당정보가 없는 부품번호입니다(계열)
        private const string MP_NOALLOC = "MP532";  // 할당정보가 없는 부품번호입니다
        private const string MP_DONE = "MP316";     // 처리된 부품번호입니다
        private const string MP_PREINPUT = "MP504"; // 사업소 미발송 상태입니다
        private const string MP_NOLOCREG = "MP521"; // 할당된 부품의 로케이션이 없습니다. 등록하시겠습니까
        private const string MP_LOCDIFF = "MP557";  // 저장로케이션과 일치하지 않습니다
        private const string MP_SKIP = "MP509";     // SKIP 하시겠습니까
        private const string MP_REALLOC = "MP538";  // 이전에 선택한 설정내역은 삭제됩니다. 진행하시겠습니까
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까
        private const string MP_NOLOC = "MP310";    // 로케이션을 확인하세요

        private readonly ArrayList _items = new ArrayList();   // SortItem. 원본 grd + hidGrd
        private int _idx;                // 원본 m_nIdx : 작업 중인 줄
        private bool _sortAsc = true;    // 원본 m_bSort1
        private bool _busy;

        // 위쪽(수집) 조회 상태
        private string _lep = "H";
        private ClassInfo _class;

        // 아래쪽(작업) 상태. 원본 txtLEP2 / txtCLASS2
        private string _workLep = "H";

        public override int ScreenNo { get { return ScreenId.SortInboundSave; } }
        public override string ScreenName { get { return "정렬입고저장"; } }

        public S141_SortInboundSave()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            InitAll(false);
            LoadWarehouse();
            txtPart.Focus();
            Msg("입고할 부번을 스캔하세요.", MsgLevel.Info);
        }

        // 원본 GV_FocusGbn : 포커스 위치에 따라 스캔을 나눈다
        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;
            if (txtLoc.Focused)
            {
                txtLoc.Text = Loc.Display(data.Text);
                CheckLoc();
            }
            else if (txtPart2.Focused)
            {
                txtPart2.Text = PartNo.Display(data.Text);
                CheckPart2();
            }
            else
            {
                txtPart.Text = PartNo.Display(data.Text);
                SearchPart();
            }
        }

        // ------------------------------------------------------------------
        // 키 / 버튼
        // ------------------------------------------------------------------
        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchPart(); }
        }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            txtLoc.Text = Loc.Display(txtLoc.Text);
            CheckLoc();
        }

        private void OnPart2KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            txtPart2.Text = PartNo.Display(txtPart2.Text);
            CheckPart2();
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; Save(); }
        }

        private void OnSearch(object sender, EventArgs e) { SearchPart(); }
        private void OnSave(object sender, EventArgs e) { Save(); }

        // 원본 fn_CellClick : 줄을 고르면 작업칸에 채우고 LOC 로 간다
        private void OnListSelected(object sender, EventArgs e)
        {
            if (_busy || lstWork.SelectedIndices.Count == 0) return;
            _idx = lstWork.SelectedIndices[0];
            SortItem it = (SortItem)_items[_idx];
            txtPart.Text = PartNo.Display(it.Ptno);
            ShowWork(it);
            txtLoc.Focus();
        }

        // 원본 fn_OnBtnSort : LOC 순 정렬 (누를 때마다 오름/내림 토글)
        private void OnSort(object sender, EventArgs e)
        {
            if (_busy || _items.Count == 0) return;
            _sortAsc = !_sortAsc;
            _items.Sort(new LocComparer(_sortAsc));
            _idx = 0;
            RefreshList();
            DisplayWork();
            Msg("LOC " + (_sortAsc ? "오름차순" : "내림차순") + " 정렬", MsgLevel.Info);
        }

        // 원본 fn_OnBtnSkip
        private void OnSkip(object sender, EventArgs e)
        {
            if (_busy || _items.Count == 0) return;
            if (!Confirm(CommonCache.Msg(MP_SKIP, "SKIP 하시겠습니까?"))) return;
            _idx++;
            DisplayWork();
        }

        // 원본 fn_OnBtnDel : 작업 중인 줄을 목록에서 뺀다 (서버 호출 없음)
        private void OnDelete(object sender, EventArgs e)
        {
            if (_busy) return;
            if (PartNo.Key(txtPart2.Text).Length == 0 || _idx < 0 || _idx >= _items.Count)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                return;
            }
            _items.RemoveAt(_idx);
            RefreshList();
            ClearWork();
            Msg("목록에서 삭제했습니다. (" + _items.Count + "건 남음)", MsgLevel.Info);
        }

        // 원본 fn_Clear
        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "화면을 지우시겠습니까?"))) { txtPart.Focus(); return; }
            InitAll(false);
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // 원본 textbox15 -> fn_MoveLoc : [410] LOC등록으로
        private void OnLocRegister(object sender, EventArgs e)
        {
            if (_busy) return;
            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            MoveLoc();
        }

        // 원본 fn_OnBtnAlloc
        private void OnAssignList(object sender, EventArgs e)
        {
            if (_busy) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_NOLEP, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            int at = Find(_lep, ptno);
            if (at < 0)
            {
                // 목록에 없으면 처음 조회와 같다. 다건이면 팝업이 뜬다.
                SearchPart();
                return;
            }

            if (!Confirm(CommonCache.Msg(MP_REALLOC,
                    "이전에 선택한 설정내역은 삭제됩니다. 진행하시겠습니까?")))
                return;

            ReAlloc(at);
        }

        // ------------------------------------------------------------------
        // 창고 콤보 (plus:PL000_W01_S01) — 140 과 같은 호출
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

                    // 원본 fn_Init : 창고 기본값 M
                    int m = cboWh.Items.IndexOf("M");
                    cboWh.SelectedIndex = (m >= 0) ? m : 0;
                });
        }

        // ==================================================================
        // 수집 : fn_SearchLep -> fn_SearchList(중복) -> fn_SearchClass -> fn_Search
        // ==================================================================
        private void SearchPart()
        {
            if (_busy) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            txtPart.Text = PartNo.Display(ptno);
            ClearTop();

            Begin("조회중...");
            Async.Run(this,
                delegate { return InboundService.SearchLep(ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;
                    if (leps.Count == 0)
                    {
                        SetLep("H");
                        Report(MP_NOLEP, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                        FocusPart();
                        return;
                    }

                    int idx = HaimsPda.Controls.LepSelect.Pick(
                                  leps, "계열 선택 - " + PartNo.Display(ptno));
                    if (idx < 0) { End("취소했습니다.", MsgLevel.Info); FocusPart(); return; }

                    SetLep((string)leps[idx]);

                    // 원본 fn_SearchList : 같은 계열+부번이 이미 목록에 있으면 막는다
                    if (Find(_lep, ptno) >= 0)
                    {
                        Report(MP_DONE, "처리된 부품번호입니다.", MsgLevel.Warn);
                        FocusPart();
                        return;
                    }

                    SearchClass(ptno, true);
                });
        }

        // fn_SearchClass. 결과가 없으면 재고등록(PL141_W01_I02) 후 한 번 더 조회한다.
        private void SearchClass(string ptno, bool allowRegister)
        {
            string lep = _lep;
            Async.Run(this,
                delegate { return InboundService.SearchClass(lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassInfo info = (ClassInfo)r;
                    if (!info.Found)
                    {
                        if (!allowRegister)
                        {
                            ReportText("재고정보를 찾을 수 없습니다.", MsgLevel.Error);
                            FocusPart();
                            return;
                        }
                        Msg("재고정보가 없어 재고를 등록합니다.", MsgLevel.Warn);
                        SaveInv(ptno);
                        return;
                    }

                    _class = info;
                    lblClass.Text = info.Grade;
                    lblPartName.Text = info.PartName;
                    SearchDetail(ptno);
                });
        }

        private void SaveInv(string ptno)
        {
            string lep = _lep;
            Async.Run(this,
                delegate { SortInboundService.SaveInv(lep, ptno); return null; },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    SearchClass(ptno, false);   // 원본 fn_SaveInv_After -> fn_SearchClass
                });
        }

        // fn_Search -> fn_Search_After
        private void SearchDetail(string ptno)
        {
            string lep = _lep;
            string whscd = CurrentWh();

            Async.Run(this,
                delegate { return SortInboundService.Search(lep, ptno, whscd); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    SortSearchResult sr = (SortSearchResult)r;
                    if (sr.Allocs.Count == 0)
                    {
                        Report(MP_NOALLOC, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                        FocusPart();
                        return;
                    }

                    txtAssignCnt.Text = sr.Screen_("WSF_CNT");
                    int wsfCnt = ToInt(sr.Screen_("WSF_CNT"));
                    if (wsfCnt == 0)
                    {
                        Report(MP_NOALLOC, "할당 정보가 없는 부품번호입니다.", MsgLevel.Warn);
                        FocusPart();
                        return;
                    }

                    SortItem it = NewItem(ptno, sr);

                    if (wsfCnt > 1)
                    {
                        // 원본은 P179(PL140_P01)로 이동했다가 돌아와 다시 조회한다.
                        // 여기서는 팝업이 모달이라 그 자리에서 고르고 이어서 담는다.
                        End("할당내역을 선택하세요.", MsgLevel.Info);
                        PickAllocs(it, -1);
                        return;
                    }

                    // 단건 : 수량은 첫 레코드 할당수량, 커밋 대상은 조회된 레코드 전부
                    it.Allocs = sr.Allocs;
                    it.Qty = sr.FirstAlloc["WSF_WSFQT"];
                    CheckPreInput(it.Allocs);
                    AddItem(it);
                });
        }

        private SortItem NewItem(string ptno, SortSearchResult sr)
        {
            Row a = sr.FirstAlloc;
            SortItem it = new SortItem();
            it.Lep = _lep;
            it.Ptno = PartNo.Key(ptno);
            it.Grade = lblClass.Text;
            it.PartName = lblPartName.Text;
            it.Loc = a["LOC_LOCNO"];
            it.CurInv = sr.Screen_("CURINV");
            it.MinusHk = a["MINUS_HK"];
            if (_class != null)
            {
                it.VapSysdt = _class.VapSysdt;
                it.VapSysdtL = _class.VapSysdtL;
                it.Vchym = _class.Vchym;
            }
            return it;
        }

        /// <summary>
        /// [1401] 팝업으로 할당을 고른다.
        /// replaceAt &lt; 0 이면 새 줄로 담고, 아니면 그 줄의 선택을 바꾼다.
        /// </summary>
        private void PickAllocs(SortItem it, int replaceAt)
        {
            string lep = it.Lep, ptno = it.Ptno, whscd = CurrentWh();

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
                        FocusPart();
                        return;
                    }
                    End("할당내역을 선택하세요.", MsgLevel.Info);

                    string title = "[" + ScreenId.AllocSelect + "] 할당내역  " + it.Lep + " "
                                 + PartNo.Display(it.Ptno) + " " + it.Grade + "\r\n" + it.PartName;
                    ArrayList sel = AllocSelect.Pick(rows, title);
                    if (sel == null)
                    {
                        Msg("할당내역 선택을 취소했습니다.", MsgLevel.Info);
                        FocusPart();
                        return;
                    }

                    it.Allocs = sel;
                    it.Qty = InboundService.SumQty(sel).ToString();
                    CheckPreInput(sel);

                    if (replaceAt < 0)
                    {
                        AddItem(it);
                        return;
                    }

                    _items[replaceAt] = it;
                    RefreshList();
                    Msg("할당 " + sel.Count + "건 / 수량 " + it.Qty + " 으로 바꿨습니다.", MsgLevel.Success);
                    FocusPart();
                });
        }

        // 목록에 있는 줄의 할당 선택을 다시 한다
        private void ReAlloc(int at)
        {
            SortItem old = (SortItem)_items[at];
            SortItem it = new SortItem();
            it.Lep = old.Lep; it.Ptno = old.Ptno; it.Grade = old.Grade; it.PartName = old.PartName;
            it.Loc = old.Loc; it.CurInv = old.CurInv; it.MinusHk = old.MinusHk;
            it.VapSysdt = old.VapSysdt; it.VapSysdtL = old.VapSysdtL; it.Vchym = old.Vchym;
            PickAllocs(it, at);
        }

        // 원본 : PRE_INPUT_YN=Y 가 하나라도 있으면 MP504 안내
        private void CheckPreInput(ArrayList allocs)
        {
            for (int i = 0; i < allocs.Count; i++)
            {
                if (((Row)allocs[i])["PRE_INPUT_YN"] == "Y")
                {
                    MessageBox.Show(CommonCache.Msg(MP_PREINPUT,
                        "사업소 미발송 상태입니다. 선입고처리 하겠습니까?"), "[141] " + ScreenName);
                    return;
                }
            }
        }

        // InsertList + InsertHideList, 그리고 LOC 없음 처리(MP521)
        private void AddItem(SortItem it)
        {
            _items.Add(it);
            RefreshList();
            ClearWork();

            End(CommonCache.Msg(MP_OK, "정상 조회되었습니다.") + " (목록 " + _items.Count + "건)",
                MsgLevel.Success);

            if (!it.HasLoc)
            {
                txtLoc.Text = "NONE";
                bool yes = Confirm(CommonCache.Msg(MP_NOLOCREG,
                    "할당된 부품의 로케이션이 없습니다. 로케이션을 등록하시겠습니까?"));
                if (yes) { MoveLoc(); return; }
                RegisterDefaultLoc(it);
                return;
            }
            FocusPart();
        }

        // 원본 fn_SearchCheckInUp : 기본 LOC "M" 등록. 실패해도 목록은 그대로 둔다.
        private void RegisterDefaultLoc(SortItem it)
        {
            string whscd = CurrentWh();
            Begin("기본 LOC 등록중...");
            Async.Run(this,
                delegate { return SortInboundService.RegisterDefaultLoc(whscd, it.Lep, it.Ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) { FocusPart(); return; }
                    End((bool)r ? "기본 LOC(M)를 등록했습니다." : "LOC 정보가 없어 등록하지 않았습니다.",
                        MsgLevel.Info);
                    FocusPart();
                });
        }

        // 원본 fn_MoveLoc : [410] LOC등록. 저장하면 돌아온다(RETURN=Y). 작업목록은 이 화면에 남아 있다.
        private void MoveLoc()
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

        // ==================================================================
        // 작업 : fn_CheckLoc -> fn_CheckPartNo -> fn_Save
        // ==================================================================

        // 원본 fn_CheckLoc : 스캔한 LOC 를 목록에서 찾는다
        private void CheckLoc()
        {
            string loc = Loc.Key(txtLoc.Text);
            int found = -1;
            for (int i = 0; i < _items.Count; i++)
            {
                if (Loc.Key(((SortItem)_items[i]).Loc) == loc) { found = i; break; }
            }

            if (found < 0)
            {
                _idx = -1;
                Report(MP_LOCDIFF, "저장로케이션과 일치하지 않습니다.", MsgLevel.Warn);
                txtLoc.Focus();
                txtLoc.SelectAll();
                return;
            }

            _idx = found;
            SelectRow(found);
            Msg("LOC 확인. 부번을 스캔하세요.", MsgLevel.Info);
            txtPart2.Focus();
            txtPart2.SelectAll();
        }

        // 원본 fn_CheckPartNo : 그 LOC 줄의 부번과 같은지
        private void CheckPart2()
        {
            if (_idx < 0 || _idx >= _items.Count
                || PartNo.Key(txtPart2.Text) != ((SortItem)_items[_idx]).Ptno)
            {
                Report(MP_NOALLOC, "입고할당된 부품이 아닙니다.", MsgLevel.Warn);
                txtPart2.Text = "";
                txtPart2.Focus();
                return;
            }

            SortItem it = (SortItem)_items[_idx];
            SetWorkLep(it.Lep);
            lblClass2.Text = it.Grade;
            txtCurInv.Text = it.CurInv;
            txtQty.Text = it.Qty;
            Msg("부번 확인. 수량 확인 후 Enter = 저장", MsgLevel.Info);
            txtQty.Focus();
            txtQty.SelectAll();
        }

        // 원본 fn_Save : 그 부번의 할당 레코드를 한 건씩 커밋한다 (원본도 동기 호출 루프)
        private void Save()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart2.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요!", MsgLevel.Warn);
                txtPart2.Focus();
                return;
            }

            int at = Find(_workLep, ptno);
            if (at < 0)
            {
                Report(MP_NOALLOC, "입고할당된 부품이 아닙니다.", MsgLevel.Warn);
                txtPart2.Focus();
                return;
            }

            string loc = Loc.Key(txtLoc.Text);
            if (loc.Length == 0 || loc == "NONE")
            {
                Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn);
                txtLoc.Focus();
                return;
            }

            SortItem it = (SortItem)_items[at];
            string whscd = CurrentWh();
            int[] done = new int[1];

            Begin("저장중...");
            Async.Run(this,
                delegate
                {
                    for (int i = 0; i < it.Allocs.Count; i++)
                    {
                        Row a = (Row)it.Allocs[i];
                        CommitPrepare p = (a["WSF_STAT"] == "2")
                            ? new CommitPrepare()
                            : SortInboundService.Prepare(a, it);
                        SortInboundService.Commit(a, it, whscd, loc, p);
                        done[0]++;
                    }
                    return null;
                },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        if (done[0] == 0) { Fail(ex); return; }

                        // 일부만 들어갔다. 이 줄은 더 이상 믿을 수 없으니 목록에서 빼고 재조회하게 한다.
                        _items.Remove(it);
                        RefreshList();
                        ClearWork();
                        ReportText(it.Allocs.Count + "건 중 " + done[0] + "건 저장 후 오류가 났습니다.\r\n"
                                 + ex.Message + "\r\n부번을 다시 조회하세요.", MsgLevel.Error);
                        FocusPart();
                        return;
                    }

                    // fn_Save_After : 그 줄을 지우고 다음 줄을 띄운다
                    _items.Remove(it);
                    RefreshList();
                    ClearWork();

                    string msg = CommonCache.Msg(MP_SAVED, "정상 처리되었습니다.")
                               + "  " + PartNo.Display(it.Ptno) + " / " + it.Qty;
                    End(msg, MsgLevel.Success);

                    if (_items.Count == 0) { InitAll(true); txtPart.Focus(); return; }
                    DisplayWork();
                    txtLoc.Focus();
                });
        }

        // 원본 DisplayWork : _idx 줄을 작업칸에 띄운다
        private void DisplayWork()
        {
            if (_items.Count == 0) { ClearWork(); return; }
            if (_idx < 0 || _idx + 1 > _items.Count) _idx = 0;
            SortItem it = (SortItem)_items[_idx];
            ShowWork(it);
            SelectRow(_idx);
        }

        private void ShowWork(SortItem it)
        {
            txtLoc.Text = Loc.Display(it.Loc);
            SetWorkLep(it.Lep);
            txtPart2.Text = PartNo.Display(it.Ptno);
            lblClass2.Text = it.Grade;
            txtCurInv.Text = it.CurInv;
            txtQty.Text = it.Qty;
        }

        // ------------------------------------------------------------------
        // 목록
        // ------------------------------------------------------------------
        private void RefreshList()
        {
            lstWork.BeginUpdate();
            try
            {
                lstWork.Items.Clear();
                for (int i = 0; i < _items.Count; i++)
                {
                    SortItem it = (SortItem)_items[i];
                    ListViewItem li = new ListViewItem(it.Lep);
                    li.SubItems.Add(PartNo.Display(it.Ptno));
                    li.SubItems.Add(it.Qty);
                    li.SubItems.Add(it.HasLoc ? Loc.Display(it.Loc) : "NONE");
                    lstWork.Items.Add(li);
                }
            }
            finally { lstWork.EndUpdate(); }
            lblCount.Text = _items.Count + "건";
        }

        private void SelectRow(int i)
        {
            if (i < 0 || i >= lstWork.Items.Count) return;
            bool b = _busy;
            _busy = true;   // OnListSelected 재진입 방지
            try
            {
                lstWork.Items[i].Selected = true;
                lstWork.EnsureVisible(i);
            }
            finally { _busy = b; }
        }

        private int Find(string lep, string ptno)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                SortItem it = (SortItem)_items[i];
                if (it.Lep == lep && it.Ptno == ptno) return i;
            }
            return -1;
        }

        /// <summary>LOC 정렬. LOC 없는 줄은 늘 뒤로 보낸다.</summary>
        private sealed class LocComparer : IComparer
        {
            private readonly bool _asc;
            public LocComparer(bool asc) { _asc = asc; }
            public int Compare(object x, object y)
            {
                SortItem a = (SortItem)x, b = (SortItem)y;
                if (a.HasLoc != b.HasLoc) return a.HasLoc ? -1 : 1;
                int c = string.CompareOrdinal(Loc.Key(a.Loc), Loc.Key(b.Loc));
                return _asc ? c : -c;
            }
        }

        // ------------------------------------------------------------------
        // 공통
        // ------------------------------------------------------------------
        private string CurrentWh()
        {
            if (cboWh.SelectedIndex < 0) return "M";
            string v = cboWh.Items[cboWh.SelectedIndex].ToString();
            return (v.Length == 0) ? "M" : v;
        }

        private void SetLep(string lep) { _lep = lep; lblPrefix.Text = lep; }
        private void SetWorkLep(string lep) { _workLep = lep; lblPrefix2.Text = lep; }

        private void FocusPart()
        {
            txtPart.Focus();
            txtPart.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[141] " + ScreenName,
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
            Msg(msg, lv);
        }

        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[141] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[141] " + ScreenName);
        }

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
            btnLocReg.Enabled = on;
            btnSort.Enabled = on;
            btnSkip.Enabled = on;
            btnDelete.Enabled = on;
            btnClear.Enabled = on;
            btnSave.Enabled = on;
        }

        private void ClearTop()
        {
            _class = null;
            lblClass.Text = "";
            lblPartName.Text = "";
            txtAssignCnt.Text = "";
        }

        private void ClearWork()
        {
            SetWorkLep("H");
            txtLoc.Text = "";
            txtPart2.Text = "";
            lblClass2.Text = "";
            txtCurInv.Text = "";
            txtQty.Text = "";
        }

        /// <summary>원본 fn_Init. keepWh=true 면 창고는 그대로 둔다(원본 fn_Init("1")).</summary>
        private void InitAll(bool keepWh)
        {
            SetLep("H");
            txtPart.Text = "";
            ClearTop();
            ClearWork();
            _items.Clear();
            _idx = 0;
            RefreshList();
            if (!keepWh && cboWh.Items.Count > 0)
            {
                int m = cboWh.Items.IndexOf("M");
                cboWh.SelectedIndex = (m >= 0) ? m : 0;
            }
        }

        private static int ToInt(string s)
        {
            if (s == null || s.Length == 0) return 0;
            try { return (int)double.Parse(s.Replace(",", "")); } catch { return 0; }
        }
    }
}
