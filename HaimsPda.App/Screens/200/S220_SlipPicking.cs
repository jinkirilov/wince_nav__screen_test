using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [220] 피킹(판매증표출고) : 출력된 판매증표 번호로 라인을 받아 LOC 순으로 피킹한다.
    // 원본 웹화면 : /ui/ws/plus/PL220_W01.xml (서버 메뉴 P122)
    //
    //   증표 입력(yyyyMM - 순번 5자리) -> PL220_W01_S01 -> ds_List01 (기존 목록 뒤에 붙인다, 원본 setXML append)
    //   멀티 -> [221] 에서 증표 여러 개를 골라 오면 목록을 새로 만들고 LOC 순 정렬
    //   [221] 에서 [출고] 로 들어오면 고른 증표들로 바로 시작
    //   LOC·부번 확인 -> 저장 : PL220_W01_U01 (+ 할당번호 있으면 PL200_W01_U02)
    //   201→210 과 달리 선점(PL200_W01_*) 단계가 없다.
    //   끝 : 모두 처리하면 MP602 후 화면 초기화, SKIP 이 남으면 MP548 확인
    //
    // 원본과 다르게 한 부분
    //   - 저장은 같은 LOC+계열+부번 행(여러 증표)을 한 번에 확정하는데 원본은 목록에서 한 행만 지운다.
    //     나머지 행이 다시 나와 같은 UPDATE 를 또 보내므로, 여기서는 묶음 행을 모두 지우고 그 수만큼 센다.
    //     SKIP 도 묶음 단위로 넘긴다.
    //   - 할당번호(TRS_WSFNO)는 원본이 지금 행 값만 반복해 넣는다. 묶음 각 행의 값을 넣는다.
    //   - 원본 SKIP 취소(m_nSkip = m_nSkip--)는 값이 그대로 남는 버그다. 여기서는 되돌린다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S220_SlipPicking.Designer.cs 에서 관리한다.
    public sealed partial class S220_SlipPicking : WorkScreen
    {
        private ArrayList _rows = new ArrayList();
        private ArrayList _dbl = new ArrayList();
        private int _skip, _work, _total;
        private string _picDt, _picTm;
        private string _focus = "";
        private bool _saved;

        public override int ScreenNo { get { return ScreenId.SlipPicking; } }
        public override string ScreenName { get { return "피킹(판매증표출고)"; } }
        public override NavArgs BackResult { get { return _saved ? NavResult.Saved(ScreenNo) : null; } }

        public S220_SlipPicking()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            DateTime now = DateTime.Now;
            _picDt = now.ToString("yyyyMMdd");
            _picTm = now.ToString("HHmmss");
            Init();

            string list = Arg(args, "VCHNOS");
            if (list.Length > 0) { MultiSearch(list); return; }
            string one = Arg(args, "VCHNO").Replace("-", "");
            if (one.Length == 11)
            {
                txtVch1.Text = one.Substring(0, 6);
                txtVch2.Text = one.Substring(6);
                SearchOne();
            }
        }

        // [221] 멀티 선택에서 돌아옴
        public override void OnReturn(NavArgs result)
        {
            string list = Arg(result, "VCHNOS");
            if (list.Length > 0) MultiSearch(list);
        }

        /// <summary>원본 fn_Init</summary>
        private void Init()
        {
            txtVch1.Text = DateTime.Now.ToString("yyyyMM");
            txtVch2.Text = "";
            lblVndmn.Text = "";
            lblVndnm.Text = "";
            lblWh.Text = "M";
            lblWork.Text = "0";
            lblSalgb.Text = "";
            lblGrtno.Text = "";
            lblNoarg.Text = "0";
            ClearPart();
            _rows = new ArrayList();
            _dbl = new ArrayList();
            _skip = 0;
            _total = 0;
            _work = 0;
            Msg("증표번호를 입력하세요.", MsgLevel.Info);
            txtVch2.Focus();
        }

        private void ClearPart()
        {
            txtLoc.Text = "";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblClass.Text = "";
            lblPartName.Text = "";
            lblAvl.Text = "";
            txtQty.Text = "";
        }

        // ------------------------------------------------------------------
        // 증표 조회
        // ------------------------------------------------------------------
        private void OnNumKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void OnVch1KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            txtVch2.Focus();
        }

        private void OnVch2KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            SearchOne();
        }

        private void OnSearch(object sender, EventArgs e) { SearchOne(); }

        private void SearchOne()
        {
            if (Busy) return;
            string s2 = txtVch2.Text.Trim();
            if (s2.Length > 0 && s2.Length < 5) s2 = s2.PadLeft(5, '0');
            txtVch2.Text = s2;
            string v = txtVch1.Text.Trim() + s2;
            if (v.Length != 11)
            {
                ReportText("증표번호를 확인하세요. (년월 6자리 + 순번 5자리)", MsgLevel.Warn);
                txtVch2.Focus();
                return;
            }
            ArrayList keys = new ArrayList();
            keys.Add(v);
            Load(keys, false, false);
        }

        /// <summary>원본 fn_MultiSearch : 목록을 비우고 증표마다 조회해 붙인 뒤 LOC 순 정렬</summary>
        private void MultiSearch(string list)
        {
            string[] parts = list.Split(',');
            ArrayList keys = new ArrayList();
            for (int i = 0; i < parts.Length; i++)
            {
                string k = OutboundService.VchKey(parts[i]);
                if (k.Length == 11) keys.Add(k);
            }
            if (keys.Count == 0) return;
            Load(keys, true, true);
        }

        private sealed class RowLocCompare : IComparer
        {
            public int Compare(object a, object b)
            {
                return string.Compare(((OutRow)a)["TRS_LOCNO"], ((OutRow)b)["TRS_LOCNO"], StringComparison.Ordinal);
            }
        }

        private void Load(ArrayList keys, bool reset, bool sort)
        {
            Run("조회중...",
                delegate
                {
                    ArrayList all = new ArrayList();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        string k = (string)keys[i];
                        all.AddRange(OutboundService.SearchSlipLines(k.Substring(0, 6), k.Substring(6)));
                    }
                    return all;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    ArrayList got = (ArrayList)r;
                    if (got.Count == 0)
                    {
                        Report("MP322", "피킹 대상이 없습니다.", MsgLevel.Warn);
                        txtVch2.Focus();
                        return;
                    }
                    if (reset) { _rows = new ArrayList(); _skip = 0; _work = 0; }
                    _rows.AddRange(got);
                    if (sort)
                    {
                        // ArrayList.Sort 는 안정 정렬이 아니므로 원래 순서를 보존하며 LOC 순으로 다시 쌓는다
                        ArrayList sorted = StableSort(_rows);
                        _rows = sorted;
                    }
                    _total = _work + _rows.Count;
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                    DisplayWork();
                });
        }

        private static ArrayList StableSort(ArrayList rows)
        {
            ArrayList outList = new ArrayList();
            IComparer c = new RowLocCompare();
            for (int i = 0; i < rows.Count; i++)
            {
                int p = outList.Count;
                while (p > 0 && c.Compare(outList[p - 1], rows[i]) > 0) p--;
                outList.Insert(p, rows[i]);
            }
            return outList;
        }

        // ------------------------------------------------------------------
        // DisplayWork
        // ------------------------------------------------------------------
        private void DisplayWork()
        {
            if (_skip >= _rows.Count) { ClearPart(); return; }
            OutRow o = (OutRow)_rows[_skip];
            _dbl = Doubles(_skip, o["TRS_LEP"], o["TRS_PTNO"], o["TRS_LOCNO"]);

            lblPrefix.Text = o["TRS_LEP"].Length > 0 ? o["TRS_LEP"] : "H";
            txtPart.Text = PartNo.Display(o["TRS_PTNO"]);
            lblPartName.Text = o["INV_PTNM_A"];
            lblClass.Text = o["UWMG_GRADE"];
            string loc = o["TRS_LOCNO"].Replace("-", "");
            txtLoc.Text = loc.Length == 0 ? "NONE" : Loc.Display(loc);
            lblAvl.Text = Num(o["LOC_AVLQT"]);
            int qty = 0;
            for (int i = 0; i < _dbl.Count; i++) qty += ToInt(((OutRow)_rows[(int)_dbl[i]])["TRS_SALQT"]);
            txtQty.Text = qty.ToString();

            string vch = OutboundService.VchKey(o["TRS_VCHNO"]);
            if (vch.Length == 11) { txtVch1.Text = vch.Substring(0, 6); txtVch2.Text = vch.Substring(6); }
            lblSalgb.Text = S210_Picking.SalgbName(o["TRS_SALGB"], "");
            lblWh.Text = o["TRS_WHSFL"];
            lblVndmn.Text = o["TRS_VNDMN"];
            lblVndnm.Text = o["VEN_VNDNM"];
            lblGrtno.Text = o["TRS_GRTNO"];
            lblNoarg.Text = Num(o["TRS_NOARG_QT"]);
            lblWork.Text = _work + "/" + _total;

            Msg("LOC 을 확인(스캔)하세요." + (_dbl.Count > 1 ? " (증표 " + _dbl.Count + "건 묶음)" : ""), MsgLevel.Info);
            txtLoc.Focus();
            txtLoc.SelectAll();
        }

        private ArrayList Doubles(int start, string lep, string ptno, string loc)
        {
            ArrayList d = new ArrayList();
            string p = PartNo.Key(ptno), l = Loc.Key(loc);
            for (int i = start; i < _rows.Count; i++)
            {
                OutRow o = (OutRow)_rows[i];
                if (o["TRS_LEP"] == lep && PartNo.Key(o["TRS_PTNO"]) == p && Loc.Key(o["TRS_LOCNO"]) == l) d.Add(i);
            }
            return d;
        }

        private bool HasWork { get { return _skip < _rows.Count && _dbl.Count > 0; } }

        // ------------------------------------------------------------------
        // LOC / 부번 확인
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
            CheckPart();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (Busy) return;
            if (!HasWork)
            {
                // 작업 전이면 증표번호 바코드로 본다
                string v = data.Text.Replace("-", "").Trim();
                if (v.Length == 11) { txtVch1.Text = v.Substring(0, 6); txtVch2.Text = v.Substring(6); SearchOne(); }
                return;
            }
            if (_focus == "PTNO") { txtPart.Text = PartNo.Display(data.Text); CheckPart(); }
            else { txtLoc.Text = Loc.Display(data.Text); CheckLoc(); }
        }

        private bool LocOk()
        {
            string loc = Loc.Key(txtLoc.Text);
            return loc == "NONE" || loc == Loc.Key(((OutRow)_rows[_skip])["TRS_LOCNO"]);
        }

        private void CheckLoc()
        {
            if (!HasWork) return;
            if (LocOk()) { Msg("LOC 확인. 부품을 스캔하세요.", MsgLevel.Success); txtPart.Focus(); return; }
            Report("MP553", "LOC 가 일치하지 않습니다.", MsgLevel.Warn);
            txtLoc.Focus();
            txtLoc.SelectAll();
        }

        private void CheckPart()
        {
            if (!HasWork) return;
            if (!LocOk()) { Report("MP553", "LOC 가 일치하지 않습니다.", MsgLevel.Warn); txtLoc.Focus(); return; }
            if (PartNo.Key(txtPart.Text) == PartNo.Key(((OutRow)_rows[_skip])["TRS_PTNO"]))
            {
                Msg("부품 확인. 수량을 확인하고 [저장] 하세요.", MsgLevel.Success);
                txtQty.Focus();
                txtQty.SelectAll();
                return;
            }
            Report("MP554", "부품번호가 일치하지 않습니다.", MsgLevel.Warn);
            txtPart.Focus();
            txtPart.SelectAll();
        }

        // ------------------------------------------------------------------
        // 저장 (fn_SavePicking)
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
            if (!HasWork || PartNo.Key(txtPart.Text).Length == 0)
            {
                Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn);
                return;
            }
            if (txtQty.Text.Trim().Length == 0) { Report("MP304", "수량을 입력하십시오.", MsgLevel.Warn); return; }

            OutRow cur = (OutRow)_rows[_skip];
            ArrayList vch = new ArrayList(), wsf = new ArrayList();
            for (int i = 0; i < _dbl.Count; i++)
            {
                OutRow o = (OutRow)_rows[(int)_dbl[i]];
                vch.Add(OutboundService.VchKey(o["TRS_VCHNO"]));
                string w = o["TRS_WSFNO"].Trim();
                if (w.Length > 0 && !wsf.Contains(w)) wsf.Add(w);
            }

            OutboundService.PickSave s = new OutboundService.PickSave();
            s.SqlU01 = "plus:PL220_W01_U01";
            s.SendVchno = false;
            s.VchIn = OutboundService.InList(vch);
            s.Vndmn = lblVndmn.Text;
            s.Whsfl = lblWh.Text;
            s.Grtno = cur["TRS_GRTNO"];
            s.Ptno = cur["TRS_PTNO"];
            s.Lep = cur["TRS_LEP"];
            s.PicDt = _picDt;
            s.PicTm = _picTm;
            s.WsfIn = OutboundService.InList(wsf);

            Run("저장중...",
                delegate { OutboundService.SavePicking(OutboundService.Pgm220, s); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText("저장중 에러가 발생하였습니다. " + ex.Message, MsgLevel.Error);
                        return;
                    }
                    _saved = true;
                    ArrayList d = (ArrayList)_dbl.Clone();
                    d.Sort();
                    for (int k = d.Count - 1; k >= 0; k--) { _rows.RemoveAt((int)d[k]); _work++; }
                    _dbl.Clear();
                    lblWork.Text = _work + "/" + _total;
                    Report("MP102", "정상 처리되었습니다.", MsgLevel.Success);
                    EndCheck();
                });
        }

        private void EndCheck()
        {
            if (_skip > 0)
            {
                if (_total == _work + _skip)
                {
                    string m = CommonCache.Msg("MP548", "SKIP 한 %d 건이 있습니다. 작업을 종료하시겠습니까?")
                                          .Replace("%d", _skip.ToString());
                    if (Confirm(m)) { Shell.GoBack(); return; }
                    _skip = 0;
                }
                DisplayWork();
                return;
            }
            if (_total == _work)
            {
                Report("MP602", "피킹 작업이 완료되었습니다.", MsgLevel.Success);
                Init();
                _work = 0;
                return;
            }
            DisplayWork();
        }

        // ------------------------------------------------------------------
        // SKIP / 멀티 / 재고 / 지움
        // ------------------------------------------------------------------
        private void OnSkip(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!HasWork) { Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn); return; }

            int n = _dbl.Count;
            _skip += n;
            if (_skip >= _rows.Count)
            {
                if (ConfirmMsg("MP550", "마지막 항목입니다. 처음부터 다시 하시겠습니까?")) { _skip = 0; DisplayWork(); }
                else { _skip -= n; Shell.GoBack(); }
                return;
            }
            if (ConfirmMsg("MP509", "SKIP 하시겠습니까?")) DisplayWork();
            else _skip -= n;
        }

        private void OnMulti(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!ConfirmMsg("MP565", "여러 판매증표를 선택하시겠습니까?")) return;
            NavArgs a = new NavArgs();
            a.Set("MULTI", "Y");
            Shell.Navigate(ScreenId.SlipSelect, a);
        }

        private void OnStock(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!HasWork) { Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn); return; }
            OutRow o = (OutRow)_rows[_skip];
            NavArgs a = new NavArgs();
            a.Set("LEP", o["TRS_LEP"]);
            a.Set("PTNO", PartNo.Key(o["TRS_PTNO"]));
            a.Set("PTNM", o["INV_PTNM_A"]);
            a.Set("CLASS", o["UWMG_GRADE"]);
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (Busy) return;
            if (!ConfirmMsg("MP503", "화면을 지우시겠습니까?")) return;
            Init();
        }

        protected override void SetButtons(bool on)
        {
            btnSearch.Enabled = on;
            btnMulti.Enabled = on;
            btnSave.Enabled = on;
            btnStock.Enabled = on;
            btnSkip.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
