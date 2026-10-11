using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [260] 배송분류(판매증표) : [261] 에서 고른 증표들 안에서 부번을 스캔해 분류 완료로 표시한다.
    // 원본 웹화면 : /ui/ws/plus/PL260_W01.xml (서버 메뉴 P185)
    //
    //   부번 스캔 -> PL260_W01_S03 -> ds_List 첫 건 (계열 / 품명 / 수량 / PIC_CNT / 판매구분)
    //   저장      -> PL260_W01_U01 (HAR 2026-09-20 실측 : 응답 데이터셋 이름이 "none")
    //               작업수 += PIC_CNT. 총건수에 닿으면 MP556 확인 -> [261] 로, 아니면 다음 부번 대기
    //   세부      -> [262] 증표분류세부
    //
    // 원본과 다르게 한 부분
    //   - 원본은 조회 없이도 부번 칸에 값만 있으면 저장(계열 H 기본값)이 나간다.
    //     여기서는 조회해서 찾은 부번일 때만 저장한다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S260_SlipClassify.Designer.cs 에서 관리한다.
    public sealed partial class S260_SlipClassify : WorkScreen
    {
        private string _vchIn = "", _from = "", _to = "", _vnd = "";
        private int _tot, _work, _pickCnt;
        private string _qty = "0";
        private string _foundPtno = "";
        private bool _saved;

        public override int ScreenNo { get { return ScreenId.SlipClassify; } }
        public override string ScreenName { get { return "배송분류(판매증표)"; } }
        public override NavArgs BackResult { get { return _saved ? NavResult.Saved(ScreenNo) : null; } }

        public S260_SlipClassify()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            _vnd = Arg(args, "CUSTCODE");
            _from = Arg(args, "FROMDT");
            _to = Arg(args, "TODT");
            _vchIn = Arg(args, "DETAIL_INFO");
            _tot = ToInt(Arg(args, "CNT"));
            _work = 0;

            lblVndmn.Text = _vnd;
            string nm = Arg(args, "CUSTNAME");
            int p = nm.IndexOf('-');
            lblVndnm.Text = (p >= 0) ? nm.Substring(p + 1) : nm;
            string wh = Arg(args, "WHSCD");
            lblWh.Text = wh.Length > 0 ? wh : "M";
            lblSalgb.Text = S210_Picking.SalgbName(Arg(args, "TRS_SALGB"), "");
            ShowCount();
            Init();

            if (_vchIn.Length == 0)
            {
                SetButtons(false);
                Msg("[261] 피킹분류(증표)에서 증표를 골라 진입하세요.", MsgLevel.Warn);
            }
        }

        private void ShowCount() { lblWork.Text = _work + "/" + _tot; }

        /// <summary>원본 fn_Init</summary>
        private void Init()
        {
            _qty = "0";
            _foundPtno = "";
            lblPrefix.Text = "H";
            txtPart.Text = "";
            lblPartName.Text = "";
            txtQty.Text = "";
            Msg("부품을 스캔하세요.", MsgLevel.Info);
            txtPart.Focus();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (Busy || _vchIn.Length == 0) return;
            txtPart.Text = PartNo.Display(data.Text);
            Search();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Search();
        }

        private void OnSearch(object sender, EventArgs e) { Search(); }

        private void Search()
        {
            if (Busy || _vchIn.Length == 0) return;
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn); txtPart.Focus(); return; }
            txtPart.Text = PartNo.Display(ptno);
            _foundPtno = "";

            string vch = _vchIn, from = _from, to = _to, vnd = _vnd;
            Run("조회중...",
                delegate { return OutboundService.SearchClassifyPart(from, to, vnd, vch, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    OutRow o = (OutRow)r;
                    if (o == null)
                    {
                        Report("MP578", "분류 대상이 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }
                    lblSalgb.Text = S210_Picking.SalgbName(o["TRS_SALGB"], lblSalgb.Text);
                    lblPrefix.Text = o["TRS_LEP"].Length > 0 ? o["TRS_LEP"] : "H";
                    lblPartName.Text = o["INV_PTNM_A"];
                    _qty = Num(o["TRS_SALQT"]);
                    txtQty.Text = _qty;
                    _pickCnt = ToInt(o["PIC_CNT"]);
                    _foundPtno = ptno;
                    Report("MP101", "정상 조회되었습니다.", MsgLevel.Success);
                    txtQty.Focus();
                    txtQty.SelectAll();
                });
        }

        // ------------------------------------------------------------------
        // 저장 (fn_Save) — 원본은 저장 직전에 수량을 조회값(GV_QTY)으로 되돌린다. 수량은 전송하지 않는다.
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
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0 || ptno != _foundPtno)
            {
                Report("MP303", "부품번호를 확인하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            txtQty.Text = _qty;

            string vch = _vchIn, lep = lblPrefix.Text, vnd = _vnd, from = _from, to = _to;
            Run("저장중...",
                delegate { OutboundService.SaveClassify(vch, lep, ptno, vnd, from, to); return null; },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;
                    _saved = true;
                    _work += _pickCnt;
                    ShowCount();
                    Report("MP102", "정상 처리되었습니다.", MsgLevel.Success);

                    // 원본 EndCheck
                    if (_work >= _tot && _tot > 0)
                    {
                        if (ConfirmMsg("MP556", "분류 작업이 완료되었습니다. 이전 화면으로 이동하시겠습니까?"))
                        {
                            Shell.GoBack();
                            return;
                        }
                    }
                    Init();
                });
        }

        // ------------------------------------------------------------------
        private void OnDetail(object sender, EventArgs e)
        {
            if (Busy || _vchIn.Length == 0) return;
            NavArgs a = new NavArgs();
            a.Set("WHSCD", lblWh.Text);
            a.Set("FROM_SALDT", _from);
            a.Set("TO_SALDT", _to);
            a.Set("DETAIL_INFO", _vchIn);
            a.Set("SELECT", _vnd);
            Shell.Navigate(ScreenId.SlipClassifyDetail, a);
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
            btnSave.Enabled = on;
            btnDetail.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
