namespace HaimsPda.Screens
{
    partial class S310_InventoryTarget
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
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblGubunCap = new HaimsPda.Controls.VLabel();
            this.cboGubun = new System.Windows.Forms.ComboBox();
            this.lblStatCap = new HaimsPda.Controls.VLabel();
            this.cboStat = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lstTarget = new System.Windows.Forms.ListView();
            this.colSeq = new System.Windows.Forms.ColumnHeader();
            this.colCnt = new System.Windows.Forms.ColumnHeader();
            this.colStat = new System.Windows.Forms.ColumnHeader();
            this.colGubun = new System.Windows.Forms.ColumnHeader();
            this.colZone = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.btnPrev);
            this._fields.Controls.Add(this.btnNext);
            this._fields.Controls.Add(this.lblGubunCap);
            this._fields.Controls.Add(this.cboGubun);
            this._fields.Controls.Add(this.lblStatCap);
            this._fields.Controls.Add(this.cboStat);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lstTarget);
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
            this.lblWhCap.Size = new System.Drawing.Size(84, 40);
            this.lblWhCap.TabIndex = 30;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(88, 2);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(110, 40);
            this.cboWh.TabIndex = 0;
            // 
            // lblDateCap
            // 
            this.lblDateCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblDateCap.BackColor = System.Drawing.Color.White;
            this.lblDateCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDateCap.ForeColor = System.Drawing.Color.Black;
            this.lblDateCap.Location = new System.Drawing.Point(0, 48);
            this.lblDateCap.Name = "lblDateCap";
            this.lblDateCap.Size = new System.Drawing.Size(84, 40);
            this.lblDateCap.TabIndex = 31;
            this.lblDateCap.Text = "생성일자";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(88, 48);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(220, 40);
            this.dtpDate.TabIndex = 1;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPrev.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrev.Location = new System.Drawing.Point(314, 48);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(78, 40);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "◀ 이전";
            this.btnPrev.Click += new System.EventHandler(this.OnPrevDay);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnNext.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNext.Location = new System.Drawing.Point(396, 48);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 40);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "다음 ▶";
            this.btnNext.Click += new System.EventHandler(this.OnNextDay);
            // 
            // lblGubunCap
            // 
            this.lblGubunCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblGubunCap.BackColor = System.Drawing.Color.White;
            this.lblGubunCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblGubunCap.ForeColor = System.Drawing.Color.Black;
            this.lblGubunCap.Location = new System.Drawing.Point(0, 94);
            this.lblGubunCap.Name = "lblGubunCap";
            this.lblGubunCap.Size = new System.Drawing.Size(84, 40);
            this.lblGubunCap.TabIndex = 32;
            this.lblGubunCap.Text = "구분";
            // 
            // cboGubun
            // 
            this.cboGubun.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboGubun.Location = new System.Drawing.Point(88, 94);
            this.cboGubun.Name = "cboGubun";
            this.cboGubun.Size = new System.Drawing.Size(110, 40);
            this.cboGubun.TabIndex = 4;
            // 
            // lblStatCap
            // 
            this.lblStatCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblStatCap.BackColor = System.Drawing.Color.White;
            this.lblStatCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblStatCap.ForeColor = System.Drawing.Color.Black;
            this.lblStatCap.Location = new System.Drawing.Point(198, 94);
            this.lblStatCap.Name = "lblStatCap";
            this.lblStatCap.Size = new System.Drawing.Size(60, 40);
            this.lblStatCap.TabIndex = 33;
            this.lblStatCap.Text = "완료";
            // 
            // cboStat
            // 
            this.cboStat.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboStat.Location = new System.Drawing.Point(262, 94);
            this.cboStat.Name = "cboStat";
            this.cboStat.Size = new System.Drawing.Size(124, 40);
            this.cboStat.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(392, 94);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(84, 40);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(0, 140);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(150, 36);
            this.lblCntCap.TabIndex = 34;
            this.lblCntCap.Text = "ZONE구분건수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.Black;
            this.lblCnt.Location = new System.Drawing.Point(154, 140);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(100, 36);
            this.lblCnt.TabIndex = 35;
            // 
            // lstTarget
            // 
            this.lstTarget.Columns.Add(this.colSeq);
            this.lstTarget.Columns.Add(this.colCnt);
            this.lstTarget.Columns.Add(this.colStat);
            this.lstTarget.Columns.Add(this.colGubun);
            this.lstTarget.Columns.Add(this.colZone);
            this.lstTarget.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstTarget.FullRowSelect = true;
            this.lstTarget.Location = new System.Drawing.Point(4, 182);
            this.lstTarget.Name = "lstTarget";
            this.lstTarget.Size = new System.Drawing.Size(472, 298);
            this.lstTarget.TabIndex = 7;
            this.lstTarget.View = System.Windows.Forms.View.Details;
            this.lstTarget.ItemActivate += new System.EventHandler(this.OnRowActivate);
            this.lstTarget.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListKeyDown);
            // 
            // colSeq
            // 
            this.colSeq.Text = "재물조사번호";
            this.colSeq.Width = 140;
            // 
            // colCnt
            // 
            this.colCnt.Text = "건수";
            this.colCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCnt.Width = 66;
            // 
            // colStat
            // 
            this.colStat.Text = "완료";
            this.colStat.Width = 80;
            // 
            // colGubun
            // 
            this.colGubun.Text = "구분";
            this.colGubun.Width = 66;
            // 
            // colZone
            // 
            this.colZone.Text = "ZONE";
            this.colZone.Width = 100;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnInventory);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnInventory.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInventory.Location = new System.Drawing.Point(3, 4);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(236, 44);
            this.btnInventory.TabIndex = 10;
            this.btnInventory.Text = "재물조사";
            this.btnInventory.Click += new System.EventHandler(this.OnInventory);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(242, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(235, 44);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S310_InventoryTarget
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S310_InventoryTarget";
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
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private HaimsPda.Controls.VLabel lblGubunCap;
        private System.Windows.Forms.ComboBox cboGubun;
        private HaimsPda.Controls.VLabel lblStatCap;
        private System.Windows.Forms.ComboBox cboStat;
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblCntCap;
        private HaimsPda.Controls.VLabel lblCnt;
        private System.Windows.Forms.ListView lstTarget;
        private System.Windows.Forms.ColumnHeader colSeq;
        private System.Windows.Forms.ColumnHeader colCnt;
        private System.Windows.Forms.ColumnHeader colStat;
        private System.Windows.Forms.ColumnHeader colGubun;
        private System.Windows.Forms.ColumnHeader colZone;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnClear;
    }
}
