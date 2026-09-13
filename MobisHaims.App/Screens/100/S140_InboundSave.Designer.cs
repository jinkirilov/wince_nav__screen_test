namespace MobisHaims.Screens
{
    partial class S140_InboundSave
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
            this.lblPartCap = new MobisHaims.Controls.VLabel();
            this.lblPrefix = new MobisHaims.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new MobisHaims.Controls.VLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblWhCap = new MobisHaims.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.lblAssignCntCap = new MobisHaims.Controls.VLabel();
            this.txtAssignCnt = new System.Windows.Forms.TextBox();
            this.btnAssignList = new System.Windows.Forms.Button();
            this.lblLocCap = new MobisHaims.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lblCurStockCap = new MobisHaims.Controls.VLabel();
            this.txtCurStock = new System.Windows.Forms.TextBox();
            this.lblSchedCap = new MobisHaims.Controls.VLabel();
            this.txtSched = new System.Windows.Forms.TextBox();
            this.lblNoStockCap = new MobisHaims.Controls.VLabel();
            this.txtNoStock = new System.Windows.Forms.TextBox();
            this.lblReserveCap = new MobisHaims.Controls.VLabel();
            this.txtReserve = new System.Windows.Forms.TextBox();
            this.lblAssignQtyCap = new MobisHaims.Controls.VLabel();
            this.txtAssignQty = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new MobisHaims.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnNotRecv = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblAssignCntCap);
            this._fields.Controls.Add(this.txtAssignCnt);
            this._fields.Controls.Add(this.btnAssignList);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lblCurStockCap);
            this._fields.Controls.Add(this.txtCurStock);
            this._fields.Controls.Add(this.lblSchedCap);
            this._fields.Controls.Add(this.txtSched);
            this._fields.Controls.Add(this.lblNoStockCap);
            this._fields.Controls.Add(this.txtNoStock);
            this._fields.Controls.Add(this.lblReserveCap);
            this._fields.Controls.Add(this.txtReserve);
            this._fields.Controls.Add(this.lblAssignQtyCap);
            this._fields.Controls.Add(this.txtAssignQty);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 2);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 40);
            this.lblPartCap.TabIndex = 0;
            this.lblPartCap.Text = "부번";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(78, 3);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 1;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(106, 2);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(220, 46);
            this.txtPart.TabIndex = 0;
            this.txtPart.TextChanged += new System.EventHandler(this.txtPart_TextChanged);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblClass.Location = new System.Drawing.Point(330, 3);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(54, 40);
            this.lblClass.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(388, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPartName.Location = new System.Drawing.Point(6, 50);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(462, 34);
            this.lblPartName.TabIndex = 3;
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 92);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(100, 40);
            this.lblWhCap.TabIndex = 4;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(106, 92);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(110, 40);
            this.cboWh.TabIndex = 2;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnWhChanged);
            // 
            // lblAssignCntCap
            // 
            this.lblAssignCntCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblAssignCntCap.BackColor = System.Drawing.Color.White;
            this.lblAssignCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAssignCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblAssignCntCap.Location = new System.Drawing.Point(0, 134);
            this.lblAssignCntCap.Name = "lblAssignCntCap";
            this.lblAssignCntCap.Size = new System.Drawing.Size(100, 40);
            this.lblAssignCntCap.TabIndex = 5;
            this.lblAssignCntCap.Text = "할당건수";
            // 
            // txtAssignCnt
            // 
            this.txtAssignCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAssignCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtAssignCnt.Location = new System.Drawing.Point(106, 134);
            this.txtAssignCnt.Name = "txtAssignCnt";
            this.txtAssignCnt.ReadOnly = true;
            this.txtAssignCnt.Size = new System.Drawing.Size(110, 40);
            this.txtAssignCnt.TabIndex = 20;
            // 
            // btnAssignList
            // 
            this.btnAssignList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAssignList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAssignList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAssignList.Location = new System.Drawing.Point(224, 134);
            this.btnAssignList.Name = "btnAssignList";
            this.btnAssignList.Size = new System.Drawing.Size(118, 40);
            this.btnAssignList.TabIndex = 3;
            this.btnAssignList.Text = "할당내역";
            this.btnAssignList.Click += new System.EventHandler(this.OnAssignList);
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 176);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(100, 44);
            this.lblLocCap.TabIndex = 21;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(106, 176);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(370, 49);
            this.txtLoc.TabIndex = 4;
            this.txtLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            // 
            // lblCurStockCap
            // 
            this.lblCurStockCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblCurStockCap.BackColor = System.Drawing.Color.White;
            this.lblCurStockCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCurStockCap.ForeColor = System.Drawing.Color.Black;
            this.lblCurStockCap.Location = new System.Drawing.Point(0, 222);
            this.lblCurStockCap.Name = "lblCurStockCap";
            this.lblCurStockCap.Size = new System.Drawing.Size(100, 40);
            this.lblCurStockCap.TabIndex = 22;
            this.lblCurStockCap.Text = "가용재고";
            // 
            // txtCurStock
            // 
            this.txtCurStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtCurStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtCurStock.Location = new System.Drawing.Point(106, 222);
            this.txtCurStock.Name = "txtCurStock";
            this.txtCurStock.ReadOnly = true;
            this.txtCurStock.Size = new System.Drawing.Size(370, 40);
            this.txtCurStock.TabIndex = 21;
            // 
            // lblSchedCap
            // 
            this.lblSchedCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblSchedCap.BackColor = System.Drawing.Color.White;
            this.lblSchedCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblSchedCap.ForeColor = System.Drawing.Color.Black;
            this.lblSchedCap.Location = new System.Drawing.Point(0, 264);
            this.lblSchedCap.Name = "lblSchedCap";
            this.lblSchedCap.Size = new System.Drawing.Size(100, 40);
            this.lblSchedCap.TabIndex = 23;
            this.lblSchedCap.Text = "입고예정(D/I)";
            // 
            // txtSched
            // 
            this.txtSched.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtSched.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtSched.Location = new System.Drawing.Point(106, 264);
            this.txtSched.Name = "txtSched";
            this.txtSched.ReadOnly = true;
            this.txtSched.Size = new System.Drawing.Size(370, 40);
            this.txtSched.TabIndex = 22;
            // 
            // lblNoStockCap
            // 
            this.lblNoStockCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblNoStockCap.BackColor = System.Drawing.Color.White;
            this.lblNoStockCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblNoStockCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblNoStockCap.Location = new System.Drawing.Point(0, 306);
            this.lblNoStockCap.Name = "lblNoStockCap";
            this.lblNoStockCap.Size = new System.Drawing.Size(100, 40);
            this.lblNoStockCap.TabIndex = 24;
            this.lblNoStockCap.Text = "무재고수량";
            // 
            // txtNoStock
            // 
            this.txtNoStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtNoStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtNoStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtNoStock.Location = new System.Drawing.Point(106, 306);
            this.txtNoStock.Name = "txtNoStock";
            this.txtNoStock.ReadOnly = true;
            this.txtNoStock.Size = new System.Drawing.Size(370, 40);
            this.txtNoStock.TabIndex = 23;
            // 
            // lblReserveCap
            // 
            this.lblReserveCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblReserveCap.BackColor = System.Drawing.Color.White;
            this.lblReserveCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblReserveCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblReserveCap.Location = new System.Drawing.Point(0, 348);
            this.lblReserveCap.Name = "lblReserveCap";
            this.lblReserveCap.Size = new System.Drawing.Size(100, 40);
            this.lblReserveCap.TabIndex = 25;
            this.lblReserveCap.Text = "예약(긴급)";
            // 
            // txtReserve
            // 
            this.txtReserve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtReserve.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtReserve.Location = new System.Drawing.Point(106, 348);
            this.txtReserve.Name = "txtReserve";
            this.txtReserve.ReadOnly = true;
            this.txtReserve.Size = new System.Drawing.Size(370, 40);
            this.txtReserve.TabIndex = 24;
            // 
            // lblAssignQtyCap
            // 
            this.lblAssignQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblAssignQtyCap.BackColor = System.Drawing.Color.White;
            this.lblAssignQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAssignQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblAssignQtyCap.Location = new System.Drawing.Point(0, 390);
            this.lblAssignQtyCap.Name = "lblAssignQtyCap";
            this.lblAssignQtyCap.Size = new System.Drawing.Size(100, 44);
            this.lblAssignQtyCap.TabIndex = 26;
            this.lblAssignQtyCap.Text = "할당수량";
            // 
            // txtAssignQty
            // 
            this.txtAssignQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAssignQty.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular);
            this.txtAssignQty.Location = new System.Drawing.Point(106, 390);
            this.txtAssignQty.Name = "txtAssignQty";
            this.txtAssignQty.ReadOnly = true;
            this.txtAssignQty.Size = new System.Drawing.Size(130, 50);
            this.txtAssignQty.TabIndex = 25;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(286, 395);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(54, 44);
            this.lblQtyCap.TabIndex = 27;
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(346, 390);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(130, 49);
            this.txtQty.TabIndex = 5;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnStock);
            this._buttons.Controls.Add(this.btnLoc);
            this._buttons.Controls.Add(this.btnNotRecv);
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Location = new System.Drawing.Point(3, 4);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(92, 44);
            this.btnStock.TabIndex = 10;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(98, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(92, 44);
            this.btnLoc.TabIndex = 11;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
            // 
            // btnNotRecv
            // 
            this.btnNotRecv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnNotRecv.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnNotRecv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNotRecv.Location = new System.Drawing.Point(193, 4);
            this.btnNotRecv.Name = "btnNotRecv";
            this.btnNotRecv.Size = new System.Drawing.Size(92, 44);
            this.btnNotRecv.TabIndex = 12;
            this.btnNotRecv.Text = "미수령";
            this.btnNotRecv.Click += new System.EventHandler(this.OnNotRecv);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Location = new System.Drawing.Point(288, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 44);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(383, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(92, 44);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S140_InboundSave
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S140_InboundSave";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private MobisHaims.Controls.VLabel lblPartCap;
        private MobisHaims.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblClass;
        private System.Windows.Forms.Button btnSearch;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private MobisHaims.Controls.VLabel lblAssignCntCap;
        private System.Windows.Forms.TextBox txtAssignCnt;
        private System.Windows.Forms.Button btnAssignList;
        private MobisHaims.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private MobisHaims.Controls.VLabel lblCurStockCap;
        private System.Windows.Forms.TextBox txtCurStock;
        private MobisHaims.Controls.VLabel lblSchedCap;
        private System.Windows.Forms.TextBox txtSched;
        private MobisHaims.Controls.VLabel lblNoStockCap;
        private System.Windows.Forms.TextBox txtNoStock;
        private MobisHaims.Controls.VLabel lblReserveCap;
        private System.Windows.Forms.TextBox txtReserve;
        private MobisHaims.Controls.VLabel lblAssignQtyCap;
        private System.Windows.Forms.TextBox txtAssignQty;
        private MobisHaims.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnNotRecv;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
    }
}
