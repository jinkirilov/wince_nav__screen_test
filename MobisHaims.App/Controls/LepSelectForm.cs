using System;
using System.Collections;
using System.Windows.Forms;

namespace MobisHaims.Controls
{
    /// <summary>
    /// 계열(LEP) 선택 팝업. 원본 웹화면의 lep_popup / lep_grid1 대응.
    ///
    /// 부번 하나에 계열이 둘 이상 걸리는 경우가 [140] [131] [320] [321] 에서
    /// 똑같이 나온다. 화면마다 "첫 건으로 진행" 하던 것을 이 팝업으로 모은다.
    ///
    /// 호출부는 LepSelect.Pick() 을 쓴다. 이 폼을 직접 열 필요는 없다.
    /// </summary>
    public partial class LepSelectForm : Form
    {
        /// <summary>선택된 항목의 인덱스. 취소면 -1.</summary>
        public int SelectedIdx = -1;

        public LepSelectForm(ArrayList items, string title)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            if (title != null && title.Length > 0) lblTitle.Text = title;

            lstLep.BeginUpdate();
            try
            {
                for (int i = 0; i < items.Count; i++)
                {
                    object o = items[i];
                    lstLep.Items.Add(new ListViewItem((o == null) ? "" : o.ToString()));
                }
            }
            finally { lstLep.EndUpdate(); }

            if (lstLep.Items.Count > 0)
                lstLep.Items[0].Selected = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lstLep.Focus();
        }

        private void OnListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                Commit();
            }
        }

        private void OnOk(object sender, EventArgs e) { Commit(); }

        private void OnCancel(object sender, EventArgs e)
        {
            SelectedIdx = -1;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Commit()
        {
            if (lstLep.SelectedIndices.Count == 0)
            {
                // 스캐너 장비는 방향키만 있는 경우가 있어 선택이 비어 있을 수 있다
                if (lstLep.Items.Count == 0) { OnCancel(null, null); return; }
                lstLep.Items[0].Selected = true;
            }

            SelectedIdx = lstLep.SelectedIndices[0];
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    /// <summary>계열 선택 진입점. 화면에서는 이것만 쓴다.</summary>
    public static class LepSelect
    {
        /// <summary>
        /// 계열을 고르게 하고 선택된 인덱스를 돌려준다.
        ///
        ///   0건       -> -1 (호출부가 "계열 없음" 처리)
        ///   1건       -> 0  (팝업을 띄우지 않는다. 원본도 바로 진행한다)
        ///   2건 이상  -> 팝업. 취소하면 -1
        ///
        /// 인덱스를 돌려주는 이유는 [320] 처럼 계열 문자열이 아니라
        /// 목록의 행을 골라야 하는 화면이 있기 때문이다.
        /// </summary>
        public static int Pick(ArrayList items)
        {
            return Pick(items, null);
        }

        public static int Pick(ArrayList items, string title)
        {
            if (items == null || items.Count == 0) return -1;
            if (items.Count == 1) return 0;

            using (LepSelectForm f = new LepSelectForm(items, title))
            {
                f.ShowDialog();
                return f.SelectedIdx;
            }
        }
    }
}
