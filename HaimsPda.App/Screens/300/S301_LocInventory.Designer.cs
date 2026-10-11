namespace HaimsPda.Screens
{
    partial class S301_LocInventory
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
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lstLoc = new System.Windows.Forms.ListView();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colLocFl = new System.Windows.Forms.ColumnHeader();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblAvlCap = new HaimsPda.Controls.VLabel();
            this.lblAvl = new HaimsPda.Controls.VLabel();
            this.lblOutQtyCap = new HaimsPda.Controls.VLabel();
            this.lblOutQty = new HaimsPda.Controls.VLabel();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblPriceCap = new HaimsPda.Controls.VLabel();
            this.lblPrice = new HaimsPda.Controls.VLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnPart = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lstLoc);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblAvlCap);
            this._fields.Controls.Add(this.lblAvl);
            this._fields.Controls.Add(this.lblOutQtyCap);
            this._fields.Controls.Add(this.lblOutQty);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Controls.Add(this.lblPriceCap);
            this._fields.Controls.Add(this.lblPrice);
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
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(152, 2);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(48, 40);
            this.lblLocCap.TabIndex = 31;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(204, 0);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(272, 46);
            this.txtLoc.TabIndex = 0;
            this.txtLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            this.txtLoc.GotFocus += new System.EventHandler(this.OnLocFocus);
            // 
            // lstLoc
            // 
            this.lstLoc.Columns.Add(this.colLep);
            this.lstLoc.Columns.Add(this.colPtno);
            this.lstLoc.Columns.Add(this.colQty);
            this.lstLoc.Columns.Add(this.colLocFl);
            this.lstLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstLoc.FullRowSelect = true;
            this.lstLoc.Location = new System.Drawing.Point(4, 50);
            this.lstLoc.Name = "lstLoc";
            this.lstLoc.Size = new System.Drawing.Size(472, 172);
            this.lstLoc.TabIndex = 21;
            this.lstLoc.View = System.Windows.Forms.View.Details;
            this.lstLoc.SelectedIndexChanged += new System.EventHandler(this.OnRowSelected);
            // 
            // colLep
            // 
            this.colLep.Text = "";
            this.colLep.Width = 40;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 210;
            // 
            // colQty
            // 
            this.colQty.Text = "재고";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 90;
            // 
            // colLocFl
            // 
            this.colLocFl.Text = "통합";
            this.colLocFl.Width = 70;
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 228);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 40);
            this.lblPartCap.TabIndex = 32;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(60, 229);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 33;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(88, 226);
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
            this.lblClass.Location = new System.Drawing.Point(342, 229);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(54, 40);
            this.lblClass.TabIndex = 34;
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(400, 229);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(76, 40);
            this.lblCnt.TabIndex = 35;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPartName.Location = new System.Drawing.Point(4, 276);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 36;
            // 
            // lblAvlCap
            // 
            this.lblAvlCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAvlCap.BackColor = System.Drawing.Color.White;
            this.lblAvlCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAvlCap.ForeColor = System.Drawing.Color.Black;
            this.lblAvlCap.Location = new System.Drawing.Point(0, 316);
            this.lblAvlCap.Name = "lblAvlCap";
            this.lblAvlCap.Size = new System.Drawing.Size(90, 40);
            this.lblAvlCap.TabIndex = 37;
            this.lblAvlCap.Text = "재고수량";
            // 
            // lblAvl
            // 
            this.lblAvl.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAvl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblAvl.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvl.ForeColor = System.Drawing.Color.Black;
            this.lblAvl.Location = new System.Drawing.Point(94, 316);
            this.lblAvl.Name = "lblAvl";
            this.lblAvl.Size = new System.Drawing.Size(140, 40);
            this.lblAvl.TabIndex = 38;
            // 
            // lblOutQtyCap
            // 
            this.lblOutQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblOutQtyCap.BackColor = System.Drawing.Color.White;
            this.lblOutQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblOutQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblOutQtyCap.Location = new System.Drawing.Point(238, 316);
            this.lblOutQtyCap.Name = "lblOutQtyCap";
            this.lblOutQtyCap.Size = new System.Drawing.Size(90, 40);
            this.lblOutQtyCap.TabIndex = 39;
            this.lblOutQtyCap.Text = "출고대기";
            // 
            // lblOutQty
            // 
            this.lblOutQty.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblOutQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblOutQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblOutQty.ForeColor = System.Drawing.Color.Black;
            this.lblOutQty.Location = new System.Drawing.Point(332, 316);
            this.lblOutQty.Name = "lblOutQty";
            this.lblOutQty.Size = new System.Drawing.Size(144, 40);
            this.lblOutQty.TabIndex = 40;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(0, 364);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(90, 46);
            this.lblQtyCap.TabIndex = 41;
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.Location = new System.Drawing.Point(94, 362);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(140, 46);
            this.txtQty.TabIndex = 2;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnQtyKeyPress);
            // 
            // lblPriceCap
            // 
            this.lblPriceCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPriceCap.BackColor = System.Drawing.Color.White;
            this.lblPriceCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPriceCap.ForeColor = System.Drawing.Color.Black;
            this.lblPriceCap.Location = new System.Drawing.Point(238, 364);
            this.lblPriceCap.Name = "lblPriceCap";
            this.lblPriceCap.Size = new System.Drawing.Size(90, 46);
            this.lblPriceCap.TabIndex = 42;
            this.lblPriceCap.Text = "단가";
            // 
            // lblPrice
            // 
            this.lblPrice.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblPrice.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.Black;
            this.lblPrice.Location = new System.Drawing.Point(332, 364);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(144, 46);
            this.lblPrice.TabIndex = 43;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnSkip);
            this._buttons.Controls.Add(this.btnPart);
            this._buttons.Controls.Add(this.btnClear);
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
            this.btnSave.Size = new System.Drawing.Size(117, 44);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSkip.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSkip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSkip.Location = new System.Drawing.Point(122, 4);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(117, 44);
            this.btnSkip.TabIndex = 11;
            this.btnSkip.Text = "SKIP";
            this.btnSkip.Click += new System.EventHandler(this.OnSkip);
            // 
            // btnPart
            // 
            this.btnPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPart.Location = new System.Drawing.Point(241, 4);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new System.Drawing.Size(117, 44);
            this.btnPart.TabIndex = 12;
            this.btnPart.Text = "파트";
            this.btnPart.Click += new System.EventHandler(this.OnPart);
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
            // S301_LocInventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S301_LocInventory";
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
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private System.Windows.Forms.ListView lstLoc;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colLocFl;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblCnt;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblAvlCap;
        private HaimsPda.Controls.VLabel lblAvl;
        private HaimsPda.Controls.VLabel lblOutQtyCap;
        private HaimsPda.Controls.VLabel lblOutQty;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private HaimsPda.Controls.VLabel lblPriceCap;
        private HaimsPda.Controls.VLabel lblPrice;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnPart;
        private System.Windows.Forms.Button btnClear;
    }
}
