namespace MobisHaims.Screens
{
    partial class S000_MainMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnInbound = new System.Windows.Forms.Button();
            this.btnOutbound = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnInquiry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInbound
            // 
            this.btnInbound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(66)))));
            this.btnInbound.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnInbound.ForeColor = System.Drawing.Color.White;
            this.btnInbound.Location = new System.Drawing.Point(12, 6);
            this.btnInbound.Name = "btnInbound";
            this.btnInbound.Size = new System.Drawing.Size(457, 89);
            this.btnInbound.TabIndex = 0;
            this.btnInbound.Text = "입고관리";
            this.btnInbound.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnOutbound
            // 
            this.btnOutbound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(66)))));
            this.btnOutbound.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnOutbound.ForeColor = System.Drawing.Color.White;
            this.btnOutbound.Location = new System.Drawing.Point(12, 99);
            this.btnOutbound.Name = "btnOutbound";
            this.btnOutbound.Size = new System.Drawing.Size(457, 89);
            this.btnOutbound.TabIndex = 1;
            this.btnOutbound.Text = "출고관리";
            this.btnOutbound.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(66)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.White;
            this.btnStock.Location = new System.Drawing.Point(12, 285);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(457, 89);
            this.btnStock.TabIndex = 2;
            this.btnStock.Text = "재고관리";
            this.btnStock.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(66)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(12, 192);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(457, 89);
            this.btnLoc.TabIndex = 3;
            this.btnLoc.Text = "LOC 관리";
            this.btnLoc.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnInquiry
            // 
            this.btnInquiry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(66)))));
            this.btnInquiry.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnInquiry.ForeColor = System.Drawing.Color.White;
            this.btnInquiry.Location = new System.Drawing.Point(12, 378);
            this.btnInquiry.Name = "btnInquiry";
            this.btnInquiry.Size = new System.Drawing.Size(457, 89);
            this.btnInquiry.TabIndex = 4;
            this.btnInquiry.Text = "조회관리";
            this.btnInquiry.Click += new System.EventHandler(this.OnTileClick);
            // 
            // S000_MainMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(85)))), ((int)(((byte)(104)))));
            this.Controls.Add(this.btnInbound);
            this.Controls.Add(this.btnOutbound);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.btnInquiry);
            this.Name = "S000_MainMenu";
            this.Size = new System.Drawing.Size(480, 528);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInbound;
        private System.Windows.Forms.Button btnOutbound;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnInquiry;
    }
}
