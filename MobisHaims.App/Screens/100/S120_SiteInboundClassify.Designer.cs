namespace MobisHaims.Screens
{
    partial class S120_SiteInboundClassify
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
            this.lblPartCap = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblReserveCap = new System.Windows.Forms.Label();
            this.lblAssignCap = new System.Windows.Forms.Label();
            this.lblReserve = new System.Windows.Forms.Label();
            this.lblAssign = new System.Windows.Forms.Label();
            this.lblLocCap = new System.Windows.Forms.Label();
            this.lblLoc = new System.Windows.Forms.Label();
            this.lblAssignCntCap = new System.Windows.Forms.Label();
            this.lblAssignCnt = new System.Windows.Forms.Label();
            this.lblCurStockCap = new System.Windows.Forms.Label();
            this.lblCurStock = new System.Windows.Forms.Label();
            this.lblQtyCap = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblNotRecvCap = new System.Windows.Forms.Label();
            this.lblNotRecv = new System.Windows.Forms.Label();
            this._buttons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnNotRecv = new System.Windows.Forms.Button();
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
            this._fields.Controls.Add(this.lblReserveCap);
            this._fields.Controls.Add(this.lblAssignCap);
            this._fields.Controls.Add(this.lblReserve);
            this._fields.Controls.Add(this.lblAssign);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.lblLoc);
            this._fields.Controls.Add(this.lblAssignCntCap);
            this._fields.Controls.Add(this.lblAssignCnt);
            this._fields.Controls.Add(this.lblCurStockCap);
            this._fields.Controls.Add(this.lblCurStock);
            this._fields.Controls.Add(this.lblQtyCap);
            this._fields.Controls.Add(this.txtQty);
            this._fields.Controls.Add(this.lblNotRecvCap);
            this._fields.Controls.Add(this.lblNotRecv);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 490);
            // 
            // lblPartCap
            // 
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.Location = new System.Drawing.Point(4, 6);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(36, 24);
            this.lblPartCap.Text = "부번";
            // 
            // lblPrefix
            // 
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(42, 6);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(22, 24);
            this.lblPrefix.Text = "H";
            this.lblPrefix.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(180)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.txtPart.Location = new System.Drawing.Point(66, 6);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(408, 40);
            this.txtPart.TabIndex = 2;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblReserveCap
            // 
            this.lblReserveCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblReserveCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblReserveCap.Location = new System.Drawing.Point(25, 49);
            this.lblReserveCap.Name = "lblReserveCap";
            this.lblReserveCap.Size = new System.Drawing.Size(150, 22);
            this.lblReserveCap.Text = "예약(긴급)";
            // 
            // lblAssignCap
            // 
            this.lblAssignCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAssignCap.Location = new System.Drawing.Point(249, 49);
            this.lblAssignCap.Name = "lblAssignCap";
            this.lblAssignCap.Size = new System.Drawing.Size(119, 22);
            this.lblAssignCap.Text = "할당수량";
            // 
            // lblReserve
            // 
            this.lblReserve.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblReserve.Location = new System.Drawing.Point(0, 49);
            this.lblReserve.Name = "lblReserve";
            this.lblReserve.Size = new System.Drawing.Size(224, 30);
            this.lblReserve.Text = "0";
            this.lblReserve.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAssign
            // 
            this.lblAssign.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblAssign.Location = new System.Drawing.Point(374, 41);
            this.lblAssign.Name = "lblAssign";
            this.lblAssign.Size = new System.Drawing.Size(98, 30);
            this.lblAssign.Text = "0";
            this.lblAssign.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.Location = new System.Drawing.Point(8, 104);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(40, 22);
            this.lblLocCap.Text = "LOC";
            // 
            // lblLoc
            // 
            this.lblLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLoc.Location = new System.Drawing.Point(50, 104);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(210, 22);
            // 
            // lblAssignCntCap
            // 
            this.lblAssignCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAssignCntCap.Location = new System.Drawing.Point(25, 249);
            this.lblAssignCntCap.Name = "lblAssignCntCap";
            this.lblAssignCntCap.Size = new System.Drawing.Size(161, 37);
            this.lblAssignCntCap.Text = "할당건수";
            this.lblAssignCntCap.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAssignCnt
            // 
            this.lblAssignCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblAssignCnt.Location = new System.Drawing.Point(412, 104);
            this.lblAssignCnt.Name = "lblAssignCnt";
            this.lblAssignCnt.Size = new System.Drawing.Size(62, 22);
            this.lblAssignCnt.Text = "0";
            this.lblAssignCnt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCurStockCap
            // 
            this.lblCurStockCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCurStockCap.Location = new System.Drawing.Point(8, 146);
            this.lblCurStockCap.Name = "lblCurStockCap";
            this.lblCurStockCap.Size = new System.Drawing.Size(54, 24);
            this.lblCurStockCap.Text = "현재고:";
            // 
            // lblCurStock
            // 
            this.lblCurStock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCurStock.Location = new System.Drawing.Point(64, 146);
            this.lblCurStock.Name = "lblCurStock";
            this.lblCurStock.Size = new System.Drawing.Size(168, 24);
            this.lblCurStock.Text = "0";
            this.lblCurStock.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblQtyCap
            // 
            this.lblQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblQtyCap.Location = new System.Drawing.Point(240, 146);
            this.lblQtyCap.Name = "lblQtyCap";
            this.lblQtyCap.Size = new System.Drawing.Size(44, 24);
            this.lblQtyCap.Text = "수량";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtQty.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(384, 144);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(90, 49);
            this.txtQty.TabIndex = 14;
            this.txtQty.Text = "0";
            // 
            // lblNotRecvCap
            // 
            this.lblNotRecvCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblNotRecvCap.Location = new System.Drawing.Point(8, 178);
            this.lblNotRecvCap.Name = "lblNotRecvCap";
            this.lblNotRecvCap.Size = new System.Drawing.Size(54, 22);
            this.lblNotRecvCap.Text = "미수령:";
            // 
            // lblNotRecv
            // 
            this.lblNotRecv.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblNotRecv.Location = new System.Drawing.Point(64, 178);
            this.lblNotRecv.Name = "lblNotRecv";
            this.lblNotRecv.Size = new System.Drawing.Size(168, 22);
            this.lblNotRecv.Text = "0";
            this.lblNotRecv.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnSave);
            this._buttons.Controls.Add(this.btnSort);
            this._buttons.Controls.Add(this.btnLoc);
            this._buttons.Controls.Add(this.btnNotRecv);
            this._buttons.Controls.Add(this.btnClear);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 490);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 38);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnSort.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSort.ForeColor = System.Drawing.Color.White;
            this.btnSort.Location = new System.Drawing.Point(98, 3);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(92, 32);
            this.btnSort.TabIndex = 1;
            this.btnSort.Text = "정렬";
            this.btnSort.Click += new System.EventHandler(this.OnSort);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnLoc.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(193, 3);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(92, 32);
            this.btnLoc.TabIndex = 2;
            this.btnLoc.Text = "LOC";
            this.btnLoc.Click += new System.EventHandler(this.OnLoc);
            // 
            // btnNotRecv
            // 
            this.btnNotRecv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnNotRecv.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnNotRecv.ForeColor = System.Drawing.Color.White;
            this.btnNotRecv.Location = new System.Drawing.Point(288, 3);
            this.btnNotRecv.Name = "btnNotRecv";
            this.btnNotRecv.Size = new System.Drawing.Size(92, 32);
            this.btnNotRecv.TabIndex = 3;
            this.btnNotRecv.Text = "미수령";
            this.btnNotRecv.Click += new System.EventHandler(this.OnNotRecv);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(158)))), ((int)(((byte)(214)))));
            this.btnClear.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(383, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(92, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "지움";
            this.btnClear.Click += new System.EventHandler(this.OnClear);
            // 
            // S120_SiteInboundClassify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S120_SiteInboundClassify";
            this.Size = new System.Drawing.Size(480, 528);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private System.Windows.Forms.Label lblPartCap;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private System.Windows.Forms.Label lblReserveCap;
        private System.Windows.Forms.Label lblAssignCap;
        private System.Windows.Forms.Label lblReserve;
        private System.Windows.Forms.Label lblAssign;
        private System.Windows.Forms.Label lblLocCap;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Label lblAssignCntCap;
        private System.Windows.Forms.Label lblAssignCnt;
        private System.Windows.Forms.Label lblCurStockCap;
        private System.Windows.Forms.Label lblCurStock;
        private System.Windows.Forms.Label lblQtyCap;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblNotRecvCap;
        private System.Windows.Forms.Label lblNotRecv;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnNotRecv;
        private System.Windows.Forms.Button btnClear;
    }
}
