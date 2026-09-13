namespace MobisHaims.Screens
{
    partial class S300_StockMenu
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
            this.btnPartStock = new System.Windows.Forms.Button();
            this.btnLocStock = new System.Windows.Forms.Button();
            this.btnPartInfo = new System.Windows.Forms.Button();
            this.btnMoveHist = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnLocInventory = new System.Windows.Forms.Button();
            this.btnInvTarget = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPartStock
            // 
            this.btnPartStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnPartStock.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnPartStock.ForeColor = System.Drawing.Color.White;
            this.btnPartStock.Location = new System.Drawing.Point(4, 8);
            this.btnPartStock.Name = "btnPartStock";
            this.btnPartStock.Size = new System.Drawing.Size(232, 80);
            this.btnPartStock.TabIndex = 0;
            this.btnPartStock.Text = "파트별재고";
            this.btnPartStock.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnLocStock
            // 
            this.btnLocStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnLocStock.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnLocStock.ForeColor = System.Drawing.Color.White;
            this.btnLocStock.Location = new System.Drawing.Point(245, 8);
            this.btnLocStock.Name = "btnLocStock";
            this.btnLocStock.Size = new System.Drawing.Size(232, 80);
            this.btnLocStock.TabIndex = 1;
            this.btnLocStock.Text = "LOC별재고";
            this.btnLocStock.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnPartInfo
            // 
            this.btnPartInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnPartInfo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnPartInfo.ForeColor = System.Drawing.Color.White;
            this.btnPartInfo.Location = new System.Drawing.Point(4, 109);
            this.btnPartInfo.Name = "btnPartInfo";
            this.btnPartInfo.Size = new System.Drawing.Size(232, 80);
            this.btnPartInfo.TabIndex = 2;
            this.btnPartInfo.Text = "부품정보조회";
            this.btnPartInfo.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnMoveHist
            // 
            this.btnMoveHist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnMoveHist.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnMoveHist.ForeColor = System.Drawing.Color.White;
            this.btnMoveHist.Location = new System.Drawing.Point(245, 109);
            this.btnMoveHist.Name = "btnMoveHist";
            this.btnMoveHist.Size = new System.Drawing.Size(232, 80);
            this.btnMoveHist.TabIndex = 3;
            this.btnMoveHist.Text = "부품수불이력";
            this.btnMoveHist.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnInventory.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(4, 210);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(232, 80);
            this.btnInventory.TabIndex = 4;
            this.btnInventory.Text = "재물조사";
            this.btnInventory.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnLocInventory
            // 
            this.btnLocInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnLocInventory.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnLocInventory.ForeColor = System.Drawing.Color.White;
            this.btnLocInventory.Location = new System.Drawing.Point(245, 210);
            this.btnLocInventory.Name = "btnLocInventory";
            this.btnLocInventory.Size = new System.Drawing.Size(232, 80);
            this.btnLocInventory.TabIndex = 5;
            this.btnLocInventory.Text = "재물조사(LOC)";
            this.btnLocInventory.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnInvTarget
            // 
            this.btnInvTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnInvTarget.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnInvTarget.ForeColor = System.Drawing.Color.White;
            this.btnInvTarget.Location = new System.Drawing.Point(4, 311);
            this.btnInvTarget.Name = "btnInvTarget";
            this.btnInvTarget.Size = new System.Drawing.Size(232, 80);
            this.btnInvTarget.TabIndex = 6;
            this.btnInvTarget.Text = "재물조사대상";
            this.btnInvTarget.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnAdjust
            // 
            this.btnAdjust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(106)))));
            this.btnAdjust.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdjust.ForeColor = System.Drawing.Color.White;
            this.btnAdjust.Location = new System.Drawing.Point(245, 311);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(232, 80);
            this.btnAdjust.TabIndex = 7;
            this.btnAdjust.Text = "재고조정";
            this.btnAdjust.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // S300_StockMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(105)))), ((int)(((byte)(125)))));
            this.Controls.Add(this.btnPartStock);
            this.Controls.Add(this.btnLocStock);
            this.Controls.Add(this.btnPartInfo);
            this.Controls.Add(this.btnMoveHist);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.btnLocInventory);
            this.Controls.Add(this.btnInvTarget);
            this.Controls.Add(this.btnAdjust);
            this.Name = "S300_StockMenu";
            this.Size = new System.Drawing.Size(480, 484);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPartStock;
        private System.Windows.Forms.Button btnLocStock;
        private System.Windows.Forms.Button btnPartInfo;
        private System.Windows.Forms.Button btnMoveHist;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnLocInventory;
        private System.Windows.Forms.Button btnInvTarget;
        private System.Windows.Forms.Button btnAdjust;
    }
}
