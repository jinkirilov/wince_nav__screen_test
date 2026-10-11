namespace HaimsPda.Screens
{
    partial class S210_Picking
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
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.lblWh = new HaimsPda.Controls.VLabel();
            this.lblSalgbCap = new HaimsPda.Controls.VLabel();
            this.lblSalgb = new HaimsPda.Controls.VLabel();
            this.lblWorkCap = new HaimsPda.Controls.VLabel();
            this.lblWork = new HaimsPda.Controls.VLabel();
            this.lblGrtCap = new HaimsPda.Controls.VLabel();
            this.lblGrtno = new HaimsPda.Controls.VLabel();
            this.lblTagCap = new HaimsPda.Controls.VLabel();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.lblSalCntCap = new HaimsPda.Controls.VLabel();
            this.lblSalCnt = new HaimsPda.Controls.VLabel();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblAvlCap = new HaimsPda.Controls.VLabel();
            this.lblAvl = new HaimsPda.Controls.VLabel();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lstLines = new System.Windows.Forms.ListView();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colAvl = new System.Windows.Forms.ColumnHeader();
            this.colVch = new System.Windows.Forms.ColumnHeader();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.lblWh);
            this._fields.Controls.Add(this.lblSalgbCap);
            this._fields.Controls.Add(this.lblSalgb);
            this._fields.Controls.Add(this.lblWorkCap);
            this._fields.Controls.Add(this.lblWork);
            this._fields.Controls.Add(this.lblGrtCap);
            this._fields.Controls.Add(this.lblGrtno);
            this._fields.Controls.Add(this.lblTagCap);
            this._fields.Controls.Add(this.txtTag);
            this._fields.Controls.Add(this.lblSalCntCap);
            this._fields.Controls.Add(this.lblSalCnt);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblAvlCap);
            this._fields.Controls.Add(this.lblAvl);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
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
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 42);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 36);
            this.lblWhCap.TabIndex = 60;
            this.lblWhCap.Text = "창고";
            // 
            // lblWh
            // 
            this.lblWh.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblWh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblWh.ForeColor = System.Drawing.Color.Black;
            this.lblWh.Location = new System.Drawing.Point(60, 42);
            this.lblWh.Name = "lblWh";
            this.lblWh.Size = new System.Drawing.Size(50, 36);
            this.lblWh.TabIndex = 61;
            // 
            // lblSalgbCap
            // 
            this.lblSalgbCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblSalgbCap.BackColor = System.Drawing.Color.White;
            this.lblSalgbCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblSalgbCap.ForeColor = System.Drawing.Color.Black;
            this.lblSalgbCap.Location = new System.Drawing.Point(114, 42);
            this.lblSalgbCap.Name = "lblSalgbCap";
            this.lblSalgbCap.Size = new System.Drawing.Size(56, 36);
            this.lblSalgbCap.TabIndex = 62;
            this.lblSalgbCap.Text = "판매";
            // 
            // lblSalgb
            // 
            this.lblSalgb.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblSalgb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblSalgb.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalgb.ForeColor = System.Drawing.Color.Black;
            this.lblSalgb.Location = new System.Drawing.Point(174, 42);
            this.lblSalgb.Name = "lblSalgb";
            this.lblSalgb.Size = new System.Drawing.Size(90, 36);
            this.lblSalgb.TabIndex = 63;
            // 
            // lblWorkCap
            // 
            this.lblWorkCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWorkCap.BackColor = System.Drawing.Color.White;
            this.lblWorkCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWorkCap.ForeColor = System.Drawing.Color.Black;
            this.lblWorkCap.Location = new System.Drawing.Point(268, 42);
            this.lblWorkCap.Name = "lblWorkCap";
            this.lblWorkCap.Size = new System.Drawing.Size(80, 36);
            this.lblWorkCap.TabIndex = 64;
            this.lblWorkCap.Text = "총건수";
            // 
            // lblWork
            // 
            this.lblWork.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblWork.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblWork.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblWork.Location = new System.Drawing.Point(352, 42);
            this.lblWork.Name = "lblWork";
            this.lblWork.Size = new System.Drawing.Size(124, 36);
            this.lblWork.TabIndex = 65;
            // 
            // lblGrtCap
            // 
            this.lblGrtCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblGrtCap.BackColor = System.Drawing.Color.White;
            this.lblGrtCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblGrtCap.ForeColor = System.Drawing.Color.Black;
            this.lblGrtCap.Location = new System.Drawing.Point(0, 82);
            this.lblGrtCap.Name = "lblGrtCap";
            this.lblGrtCap.Size = new System.Drawing.Size(56, 36);
            this.lblGrtCap.TabIndex = 66;
            this.lblGrtCap.Text = "차량";
            // 
            // lblGrtno
            // 
            this.lblGrtno.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblGrtno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblGrtno.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblGrtno.ForeColor = System.Drawing.Color.Black;
            this.lblGrtno.Location = new System.Drawing.Point(60, 82);
            this.lblGrtno.Name = "lblGrtno";
            this.lblGrtno.Size = new System.Drawing.Size(140, 36);
            this.lblGrtno.TabIndex = 67;
            // 
            // lblTagCap
            // 
            this.lblTagCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblTagCap.BackColor = System.Drawing.Color.White;
            this.lblTagCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblTagCap.ForeColor = System.Drawing.Color.Black;
            this.lblTagCap.Location = new System.Drawing.Point(0, 82);
            this.lblTagCap.Name = "lblTagCap";
            this.lblTagCap.Size = new System.Drawing.Size(56, 36);
            this.lblTagCap.TabIndex = 74;
            this.lblTagCap.Text = "TAG";
            // 
            // txtTag
            // 
            this.txtTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtTag.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.txtTag.Location = new System.Drawing.Point(60, 80);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(140, 40);
            this.txtTag.TabIndex = 5;
            this.txtTag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTagKeyDown);
            this.txtTag.GotFocus += new System.EventHandler(this.OnTagFocus);
            // 
            // lblSalCntCap
            // 
            this.lblSalCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblSalCntCap.BackColor = System.Drawing.Color.White;
            this.lblSalCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblSalCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblSalCntCap.Location = new System.Drawing.Point(204, 82);
            this.lblSalCntCap.Name = "lblSalCntCap";
            this.lblSalCntCap.Size = new System.Drawing.Size(100, 36);
            this.lblSalCntCap.TabIndex = 68;
            this.lblSalCntCap.Text = "판매건수";
            // 
            // lblSalCnt
            // 
            this.lblSalCnt.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblSalCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblSalCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalCnt.ForeColor = System.Drawing.Color.Black;
            this.lblSalCnt.Location = new System.Drawing.Point(308, 82);
            this.lblSalCnt.Name = "lblSalCnt";
            this.lblSalCnt.Size = new System.Drawing.Size(168, 36);
            this.lblSalCnt.TabIndex = 69;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 126);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 40);
            this.lblLocCap.TabIndex = 73;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(60, 122);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(416, 46);
            this.txtLoc.TabIndex = 0;
            this.txtLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            this.txtLoc.GotFocus += new System.EventHandler(this.OnLocFocus);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 176);
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
            this.lblPrefix.Location = new System.Drawing.Point(60, 177);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 81;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(88, 174);
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
            this.lblClass.Location = new System.Drawing.Point(342, 177);
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
            this.lblPartName.Location = new System.Drawing.Point(4, 224);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 83;
            // 
            // lblAvlCap
            // 
            this.lblAvlCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAvlCap.BackColor = System.Drawing.Color.White;
            this.lblAvlCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAvlCap.ForeColor = System.Drawing.Color.Black;
            this.lblAvlCap.Location = new System.Drawing.Point(0, 266);
            this.lblAvlCap.Name = "lblAvlCap";
            this.lblAvlCap.Size = new System.Drawing.Size(90, 46);
            this.lblAvlCap.TabIndex = 84;
            this.lblAvlCap.Text = "LOC수량";
            // 
            // lblAvl
            // 
            this.lblAvl.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAvl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblAvl.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblAvl.ForeColor = System.Drawing.Color.Black;
            this.lblAvl.Location = new System.Drawing.Point(94, 266);
            this.lblAvl.Name = "lblAvl";
            this.lblAvl.Size = new System.Drawing.Size(140, 46);
            this.lblAvl.TabIndex = 85;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(238, 266);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(70, 46);
            this.lblQtyCap.TabIndex = 86;
            this.lblQtyCap.Text = "QTY";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.Location = new System.Drawing.Point(312, 264);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(164, 46);
            this.txtQty.TabIndex = 2;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // lstLines
            // 
            this.lstLines.Columns.Add(this.colLoc);
            this.lstLines.Columns.Add(this.colLep);
            this.lstLines.Columns.Add(this.colPtno);
            this.lstLines.Columns.Add(this.colQty);
            this.lstLines.Columns.Add(this.colAvl);
            this.lstLines.Columns.Add(this.colVch);
            this.lstLines.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstLines.FullRowSelect = true;
            this.lstLines.Location = new System.Drawing.Point(4, 318);
            this.lstLines.Name = "lstLines";
            this.lstLines.Size = new System.Drawing.Size(472, 162);
            this.lstLines.TabIndex = 3;
            this.lstLines.View = System.Windows.Forms.View.Details;
            // 
            // colLoc
            // 
            this.colLoc.Text = "LOCATION";
            this.colLoc.Width = 126;
            // 
            // colLep
            // 
            this.colLep.Text = "";
            this.colLep.Width = 30;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부번";
            this.colPtno.Width = 120;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 50;
            // 
            // colAvl
            // 
            this.colAvl.Text = "LOC수량";
            this.colAvl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAvl.Width = 66;
            // 
            // colVch
            // 
            this.colVch.Text = "증표번호";
            this.colVch.Width = 110;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnStock);
            this._buttons.Controls.Add(this.btnSkip);
            this._buttons.Controls.Add(this.btnReturn);
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
            this.btnSave.Size = new System.Drawing.Size(116, 44);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Location = new System.Drawing.Point(122, 4);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(116, 44);
            this.btnStock.TabIndex = 51;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSkip.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSkip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSkip.Location = new System.Drawing.Point(241, 4);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(116, 44);
            this.btnSkip.TabIndex = 52;
            this.btnSkip.Text = "SKIP";
            this.btnSkip.Click += new System.EventHandler(this.OnSkip);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnReturn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReturn.Location = new System.Drawing.Point(360, 4);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(117, 44);
            this.btnReturn.TabIndex = 53;
            this.btnReturn.Text = "반품";
            this.btnReturn.Click += new System.EventHandler(this.OnReturnClick);
            // 
            // S210_Picking
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S210_Picking";
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
        private HaimsPda.Controls.VLabel lblWhCap;
        private HaimsPda.Controls.VLabel lblWh;
        private HaimsPda.Controls.VLabel lblSalgbCap;
        private HaimsPda.Controls.VLabel lblSalgb;
        private HaimsPda.Controls.VLabel lblWorkCap;
        private HaimsPda.Controls.VLabel lblWork;
        private HaimsPda.Controls.VLabel lblGrtCap;
        private HaimsPda.Controls.VLabel lblGrtno;
        private HaimsPda.Controls.VLabel lblTagCap;
        private System.Windows.Forms.TextBox txtTag;
        private HaimsPda.Controls.VLabel lblSalCntCap;
        private HaimsPda.Controls.VLabel lblSalCnt;
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblAvlCap;
        private HaimsPda.Controls.VLabel lblAvl;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ListView lstLines;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colAvl;
        private System.Windows.Forms.ColumnHeader colVch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnReturn;
    }
}
