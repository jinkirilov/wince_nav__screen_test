using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Controls;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [131] 예약내역 : 부번 -> 계열 -> 품명/등급 -> 업체별 예약 목록
    // 원본 웹화면 : /ui/ws/plus/PL131_W01.xml
    //
    //   fn_SearchLep   (PL131_W01_S01)  계열 목록. 2건 이상이면 원본은 선택 팝업
    //   fn_SearchClass (PL100_W01_S02)  품명 / 등급
    //   fn_Search      (PL131_W01_S02)  업체별 예약 목록 + 수량 합계
    //
    // 조회 전용 화면이다. 저장 경로가 없다.
    // 좌표/크기/색/폰트/TabIndex 는 전부 S131_ReserveList.Designer.cs 에서 관리한다.
    public sealed partial class S131_ReserveList : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_NOPART = "MP540";  // 부품번호를 입력하십시오
        private const string MP_NORSV = "MP575";   // 예약정보가 없습니다

        private string _lep = "H";
        private bool _busy;

        public override int ScreenNo { get { return ScreenId.ReserveList; } }
        public override string ScreenName { get { return "예약내역"; } }

        public S131_ReserveList()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        // ------------------------------------------------------------------
        // 진입 (fn_OnLoad)
        //
        // 다른 화면에서 부번을 들고 넘어오면 바로 조회한다 (원본 ds_LinkInfo 대응).
        // ------------------------------------------------------------------
        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string ptno = (args == null) ? null : args.GetString("PTNO");
            if (ptno != null && ptno.Length > 0)
            {
                string lep = args.GetString("LEP");
                if (lep != null && lep.Length > 0)
                {
                    _lep = lep;
                    lblPrefix.Text = lep;
                }

                txtPart.Text = PartNo.Display(ptno);

                // 계열까지 넘어왔으면 계열 조회를 건너뛴다 (원본과 동일)
                if (lep != null && lep.Length > 0) SearchClass();
                else SearchLep();
                return;
            }

            txtPart.Focus();
            Msg("부번을 스캔/입력하세요.", MsgLevel.Info);
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

            Begin("조회중...");
            Async.Run(this,
                delegate { return ReserveService.SearchLep(ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList leps = (ArrayList)r;

                    if (leps.Count == 0)
                    {
                        // 원본도 계열을 H 로 되돌리고 예약정보 없음으로 끝낸다
                        _lep = "H";
                        lblPrefix.Text = "H";
                        Report(MP_NORSV, "예약정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    // 계열이 2건 이상이면 선택 팝업을 띄운다 (원본 lep_popup)
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
        // 2단계 : 품명 / 등급 (fn_SearchClass)
        // ------------------------------------------------------------------
        private void SearchClass()
        {
            string ptno = PartNo.Key(txtPart.Text);
            string lep = _lep;

            Async.Run(this,
                delegate { return ReserveService.SearchClass(lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ClassInfo info = (ClassInfo)r;
                    if (!info.Found)
                    {
                        Report(MP_NORSV, "예약정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    lblPartName.Text = info.PartName;
                    lblClass.Text = info.Grade;

                    SearchList();
                });
        }

        // ------------------------------------------------------------------
        // 3단계 : 업체별 예약 목록 (fn_Search)
        // ------------------------------------------------------------------
        private void SearchList()
        {
            string ptno = PartNo.Key(txtPart.Text);
            string lep = _lep;

            Async.Run(this,
                delegate { return ReserveService.Search(lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ReserveResult sr = (ReserveResult)r;
                    if (!sr.HasAny)
                    {
                        Report(MP_NORSV, "예약정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    FillGrid(sr.Rows);
                    txtCnt.Text = sr.Rows.Count.ToString();
                    txtQty.Text = sr.TotalQty.ToString();

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);
                    txtPart.Focus();
                    txtPart.SelectAll();
                });
        }

        private void FillGrid(ArrayList rows)
        {
            lstRsv.BeginUpdate();
            try
            {
                lstRsv.Items.Clear();
                for (int i = 0; i < rows.Count; i++)
                {
                    ReserveRow v = (ReserveRow)rows[i];
                    ListViewItem it = new ListViewItem(v.VndMn);
                    it.SubItems.Add(v.VndNm);
                    it.SubItems.Add(v.Qty);
                    it.SubItems.Add(v.GrtNo);
                    it.Tag = v;
                    lstRsv.Items.Add(it);
                }
            }
            finally { lstRsv.EndUpdate(); }
        }

        // ------------------------------------------------------------------
        // 지움 (fn_Clear -> fn_Init)
        // ------------------------------------------------------------------
        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;

            if (MessageBox.Show("입력한 내용을 지우시겠습니까?", "[131] " + ScreenName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        private void ClearResult()
        {
            lblClass.Text = "";
            lblPartName.Text = "";
            txtCnt.Text = "0";
            txtQty.Text = "0";
            lstRsv.Items.Clear();
        }

        private void ClearAll()
        {
            _lep = "H";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            ClearResult();
        }

        // ------------------------------------------------------------------
        private void Begin(string msg)
        {
            _busy = true;
            Cursor.Current = Cursors.WaitCursor;
            btnClear.Enabled = false;
            Msg(msg, MsgLevel.Info);
        }

        private void End(string msg, MsgLevel lv)
        {
            _busy = false;
            Cursor.Current = Cursors.Default;
            btnClear.Enabled = true;
            Msg(msg, lv);
        }

        /// <summary>MP101 이 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[131] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            End(ex.Message, MsgLevel.Error);
            MessageBox.Show(ex.Message, "[131] " + ScreenName);
            return true;
        }
    }
}
