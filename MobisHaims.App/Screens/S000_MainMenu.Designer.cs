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
            this.btnInquiry = new System.Windows.Forms.Button();
            this.btnDelivery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInbound
            // 
            this.btnInbound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnInbound.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnInbound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInbound.Location = new System.Drawing.Point(8, 8);
            this.btnInbound.Name = "btnInbound";
            this.btnInbound.Size = new System.Drawing.Size(464, 96);
            this.btnInbound.TabIndex = 0;
            this.btnInbound.Text = "입고메뉴";
            this.btnInbound.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnOutbound
            // 
            this.btnOutbound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnOutbound.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnOutbound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOutbound.Location = new System.Drawing.Point(8, 112);
            this.btnOutbound.Name = "btnOutbound";
            this.btnOutbound.Size = new System.Drawing.Size(464, 96);
            this.btnOutbound.TabIndex = 1;
            this.btnOutbound.Text = "출고메뉴";
            this.btnOutbound.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Location = new System.Drawing.Point(8, 216);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(464, 96);
            this.btnStock.TabIndex = 2;
            this.btnStock.Text = "재고메뉴";
            this.btnStock.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnInquiry
            // 
            this.btnInquiry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnInquiry.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnInquiry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInquiry.Location = new System.Drawing.Point(8, 320);
            this.btnInquiry.Name = "btnInquiry";
            this.btnInquiry.Size = new System.Drawing.Size(464, 96);
            this.btnInquiry.TabIndex = 3;
            this.btnInquiry.Text = "조회메뉴";
            this.btnInquiry.Click += new System.EventHandler(this.OnTileClick);
            // 
            // btnDelivery
            // 
            this.btnDelivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnDelivery.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelivery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDelivery.Location = new System.Drawing.Point(8, 424);
            this.btnDelivery.Name = "btnDelivery";
            this.btnDelivery.Size = new System.Drawing.Size(464, 96);
            this.btnDelivery.TabIndex = 4;
            this.btnDelivery.Text = "배송메뉴";
            this.btnDelivery.Click += new System.EventHandler(this.OnTileClick);
            // 
            // S000_MainMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(64)))), ((int)(((byte)(106)))));
            this.Controls.Add(this.btnInbound);
            this.Controls.Add(this.btnOutbound);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnInquiry);
            this.Controls.Add(this.btnDelivery);
            this.Name = "S000_MainMenu";
            this.Size = new System.Drawing.Size(480, 528);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInbound;
        private System.Windows.Forms.Button btnOutbound;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnInquiry;
        private System.Windows.Forms.Button btnDelivery;
    }
}
