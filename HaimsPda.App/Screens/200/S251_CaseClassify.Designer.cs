namespace HaimsPda.Screens
{
    partial class S251_CaseClassify
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
            this.lblDateCap = new HaimsPda.Controls.VLabel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lblTagCap = new HaimsPda.Controls.VLabel();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lstVnd = new System.Windows.Forms.ListView();
            this.colVndmn = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colVch = new System.Windows.Forms.ColumnHeader();
            this.colSum = new System.Windows.Forms.ColumnHeader();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblCaseCap = new HaimsPda.Controls.VLabel();
            this.txtCase = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._buttons = new System.Windows.Forms.Panel();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblDateCap);
            this._fields.Controls.Add(this.dtpDate);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lblTagCap);
            this._fields.Controls.Add(this.txtTag);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lstVnd);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Controls.Add(this.lblCaseCap);
            this._fields.Controls.Add(this.txtCase);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblDateCap
            // 
            this.lblDateCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblDateCap.BackColor = System.Drawing.Color.White;
            this.lblDateCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDateCap.ForeColor = System.Drawing.Color.Black;
            this.lblDateCap.Location = new System.Drawing.Point(0, 2);
            this.lblDateCap.Name = "lblDateCap";
            this.lblDateCap.Size = new System.Drawing.Size(90, 40);
            this.lblDateCap.TabIndex = 60;
            this.lblDateCap.Text = "피킹일자";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(94, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 40);
            this.dtpDate.TabIndex = 10;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(298, 2);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(60, 40);
            this.lblCntCap.TabIndex = 61;
            this.lblCntCap.Text = "건수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(362, 2);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(114, 40);
            this.lblCnt.TabIndex = 62;
            // 
            // lblTagCap
            // 
            this.lblTagCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblTagCap.BackColor = System.Drawing.Color.White;
            this.lblTagCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblTagCap.ForeColor = System.Drawing.Color.Black;
            this.lblTagCap.Location = new System.Drawing.Point(0, 50);
            this.lblTagCap.Name = "lblTagCap";
            this.lblTagCap.Size = new System.Drawing.Size(56, 40);
            this.lblTagCap.TabIndex = 63;
            this.lblTagCap.Text = "TAG";
            // 
            // txtTag
            // 
            this.txtTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtTag.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtTag.Location = new System.Drawing.Point(60, 46);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(416, 46);
            this.txtTag.TabIndex = 0;
            this.txtTag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTagKeyDown);
            this.txtTag.GotFocus += new System.EventHandler(this.OnTagFocus);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 100);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 40);
            this.lblPartCap.TabIndex = 80;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(60, 101);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 81;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(88, 98);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(250, 46);
            this.txtPart.TabIndex = 1;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            this.txtPart.GotFocus += new System.EventHandler(this.OnPartFocus);
            // 
            // lblClass
            // 
            this.lblClass.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblClass.Location = new System.Drawing.Point(342, 101);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(134, 40);
            this.lblClass.TabIndex = 82;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPartName.Location = new System.Drawing.Point(4, 148);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 83;
            // 
            // lstVnd
            // 
            this.lstVnd.Columns.Add(this.colVndmn);
            this.lstVnd.Columns.Add(this.colQty);
            this.lstVnd.Columns.Add(this.colVch);
            this.lstVnd.Columns.Add(this.colSum);
            this.lstVnd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstVnd.FullRowSelect = true;
            this.lstVnd.Location = new System.Drawing.Point(4, 188);
            this.lstVnd.Name = "lstVnd";
            this.lstVnd.Size = new System.Drawing.Size(472, 146);
            this.lstVnd.TabIndex = 4;
            this.lstVnd.View = System.Windows.Forms.View.Details;
            this.lstVnd.SelectedIndexChanged += new System.EventHandler(this.OnRowSelected);
            // 
            // colVndmn
            // 
            this.colVndmn.Text = "업체";
            this.colVndmn.Width = 90;
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
            // colSum
            // 
            this.colSum.Text = "QTY";
            this.colSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSum.Width = 60;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(0, 342);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(70, 46);
            this.lblQtyCap.TabIndex = 84;
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.Location = new System.Drawing.Point(74, 340);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(120, 46);
            this.txtQty.TabIndex = 2;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // lblCaseCap
            // 
            this.lblCaseCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCaseCap.BackColor = System.Drawing.Color.White;
            this.lblCaseCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCaseCap.ForeColor = System.Drawing.Color.Black;
            this.lblCaseCap.Location = new System.Drawing.Point(198, 342);
            this.lblCaseCap.Name = "lblCaseCap";
            this.lblCaseCap.Size = new System.Drawing.Size(70, 46);
            this.lblCaseCap.TabIndex = 85;
            this.lblCaseCap.Text = "CASE";
            // 
            // txtCase
            // 
            this.txtCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtCase.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtCase.Location = new System.Drawing.Point(272, 340);
            this.txtCase.Name = "txtCase";
            this.txtCase.Size = new System.Drawing.Size(204, 46);
            this.txtCase.TabIndex = 3;
            this.txtCase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnCaseKeyDown);
            this.txtCase.GotFocus += new System.EventHandler(this.OnCaseFocus);
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnDetail);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Location = new System.Drawing.Point(3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 44);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDetail.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDetail.Location = new System.Drawing.Point(162, 4);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(156, 44);
            this.btnDetail.TabIndex = 51;
            this.btnDetail.Text = "세부";
            this.btnDetail.Click += new System.EventHandler(this.OnDetail);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(321, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(156, 44);
            this.btnClear.TabIndex = 52;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S251_CaseClassify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S251_CaseClassify";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblDateCap;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private HaimsPda.Controls.VLabel lblCntCap;
        private HaimsPda.Controls.VLabel lblCnt;
        private HaimsPda.Controls.VLabel lblTagCap;
        private System.Windows.Forms.TextBox txtTag;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblPartName;
        private System.Windows.Forms.ListView lstVnd;
        private System.Windows.Forms.ColumnHeader colVndmn;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colVch;
        private System.Windows.Forms.ColumnHeader colSum;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private HaimsPda.Controls.VLabel lblCaseCap;
        private System.Windows.Forms.TextBox txtCase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnClear;
    }
}
