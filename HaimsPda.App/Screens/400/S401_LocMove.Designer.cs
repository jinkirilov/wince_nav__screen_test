namespace HaimsPda.Screens
{
    partial class S401_LocMove
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
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLocFr = new System.Windows.Forms.TextBox();
            this.btnMode = new System.Windows.Forms.Button();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblAvlCap = new HaimsPda.Controls.VLabel();
            this.txtAvlFr = new System.Windows.Forms.TextBox();
            this.lblOutCap = new HaimsPda.Controls.VLabel();
            this.txtOutQty = new System.Windows.Forms.TextBox();
            this.lstList = new System.Windows.Forms.ListView();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colAvl = new System.Windows.Forms.ColumnHeader();
            this.colSal = new System.Windows.Forms.ColumnHeader();
            this.colCls = new System.Windows.Forms.ColumnHeader();
            this.lblPartToCap = new HaimsPda.Controls.VLabel();
            this.lblPrefixTo = new HaimsPda.Controls.VLabel();
            this.txtPartTo = new System.Windows.Forms.TextBox();
            this.lblClassTo = new HaimsPda.Controls.VLabel();
            this.lblLocToCap = new HaimsPda.Controls.VLabel();
            this.txtLocTo = new System.Windows.Forms.TextBox();
            this.lblQtyToCap = new HaimsPda.Controls.VLabel();
            this.txtQtyTo = new System.Windows.Forms.TextBox();
            this.btnPart = new System.Windows.Forms.Button();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnList = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.txtCnt);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLocFr);
            this._fields.Controls.Add(this.btnMode);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblAvlCap);
            this._fields.Controls.Add(this.txtAvlFr);
            this._fields.Controls.Add(this.lblOutCap);
            this._fields.Controls.Add(this.txtOutQty);
            this._fields.Controls.Add(this.lstList);
            this._fields.Controls.Add(this.lblPartToCap);
            this._fields.Controls.Add(this.lblPrefixTo);
            this._fields.Controls.Add(this.txtPartTo);
            this._fields.Controls.Add(this.lblClassTo);
            this._fields.Controls.Add(this.lblLocToCap);
            this._fields.Controls.Add(this.txtLocTo);
            this._fields.Controls.Add(this.lblQtyToCap);
            this._fields.Controls.Add(this.txtQtyTo);
            this._fields.Controls.Add(this.btnPart);
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
            this.lblWhCap.TabIndex = 50;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.cboWh.Location = new System.Drawing.Point(62, 2);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(120, 46);
            this.cboWh.TabIndex = 9;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnWhChanged);
            this.cboWh.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(230, 2);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(96, 40);
            this.lblCntCap.TabIndex = 52;
            this.lblCntCap.Text = "건수";
            // 
            // txtCnt
            // 
            this.txtCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtCnt.Location = new System.Drawing.Point(334, 2);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.ReadOnly = true;
            this.txtCnt.Size = new System.Drawing.Size(142, 40);
            this.txtCnt.TabIndex = 20;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 48);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 46);
            this.lblLocCap.TabIndex = 54;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLocFr
            // 
            this.txtLocFr.BackColor = System.Drawing.Color.White;
            this.txtLocFr.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocFr.Location = new System.Drawing.Point(62, 49);
            this.txtLocFr.Name = "txtLocFr";
            this.txtLocFr.Size = new System.Drawing.Size(300, 46);
            this.txtLocFr.TabIndex = 0;
            this.txtLocFr.GotFocus += new System.EventHandler(this.OnLocFrFocus);
            this.txtLocFr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocFrKeyDown);
            // 
            // btnMode
            // 
            this.btnMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnMode.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMode.Location = new System.Drawing.Point(368, 49);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(108, 44);
            this.btnMode.TabIndex = 8;
            this.btnMode.Text = "싱글";
            this.btnMode.Click += new System.EventHandler(this.OnMode);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 98);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 57;
            this.lblPartCap.Text = "부번";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(61, 99);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(30, 42);
            this.lblPrefix.TabIndex = 58;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(93, 99);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(269, 46);
            this.txtPart.TabIndex = 1;
            this.txtPart.GotFocus += new System.EventHandler(this.OnPartFocus);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.Black;
            this.lblClass.Location = new System.Drawing.Point(368, 99);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(108, 42);
            this.lblClass.TabIndex = 60;
            // 
            // lblAvlCap
            // 
            this.lblAvlCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAvlCap.BackColor = System.Drawing.Color.White;
            this.lblAvlCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAvlCap.ForeColor = System.Drawing.Color.Black;
            this.lblAvlCap.Location = new System.Drawing.Point(0, 148);
            this.lblAvlCap.Name = "lblAvlCap";
            this.lblAvlCap.Size = new System.Drawing.Size(96, 40);
            this.lblAvlCap.TabIndex = 61;
            this.lblAvlCap.Text = "가능수량";
            // 
            // txtAvlFr
            // 
            this.txtAvlFr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAvlFr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtAvlFr.Location = new System.Drawing.Point(100, 148);
            this.txtAvlFr.Name = "txtAvlFr";
            this.txtAvlFr.ReadOnly = true;
            this.txtAvlFr.Size = new System.Drawing.Size(128, 40);
            this.txtAvlFr.TabIndex = 21;
            // 
            // lblOutCap
            // 
            this.lblOutCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblOutCap.BackColor = System.Drawing.Color.White;
            this.lblOutCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblOutCap.ForeColor = System.Drawing.Color.Black;
            this.lblOutCap.Location = new System.Drawing.Point(232, 148);
            this.lblOutCap.Name = "lblOutCap";
            this.lblOutCap.Size = new System.Drawing.Size(96, 40);
            this.lblOutCap.TabIndex = 63;
            this.lblOutCap.Text = "출고대기";
            // 
            // txtOutQty
            // 
            this.txtOutQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtOutQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtOutQty.Location = new System.Drawing.Point(334, 148);
            this.txtOutQty.Name = "txtOutQty";
            this.txtOutQty.ReadOnly = true;
            this.txtOutQty.Size = new System.Drawing.Size(142, 40);
            this.txtOutQty.TabIndex = 22;
            // 
            // lstList
            // 
            this.lstList.Columns.Add(this.colLep);
            this.lstList.Columns.Add(this.colPtno);
            this.lstList.Columns.Add(this.colAvl);
            this.lstList.Columns.Add(this.colSal);
            this.lstList.Columns.Add(this.colCls);
            this.lstList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstList.FullRowSelect = true;
            this.lstList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstList.Location = new System.Drawing.Point(2, 194);
            this.lstList.Name = "lstList";
            this.lstList.Size = new System.Drawing.Size(476, 140);
            this.lstList.TabIndex = 7;
            this.lstList.View = System.Windows.Forms.View.Details;
            this.lstList.SelectedIndexChanged += new System.EventHandler(this.OnListSelected);
            this.lstList.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // colLep
            // 
            this.colLep.Text = "계열";
            this.colLep.Width = 50;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 190;
            // 
            // colAvl
            // 
            this.colAvl.Text = "재고";
            this.colAvl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAvl.Width = 80;
            // 
            // colSal
            // 
            this.colSal.Text = "출고";
            this.colSal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSal.Width = 70;
            // 
            // colCls
            // 
            this.colCls.Text = "CLS";
            this.colCls.Width = 70;
            // 
            // lblPartToCap
            // 
            this.lblPartToCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartToCap.BackColor = System.Drawing.Color.White;
            this.lblPartToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartToCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartToCap.Location = new System.Drawing.Point(0, 338);
            this.lblPartToCap.Name = "lblPartToCap";
            this.lblPartToCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartToCap.TabIndex = 66;
            this.lblPartToCap.Text = "부번";
            // 
            // lblPrefixTo
            // 
            this.lblPrefixTo.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefixTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefixTo.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefixTo.ForeColor = System.Drawing.Color.White;
            this.lblPrefixTo.Location = new System.Drawing.Point(61, 340);
            this.lblPrefixTo.Name = "lblPrefixTo";
            this.lblPrefixTo.Size = new System.Drawing.Size(30, 42);
            this.lblPrefixTo.TabIndex = 67;
            this.lblPrefixTo.Text = "H";
            // 
            // txtPartTo
            // 
            this.txtPartTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtPartTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPartTo.Location = new System.Drawing.Point(93, 338);
            this.txtPartTo.Name = "txtPartTo";
            this.txtPartTo.ReadOnly = true;
            this.txtPartTo.Size = new System.Drawing.Size(269, 46);
            this.txtPartTo.TabIndex = 23;
            // 
            // lblClassTo
            // 
            this.lblClassTo.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClassTo.BackColor = System.Drawing.Color.LightGray;
            this.lblClassTo.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClassTo.ForeColor = System.Drawing.Color.Black;
            this.lblClassTo.Location = new System.Drawing.Point(368, 339);
            this.lblClassTo.Name = "lblClassTo";
            this.lblClassTo.Size = new System.Drawing.Size(108, 40);
            this.lblClassTo.TabIndex = 69;
            // 
            // lblLocToCap
            // 
            this.lblLocToCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocToCap.BackColor = System.Drawing.Color.White;
            this.lblLocToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocToCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocToCap.Location = new System.Drawing.Point(0, 388);
            this.lblLocToCap.Name = "lblLocToCap";
            this.lblLocToCap.Size = new System.Drawing.Size(56, 46);
            this.lblLocToCap.TabIndex = 70;
            this.lblLocToCap.Text = "LOC";
            // 
            // txtLocTo
            // 
            this.txtLocTo.BackColor = System.Drawing.Color.White;
            this.txtLocTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocTo.Location = new System.Drawing.Point(62, 388);
            this.txtLocTo.Name = "txtLocTo";
            this.txtLocTo.Size = new System.Drawing.Size(300, 46);
            this.txtLocTo.TabIndex = 2;
            this.txtLocTo.GotFocus += new System.EventHandler(this.OnLocToFocus);
            this.txtLocTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocToKeyDown);
            // 
            // lblQtyToCap
            // 
            this.lblQtyToCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyToCap.BackColor = System.Drawing.Color.White;
            this.lblQtyToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblQtyToCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyToCap.Location = new System.Drawing.Point(0, 438);
            this.lblQtyToCap.Name = "lblQtyToCap";
            this.lblQtyToCap.Size = new System.Drawing.Size(56, 44);
            this.lblQtyToCap.TabIndex = 72;
            this.lblQtyToCap.Text = "수량";
            // 
            // txtQtyTo
            // 
            this.txtQtyTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtQtyTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtQtyTo.Location = new System.Drawing.Point(62, 438);
            this.txtQtyTo.Name = "txtQtyTo";
            this.txtQtyTo.ReadOnly = true;
            this.txtQtyTo.Size = new System.Drawing.Size(300, 46);
            this.txtQtyTo.TabIndex = 3;
            this.txtQtyTo.GotFocus += new System.EventHandler(this.OnOtherFocus);
            this.txtQtyTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // btnPart
            // 
            this.btnPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPart.Location = new System.Drawing.Point(368, 438);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new System.Drawing.Size(108, 44);
            this.btnPart.TabIndex = 15;
            this.btnPart.Text = "PART";
            this.btnPart.Click += new System.EventHandler(this.OnPart);
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnList);
            this._buttons.Controls.Add(this.btnMove);
            this._buttons.Controls.Add(this.btnDel);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnList
            // 
            this.btnList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnList.Location = new System.Drawing.Point(3, 4);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(117, 44);
            this.btnList.TabIndex = 10;
            this.btnList.Text = "내역";
            this.btnList.Click += new System.EventHandler(this.OnList);
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnMove.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMove.Location = new System.Drawing.Point(122, 4);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(117, 44);
            this.btnMove.TabIndex = 11;
            this.btnMove.Text = "불출";
            this.btnMove.Click += new System.EventHandler(this.OnMove);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDel.Location = new System.Drawing.Point(241, 4);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(117, 44);
            this.btnDel.TabIndex = 12;
            this.btnDel.Text = "삭제";
            this.btnDel.Click += new System.EventHandler(this.OnDelete);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(360, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(117, 44);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S401_LocMove
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S401_LocMove";
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
        private HaimsPda.Controls.VLabel lblCntCap;
        private System.Windows.Forms.TextBox txtCnt;
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLocFr;
        private System.Windows.Forms.Button btnMode;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblAvlCap;
        private System.Windows.Forms.TextBox txtAvlFr;
        private HaimsPda.Controls.VLabel lblOutCap;
        private System.Windows.Forms.TextBox txtOutQty;
        private System.Windows.Forms.ListView lstList;
        private HaimsPda.Controls.VLabel lblPartToCap;
        private HaimsPda.Controls.VLabel lblPrefixTo;
        private System.Windows.Forms.TextBox txtPartTo;
        private HaimsPda.Controls.VLabel lblClassTo;
        private HaimsPda.Controls.VLabel lblLocToCap;
        private System.Windows.Forms.TextBox txtLocTo;
        private HaimsPda.Controls.VLabel lblQtyToCap;
        private System.Windows.Forms.TextBox txtQtyTo;
        private System.Windows.Forms.Button btnPart;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colAvl;
        private System.Windows.Forms.ColumnHeader colSal;
        private System.Windows.Forms.ColumnHeader colCls;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnClear;
    }
}
