namespace HaimsPda
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.CheckBox chkSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 컨트롤 생성만 한다. 좌표/크기/폰트는 LayoutControls() 에서
        /// 실행 시 해상도에 맞춰 계산하므로 여기서는 지정하지 않는다.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.chkSave = new System.Windows.Forms.CheckBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnEnv = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.Location = new System.Drawing.Point(148, 322);
            this.txtId.MaxLength = 10;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(178, 41);
            this.txtId.TabIndex = 0;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            // 
            // txtPw
            // 
            this.txtPw.BackColor = System.Drawing.Color.White;
            this.txtPw.Location = new System.Drawing.Point(148, 368);
            this.txtPw.MaxLength = 20;
            this.txtPw.Name = "txtPw";
            this.txtPw.PasswordChar = '*';
            this.txtPw.Size = new System.Drawing.Size(178, 41);
            this.txtPw.TabIndex = 1;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            // 
            // chkSave
            // 
            this.chkSave.Checked = true;
            this.chkSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkSave.Location = new System.Drawing.Point(148, 280);
            this.chkSave.Name = "chkSave";
            this.chkSave.Size = new System.Drawing.Size(178, 38);
            this.chkSave.TabIndex = 2;
            this.chkSave.Text = "저장";
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnChange.Location = new System.Drawing.Point(330, 372);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(142, 38);
            this.btnChange.TabIndex = 6;
            this.btnChange.Text = "변경";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLogin.Location = new System.Drawing.Point(8, 418);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(142, 62);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "로그인";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnEnv
            // 
            this.btnEnv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnEnv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEnv.Location = new System.Drawing.Point(169, 418);
            this.btnEnv.Name = "btnEnv";
            this.btnEnv.Size = new System.Drawing.Size(142, 62);
            this.btnEnv.TabIndex = 8;
            this.btnEnv.Text = "호스트";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.Location = new System.Drawing.Point(330, 418);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(142, 62);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "종료";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 324);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 40);
            this.label1.Text = "사원번호";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 40);
            this.label2.Text = "비밀번호";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(64)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEnv);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.chkSave);
            this.Location = new System.Drawing.Point(0, 52);
            this.Name = "LoginForm";
            this.Text = "LoginPage";
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnEnv;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
