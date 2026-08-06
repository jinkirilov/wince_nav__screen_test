using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MobisHaims.Controls
{
    // 화면번호 직접입력 이동(JUMP) 다이얼로그
    public sealed partial class JumpForm : Form
    {
        public int ScreenNo;

        public JumpForm()
        {
            InitializeComponent();
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            try
            {
                ScreenNo = int.Parse(txtScreenNo.Text.Trim());
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("숫자 화면번호를 입력하세요.");
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
