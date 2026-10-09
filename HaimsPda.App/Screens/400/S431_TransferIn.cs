using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [431] 실시간창고이전(입고) : 구창고에서 [430] 으로 출고된 부품을 신창고 LOC 에 저장한다.
    // 원본 웹화면 : /ui/ws/plus/PL431_W01.xml (메뉴 1D04 / P144), 매뉴얼 LOC관리 192~194p
    //
    //   부번 스캔 -> fn_SearchFromPTNO(S01) -> [FROM] 구창고/LOC/대상수량 표시
    //   -> [TO] 신창고 LOC 스캔 -> 수량 Enter -> 저장(U01 + U02) -> 화면 초기화
    //
    // 원본과 다르게 한 부분
    //   - 원본 fn_SearchLoc 는 스캔한 LOC 가 "구창고 LOC(LOH_LOCFR)" 와 다르면 MP311 을 띄운다.
    //     매뉴얼 예시(02C-01-24-03-A -> 11A-01-01-01-A)부터 다른 LOC 로 저장하므로 이 비교는 버그로 보고 뺐다.
    //     대신 서버가 LOH_LOCTO 를 미리 지정해 준 경우에만, 다르면 확인을 받는다.
    //   - 원본 fn_SaveLoc_After 는 성공 여부를 보지 않고 초기화한다. 여기서는 실패면 남겨 둔다.
    //
    // 원본과 같게 둔 부분
    //   - 수량은 고칠 수 없다(원본 onkeyup 이 LOH_QTY 로 되돌림). 저장 전문에도 수량이 없다.
    //   - 저장 WHSCD 에는 구창고 코드(LOH_WHSFR)를 그대로 보낸다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S431_TransferIn.Designer.cs 에서 관리한다.
    public sealed partial class S431_TransferIn : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";    // 정상 저장되었습니다
        private const string MP_SAVEERR = "MP108";  // 저장중 에러가 발생하였습니다
        private const string MP_NOPART = "MP303";   // 부품번호를 확인하세요
        private const string MP_NODATA = "MP307";   // 할당 정보가 없는 품목입니다
        private const string MP_NOLOC = "MP310";    // 로케이션을 확인하세요
        private const string MP_CLEAR = "MP503";    // 지우시겠습니까

        private bool _busy;
        private string _focus = "";       // PTNO / LOCNO
        private TransferInItem _cur;

        public override int ScreenNo { get { return ScreenId.WhTransferIn; } }
        public override string ScreenName { get { return "실시간창고이전(입고)"; } }

        public S431_TransferIn()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string ptno = Arg(args, "PTNO");
            if (ptno.Length > 0)
            {
                txtPart.Text = PartNo.Display(ptno);
                Search();
            }
            else
            {
                txtPart.Focus();
                Msg("부번을 스캔하세요.", MsgLevel.Info);
            }
        }

        private static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        // ------------------------------------------------------------------
        // 스캔 / 입력 (원본 fn_Barcode : 포커스가 없으면 부번)
        // ------------------------------------------------------------------
        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            if (_busy) return;

            if (_focus == "LOCNO" || (_focus.Length == 0 && _cur != null))
            {
                txtLocTo.Text = Loc.Display(data.Text);
                LocEnter();
            }
            else
            {
                txtPart.Text = PartNo.Display(data.Text);
                Search();
            }
        }

        private void OnPartFocus(object sender, EventArgs e) { _focus = "PTNO"; }
        private void OnLocFocus(object sender, EventArgs e) { _focus = "LOCNO"; }
        private void OnOtherFocus(object sender, EventArgs e) { _focus = ""; }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Search();
        }

        private void OnLocKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            LocEnter();
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            Save();
        }

        // ------------------------------------------------------------------
        // 조회 (원본 fn_SearchFromPTNO / fn_popPart)
        // ------------------------------------------------------------------
        private void Search()
        {
            if (_busy) return;

            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length == 0) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }
            txtPart.Text = PartNo.Display(ptno);

            Begin("조회중...");
            Async.Run(this,
                delegate { return TransferInService.Search(ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList list = (ArrayList)r;
                    ClearResult();

                    if (list.Count == 0)
                    {
                        Report(MP_NODATA, "이전(출고) 정보가 없는 품목입니다.", MsgLevel.Warn);
                        FocusSel(txtPart);
                        return;
                    }

                    // 계열이 둘 이상이면 고르게 한다 (원본 lep_popup)
                    int idx = HaimsPda.Controls.LepSelect.Pick(list, "계열 선택 - " + PartNo.Display(ptno));
                    if (idx < 0) { End("취소했습니다.", MsgLevel.Info); FocusSel(txtPart); return; }

                    TransferInItem it = (TransferInItem)list[idx];
                    _cur = it;
                    lblPrefix.Text = it.Lep;
                    txtPart.Text = PartNo.Display(it.Ptno);
                    lblClass.Text = it.Grade;
                    lblPartName.Text = it.PartName;
                    txtWh.Text = it.WhsFr;
                    txtLocFr.Text = Loc.Display(it.LocFr);
                    txtExpQty.Text = it.Qty;
                    txtLocTo.Text = Loc.Display(it.LocTo);
                    txtQty.Text = it.Qty;

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다.") + " 저장할 LOC 를 스캔하세요.", MsgLevel.Success);
                    FocusSel(txtLocTo);
                });
        }

        // ------------------------------------------------------------------
        // TO LOC (원본 fn_SearchLoc)
        // ------------------------------------------------------------------
        private void LocEnter()
        {
            if (_busy) return;
            if (_cur == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            string loc = Loc.Key(txtLocTo.Text);
            if (loc.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocTo); return; }
            txtLocTo.Text = Loc.Display(loc);

            // 서버가 신창고 LOC 를 지정해 둔 경우에만 다르면 확인
            if (_cur.LocTo.Length > 0 && _cur.LocTo != loc)
            {
                if (!Confirm("지정된 LOC(" + Loc.Display(_cur.LocTo) + ")와 다릅니다.\r\n" + txtLocTo.Text + " 에 저장하시겠습니까?"))
                {
                    FocusSel(txtLocTo);
                    return;
                }
            }

            Msg("수량을 확인하고 Enter 를 누르세요.", MsgLevel.Info);
            txtQty.Focus();
        }

        // ------------------------------------------------------------------
        // 저장 (원본 fn_SaveLoc)
        // ------------------------------------------------------------------
        private void Save()
        {
            if (_busy) return;
            if (_cur == null) { Report(MP_NOPART, "부품번호를 확인하세요.", MsgLevel.Warn); FocusSel(txtPart); return; }

            string loc = Loc.Key(txtLocTo.Text);
            if (loc.Length == 0) { Report(MP_NOLOC, "로케이션을 확인하세요.", MsgLevel.Warn); FocusSel(txtLocTo); return; }

            TransferInItem it = _cur;

            Begin("저장중...");
            Async.Run(this,
                delegate { TransferInService.Save(it, loc); return null; },
                delegate(object r, Exception ex)
                {
                    if (ex != null)
                    {
                        ReportText(CommonCache.Msg(MP_SAVEERR, "저장중 에러가 발생하였습니다.") + "\r\n" + ex.Message, MsgLevel.Error);
                        FocusSel(txtLocTo);
                        return;
                    }

                    Report(MP_SAVED, "정상 저장되었습니다.", MsgLevel.Success);
                    ClearAll();
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        // LOC : [410] LOC등록 (원본 OnBtnLoc)
        private void OnLoc(object sender, EventArgs e)
        {
            if (_busy) return;
            Shell.Navigate(ScreenId.LocRegister, LinkArgs());
        }

        // 재고 : [321] 파트별재고 (원본 OnBtnPart)
        private void OnStock(object sender, EventArgs e)
        {
            if (_busy) return;
            Shell.Navigate(ScreenId.StockByPart, LinkArgs());
        }

        private NavArgs LinkArgs()
        {
            NavArgs a = new NavArgs();
            string ptno = PartNo.Key(txtPart.Text);
            if (ptno.Length > 0)
            {
                a.Set("LEP", lblPrefix.Text);
                a.Set("PTNO", ptno);
                a.Set("PTNM", lblPartName.Text);
                a.Set("CLASS", lblClass.Text);
            }
            return a;
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Confirm(CommonCache.Msg(MP_CLEAR, "입력한 내용을 지우시겠습니까?"))) return;
            ClearAll();
            txtPart.Focus();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void ClearResult()
        {
            _cur = null;
            lblPrefix.Text = "H";
            lblClass.Text = "";
            lblPartName.Text = "";
            txtWh.Text = "";
            txtLocFr.Text = "";
            txtExpQty.Text = "";
            txtLocTo.Text = "";
            txtQty.Text = "";
        }

        private void ClearAll()
        {
            txtPart.Text = "";
            _focus = "";
            ClearResult();
        }

        private void FocusSel(TextBox t)
        {
            t.Focus();
            t.SelectAll();
        }

        private bool Confirm(string text)
        {
            return MessageBox.Show(text, "[431] " + ScreenName,
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
            if (code != MP_OK && code != MP_SAVED) MessageBox.Show(text, "[431] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[431] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnLoc.Enabled = on;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
