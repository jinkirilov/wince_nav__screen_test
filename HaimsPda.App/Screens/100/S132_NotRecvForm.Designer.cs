namespace HaimsPda.Screens
{
    partial class S132_NotRecvForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVchnoCap;
        private System.Windows.Forms.Label lblVchno;
        private System.Windows.Forms.Label lblWsfCap;
        private System.Windows.Forms.Label lblWsf;
        private System.Windows.Forms.Label lblNarCap;
        private System.Windows.Forms.TextBox txtNar;
        private System.Windows.Forms.Label lblReasonCap;
        private System.Windows.Forms.ComboBox cboReason;
        private System.Windows.Forms.Label lblInCap;
        private System.Windows.Forms.Label lblIn;
        private System.Windows.Forms.Panel _bottom;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVchnoCap = new System.Windows.Forms.Label();
            this.lblVchno = new System.Windows.Forms.Label();
            this.lblWsfCap = new System.Windows.Forms.Label();
            this.lblWsf = new System.Windows.Forms.Label();
            this.lblNarCap = new System.Windows.Forms.Label();
            this.txtNar = new System.Windows.Forms.TextBox();
            this.lblReasonCap = new System.Windows.Forms.Label();
            this.cboReason = new System.Windows.Forms.ComboBox();
            this.lblInCap = new System.Windows.Forms.Label();
            this.lblIn = new System.Windows.Forms.Label();
            this._bottom = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(464, 56);
            this.lblTitle.Text = "[132] 미수령등록";
            // 
            // lblVchnoCap
            // 
            this.lblVchnoCap.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblVchnoCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblVchnoCap.Location = new System.Drawing.Point(8, 82);
            this.lblVchnoCap.Name = "lblVchnoCap";
            this.lblVchnoCap.Size = new System.Drawing.Size(140, 36);
            this.lblVchnoCap.Text = "할당번호";
            // 
            // lblVchno
            // 
            this.lblVchno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblVchno.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            this.lblVchno.ForeColor = System.Drawing.Color.Black;
            this.lblVchno.Location = new System.Drawing.Point(152, 76);
            this.lblVchno.Name = "lblVchno";
            this.lblVchno.Size = new System.Drawing.Size(320, 40);
            // 
            // lblWsfCap
            // 
            this.lblWsfCap.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblWsfCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWsfCap.Location = new System.Drawing.Point(8, 134);
            this.lblWsfCap.Name = "lblWsfCap";
            this.lblWsfCap.Size = new System.Drawing.Size(140, 36);
            this.lblWsfCap.Text = "입고대상수량";
            // 
            // lblWsf
            // 
            this.lblWsf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblWsf.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblWsf.ForeColor = System.Drawing.Color.Black;
            this.lblWsf.Location = new System.Drawing.Point(152, 128);
            this.lblWsf.Name = "lblWsf";
            this.lblWsf.Size = new System.Drawing.Size(150, 40);
            this.lblWsf.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNarCap
            // 
            this.lblNarCap.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblNarCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNarCap.Location = new System.Drawing.Point(8, 188);
            this.lblNarCap.Name = "lblNarCap";
            this.lblNarCap.Size = new System.Drawing.Size(140, 36);
            this.lblNarCap.Text = "미수령수량";
            // 
            // txtNar
            // 
            this.txtNar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtNar.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtNar.Location = new System.Drawing.Point(152, 180);
            this.txtNar.Name = "txtNar";
            this.txtNar.Size = new System.Drawing.Size(150, 49);
            this.txtNar.TabIndex = 0;
            this.txtNar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnNarKeyDown);
            // 
            // lblReasonCap
            // 
            this.lblReasonCap.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblReasonCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReasonCap.Location = new System.Drawing.Point(8, 246);
            this.lblReasonCap.Name = "lblReasonCap";
            this.lblReasonCap.Size = new System.Drawing.Size(140, 36);
            this.lblReasonCap.Text = "미수령사유";
            // 
            // cboReason
            // 
            this.cboReason.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            this.cboReason.Location = new System.Drawing.Point(152, 240);
            this.cboReason.Name = "cboReason";
            this.cboReason.Size = new System.Drawing.Size(320, 40);
            this.cboReason.TabIndex = 1;
            this.cboReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnReasonKeyDown);
            // 
            // lblInCap
            // 
            this.lblInCap.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblInCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInCap.Location = new System.Drawing.Point(8, 302);
            this.lblInCap.Name = "lblInCap";
            this.lblInCap.Size = new System.Drawing.Size(140, 36);
            this.lblInCap.Text = "입고수량";
            // 
            // lblIn
            // 
            this.lblIn.BackColor = System.Drawing.Color.White;
            this.lblIn.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblIn.Location = new System.Drawing.Point(152, 296);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new System.Drawing.Size(150, 40);
            this.lblIn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _bottom
            // 
            this._bottom.Controls.Add(this.btnOk);
            this._bottom.Controls.Add(this.btnClear);
            this._bottom.Controls.Add(this.btnCancel);
            this._bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottom.Location = new System.Drawing.Point(0, 526);
            this._bottom.Name = "_bottom";
            this._bottom.Size = new System.Drawing.Size(480, 62);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnOk.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.Location = new System.Drawing.Point(8, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(152, 50);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "리턴";
            this.btnOk.Click += new System.EventHandler(this.OnOk);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(164, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(152, 50);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnCancel.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Location = new System.Drawing.Point(320, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 50);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // S132_NotRecvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(64)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblVchnoCap);
            this.Controls.Add(this.lblVchno);
            this.Controls.Add(this.lblWsfCap);
            this.Controls.Add(this.lblWsf);
            this.Controls.Add(this.lblNarCap);
            this.Controls.Add(this.txtNar);
            this.Controls.Add(this.lblReasonCap);
            this.Controls.Add(this.cboReason);
            this.Controls.Add(this.lblInCap);
            this.Controls.Add(this.lblIn);
            this.Controls.Add(this._bottom);
            this.Name = "S132_NotRecvForm";
            this.Text = "미수령등록";
            this._bottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
