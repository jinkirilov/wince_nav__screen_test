namespace MobisHaims.Screens
{
    partial class S100_InboundMenu
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
            this.btnSiteClassify = new System.Windows.Forms.Button();
            this.btnShopClassify = new System.Windows.Forms.Button();
            this.btnInboundSave = new System.Windows.Forms.Button();
            this.btnSortInboundSave = new System.Windows.Forms.Button();
            this.btnReserveList = new System.Windows.Forms.Button();
            this.btnWaitInquiry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSiteClassify
            // 
            this.btnSiteClassify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnSiteClassify.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSiteClassify.ForeColor = System.Drawing.Color.White;
            this.btnSiteClassify.Location = new System.Drawing.Point(8, 8);
            this.btnSiteClassify.Name = "btnSiteClassify";
            this.btnSiteClassify.Size = new System.Drawing.Size(469, 76);
            this.btnSiteClassify.TabIndex = 0;
            this.btnSiteClassify.Text = "사업소입고분류";
            this.btnSiteClassify.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnShopClassify
            // 
            this.btnShopClassify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnShopClassify.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnShopClassify.ForeColor = System.Drawing.Color.White;
            this.btnShopClassify.Location = new System.Drawing.Point(8, 96);
            this.btnShopClassify.Name = "btnShopClassify";
            this.btnShopClassify.Size = new System.Drawing.Size(469, 66);
            this.btnShopClassify.TabIndex = 1;
            this.btnShopClassify.Text = "전문점 입고분류";
            this.btnShopClassify.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnInboundSave
            // 
            this.btnInboundSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnInboundSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnInboundSave.ForeColor = System.Drawing.Color.White;
            this.btnInboundSave.Location = new System.Drawing.Point(8, 184);
            this.btnInboundSave.Name = "btnInboundSave";
            this.btnInboundSave.Size = new System.Drawing.Size(469, 66);
            this.btnInboundSave.TabIndex = 2;
            this.btnInboundSave.Text = "입고저장";
            this.btnInboundSave.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnSortInboundSave
            // 
            this.btnSortInboundSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnSortInboundSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSortInboundSave.ForeColor = System.Drawing.Color.White;
            this.btnSortInboundSave.Location = new System.Drawing.Point(8, 360);
            this.btnSortInboundSave.Name = "btnSortInboundSave";
            this.btnSortInboundSave.Size = new System.Drawing.Size(469, 66);
            this.btnSortInboundSave.TabIndex = 3;
            this.btnSortInboundSave.Text = "정렬입고 저장";
            this.btnSortInboundSave.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnReserveList
            // 
            this.btnReserveList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnReserveList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnReserveList.ForeColor = System.Drawing.Color.White;
            this.btnReserveList.Location = new System.Drawing.Point(8, 272);
            this.btnReserveList.Name = "btnReserveList";
            this.btnReserveList.Size = new System.Drawing.Size(469, 66);
            this.btnReserveList.TabIndex = 4;
            this.btnReserveList.Text = "예약내역";
            this.btnReserveList.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnWaitInquiry
            // 
            this.btnWaitInquiry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnWaitInquiry.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnWaitInquiry.ForeColor = System.Drawing.Color.White;
            this.btnWaitInquiry.Location = new System.Drawing.Point(8, 448);
            this.btnWaitInquiry.Name = "btnWaitInquiry";
            this.btnWaitInquiry.Size = new System.Drawing.Size(469, 66);
            this.btnWaitInquiry.TabIndex = 5;
            this.btnWaitInquiry.Text = "입고대기품목조회";
            this.btnWaitInquiry.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // S100_InboundMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(85)))), ((int)(((byte)(104)))));
            this.Controls.Add(this.btnSiteClassify);
            this.Controls.Add(this.btnShopClassify);
            this.Controls.Add(this.btnInboundSave);
            this.Controls.Add(this.btnSortInboundSave);
            this.Controls.Add(this.btnReserveList);
            this.Controls.Add(this.btnWaitInquiry);
            this.Name = "S100_InboundMenu";
            this.Size = new System.Drawing.Size(480, 528);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSiteClassify;
        private System.Windows.Forms.Button btnShopClassify;
        private System.Windows.Forms.Button btnInboundSave;
        private System.Windows.Forms.Button btnSortInboundSave;
        private System.Windows.Forms.Button btnReserveList;
        private System.Windows.Forms.Button btnWaitInquiry;
    }
}
