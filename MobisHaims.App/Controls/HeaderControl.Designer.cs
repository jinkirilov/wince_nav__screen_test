namespace MobisHaims.Controls
{
    partial class HeaderControl
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
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new MobisHaims.Controls.VLabel();
            this.SuspendLayout();
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(52, 30);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "≡";
            this.btnMenu.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(296, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.OnCloseClick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.lblTitle.Location = new System.Drawing.Point(52, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(244, 30);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "vLabel1";
            // 
            // HeaderControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnClose);
            this.Name = "HeaderControl";
            this.Size = new System.Drawing.Size(348, 30);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMenu;
        private VLabel lblTitle;
    }
}
