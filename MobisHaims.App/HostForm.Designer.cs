namespace HaimsPda
{
    partial class HostForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUrlCap;
        private System.Windows.Forms.Label lblTimeoutCap;
        private System.Windows.Forms.Label lblResult;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUrlCap = new System.Windows.Forms.Label();
            this.lblTimeoutCap = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(8, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(464, 44);
            this.lblTitle.Text = "호스트 설정";
            // 
            // lblUrlCap
            // 
            this.lblUrlCap.ForeColor = System.Drawing.Color.White;
            this.lblUrlCap.Location = new System.Drawing.Point(8, 86);
            this.lblUrlCap.Name = "lblUrlCap";
            this.lblUrlCap.Size = new System.Drawing.Size(464, 36);
            this.lblUrlCap.Text = "서버 주소";
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.Color.White;
            this.txtUrl.Location = new System.Drawing.Point(8, 124);
            this.txtUrl.MaxLength = 200;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(464, 41);
            this.txtUrl.TabIndex = 0;
            // 
            // lblTimeoutCap
            // 
            this.lblTimeoutCap.ForeColor = System.Drawing.Color.White;
            this.lblTimeoutCap.Location = new System.Drawing.Point(8, 178);
            this.lblTimeoutCap.Name = "lblTimeoutCap";
            this.lblTimeoutCap.Size = new System.Drawing.Size(250, 36);
            this.lblTimeoutCap.Text = "타임아웃(초)";
            // 
            // txtTimeout
            // 
            this.txtTimeout.BackColor = System.Drawing.Color.White;
            this.txtTimeout.Location = new System.Drawing.Point(262, 174);
            this.txtTimeout.MaxLength = 3;
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(210, 41);
            this.txtTimeout.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(218)))), ((int)(((byte)(224)))));
            this.lblResult.Location = new System.Drawing.Point(8, 232);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(464, 110);
            this.lblResult.Text = "";
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTest.Location = new System.Drawing.Point(8, 352);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(225, 58);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "접속확인";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDefault.Location = new System.Drawing.Point(247, 352);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(225, 58);
            this.btnDefault.TabIndex = 3;
            this.btnDefault.Text = "기본값";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Location = new System.Drawing.Point(8, 424);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(225, 62);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Location = new System.Drawing.Point(247, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(225, 62);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // HostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(64)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.lblTimeoutCap);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblUrlCap);
            this.Controls.Add(this.lblTitle);
            this.Location = new System.Drawing.Point(0, 52);
            this.Name = "HostForm";
            this.Text = "HostPage";
            this.ResumeLayout(false);
        }
    }
}
