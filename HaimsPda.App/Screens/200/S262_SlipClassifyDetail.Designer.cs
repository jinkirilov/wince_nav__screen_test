namespace HaimsPda.Screens
{
    partial class S262_SlipClassifyDetail
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
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.lblWh = new HaimsPda.Controls.VLabel();
            this.lblDateCap = new HaimsPda.Controls.VLabel();
            this.lblDates = new HaimsPda.Controls.VLabel();
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.lblQty = new HaimsPda.Controls.VLabel();
            this.lstLines = new System.Windows.Forms.ListView();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colVch = new System.Windows.Forms.ColumnHeader();
            this.btnClose = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.lblWh);
            this._fields.Controls.Add(this.lblDateCap);
            this._fields.Controls.Add(this.lblDates);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.lblQty);
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
            this.lblVndCap.TabIndex = 60;
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
            this.lblVndmn.Size = new System.Drawing.Size(120, 36);
            this.lblVndmn.TabIndex = 61;
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(184, 2);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 36);
            this.lblWhCap.TabIndex = 62;
            this.lblWhCap.Text = "창고";
            // 
            // lblWh
            // 
            this.lblWh.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblWh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblWh.ForeColor = System.Drawing.Color.Black;
            this.lblWh.Location = new System.Drawing.Point(244, 2);
            this.lblWh.Name = "lblWh";
            this.lblWh.Size = new System.Drawing.Size(60, 36);
            this.lblWh.TabIndex = 63;
            // 
            // lblDateCap
            // 
            this.lblDateCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblDateCap.BackColor = System.Drawing.Color.White;
            this.lblDateCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDateCap.ForeColor = System.Drawing.Color.Black;
            this.lblDateCap.Location = new System.Drawing.Point(0, 42);
            this.lblDateCap.Name = "lblDateCap";
            this.lblDateCap.Size = new System.Drawing.Size(56, 36);
            this.lblDateCap.TabIndex = 64;
            this.lblDateCap.Text = "일자";
            // 
            // lblDates
            // 
            this.lblDates.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblDates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDates.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblDates.ForeColor = System.Drawing.Color.Black;
            this.lblDates.Location = new System.Drawing.Point(60, 42);
            this.lblDates.Name = "lblDates";
            this.lblDates.Size = new System.Drawing.Size(416, 36);
            this.lblDates.TabIndex = 65;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(0, 82);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(80, 36);
            this.lblCntCap.TabIndex = 66;
            this.lblCntCap.Text = "총건수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(84, 82);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(100, 36);
            this.lblCnt.TabIndex = 67;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(188, 82);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(80, 36);
            this.lblQtyCap.TabIndex = 68;
            this.lblQtyCap.Text = "총수량";
            // 
            // lblQty
            // 
            this.lblQty.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblQty.Location = new System.Drawing.Point(272, 82);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(100, 36);
            this.lblQty.TabIndex = 69;
            // 
            // lstLines
            // 
            this.lstLines.Columns.Add(this.colLep);
            this.lstLines.Columns.Add(this.colPtno);
            this.lstLines.Columns.Add(this.colQty);
            this.lstLines.Columns.Add(this.colVch);
            this.lstLines.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstLines.FullRowSelect = true;
            this.lstLines.Location = new System.Drawing.Point(4, 124);
            this.lstLines.Name = "lstLines";
            this.lstLines.Size = new System.Drawing.Size(472, 356);
            this.lstLines.TabIndex = 0;
            this.lstLines.View = System.Windows.Forms.View.Details;
            // 
            // colLep
            // 
            this.colLep.Text = "";
            this.colLep.Width = 36;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부번";
            this.colPtno.Width = 150;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 60;
            // 
            // colVch
            // 
            this.colVch.Text = "증표번호";
            this.colVch.Width = 150;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnClose);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnClose.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClose.Location = new System.Drawing.Point(3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(474, 44);
            this.btnClose.TabIndex = 50;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // S262_SlipClassifyDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S262_SlipClassifyDetail";
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
        private HaimsPda.Controls.VLabel lblWhCap;
        private HaimsPda.Controls.VLabel lblWh;
        private HaimsPda.Controls.VLabel lblDateCap;
        private HaimsPda.Controls.VLabel lblDates;
        private HaimsPda.Controls.VLabel lblCntCap;
        private HaimsPda.Controls.VLabel lblCnt;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private HaimsPda.Controls.VLabel lblQty;
        private System.Windows.Forms.ListView lstLines;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colVch;
        private System.Windows.Forms.Button btnClose;
    }
}
