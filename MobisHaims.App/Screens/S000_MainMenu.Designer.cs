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
            this.btnDelivery = new System.Windows.Forms.Button();
            this.btnPallet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInbound
            // 
            this.btnInbound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnInbound.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnInbound.ForeColor = System.Drawing.Color.White;
            this.btnInbound.Location = new System.Drawing.Point(8, 8);
            this.btnInbound.Name = "btnInbound";
            this.btnInbound.Size = new System.Drawing.Size(464, 77);
            this.btnInbound.TabIndex = 0;
            this.btnInbound.Text = "입고메뉴";
            this.btnInbound.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnOutbound
            // 
            this.btnOutbound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnOutbound.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnOutbound.ForeColor = System.Drawing.Color.White;
            this.btnOutbound.Location = new System.Drawing.Point(8, 93);
            this.btnOutbound.Name = "btnOutbound";
            this.btnOutbound.Size = new System.Drawing.Size(464, 77);
            this.btnOutbound.TabIndex = 1;
            this.btnOutbound.Text = "출고메뉴";
            this.btnOutbound.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.White;
            this.btnStock.Location = new System.Drawing.Point(8, 178);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(464, 77);
            this.btnStock.TabIndex = 2;
            this.btnStock.Text = "재고메뉴";
            this.btnStock.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(8, 263);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(464, 77);
            this.btnLoc.TabIndex = 3;
            this.btnLoc.Text = "LOC메뉴";
            this.btnLoc.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnDelivery
            // 
            this.btnDelivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDelivery.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelivery.ForeColor = System.Drawing.Color.White;
            this.btnDelivery.Location = new System.Drawing.Point(8, 348);
            this.btnDelivery.Name = "btnDelivery";
            this.btnDelivery.Size = new System.Drawing.Size(464, 77);
            this.btnDelivery.TabIndex = 4;
            this.btnDelivery.Text = "배송메뉴";
            this.btnDelivery.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnPallet
            // 
            this.btnPallet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPallet.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnPallet.ForeColor = System.Drawing.Color.White;
            this.btnPallet.Location = new System.Drawing.Point(8, 433);
            this.btnPallet.Name = "btnPallet";
            this.btnPallet.Size = new System.Drawing.Size(464, 77);
            this.btnPallet.TabIndex = 5;
            this.btnPallet.Text = "팔레트 메뉴";
            this.btnPallet.Click += new System.EventHandler(this.OnTileClick);
            // 
            // S000_MainMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnPallet);
            this.Controls.Add(this.btnInbound);
            this.Controls.Add(this.btnOutbound);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.btnDelivery);
            this.Name = "S000_MainMenu";
            this.Size = new System.Drawing.Size(480, 536);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInbound;
        private System.Windows.Forms.Button btnOutbound;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnDelivery;
        private System.Windows.Forms.Button btnPallet;
    }
}
