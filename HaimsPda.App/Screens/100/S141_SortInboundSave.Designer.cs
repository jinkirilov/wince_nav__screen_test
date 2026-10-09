namespace HaimsPda.Screens
{
    partial class S141_SortInboundSave
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
            this.lblAssignCntCap = new HaimsPda.Controls.VLabel();
            this.txtAssignCnt = new System.Windows.Forms.TextBox();
            this.btnAssignList = new System.Windows.Forms.Button();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lstWork = new System.Windows.Forms.ListView();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.lblCount = new HaimsPda.Controls.VLabel();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lblPart2Cap = new HaimsPda.Controls.VLabel();
            this.lblPrefix2 = new HaimsPda.Controls.VLabel();
            this.txtPart2 = new System.Windows.Forms.TextBox();
            this.lblClass2 = new HaimsPda.Controls.VLabel();
            this.lblCurInvCap = new HaimsPda.Controls.VLabel();
            this.txtCurInv = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnLocReg = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblAssignCntCap);
            this._fields.Controls.Add(this.txtAssignCnt);
            this._fields.Controls.Add(this.btnAssignList);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lstWork);
            this._fields.Controls.Add(this.lblCount);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lblPart2Cap);
            this._fields.Controls.Add(this.lblPrefix2);
            this._fields.Controls.Add(this.txtPart2);
            this._fields.Controls.Add(this.lblClass2);
            this._fields.Controls.Add(this.lblCurInvCap);
            this._fields.Controls.Add(this.txtCurInv);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
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
            this.lblWhCap.TabIndex = 30;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(60, 2);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(90, 40);
            this.cboWh.TabIndex = 20;
            // 
            // lblAssignCntCap
            // 
            this.lblAssignCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAssignCntCap.BackColor = System.Drawing.Color.White;
            this.lblAssignCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAssignCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblAssignCntCap.Location = new System.Drawing.Point(152, 2);
            this.lblAssignCntCap.Name = "lblAssignCntCap";
            this.lblAssignCntCap.Size = new System.Drawing.Size(90, 40);
            this.lblAssignCntCap.TabIndex = 31;
            this.lblAssignCntCap.Text = "할당건수";
            // 
            // txtAssignCnt
            // 
            this.txtAssignCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAssignCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtAssignCnt.Location = new System.Drawing.Point(246, 2);
            this.txtAssignCnt.Name = "txtAssignCnt";
            this.txtAssignCnt.ReadOnly = true;
            this.txtAssignCnt.Size = new System.Drawing.Size(70, 40);
            this.txtAssignCnt.TabIndex = 32;
            // 
            // btnAssignList
            // 
            this.btnAssignList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAssignList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAssignList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAssignList.Location = new System.Drawing.Point(322, 2);
            this.btnAssignList.Name = "btnAssignList";
            this.btnAssignList.Size = new System.Drawing.Size(154, 40);
            this.btnAssignList.TabIndex = 21;
            this.btnAssignList.Text = "할당내역";
            this.btnAssignList.Click += new System.EventHandler(this.OnAssignList);
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
            this.lblPartCap.TabIndex = 33;
            this.lblPartCap.Text = "부번";
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
            this.lblPrefix.TabIndex = 34;
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
            this.lblClass.TabIndex = 35;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(400, 48);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 40);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPartName.Location = new System.Drawing.Point(6, 96);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(390, 34);
            this.lblPartName.TabIndex = 36;
            // 
            // lblCount
            // 
            this.lblCount.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCount.Location = new System.Drawing.Point(400, 96);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(76, 34);
            this.lblCount.TabIndex = 37;
            this.lblCount.Text = "0건";
            // 
            // lstWork
            // 
            this.lstWork.Columns.Add(this.colLep);
            this.lstWork.Columns.Add(this.colPtno);
            this.lstWork.Columns.Add(this.colQty);
            this.lstWork.Columns.Add(this.colLoc);
            this.lstWork.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstWork.FullRowSelect = true;
            this.lstWork.Location = new System.Drawing.Point(4, 134);
            this.lstWork.Name = "lstWork";
            this.lstWork.Size = new System.Drawing.Size(472, 172);
            this.lstWork.TabIndex = 23;
            this.lstWork.View = System.Windows.Forms.View.Details;
            this.lstWork.SelectedIndexChanged += new System.EventHandler(this.OnListSelected);
            // 
            // colLep
            // 
            this.colLep.Text = "L";
            this.colLep.Width = 40;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 180;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 70;
            // 
            // colLoc
            // 
            this.colLoc.Text = "로케이션";
            this.colLoc.Width = 170;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 312);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 44);
            this.lblLocCap.TabIndex = 38;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(60, 310);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(416, 49);
            this.txtLoc.TabIndex = 1;
            this.txtLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            // 
            // lblPart2Cap
            // 
            this.lblPart2Cap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPart2Cap.BackColor = System.Drawing.Color.White;
            this.lblPart2Cap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPart2Cap.ForeColor = System.Drawing.Color.Black;
            this.lblPart2Cap.Location = new System.Drawing.Point(0, 364);
            this.lblPart2Cap.Name = "lblPart2Cap";
            this.lblPart2Cap.Size = new System.Drawing.Size(56, 40);
            this.lblPart2Cap.TabIndex = 39;
            this.lblPart2Cap.Text = "부번";
            // 
            // lblPrefix2
            // 
            this.lblPrefix2.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix2.ForeColor = System.Drawing.Color.White;
            this.lblPrefix2.Location = new System.Drawing.Point(60, 365);
            this.lblPrefix2.Name = "lblPrefix2";
            this.lblPrefix2.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix2.TabIndex = 40;
            this.lblPrefix2.Text = "H";
            // 
            // txtPart2
            // 
            this.txtPart2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart2.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart2.Location = new System.Drawing.Point(88, 362);
            this.txtPart2.Name = "txtPart2";
            this.txtPart2.Size = new System.Drawing.Size(250, 46);
            this.txtPart2.TabIndex = 2;
            this.txtPart2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPart2KeyDown);
            // 
            // lblClass2
            // 
            this.lblClass2.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass2.BackColor = System.Drawing.Color.LightGray;
            this.lblClass2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblClass2.Location = new System.Drawing.Point(342, 365);
            this.lblClass2.Name = "lblClass2";
            this.lblClass2.Size = new System.Drawing.Size(54, 40);
            this.lblClass2.TabIndex = 41;
            // 
            // lblCurInvCap
            // 
            this.lblCurInvCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCurInvCap.BackColor = System.Drawing.Color.White;
            this.lblCurInvCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCurInvCap.ForeColor = System.Drawing.Color.Black;
            this.lblCurInvCap.Location = new System.Drawing.Point(0, 416);
            this.lblCurInvCap.Name = "lblCurInvCap";
            this.lblCurInvCap.Size = new System.Drawing.Size(90, 44);
            this.lblCurInvCap.TabIndex = 42;
            this.lblCurInvCap.Text = "가용재고";
            // 
            // txtCurInv
            // 
            this.txtCurInv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtCurInv.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtCurInv.Location = new System.Drawing.Point(94, 414);
            this.txtCurInv.Name = "txtCurInv";
            this.txtCurInv.ReadOnly = true;
            this.txtCurInv.Size = new System.Drawing.Size(130, 46);
            this.txtCurInv.TabIndex = 43;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(230, 416);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(70, 44);
            this.lblQtyCap.TabIndex = 44;
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(304, 412);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(172, 49);
            this.txtQty.TabIndex = 3;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnLocReg);
            this._buttons.Controls.Add(this.btnSort);
            this._buttons.Controls.Add(this.btnSkip);
            this._buttons.Controls.Add(this.btnDelete);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnLocReg
            // 
            this.btnLocReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLocReg.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLocReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLocReg.Location = new System.Drawing.Point(2, 4);
            this.btnLocReg.Name = "btnLocReg";
            this.btnLocReg.Size = new System.Drawing.Size(76, 44);
            this.btnLocReg.TabIndex = 10;
            this.btnLocReg.Text = "LOC등록";
            this.btnLocReg.Click += new System.EventHandler(this.OnLocRegister);
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSort.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSort.Location = new System.Drawing.Point(82, 4);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(76, 44);
            this.btnSort.TabIndex = 11;
            this.btnSort.Text = "SORT";
            this.btnSort.Click += new System.EventHandler(this.OnSort);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSkip.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSkip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSkip.Location = new System.Drawing.Point(162, 4);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(76, 44);
            this.btnSkip.TabIndex = 12;
            this.btnSkip.Text = "SKIP";
            this.btnSkip.Click += new System.EventHandler(this.OnSkip);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDelete.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDelete.Location = new System.Drawing.Point(242, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(76, 44);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Click += new System.EventHandler(this.OnDelete);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(322, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 44);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Location = new System.Drawing.Point(402, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 44);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // S141_SortInboundSave
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S141_SortInboundSave";
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
        private HaimsPda.Controls.VLabel lblAssignCntCap;
        private System.Windows.Forms.TextBox txtAssignCnt;
        private System.Windows.Forms.Button btnAssignList;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblPartName;
        private System.Windows.Forms.ListView lstWork;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colLoc;
        private HaimsPda.Controls.VLabel lblCount;
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private HaimsPda.Controls.VLabel lblPart2Cap;
        private HaimsPda.Controls.VLabel lblPrefix2;
        private System.Windows.Forms.TextBox txtPart2;
        private HaimsPda.Controls.VLabel lblClass2;
        private HaimsPda.Controls.VLabel lblCurInvCap;
        private System.Windows.Forms.TextBox txtCurInv;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnLocReg;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
    }
}
