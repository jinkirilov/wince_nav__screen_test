namespace HaimsPda.Screens
{
    partial class S201_PickTarget
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
            this.lblSalgbCap = new HaimsPda.Controls.VLabel();
            this.cboSalgb = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblTotalCap = new HaimsPda.Controls.VLabel();
            this.lblTotal = new HaimsPda.Controls.VLabel();
            this.lstTarget = new System.Windows.Forms.ListView();
            this.colVndmn = new System.Windows.Forms.ColumnHeader();
            this.colCnt = new System.Windows.Forms.ColumnHeader();
            this.colVndnm = new System.Windows.Forms.ColumnHeader();
            this.colPre = new System.Windows.Forms.ColumnHeader();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnPicking = new System.Windows.Forms.Button();
            this.btnListPicking = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.dtpFrom);
            this._fields.Controls.Add(this.lblTilde);
            this._fields.Controls.Add(this.dtpTo);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblSalgbCap);
            this._fields.Controls.Add(this.cboSalgb);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblTotalCap);
            this._fields.Controls.Add(this.lblTotal);
            this._fields.Controls.Add(this.lstTarget);
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
            // 
            // lblSalgbCap
            // 
            this.lblSalgbCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblSalgbCap.BackColor = System.Drawing.Color.White;
            this.lblSalgbCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblSalgbCap.ForeColor = System.Drawing.Color.Black;
            this.lblSalgbCap.Location = new System.Drawing.Point(144, 48);
            this.lblSalgbCap.Name = "lblSalgbCap";
            this.lblSalgbCap.Size = new System.Drawing.Size(56, 40);
            this.lblSalgbCap.TabIndex = 63;
            this.lblSalgbCap.Text = "판매";
            // 
            // cboSalgb
            // 
            this.cboSalgb.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboSalgb.Location = new System.Drawing.Point(204, 48);
            this.cboSalgb.Name = "cboSalgb";
            this.cboSalgb.Size = new System.Drawing.Size(130, 40);
            this.cboSalgb.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(340, 48);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(136, 40);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblTotalCap
            // 
            this.lblTotalCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblTotalCap.BackColor = System.Drawing.Color.White;
            this.lblTotalCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblTotalCap.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCap.Location = new System.Drawing.Point(0, 94);
            this.lblTotalCap.Name = "lblTotalCap";
            this.lblTotalCap.Size = new System.Drawing.Size(80, 36);
            this.lblTotalCap.TabIndex = 64;
            this.lblTotalCap.Text = "총건수";
            // 
            // lblTotal
            // 
            this.lblTotal.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTotal.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblTotal.Location = new System.Drawing.Point(84, 94);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(100, 36);
            this.lblTotal.TabIndex = 65;
            // 
            // lstTarget
            // 
            this.lstTarget.Columns.Add(this.colVndmn);
            this.lstTarget.Columns.Add(this.colCnt);
            this.lstTarget.Columns.Add(this.colVndnm);
            this.lstTarget.Columns.Add(this.colPre);
            this.lstTarget.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstTarget.FullRowSelect = true;
            this.lstTarget.Location = new System.Drawing.Point(4, 136);
            this.lstTarget.Name = "lstTarget";
            this.lstTarget.Size = new System.Drawing.Size(472, 344);
            this.lstTarget.TabIndex = 5;
            this.lstTarget.View = System.Windows.Forms.View.Details;
            this.lstTarget.ItemActivate += new System.EventHandler(this.OnDetail);
            // 
            // colVndmn
            // 
            this.colVndmn.Text = "업체";
            this.colVndmn.Width = 90;
            // 
            // colCnt
            // 
            this.colCnt.Text = "건수";
            this.colCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCnt.Width = 60;
            // 
            // colVndnm
            // 
            this.colVndnm.Text = "업체명";
            this.colVndnm.Width = 230;
            // 
            // colPre
            // 
            this.colPre.Text = "선점";
            this.colPre.Width = 60;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnDetail);
            this._buttons.Controls.Add(this.btnPicking);
            this._buttons.Controls.Add(this.btnListPicking);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDetail.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDetail.Location = new System.Drawing.Point(3, 4);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(116, 44);
            this.btnDetail.TabIndex = 50;
            this.btnDetail.Text = "세부";
            this.btnDetail.Click += new System.EventHandler(this.OnDetail);
            // 
            // btnPicking
            // 
            this.btnPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnPicking.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPicking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPicking.Location = new System.Drawing.Point(122, 4);
            this.btnPicking.Name = "btnPicking";
            this.btnPicking.Size = new System.Drawing.Size(116, 44);
            this.btnPicking.TabIndex = 51;
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
            this.btnListPicking.Size = new System.Drawing.Size(116, 44);
            this.btnListPicking.TabIndex = 52;
            this.btnListPicking.Text = "리스트피킹";
            this.btnListPicking.Click += new System.EventHandler(this.OnListPicking);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(360, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(117, 44);
            this.btnClear.TabIndex = 53;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S201_PickTarget
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S201_PickTarget";
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
        private HaimsPda.Controls.VLabel lblSalgbCap;
        private System.Windows.Forms.ComboBox cboSalgb;
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblTotalCap;
        private HaimsPda.Controls.VLabel lblTotal;
        private System.Windows.Forms.ListView lstTarget;
        private System.Windows.Forms.ColumnHeader colVndmn;
        private System.Windows.Forms.ColumnHeader colCnt;
        private System.Windows.Forms.ColumnHeader colVndnm;
        private System.Windows.Forms.ColumnHeader colPre;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnPicking;
        private System.Windows.Forms.Button btnListPicking;
        private System.Windows.Forms.Button btnClear;
    }
}
