namespace HaimsPda.Screens
{
    partial class S202_PickDetail
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
            this._fields = new System.Windows.Forms.Panel();
            this.lblVndCap = new HaimsPda.Controls.VLabel();
            this.lblVndmn = new HaimsPda.Controls.VLabel();
            this.lblVndnm = new HaimsPda.Controls.VLabel();
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lstLines = new System.Windows.Forms.ListView();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colGrade = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colVch = new System.Windows.Forms.ColumnHeader();
            this.colGrt = new System.Windows.Forms.ColumnHeader();
            this.btnPicking = new System.Windows.Forms.Button();
            this.btnListPicking = new System.Windows.Forms.Button();
            this._buttons = new System.Windows.Forms.Panel();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblVndCap);
            this._fields.Controls.Add(this.lblVndmn);
            this._fields.Controls.Add(this.lblVndnm);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lstLines);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblVndCap
            // 
            this.lblVndCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblVndCap.BackColor = System.Drawing.Color.White;
            this.lblVndCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblVndCap.ForeColor = System.Drawing.Color.Black;
            this.lblVndCap.Location = new System.Drawing.Point(0, 2);
            this.lblVndCap.Name = "lblVndCap";
            this.lblVndCap.Size = new System.Drawing.Size(56, 36);
            this.lblVndCap.TabIndex = 70;
            this.lblVndCap.Text = "업체";
            // 
            // lblVndmn
            // 
            this.lblVndmn.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblVndmn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblVndmn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblVndmn.ForeColor = System.Drawing.Color.Black;
            this.lblVndmn.Location = new System.Drawing.Point(60, 2);
            this.lblVndmn.Name = "lblVndmn";
            this.lblVndmn.Size = new System.Drawing.Size(80, 36);
            this.lblVndmn.TabIndex = 71;
            // 
            // lblVndnm
            // 
            this.lblVndnm.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblVndnm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblVndnm.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblVndnm.ForeColor = System.Drawing.Color.Black;
            this.lblVndnm.Location = new System.Drawing.Point(144, 2);
            this.lblVndnm.Name = "lblVndnm";
            this.lblVndnm.Size = new System.Drawing.Size(332, 36);
            this.lblVndnm.TabIndex = 72;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(0, 42);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(80, 36);
            this.lblCntCap.TabIndex = 64;
            this.lblCntCap.Text = "총건수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(84, 42);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(100, 36);
            this.lblCnt.TabIndex = 65;
            // 
            // lstLines
            // 
            this.lstLines.Columns.Add(this.colLoc);
            this.lstLines.Columns.Add(this.colLep);
            this.lstLines.Columns.Add(this.colPtno);
            this.lstLines.Columns.Add(this.colGrade);
            this.lstLines.Columns.Add(this.colQty);
            this.lstLines.Columns.Add(this.colVch);
            this.lstLines.Columns.Add(this.colGrt);
            this.lstLines.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstLines.FullRowSelect = true;
            this.lstLines.Location = new System.Drawing.Point(4, 84);
            this.lstLines.Name = "lstLines";
            this.lstLines.Size = new System.Drawing.Size(472, 396);
            this.lstLines.TabIndex = 0;
            this.lstLines.View = System.Windows.Forms.View.Details;
            // 
            // colLoc
            // 
            this.colLoc.Text = "LOCATION";
            this.colLoc.Width = 130;
            // 
            // colLep
            // 
            this.colLep.Text = "";
            this.colLep.Width = 36;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 120;
            // 
            // colGrade
            // 
            this.colGrade.Text = "등급";
            this.colGrade.Width = 50;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 50;
            // 
            // colVch
            // 
            this.colVch.Text = "판매번호";
            this.colVch.Width = 120;
            // 
            // colGrt
            // 
            this.colGrt.Text = "차량번호";
            this.colGrt.Width = 80;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnPicking);
            this._buttons.Controls.Add(this.btnListPicking);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnPicking
            // 
            this.btnPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnPicking.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPicking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPicking.Location = new System.Drawing.Point(3, 4);
            this.btnPicking.Name = "btnPicking";
            this.btnPicking.Size = new System.Drawing.Size(235, 44);
            this.btnPicking.TabIndex = 50;
            this.btnPicking.Text = "피킹";
            this.btnPicking.Click += new System.EventHandler(this.OnPicking);
            // 
            // btnListPicking
            // 
            this.btnListPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnListPicking.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnListPicking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnListPicking.Location = new System.Drawing.Point(241, 4);
            this.btnListPicking.Name = "btnListPicking";
            this.btnListPicking.Size = new System.Drawing.Size(236, 44);
            this.btnListPicking.TabIndex = 51;
            this.btnListPicking.Text = "리스트피킹";
            this.btnListPicking.Click += new System.EventHandler(this.OnListPicking);
            // 
            // S202_PickDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S202_PickDetail";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblVndCap;
        private HaimsPda.Controls.VLabel lblVndmn;
        private HaimsPda.Controls.VLabel lblVndnm;
        private HaimsPda.Controls.VLabel lblCntCap;
        private HaimsPda.Controls.VLabel lblCnt;
        private System.Windows.Forms.ListView lstLines;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colGrade;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colVch;
        private System.Windows.Forms.ColumnHeader colGrt;
        private System.Windows.Forms.Button btnPicking;
        private System.Windows.Forms.Button btnListPicking;
    }
}
