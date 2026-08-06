namespace MobisHaims
{
    partial class ShellForm
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
            this._content = new System.Windows.Forms.Panel();
            this._header = new MobisHaims.Controls.HeaderControl();
            this._footer = new MobisHaims.Controls.FooterControl();
            this.SuspendLayout();
            //
            // _content
            //
            this._content.Dock = System.Windows.Forms.DockStyle.Fill;
            this._content.Location = new System.Drawing.Point(0, 30);
            this._content.Name = "_content";
            this._content.Size = new System.Drawing.Size(480, 528);
            //
            // _header
            //
            this._header.Dock = System.Windows.Forms.DockStyle.Top;
            this._header.Location = new System.Drawing.Point(0, 0);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(480, 30);
            this._header.MenuClicked += new System.EventHandler(this.OnHeaderMenuClicked);
            this._header.CloseClicked += new System.EventHandler(this.OnHeaderCloseClicked);
            //
            // _footer
            //
            this._footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footer.Location = new System.Drawing.Point(0, 558);
            this._footer.Name = "_footer";
            this._footer.Size = new System.Drawing.Size(480, 30);
            this._footer.JumpRequested += new MobisHaims.Controls.JumpEventHandler(this.OnJump);
            //
            // ShellForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(480, 588);
            // Dock 우선순위: Fill(_content) 를 먼저 Add
            this.Controls.Add(this._content);
            this.Controls.Add(this._header);
            this.Controls.Add(this._footer);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 52);
            this.MinimizeBox = false;
            this.Name = "ShellForm";
            this.Text = "HAIMS PLUS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        private MobisHaims.Controls.FooterControl _footer;
        private MobisHaims.Controls.HeaderControl _header;
        private System.Windows.Forms.Panel _content;
    }
}
