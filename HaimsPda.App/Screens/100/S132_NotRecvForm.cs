using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;

namespace HaimsPda.Screens
{
    /// <summary>
    /// [132] 미수령등록 팝업. 원본 /ui/ws/plus/PL132_W01.xml (메뉴 P119).
    ///
    /// [140] [120] [121] 에서 할당 1건에 대해 미수령 수량과 사유를 받는다.
    /// 서버에 쓰지 않고 값만 돌려준다(원본도 ds_LinkInfo 로 돌려주기만 한다).
    ///
    ///   미수령수량 Enter  -> 입고수량 계산 (원본 fn_ChangeNarqt("0"))
    ///   리턴              -> 검증 후 닫기  (원본 fn_ChangeNarqt("1") -> fn_BottomButton('return'))
    ///
    /// 원본과 다르게 한 부분
    ///   - 미수령수량 0 은 받지 않는다(원본은 0 도 통과해 CTLQT=0 인 미수령이 기록될 수 있다).
    ///   - "재고" 버튼([321] 이동)은 모달 팝업에서는 두지 않는다.
    ///
    /// 호출부는 NotRecv.Show() 를 쓴다.
    /// </summary>
    public partial class S132_NotRecvForm : Form
    {
        private readonly int _wsfQty;

        /// <summary>리턴하면 채워진다. 취소면 null.</summary>
        public NotRecvResult Result;

        public S132_NotRecvForm(string title, string vchno, int wsfQty, ArrayList reasons)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            if (title != null && title.Length > 0) lblTitle.Text = title;
            _wsfQty = wsfQty;
            lblVchno.Text = vchno;
            lblWsf.Text = wsfQty.ToString();
            txtNar.Text = "0";
            lblIn.Text = "";

            cboReason.Items.Clear();
            for (int i = 0; i < reasons.Count; i++) cboReason.Items.Add(reasons[i]);
            if (cboReason.Items.Count > 0) cboReason.SelectedIndex = 0;   // 원본 selNar 첫 항목
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtNar.Focus();
            txtNar.SelectAll();
        }

        private void OnNarKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            if (Calc()) cboReason.Focus();
        }

        private void OnReasonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            OnOk(null, null);
        }

        /// <summary>원본 fn_ChangeNarqt : 미수령수량을 검사하고 입고수량을 계산한다.</summary>
        private bool Calc()
        {
            int nar = ToInt(txtNar.Text);
            if (nar <= 0)
            {
                Alert("미수령수량을 입력하세요.");
                return false;
            }
            if (nar > _wsfQty)
            {
                Alert(CommonCache.Msg("MP535", "미수령수량이 입고대상수량보다 많습니다."));
                return false;
            }
            if (nar == _wsfQty)
            {
                Alert(CommonCache.Msg("MP536", "미수령수량이 입고대상수량과 같습니다."));
                return false;
            }
            lblIn.Text = (_wsfQty - nar).ToString();
            return true;
        }

        private void OnOk(object sender, EventArgs e)
        {
            if (!Calc()) return;

            CodeItem rc = cboReason.SelectedItem as CodeItem;
            if (rc == null)
            {
                MessageBox.Show("미수령 사유를 선택하세요.", Text);
                cboReason.Focus();
                return;
            }

            NotRecvResult r = new NotRecvResult();
            r.NarQty = ToInt(txtNar.Text);
            r.InQty = _wsfQty - r.NarQty;
            r.ReasonCd = rc.Code;
            r.ReasonNm = rc.Name;
            Result = r;

            DialogResult = DialogResult.OK;
            Close();
        }

        // 원본 fn_Init 에 해당. 팝업 안의 입력만 지운다.
        private void OnClear(object sender, EventArgs e)
        {
            txtNar.Text = "0";
            lblIn.Text = "";
            if (cboReason.Items.Count > 0) cboReason.SelectedIndex = 0;
            txtNar.Focus();
            txtNar.SelectAll();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Result = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Alert(string text)
        {
            MessageBox.Show(text, Text);
            txtNar.Focus();
            txtNar.SelectAll();
        }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }
    }

    /// <summary>[132] 미수령등록 진입점.</summary>
    public static class NotRecv
    {
        /// <summary>미수령 수량/사유를 받는다. 취소면 null.</summary>
        public static NotRecvResult Show(string title, string vchno, int wsfQty, ArrayList reasons)
        {
            using (S132_NotRecvForm f = new S132_NotRecvForm(title, vchno, wsfQty, reasons))
            {
                f.ShowDialog();
                return f.Result;
            }
        }
    }
}
