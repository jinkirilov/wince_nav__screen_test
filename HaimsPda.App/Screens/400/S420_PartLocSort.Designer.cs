namespace HaimsPda.Screens
{
    partial class S420_PartLocSort
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
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lstList = new System.Windows.Forms.ListView();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colWh = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.lblPartToCap = new HaimsPda.Controls.VLabel();
            this.lblPrefixTo = new HaimsPda.Controls.VLabel();
            this.txtPartTo = new System.Windows.Forms.TextBox();
            this.lblClassTo = new HaimsPda.Controls.VLabel();
            this.lblLocToCap = new HaimsPda.Controls.VLabel();
            this.txtLocTo = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lstList);
            this._fields.Controls.Add(this.lblPartToCap);
            this._fields.Controls.Add(this.lblPrefixTo);
            this._fields.Controls.Add(this.txtPartTo);
            this._fields.Controls.Add(this.lblClassTo);
            this._fields.Controls.Add(this.lblLocToCap);
            this._fields.Controls.Add(this.txtLocTo);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 2);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 50;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(61, 4);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(30, 40);
            this.lblPrefix.TabIndex = 51;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(93, 4);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(277, 46);
            this.txtPart.TabIndex = 0;
            this.txtPart.GotFocus += new System.EventHandler(this.OnPartFocus);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.Black;
            this.lblClass.Location = new System.Drawing.Point(376, 4);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 42);
            this.lblClass.TabIndex = 53;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 52);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 54;
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 90);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 40);
            this.lblWhCap.TabIndex = 55;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.cboWh.Location = new System.Drawing.Point(62, 90);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(100, 46);
            this.cboWh.TabIndex = 8;
            this.cboWh.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(166, 90);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 40);
            this.lblLocCap.TabIndex = 57;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtLoc.Location = new System.Drawing.Point(226, 90);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.ReadOnly = true;
            this.txtLoc.Size = new System.Drawing.Size(250, 40);
            this.txtLoc.TabIndex = 20;
            this.txtLoc.TabStop = false;
            // 
            // lstList
            // 
            this.lstList.Columns.Add(this.colLep);
            this.lstList.Columns.Add(this.colPtno);
            this.lstList.Columns.Add(this.colWh);
            this.lstList.Columns.Add(this.colQty);
            this.lstList.Columns.Add(this.colLoc);
            this.lstList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstList.FullRowSelect = true;
            this.lstList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstList.Location = new System.Drawing.Point(2, 142);
            this.lstList.Name = "lstList";
            this.lstList.Size = new System.Drawing.Size(476, 234);
            this.lstList.TabIndex = 7;
            this.lstList.View = System.Windows.Forms.View.Details;
            this.lstList.SelectedIndexChanged += new System.EventHandler(this.OnListSelected);
            this.lstList.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // colLep
            // 
            this.colLep.Text = "계열";
            this.colLep.Width = 45;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 180;
            // 
            // colWh
            // 
            this.colWh.Text = "창고";
            this.colWh.Width = 55;
            // 
            // colQty
            // 
            this.colQty.Text = "재고";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 70;
            // 
            // colLoc
            // 
            this.colLoc.Text = "LOCATION";
            this.colLoc.Width = 150;
            // 
            // lblPartToCap
            // 
            this.lblPartToCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartToCap.BackColor = System.Drawing.Color.White;
            this.lblPartToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartToCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartToCap.Location = new System.Drawing.Point(0, 382);
            this.lblPartToCap.Name = "lblPartToCap";
            this.lblPartToCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartToCap.TabIndex = 60;
            this.lblPartToCap.Text = "부품";
            // 
            // lblPrefixTo
            // 
            this.lblPrefixTo.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefixTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefixTo.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrefixTo.ForeColor = System.Drawing.Color.White;
            this.lblPrefixTo.Location = new System.Drawing.Point(61, 384);
            this.lblPrefixTo.Name = "lblPrefixTo";
            this.lblPrefixTo.Size = new System.Drawing.Size(30, 40);
            this.lblPrefixTo.TabIndex = 61;
            this.lblPrefixTo.Text = "H";
            // 
            // txtPartTo
            // 
            this.txtPartTo.BackColor = System.Drawing.Color.White;
            this.txtPartTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPartTo.Location = new System.Drawing.Point(93, 384);
            this.txtPartTo.Name = "txtPartTo";
            this.txtPartTo.Size = new System.Drawing.Size(277, 46);
            this.txtPartTo.TabIndex = 1;
            this.txtPartTo.GotFocus += new System.EventHandler(this.OnPartToFocus);
            this.txtPartTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartToKeyDown);
            // 
            // lblClassTo
            // 
            this.lblClassTo.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClassTo.BackColor = System.Drawing.Color.LightGray;
            this.lblClassTo.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClassTo.ForeColor = System.Drawing.Color.Black;
            this.lblClassTo.Location = new System.Drawing.Point(376, 384);
            this.lblClassTo.Name = "lblClassTo";
            this.lblClassTo.Size = new System.Drawing.Size(100, 42);
            this.lblClassTo.TabIndex = 63;
            // 
            // lblLocToCap
            // 
            this.lblLocToCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocToCap.BackColor = System.Drawing.Color.White;
            this.lblLocToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocToCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocToCap.Location = new System.Drawing.Point(0, 432);
            this.lblLocToCap.Name = "lblLocToCap";
            this.lblLocToCap.Size = new System.Drawing.Size(56, 46);
            this.lblLocToCap.TabIndex = 64;
            this.lblLocToCap.Text = "LOC";
            // 
            // txtLocTo
            // 
            this.txtLocTo.BackColor = System.Drawing.Color.White;
            this.txtLocTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocTo.Location = new System.Drawing.Point(62, 432);
            this.txtLocTo.Name = "txtLocTo";
            this.txtLocTo.Size = new System.Drawing.Size(260, 46);
            this.txtLocTo.TabIndex = 2;
            this.txtLocTo.GotFocus += new System.EventHandler(this.OnLocToFocus);
            this.txtLocTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocToKeyDown);
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(326, 432);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(60, 46);
            this.lblQtyCap.TabIndex = 66;
            this.lblQtyCap.Text = "재고";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(390, 432);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(86, 46);
            this.txtQty.TabIndex = 21;
            this.txtQty.TabStop = false;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnConfirm);
            this._buttons.Controls.Add(this.btnSort);
            this._buttons.Controls.Add(this.btnStock);
            this._buttons.Controls.Add(this.btnClear);
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
            this.btnConfirm.Size = new System.Drawing.Size(117, 44);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "확인";
            this.btnConfirm.Click += new System.EventHandler(this.OnConfirm);
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSort.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSort.Location = new System.Drawing.Point(122, 4);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(117, 44);
            this.btnSort.TabIndex = 11;
            this.btnSort.Text = "정렬";
            this.btnSort.Click += new System.EventHandler(this.OnSort);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Location = new System.Drawing.Point(241, 4);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(117, 44);
            this.btnStock.TabIndex = 12;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
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
            // S420_PartLocSort
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S420_PartLocSort";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private System.Windows.Forms.ListView lstList;
        private HaimsPda.Controls.VLabel lblPartToCap;
        private HaimsPda.Controls.VLabel lblPrefixTo;
        private System.Windows.Forms.TextBox txtPartTo;
        private HaimsPda.Controls.VLabel lblClassTo;
        private HaimsPda.Controls.VLabel lblLocToCap;
        private System.Windows.Forms.TextBox txtLocTo;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colWh;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnClear;
    }
}
