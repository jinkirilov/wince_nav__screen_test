using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;

namespace HaimsPda.Screens
{
    /// <summary>
    /// [1401] 할당내역 선택 팝업. 원본 /ui/ws/plus/PL140_P01.xml (메뉴 P179, 제목 "저장대기품목조회").
    ///
    /// 부번 하나에 할당이 여러 건 걸려 있을 때 [140] 입고저장에서 띄운다.
    /// 체크한 행이 원본의 dsInput, 수량 합계가 원본의 ds_LinkInfo/QTY 에 해당한다.
    ///
    /// 원본 그리드는 2줄(발송일자/할당번호/할당코드 + 할당수량/CASE/LOC)이지만
    /// CF ListView 는 한 줄이라 6개 열로 펼쳤다.
    ///
    /// 호출부는 AllocSelect.Pick() 을 쓴다. 이 폼을 직접 열 필요는 없다.
    /// </summary>
    public partial class S1401_AllocSelectForm : Form
    {
        private const int Col_SndDt = 0;   // 발송일자   WSF_SNDDT
        private const int Col_Vchno = 1;   // 할당번호   WSF_VCHNO
        private const int Col_Reqcd = 2;   // 할당코드   WSF_REQCD
        private const int Col_Qty = 3;     // 할당수량   WSF_WSFQT
        private const int Col_Casno = 4;   // CASE NO    WSF_CASNO
        private const int Col_Loc = 5;     // LOCATION   LOC_LOCNO

        private readonly ArrayList _rows;
        private bool _allOn;      // 원본 chkFlag : ALL 버튼 토글 상태

        /// <summary>체크된 행(Row). 취소면 null.</summary>
        public ArrayList Selected;

        public S1401_AllocSelectForm(ArrayList rows, string title)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            _rows = rows;
            if (title != null && title.Length > 0) lblTitle.Text = title;

            lstAlloc.BeginUpdate();
            try
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    Row a = (Row)rows[i];
                    ListViewItem it = new ListViewItem(Date(a["WSF_SNDDT"]));
                    it.SubItems.Add(a["WSF_VCHNO"]);
                    it.SubItems.Add(a["WSF_REQCD"]);
                    it.SubItems.Add(InboundService.QtyOf(a).ToString("#,##0"));
                    it.SubItems.Add(a["WSF_CASNO"]);
                    it.SubItems.Add(Loc.Display(a["LOC_LOCNO"]));
                    lstAlloc.Items.Add(it);
                }
            }
            finally { lstAlloc.EndUpdate(); }

            if (lstAlloc.Items.Count > 0)
                lstAlloc.Items[0].Selected = true;

            UpdateSum(-1, false);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lstAlloc.Focus();
        }

        // 체크 직전에 불린다. e.NewValue 가 바뀔 값이다.
        private void OnItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateSum(e.Index, e.NewValue == CheckState.Checked);
        }

        // 원본은 행을 누르면 체크가 토글된다. 스캐너 장비는 터치보다 키를 쓰므로 Enter 로 토글한다.
        private void OnListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            if (lstAlloc.SelectedIndices.Count == 0) return;
            ListViewItem it = lstAlloc.Items[lstAlloc.SelectedIndices[0]];
            it.Checked = !it.Checked;
        }

        /// <summary>원본 fn_BottomButton('all') : 전체 체크/해제 토글</summary>
        private void OnAll(object sender, EventArgs e)
        {
            _allOn = !_allOn;
            for (int i = 0; i < lstAlloc.Items.Count; i++)
                lstAlloc.Items[i].Checked = _allOn;
            UpdateSum(-1, false);
        }

        /// <summary>원본 fn_BottomButton('return') : 체크된 행만 돌려준다</summary>
        private void OnOk(object sender, EventArgs e)
        {
            ArrayList sel = new ArrayList();
            for (int i = 0; i < lstAlloc.Items.Count; i++)
                if (lstAlloc.Items[i].Checked) sel.Add(_rows[i]);

            if (sel.Count == 0)
            {
                MessageBox.Show(CommonCache.Msg("MP545", "할당내역을 선택하세요."), Text);
                lstAlloc.Focus();
                return;
            }

            Selected = sel;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Selected = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 원본 fn_CheckList : 체크된 행의 할당수량 합계.
        /// ItemCheck 는 값이 바뀌기 전에 오므로 바뀌는 행(idx)은 newChecked 로 계산한다.
        /// </summary>
        private void UpdateSum(int idx, bool newChecked)
        {
            int cnt = 0, qty = 0;
            for (int i = 0; i < lstAlloc.Items.Count; i++)
            {
                bool on = (i == idx) ? newChecked : lstAlloc.Items[i].Checked;
                if (!on) continue;
                cnt++;
                qty += InboundService.QtyOf((Row)_rows[i]);
            }
            lblSum.Text = "선택 " + cnt + " / " + lstAlloc.Items.Count + "건";
            lblQty.Text = qty.ToString("#,##0");
        }

        private static string Date(string s)
        {
            if (s == null || s.Length != 8) return s;
            return s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);
        }
    }

    /// <summary>[1401] 할당내역 선택 진입점. 화면에서는 이것만 쓴다.</summary>
    public static class AllocSelect
    {
        /// <summary>
        /// 할당 레코드를 고르게 하고 체크된 Row 목록을 돌려준다. 취소면 null.
        /// 건수와 상관없이 팝업을 띄운다(원본 할당내역 버튼도 1건일 때 그대로 연다).
        /// </summary>
        public static ArrayList Pick(ArrayList rows, string title)
        {
            if (rows == null || rows.Count == 0) return null;

            using (S1401_AllocSelectForm f = new S1401_AllocSelectForm(rows, title))
            {
                f.ShowDialog();
                return f.Selected;
            }
        }
    }
}
