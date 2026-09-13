namespace MobisHaims.Screens
{
    partial class S321_PartStock
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
            this.btnGubun = new System.Windows.Forms.Button();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblAvlQtyCap = new MobisHaims.Controls.VLabel();
            this.txtAvlQty = new System.Windows.Forms.TextBox();
            this.lblPriceCap = new MobisHaims.Controls.VLabel();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblDefQtyCap = new MobisHaims.Controls.VLabel();
            this.txtDefQty = new System.Windows.Forms.TextBox();
            this.lblDoQtyCap = new MobisHaims.Controls.VLabel();
            this.txtDoQty = new System.Windows.Forms.TextBox();
            this.lblAmsCap = new MobisHaims.Controls.VLabel();
            this.txtAms = new System.Windows.Forms.TextBox();
            this.lblSftQtyCap = new MobisHaims.Controls.VLabel();
            this.txtSftQty = new System.Windows.Forms.TextBox();
            this.lstLoc = new System.Windows.Forms.ListView();
            this.colWh = new System.Windows.Forms.ColumnHeader();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.lblVhcCap = new MobisHaims.Controls.VLabel();
            this.txtVhc = new System.Windows.Forms.TextBox();
            this.lblStdInCap = new MobisHaims.Controls.VLabel();
            this.txtStdIn = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnWealth = new System.Windows.Forms.Button();
            this.btnControl = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.btnGubun);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblAvlQtyCap);
            this._fields.Controls.Add(this.txtAvlQty);
            this._fields.Controls.Add(this.lblPriceCap);
            this._fields.Controls.Add(this.txtPrice);
            this._fields.Controls.Add(this.lblDefQtyCap);
            this._fields.Controls.Add(this.txtDefQty);
            this._fields.Controls.Add(this.lblDoQtyCap);
            this._fields.Controls.Add(this.txtDoQty);
            this._fields.Controls.Add(this.lblAmsCap);
            this._fields.Controls.Add(this.txtAms);
            this._fields.Controls.Add(this.lblSftQtyCap);
            this._fields.Controls.Add(this.txtSftQty);
            this._fields.Controls.Add(this.lstLoc);
            this._fields.Controls.Add(this.lblVhcCap);
            this._fields.Controls.Add(this.txtVhc);
            this._fields.Controls.Add(this.lblStdInCap);
            this._fields.Controls.Add(this.txtStdIn);
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
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(73, 3);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 1;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(100, 3);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(208, 46);
            this.txtPart.TabIndex = 0;
            this.txtPart.TextChanged += new System.EventHandler(this.txtPart_TextChanged);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.Gainsboro;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.Black;
            this.lblClass.Location = new System.Drawing.Point(310, 4);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(54, 40);
            this.lblClass.TabIndex = 2;
            this.lblClass.Text = "DA";
            // 
            // btnGubun
            // 
            this.btnGubun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnGubun.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnGubun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGubun.Location = new System.Drawing.Point(366, 4);
            this.btnGubun.Name = "btnGubun";
            this.btnGubun.Size = new System.Drawing.Size(111, 40);
            this.btnGubun.TabIndex = 1;
            this.btnGubun.Text = "재고유";
            this.btnGubun.Click += new System.EventHandler(this.OnGubunToggle);
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
            // lblAvlQtyCap
            // 
            this.lblAvlQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblAvlQtyCap.BackColor = System.Drawing.Color.White;
            this.lblAvlQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAvlQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblAvlQtyCap.Location = new System.Drawing.Point(2, 94);
            this.lblAvlQtyCap.Name = "lblAvlQtyCap";
            this.lblAvlQtyCap.Size = new System.Drawing.Size(69, 36);
            this.lblAvlQtyCap.TabIndex = 4;
            this.lblAvlQtyCap.Text = "재고";
            // 
            // txtAvlQty
            // 
            this.txtAvlQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAvlQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtAvlQty.Location = new System.Drawing.Point(77, 94);
            this.txtAvlQty.Name = "txtAvlQty";
            this.txtAvlQty.ReadOnly = true;
            this.txtAvlQty.Size = new System.Drawing.Size(143, 40);
            this.txtAvlQty.TabIndex = 20;
            // 
            // lblPriceCap
            // 
            this.lblPriceCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblPriceCap.BackColor = System.Drawing.Color.White;
            this.lblPriceCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPriceCap.ForeColor = System.Drawing.Color.Black;
            this.lblPriceCap.Location = new System.Drawing.Point(259, 94);
            this.lblPriceCap.Name = "lblPriceCap";
            this.lblPriceCap.Size = new System.Drawing.Size(69, 36);
            this.lblPriceCap.TabIndex = 21;
            this.lblPriceCap.Text = "단가";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtPrice.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtPrice.Location = new System.Drawing.Point(334, 94);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(143, 40);
            this.txtPrice.TabIndex = 21;
            // 
            // lblDefQtyCap
            // 
            this.lblDefQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblDefQtyCap.BackColor = System.Drawing.Color.White;
            this.lblDefQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDefQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblDefQtyCap.Location = new System.Drawing.Point(2, 137);
            this.lblDefQtyCap.Name = "lblDefQtyCap";
            this.lblDefQtyCap.Size = new System.Drawing.Size(69, 36);
            this.lblDefQtyCap.TabIndex = 22;
            this.lblDefQtyCap.Text = "결함";
            // 
            // txtDefQty
            // 
            this.txtDefQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDefQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtDefQty.Location = new System.Drawing.Point(77, 137);
            this.txtDefQty.Name = "txtDefQty";
            this.txtDefQty.ReadOnly = true;
            this.txtDefQty.Size = new System.Drawing.Size(143, 40);
            this.txtDefQty.TabIndex = 22;
            // 
            // lblDoQtyCap
            // 
            this.lblDoQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblDoQtyCap.BackColor = System.Drawing.Color.White;
            this.lblDoQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDoQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblDoQtyCap.Location = new System.Drawing.Point(259, 137);
            this.lblDoQtyCap.Name = "lblDoQtyCap";
            this.lblDoQtyCap.Size = new System.Drawing.Size(69, 36);
            this.lblDoQtyCap.TabIndex = 23;
            this.lblDoQtyCap.Text = "D/O";
            // 
            // txtDoQty
            // 
            this.txtDoQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDoQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtDoQty.Location = new System.Drawing.Point(334, 137);
            this.txtDoQty.Name = "txtDoQty";
            this.txtDoQty.ReadOnly = true;
            this.txtDoQty.Size = new System.Drawing.Size(143, 40);
            this.txtDoQty.TabIndex = 23;
            // 
            // lblAmsCap
            // 
            this.lblAmsCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblAmsCap.BackColor = System.Drawing.Color.White;
            this.lblAmsCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAmsCap.ForeColor = System.Drawing.Color.Black;
            this.lblAmsCap.Location = new System.Drawing.Point(2, 180);
            this.lblAmsCap.Name = "lblAmsCap";
            this.lblAmsCap.Size = new System.Drawing.Size(69, 36);
            this.lblAmsCap.TabIndex = 24;
            this.lblAmsCap.Text = "AMS";
            // 
            // txtAms
            // 
            this.txtAms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtAms.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtAms.Location = new System.Drawing.Point(77, 180);
            this.txtAms.Name = "txtAms";
            this.txtAms.ReadOnly = true;
            this.txtAms.Size = new System.Drawing.Size(143, 40);
            this.txtAms.TabIndex = 24;
            // 
            // lblSftQtyCap
            // 
            this.lblSftQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblSftQtyCap.BackColor = System.Drawing.Color.White;
            this.lblSftQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblSftQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblSftQtyCap.Location = new System.Drawing.Point(259, 180);
            this.lblSftQtyCap.Name = "lblSftQtyCap";
            this.lblSftQtyCap.Size = new System.Drawing.Size(69, 36);
            this.lblSftQtyCap.TabIndex = 25;
            this.lblSftQtyCap.Text = "안전";
            // 
            // txtSftQty
            // 
            this.txtSftQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtSftQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtSftQty.Location = new System.Drawing.Point(334, 180);
            this.txtSftQty.Name = "txtSftQty";
            this.txtSftQty.ReadOnly = true;
            this.txtSftQty.Size = new System.Drawing.Size(143, 40);
            this.txtSftQty.TabIndex = 25;
            // 
            // lstLoc
            // 
            this.lstLoc.Columns.Add(this.colWh);
            this.lstLoc.Columns.Add(this.colLoc);
            this.lstLoc.Columns.Add(this.colQty);
            this.lstLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstLoc.FullRowSelect = true;
            this.lstLoc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLoc.Location = new System.Drawing.Point(2, 232);
            this.lstLoc.Name = "lstLoc";
            this.lstLoc.Size = new System.Drawing.Size(476, 185);
            this.lstLoc.TabIndex = 2;
            this.lstLoc.View = System.Windows.Forms.View.Details;
            // 
            // colWh
            // 
            this.colWh.Text = "창고";
            this.colWh.Width = 75;
            // 
            // colLoc
            // 
            this.colLoc.Text = "LOC";
            this.colLoc.Width = 250;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 130;
            // 
            // lblVhcCap
            // 
            this.lblVhcCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblVhcCap.BackColor = System.Drawing.Color.White;
            this.lblVhcCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblVhcCap.ForeColor = System.Drawing.Color.Black;
            this.lblVhcCap.Location = new System.Drawing.Point(2, 433);
            this.lblVhcCap.Name = "lblVhcCap";
            this.lblVhcCap.Size = new System.Drawing.Size(69, 36);
            this.lblVhcCap.TabIndex = 26;
            this.lblVhcCap.Text = "차종";
            // 
            // txtVhc
            // 
            this.txtVhc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtVhc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtVhc.Location = new System.Drawing.Point(77, 431);
            this.txtVhc.Name = "txtVhc";
            this.txtVhc.ReadOnly = true;
            this.txtVhc.Size = new System.Drawing.Size(143, 40);
            this.txtVhc.TabIndex = 26;
            // 
            // lblStdInCap
            // 
            this.lblStdInCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblStdInCap.BackColor = System.Drawing.Color.White;
            this.lblStdInCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblStdInCap.ForeColor = System.Drawing.Color.Black;
            this.lblStdInCap.Location = new System.Drawing.Point(226, 433);
            this.lblStdInCap.Name = "lblStdInCap";
            this.lblStdInCap.Size = new System.Drawing.Size(107, 36);
            this.lblStdInCap.TabIndex = 27;
            this.lblStdInCap.Text = "저장대기";
            // 
            // txtStdIn
            // 
            this.txtStdIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtStdIn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtStdIn.Location = new System.Drawing.Point(334, 431);
            this.txtStdIn.Name = "txtStdIn";
            this.txtStdIn.ReadOnly = true;
            this.txtStdIn.Size = new System.Drawing.Size(143, 40);
            this.txtStdIn.TabIndex = 27;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnWealth);
            this._buttons.Controls.Add(this.btnControl);
            this._buttons.Controls.Add(this.btnAdjust);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnWealth
            // 
            this.btnWealth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnWealth.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnWealth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWealth.Location = new System.Drawing.Point(3, 4);
            this.btnWealth.Name = "btnWealth";
            this.btnWealth.Size = new System.Drawing.Size(116, 44);
            this.btnWealth.TabIndex = 10;
            this.btnWealth.Text = "재물";
            this.btnWealth.Click += new System.EventHandler(this.OnWealth);
            // 
            // btnControl
            // 
            this.btnControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnControl.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnControl.Location = new System.Drawing.Point(122, 4);
            this.btnControl.Name = "btnControl";
            this.btnControl.Size = new System.Drawing.Size(116, 44);
            this.btnControl.TabIndex = 11;
            this.btnControl.Text = "통제";
            this.btnControl.Click += new System.EventHandler(this.OnControl);
            // 
            // btnAdjust
            // 
            this.btnAdjust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAdjust.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdjust.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAdjust.Location = new System.Drawing.Point(241, 4);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(116, 44);
            this.btnAdjust.TabIndex = 12;
            this.btnAdjust.Text = "조정";
            this.btnAdjust.Click += new System.EventHandler(this.OnAdjust);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(360, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 44);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S321_PartStock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S321_PartStock";
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
        private System.Windows.Forms.Button btnGubun;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblAvlQtyCap;
        private System.Windows.Forms.TextBox txtAvlQty;
        private MobisHaims.Controls.VLabel lblPriceCap;
        private System.Windows.Forms.TextBox txtPrice;
        private MobisHaims.Controls.VLabel lblDefQtyCap;
        private System.Windows.Forms.TextBox txtDefQty;
        private MobisHaims.Controls.VLabel lblDoQtyCap;
        private System.Windows.Forms.TextBox txtDoQty;
        private MobisHaims.Controls.VLabel lblAmsCap;
        private System.Windows.Forms.TextBox txtAms;
        private MobisHaims.Controls.VLabel lblSftQtyCap;
        private System.Windows.Forms.TextBox txtSftQty;
        private System.Windows.Forms.ListView lstLoc;
        private System.Windows.Forms.ColumnHeader colWh;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.ColumnHeader colQty;
        private MobisHaims.Controls.VLabel lblVhcCap;
        private System.Windows.Forms.TextBox txtVhc;
        private MobisHaims.Controls.VLabel lblStdInCap;
        private System.Windows.Forms.TextBox txtStdIn;
        private System.Windows.Forms.Button btnWealth;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Button btnClear;
    }
}
