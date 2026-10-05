namespace MobisHaims.Screens
{
    partial class S400_LocMenu
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
            this.btnLocMove = new System.Windows.Forms.Button();
            this.btnLocRegister = new System.Windows.Forms.Button();
            this.btnLocSort = new System.Windows.Forms.Button();
            this.btnTransferOut = new System.Windows.Forms.Button();
            this.btnTransferIn = new System.Windows.Forms.Button();
            this.btnMigrateOut = new System.Windows.Forms.Button();
            this.btnMigrateIn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLocMove
            // 
            this.btnLocMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLocMove.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnLocMove.ForeColor = System.Drawing.Color.White;
            this.btnLocMove.Location = new System.Drawing.Point(4, 8);
            this.btnLocMove.Name = "btnLocMove";
            this.btnLocMove.Size = new System.Drawing.Size(232, 80);
            this.btnLocMove.TabIndex = 0;
            this.btnLocMove.Text = "LOC재고이동";
            this.btnLocMove.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnLocRegister
            // 
            this.btnLocRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLocRegister.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnLocRegister.ForeColor = System.Drawing.Color.White;
            this.btnLocRegister.Location = new System.Drawing.Point(4, 109);
            this.btnLocRegister.Name = "btnLocRegister";
            this.btnLocRegister.Size = new System.Drawing.Size(232, 80);
            this.btnLocRegister.TabIndex = 1;
            this.btnLocRegister.Text = "LOC등록";
            this.btnLocRegister.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnLocSort
            // 
            this.btnLocSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLocSort.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnLocSort.ForeColor = System.Drawing.Color.White;
            this.btnLocSort.Location = new System.Drawing.Point(245, 109);
            this.btnLocSort.Name = "btnLocSort";
            this.btnLocSort.Size = new System.Drawing.Size(232, 80);
            this.btnLocSort.TabIndex = 2;
            this.btnLocSort.Text = "LOC정렬";
            this.btnLocSort.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnTransferOut
            // 
            this.btnTransferOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnTransferOut.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnTransferOut.ForeColor = System.Drawing.Color.White;
            this.btnTransferOut.Location = new System.Drawing.Point(4, 210);
            this.btnTransferOut.Name = "btnTransferOut";
            this.btnTransferOut.Size = new System.Drawing.Size(232, 80);
            this.btnTransferOut.TabIndex = 3;
            this.btnTransferOut.Text = "창고이전(출고)";
            this.btnTransferOut.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnTransferIn
            // 
            this.btnTransferIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnTransferIn.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnTransferIn.ForeColor = System.Drawing.Color.White;
            this.btnTransferIn.Location = new System.Drawing.Point(245, 210);
            this.btnTransferIn.Name = "btnTransferIn";
            this.btnTransferIn.Size = new System.Drawing.Size(232, 80);
            this.btnTransferIn.TabIndex = 4;
            this.btnTransferIn.Text = "창고이전(입고)";
            this.btnTransferIn.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnMigrateOut
            // 
            this.btnMigrateOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnMigrateOut.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnMigrateOut.ForeColor = System.Drawing.Color.White;
            this.btnMigrateOut.Location = new System.Drawing.Point(4, 311);
            this.btnMigrateOut.Name = "btnMigrateOut";
            this.btnMigrateOut.Size = new System.Drawing.Size(232, 80);
            this.btnMigrateOut.TabIndex = 5;
            this.btnMigrateOut.Text = "창고이관(출고)";
            this.btnMigrateOut.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // btnMigrateIn
            // 
            this.btnMigrateIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnMigrateIn.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnMigrateIn.ForeColor = System.Drawing.Color.White;
            this.btnMigrateIn.Location = new System.Drawing.Point(245, 311);
            this.btnMigrateIn.Name = "btnMigrateIn";
            this.btnMigrateIn.Size = new System.Drawing.Size(232, 80);
            this.btnMigrateIn.TabIndex = 6;
            this.btnMigrateIn.Text = "창고이관(입고)";
            this.btnMigrateIn.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // S400_LocMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.btnLocMove);
            this.Controls.Add(this.btnLocRegister);
            this.Controls.Add(this.btnLocSort);
            this.Controls.Add(this.btnTransferOut);
            this.Controls.Add(this.btnTransferIn);
            this.Controls.Add(this.btnMigrateOut);
            this.Controls.Add(this.btnMigrateIn);
            this.Name = "S400_LocMenu";
            this.Size = new System.Drawing.Size(480, 484);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLocMove;
        private System.Windows.Forms.Button btnLocRegister;
        private System.Windows.Forms.Button btnLocSort;
        private System.Windows.Forms.Button btnTransferOut;
        private System.Windows.Forms.Button btnTransferIn;
        private System.Windows.Forms.Button btnMigrateOut;
        private System.Windows.Forms.Button btnMigrateIn;
    }
}
