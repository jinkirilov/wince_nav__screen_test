namespace MobisHaims.Screens
{
    partial class S322_OsdControl
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
            this.lblLocCap = new MobisHaims.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.lblPartCap = new MobisHaims.Controls.VLabel();
            this.lblPrefix = new MobisHaims.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new MobisHaims.Controls.VLabel();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblObjQtyCap = new MobisHaims.Controls.VLabel();
            this.txtObjQty = new System.Windows.Forms.TextBox();
            this.lblOsdQtyCap = new MobisHaims.Controls.VLabel();
            this.txtOsdQty = new System.Windows.Forms.TextBox();
            this.lblReasonCap = new MobisHaims.Controls.VLabel();
            this.cboReason = new System.Windows.Forms.ComboBox();
            this.lblDoQtyCap = new MobisHaims.Controls.VLabel();
            this.txtDoQty = new System.Windows.Forms.TextBox();
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
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblObjQtyCap);
            this._fields.Controls.Add(this.txtObjQty);
            this._fields.Controls.Add(this.lblOsdQtyCap);
            this._fields.Controls.Add(this.txtOsdQty);
            this._fields.Controls.Add(this.lblReasonCap);
            this._fields.Controls.Add(this.cboReason);
            this._fields.Controls.Add(this.lblDoQtyCap);
            this._fields.Controls.Add(this.txtDoQty);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
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
            this.txtLoc.Size = new System.Drawing.Size(308, 46);
            this.txtLoc.TabIndex = 21;
            this.txtLoc.TabStop = false;
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 48);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 22;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(61, 50);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(30, 42);
            this.lblPrefix.TabIndex = 23;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(93, 50);
            this.txtPart.Name = "txtPart";
            this.txtPart.ReadOnly = true;
            this.txtPart.Size = new System.Drawing.Size(277, 46);
            this.txtPart.TabIndex = 24;
            this.txtPart.TabStop = false;
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.Black;
            this.lblClass.Location = new System.Drawing.Point(376, 50);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 42);
            this.lblClass.TabIndex = 25;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 98);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 26;
            // 
            // lblObjQtyCap
            // 
            this.lblObjQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblObjQtyCap.BackColor = System.Drawing.Color.White;
            this.lblObjQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblObjQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblObjQtyCap.Location = new System.Drawing.Point(0, 140);
            this.lblObjQtyCap.Name = "lblObjQtyCap";
            this.lblObjQtyCap.Size = new System.Drawing.Size(150, 40);
            this.lblObjQtyCap.TabIndex = 27;
            this.lblObjQtyCap.Text = "LOC수량";
            // 
            // txtObjQty
            // 
            this.txtObjQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtObjQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtObjQty.Location = new System.Drawing.Point(156, 140);
            this.txtObjQty.Name = "txtObjQty";
            this.txtObjQty.ReadOnly = true;
            this.txtObjQty.Size = new System.Drawing.Size(320, 40);
            this.txtObjQty.TabIndex = 28;
            this.txtObjQty.TabStop = false;
            this.txtObjQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblOsdQtyCap
            // 
            this.lblOsdQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblOsdQtyCap.BackColor = System.Drawing.Color.White;
            this.lblOsdQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblOsdQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblOsdQtyCap.Location = new System.Drawing.Point(0, 188);
            this.lblOsdQtyCap.Name = "lblOsdQtyCap";
            this.lblOsdQtyCap.Size = new System.Drawing.Size(150, 46);
            this.lblOsdQtyCap.TabIndex = 29;
            this.lblOsdQtyCap.Text = "OS/D처리수량";
            // 
            // txtOsdQty
            // 
            this.txtOsdQty.BackColor = System.Drawing.Color.White;
            this.txtOsdQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtOsdQty.Location = new System.Drawing.Point(156, 188);
            this.txtOsdQty.MaxLength = 9;
            this.txtOsdQty.Name = "txtOsdQty";
            this.txtOsdQty.Size = new System.Drawing.Size(320, 46);
            this.txtOsdQty.TabIndex = 0;
            this.txtOsdQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOsdQty.TextChanged += new System.EventHandler(this.OnOsdTextChanged);
            this.txtOsdQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnOsdKeyDown);
            this.txtOsdQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnOsdKeyPress);
            // 
            // lblReasonCap
            // 
            this.lblReasonCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblReasonCap.BackColor = System.Drawing.Color.White;
            this.lblReasonCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblReasonCap.ForeColor = System.Drawing.Color.Black;
            this.lblReasonCap.Location = new System.Drawing.Point(0, 242);
            this.lblReasonCap.Name = "lblReasonCap";
            this.lblReasonCap.Size = new System.Drawing.Size(150, 46);
            this.lblReasonCap.TabIndex = 30;
            this.lblReasonCap.Text = "OS&D 사유";
            // 
            // cboReason
            // 
            this.cboReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReason.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.cboReason.Location = new System.Drawing.Point(156, 242);
            this.cboReason.Name = "cboReason";
            this.cboReason.Size = new System.Drawing.Size(320, 46);
            this.cboReason.TabIndex = 1;
            this.cboReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnReasonKeyDown);
            // 
            // lblDoQtyCap
            // 
            this.lblDoQtyCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblDoQtyCap.BackColor = System.Drawing.Color.White;
            this.lblDoQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDoQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblDoQtyCap.Location = new System.Drawing.Point(0, 296);
            this.lblDoQtyCap.Name = "lblDoQtyCap";
            this.lblDoQtyCap.Size = new System.Drawing.Size(150, 40);
            this.lblDoQtyCap.TabIndex = 31;
            this.lblDoQtyCap.Text = "수정후수량";
            // 
            // txtDoQty
            // 
            this.txtDoQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDoQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtDoQty.Location = new System.Drawing.Point(156, 296);
            this.txtDoQty.Name = "txtDoQty";
            this.txtDoQty.ReadOnly = true;
            this.txtDoQty.Size = new System.Drawing.Size(320, 40);
            this.txtDoQty.TabIndex = 32;
            this.txtDoQty.TabStop = false;
            this.txtDoQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // S322_OsdControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S322_OsdControl";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private MobisHaims.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private MobisHaims.Controls.VLabel lblPartCap;
        private MobisHaims.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblClass;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblObjQtyCap;
        private System.Windows.Forms.TextBox txtObjQty;
        private MobisHaims.Controls.VLabel lblOsdQtyCap;
        private System.Windows.Forms.TextBox txtOsdQty;
        private MobisHaims.Controls.VLabel lblReasonCap;
        private System.Windows.Forms.ComboBox cboReason;
        private MobisHaims.Controls.VLabel lblDoQtyCap;
        private System.Windows.Forms.TextBox txtDoQty;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnClear;
    }
}
