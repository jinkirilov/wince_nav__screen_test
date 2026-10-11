namespace HaimsPda.Screens
{
    partial class S221_SlipSelect
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
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.lblDateCap = new HaimsPda.Controls.VLabel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblCustCap = new HaimsPda.Controls.VLabel();
            this.cboCust = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCust2Cap = new HaimsPda.Controls.VLabel();
            this.cboCust2 = new System.Windows.Forms.ComboBox();
            this.lstSlips = new System.Windows.Forms.ListView();
            this.colVch = new System.Windows.Forms.ColumnHeader();
            this.colCnt = new System.Windows.Forms.ColumnHeader();
            this.colVndnm = new System.Windows.Forms.ColumnHeader();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._buttons = new System.Windows.Forms.Panel();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblDateCap);
            this._fields.Controls.Add(this.dtpDate);
            this._fields.Controls.Add(this.lblCustCap);
            this._fields.Controls.Add(this.cboCust);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblCust2Cap);
            this._fields.Controls.Add(this.cboCust2);
            this._fields.Controls.Add(this.lstSlips);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 2);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 40);
            this.lblWhCap.TabIndex = 60;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(60, 2);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(80, 40);
            this.cboWh.TabIndex = 0;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnCondChanged);
            // 
            // lblDateCap
            // 
            this.lblDateCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblDateCap.BackColor = System.Drawing.Color.White;
            this.lblDateCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDateCap.ForeColor = System.Drawing.Color.Black;
            this.lblDateCap.Location = new System.Drawing.Point(144, 2);
            this.lblDateCap.Name = "lblDateCap";
            this.lblDateCap.Size = new System.Drawing.Size(56, 40);
            this.lblDateCap.TabIndex = 61;
            this.lblDateCap.Text = "일자";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(204, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(272, 40);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.ValueChanged += new System.EventHandler(this.OnCondChanged);
            // 
            // lblCustCap
            // 
            this.lblCustCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCustCap.BackColor = System.Drawing.Color.White;
            this.lblCustCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCustCap.ForeColor = System.Drawing.Color.Black;
            this.lblCustCap.Location = new System.Drawing.Point(0, 48);
            this.lblCustCap.Name = "lblCustCap";
            this.lblCustCap.Size = new System.Drawing.Size(56, 40);
            this.lblCustCap.TabIndex = 62;
            this.lblCustCap.Text = "업체";
            // 
            // cboCust
            // 
            this.cboCust.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboCust.Location = new System.Drawing.Point(60, 48);
            this.cboCust.Name = "cboCust";
            this.cboCust.Size = new System.Drawing.Size(300, 40);
            this.cboCust.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(366, 48);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 40);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblCust2Cap
            // 
            this.lblCust2Cap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCust2Cap.BackColor = System.Drawing.Color.White;
            this.lblCust2Cap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCust2Cap.ForeColor = System.Drawing.Color.Black;
            this.lblCust2Cap.Location = new System.Drawing.Point(0, 94);
            this.lblCust2Cap.Name = "lblCust2Cap";
            this.lblCust2Cap.Size = new System.Drawing.Size(56, 40);
            this.lblCust2Cap.TabIndex = 63;
            this.lblCust2Cap.Text = "정비";
            // 
            // cboCust2
            // 
            this.cboCust2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboCust2.Location = new System.Drawing.Point(60, 94);
            this.cboCust2.Name = "cboCust2";
            this.cboCust2.Size = new System.Drawing.Size(300, 40);
            this.cboCust2.TabIndex = 3;
            // 
            // lstSlips
            // 
            this.lstSlips.CheckBoxes = true;
            this.lstSlips.Columns.Add(this.colVch);
            this.lstSlips.Columns.Add(this.colCnt);
            this.lstSlips.Columns.Add(this.colVndnm);
            this.lstSlips.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstSlips.FullRowSelect = true;
            this.lstSlips.Location = new System.Drawing.Point(4, 140);
            this.lstSlips.Name = "lstSlips";
            this.lstSlips.Size = new System.Drawing.Size(472, 340);
            this.lstSlips.TabIndex = 5;
            this.lstSlips.View = System.Windows.Forms.View.Details;
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
            // colVndnm
            // 
            this.colVndnm.Text = "거래처";
            this.colVndnm.Width = 230;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnOutput);
            this._buttons.Controls.Add(this.btnAll);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnOutput
            // 
            this.btnOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnOutput.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOutput.Location = new System.Drawing.Point(3, 4);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(156, 44);
            this.btnOutput.TabIndex = 50;
            this.btnOutput.Text = "출고";
            this.btnOutput.Click += new System.EventHandler(this.OnOutput);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAll.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAll.Location = new System.Drawing.Point(162, 4);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(156, 44);
            this.btnAll.TabIndex = 51;
            this.btnAll.Text = "ALL";
            this.btnAll.Click += new System.EventHandler(this.OnAll);
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
            // S221_SlipSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S221_SlipSelect";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private HaimsPda.Controls.VLabel lblDateCap;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private HaimsPda.Controls.VLabel lblCustCap;
        private System.Windows.Forms.ComboBox cboCust;
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblCust2Cap;
        private System.Windows.Forms.ComboBox cboCust2;
        private System.Windows.Forms.ListView lstSlips;
        private System.Windows.Forms.ColumnHeader colVch;
        private System.Windows.Forms.ColumnHeader colCnt;
        private System.Windows.Forms.ColumnHeader colVndnm;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnClear;
    }
}
