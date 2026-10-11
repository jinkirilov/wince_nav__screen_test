namespace HaimsPda.Screens
{
    partial class S204_ShopPickTarget2
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblRutCap = new HaimsPda.Controls.VLabel();
            this.cboRut = new System.Windows.Forms.ComboBox();
            this.lblAreaCap = new HaimsPda.Controls.VLabel();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.lstTarget = new System.Windows.Forms.ListView();
            this.colVndmn = new System.Windows.Forms.ColumnHeader();
            this.colCnt = new System.Windows.Forms.ColumnHeader();
            this.colVndnm = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colSeq = new System.Windows.Forms.ColumnHeader();
            this.colPre = new System.Windows.Forms.ColumnHeader();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnPicking = new System.Windows.Forms.Button();
            this.btnLocPicking = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblRutCap);
            this._fields.Controls.Add(this.cboRut);
            this._fields.Controls.Add(this.lblAreaCap);
            this._fields.Controls.Add(this.cboArea);
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
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(144, 48);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(76, 40);
            this.lblCntCap.TabIndex = 63;
            this.lblCntCap.Text = "건수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(224, 48);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(100, 40);
            this.lblCnt.TabIndex = 64;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(340, 48);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(136, 40);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblRutCap
            // 
            this.lblRutCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblRutCap.BackColor = System.Drawing.Color.White;
            this.lblRutCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblRutCap.ForeColor = System.Drawing.Color.Black;
            this.lblRutCap.Location = new System.Drawing.Point(0, 94);
            this.lblRutCap.Name = "lblRutCap";
            this.lblRutCap.Size = new System.Drawing.Size(56, 40);
            this.lblRutCap.TabIndex = 65;
            this.lblRutCap.Text = "노선";
            // 
            // cboRut
            // 
            this.cboRut.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboRut.Location = new System.Drawing.Point(60, 94);
            this.cboRut.Name = "cboRut";
            this.cboRut.Size = new System.Drawing.Size(150, 40);
            this.cboRut.TabIndex = 3;
            // 
            // lblAreaCap
            // 
            this.lblAreaCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAreaCap.BackColor = System.Drawing.Color.White;
            this.lblAreaCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAreaCap.ForeColor = System.Drawing.Color.Black;
            this.lblAreaCap.Location = new System.Drawing.Point(214, 94);
            this.lblAreaCap.Name = "lblAreaCap";
            this.lblAreaCap.Size = new System.Drawing.Size(56, 40);
            this.lblAreaCap.TabIndex = 66;
            this.lblAreaCap.Text = "지역";
            // 
            // cboArea
            // 
            this.cboArea.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboArea.Location = new System.Drawing.Point(274, 94);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(202, 40);
            this.cboArea.TabIndex = 4;
            // 
            // lstTarget
            // 
            this.lstTarget.CheckBoxes = true;
            this.lstTarget.Columns.Add(this.colVndmn);
            this.lstTarget.Columns.Add(this.colCnt);
            this.lstTarget.Columns.Add(this.colVndnm);
            this.lstTarget.Columns.Add(this.colDate);
            this.lstTarget.Columns.Add(this.colSeq);
            this.lstTarget.Columns.Add(this.colPre);
            this.lstTarget.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstTarget.FullRowSelect = true;
            this.lstTarget.Location = new System.Drawing.Point(4, 140);
            this.lstTarget.Name = "lstTarget";
            this.lstTarget.Size = new System.Drawing.Size(472, 340);
            this.lstTarget.TabIndex = 6;
            this.lstTarget.View = System.Windows.Forms.View.Details;
            // 
            // colVndmn
            // 
            this.colVndmn.Text = "업체";
            this.colVndmn.Width = 70;
            // 
            // colCnt
            // 
            this.colCnt.Text = "건수";
            this.colCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCnt.Width = 50;
            // 
            // colVndnm
            // 
            this.colVndnm.Text = "업체명";
            this.colVndnm.Width = 160;
            // 
            // colDate
            // 
            this.colDate.Text = "일자";
            this.colDate.Width = 90;
            // 
            // colSeq
            // 
            this.colSeq.Text = "SEQ";
            this.colSeq.Width = 46;
            // 
            // colPre
            // 
            this.colPre.Text = "선점";
            this.colPre.Width = 50;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnDetail);
            this._buttons.Controls.Add(this.btnPicking);
            this._buttons.Controls.Add(this.btnLocPicking);
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
            this.btnDetail.Size = new System.Drawing.Size(92, 44);
            this.btnDetail.TabIndex = 50;
            this.btnDetail.Text = "세부";
            this.btnDetail.Click += new System.EventHandler(this.OnDetail);
            // 
            // btnPicking
            // 
            this.btnPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnPicking.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPicking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPicking.Location = new System.Drawing.Point(98, 4);
            this.btnPicking.Name = "btnPicking";
            this.btnPicking.Size = new System.Drawing.Size(92, 44);
            this.btnPicking.TabIndex = 51;
            this.btnPicking.Text = "피킹";
            this.btnPicking.Click += new System.EventHandler(this.OnPicking);
            // 
            // btnLocPicking
            // 
            this.btnLocPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLocPicking.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLocPicking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLocPicking.Location = new System.Drawing.Point(193, 4);
            this.btnLocPicking.Name = "btnLocPicking";
            this.btnLocPicking.Size = new System.Drawing.Size(92, 44);
            this.btnLocPicking.TabIndex = 52;
            this.btnLocPicking.Text = "LOC피킹";
            this.btnLocPicking.Click += new System.EventHandler(this.OnLocPicking);
            // 
            // btnListPicking
            // 
            this.btnListPicking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnListPicking.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnListPicking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnListPicking.Location = new System.Drawing.Point(288, 4);
            this.btnListPicking.Name = "btnListPicking";
            this.btnListPicking.Size = new System.Drawing.Size(92, 44);
            this.btnListPicking.TabIndex = 53;
            this.btnListPicking.Text = "리스트";
            this.btnListPicking.Click += new System.EventHandler(this.OnListPicking);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(383, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 44);
            this.btnClear.TabIndex = 54;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S204_ShopPickTarget2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S204_ShopPickTarget2";
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
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblRutCap;
        private System.Windows.Forms.ComboBox cboRut;
        private HaimsPda.Controls.VLabel lblAreaCap;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.ListView lstTarget;
        private System.Windows.Forms.ColumnHeader colVndmn;
        private System.Windows.Forms.ColumnHeader colCnt;
        private System.Windows.Forms.ColumnHeader colVndnm;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colSeq;
        private System.Windows.Forms.ColumnHeader colPre;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnPicking;
        private System.Windows.Forms.Button btnLocPicking;
        private System.Windows.Forms.Button btnListPicking;
        private System.Windows.Forms.Button btnClear;
    }
}
