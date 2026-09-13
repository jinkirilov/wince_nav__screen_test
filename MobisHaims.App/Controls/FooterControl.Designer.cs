

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
            this.btnJump = new System.Windows.Forms.PictureBox();
            this.lbMsg = new MobisHaims.Controls.VLabel();
            this.SuspendLayout();
            // 
            // btnJump
            // 
            this.btnJump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJump.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnJump.Location = new System.Drawing.Point(0, 0);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(45, 40);
            this.btnJump.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnJump.Click += new System.EventHandler(this.OnJumpClick);
            // 
            // lbMsg
            // 
            this.lbMsg.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lbMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(62)))));
            this.lbMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMsg.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbMsg.ForeColor = System.Drawing.Color.White;
            this.lbMsg.Location = new System.Drawing.Point(45, 0);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(435, 40);
            this.lbMsg.TabIndex = 2;
            this.lbMsg.Text = "lbMsg 테스트대리점 홍길동 20206-09-23 12:34";
            // 
            // FooterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.btnJump);
            this.Name = "FooterControl";
            this.Size = new System.Drawing.Size(480, 40);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox btnJump;
        private VLabel lbMsg;
    }
}
