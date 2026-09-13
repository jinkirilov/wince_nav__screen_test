namespace MobisHaims.Screens
{
    partial class S320_LocStock
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
            this.lblWhCap = new MobisHaims.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.lblCntCap = new MobisHaims.Controls.VLabel();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.btnGubun2 = new System.Windows.Forms.Button();
            this.lblLocCap = new MobisHaims.Controls.VLabel();
            this.txtLoc = new System.Windows.Forms.TextBox();
            this.btnGubun = new System.Windows.Forms.Button();
            this.lblPartCap = new MobisHaims.Controls.VLabel();
            this.lblPrefix = new MobisHaims.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new MobisHaims.Controls.VLabel();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lstLoc = new System.Windows.Forms.ListView();
            this.colPos = new System.Windows.Forms.ColumnHeader();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnControl = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.txtCnt);
            this._fields.Controls.Add(this.btnGubun2);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.txtLoc);
            this._fields.Controls.Add(this.btnGubun);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lstLoc);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 2);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 40);
            this.lblWhCap.TabIndex = 20;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(62, 2);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(70, 40);
            this.cboWh.TabIndex = 0;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(140, 2);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(80, 40);
            this.lblCntCap.TabIndex = 21;
            this.lblCntCap.Text = "품목수";
            // 
            // txtCnt
            // 
            this.txtCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtCnt.Location = new System.Drawing.Point(226, 2);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.ReadOnly = true;
            this.txtCnt.Size = new System.Drawing.Size(110, 40);
            this.txtCnt.TabIndex = 22;
            // 
            // btnGubun2
            // 
            this.btnGubun2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnGubun2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnGubun2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGubun2.Location = new System.Drawing.Point(376, 2);
            this.btnGubun2.Name = "btnGubun2";
            this.btnGubun2.Size = new System.Drawing.Size(100, 42);
            this.btnGubun2.TabIndex = 4;
            this.btnGubun2.Text = "표준유";
            this.btnGubun2.Click += new System.EventHandler(this.OnStdToggle);
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 50);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 44);
            this.lblLocCap.TabIndex = 23;
            this.lblLocCap.Text = "LOC";
            // 
            // txtLoc
            // 
            this.txtLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtLoc.Location = new System.Drawing.Point(62, 50);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(308, 46);
            this.txtLoc.TabIndex = 1;
            this.txtLoc.GotFocus += new System.EventHandler(this.OnLocFocus);
            this.txtLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLocKeyDown);
            // 
            // btnGubun
            // 
            this.btnGubun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnGubun.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnGubun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGubun.Location = new System.Drawing.Point(376, 50);
            this.btnGubun.Name = "btnGubun";
            this.btnGubun.Size = new System.Drawing.Size(100, 44);
            this.btnGubun.TabIndex = 3;
            this.btnGubun.Text = "재고유";
            this.btnGubun.Click += new System.EventHandler(this.OnGubunToggle);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 100);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 46);
            this.lblPartCap.TabIndex = 24;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(60, 100);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 25;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(87, 100);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(282, 46);
            this.txtPart.TabIndex = 2;
            this.txtPart.GotFocus += new System.EventHandler(this.OnPartFocus);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.Black;
            this.lblClass.Location = new System.Drawing.Point(376, 102);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 42);
            this.lblClass.TabIndex = 26;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 150);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 27;
            // 
            // lstLoc
            // 
            this.lstLoc.Columns.Add(this.colPos);
            this.lstLoc.Columns.Add(this.colLep);
            this.lstLoc.Columns.Add(this.colPtno);
            this.lstLoc.Columns.Add(this.colQty);
            this.lstLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstLoc.FullRowSelect = true;
            this.lstLoc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLoc.Location = new System.Drawing.Point(2, 188);
            this.lstLoc.Name = "lstLoc";
            this.lstLoc.Size = new System.Drawing.Size(476, 292);
            this.lstLoc.TabIndex = 5;
            this.lstLoc.View = System.Windows.Forms.View.Details;
            this.lstLoc.SelectedIndexChanged += new System.EventHandler(this.OnRowSelected);
            // 
            // colPos
            // 
            this.colPos.Text = "POS";
            this.colPos.Width = 60;
            // 
            // colLep
            // 
            this.colLep.Text = "L";
            this.colLep.Width = 50;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 195;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colQty.Width = 130;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnDetail);
            this._buttons.Controls.Add(this.btnControl);
            this._buttons.Controls.Add(this.btnAdjust);
            this._buttons.Controls.Add(this.btnLoc);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnDetail.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDetail.Location = new System.Drawing.Point(3, 4);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(92, 44);
            this.btnDetail.TabIndex = 10;
            this.btnDetail.Text = "세부";
            this.btnDetail.Click += new System.EventHandler(this.OnDetail);
            // 
            // btnControl
            // 
            this.btnControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnControl.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnControl.Location = new System.Drawing.Point(98, 4);
            this.btnControl.Name = "btnControl";
            this.btnControl.Size = new System.Drawing.Size(92, 44);
            this.btnControl.TabIndex = 11;
            this.btnControl.Text = "통제";
            this.btnControl.Click += new System.EventHandler(this.OnControl);
            // 
            // btnAdjust
            // 
            this.btnAdjust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnAdjust.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdjust.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAdjust.Location = new System.Drawing.Point(193, 4);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(92, 44);
            this.btnAdjust.TabIndex = 12;
            this.btnAdjust.Text = "조정";
            this.btnAdjust.Click += new System.EventHandler(this.OnAdjust);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(288, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(92, 44);
            this.btnLoc.TabIndex = 13;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLocMove);
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
            // S320_LocStock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S320_LocStock";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private MobisHaims.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private MobisHaims.Controls.VLabel lblCntCap;
        private System.Windows.Forms.TextBox txtCnt;
        private System.Windows.Forms.Button btnGubun2;
        private MobisHaims.Controls.VLabel lblLocCap;
        private System.Windows.Forms.TextBox txtLoc;
        private System.Windows.Forms.Button btnGubun;
        private MobisHaims.Controls.VLabel lblPartCap;
        private MobisHaims.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblClass;
        private MobisHaims.Controls.VLabel lblPartName;
        private System.Windows.Forms.ListView lstLoc;
        private System.Windows.Forms.ColumnHeader colPos;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnClear;
    }
}
