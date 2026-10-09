namespace HaimsPda.Screens
{
    partial class S330_StockAdjust
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
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.lblWh = new HaimsPda.Controls.VLabel();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblLocQtyCap = new HaimsPda.Controls.VLabel();
            this.txtLocQty = new System.Windows.Forms.TextBox();
            this.lblAdjQtyCap = new HaimsPda.Controls.VLabel();
            this.txtAdjQty = new System.Windows.Forms.TextBox();
            this.lblAfterQtyCap = new HaimsPda.Controls.VLabel();
            this.txtAfterQty = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.lblWh);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblLocQtyCap);
            this._fields.Controls.Add(this.txtLocQty);
            this._fields.Controls.Add(this.lblAdjQtyCap);
            this._fields.Controls.Add(this.txtAdjQty);
            this._fields.Controls.Add(this.lblAfterQtyCap);
            this._fields.Controls.Add(this.txtAfterQty);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 2);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 40);
            this.lblLocCap.TabIndex = 20;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(62, 2);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.ReadOnly = true;
            this.txtLoc.Size = new System.Drawing.Size(270, 46);
            this.txtLoc.TabIndex = 21;
            this.txtLoc.TabStop = false;
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(336, 2);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(60, 42);
            this.lblWhCap.TabIndex = 22;
            this.lblWhCap.Text = "창고";
            // 
            // lblWh
            // 
            this.lblWh.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblWh.BackColor = System.Drawing.Color.LightGray;
            this.lblWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblWh.ForeColor = System.Drawing.Color.Black;
            this.lblWh.Location = new System.Drawing.Point(400, 4);
            this.lblWh.Name = "lblWh";
            this.lblWh.Size = new System.Drawing.Size(76, 42);
            this.lblWh.TabIndex = 23;
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 50);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 24;
            this.lblPartCap.Text = "부번";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(61, 52);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(30, 42);
            this.lblPrefix.TabIndex = 25;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(93, 52);
            this.txtPart.Name = "txtPart";
            this.txtPart.ReadOnly = true;
            this.txtPart.Size = new System.Drawing.Size(277, 46);
            this.txtPart.TabIndex = 26;
            this.txtPart.TabStop = false;
            // 
            // lblClass
            // 
            this.lblClass.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblClass.Location = new System.Drawing.Point(376, 52);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 42);
            this.lblClass.TabIndex = 27;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 100);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 28;
            // 
            // lblLocQtyCap
            // 
            this.lblLocQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocQtyCap.BackColor = System.Drawing.Color.White;
            this.lblLocQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocQtyCap.Location = new System.Drawing.Point(0, 142);
            this.lblLocQtyCap.Name = "lblLocQtyCap";
            this.lblLocQtyCap.Size = new System.Drawing.Size(150, 40);
            this.lblLocQtyCap.TabIndex = 29;
            this.lblLocQtyCap.Text = "LOC수량";
            // 
            // txtLocQty
            // 
            this.txtLocQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLocQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocQty.Location = new System.Drawing.Point(156, 142);
            this.txtLocQty.Name = "txtLocQty";
            this.txtLocQty.ReadOnly = true;
            this.txtLocQty.Size = new System.Drawing.Size(320, 40);
            this.txtLocQty.TabIndex = 30;
            this.txtLocQty.TabStop = false;
            this.txtLocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAdjQtyCap
            // 
            this.lblAdjQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAdjQtyCap.BackColor = System.Drawing.Color.White;
            this.lblAdjQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdjQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblAdjQtyCap.Location = new System.Drawing.Point(0, 190);
            this.lblAdjQtyCap.Name = "lblAdjQtyCap";
            this.lblAdjQtyCap.Size = new System.Drawing.Size(150, 46);
            this.lblAdjQtyCap.TabIndex = 31;
            this.lblAdjQtyCap.Text = "조정처리수량";
            // 
            // txtAdjQty
            // 
            this.txtAdjQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtAdjQty.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtAdjQty.Location = new System.Drawing.Point(156, 188);
            this.txtAdjQty.MaxLength = 9;
            this.txtAdjQty.Name = "txtAdjQty";
            this.txtAdjQty.Size = new System.Drawing.Size(320, 49);
            this.txtAdjQty.TabIndex = 0;
            this.txtAdjQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdjQty.TextChanged += new System.EventHandler(this.OnAdjTextChanged);
            this.txtAdjQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnAdjKeyDown);
            this.txtAdjQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnAdjKeyPress);
            // 
            // lblAfterQtyCap
            // 
            this.lblAfterQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblAfterQtyCap.BackColor = System.Drawing.Color.White;
            this.lblAfterQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAfterQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblAfterQtyCap.Location = new System.Drawing.Point(0, 246);
            this.lblAfterQtyCap.Name = "lblAfterQtyCap";
            this.lblAfterQtyCap.Size = new System.Drawing.Size(150, 40);
            this.lblAfterQtyCap.TabIndex = 32;
            this.lblAfterQtyCap.Text = "조정후수량";
            // 
            // txtAfterQty
            // 
            this.txtAfterQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAfterQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtAfterQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.txtAfterQty.Location = new System.Drawing.Point(156, 246);
            this.txtAfterQty.Name = "txtAfterQty";
            this.txtAfterQty.ReadOnly = true;
            this.txtAfterQty.Size = new System.Drawing.Size(320, 40);
            this.txtAfterQty.TabIndex = 33;
            this.txtAfterQty.TabStop = false;
            this.txtAfterQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnStock);
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
            this.btnSave.Size = new System.Drawing.Size(156, 44);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Location = new System.Drawing.Point(162, 4);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(156, 44);
            this.btnStock.TabIndex = 11;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
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
            // S330_StockAdjust
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S330_StockAdjust";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private HaimsPda.Controls.VLabel lblWhCap;
        private HaimsPda.Controls.VLabel lblWh;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblLocQtyCap;
        private System.Windows.Forms.TextBox txtLocQty;
        private HaimsPda.Controls.VLabel lblAdjQtyCap;
        private System.Windows.Forms.TextBox txtAdjQty;
        private HaimsPda.Controls.VLabel lblAfterQtyCap;
        private System.Windows.Forms.TextBox txtAfterQty;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnClear;
    }
}
