namespace MobisHaims.Screens
{
    partial class S324_PartInfo
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
            this.cboLep = new System.Windows.Forms.ComboBox();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new MobisHaims.Controls.VLabel();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblCarCap = new MobisHaims.Controls.VLabel();
            this.cboCar = new System.Windows.Forms.ComboBox();
            this.lblWhCap = new MobisHaims.Controls.VLabel();
            this.cboWh = new System.Windows.Forms.ComboBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lstPart = new System.Windows.Forms.ListView();
            this.colPtno = new System.Windows.Forms.ColumnHeader();
            this.colLocno = new System.Windows.Forms.ColumnHeader();
            this.colAvlQty = new System.Windows.Forms.ColumnHeader();
            this.colPrice = new System.Windows.Forms.ColumnHeader();
            this.colPtnm = new System.Windows.Forms.ColumnHeader();
            this.colGrade = new System.Windows.Forms.ColumnHeader();
            this.colVhc = new System.Windows.Forms.ColumnHeader();
            this.colInvQty = new System.Windows.Forms.ColumnHeader();
            this.colLep = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnPart = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.cboLep);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblCarCap);
            this._fields.Controls.Add(this.cboCar);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.cboWh);
            this._fields.Controls.Add(this.btnPrev);
            this._fields.Controls.Add(this.btnNext);
            this._fields.Controls.Add(this.lstPart);
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
            // cboLep
            // 
            this.cboLep.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboLep.Location = new System.Drawing.Point(62, 2);
            this.cboLep.Name = "cboLep";
            this.cboLep.Size = new System.Drawing.Size(60, 40);
            this.cboLep.TabIndex = 0;
            this.cboLep.SelectedIndexChanged += new System.EventHandler(this.OnLepChanged);
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(128, 2);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(274, 46);
            this.txtPart.TabIndex = 1;
            this.txtPart.TextChanged += new System.EventHandler(this.txtPart_TextChanged);
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.Black;
            this.lblClass.Location = new System.Drawing.Point(406, 2);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(70, 40);
            this.lblClass.TabIndex = 2;
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(4, 47);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 3;
            // 
            // lblCarCap
            // 
            this.lblCarCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblCarCap.BackColor = System.Drawing.Color.White;
            this.lblCarCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCarCap.ForeColor = System.Drawing.Color.Black;
            this.lblCarCap.Location = new System.Drawing.Point(0, 84);
            this.lblCarCap.Name = "lblCarCap";
            this.lblCarCap.Size = new System.Drawing.Size(56, 40);
            this.lblCarCap.TabIndex = 4;
            this.lblCarCap.Text = "차종";
            // 
            // cboCar
            // 
            this.cboCar.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboCar.Location = new System.Drawing.Point(62, 84);
            this.cboCar.Name = "cboCar";
            this.cboCar.Size = new System.Drawing.Size(150, 40);
            this.cboCar.TabIndex = 2;
            this.cboCar.SelectedIndexChanged += new System.EventHandler(this.OnCarChanged);
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = MobisHaims.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(213, 84);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(51, 40);
            this.lblWhCap.TabIndex = 5;
            this.lblWhCap.Text = "창고";
            // 
            // cboWh
            // 
            this.cboWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.cboWh.Location = new System.Drawing.Point(266, 84);
            this.cboWh.Name = "cboWh";
            this.cboWh.Size = new System.Drawing.Size(60, 40);
            this.cboWh.TabIndex = 3;
            this.cboWh.SelectedIndexChanged += new System.EventHandler(this.OnWhChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPrev.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrev.Location = new System.Drawing.Point(332, 84);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(70, 40);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "이전";
            this.btnPrev.Click += new System.EventHandler(this.OnPrev);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnNext.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNext.Location = new System.Drawing.Point(406, 84);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 40);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "다음";
            this.btnNext.Click += new System.EventHandler(this.OnNext);
            // 
            // lstPart
            // 
            this.lstPart.Columns.Add(this.colPtno);
            this.lstPart.Columns.Add(this.colLocno);
            this.lstPart.Columns.Add(this.colAvlQty);
            this.lstPart.Columns.Add(this.colPrice);
            this.lstPart.Columns.Add(this.colPtnm);
            this.lstPart.Columns.Add(this.colGrade);
            this.lstPart.Columns.Add(this.colVhc);
            this.lstPart.Columns.Add(this.colInvQty);
            this.lstPart.Columns.Add(this.colLep);
            this.lstPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lstPart.FullRowSelect = true;
            this.lstPart.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstPart.Location = new System.Drawing.Point(2, 128);
            this.lstPart.Name = "lstPart";
            this.lstPart.Size = new System.Drawing.Size(476, 354);
            this.lstPart.TabIndex = 6;
            this.lstPart.View = System.Windows.Forms.View.Details;
            // 
            // colPtno
            // 
            this.colPtno.Text = "부품번호";
            this.colPtno.Width = 182;
            // 
            // colLocno
            // 
            this.colLocno.Text = "LOCATION";
            this.colLocno.Width = 200;
            // 
            // colAvlQty
            // 
            this.colAvlQty.Text = "재고";
            this.colAvlQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAvlQty.Width = 80;
            // 
            // colPrice
            // 
            this.colPrice.Text = "단가";
            this.colPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPrice.Width = 80;
            // 
            // colPtnm
            // 
            this.colPtnm.Text = "부품명";
            this.colPtnm.Width = 420;
            // 
            // colGrade
            // 
            this.colGrade.Text = "수불등급";
            this.colGrade.Width = 73;
            // 
            // colVhc
            // 
            this.colVhc.Text = "차종";
            this.colVhc.Width = 80;
            // 
            // colInvQty
            // 
            this.colInvQty.Text = "총재고";
            this.colInvQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colInvQty.Width = 80;
            // 
            // colLep
            // 
            this.colLep.Text = "L";
            this.colLep.Width = 40;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnPart);
            this._buttons.Controls.Add(this.btnLoc);
            this._buttons.Controls.Add(this.btnAdjust);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnPart
            // 
            this.btnPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPart.Location = new System.Drawing.Point(3, 4);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new System.Drawing.Size(116, 44);
            this.btnPart.TabIndex = 10;
            this.btnPart.Text = "파트";
            this.btnPart.Click += new System.EventHandler(this.OnPart);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(122, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(116, 44);
            this.btnLoc.TabIndex = 11;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
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
            // S324_PartInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S324_PartInfo";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private MobisHaims.Controls.VLabel lblPartCap;
        private System.Windows.Forms.ComboBox cboLep;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblClass;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblCarCap;
        private System.Windows.Forms.ComboBox cboCar;
        private MobisHaims.Controls.VLabel lblWhCap;
        private System.Windows.Forms.ComboBox cboWh;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ListView lstPart;
        private System.Windows.Forms.ColumnHeader colPtno;
        private System.Windows.Forms.ColumnHeader colLocno;
        private System.Windows.Forms.ColumnHeader colAvlQty;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ColumnHeader colPtnm;
        private System.Windows.Forms.ColumnHeader colGrade;
        private System.Windows.Forms.ColumnHeader colVhc;
        private System.Windows.Forms.ColumnHeader colInvQty;
        private System.Windows.Forms.ColumnHeader colLep;
        private System.Windows.Forms.Button btnPart;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Button btnClear;
    }
}
