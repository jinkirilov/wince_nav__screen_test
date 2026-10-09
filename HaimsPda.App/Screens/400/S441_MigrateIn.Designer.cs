namespace HaimsPda.Screens
{
    partial class S441_MigrateIn
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
            this.lblFromHdr = new HaimsPda.Controls.VLabel();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblFromWhCap = new HaimsPda.Controls.VLabel();
            this.txtFromWh = new System.Windows.Forms.TextBox();
            this.lblFromLocCap = new HaimsPda.Controls.VLabel();
            this.txtFromLoc = new System.Windows.Forms.TextBox();
            this.lblExpCap = new HaimsPda.Controls.VLabel();
            this.txtExpQty = new System.Windows.Forms.TextBox();
            this.lblToHdr = new HaimsPda.Controls.VLabel();
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.lblLocToCap = new HaimsPda.Controls.VLabel();
            this.txtLocTo = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new HaimsPda.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblFromHdr);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblFromWhCap);
            this._fields.Controls.Add(this.txtFromWh);
            this._fields.Controls.Add(this.lblFromLocCap);
            this._fields.Controls.Add(this.txtFromLoc);
            this._fields.Controls.Add(this.lblExpCap);
            this._fields.Controls.Add(this.txtExpQty);
            this._fields.Controls.Add(this.lblToHdr);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.lblLocToCap);
            this._fields.Controls.Add(this.txtLocTo);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblFromHdr
            // 
            this.lblFromHdr.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblFromHdr.BackColor = System.Drawing.Color.White;
            this.lblFromHdr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblFromHdr.ForeColor = System.Drawing.Color.Black;
            this.lblFromHdr.Location = new System.Drawing.Point(4, 2);
            this.lblFromHdr.Name = "lblFromHdr";
            this.lblFromHdr.Size = new System.Drawing.Size(472, 30);
            this.lblFromHdr.TabIndex = 50;
            this.lblFromHdr.Text = "[FROM 정보]";
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 34);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 51;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(61, 36);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(30, 40);
            this.lblPrefix.TabIndex = 52;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(93, 35);
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
            this.lblClass.Location = new System.Drawing.Point(376, 36);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 42);
            this.lblClass.TabIndex = 54;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 84);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 55;
            // 
            // lblFromWhCap
            // 
            this.lblFromWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblFromWhCap.BackColor = System.Drawing.Color.White;
            this.lblFromWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblFromWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblFromWhCap.Location = new System.Drawing.Point(0, 122);
            this.lblFromWhCap.Name = "lblFromWhCap";
            this.lblFromWhCap.Size = new System.Drawing.Size(96, 40);
            this.lblFromWhCap.TabIndex = 56;
            this.lblFromWhCap.Text = "창고";
            // 
            // txtFromWh
            // 
            this.txtFromWh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtFromWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtFromWh.Location = new System.Drawing.Point(100, 122);
            this.txtFromWh.Name = "txtFromWh";
            this.txtFromWh.ReadOnly = true;
            this.txtFromWh.Size = new System.Drawing.Size(120, 40);
            this.txtFromWh.TabIndex = 20;
            this.txtFromWh.TabStop = false;
            // 
            // lblFromLocCap
            // 
            this.lblFromLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblFromLocCap.BackColor = System.Drawing.Color.White;
            this.lblFromLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblFromLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblFromLocCap.Location = new System.Drawing.Point(0, 166);
            this.lblFromLocCap.Name = "lblFromLocCap";
            this.lblFromLocCap.Size = new System.Drawing.Size(96, 40);
            this.lblFromLocCap.TabIndex = 58;
            this.lblFromLocCap.Text = "LOCATION";
            // 
            // txtFromLoc
            // 
            this.txtFromLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtFromLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtFromLoc.Location = new System.Drawing.Point(100, 166);
            this.txtFromLoc.Name = "txtFromLoc";
            this.txtFromLoc.ReadOnly = true;
            this.txtFromLoc.Size = new System.Drawing.Size(376, 40);
            this.txtFromLoc.TabIndex = 21;
            this.txtFromLoc.TabStop = false;
            // 
            // lblExpCap
            // 
            this.lblExpCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblExpCap.BackColor = System.Drawing.Color.White;
            this.lblExpCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblExpCap.ForeColor = System.Drawing.Color.Black;
            this.lblExpCap.Location = new System.Drawing.Point(0, 210);
            this.lblExpCap.Name = "lblExpCap";
            this.lblExpCap.Size = new System.Drawing.Size(96, 40);
            this.lblExpCap.TabIndex = 60;
            this.lblExpCap.Text = "대상수량";
            // 
            // txtExpQty
            // 
            this.txtExpQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtExpQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtExpQty.Location = new System.Drawing.Point(100, 210);
            this.txtExpQty.Name = "txtExpQty";
            this.txtExpQty.ReadOnly = true;
            this.txtExpQty.Size = new System.Drawing.Size(120, 40);
            this.txtExpQty.TabIndex = 22;
            this.txtExpQty.TabStop = false;
            // 
            // lblToHdr
            // 
            this.lblToHdr.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblToHdr.BackColor = System.Drawing.Color.White;
            this.lblToHdr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblToHdr.ForeColor = System.Drawing.Color.Black;
            this.lblToHdr.Location = new System.Drawing.Point(4, 254);
            this.lblToHdr.Name = "lblToHdr";
            this.lblToHdr.Size = new System.Drawing.Size(472, 30);
            this.lblToHdr.TabIndex = 62;
            this.lblToHdr.Text = "[TO LOC 저장]";
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 288);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 42);
            this.lblWhCap.TabIndex = 63;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.cboWh.Location = new System.Drawing.Point(62, 288);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(100, 46);
            this.cboWh.TabIndex = 8;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnWhChanged);
            this.cboWh.GotFocus += new System.EventHandler(this.OnOtherFocus);
            // 
            // lblLocToCap
            // 
            this.lblLocToCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocToCap.BackColor = System.Drawing.Color.White;
            this.lblLocToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocToCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocToCap.Location = new System.Drawing.Point(0, 336);
            this.lblLocToCap.Name = "lblLocToCap";
            this.lblLocToCap.Size = new System.Drawing.Size(56, 50);
            this.lblLocToCap.TabIndex = 65;
            this.lblLocToCap.Text = "LOC";
            // 
            // txtLocTo
            // 
            this.txtLocTo.BackColor = System.Drawing.Color.White;
            this.txtLocTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocTo.Location = new System.Drawing.Point(62, 336);
            this.txtLocTo.Name = "txtLocTo";
            this.txtLocTo.Size = new System.Drawing.Size(414, 46);
            this.txtLocTo.TabIndex = 1;
            this.txtLocTo.GotFocus += new System.EventHandler(this.OnLocFocus);
            this.txtLocTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(0, 392);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(56, 50);
            this.lblQtyCap.TabIndex = 67;
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(62, 392);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(200, 55);
            this.txtQty.TabIndex = 2;
            this.txtQty.GotFocus += new System.EventHandler(this.OnOtherFocus);
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnQtyKeyPress);
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnLoc);
            this._buttons.Controls.Add(this.btnStock);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(3, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(155, 44);
            this.btnLoc.TabIndex = 10;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Location = new System.Drawing.Point(161, 4);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(155, 44);
            this.btnStock.TabIndex = 11;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(319, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(155, 44);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S441_MigrateIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S441_MigrateIn";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblFromHdr;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblFromWhCap;
        private System.Windows.Forms.TextBox txtFromWh;
        private HaimsPda.Controls.VLabel lblFromLocCap;
        private System.Windows.Forms.TextBox txtFromLoc;
        private HaimsPda.Controls.VLabel lblExpCap;
        private System.Windows.Forms.TextBox txtExpQty;
        private HaimsPda.Controls.VLabel lblToHdr;
        private HaimsPda.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private HaimsPda.Controls.VLabel lblLocToCap;
        private System.Windows.Forms.TextBox txtLocTo;
        private HaimsPda.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnClear;
    }
}
