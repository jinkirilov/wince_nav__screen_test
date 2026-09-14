namespace MobisHaims.Screens
{
    partial class S131_ReserveList
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
            this.lblPartName = new MobisHaims.Controls.VLabel();
            this.lblCntCap = new MobisHaims.Controls.VLabel();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.lblQtyCap = new MobisHaims.Controls.VLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lstRsv = new System.Windows.Forms.ListView();
            this.colVnd = new System.Windows.Forms.ColumnHeader();
            this.colVndNm = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colGrt = new System.Windows.Forms.ColumnHeader();
            this._buttons = new System.Windows.Forms.Panel();
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
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.txtCnt);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Controls.Add(this.lstRsv);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(4, 4);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(80, 38);
            this.lblPartCap.Text = "부번";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.LightGray;
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.Black;
            this.lblPrefix.Location = new System.Drawing.Point(88, 4);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(44, 38);
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(180)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(136, 4);
            this.txtPart.MaxLength = 20;
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(340, 38);
            this.txtPart.TabIndex = 0;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.ForeColor = System.Drawing.Color.Black;
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
            // lblCntCap
            // 
            this.lblCntCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(4, 88);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(80, 34);
            this.lblCntCap.Text = "건수";
            // 
            // txtCnt
            // 
            this.txtCnt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCnt.Location = new System.Drawing.Point(88, 88);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.ReadOnly = true;
            this.txtCnt.Size = new System.Drawing.Size(120, 38);
            this.txtCnt.TabIndex = 1;
            this.txtCnt.Text = "0";
            this.txtCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblQtyCap.BackColor = System.Drawing.Color.White;
            this.lblQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblQtyCap.Location = new System.Drawing.Point(220, 88);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(110, 34);
            this.lblQtyCap.Text = "예약수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(334, 88);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(142, 38);
            this.txtQty.TabIndex = 2;
            this.txtQty.Text = "0";
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lstRsv
            // 
            this.lstRsv.Columns.Add(this.colVnd);
            this.lstRsv.Columns.Add(this.colVndNm);
            this.lstRsv.Columns.Add(this.colQty);
            this.lstRsv.Columns.Add(this.colGrt);
            this.lstRsv.FullRowSelect = true;
            this.lstRsv.Location = new System.Drawing.Point(4, 132);
            this.lstRsv.Name = "lstRsv";
            this.lstRsv.Size = new System.Drawing.Size(472, 348);
            this.lstRsv.TabIndex = 3;
            this.lstRsv.View = System.Windows.Forms.View.Details;
            // 
            // colVnd
            // 
            this.colVnd.Text = "업체";
            this.colVnd.Width = 96;
            // 
            // colVndNm
            // 
            this.colVndNm.Text = "업체명";
            this.colVndNm.Width = 196;
            // 
            // colQty
            // 
            this.colQty.Text = "수량";
            this.colQty.Width = 76;
            // 
            // colGrt
            // 
            this.colGrt.Text = "차량코드";
            this.colGrt.Width = 100;
            // 
            // _buttons
            // 
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(360, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 44);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S131_ReserveList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S131_ReserveList";
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
        private MobisHaims.Controls.VLabel lblPartName;
        private MobisHaims.Controls.VLabel lblCntCap;
        private System.Windows.Forms.TextBox txtCnt;
        private MobisHaims.Controls.VLabel lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ListView lstRsv;
        private System.Windows.Forms.ColumnHeader colVnd;
        private System.Windows.Forms.ColumnHeader colVndNm;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colGrt;
        private System.Windows.Forms.Button btnClear;
    }
}
