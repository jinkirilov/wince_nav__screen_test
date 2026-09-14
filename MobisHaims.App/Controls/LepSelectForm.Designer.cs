namespace MobisHaims.Controls
{
    partial class LepSelectForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lstLep;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.Panel _buttons;
        private System.Windows.Forms.Button btnOk;
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
            this.lstLep = new System.Windows.Forms.ListView();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(8, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(464, 44);
            this.lblTitle.Text = "계열 선택";
            // 
            // lstLep
            // 
            this.lstLep.Columns.Add(this.colLep);
            this.lstLep.FullRowSelect = true;
            this.lstLep.Location = new System.Drawing.Point(8, 64);
            this.lstLep.Name = "lstLep";
            this.lstLep.Size = new System.Drawing.Size(464, 456);
            this.lstLep.TabIndex = 0;
            this.lstLep.View = System.Windows.Forms.View.Details;
            this.lstLep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListKeyDown);
            // 
            // colLep
            // 
            this.colLep.Text = "계열";
            this.colLep.Width = 440;
            // 
            // _buttons
            // 
            this._buttons.Controls.Add(this.btnOk);
            this._buttons.Controls.Add(this.btnCancel);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 526);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 62);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnOk.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.Location = new System.Drawing.Point(8, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(232, 50);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "선택";
            this.btnOk.Click += new System.EventHandler(this.OnOk);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnCancel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Location = new System.Drawing.Point(244, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(228, 50);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // LepSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(64)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstLep);
            this.Controls.Add(this._buttons);
            this.Name = "LepSelectForm";
            this.Text = "계열 선택";
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
