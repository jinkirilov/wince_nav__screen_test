namespace HaimsPda.Screens
{
    partial class S410_LocRegister
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
            this.btnMode = new System.Windows.Forms.Button();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.cboLep = new System.Windows.Forms.ComboBox();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblLoc1Cap = new HaimsPda.Controls.VLabel();
            this.txtLoc1 = new System.Windows.Forms.TextBox();
            this.txtLocQty1 = new System.Windows.Forms.TextBox();
            this.lblLoc2Cap = new HaimsPda.Controls.VLabel();
            this.txtLoc2 = new System.Windows.Forms.TextBox();
            this.txtLocQty2 = new System.Windows.Forms.TextBox();
            this.lblAvlCap = new HaimsPda.Controls.VLabel();
            this.txtAvl = new System.Windows.Forms.TextBox();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lblInCap = new HaimsPda.Controls.VLabel();
            this.lblOutCap = new HaimsPda.Controls.VLabel();
            this.txtIn = new System.Windows.Forms.TextBox();
            this.txtOut = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.btnMode);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.cboLep);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblLoc1Cap);
            this._fields.Controls.Add(this.txtLoc1);
            this._fields.Controls.Add(this.txtLocQty1);
            this._fields.Controls.Add(this.lblLoc2Cap);
            this._fields.Controls.Add(this.txtLoc2);
            this._fields.Controls.Add(this.txtLocQty2);
            this._fields.Controls.Add(this.lblAvlCap);
            this._fields.Controls.Add(this.txtAvl);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lblInCap);
            this._fields.Controls.Add(this.lblOutCap);
            this._fields.Controls.Add(this.txtIn);
            this._fields.Controls.Add(this.txtOut);
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
            this.cboWh.Size = new System.Drawing.Size(72, 46);
            this.cboWh.TabIndex = 8;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnWhChanged);
            this.cboWh.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // btnMode
            // 
            this.btnMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnMode.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMode.Location = new System.Drawing.Point(354, 2);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(122, 42);
            this.btnMode.TabIndex = 9;
            this.btnMode.Text = "등록용";
            this.btnMode.Click += new System.EventHandler(this.OnMode);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 48);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 53;
            this.lblPartCap.Text = "부품";
            // 
            // cboLep
            // 
            this.cboLep.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.cboLep.Location = new System.Drawing.Point(62, 50);
            this.cboLep.Name = "cboLep";
            this.cboLep.Size = new System.Drawing.Size(72, 46);
            this.cboLep.TabIndex = 7;
            this.cboLep.SelectedIndexChanged += new System.EventHandler(this.OnLepChanged);
            this.cboLep.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(138, 50);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(232, 46);
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
            this.lblClass.Location = new System.Drawing.Point(376, 50);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 42);
            this.lblClass.TabIndex = 56;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 98);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 57;
            // 
            // lblLoc1Cap
            // 
            this.lblLoc1Cap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLoc1Cap.BackColor = System.Drawing.Color.White;
            this.lblLoc1Cap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoc1Cap.ForeColor = System.Drawing.Color.Black;
            this.lblLoc1Cap.Location = new System.Drawing.Point(0, 138);
            this.lblLoc1Cap.Name = "lblLoc1Cap";
            this.lblLoc1Cap.Size = new System.Drawing.Size(96, 46);
            this.lblLoc1Cap.TabIndex = 58;
            this.lblLoc1Cap.Text = "LOC_H";
            // 
            // txtLoc1
            // 
            this.txtLoc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLoc1.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc1.Location = new System.Drawing.Point(112, 138);
            this.txtLoc1.Name = "txtLoc1";
            this.txtLoc1.ReadOnly = true;
            this.txtLoc1.Size = new System.Drawing.Size(255, 46);
            this.txtLoc1.TabIndex = 20;
            this.txtLoc1.TabStop = false;
            // 
            // txtLocQty1
            // 
            this.txtLocQty1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLocQty1.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocQty1.Location = new System.Drawing.Point(370, 138);
            this.txtLocQty1.Name = "txtLocQty1";
            this.txtLocQty1.ReadOnly = true;
            this.txtLocQty1.Size = new System.Drawing.Size(106, 46);
            this.txtLocQty1.TabIndex = 21;
            this.txtLocQty1.TabStop = false;
            // 
            // lblLoc2Cap
            // 
            this.lblLoc2Cap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLoc2Cap.BackColor = System.Drawing.Color.White;
            this.lblLoc2Cap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoc2Cap.ForeColor = System.Drawing.Color.Black;
            this.lblLoc2Cap.Location = new System.Drawing.Point(0, 188);
            this.lblLoc2Cap.Name = "lblLoc2Cap";
            this.lblLoc2Cap.Size = new System.Drawing.Size(96, 46);
            this.lblLoc2Cap.TabIndex = 61;
            this.lblLoc2Cap.Text = "LOC_K";
            // 
            // txtLoc2
            // 
            this.txtLoc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLoc2.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc2.Location = new System.Drawing.Point(112, 188);
            this.txtLoc2.Name = "txtLoc2";
            this.txtLoc2.ReadOnly = true;
            this.txtLoc2.Size = new System.Drawing.Size(255, 46);
            this.txtLoc2.TabIndex = 22;
            this.txtLoc2.TabStop = false;
            // 
            // txtLocQty2
            // 
            this.txtLocQty2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLocQty2.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocQty2.Location = new System.Drawing.Point(370, 188);
            this.txtLocQty2.Name = "txtLocQty2";
            this.txtLocQty2.ReadOnly = true;
            this.txtLocQty2.Size = new System.Drawing.Size(106, 46);
            this.txtLocQty2.TabIndex = 23;
            this.txtLocQty2.TabStop = false;
            // 
            // lblAvlCap
            // 
            this.lblAvlCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAvlCap.BackColor = System.Drawing.Color.White;
            this.lblAvlCap.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblAvlCap.ForeColor = System.Drawing.Color.Black;
            this.lblAvlCap.Location = new System.Drawing.Point(0, 240);
            this.lblAvlCap.Name = "lblAvlCap";
            this.lblAvlCap.Size = new System.Drawing.Size(150, 50);
            this.lblAvlCap.TabIndex = 64;
            this.lblAvlCap.Text = "가용재고";
            // 
            // txtAvl
            // 
            this.txtAvl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAvl.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.txtAvl.Location = new System.Drawing.Point(156, 240);
            this.txtAvl.Name = "txtAvl";
            this.txtAvl.ReadOnly = true;
            this.txtAvl.Size = new System.Drawing.Size(320, 55);
            this.txtAvl.TabIndex = 24;
            this.txtAvl.TabStop = false;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 298);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(107, 40);
            this.lblLocCap.TabIndex = 66;
            this.lblLocCap.Text = "등록LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.White;
            this.txtLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(112, 298);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(364, 46);
            this.txtLoc.TabIndex = 1;
            this.txtLoc.GotFocus += new System.EventHandler(this.OnLocFocus);
            this.txtLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            // 
            // lblInCap
            // 
            this.lblInCap.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblInCap.BackColor = System.Drawing.Color.LightGray;
            this.lblInCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblInCap.ForeColor = System.Drawing.Color.Black;
            this.lblInCap.Location = new System.Drawing.Point(4, 356);
            this.lblInCap.Name = "lblInCap";
            this.lblInCap.Size = new System.Drawing.Size(232, 34);
            this.lblInCap.TabIndex = 68;
            this.lblInCap.Text = "입고대기수량(D/I)";
            // 
            // lblOutCap
            // 
            this.lblOutCap.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblOutCap.BackColor = System.Drawing.Color.LightGray;
            this.lblOutCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblOutCap.ForeColor = System.Drawing.Color.Black;
            this.lblOutCap.Location = new System.Drawing.Point(244, 356);
            this.lblOutCap.Name = "lblOutCap";
            this.lblOutCap.Size = new System.Drawing.Size(232, 34);
            this.lblOutCap.TabIndex = 69;
            this.lblOutCap.Text = "출고대기수량(D/O)";
            // 
            // txtIn
            // 
            this.txtIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtIn.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtIn.Location = new System.Drawing.Point(4, 392);
            this.txtIn.Name = "txtIn";
            this.txtIn.ReadOnly = true;
            this.txtIn.Size = new System.Drawing.Size(232, 46);
            this.txtIn.TabIndex = 25;
            this.txtIn.TabStop = false;
            // 
            // txtOut
            // 
            this.txtOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtOut.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtOut.Location = new System.Drawing.Point(244, 392);
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.Size = new System.Drawing.Size(232, 46);
            this.txtOut.TabIndex = 26;
            this.txtOut.TabStop = false;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnStock);
            this._buttons.Controls.Add(this.btnAdjust);
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
            this.btnStock.Size = new System.Drawing.Size(156, 44);
            this.btnStock.TabIndex = 10;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
            // 
            // btnAdjust
            // 
            this.btnAdjust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAdjust.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdjust.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAdjust.Location = new System.Drawing.Point(162, 4);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(156, 44);
            this.btnAdjust.TabIndex = 11;
            this.btnAdjust.Text = "조정";
            this.btnAdjust.Click += new System.EventHandler(this.OnAdjust);
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
            // S410_LocRegister
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S410_LocRegister";
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
        private System.Windows.Forms.Button btnMode;
        private HaimsPda.Controls.VLabel lblPartCap;
        private System.Windows.Forms.ComboBox cboLep;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblLoc1Cap;
        private System.Windows.Forms.TextBox txtLoc1;
        private System.Windows.Forms.TextBox txtLocQty1;
        private HaimsPda.Controls.VLabel lblLoc2Cap;
        private System.Windows.Forms.TextBox txtLoc2;
        private System.Windows.Forms.TextBox txtLocQty2;
        private HaimsPda.Controls.VLabel lblAvlCap;
        private System.Windows.Forms.TextBox txtAvl;
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private HaimsPda.Controls.VLabel lblInCap;
        private HaimsPda.Controls.VLabel lblOutCap;
        private System.Windows.Forms.TextBox txtIn;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Button btnClear;
    }
}
