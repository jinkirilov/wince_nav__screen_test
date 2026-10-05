namespace MobisHaims.Screens
{
    partial class S431_TransferIn
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
            this.lblFromHdr = new MobisHaims.Controls.VLabel();
            this.lblPartCap = new MobisHaims.Controls.VLabel();
            this.lblPrefix = new MobisHaims.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new MobisHaims.Controls.VLabel();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblWhCap = new MobisHaims.Controls.VLabel();
            this.txtWh = new System.Windows.Forms.TextBox();
            this.lblLocFrCap = new MobisHaims.Controls.VLabel();
            this.txtLocFr = new System.Windows.Forms.TextBox();
            this.lblExpCap = new MobisHaims.Controls.VLabel();
            this.txtExpQty = new System.Windows.Forms.TextBox();
            this.lblToHdr = new MobisHaims.Controls.VLabel();
            this.lblLocToCap = new MobisHaims.Controls.VLabel();
            this.txtLocTo = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new MobisHaims.Controls.VLabel();
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
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.txtWh);
            this._fields.Controls.Add(this.lblLocFrCap);
            this._fields.Controls.Add(this.txtLocFr);
            this._fields.Controls.Add(this.lblExpCap);
            this._fields.Controls.Add(this.txtExpQty);
            this._fields.Controls.Add(this.lblToHdr);
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
            this.lblFromHdr.Align = MobisHaims.Controls.VAlign.MiddleLeft;
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
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
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
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(61, 36);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(30, 40);
            this.lblPrefix.TabIndex = 52;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.txtPart.ForeColor = System.Drawing.Color.Black;
            this.txtPart.Location = new System.Drawing.Point(93, 36);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(277, 43);
            this.txtPart.TabIndex = 0;
            this.txtPart.GotFocus += new System.EventHandler(this.OnPartFocus);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
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
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 84);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 55;
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 124);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(96, 40);
            this.lblWhCap.TabIndex = 56;
            this.lblWhCap.Text = "창고";
            // 
            // txtWh
            // 
            this.txtWh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtWh.Location = new System.Drawing.Point(100, 124);
            this.txtWh.Name = "txtWh";
            this.txtWh.ReadOnly = true;
            this.txtWh.Size = new System.Drawing.Size(120, 40);
            this.txtWh.TabIndex = 20;
            this.txtWh.TabStop = false;
            // 
            // lblLocFrCap
            // 
            this.lblLocFrCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblLocFrCap.BackColor = System.Drawing.Color.White;
            this.lblLocFrCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocFrCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocFrCap.Location = new System.Drawing.Point(0, 168);
            this.lblLocFrCap.Name = "lblLocFrCap";
            this.lblLocFrCap.Size = new System.Drawing.Size(96, 40);
            this.lblLocFrCap.TabIndex = 58;
            this.lblLocFrCap.Text = "LOC";
            // 
            // txtLocFr
            // 
            this.txtLocFr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLocFr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtLocFr.Location = new System.Drawing.Point(100, 168);
            this.txtLocFr.Name = "txtLocFr";
            this.txtLocFr.ReadOnly = true;
            this.txtLocFr.Size = new System.Drawing.Size(376, 40);
            this.txtLocFr.TabIndex = 21;
            this.txtLocFr.TabStop = false;
            // 
            // lblExpCap
            // 
            this.lblExpCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblExpCap.BackColor = System.Drawing.Color.White;
            this.lblExpCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblExpCap.ForeColor = System.Drawing.Color.Black;
            this.lblExpCap.Location = new System.Drawing.Point(0, 212);
            this.lblExpCap.Name = "lblExpCap";
            this.lblExpCap.Size = new System.Drawing.Size(96, 40);
            this.lblExpCap.TabIndex = 60;
            this.lblExpCap.Text = "대상수량";
            // 
            // txtExpQty
            // 
            this.txtExpQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtExpQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtExpQty.Location = new System.Drawing.Point(100, 212);
            this.txtExpQty.Name = "txtExpQty";
            this.txtExpQty.ReadOnly = true;
            this.txtExpQty.Size = new System.Drawing.Size(120, 40);
            this.txtExpQty.TabIndex = 22;
            this.txtExpQty.TabStop = false;
            // 
            // lblToHdr
            // 
            this.lblToHdr.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblToHdr.BackColor = System.Drawing.Color.White;
            this.lblToHdr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblToHdr.ForeColor = System.Drawing.Color.Black;
            this.lblToHdr.Location = new System.Drawing.Point(4, 265);
            this.lblToHdr.Name = "lblToHdr";
            this.lblToHdr.Size = new System.Drawing.Size(472, 30);
            this.lblToHdr.TabIndex = 62;
            this.lblToHdr.Text = "[TO LOC 저장]";
            // 
            // lblLocToCap
            // 
            this.lblLocToCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblLocToCap.BackColor = System.Drawing.Color.White;
            this.lblLocToCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocToCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocToCap.Location = new System.Drawing.Point(0, 299);
            this.lblLocToCap.Name = "lblLocToCap";
            this.lblLocToCap.Size = new System.Drawing.Size(56, 50);
            this.lblLocToCap.TabIndex = 63;
            this.lblLocToCap.Text = "LOC";
            // 
            // txtLocTo
            // 
            this.txtLocTo.BackColor = System.Drawing.Color.White;
            this.txtLocTo.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLocTo.Location = new System.Drawing.Point(62, 299);
            this.txtLocTo.Name = "txtLocTo";
            this.txtLocTo.Size = new System.Drawing.Size(414, 46);
            this.txtLocTo.TabIndex = 1;
            this.txtLocTo.GotFocus += new System.EventHandler(this.OnLocFocus);
            this.txtLocTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(0, 355);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(56, 50);
            this.lblQtyCap.TabIndex = 65;
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(62, 355);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(200, 55);
            this.txtQty.TabIndex = 2;
            this.txtQty.GotFocus += new System.EventHandler(this.OnOtherFocus);
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
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
            this.btnLoc.Size = new System.Drawing.Size(156, 44);
            this.btnLoc.TabIndex = 10;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
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
            // S431_TransferIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S431_TransferIn";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private MobisHaims.Controls.VLabel lblFromHdr;
        private MobisHaims.Controls.VLabel lblPartCap;
        private MobisHaims.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblClass;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblWhCap;
        private System.Windows.Forms.TextBox txtWh;
        private MobisHaims.Controls.VLabel lblLocFrCap;
        private System.Windows.Forms.TextBox txtLocFr;
        private MobisHaims.Controls.VLabel lblExpCap;
        private System.Windows.Forms.TextBox txtExpQty;
        private MobisHaims.Controls.VLabel lblToHdr;
        private MobisHaims.Controls.VLabel lblLocToCap;
        private System.Windows.Forms.TextBox txtLocTo;
        private MobisHaims.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnClear;
    }
}
