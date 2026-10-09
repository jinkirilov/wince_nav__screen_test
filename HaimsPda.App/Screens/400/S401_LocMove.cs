using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [401] LOC재고이동 : 출발 LOC 의 재고를 다른 LOC 로 옮긴다.
    // 원본 웹화면 : /ui/ws/plus/PL401_W01.xml (메뉴 1D05 / P145), 매뉴얼 LOC관리 162~167p
    //
    //   모드 버튼(싱글 -> 멀티 -> ALL)
    //   싱글 : LOC 스캔 -> 부번 스캔 -> (수량 확인) -> 이동 LOC 스캔 -> 수량 Enter -> 저장
    //   멀티 : LOC 스캔 -> 부번 스캔 반복(목록) -> [불출] -> 품목마다 이동 LOC 스캔 -> 수량 Enter -> 저장
    //   ALL  : LOC 스캔 -> "N건 이동?" 확인 -> 이동 LOC 스캔 -> 전체 저장
    //
    // 원본과 다르게 한 부분 (사유는 LocMoveService 주석 참고)
    //   - 체인파트 : 원본은 [411] 체인파트 선택 화면으로 넘어가지만 미확보 화면이라 안내 팝업만 띄운다.
    //               원본의 PL411_W01_S05 무한 재호출 버그는 따라 하지 않는다.
    //   - 멀티 저장 : 원본은 첫 건 저장 후 fn_Init 로 목록까지 지워 버린다. 여기서는 목록이 빌 때까지 이어서 저장한다.
    //   - ALL 저장 : 원본은 TYPE=M 일괄(실측 없음). 여기서는 싱글 저장을 품목 수만큼 반복한다.
    //   - 내역 : 원본은 [322] 상세내역 화면(PL322_W01)으로 갔다가 돌아온다. 이미 조회한 LOC 목록에서 고르는 팝업으로 대체.
    //   - 출발/이동 LOC 가 같으면 막는다. ALL 은 출고대기가 있는 품목이 하나라도 있으면 막는다(싱글과 같은 MP583 기준).
    //
    // 원본과 같게 둔 부분
    //   - 수량은 입력할 수 없다. 원본도 onkeyup 에서 가능수량으로 되돌려서 항상 LOC 의 가능수량 전부를 옮긴다.
    //   - 이동 LOC 는 클라이언트에서 존재 여부를 확인하지 않는다(원본 fn_SearchLocTo 는 호출되지 않는 죽은 코드).
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S401_LocMove.Designer.cs 에서 관리한다.
    public sealed partial class S401_LocMove : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";    // 정상 저장되었습니다
        private const string MP_SAVEERR = "MP108";  // 저장중 에러가 발생하였습니다
        private const string MP_NOPINFO = "MP302";  // 부품정보를 확인하세요
        private const string MP_NOPART = "MP303";   // 부품번호를 확인하세요
        private const string MP_NOLOC = "MP310";    // 로케이션을 확인하세요
        private const string MP_BADLOC = "MP313";   // 정확한 로케이션 코드가 아닙니다
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까
        private const string MP_DEL = "MP512";      // 삭제하시겠습니까
        private const string MP_NOSEL = "MP545";    // 선택된 데이터가 없습니다
        private const string MP_SALQT = "MP583";    // 출고대기수량 존재. 이동불가

        private enum Mode { Single, Multi, All }

        private delegate void Step();

        private Mode _mode = Mode.Single;
        private bool _busy;
        private string _focus = "";         // 원본 GV_FocusGbn : LOCFR / PART / LOCTO
        private string _lep = "H";

        private ArrayList _locItems = new ArrayList();   // 출발 LOC 의 품목 (LocMoveItem)
        private string _locKey = "";                     // _locItems 를 조회한 LOC (원본 표기 없는 값)
        private ArrayList _picked = new ArrayList();     // 멀티 : 고른 품목
        private LocMoveItem _toItem;                     // 이동 대상 (싱글/멀티)
        private bool _transfer;                          // 멀티 : [불출] 후 저장 진행 중
        private bool _allReady;                          // ALL : 이동 확인 완료

        private string _linkPtno;                        // [320] 등에서 넘어온 부번 (LOC 조회 후 자동 적용)
        private string _linkLep;

        public override int ScreenNo { get { return ScreenId.LocMove; } }
        public override string ScreenName { get { return "LOC재고이동"; } }

        public S401_LocMove()
        {
            InitializeComponent();
            if (IsDesignMode) return;

            WinApi.GridLines(this.lstList.Handle, true);
            WinApi.DoubleBuffering(this.lstList.Handle, true);
        }

        public override void OnEnter(NavArgs args)
        {
            _mode = Mode.Single;
            InitAll();

            _linkPtno = Arg(args, "PTNO");
            _linkLep = Arg(args, "LEP");
            LoadWarehouses(Arg(args, "WHSCD"), Arg(args, "LOCNO"));
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        // ------------------------------------------------------------------
        // 창고 콤보 (원본 gfn_SearchWHS_Plus)
        // ------------------------------------------------------------------
        private void LoadWarehouses(string linkWh, string linkLoc)
        {
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
                    SelectByText(cboWh, linkWh);

                    End("LOC 를 스캔/입력하세요.", MsgLevel.Info);

                    if (linkLoc.Length > 0)
                    {
                        txtLocFr.Text = Loc.Display(linkLoc);
                        LocSearch();
                    }
                    else
                        txtLocFr.Focus();
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
        // 스캔 / 입력 이벤트 (원본 fn_Barcode : 포커스 위치로 분기)
        // ------------------------------------------------------------------
        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;
            string code = data.Text;

            string target = _focus;
            if (target.Length == 0)
            {
                if (Loc.Key(txtLocFr.Text).Length == 0 || _locItems.Count == 0) target = "LOCFR";
                else if (txtLocTo.Enabled && (_mode == Mode.All || _toItem != null)) target = "LOCTO";
                else target = "PART";
            }

            if (target == "LOCTO" && txtLocTo.Enabled)
            {
                txtLocTo.Text = Loc.Display(code);
                LocToEnter();
            }
            else if (target == "PART" && txtPart.Enabled)
            {
                txtPart.Text = PartNo.Display(code);
                PartSearch();
            }
            else if (txtLocFr.Enabled)
            {
                txtLocFr.Text = Loc.Display(code);
                LocSearch();
            }
        }

        private void OnLocFrFocus(object sender, EventArgs e) { _focus = "LOCFR"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PART"; }
        private void OnLocToFocus(object sender, EventArgs e) { _focus = "LOCTO"; }
        private void OnOtherFocus(object sender, EventArgs e) { _focus = ""; }

        private void OnLocFrKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocSearch();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            PartSearch();
        }

        private void OnLocToKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocToEnter();
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            SaveOne();
        }

        private void OnWhChanged(object sender, EventArgs e)
        {
            // 창고가 바뀌면 조회해 둔 LOC 는 무효
            if (_busy || _locItems.Count == 0) return;
            InitKeep();
            Msg("창고가 바뀌었습니다. LOC 를 다시 스캔하세요.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 1. 출발 LOC 조회 (원본 fn_SearchLocFr)
        // ------------------------------------------------------------------
        private void LocSearch()
        {
            if (_busy) return;

            string loc = Loc.Key(txtLocFr.Text);
            if (loc.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocFr); return; }

            txtLocFr.Text = Loc.Display(loc);
            string wh = CurWh;

            Begin("조회중...");
            Async.Run(this,
                delegate { return LocMoveService.SearchLoc(wh, loc); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    ResetPartFields();
                    ResetToFields();
                    _picked.Clear();
                    _allReady = false;

                    if (list.Count == 0)
                    {
                        _locItems = new ArrayList();
                        _locKey = "";
                        FillGrid(_locItems);
                        Report(MP_BADLOC, "정확한 로케이션 코드가 아닙니다.", MsgLevel.Warn);
                        FocusSel(txtLocFr);
                        return;
                    }

                    _locItems = list;
                    _locKey = loc;

                    if (_mode == Mode.All) { AfterLocAll(); return; }

                    FillGrid(_mode == Mode.Single ? _locItems : _picked);
                    txtCnt.Text = (_mode == Mode.Single ? _locItems.Count : _picked.Count).ToString();
                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);

                    // [320] 에서 부번을 들고 왔으면 바로 적용
                    if (_linkPtno != null && _linkPtno.Length > 0)
                    {
                        string p = _linkPtno, l = _linkLep;
                        _linkPtno = null; _linkLep = null;
                        LocMoveItem hit = FindItem(p, l);
                        if (hit != null) { txtPart.Text = PartNo.Display(p); ApplyItem(hit); return; }
                    }

                    txtPart.Focus();
                });
        }

        // ALL : 원본 "로케이션에 매칭된 N건의 품목을 이동하시겠습니까?"
        private void AfterLocAll()
        {
            FillGrid(_locItems);
            txtCnt.Text = _locItems.Count.ToString();

            for (int i = 0; i < _locItems.Count; i++)
            {
                LocMoveItem it = (LocMoveItem)_locItems[i];
                if (it.Sal > 0)
                {
                    Report(MP_SALQT, "출고대기수량이 있는 품목은 이동할 수 없습니다.", MsgLevel.Warn);
                    Msg("출고대기 품목 : " + it.Lep + " " + PartNo.Display(it.Ptno), MsgLevel.Warn);
                    FocusSel(txtLocFr);
                    return;
                }
            }

            End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);

            if (!Confirm("로케이션에 매칭된 " + _locItems.Count + "건의 품목을 이동하시겠습니까?"))
            {
                FocusSel(txtLocFr);
                return;
            }

            _allReady = true;
            txtLocTo.Enabled = true;
            txtLocTo.Focus();
            Msg("이동할 LOC 를 스캔하세요.", MsgLevel.Info);
        }

        private LocMoveItem FindItem(string ptno, string lep)
        {
            string key = PartNo.Key(ptno);
            LocMoveItem first = null;
            for (int i = 0; i < _locItems.Count; i++)
            {
                LocMoveItem it = (LocMoveItem)_locItems[i];
                if (it.Ptno != key) continue;
                if (lep != null && lep.Length > 0 && it.Lep == lep) return it;
                if (first == null) first = it;
            }
            return first;
        }

        // ------------------------------------------------------------------
        // 2. 부번 (원본 fn_SearchPtnoFr) : 서버를 다시 부르지 않고 LOC 목록에서 찾는다
        // ------------------------------------------------------------------
        private void PartSearch()
        {
            if (_busy || _mode == Mode.All || _transfer) return;

            if (_locItems.Count == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocFr); return; }

            string key = PartNo.Key(txtPart.Text);
            if (key.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            txtPart.Text = PartNo.Display(key);

            ArrayList hits = new ArrayList();
            for (int i = 0; i < _locItems.Count; i++)
            {
                LocMoveItem it = (LocMoveItem)_locItems[i];
                if (it.Ptno == key) hits.Add(it);
            }

            if (hits.Count == 0) { Report(MP_NOPINFO, "부품정보를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            // 계열이 둘 이상이면 고르게 한다 (원본 lep_popup)
            int idx = HaimsPda.Controls.LepSelect.Pick(hits, "계열 선택 - " + PartNo.Display(key));
            if (idx < 0) { Msg("취소했습니다.", MsgLevel.Info); FocusSel(txtPart); return; }

            ApplyItem((LocMoveItem)hits[idx]);
        }

        private void ApplyItem(LocMoveItem it)
        {
            _lep = it.Lep;
            lblPrefix.Text = it.Lep;
            txtPart.Text = PartNo.Display(it.Ptno);
            lblClass.Text = it.Class_;
            txtAvlFr.Text = it.AvlQty;
            txtOutQty.Text = it.SalQty;

            if (it.Sal > 0)
            {
                Report(MP_SALQT, "출고대기수량이 있어 이동할 수 없습니다.", MsgLevel.Warn);
                FocusSel(txtPart);
                return;
            }

            if (_mode == Mode.Multi)
            {
                for (int i = 0; i < _picked.Count; i++)
                {
                    LocMoveItem p = (LocMoveItem)_picked[i];
                    if (p.Ptno == it.Ptno && p.Lep == it.Lep)
                    {
                        Msg("이미 목록에 있는 부품입니다.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }
                }
            }

            CheckChain(it, delegate
            {
                if (_mode == Mode.Single)
                {
                    SetToItem(it);
                    txtCnt.Text = "1";
                    txtLocTo.Enabled = true;
                    txtLocTo.Focus();
                    Msg("이동할 LOC 를 스캔하세요.", MsgLevel.Info);
                }
                else
                {
                    _picked.Add(it);
                    FillGrid(_picked);
                    txtCnt.Text = _picked.Count.ToString();
                    ResetPartFields();
                    txtPart.Focus();
                    Msg("목록 " + _picked.Count + "건. 부번을 계속 스캔하거나 [불출]을 누르세요.", MsgLevel.Info);
                }
            });
        }

        // 체인파트 확인 (원본 fn_Search_ChainPartAf). 조회 실패는 이동을 막지 않는다.
        private void CheckChain(LocMoveItem it, Step next)
        {
            string wh = CurWh, lep = it.Lep, ptno = it.Ptno;

            Begin("체인파트 확인중...");
            Async.Run(this,
                delegate { return LocMoveService.GetChainParts(wh, lep, ptno); },
                delegate(object r, Exception ex)
                {
                    End("", MsgLevel.Info);
                    if (ex == null)
                    {
                        ArrayList chain = (ArrayList)r;
                        if (chain.Count > 0)
                        {
                            string text = "체인파트가 있습니다.\r\n";
                            for (int i = 0; i < chain.Count; i++) text += "\r\n" + chain[i];
                            MessageBox.Show(text, "[401] " + ScreenName);
                        }
                    }
                    next();
                });
        }

        // ------------------------------------------------------------------
        // 3. 이동 LOC (원본 CheckLoc)
        // ------------------------------------------------------------------
        private void LocToEnter()
        {
            if (_busy) return;

            string to = Loc.Key(txtLocTo.Text);
            if (to.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocTo); return; }
            txtLocTo.Text = Loc.Display(to);

            if (to == _locKey)
            {
                ReportText("출발 LOC 와 이동 LOC 가 같습니다.", MsgLevel.Warn);
                FocusSel(txtLocTo);
                return;
            }

            if (_mode == Mode.All)
            {
                if (!_allReady || _locItems.Count == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocFr); return; }
                SaveAll(to);
                return;
            }

            if (_toItem == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); txtPart.Focus(); return; }

            txtQtyTo.Focus();
            Msg("수량을 확인하고 Enter 를 누르세요.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 4. 저장 (원본 fn_SaveLoc) - 싱글 / 멀티 한 건
        // ------------------------------------------------------------------
        private void SaveOne()
        {
            if (_busy || _mode == Mode.All) return;
            if (_toItem == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); return; }

            string to = Loc.Key(txtLocTo.Text);
            if (to.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocTo); return; }
            if (to == _locKey) { ReportText("출발 LOC 와 이동 LOC 가 같습니다.", MsgLevel.Warn); FocusSel(txtLocTo); return; }

            LocMoveItem it = _toItem;
            string wh = CurWh, from = _locKey, qty = it.AvlQty;

            Begin("저장중...");
            Async.Run(this,
                delegate { LocMoveService.Move(wh, from, to, it.Lep, it.Ptno, qty); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.") + "\r\n" + ex.Message, MsgLevel.Error);
                        return;
                    }

                    if (_mode == Mode.Multi && _transfer)
                    {
                        _picked.Remove(it);
                        _locItems.Remove(it);
                        if (_picked.Count > 0)
                        {
                            FillGrid(_picked);
                            txtCnt.Text = _picked.Count.ToString();
                            End("저장 완료. 남은 " + _picked.Count + "건 - 이동할 LOC 를 스캔하세요.", MsgLevel.Success);
                            LoadTransferRow(0);
                            return;
                        }
                    }

                    Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);
                    InitKeep();
                });
        }

        // ALL : 품목마다 싱글 저장을 반복한다
        private void SaveAll(string to)
        {
            ArrayList items = (ArrayList)_locItems.Clone();
            string wh = CurWh, from = _locKey;

            Begin("저장중... (" + items.Count + "건)");
            Async.Run(this,
                delegate
                {
                    ArrayList failed = new ArrayList();
                    for (int i = 0; i < items.Count; i++)
                    {
                        LocMoveItem it = (LocMoveItem)items[i];
                        try { LocMoveService.Move(wh, from, to, it.Lep, it.Ptno, it.AvlQty); }
                        catch (Exception e) { failed.Add(it.Lep + " " + PartNo.Display(it.Ptno) + " : " + e.Message); }
                    }
                    return failed;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList failed = (ArrayList)r;
                    if (failed.Count == 0)
                    {
                        Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);
                        InitKeep();
                        return;
                    }

                    string text = (items.Count - failed.Count) + "/" + items.Count + "건 저장. 실패 " + failed.Count + "건\r\n";
                    for (int i = 0; i < failed.Count && i < 8; i++) text += "\r\n" + failed[i];
                    ReportText(text, MsgLevel.Error);

                    // 남은 재고를 다시 보여 준다
                    _allReady = false;
                    LocSearch();
                });
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        // 싱글 -> 멀티 -> ALL -> 싱글 (원본 OnBtnAll)
        private void OnMode(object sender, EventArgs e)
        {
            if (_busy) return;
            if (_mode == Mode.Single) _mode = Mode.Multi;
            else if (_mode == Mode.Multi) _mode = Mode.All;
            else _mode = Mode.Single;
            InitKeep();
        }

        // 내역 : 조회한 LOC 의 품목에서 고른다 (원본 [322] 상세내역 대체)
        private void OnList(object sender, EventArgs e)
        {
            if (_busy || _mode == Mode.All || _transfer) return;
            if (_locItems.Count == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocFr); return; }

            ArrayList items = _locItems;
            int idx;
            if (items.Count == 1) idx = 0;
            else idx = HaimsPda.Controls.LepSelect.Pick(items, "LOC 내역 - " + Loc.Display(_locKey));
            if (idx < 0) return;

            ApplyItem((LocMoveItem)items[idx]);
        }

        // 불출 : 멀티 목록 저장 시작 (원본 OnBtnMove)
        private void OnMove(object sender, EventArgs e)
        {
            if (_busy) return;
            if (_mode != Mode.Multi) { Msg("[불출]은 멀티 모드에서 사용합니다.", MsgLevel.Info); return; }
            if (_picked.Count == 0) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            _transfer = true;
            txtLocFr.Enabled = false;
            txtPart.Enabled = false;
            txtLocTo.Enabled = true;
            LoadTransferRow(0);
        }

        // 삭제 : 멀티 목록에서 선택 행 제거 (원본 OnBtnDel)
        private void OnDelete(object sender, EventArgs e)
        {
            if (_busy) return;
            if (_mode != Mode.Multi) { Msg("[삭제]는 멀티 모드에서 사용합니다.", MsgLevel.Info); return; }
            if (lstList.SelectedIndices.Count == 0) { Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn); return; }

            if (!Confirm(CommonCache.Msg(MP_DEL, "삭제하시겠습니까?"))) return;

            int idx = lstList.SelectedIndices[0];
            LocMoveItem it = lstList.Items[idx].Tag as LocMoveItem;
            if (it != null) _picked.Remove(it);
            FillGrid(_picked);
            txtCnt.Text = _picked.Count.ToString();

            if (_transfer)
            {
                if (_picked.Count == 0) { InitKeep(); Msg("목록이 비었습니다.", MsgLevel.Info); return; }
                LoadTransferRow(0);
            }
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "입력한 내용을 지우시겠습니까?"))) return;
            InitKeep();
            Msg("초기화", MsgLevel.Info);
        }

        // PART : [321] 파트별재고 (원본 OnBtnPrt)
        private void OnPart(object sender, EventArgs e)
        {
            if (_busy) return;

            NavArgs a = new NavArgs();
            string key = PartNo.Key(txtPart.Text);
            if (key.Length == 0 && _toItem != null) key = _toItem.Ptno;
            if (key.Length > 0)
            {
                a.Set("LEP", (_toItem != null) ? _toItem.Lep : _lep);
                a.Set("PTNO", key);
                a.Set("CLASS", lblClass.Text);
            }
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        // 멀티 진행 중 목록을 누르면 그 품목으로 바꾼다 (원본 스킵 대체)
        private void OnListSelected(object sender, EventArgs e)
        {
            if (!_transfer || _busy || lstList.SelectedIndices.Count == 0) return;
            LocMoveItem it = lstList.Items[lstList.SelectedIndices[0]].Tag as LocMoveItem;
            if (it == null || it == _toItem) return;
            SetToItem(it);
            txtLocTo.Text = "";
            txtLocTo.Focus();
        }

        private void LoadTransferRow(int i)
        {
            if (i < 0 || i >= _picked.Count) return;
            SetToItem((LocMoveItem)_picked[i]);
            if (i < lstList.Items.Count) lstList.Items[i].Selected = true;
            txtLocTo.Text = "";
            txtLocTo.Focus();
            Msg(PartNo.Display(_toItem.Ptno) + " - 이동할 LOC 를 스캔하세요.", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        // 화면 상태
        // ------------------------------------------------------------------
        private void SetToItem(LocMoveItem it)
        {
            _toItem = it;
            lblPrefixTo.Text = it.Lep;
            txtPartTo.Text = PartNo.Display(it.Ptno);
            lblClassTo.Text = it.Class_;
            txtQtyTo.Text = it.AvlQty;
        }

        private void FillGrid(ArrayList rows)
        {
            lstList.BeginUpdate();
            try
            {
                lstList.Items.Clear();
                for (int n = 0; n < rows.Count; n++)
                {
                    LocMoveItem it = (LocMoveItem)rows[n];
                    ListViewItem li = new ListViewItem(it.Lep);
                    li.SubItems.Add(PartNo.Display(it.Ptno));
                    li.SubItems.Add(it.AvlQty);
                    li.SubItems.Add(it.SalQty);
                    li.SubItems.Add(it.Class_);
                    li.Tag = it;
                    lstList.Items.Add(li);
                }
            }
            finally { lstList.EndUpdate(); }
        }

        private void ResetPartFields()
        {
            _lep = "H";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblClass.Text = "";
            txtAvlFr.Text = "";
            txtOutQty.Text = "";
        }

        private void ResetToFields()
        {
            _toItem = null;
            lblPrefixTo.Text = "H";
            txtPartTo.Text = "";
            lblClassTo.Text = "";
            txtLocTo.Text = "";
            txtQtyTo.Text = "";
        }

        /// <summary>처음 상태 (창고 콤보 포함)</summary>
        private void InitAll()
        {
            cboWh.Items.Clear();
            InitKeep();
        }

        /// <summary>원본 fn_Init : 창고와 모드는 두고 나머지를 비운다</summary>
        private void InitKeep()
        {
            _locItems = new ArrayList();
            _locKey = "";
            _picked.Clear();
            _transfer = false;
            _allReady = false;

            txtCnt.Text = "";
            txtLocFr.Text = "";
            ResetPartFields();
            ResetToFields();
            lstList.Items.Clear();

            btnMode.Text = (_mode == Mode.Single) ? "싱글" : (_mode == Mode.Multi) ? "멀티" : "ALL";

            bool all = (_mode == Mode.All);
            txtLocFr.Enabled = true;
            txtPart.Enabled = !all;
            txtLocTo.Enabled = false;      // 대상이 정해지면 연다
            btnList.Enabled = !all;
            btnMove.Enabled = (_mode == Mode.Multi);
            btnDel.Enabled = (_mode == Mode.Multi);

            _focus = "";
            txtLocFr.Focus();
        }

        private void FocusSel(TextBox t)
        {
            if (!t.Enabled) return;
            t.Focus();
            t.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[401] " + ScreenName,
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
            if (code != MP_OK && code != MP_SAVED) MessageBox.Show(text, "[401] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[401] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnMode.Enabled = on;
            btnPart.Enabled = on;
            btnClear.Enabled = on;
            btnList.Enabled = on && _mode != Mode.All && !_transfer;
            btnMove.Enabled = on && _mode == Mode.Multi && !_transfer;
            btnDel.Enabled = on && _mode == Mode.Multi;
        }
    }
}
