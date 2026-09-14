namespace MobisHaims.Screens
{
    partial class S120_InboundClassify
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
            this.lblVenCap = new MobisHaims.Controls.VLabel();
            this.lblVen = new MobisHaims.Controls.VLabel();
            this.lblLocCap = new MobisHaims.Controls.VLabel();
            this.lblLoc = new MobisHaims.Controls.VLabel();
            this.lblCurInvCap = new MobisHaims.Controls.VLabel();
            this.txtCurInv = new System.Windows.Forms.TextBox();
            this.lblRsvCap = new MobisHaims.Controls.VLabel();
            this.txtRsv = new System.Windows.Forms.TextBox();
            this.lblNarCap = new MobisHaims.Controls.VLabel();
            this.txtNar = new System.Windows.Forms.TextBox();
            this.lblCntCap = new MobisHaims.Controls.VLabel();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.lblWsfQtyCap = new MobisHaims.Controls.VLabel();
            this.txtWsfQty = new System.Windows.Forms.TextBox();
            this.lblSaveQtyCap = new MobisHaims.Controls.VLabel();
            this.txtSaveQty = new System.Windows.Forms.TextBox();
            this.chkSale = new System.Windows.Forms.CheckBox();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblVenCap);
            this._fields.Controls.Add(this.lblVen);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.lblLoc);
            this._fields.Controls.Add(this.lblCurInvCap);
            this._fields.Controls.Add(this.txtCurInv);
            this._fields.Controls.Add(this.lblRsvCap);
            this._fields.Controls.Add(this.txtRsv);
            this._fields.Controls.Add(this.lblNarCap);
            this._fields.Controls.Add(this.txtNar);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.txtCnt);
            this._fields.Controls.Add(this.lblWsfQtyCap);
            this._fields.Controls.Add(this.txtWsfQty);
            this._fields.Controls.Add(this.lblSaveQtyCap);
            this._fields.Controls.Add(this.txtSaveQty);
            this._fields.Controls.Add(this.chkSale);
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
            this.lblPartCap.Size = new System.Drawing.Size(60, 38);
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.LightGray;
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.Black;
            this.lblPrefix.Location = new System.Drawing.Point(68, 4);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(44, 38);
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(180)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(116, 4);
            this.txtPart.MaxLength = 20;
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(360, 38);
            this.txtPart.TabIndex = 0;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = MobisHaims.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.ForeColor = System.Drawing.Color.Red;
            this.lblClass.Location = new System.Drawing.Point(4, 46);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(70, 34);
            this.lblClass.Text = "";
            // 
            // lblPartName
            // 
            this.lblPartName.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.Location = new System.Drawing.Point(78, 46);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(398, 34);
            this.lblPartName.Text = "";
            // 
            // lblVenCap
            // 
            this.lblVenCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblVenCap.BackColor = System.Drawing.Color.White;
            this.lblVenCap.ForeColor = System.Drawing.Color.Black;
            this.lblVenCap.Location = new System.Drawing.Point(4, 84);
            this.lblVenCap.Name = "lblVenCap";
            this.lblVenCap.Size = new System.Drawing.Size(70, 34);
            this.lblVenCap.Text = "업체";
            // 
            // lblVen
            // 
            this.lblVen.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblVen.BackColor = System.Drawing.Color.LightGray;
            this.lblVen.ForeColor = System.Drawing.Color.Black;
            this.lblVen.Location = new System.Drawing.Point(78, 84);
            this.lblVen.Name = "lblVen";
            this.lblVen.Size = new System.Drawing.Size(398, 34);
            this.lblVen.Text = "";
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(4, 122);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(70, 36);
            this.lblLocCap.Text = "LOC";
            // 
            // lblLoc
            // 
            this.lblLoc.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblLoc.BackColor = System.Drawing.Color.LightGray;
            this.lblLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblLoc.ForeColor = System.Drawing.Color.Black;
            this.lblLoc.Location = new System.Drawing.Point(78, 122);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(398, 36);
            this.lblLoc.Text = "";
            // 
            // lblCurInvCap
            // 
            this.lblCurInvCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblCurInvCap.BackColor = System.Drawing.Color.White;
            this.lblCurInvCap.ForeColor = System.Drawing.Color.Black;
            this.lblCurInvCap.Location = new System.Drawing.Point(4, 166);
            this.lblCurInvCap.Name = "lblCurInvCap";
            this.lblCurInvCap.Size = new System.Drawing.Size(100, 34);
            this.lblCurInvCap.Text = "현재고";
            // 
            // txtCurInv
            // 
            this.txtCurInv.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCurInv.Location = new System.Drawing.Point(108, 166);
            this.txtCurInv.Name = "txtCurInv";
            this.txtCurInv.ReadOnly = true;
            this.txtCurInv.Size = new System.Drawing.Size(120, 38);
            this.txtCurInv.TabIndex = 1;
            this.txtCurInv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRsvCap
            // 
            this.lblRsvCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblRsvCap.BackColor = System.Drawing.Color.White;
            this.lblRsvCap.ForeColor = System.Drawing.Color.Black;
            this.lblRsvCap.Location = new System.Drawing.Point(240, 166);
            this.lblRsvCap.Name = "lblRsvCap";
            this.lblRsvCap.Size = new System.Drawing.Size(100, 34);
            this.lblRsvCap.Text = "예약";
            // 
            // txtRsv
            // 
            this.txtRsv.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRsv.Location = new System.Drawing.Point(344, 166);
            this.txtRsv.Name = "txtRsv";
            this.txtRsv.ReadOnly = true;
            this.txtRsv.Size = new System.Drawing.Size(132, 38);
            this.txtRsv.TabIndex = 2;
            this.txtRsv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNarCap
            // 
            this.lblNarCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblNarCap.BackColor = System.Drawing.Color.White;
            this.lblNarCap.ForeColor = System.Drawing.Color.Black;
            this.lblNarCap.Location = new System.Drawing.Point(4, 208);
            this.lblNarCap.Name = "lblNarCap";
            this.lblNarCap.Size = new System.Drawing.Size(100, 34);
            this.lblNarCap.Text = "미수령";
            // 
            // txtNar
            // 
            this.txtNar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNar.Location = new System.Drawing.Point(108, 208);
            this.txtNar.Name = "txtNar";
            this.txtNar.ReadOnly = true;
            this.txtNar.Size = new System.Drawing.Size(120, 38);
            this.txtNar.TabIndex = 3;
            this.txtNar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(240, 208);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(100, 34);
            this.lblCntCap.Text = "할당건수";
            // 
            // txtCnt
            // 
            this.txtCnt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCnt.Location = new System.Drawing.Point(344, 208);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.ReadOnly = true;
            this.txtCnt.Size = new System.Drawing.Size(132, 38);
            this.txtCnt.TabIndex = 4;
            this.txtCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblWsfQtyCap
            // 
            this.lblWsfQtyCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblWsfQtyCap.BackColor = System.Drawing.Color.White;
            this.lblWsfQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblWsfQtyCap.Location = new System.Drawing.Point(4, 254);
            this.lblWsfQtyCap.Name = "lblWsfQtyCap";
            this.lblWsfQtyCap.Size = new System.Drawing.Size(100, 40);
            this.lblWsfQtyCap.Text = "할당수량";
            // 
            // txtWsfQty
            // 
            this.txtWsfQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtWsfQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtWsfQty.Location = new System.Drawing.Point(108, 254);
            this.txtWsfQty.MaxLength = 6;
            this.txtWsfQty.Name = "txtWsfQty";
            this.txtWsfQty.Size = new System.Drawing.Size(120, 40);
            this.txtWsfQty.TabIndex = 5;
            this.txtWsfQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWsfQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // lblSaveQtyCap
            // 
            this.lblSaveQtyCap.Align = MobisHaims.Controls.VAlign.MiddleLeft;
            this.lblSaveQtyCap.BackColor = System.Drawing.Color.White;
            this.lblSaveQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblSaveQtyCap.Location = new System.Drawing.Point(240, 254);
            this.lblSaveQtyCap.Name = "lblSaveQtyCap";
            this.lblSaveQtyCap.Size = new System.Drawing.Size(100, 40);
            this.lblSaveQtyCap.Text = "저장수량";
            // 
            // txtSaveQty
            // 
            this.txtSaveQty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSaveQty.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtSaveQty.Location = new System.Drawing.Point(344, 254);
            this.txtSaveQty.Name = "txtSaveQty";
            this.txtSaveQty.ReadOnly = true;
            this.txtSaveQty.Size = new System.Drawing.Size(132, 40);
            this.txtSaveQty.TabIndex = 6;
            this.txtSaveQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSale
            // 
            this.chkSale.Enabled = false;
            this.chkSale.Location = new System.Drawing.Point(4, 302);
            this.chkSale.Name = "chkSale";
            this.chkSale.Size = new System.Drawing.Size(300, 36);
            this.chkSale.TabIndex = 7;
            this.chkSale.Text = "저장 후 예약내역 조회";
            // 
            // _buttons
            // 
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnLoc);
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
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "분류저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoc.Location = new System.Drawing.Point(163, 4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(155, 44);
            this.btnLoc.TabIndex = 9;
            this.btnLoc.Text = "LOC등록";
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
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S120_InboundClassify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S120_InboundClassify";
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
        private MobisHaims.Controls.VLabel lblVenCap;
        private MobisHaims.Controls.VLabel lblVen;
        private MobisHaims.Controls.VLabel lblLocCap;
        private MobisHaims.Controls.VLabel lblLoc;
        private MobisHaims.Controls.VLabel lblCurInvCap;
        private System.Windows.Forms.TextBox txtCurInv;
        private MobisHaims.Controls.VLabel lblRsvCap;
        private System.Windows.Forms.TextBox txtRsv;
        private MobisHaims.Controls.VLabel lblNarCap;
        private System.Windows.Forms.TextBox txtNar;
        private MobisHaims.Controls.VLabel lblCntCap;
        private System.Windows.Forms.TextBox txtCnt;
        private MobisHaims.Controls.VLabel lblWsfQtyCap;
        private System.Windows.Forms.TextBox txtWsfQty;
        private MobisHaims.Controls.VLabel lblSaveQtyCap;
        private System.Windows.Forms.TextBox txtSaveQty;
        private System.Windows.Forms.CheckBox chkSale;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnClear;
    }
}
