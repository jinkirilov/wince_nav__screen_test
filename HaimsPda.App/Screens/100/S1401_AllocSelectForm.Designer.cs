namespace HaimsPda.Screens
{
    partial class S1401_AllocSelectForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lstAlloc;
        private System.Windows.Forms.ColumnHeader colSndDt;
        private System.Windows.Forms.ColumnHeader colVchno;
        private System.Windows.Forms.ColumnHeader colReqcd;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colCasno;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.Panel _bottom;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label lblQtyCap;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnAll;
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
            this.lstAlloc = new System.Windows.Forms.ListView();
            this.colSndDt = new System.Windows.Forms.ColumnHeader();
            this.colVchno = new System.Windows.Forms.ColumnHeader();
            this.colReqcd = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colCasno = new System.Windows.Forms.ColumnHeader();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this._bottom = new System.Windows.Forms.Panel();
            this.lblSum = new System.Windows.Forms.Label();
            this.lblQtyCap = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(464, 52);
            this.lblTitle.Text = "[1401] 할당내역 선택";
            // 
            // lstAlloc
            // 
            this.lstAlloc.CheckBoxes = true;
            this.lstAlloc.Columns.Add(this.colSndDt);
            this.lstAlloc.Columns.Add(this.colVchno);
            this.lstAlloc.Columns.Add(this.colReqcd);
            this.lstAlloc.Columns.Add(this.colQty);
            this.lstAlloc.Columns.Add(this.colCasno);
            this.lstAlloc.Columns.Add(this.colLoc);
            this.lstAlloc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstAlloc.FullRowSelect = true;
            this.lstAlloc.Location = new System.Drawing.Point(8, 64);
            this.lstAlloc.Name = "lstAlloc";
            this.lstAlloc.Size = new System.Drawing.Size(464, 404);
            this.lstAlloc.TabIndex = 0;
            this.lstAlloc.View = System.Windows.Forms.View.Details;
            this.lstAlloc.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnItemCheck);
            this.lstAlloc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListKeyDown);
            // 
            // colSndDt
            // 
            this.colSndDt.Text = "발송일자";
            this.colSndDt.Width = 150;
            // 
            // colVchno
            // 
            this.colVchno.Text = "할당번호";
            this.colVchno.Width = 170;
            // 
            // colReqcd
            // 
            this.colReqcd.Text = "할당코드";
            this.colReqcd.Width = 110;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 80;
            // 
            // colCasno
            // 
            this.colCasno.Text = "CASE NO";
            this.colCasno.Width = 170;
            // 
            // colLoc
            // 
            this.colLoc.Text = "LOCATION";
            this.colLoc.Width = 150;
            // 
            // _bottom
            // 
            this._bottom.Controls.Add(this.lblSum);
            this._bottom.Controls.Add(this.lblQtyCap);
            this._bottom.Controls.Add(this.lblQty);
            this._bottom.Controls.Add(this.btnOk);
            this._bottom.Controls.Add(this.btnAll);
            this._bottom.Controls.Add(this.btnCancel);
            this._bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottom.Location = new System.Drawing.Point(0, 476);
            this._bottom.Name = "_bottom";
            this._bottom.Size = new System.Drawing.Size(480, 112);
            // 
            // lblSum
            // 
            this.lblSum.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblSum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSum.Location = new System.Drawing.Point(8, 12);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(220, 36);
            this.lblSum.Text = "선택 0 / 0건";
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblQtyCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblQtyCap.Location = new System.Drawing.Point(244, 12);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(80, 36);
            this.lblQtyCap.Text = "수량";
            // 
            // lblQty
            // 
            this.lblQty.BackColor = System.Drawing.Color.White;
            this.lblQty.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblQty.ForeColor = System.Drawing.Color.Black;
            this.lblQty.Location = new System.Drawing.Point(324, 6);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(148, 40);
            this.lblQty.Text = "0";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnOk.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.Location = new System.Drawing.Point(8, 56);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(152, 50);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "확인";
            this.btnOk.Click += new System.EventHandler(this.OnOk);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAll.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAll.Location = new System.Drawing.Point(164, 56);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(152, 50);
            this.btnAll.TabIndex = 2;
            this.btnAll.Text = "ALL";
            this.btnAll.Click += new System.EventHandler(this.OnAll);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnCancel.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Location = new System.Drawing.Point(320, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // S1401_AllocSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(64)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstAlloc);
            this.Controls.Add(this._bottom);
            this.Name = "S1401_AllocSelectForm";
            this.Text = "할당내역 선택";
            this._bottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
