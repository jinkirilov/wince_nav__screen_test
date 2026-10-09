namespace HaimsPda.Screens
{
    partial class S323_PartMoveHist
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
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.lblStdWh = new HaimsPda.Controls.VLabel();
            this.lblStdLoc = new HaimsPda.Controls.VLabel();
            this.lblPrevQtyCap = new HaimsPda.Controls.VLabel();
            this.lblPrevQty = new HaimsPda.Controls.VLabel();
            this.lblRegWhsCap = new HaimsPda.Controls.VLabel();
            this.lblRegWhs = new HaimsPda.Controls.VLabel();
            this.lblTrsCntCap = new HaimsPda.Controls.VLabel();
            this.lblTrsCnt = new HaimsPda.Controls.VLabel();
            this.lblCurQtyCap = new HaimsPda.Controls.VLabel();
            this.lblCurQty = new HaimsPda.Controls.VLabel();
            this.lblClCap = new HaimsPda.Controls.VLabel();
            this.lblCl = new HaimsPda.Controls.VLabel();
            this.lblPriceCap = new HaimsPda.Controls.VLabel();
            this.lblPrice = new HaimsPda.Controls.VLabel();
            this.lstHist = new System.Windows.Forms.ListView();
            this.colGubun = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colWh = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colVendor = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnPart = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblDateCap);
            this._fields.Controls.Add(this.dtpDate);
            this._fields.Controls.Add(this.btnPrev);
            this._fields.Controls.Add(this.btnNext);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.lblStdWh);
            this._fields.Controls.Add(this.lblStdLoc);
            this._fields.Controls.Add(this.lblPrevQtyCap);
            this._fields.Controls.Add(this.lblPrevQty);
            this._fields.Controls.Add(this.lblRegWhsCap);
            this._fields.Controls.Add(this.lblRegWhs);
            this._fields.Controls.Add(this.lblTrsCntCap);
            this._fields.Controls.Add(this.lblTrsCnt);
            this._fields.Controls.Add(this.lblCurQtyCap);
            this._fields.Controls.Add(this.lblCurQty);
            this._fields.Controls.Add(this.lblClCap);
            this._fields.Controls.Add(this.lblCl);
            this._fields.Controls.Add(this.lblPriceCap);
            this._fields.Controls.Add(this.lblPrice);
            this._fields.Controls.Add(this.lstHist);
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
            this.lblDateCap.Size = new System.Drawing.Size(84, 40);
            this.lblDateCap.TabIndex = 30;
            this.lblDateCap.Text = "조회일";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(88, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(220, 40);
            this.dtpDate.TabIndex = 2;
            this.dtpDate.ValueChanged += new System.EventHandler(this.OnDateChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPrev.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrev.Location = new System.Drawing.Point(314, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(78, 40);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.Text = "◀ 전월";
            this.btnPrev.Click += new System.EventHandler(this.OnPrevMonth);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnNext.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNext.Location = new System.Drawing.Point(396, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 40);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "익월 ▶";
            this.btnNext.Click += new System.EventHandler(this.OnNextMonth);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 48);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 40);
            this.lblPartCap.TabIndex = 31;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(60, 49);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 32;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(88, 46);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(250, 46);
            this.txtPart.TabIndex = 0;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblClass.Location = new System.Drawing.Point(342, 49);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(54, 40);
            this.lblClass.TabIndex = 33;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(400, 48);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPartName.Location = new System.Drawing.Point(4, 96);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 34;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 136);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 36);
            this.lblLocCap.TabIndex = 35;
            this.lblLocCap.Text = "LOC";
            // 
            // lblStdWh
            // 
            this.lblStdWh.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblStdWh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblStdWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblStdWh.ForeColor = System.Drawing.Color.Black;
            this.lblStdWh.Location = new System.Drawing.Point(60, 136);
            this.lblStdWh.Name = "lblStdWh";
            this.lblStdWh.Size = new System.Drawing.Size(36, 36);
            this.lblStdWh.TabIndex = 36;
            // 
            // lblStdLoc
            // 
            this.lblStdLoc.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblStdLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblStdLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblStdLoc.ForeColor = System.Drawing.Color.Black;
            this.lblStdLoc.Location = new System.Drawing.Point(100, 136);
            this.lblStdLoc.Name = "lblStdLoc";
            this.lblStdLoc.Size = new System.Drawing.Size(196, 36);
            this.lblStdLoc.TabIndex = 37;
            // 
            // lblPrevQtyCap
            // 
            this.lblPrevQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPrevQtyCap.BackColor = System.Drawing.Color.White;
            this.lblPrevQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPrevQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblPrevQtyCap.Location = new System.Drawing.Point(300, 136);
            this.lblPrevQtyCap.Name = "lblPrevQtyCap";
            this.lblPrevQtyCap.Size = new System.Drawing.Size(70, 36);
            this.lblPrevQtyCap.TabIndex = 38;
            this.lblPrevQtyCap.Text = "전월";
            // 
            // lblPrevQty
            // 
            this.lblPrevQty.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPrevQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblPrevQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrevQty.ForeColor = System.Drawing.Color.Black;
            this.lblPrevQty.Location = new System.Drawing.Point(374, 136);
            this.lblPrevQty.Name = "lblPrevQty";
            this.lblPrevQty.Size = new System.Drawing.Size(102, 36);
            this.lblPrevQty.TabIndex = 39;
            // 
            // lblRegWhsCap
            // 
            this.lblRegWhsCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblRegWhsCap.BackColor = System.Drawing.Color.White;
            this.lblRegWhsCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblRegWhsCap.ForeColor = System.Drawing.Color.Black;
            this.lblRegWhsCap.Location = new System.Drawing.Point(0, 176);
            this.lblRegWhsCap.Name = "lblRegWhsCap";
            this.lblRegWhsCap.Size = new System.Drawing.Size(92, 36);
            this.lblRegWhsCap.TabIndex = 40;
            this.lblRegWhsCap.Text = "등록창고";
            // 
            // lblRegWhs
            // 
            this.lblRegWhs.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblRegWhs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblRegWhs.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegWhs.ForeColor = System.Drawing.Color.Black;
            this.lblRegWhs.Location = new System.Drawing.Point(96, 176);
            this.lblRegWhs.Name = "lblRegWhs";
            this.lblRegWhs.Size = new System.Drawing.Size(130, 36);
            this.lblRegWhs.TabIndex = 41;
            // 
            // lblTrsCntCap
            // 
            this.lblTrsCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblTrsCntCap.BackColor = System.Drawing.Color.White;
            this.lblTrsCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblTrsCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblTrsCntCap.Location = new System.Drawing.Point(230, 176);
            this.lblTrsCntCap.Name = "lblTrsCntCap";
            this.lblTrsCntCap.Size = new System.Drawing.Size(140, 36);
            this.lblTrsCntCap.TabIndex = 42;
            this.lblTrsCntCap.Text = "수불수량";
            // 
            // lblTrsCnt
            // 
            this.lblTrsCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblTrsCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTrsCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrsCnt.ForeColor = System.Drawing.Color.Black;
            this.lblTrsCnt.Location = new System.Drawing.Point(374, 176);
            this.lblTrsCnt.Name = "lblTrsCnt";
            this.lblTrsCnt.Size = new System.Drawing.Size(102, 36);
            this.lblTrsCnt.TabIndex = 43;
            // 
            // lblCurQtyCap
            // 
            this.lblCurQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCurQtyCap.BackColor = System.Drawing.Color.White;
            this.lblCurQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCurQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblCurQtyCap.Location = new System.Drawing.Point(0, 216);
            this.lblCurQtyCap.Name = "lblCurQtyCap";
            this.lblCurQtyCap.Size = new System.Drawing.Size(70, 36);
            this.lblCurQtyCap.TabIndex = 44;
            this.lblCurQtyCap.Text = "현재고";
            // 
            // lblCurQty
            // 
            this.lblCurQty.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCurQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCurQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCurQty.Location = new System.Drawing.Point(74, 216);
            this.lblCurQty.Name = "lblCurQty";
            this.lblCurQty.Size = new System.Drawing.Size(92, 36);
            this.lblCurQty.TabIndex = 45;
            // 
            // lblClCap
            // 
            this.lblClCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblClCap.BackColor = System.Drawing.Color.White;
            this.lblClCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblClCap.ForeColor = System.Drawing.Color.Black;
            this.lblClCap.Location = new System.Drawing.Point(170, 216);
            this.lblClCap.Name = "lblClCap";
            this.lblClCap.Size = new System.Drawing.Size(40, 36);
            this.lblClCap.TabIndex = 46;
            this.lblClCap.Text = "CL";
            // 
            // lblCl
            // 
            this.lblCl.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblCl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCl.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCl.ForeColor = System.Drawing.Color.Black;
            this.lblCl.Location = new System.Drawing.Point(214, 216);
            this.lblCl.Name = "lblCl";
            this.lblCl.Size = new System.Drawing.Size(60, 36);
            this.lblCl.TabIndex = 47;
            // 
            // lblPriceCap
            // 
            this.lblPriceCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPriceCap.BackColor = System.Drawing.Color.White;
            this.lblPriceCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPriceCap.ForeColor = System.Drawing.Color.Black;
            this.lblPriceCap.Location = new System.Drawing.Point(278, 216);
            this.lblPriceCap.Name = "lblPriceCap";
            this.lblPriceCap.Size = new System.Drawing.Size(52, 36);
            this.lblPriceCap.TabIndex = 48;
            this.lblPriceCap.Text = "단가";
            // 
            // lblPrice
            // 
            this.lblPrice.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblPrice.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.Black;
            this.lblPrice.Location = new System.Drawing.Point(334, 216);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(142, 36);
            this.lblPrice.TabIndex = 49;
            // 
            // lstHist
            // 
            this.lstHist.Columns.Add(this.colGubun);
            this.lstHist.Columns.Add(this.colDate);
            this.lstHist.Columns.Add(this.colWh);
            this.lstHist.Columns.Add(this.colQty);
            this.lstHist.Columns.Add(this.colVendor);
            this.lstHist.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstHist.FullRowSelect = true;
            this.lstHist.Location = new System.Drawing.Point(4, 258);
            this.lstHist.Name = "lstHist";
            this.lstHist.Size = new System.Drawing.Size(472, 222);
            this.lstHist.TabIndex = 5;
            this.lstHist.View = System.Windows.Forms.View.Details;
            // 
            // colGubun
            // 
            this.colGubun.Text = "구분";
            this.colGubun.Width = 80;
            // 
            // colDate
            // 
            this.colDate.Text = "날짜";
            this.colDate.Width = 90;
            // 
            // colWh
            // 
            this.colWh.Text = "창고";
            this.colWh.Width = 56;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 70;
            // 
            // colVendor
            // 
            this.colVendor.Text = "거래처";
            this.colVendor.Width = 200;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnPart);
            this._buttons.Controls.Add(this.btnLoc);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnPart
            // 
            this.btnPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPart.Location = new System.Drawing.Point(3, 4);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new System.Drawing.Size(156, 44);
            this.btnPart.TabIndex = 10;
            this.btnPart.Text = "파트";
            this.btnPart.Click += new System.EventHandler(this.OnPart);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(162, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(156, 44);
            this.btnLoc.TabIndex = 11;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(321, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(156, 44);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S323_PartMoveHist
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S323_PartMoveHist";
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
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblLocCap;
        private HaimsPda.Controls.VLabel lblStdWh;
        private HaimsPda.Controls.VLabel lblStdLoc;
        private HaimsPda.Controls.VLabel lblPrevQtyCap;
        private HaimsPda.Controls.VLabel lblPrevQty;
        private HaimsPda.Controls.VLabel lblRegWhsCap;
        private HaimsPda.Controls.VLabel lblRegWhs;
        private HaimsPda.Controls.VLabel lblTrsCntCap;
        private HaimsPda.Controls.VLabel lblTrsCnt;
        private HaimsPda.Controls.VLabel lblCurQtyCap;
        private HaimsPda.Controls.VLabel lblCurQty;
        private HaimsPda.Controls.VLabel lblClCap;
        private HaimsPda.Controls.VLabel lblCl;
        private HaimsPda.Controls.VLabel lblPriceCap;
        private HaimsPda.Controls.VLabel lblPrice;
        private System.Windows.Forms.ListView lstHist;
        private System.Windows.Forms.ColumnHeader colGubun;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colWh;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colVendor;
        private System.Windows.Forms.Button btnPart;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnClear;
    }
}
