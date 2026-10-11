namespace HaimsPda.Screens
{
    partial class S200_OutboundMenu
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
            this.btnPickTarget = new System.Windows.Forms.Button();
            this.btnShopTarget = new System.Windows.Forms.Button();
            this.btnShopTarget2 = new System.Windows.Forms.Button();
            this.btnSlipPicking = new System.Windows.Forms.Button();
            this.btnSlipSelect = new System.Windows.Forms.Button();
            this.btnTagClassify = new System.Windows.Forms.Button();
            this.btnCaseClassify = new System.Windows.Forms.Button();
            this.btnSlipClassify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPickTarget
            // 
            this.btnPickTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPickTarget.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnPickTarget.ForeColor = System.Drawing.Color.White;
            this.btnPickTarget.Location = new System.Drawing.Point(4, 8);
            this.btnPickTarget.Name = "btnPickTarget";
            this.btnPickTarget.Size = new System.Drawing.Size(232, 80);
            this.btnPickTarget.TabIndex = 0;
            this.btnPickTarget.Text = "피킹대상/처리";
            this.btnPickTarget.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnShopTarget
            // 
            this.btnShopTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnShopTarget.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnShopTarget.ForeColor = System.Drawing.Color.White;
            this.btnShopTarget.Location = new System.Drawing.Point(245, 8);
            this.btnShopTarget.Name = "btnShopTarget";
            this.btnShopTarget.Size = new System.Drawing.Size(232, 80);
            this.btnShopTarget.TabIndex = 1;
            this.btnShopTarget.Text = "피킹대상조회(전문점)";
            this.btnShopTarget.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnShopTarget2
            // 
            this.btnShopTarget2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnShopTarget2.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnShopTarget2.ForeColor = System.Drawing.Color.White;
            this.btnShopTarget2.Location = new System.Drawing.Point(4, 109);
            this.btnShopTarget2.Name = "btnShopTarget2";
            this.btnShopTarget2.Size = new System.Drawing.Size(232, 80);
            this.btnShopTarget2.TabIndex = 2;
            this.btnShopTarget2.Text = "피킹대상(전문점)";
            this.btnShopTarget2.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnSlipPicking
            // 
            this.btnSlipPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSlipPicking.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnSlipPicking.ForeColor = System.Drawing.Color.White;
            this.btnSlipPicking.Location = new System.Drawing.Point(245, 109);
            this.btnSlipPicking.Name = "btnSlipPicking";
            this.btnSlipPicking.Size = new System.Drawing.Size(232, 80);
            this.btnSlipPicking.TabIndex = 3;
            this.btnSlipPicking.Text = "피킹(판매증표출고)";
            this.btnSlipPicking.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnSlipSelect
            // 
            this.btnSlipSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSlipSelect.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnSlipSelect.ForeColor = System.Drawing.Color.White;
            this.btnSlipSelect.Location = new System.Drawing.Point(4, 210);
            this.btnSlipSelect.Name = "btnSlipSelect";
            this.btnSlipSelect.Size = new System.Drawing.Size(232, 80);
            this.btnSlipSelect.TabIndex = 4;
            this.btnSlipSelect.Text = "판매증표선택";
            this.btnSlipSelect.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnTagClassify
            // 
            this.btnTagClassify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnTagClassify.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnTagClassify.ForeColor = System.Drawing.Color.White;
            this.btnTagClassify.Location = new System.Drawing.Point(245, 210);
            this.btnTagClassify.Name = "btnTagClassify";
            this.btnTagClassify.Size = new System.Drawing.Size(232, 80);
            this.btnTagClassify.TabIndex = 5;
            this.btnTagClassify.Text = "피킹분류(TAG)";
            this.btnTagClassify.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnCaseClassify
            // 
            this.btnCaseClassify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnCaseClassify.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnCaseClassify.ForeColor = System.Drawing.Color.White;
            this.btnCaseClassify.Location = new System.Drawing.Point(4, 311);
            this.btnCaseClassify.Name = "btnCaseClassify";
            this.btnCaseClassify.Size = new System.Drawing.Size(232, 80);
            this.btnCaseClassify.TabIndex = 6;
            this.btnCaseClassify.Text = "배송분류(CASE)";
            this.btnCaseClassify.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnSlipClassify
            // 
            this.btnSlipClassify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSlipClassify.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnSlipClassify.ForeColor = System.Drawing.Color.White;
            this.btnSlipClassify.Location = new System.Drawing.Point(245, 311);
            this.btnSlipClassify.Name = "btnSlipClassify";
            this.btnSlipClassify.Size = new System.Drawing.Size(232, 80);
            this.btnSlipClassify.TabIndex = 7;
            this.btnSlipClassify.Text = "피킹분류(증표)";
            this.btnSlipClassify.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // S200_OutboundMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.btnPickTarget);
            this.Controls.Add(this.btnShopTarget);
            this.Controls.Add(this.btnShopTarget2);
            this.Controls.Add(this.btnSlipPicking);
            this.Controls.Add(this.btnSlipSelect);
            this.Controls.Add(this.btnTagClassify);
            this.Controls.Add(this.btnCaseClassify);
            this.Controls.Add(this.btnSlipClassify);
            this.Name = "S200_OutboundMenu";
            this.Size = new System.Drawing.Size(480, 484);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPickTarget;
        private System.Windows.Forms.Button btnShopTarget;
        private System.Windows.Forms.Button btnShopTarget2;
        private System.Windows.Forms.Button btnSlipPicking;
        private System.Windows.Forms.Button btnSlipSelect;
        private System.Windows.Forms.Button btnTagClassify;
        private System.Windows.Forms.Button btnCaseClassify;
        private System.Windows.Forms.Button btnSlipClassify;
    }
}
