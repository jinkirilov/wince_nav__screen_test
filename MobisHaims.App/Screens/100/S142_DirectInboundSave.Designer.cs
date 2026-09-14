namespace MobisHaims.Screens
{
    partial class S142_DirectInboundSave
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
            this.lblVndCap = new MobisHaims.Controls.VLabel();
            this.cboVndMn = new System.Windows.Forms.ComboBox();
            this.cboVndSb = new System.Windows.Forms.ComboBox();
            this.lblPartCap = new MobisHaims.Controls.VLabel();
            this.cboLep = new System.Windows.Forms.ComboBox();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblPriceCap = new MobisHaims.Controls.VLabel();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new MobisHaims.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblWhsCap = new MobisHaims.Controls.VLabel();
            this.cboWhs = new System.Windows.Forms.ComboBox();
            this.chkAdjust = new System.Windows.Forms.CheckBox();
            this.lstRows = new System.Windows.Forms.ListView();
            this.colNo = new System.Windows.Forms.ColumnHeader();
            this.colPart = new System.Windows.Forms.ColumnHeader();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colAmt = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblVndCap);
            this._fields.Controls.Add(this.cboVndMn);
            this._fields.Controls.Add(this.cboVndSb);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.cboLep);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblPriceCap);
            this._fields.Controls.Add(this.txtPrice);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Controls.Add(this.lblWhsCap);
            this._fields.Controls.Add(this.cboWhs);
            this._fields.Controls.Add(this.chkAdjust);
            this._fields.Controls.Add(this.lstRows);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblVndCap
            // 
            this.lblVndCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblVndCap.BackColor = System.Drawing.Color.White;
            this.lblVndCap.ForeColor = System.Drawing.Color.Black;
            this.lblVndCap.Location = new System.Drawing.Point(4, 4);
            this.lblVndCap.Name = "lblVndCap";
            this.lblVndCap.Size = new System.Drawing.Size(80, 34);
            this.lblVndCap.Text = "업체";
            // 
            // cboVndMn
            // 
            this.cboVndMn.Location = new System.Drawing.Point(88, 4);
            this.cboVndMn.Name = "cboVndMn";
            this.cboVndMn.Size = new System.Drawing.Size(196, 34);
            this.cboVndMn.TabIndex = 0;
            this.cboVndMn.SelectedIndexChanged += new System.EventHandler(this.OnVndMnChanged);
            // 
            // cboVndSb
            // 
            this.cboVndSb.Location = new System.Drawing.Point(288, 4);
            this.cboVndSb.Name = "cboVndSb";
            this.cboVndSb.Size = new System.Drawing.Size(188, 34);
            this.cboVndSb.TabIndex = 1;
            this.cboVndSb.SelectedIndexChanged += new System.EventHandler(this.OnVndSbChanged);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(4, 44);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(80, 34);
            this.lblPartCap.Text = "부번";
            // 
            // cboLep
            // 
            this.cboLep.Location = new System.Drawing.Point(88, 44);
            this.cboLep.Name = "cboLep";
            this.cboLep.Size = new System.Drawing.Size(70, 34);
            this.cboLep.TabIndex = 2;
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(180)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(162, 44);
            this.txtPart.MaxLength = 20;
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(314, 38);
            this.txtPart.TabIndex = 3;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 84);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.Text = "";
            // 
            // lblPriceCap
            // 
            this.lblPriceCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPriceCap.BackColor = System.Drawing.Color.White;
            this.lblPriceCap.ForeColor = System.Drawing.Color.Black;
            this.lblPriceCap.Location = new System.Drawing.Point(4, 124);
            this.lblPriceCap.Name = "lblPriceCap";
            this.lblPriceCap.Size = new System.Drawing.Size(80, 34);
            this.lblPriceCap.Text = "단가";
            // 
            // txtPrice
            // 
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPrice.Location = new System.Drawing.Point(88, 124);
            this.txtPrice.MaxLength = 9;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(120, 38);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPriceKeyDown);
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(216, 124);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(60, 34);
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Location = new System.Drawing.Point(280, 124);
            this.txtQty.MaxLength = 5;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(90, 38);
            this.txtQty.TabIndex = 5;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // lblWhsCap
            // 
            this.lblWhsCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblWhsCap.BackColor = System.Drawing.Color.White;
            this.lblWhsCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhsCap.Location = new System.Drawing.Point(4, 166);
            this.lblWhsCap.Name = "lblWhsCap";
            this.lblWhsCap.Size = new System.Drawing.Size(80, 34);
            this.lblWhsCap.Text = "창고";
            // 
            // cboWhs
            // 
            this.cboWhs.Location = new System.Drawing.Point(88, 166);
            this.cboWhs.Name = "cboWhs";
            this.cboWhs.Size = new System.Drawing.Size(120, 34);
            this.cboWhs.TabIndex = 6;
            // 
            // chkAdjust
            // 
            this.chkAdjust.Checked = true;
            this.chkAdjust.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdjust.Location = new System.Drawing.Point(216, 166);
            this.chkAdjust.Name = "chkAdjust";
            this.chkAdjust.Size = new System.Drawing.Size(154, 34);
            this.chkAdjust.TabIndex = 7;
            this.chkAdjust.Text = "재고조정";
            // 
            // lstRows
            // 
            this.lstRows.Columns.Add(this.colNo);
            this.lstRows.Columns.Add(this.colPart);
            this.lstRows.Columns.Add(this.colLoc);
            this.lstRows.Columns.Add(this.colQty);
            this.lstRows.Columns.Add(this.colAmt);
            this.lstRows.FullRowSelect = true;
            this.lstRows.Location = new System.Drawing.Point(4, 208);
            this.lstRows.Name = "lstRows";
            this.lstRows.Size = new System.Drawing.Size(472, 272);
            this.lstRows.TabIndex = 8;
            this.lstRows.View = System.Windows.Forms.View.Details;
            // 
            // colNo
            // 
            this.colNo.Text = "No";
            this.colNo.Width = 40;
            // 
            // colPart
            // 
            this.colPart.Text = "부품번호";
            this.colPart.Width = 150;
            // 
            // colLoc
            // 
            this.colLoc.Text = "로케이션";
            this.colLoc.Width = 132;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.Width = 60;
            // 
            // colAmt
            // 
            this.colAmt.Text = "금액";
            this.colAmt.Width = 86;
            // 
            // _buttons
            // 
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnDel);
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
            this.btnSave.Location = new System.Drawing.Point(4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(155, 44);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnDel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDel.Location = new System.Drawing.Point(163, 4);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(155, 44);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "행삭제";
            this.btnDel.Click += new System.EventHandler(this.OnDel);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(321, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(155, 44);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S142_DirectInboundSave
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S142_DirectInboundSave";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private MobisHaims.Controls.VLabel lblVndCap;
        private System.Windows.Forms.ComboBox cboVndMn;
        private System.Windows.Forms.ComboBox cboVndSb;
        private MobisHaims.Controls.VLabel lblPartCap;
        private System.Windows.Forms.ComboBox cboLep;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblPriceCap;
        private System.Windows.Forms.TextBox txtPrice;
        private MobisHaims.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private MobisHaims.Controls.VLabel lblWhsCap;
        private System.Windows.Forms.ComboBox cboWhs;
        private System.Windows.Forms.CheckBox chkAdjust;
        private System.Windows.Forms.ListView lstRows;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ColumnHeader colPart;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colAmt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnClear;
    }
}
