using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Controls;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [130] 대기품목조회 : 입고대기 / 저장대기 두 모드로 부번의 대기 내역을 본다
    // 원본 웹화면 : /ui/ws/plus/PL130_W01.xml
    //
    //   fn_SearchLep   모드별 SQL (IN: PL100_W01_S01 / SAVE: PL140_W01_S01)
    //   fn_SearchClass PL100_W01_S02                      등급
    //   fn_Search      모드별 SQL (IN: PL130_W01_S02 / SAVE: PL140_W01_S02+S03)
    //
    // 원본의 rdoCheck 라디오는 PDA 화면이 좁아 토글 버튼 하나로 바꿨다.
    // 좌표/크기/색/폰트/TabIndex 는 전부 S130_WaitPartInquiry.Designer.cs 에서 관리한다.
    public sealed partial class S130_WaitPartInquiry : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_PTNO = "MP303";    // 부품번호를 확인하세요
        private const string MP_NOPART = "MP540";  // 부품번호를 입력하십시오
        private const string MP_NOALLOC = "MP532"; // 해당 부품에 할당내역이 없습니다
        private const string MP_NOGRADE = "MP531"; // 해당 부품의 재고정보가 없습니다(등급)

        private string _mode = WaitPartService.ModeIn;
        private string _lep = "H";
        private bool _busy;

        public override int ScreenNo { get { return ScreenId.WaitPartInquiry; } }
        public override string ScreenName { get { return "대기품목조회"; } }

        public S130_WaitPartInquiry()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        // ------------------------------------------------------------------
        // 진입 (fn_OnLoad)
        //
        // 원본은 링크로 넘어온 DETAIL_INFO 로 라디오 위치까지 복원한다.
        // ------------------------------------------------------------------
        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string ptno = (args == null) ? null : args.GetString("PTNO");
            if (ptno == null || ptno.Length == 0)
            {
                txtPart.Focus();
                Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
                return;
            }

            string mode = args.GetString("MODE");
            if (mode != null && mode.Length > 0) _mode = mode;
            ApplyModeText();

            string lep = args.GetString("LEP");
            if (lep != null && lep.Length > 0) { _lep = lep; lblPrefix.Text = lep; }

            txtPart.Text = PartNo.Display(ptno);
            SearchLep();
        }

        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            txtPart.Text = PartNo.Display(data.Text);
            SearchLep();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchLep(); }
        }

        // ------------------------------------------------------------------
        // 모드 토글 (fn_OnChangeRadioBtn)
        //
        // 원본도 모드를 바꾸면 목록과 합계를 비우고 부번 입력으로 돌아간다.
        // ------------------------------------------------------------------
        private void OnModeToggle(object sender, EventArgs e)
        {
            if (_busy) return;

            _mode = IsIn ? WaitPartService.ModeSave : WaitPartService.ModeIn;
            ApplyModeText();

            ClearResult();
            txtPart.Focus();
            Msg(btnMode.Text + " 모드입니다. 부번을 입력하세요.", MsgLevel.Info);
        }

        private bool IsIn
        {
            get { return _mode == WaitPartService.ModeIn; }
        }

        /// <summary>모드에 따라 버튼과 그리드 헤더를 바꾼다(원본 setHeaderValue).</summary>
        private void ApplyModeText()
        {
            btnMode.Text = IsIn ? "입고대기" : "저장대기";
            colVchno.Text = "할당";
            colCase.Text = IsIn ? "CASE" : "입고번호";
            colStat.Text = IsIn ? "상태" : "분류자";
        }

        // ------------------------------------------------------------------
        // 1단계 : 부번 -> 계열 (fn_SearchLep)
        // ------------------------------------------------------------------
        private void SearchLep()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_NOPART, "부품번호를 입력하십시오.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            txtPart.Text = PartNo.Display(txtPart.Text);
            ClearResult();

            string mode = _mode;

            Begin("조회중...");
            Async.Run(this,
                delegate { return WaitPartService.SearchLep(mode, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;
                    if (leps.Count == 0)
                    {
                        _lep = "H";
                        lblPrefix.Text = "H";
                        Report(MP_NOALLOC, "해당 부품에 할당내역이 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    int idx = LepSelect.Pick(leps, "계열 선택 - " + PartNo.Display(txtPart.Text));
                    if (idx < 0)
                    {
                        End("취소했습니다.", MsgLevel.Info);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    _lep = (string)leps[idx];
                    lblPrefix.Text = _lep;

                    SearchClass();
                });
        }

        // ------------------------------------------------------------------
        // 2단계 : 등급 (fn_SearchClass)
        // ------------------------------------------------------------------
        private void SearchClass()
        {
            string ptno = PartNo.Key(txtPart.Text);
            string lep = _lep;

            Async.Run(this,
                delegate { return WaitPartService.SearchClass(lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassInfo info = (ClassInfo)r;
                    if (!info.Found)
                    {
                        Report(MP_NOGRADE, "해당 부품의 재고정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    lblClass.Text = info.Grade;
                    lblPartName.Text = info.PartName;

                    SearchList();
                });
        }

        // ------------------------------------------------------------------
        // 3단계 : 목록 (fn_Search)
        // ------------------------------------------------------------------
        private void SearchList()
        {
            string ptno = PartNo.Key(txtPart.Text);
            string lep = _lep;
            string mode = _mode;

            Async.Run(this,
                delegate { return WaitPartService.Search(mode, lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    WaitPartResult sr = (WaitPartResult)r;

                    FillGrid(sr.Rows);
                    txtQty.Text = sr.TotalQty.ToString();
                    txtCnt.Text = sr.Rows.Count.ToString();

                    if (!sr.HasAny)
                    {
                        Report(MP_NOALLOC, "해당 부품에 할당내역이 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);
                    txtPart.Focus();
                    txtPart.SelectAll();
                });
        }

        private void FillGrid(ArrayList rows)
        {
            lstWait.BeginUpdate();
            try
            {
                lstWait.Items.Clear();
                for (int i = 0; i < rows.Count; i++)
                {
                    WaitPartRow w = (WaitPartRow)rows[i];
                    ListViewItem it = new ListViewItem(DateText(w.Date));
                    it.SubItems.Add(w.Qty);
                    it.SubItems.Add(w.Vchno);
                    it.SubItems.Add(w.CaseNo);
                    it.SubItems.Add(Loc.Display(w.Loc));
                    it.SubItems.Add(IsIn ? w.Stat : w.ReqCd);
                    it.Tag = w;
                    lstWait.Items.Add(it);
                }
            }
            finally { lstWait.EndUpdate(); }
        }

        /// <summary>서버는 yyyyMMdd 로 준다. 좁은 화면이라 연도를 떼고 MM-dd 로 보여준다.</summary>
        private static string DateText(string v)
        {
            if (v == null) return "";
            v = v.Trim();
            if (v.Length == 8) return v.Substring(4, 2) + "-" + v.Substring(6, 2);
            return v;
        }

        // ------------------------------------------------------------------
        // 버튼 : 부번을 들고 다른 화면으로 (원본 gfn_SetLinkInfo + gfn_GoToMenu)
        // ------------------------------------------------------------------
        private void OnStock(object sender, EventArgs e) { GoWith(ScreenId.StockByPart); }
        private void OnLoc(object sender, EventArgs e) { GoWith(ScreenId.LocRegister); }

        private void GoWith(int screenId)
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0)
            {
                Report(MP_PTNO, "부품번호를 확인하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            if (Shell == null) return;

            NavArgs args = new NavArgs();
            args.Set("PTNO", ptno);
            args.Set("LEP", _lep);
            args.Set("PTNM", lblPartName.Text);
            args.Set("CLASS", lblClass.Text);
            args.Set("MODE", _mode);      // 원본 DETAIL_INFO (라디오 위치) 대응

            Shell.Navigate(screenId, args);
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;

            if (MessageBox.Show("입력한 내용을 지우시겠습니까?", "[130] " + ScreenName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void ClearResult()
        {
            lblClass.Text = "";
            lblPartName.Text = "";
            txtQty.Text = "";
            txtCnt.Text = "";
            lstWait.Items.Clear();
        }

        private void ClearAll()
        {
            // fn_Init : 모드도 입고대기로 되돌린다
            _mode = WaitPartService.ModeIn;
            ApplyModeText();

            _lep = "H";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            ClearResult();
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

        /// <summary>MP101 이 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[130] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            End(ex.Message, MsgLevel.Error);
            MessageBox.Show(ex.Message, "[130] " + ScreenName);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnMode.Enabled = on;
            btnStock.Enabled = on;
            btnLoc.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
