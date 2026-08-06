

namespace MobisHaims.Controls
{
    partial class FooterControl
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.pnlDot = new System.Windows.Forms.Panel();
            this.btnJump = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMsg.Location = new System.Drawing.Point(0, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(100, 20);
            // 
            // pnlDot
            // 
            this.pnlDot.BackColor = System.Drawing.Color.Gray;
            this.pnlDot.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDot.Location = new System.Drawing.Point(0, 0);
            this.pnlDot.Name = "pnlDot";
            this.pnlDot.Size = new System.Drawing.Size(12, 100);
            // 
            // btnJump
            // 
            this.btnJump.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnJump.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btnJump.Location = new System.Drawing.Point(0, 0);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(52, 20);
            this.btnJump.TabIndex = 0;
            this.btnJump.Text = "JUMP";
            this.btnJump.Click += new System.EventHandler(this.OnJumpClick);
            //
            // FooterControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "FooterControl";
            this.Size = new System.Drawing.Size(444, 233);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnJump;
        private System.Windows.Forms.Panel pnlDot;
        private System.Windows.Forms.Label lblMsg;
    }
}