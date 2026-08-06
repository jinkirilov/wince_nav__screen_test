namespace MobisHaims.Controls
{
    partial class JumpForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCap = new System.Windows.Forms.Label();
            this.txtScreenNo = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblCap
            //
            this.lblCap.Location = new System.Drawing.Point(12, 16);
            this.lblCap.Name = "lblCap";
            this.lblCap.Size = new System.Drawing.Size(60, 20);
            this.lblCap.Text = "화면번호";
            //
            // txtScreenNo
            //
            this.txtScreenNo.Location = new System.Drawing.Point(78, 12);
            this.txtScreenNo.Name = "txtScreenNo";
            this.txtScreenNo.Size = new System.Drawing.Size(120, 23);
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(12, 52);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "이동";
            this.btnOk.Click += new System.EventHandler(this.OnOkClick);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(104, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.OnCancelClick);
            //
            // JumpForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(212, 94);
            this.Controls.Add(this.lblCap);
            this.Controls.Add(this.txtScreenNo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.MinimizeBox = false;
            this.Name = "JumpForm";
            this.Text = "화면 이동 (JUMP)";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtScreenNo;
        private System.Windows.Forms.Label lblCap;
    }
}
