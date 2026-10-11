using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [210] 피킹실적처리 / [280] 리스트피킹 (같은 화면 클래스, 280 은 남은 라인 목록을 보여 준다)
    // 원본 웹화면 : /ui/ws/plus/PL210_W01.xml (P121), PL280_W01.xml (P147)
    //
    //   진입(201/202 의 업체) -> fn_Search : PL210_W01_S02 -> 라인 목록 (LOC, 부번 순)
    //   DisplayWork : 같은 LOC+계열+부번 라인을 한 묶음으로 보고, 묶음마다
    //       PL200_W01_S01(선점판정) -> E : 이미 처리됨(목록에서 뺌) / A : 타인 선점(건너뜀)
    //                              -> C : PL200_W01_U01 로 선점하고 그 묶음을 화면에 띄운다
    //   LOC·부번 스캔으로 확인 -> 저장 : PL210_W01_U01 (+ 할당번호 있으면 PL200_W01_U02)
    //       반품 수량을 받아 온 경우([211]) 저장 전에 PL100_W01_P01(반품전표) 먼저
    //   SKIP : 지금 묶음 선점 해제(PL200_W01_U03) 후 다음 묶음
    //   끝 : 모두 처리하면 MP547 후 이전 화면, SKIP 이 남으면 MP548 확인
    //   뒤로 나갈 때 화면에 묶음이 떠 있으면 선점을 해제한다 (원본 DestroyWindow)
    //
    // HAR(2026-09-20) 실측 : S02 -> S01(PREOCC=C) -> U01 -> 저장 U01, 저장 후 201 재조회 0건.
    // PICDT/PICTM 은 원본처럼 화면을 연 시각을 보낸다(실측에서도 저장 시각이 아니라 조회 시각이었다).
    //
    // 원본과 다르게 한 부분
    //   - 창고/판매구분/차량번호는 콤보가 아니라 표시만 한다 (원본도 바꿀 수 없게 쓴다).
    //   - TRS_GRTNO 는 행의 값(빈 값)을 보낸다. 원본은 빈 콤보 getValue() 가 문자열 "null" 로 나갔다(HAR).
    //   - 부번 스캔은 원본 210 이 소리만 내고 끝낸다. 여기서는 일치/불일치를 푸터에 띄운다.
    //   - [211] 반품 수량은 원본처럼 페이지를 다시 읽지 않고, 지금 라인에 바로 반영한다.
    //   - 280 원본 DisplayWork 는 for 문 i++ 와 i += nDouble 이 겹쳐 처리된 묶음 다음 행을 하나 건너뛴다.
    //     210 과 같은 로직으로 통일했다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S210_Picking.Designer.cs 에서 관리한다.
    public partial class S210_Picking : WorkScreen
    {
        private NavArgs _link;
        private ArrayList _rows = new ArrayList();   // OutRow (원본 hidGrd)
        private ArrayList _dbl = new ArrayList();    // 지금 묶음의 행 인덱스 (원본 m_strarrDouble)
        private int _skip, _work, _total;
        private string _vchIn = "", _wsfIn = "";
        private bool _control;                       // [211] 반품 수량을 받아 옴 (원본 m_bControl)
        private bool _saved;
        private bool _showing;                       // 묶음이 화면에 떠 있다(선점 중)
        private string _picDt, _picTm;
        private string _focus = "";

        public override int ScreenNo { get { return ScreenId.Picking; } }
        public override string ScreenName { get { return "피킹실적처리"; } }

        /// <summary>280 리스트피킹이면 true : 남은 라인 목록을 보여 준다</summary>
        protected virtual bool ListMode { get { return false; } }
        protected virtual string Pgm { get { return OutboundService.Pgm210; } }

        /// <summary>조회·저장 SQL 의 갈래</summary>
        protected enum PickKind
        {
            Agency,   // 210/280 : PL210_W01_S02 / PL210_W01_U01 (+PL200_W01_U02)
            Shop,     // 230/281 : PL230_W01_S01(+AREA/RUTCD) / PL240_W01_U01 (+PL200_W01_U02)
            Loc       // 240     : PL240_W01_S01 / PL240_W01_U01 + U02 (TAG 필수)
        }
        protected virtual PickKind Kind { get { return PickKind.Agency; } }


        public S210_Picking()
        {
            InitializeComponent();
            if (IsDesignMode) return;
            WinApi.GridLines(lstLines.Handle, true);
        }

        public override NavArgs BackResult { get { return _saved ? NavResult.Saved(ScreenNo) : null; } }

        public override void OnEnter(NavArgs args)
        {
            _link = args;
            lstLines.Visible = ListMode;
            if (ListMode) btnStock.Text = "파트";

            DateTime now = DateTime.Now;
            _picDt = now.ToString("yyyyMMdd");
            _picTm = now.ToString("HHmmss");

            bool loc = (Kind == PickKind.Loc);
            lblTagCap.Visible = loc;
            txtTag.Visible = loc;
            lblGrtCap.Visible = !loc;
            lblGrtno.Visible = !loc;
            txtTag.Text = "";

            string cust = Arg(args, "CUSTCODE");
            lblVndmn.Text = cust;
            lblVndnm.Text = Arg(args, "CUSTNAME");
            if (loc)
            {
                // 원본 240 : 업체 코드가 1개(4자리)면 코드/이름, 여럿이면 "다중거래처"
                string c = cust.Replace("'", "");
                if (c.Length == 4) { lblVndmn.Text = c; lblVndnm.Text = Arg(args, "CUSTNAME").Replace("'", ""); }
                else { lblVndmn.Text = ""; lblVndnm.Text = "다중거래처"; }
            }
            lblWh.Text = Arg(args, "TRS_WHSFL");
            lblSalgb.Text = Arg(args, "TRS_SALGBNM");
            ClearPart();

            if (cust.Length == 0)
            {
                SetButtons(false);
                Msg((Kind == PickKind.Agency ? "[201] 피킹대상" : "[204] 피킹대상(전문점)") + "에서 업체를 골라 진입하세요.",
                    MsgLevel.Warn);
                return;
            }
            Search();
        }

        // 원본 DestroyWindow : 화면에 떠 있는 묶음의 선점을 풀고 나간다
        public override bool OnBack()
        {
            if (_showing) ReleaseAsync();
            _showing = false;
            return true;
        }

        private void ReleaseAsync()
        {
            string pgm = Pgm, vch = _vchIn, ptno = txtPart.Text, lep = lblPrefix.Text;
            if (vch.Length == 0) return;
            System.Threading.Thread t = new System.Threading.Thread(delegate()
            {
                try { OutboundService.Preoccupy(pgm, false, vch, ptno, lep); } catch { }
            });
            t.IsBackground = true;
            t.Start();
        }

        // [211] 반품에서 돌아옴 : 출고수량(QTY) / 반품수량(OSNDQTY)
        public override void OnReturn(NavArgs result)
        {
            if (result == null || Arg(result, "OSNDQTY").Length == 0) return;
            if (_skip >= _rows.Count) return;

            OutRow row = (OutRow)_rows[_skip];
            row["TRS_SALQT"] = Arg(result, "QTY");
            row["CNCL_SALQT"] = Arg(result, "OSNDQTY");
            txtQty.Text = Arg(result, "QTY");
            _control = true;
            RefreshList();
            Msg("반품 " + Arg(result, "OSNDQTY") + " / 출고 " + Arg(result, "QTY") + " — [저장] 하면 반품전표가 함께 처리됩니다.",
                MsgLevel.Info);
            txtQty.Focus();
        }

        // ------------------------------------------------------------------
        // 조회 (fn_Search)
        // ------------------------------------------------------------------
        private void Search()
        {
            string wh = Arg(_link, "TRS_WHSFL"), salgb = Arg(_link, "TRS_SALGB");
            string from = Arg(_link, "FROMDT"), to = Arg(_link, "TODT"), vnd = Arg(_link, "CUSTCODE");
            string area = Arg(_link, "AREA"), line = Arg(_link, "LINE");
            string pgm = Pgm;
            PickKind kind = Kind;

            Run("조회중...",
                delegate
                {
                    if (kind == PickKind.Shop)
                        return OutboundService.SearchShopPickLines(pgm, "fn_Search", wh, salgb, from, to, vnd, area, line);
                    if (kind == PickKind.Loc)
                        return OutboundService.SearchLocPickLines(wh, salgb, from, to, vnd, area, line);
                    return OutboundService.SearchPickLines(pgm, "fn_Search", wh, salgb, from, to, vnd);
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _rows = (ArrayList)r;
                    _total = _rows.Count;
                    _skip = 0;
                    if (_rows.Count == 0)
                    {
                        Report("MP322", "피킹 대상이 없습니다.", MsgLevel.Warn);
                        return;
                    }
                    ShowCounts();
                    End("", MsgLevel.Info);
                    DisplayWork();
                });
        }

        // ------------------------------------------------------------------
        // DisplayWork : 선점 판정을 묶음 단위로 돌려 다음 작업 묶음을 찾는다
        // ------------------------------------------------------------------
        private sealed class WorkResult
        {
            public bool Ok;
            public int Skip;
            public ArrayList Done = new ArrayList();   // E : 이미 처리된 행 인덱스
        }

        private void DisplayWork()
        {
            string pgm = Pgm;
            ArrayList rows = _rows;
            int start = _skip;

            Run("선점 확인중...",
                delegate
                {
                    WorkResult w = new WorkResult();
                    w.Skip = start;
                    int i = start;
                    while (i < rows.Count)
                    {
                        OutRow o = (OutRow)rows[i];
                        ArrayList dbl = Doubles(rows, i, o["TRS_LEP"], o["TRS_PTNO"], o["TRS_LOCNO"]);
                        string vch = VchIn(rows, dbl);
                        string p = OutboundService.SearchPreoccupy(pgm, vch, o["TRS_PTNO"], o["TRS_LEP"]);
                        if (p == null) { w.Ok = false; break; }
                        w.Ok = true;
                        if (p == "E")
                            w.Done.AddRange(dbl);
                        else if (p == "C")
                        {
                            w.Ok = OutboundService.Preoccupy(pgm, true, vch, o["TRS_PTNO"], o["TRS_LEP"]);
                            break;
                        }
                        // A : 다른 사람이 잡고 있다 -> 건너뜀
                        i += dbl.Count;
                        w.Skip = i;
                    }
                    return w;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    WorkResult w = (WorkResult)r;
                    _skip = w.Skip;
                    _work += w.Done.Count;

                    if (!w.Ok)
                    {
                        _showing = false;
                        ClearPart();
                        Report("MP552", "선점할 대상이 없습니다.", MsgLevel.Warn);
                        Finish();
                        return;
                    }

                    // 처리된(E) 행은 목록에서 뺀다
                    w.Done.Sort();
                    for (int k = w.Done.Count - 1; k >= 0; k--)
                    {
                        int idx = (int)w.Done[k];
                        _rows.RemoveAt(idx);
                        if (idx < _skip) _skip--;
                    }

                    End("", MsgLevel.Info);
                    if (_skip >= _rows.Count) { _showing = false; EndCheck(); return; }
                    ShowCurrent();
                });
        }

        /// <summary>원본 GetDoubleRecord : start 부터 끝까지 같은 LOC+계열+부번 행</summary>
        private static ArrayList Doubles(ArrayList rows, int start, string lep, string ptno, string loc)
        {
            ArrayList d = new ArrayList();
            string p = PartNo.Key(ptno), l = Loc.Key(loc);
            for (int i = start; i < rows.Count; i++)
            {
                OutRow o = (OutRow)rows[i];
                if (o["TRS_LEP"] == lep && PartNo.Key(o["TRS_PTNO"]) == p && Loc.Key(o["TRS_LOCNO"]) == l) d.Add(i);
            }
            return d;
        }

        private static string VchIn(ArrayList rows, ArrayList dbl)
        {
            ArrayList k = new ArrayList();
            for (int i = 0; i < dbl.Count; i++)
                k.Add(OutboundService.VchKey(((OutRow)rows[(int)dbl[i]])["TRS_VCHNO"]));
            return OutboundService.InList(k);
        }

        private static string WsfIn(ArrayList rows, ArrayList dbl)
        {
            ArrayList k = new ArrayList();
            for (int i = 0; i < dbl.Count; i++)
            {
                string w = ((OutRow)rows[(int)dbl[i]])["TRS_WSFNO"].Trim();
                if (w.Length > 0) k.Add(w);
            }
            return OutboundService.InList(k);
        }

        private void ShowCurrent()
        {
            OutRow o = (OutRow)_rows[_skip];
            _dbl = Doubles(_rows, _skip, o["TRS_LEP"], o["TRS_PTNO"], o["TRS_LOCNO"]);
            _vchIn = VchIn(_rows, _dbl);
            _wsfIn = WsfIn(_rows, _dbl);

            int qty = 0;
            for (int i = 0; i < _dbl.Count; i++) qty += ToInt(((OutRow)_rows[(int)_dbl[i]])["TRS_SALQT"]);

            lblPrefix.Text = o["TRS_LEP"].Length > 0 ? o["TRS_LEP"] : "H";
            txtPart.Text = PartNo.Display(o["TRS_PTNO"]);
            lblClass.Text = o["UWMG_GRADE"];
            lblPartName.Text = o["INV_PTNM_A"];
            lblAvl.Text = Num(o["LOC_AVLQT"]);
            txtQty.Text = qty.ToString();
            lblGrtno.Text = o["TRS_GRTNO"];
            lblSalgb.Text = SalgbName(o["TRS_SALGB"], lblSalgb.Text);
            string loc = o["TRS_LOCNO"].Replace("-", "");
            txtLoc.Text = (loc.Length == 0) ? "NONE" : Loc.Display(loc);
            lblSalCnt.Text = _dbl.Count.ToString();
            _showing = true;
            ShowCounts();
            RefreshList();

            Msg("LOC 을 확인(스캔)하세요.", MsgLevel.Info);
            txtLoc.Focus();
            txtLoc.SelectAll();
        }

        public static string SalgbName(string code, string def)
        {
            switch (code)
            {
                case "1": return "고객";
                case "2": return "계약";
                case "3": return "보험";
                case "4": return "일반";
            }
            return (code == null || code.Length == 0) ? def : code;
        }

        private void ShowCounts()
        {
            lblWork.Text = _work + "/" + _total;
        }

        private void RefreshList()
        {
            if (!ListMode) return;
            lstLines.BeginUpdate();
            try
            {
                lstLines.Items.Clear();
                for (int i = 0; i < _rows.Count; i++)
                {
                    OutRow o = (OutRow)_rows[i];
                    ListViewItem it = new ListViewItem(Loc.Display(o["TRS_LOCNO"]));
                    it.SubItems.Add(o["TRS_LEP"]);
                    it.SubItems.Add(PartNo.Display(o["TRS_PTNO"]));
                    it.SubItems.Add(Num(o["TRS_SALQT"]));
                    it.SubItems.Add(Num(o["LOC_AVLQT"]));
                    it.SubItems.Add(o["TRS_VCHNO"]);
                    lstLines.Items.Add(it);
                }
            }
            finally { lstLines.EndUpdate(); }
            if (_skip < lstLines.Items.Count)
            {
                lstLines.Items[_skip].Selected = true;
                lstLines.EnsureVisible(_skip);
            }
        }

        // ------------------------------------------------------------------
        // 끝 판정 (EndCheck)
        // ------------------------------------------------------------------
        private void EndCheck()
        {
            if (_skip > 0)
            {
                if (_total == _work + _skip)
                {
                    string m = CommonCache.Msg("MP548", "SKIP 한 %d 건이 있습니다. 작업을 종료하시겠습니까?")
                                          .Replace("%d", _skip.ToString());
                    if (Confirm(m)) { ClearPart(); Finish(); }
                    else { _skip = 0; DisplayWork(); }
                }
                else DisplayWork();
            }
            else
            {
                if (_total == _work)
                {
                    ClearPart();
                    Report("MP547", "피킹 작업이 완료되었습니다.", MsgLevel.Success);
                    Finish();
                }
                else DisplayWork();
            }
        }

        /// <summary>원본 EndDialog : 이전 화면으로</summary>
        private void Finish()
        {
            _showing = false;
            Shell.GoBack();
        }

        // ------------------------------------------------------------------
        // LOC / 부번 확인 (fn_CheckLocno / fn_CheckPtno)
        // ------------------------------------------------------------------
        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            CheckLoc();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            CheckPart(txtPart.Text);
        }

        // ------------------------------------------------------------------
        // [240] TAG (fn_SearchTag : PL240_W01_S02)
        // ------------------------------------------------------------------
        private void OnTagFocus(object sender, EventArgs e) { _focus = "TAG"; }

        private void OnTagKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            CheckTag();
        }

        /// <summary>plus.js gfn_DispTag_Plus : 16자리면 6-4-1-나머지</summary>
        private static string DispTag(string v)
        {
            v = (v == null) ? "" : v.Trim().Replace("-", "");
            if (v.Length == 16) v = v.Substring(0, 6) + "-" + v.Substring(6, 4) + "-" + v.Substring(10, 1) + "-" + v.Substring(11);
            return v.ToUpper();
        }

        private void CheckTag()
        {
            if (Busy) return;
            txtTag.Text = DispTag(txtTag.Text);
            string tag = txtTag.Text.Trim();
            if (tag.Length == 0) { Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn); return; }
            Run("TAG 확인중...",
                delegate { return OutboundService.SearchLocTag(tag); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    OutRow o = (OutRow)r;
                    // 원본은 USE_YN 을 응답이 아닌 common/ds_LinkInfo 에서 읽어 사실상 검사하지 않는다. 응답 값으로 본다.
                    if (o == null || o["USE_YN"] == "N")
                    {
                        Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn);
                        txtTag.Focus();
                        txtTag.SelectAll();
                        return;
                    }
                    End("TAG 확인. LOC 을 확인(스캔)하세요.", MsgLevel.Success);
                    txtLoc.Focus();
                });
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (Busy) return;
            if (_focus == "TAG" && Kind == PickKind.Loc)
            {
                txtTag.Text = data.Text;
                CheckTag();
                return;
            }
            if (!_showing) return;
            if (_focus == "LOCNO")
            {
                txtLoc.Text = Loc.Display(data.Text);
                CheckLoc();
            }
            else
            {
                if (ListMode) txtPart.Text = PartNo.Display(data.Text);
                CheckPart(data.Text);
            }
        }

        private string ExpectLoc { get { return Loc.Key(((OutRow)_rows[_skip])["TRS_LOCNO"]); } }

        private bool CheckLoc()
        {
            if (!_showing) return false;
            string loc = Loc.Key(txtLoc.Text);
            if (loc == "NONE") loc = "";
            if (loc == ExpectLoc)
            {
                Msg("LOC 확인. 부품을 스캔하세요.", MsgLevel.Success);
                txtPart.Focus();
                return true;
            }
            string expect = ExpectLoc;
            txtLoc.Text = expect.Length == 0 ? "NONE" : Loc.Display(expect);
            Report("MP553", "LOC 가 일치하지 않습니다.", MsgLevel.Warn);
            txtLoc.Focus();
            return false;
        }

        private bool CheckPart(string scanned)
        {
            if (!_showing) return false;
            OutRow o = (OutRow)_rows[_skip];
            if (PartNo.Key(PartNo.Display(scanned)) == PartNo.Key(o["TRS_PTNO"]))
            {
                txtPart.Text = PartNo.Display(o["TRS_PTNO"]);
                Msg("부품 확인. 수량을 확인하고 [저장] 하세요.", MsgLevel.Success);
                txtQty.Focus();
                txtQty.SelectAll();
                return true;
            }
            txtPart.Text = PartNo.Display(o["TRS_PTNO"]);
            Report("MP554", "부품번호가 일치하지 않습니다.", MsgLevel.Warn);
            txtPart.Focus();
            return false;
        }

        // ------------------------------------------------------------------
        // 저장 (fn_Return -> fn_SavePicking)
        // ------------------------------------------------------------------
        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Save();
        }

        private void OnSave(object sender, EventArgs e) { Save(); }

        private void Save()
        {
            if (Busy) return;
            PickKind kind = Kind;

            // 원본 240 저장 : TAG(MP576) -> 부번(MP525) -> LOC(MP310) 순으로 막는다
            if (kind == PickKind.Loc && txtTag.Text.Trim().Length == 0)
            {
                Report("MP576", "TAG 를 확인하세요.", MsgLevel.Warn);
                txtTag.Focus();
                return;
            }
            if (!_showing || PartNo.Key(txtPart.Text).Length == 0)
            {
                if (ListMode || kind == PickKind.Loc) Report("MP525", "부품번호를 입력하십시오.", MsgLevel.Warn);
                else Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn);
                return;
            }
            if (kind == PickKind.Loc && txtLoc.Text.Trim().Length == 0)
            {
                Report("MP310", "LOC 을 입력하십시오.", MsgLevel.Warn);
                txtLoc.Focus();
                return;
            }

            OutRow cur = (OutRow)_rows[_skip];
            ArrayList dblRows = new ArrayList();
            for (int i = 0; i < _dbl.Count; i++) dblRows.Add(_rows[(int)_dbl[i]]);

            // [240] 저장 값
            string tag = txtTag.Text.Trim(), salCnt = _dbl.Count.ToString(), salQt = ToInt(txtQty.Text).ToString();

            OutboundService.PickSave s = new OutboundService.PickSave();
            s.SqlU01 = (kind == PickKind.Agency) ? "plus:PL210_W01_U01" : "plus:PL240_W01_U01";   // 230 은 PL240_W01_U01
            s.SendVchno = true;
            s.VchIn = _vchIn;
            s.Vndmn = Arg(_link, "CUSTCODE");
            s.Whsfl = Arg(_link, "TRS_WHSFL");
            s.Grtno = cur["TRS_GRTNO"];
            s.Ptno = cur["TRS_PTNO"];
            s.Lep = cur["TRS_LEP"];
            s.PicDt = _picDt;
            s.PicTm = _picTm;
            s.WsfIn = _wsfIn;

            bool control = _control;
            string pgm = Pgm;
            string retFail = CommonCache.Msg("MP614", "반품 처리에 실패하였습니다.");
            string saveFail = CommonCache.Msg("MP108", "저장에 실패하였습니다.");

            Run("저장중...",
                delegate
                {
                    bool noAgt = false;
                    if (control)
                    {
                        for (int i = 0; i < dblRows.Count; i++)
                        {
                            string outAgt;
                            try { outAgt = OutboundService.SaveReturn(pgm, (OutRow)dblRows[i]); }
                            catch (Exception) { throw new HaimsException(retFail); }
                            if (outAgt.Length == 0) noAgt = true;
                        }
                    }
                    try
                    {
                        if (kind == PickKind.Loc)
                            OutboundService.SaveLocPicking(s.VchIn, s.Ptno, s.Lep, s.PicDt, s.PicTm,
                                                           tag, salCnt, salQt, s.WsfIn);
                        else
                            OutboundService.SavePicking(pgm, s);
                    }
                    catch (Exception) { throw new HaimsException(saveFail); }
                    return noAgt;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _saved = true;
                    if ((bool)r) MessageBox.Show(CommonCache.Msg("MP593", "반품 전표의 출고 대리점 정보가 없습니다."), Caption);

                    // 원본 fn_SavePicking_After : 묶음 행을 지우고 작업 수를 올린다
                    _control = false;
                    _showing = false;
                    ArrayList d = (ArrayList)_dbl.Clone();
                    d.Sort();
                    for (int k = d.Count - 1; k >= 0; k--) { _rows.RemoveAt((int)d[k]); _work++; }
                    _dbl.Clear();
                    ClearPart();
                    ShowCounts();
                    Report("MP102", "정상 처리되었습니다.", MsgLevel.Success);
                    EndCheck();
                });
        }

        // ------------------------------------------------------------------
        // SKIP (OnBtnSkip)
        // ------------------------------------------------------------------
        private void OnSkip(object sender, EventArgs e)
        {
            if (Busy || !_showing) return;
            if (_control) { Report("MP549", "반품 처리 중에는 SKIP 할 수 없습니다.", MsgLevel.Warn); return; }

            int n = _dbl.Count;
            _skip += n;
            bool last = (_skip == _rows.Count);
            string q = last
                ? CommonCache.Msg("MP550", "마지막 항목입니다. 처음부터 다시 하시겠습니까?").Replace("아니오", "취소")
                : CommonCache.Msg("MP509", "SKIP 하시겠습니까?");

            if (!Confirm(q))
            {
                _skip -= n;
                if (last) { ReleaseAsync(); Finish(); }   // 원본 : 마지막에서 취소하면 화면을 닫는다
                return;
            }

            string pgm = Pgm, vch = _vchIn, ptno = txtPart.Text, lep = lblPrefix.Text;
            Run("선점 해제중...",
                delegate { return OutboundService.Preoccupy(pgm, false, vch, ptno, lep); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) { _skip -= n; return; }
                    if (!(bool)r)
                    {
                        _skip -= n;
                        Report("MP552", "선점 해제에 실패하였습니다.", MsgLevel.Warn);
                        return;
                    }
                    _showing = false;
                    if (last) _skip = 0;
                    End("", MsgLevel.Info);
                    DisplayWork();
                });
        }

        // ------------------------------------------------------------------
        // 반품 -> [211] (OnBtnOsd)  /  재고·파트 -> [321] (OnBtnPart)
        // ------------------------------------------------------------------
        private void OnReturnClick(object sender, EventArgs e)
        {
            if (Busy || !_showing) return;
            OutRow o = (OutRow)_rows[_skip];
            if (_dbl.Count > 1 || o["CANCEL_YN"] == "N")
            {
                Report("MP615", "반품할 수 없는 항목입니다.", MsgLevel.Warn);
                return;
            }
            NavArgs a = PartArgs(o);
            a.Set("SALENO", o["TRS_VCHNO"]);
            a.Set("LOCNO", Loc.Key(o["TRS_LOCNO"]));
            a.Set("EXPECTQTY", txtQty.Text.Trim());
            Shell.Navigate(ScreenId.PickReturn, a);
        }

        private void OnStock(object sender, EventArgs e)
        {
            if (Busy || !_showing) return;
            OutRow o = (OutRow)_rows[_skip];
            NavArgs a = PartArgs(o);
            if (ListMode) a.Set("LOCNO", Loc.Key(o["TRS_LOCNO"]));
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        private NavArgs PartArgs(OutRow o)
        {
            NavArgs a = new NavArgs();
            a.Set("LEP", o["TRS_LEP"]);
            a.Set("PTNO", PartNo.Key(o["TRS_PTNO"]));
            a.Set("PTNM", o["INV_PTNM_A"]);
            a.Set("CLASS", o["UWMG_GRADE"]);
            return a;
        }

        // ------------------------------------------------------------------
        private void ClearPart()
        {
            lblPrefix.Text = "H";
            txtLoc.Text = "";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            lblAvl.Text = "";
            txtQty.Text = "";
            lblSalCnt.Text = "";
            lblGrtno.Text = "";
        }

        protected override void SetButtons(bool on)
        {
            btnSave.Enabled = on;
            btnStock.Enabled = on;
            btnSkip.Enabled = on;
            btnReturn.Enabled = on;
        }
    }

    /// <summary>
    /// [280] 리스트피킹 : [210] 과 같은 처리에 남은 라인 목록을 함께 보여 준다.
    /// 원본 /ui/ws/plus/PL280_W01.xml (서버 메뉴 P147). 서버 호출(SQL, 파라미터)은 210 과 같다.
    /// </summary>
    public sealed class S280_ListPicking : S210_Picking
    {
        public override int ScreenNo { get { return ScreenId.ListPicking; } }
        public override string ScreenName { get { return "리스트피킹"; } }
        protected override bool ListMode { get { return true; } }
        protected override string Pgm { get { return OutboundService.Pgm280; } }
    }

    /// <summary>
    /// [230] 피킹실적처리(전문점) : 원본 /ui/ws/plus/PL230_W01.xml (서버 메뉴 P123), [204]/[202] 에서 진입.
    /// 210 과 같은 흐름. 조회가 PL230_W01_S01(+AREA / RUTCD, NONE 이면 RUTCD=""), 저장이 PL240_W01_U01 이다.
    /// 원본 230 DisplayWork 도 280 과 같은 i++ 중복이 있어 210 로직으로 통일했다. 실측 없음.
    /// </summary>
    public sealed class S230_ShopPicking : S210_Picking
    {
        public override int ScreenNo { get { return ScreenId.ShopPicking; } }
        public override string ScreenName { get { return "피킹실적처리(전문점)"; } }
        protected override string Pgm { get { return OutboundService.Pgm230; } }
        protected override PickKind Kind { get { return PickKind.Shop; } }
    }

    /// <summary>
    /// [281] 리스트피킹(전문점) : 원본 /ui/ws/plus/PL281_W01.xml (서버 메뉴 P187), [204]/[202] 에서 진입.
    /// 230 과 같은 SQL 에 남은 라인 목록을 함께 보여 준다. 실측 없음.
    /// </summary>
    public sealed class S281_ShopListPicking : S210_Picking
    {
        public override int ScreenNo { get { return ScreenId.ShopListPicking; } }
        public override string ScreenName { get { return "리스트피킹(전문점)"; } }
        protected override bool ListMode { get { return true; } }
        protected override string Pgm { get { return OutboundService.Pgm281; } }
        protected override PickKind Kind { get { return PickKind.Shop; } }
    }

    /// <summary>
    /// [240] LOC별 피킹실적처리 : 원본 /ui/ws/plus/PL240_W01.xml (서버 메뉴 P124), [204] 의 LOC피킹에서 진입.
    ///   여러 업체를 'a','b' 로 받아 PL240_W01_S01 -> ds_List03 으로 한꺼번에 LOC 순 피킹한다.
    ///   차량번호 자리에 TAG 입력이 있다 (PL240_W01_S02 로 확인).
    ///   저장 : TAG 없으면 MP576 / 부번 없으면 MP525 / LOC 없으면 MP310,
    ///          PL240_W01_U01 + U02 (TRS_TEMP_TAG, SAL_CNT=묶음 건수, SALQT=수량 포함).
    ///   선점(PL200_W01_*)·반품([211])·SKIP 은 210 과 같다.
    /// 원본과 다르게 한 부분 : TAG 의 USE_YN 을 응답에서 읽는다(원본은 엉뚱한 곳을 읽어 검사가 안 됨).
    /// 실측 없음.
    /// </summary>
    public sealed class S240_LocPicking : S210_Picking
    {
        public override int ScreenNo { get { return ScreenId.LocPicking; } }
        public override string ScreenName { get { return "LOC별 피킹실적처리"; } }
        protected override string Pgm { get { return OutboundService.Pgm240; } }
        protected override PickKind Kind { get { return PickKind.Loc; } }
    }
}
