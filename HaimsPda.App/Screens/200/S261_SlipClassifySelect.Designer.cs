namespace HaimsPda.Screens
{
    partial class S261_SlipClassifySelect
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
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTilde = new HaimsPda.Controls.VLabel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lblCustCap = new HaimsPda.Controls.VLabel();
            this.cboCust = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstSlips = new System.Windows.Forms.ListView();
            this.colVch = new System.Windows.Forms.ColumnHeader();
            this.colCnt = new System.Windows.Forms.ColumnHeader();
            this.colSalgb = new System.Windows.Forms.ColumnHeader();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this._buttons = new System.Windows.Forms.Panel();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblDateCap);
            this._fields.Controls.Add(this.dtpFrom);
            this._fields.Controls.Add(this.lblTilde);
            this._fields.Controls.Add(this.dtpTo);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lblCustCap);
            this._fields.Controls.Add(this.cboCust);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lstSlips);
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
            this.lblDateCap.Size = new System.Drawing.Size(56, 40);
            this.lblDateCap.TabIndex = 60;
            this.lblDateCap.Text = "일자";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpFrom.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(60, 2);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(186, 40);
            this.dtpFrom.TabIndex = 0;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnCondChanged);
            // 
            // lblTilde
            // 
            this.lblTilde.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblTilde.BackColor = System.Drawing.Color.White;
            this.lblTilde.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblTilde.ForeColor = System.Drawing.Color.Black;
            this.lblTilde.Location = new System.Drawing.Point(246, 2);
            this.lblTilde.Name = "lblTilde";
            this.lblTilde.Size = new System.Drawing.Size(30, 40);
            this.lblTilde.TabIndex = 61;
            this.lblTilde.Text = "~";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "yyyy-MM-dd";
            this.dtpTo.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(276, 2);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 40);
            this.dtpTo.TabIndex = 1;
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnCondChanged);
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 48);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 40);
            this.lblWhCap.TabIndex = 62;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(60, 48);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(80, 40);
            this.cboWh.TabIndex = 2;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnCondChanged);
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(144, 48);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(112, 40);
            this.lblCntCap.TabIndex = 63;
            this.lblCntCap.Text = "선택 건수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(260, 48);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(100, 40);
            this.lblCnt.TabIndex = 64;
            // 
            // lblCustCap
            // 
            this.lblCustCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCustCap.BackColor = System.Drawing.Color.White;
            this.lblCustCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCustCap.ForeColor = System.Drawing.Color.Black;
            this.lblCustCap.Location = new System.Drawing.Point(0, 94);
            this.lblCustCap.Name = "lblCustCap";
            this.lblCustCap.Size = new System.Drawing.Size(56, 40);
            this.lblCustCap.TabIndex = 65;
            this.lblCustCap.Text = "업체";
            // 
            // cboCust
            // 
            this.cboCust.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboCust.Location = new System.Drawing.Point(60, 94);
            this.cboCust.Name = "cboCust";
            this.cboCust.Size = new System.Drawing.Size(300, 40);
            this.cboCust.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(366, 94);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 40);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lstSlips
            // 
            this.lstSlips.CheckBoxes = true;
            this.lstSlips.Columns.Add(this.colVch);
            this.lstSlips.Columns.Add(this.colCnt);
            this.lstSlips.Columns.Add(this.colSalgb);
            this.lstSlips.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstSlips.FullRowSelect = true;
            this.lstSlips.Location = new System.Drawing.Point(4, 140);
            this.lstSlips.Name = "lstSlips";
            this.lstSlips.Size = new System.Drawing.Size(472, 340);
            this.lstSlips.TabIndex = 5;
            this.lstSlips.View = System.Windows.Forms.View.Details;
            this.lstSlips.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnItemCheck);
            // 
            // colVch
            // 
            this.colVch.Text = "증표번호";
            this.colVch.Width = 150;
            // 
            // colCnt
            // 
            this.colCnt.Text = "건수";
            this.colCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCnt.Width = 60;
            // 
            // colSalgb
            // 
            this.colSalgb.Text = "판매구분";
            this.colSalgb.Width = 90;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnConfirm);
            this._buttons.Controls.Add(this.btnAll);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnConfirm.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfirm.Location = new System.Drawing.Point(3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(235, 44);
            this.btnConfirm.TabIndex = 50;
            this.btnConfirm.Text = "확인";
            this.btnConfirm.Click += new System.EventHandler(this.OnConfirm);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAll.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAll.Location = new System.Drawing.Point(241, 4);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(236, 44);
            this.btnAll.TabIndex = 51;
            this.btnAll.Text = "ALL";
            this.btnAll.Click += new System.EventHandler(this.OnAll);
            // 
            // S261_SlipClassifySelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S261_SlipClassifySelect";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblDateCap;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private HaimsPda.Controls.VLabel lblTilde;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private HaimsPda.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private HaimsPda.Controls.VLabel lblCntCap;
        private HaimsPda.Controls.VLabel lblCnt;
        private HaimsPda.Controls.VLabel lblCustCap;
        private System.Windows.Forms.ComboBox cboCust;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView lstSlips;
        private System.Windows.Forms.ColumnHeader colVch;
        private System.Windows.Forms.ColumnHeader colCnt;
        private System.Windows.Forms.ColumnHeader colSalgb;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnAll;
    }
}
