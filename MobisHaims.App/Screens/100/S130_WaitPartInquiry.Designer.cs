namespace MobisHaims.Screens
{
    partial class S130_WaitPartInquiry
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
            this.btnMode = new System.Windows.Forms.Button();
            this.lblPartCap = new MobisHaims.Controls.VLabel();
            this.lblPrefix = new MobisHaims.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new MobisHaims.Controls.VLabel();
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblQtyCap = new MobisHaims.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblCntCap = new MobisHaims.Controls.VLabel();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.lstWait = new System.Windows.Forms.ListView();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colVchno = new System.Windows.Forms.ColumnHeader();
            this.colCase = new System.Windows.Forms.ColumnHeader();
            this.colLoc = new System.Windows.Forms.ColumnHeader();
            this.colStat = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.btnMode);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.txtCnt);
            this._fields.Controls.Add(this.lstWait);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // btnMode
            // 
            this.btnMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnMode.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMode.Location = new System.Drawing.Point(4, 4);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(120, 38);
            this.btnMode.TabIndex = 0;
            this.btnMode.Text = "입고대기";
            this.btnMode.Click += new System.EventHandler(this.OnModeToggle);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(132, 4);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(60, 38);
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.LightGray;
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.Black;
            this.lblPrefix.Location = new System.Drawing.Point(196, 4);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(44, 38);
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(180)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(244, 4);
            this.txtPart.MaxLength = 20;
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(232, 38);
            this.txtPart.TabIndex = 1;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.ForeColor = System.Drawing.Color.Red;
            this.lblClass.Location = new System.Drawing.Point(4, 48);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(70, 34);
            this.lblClass.Text = "";
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(78, 48);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(398, 34);
            this.lblPartName.Text = "";
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(4, 88);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(110, 34);
            this.lblQtyCap.Text = "할당수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(118, 88);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(110, 38);
            this.txtQty.TabIndex = 2;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(240, 88);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(110, 34);
            this.lblCntCap.Text = "할당건수";
            // 
            // txtCnt
            // 
            this.txtCnt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCnt.Location = new System.Drawing.Point(354, 88);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.ReadOnly = true;
            this.txtCnt.Size = new System.Drawing.Size(122, 38);
            this.txtCnt.TabIndex = 3;
            this.txtCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lstWait
            // 
            this.lstWait.Columns.Add(this.colDate);
            this.lstWait.Columns.Add(this.colQty);
            this.lstWait.Columns.Add(this.colVchno);
            this.lstWait.Columns.Add(this.colCase);
            this.lstWait.Columns.Add(this.colLoc);
            this.lstWait.Columns.Add(this.colStat);
            this.lstWait.FullRowSelect = true;
            this.lstWait.Location = new System.Drawing.Point(4, 132);
            this.lstWait.Name = "lstWait";
            this.lstWait.Size = new System.Drawing.Size(472, 348);
            this.lstWait.TabIndex = 4;
            this.lstWait.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "일자";
            this.colDate.Width = 92;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.Width = 56;
            // 
            // colVchno
            // 
            this.colVchno.Text = "할당";
            this.colVchno.Width = 96;
            // 
            // colCase
            // 
            this.colCase.Text = "CASE";
            this.colCase.Width = 96;
            // 
            // colLoc
            // 
            this.colLoc.Text = "LOC";
            this.colLoc.Width = 86;
            // 
            // colStat
            // 
            this.colStat.Text = "상태";
            this.colStat.Width = 60;
            // 
            // _buttons
            // 
            this._buttons.Controls.Add(this.btnStock);
            this._buttons.Controls.Add(this.btnLoc);
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
            this.btnStock.Location = new System.Drawing.Point(4, 4);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(155, 44);
            this.btnStock.TabIndex = 5;
            this.btnStock.Text = "재고";
            this.btnStock.Click += new System.EventHandler(this.OnStock);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(163, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(155, 44);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(321, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(155, 44);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S130_WaitPartInquiry
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S130_WaitPartInquiry";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private System.Windows.Forms.Button btnMode;
        private MobisHaims.Controls.VLabel lblPartCap;
        private MobisHaims.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private MobisHaims.Controls.VLabel lblClass;
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private MobisHaims.Controls.VLabel lblCntCap;
        private System.Windows.Forms.TextBox txtCnt;
        private System.Windows.Forms.ListView lstWait;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colVchno;
        private System.Windows.Forms.ColumnHeader colCase;
        private System.Windows.Forms.ColumnHeader colLoc;
        private System.Windows.Forms.ColumnHeader colStat;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnClear;
    }
}
