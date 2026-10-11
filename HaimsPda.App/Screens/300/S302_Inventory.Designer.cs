namespace HaimsPda.Screens
{
    partial class S302_Inventory
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
            this.lblSeqCap = new HaimsPda.Controls.VLabel();
            this.lblSeq = new HaimsPda.Controls.VLabel();
            this.lblCntCap = new HaimsPda.Controls.VLabel();
            this.lblCnt = new HaimsPda.Controls.VLabel();
            this.lblWhCap = new HaimsPda.Controls.VLabel();
            this.lblWh = new HaimsPda.Controls.VLabel();
            this.lblZoneCap = new HaimsPda.Controls.VLabel();
            this.lblZone = new HaimsPda.Controls.VLabel();
            this.lblGubunCap = new HaimsPda.Controls.VLabel();
            this.lblGubun = new HaimsPda.Controls.VLabel();
            this.lblLocCap = new HaimsPda.Controls.VLabel();
            this.lblLoc = new HaimsPda.Controls.VLabel();
            this.lblPartCap = new HaimsPda.Controls.VLabel();
            this.lblPrefix = new HaimsPda.Controls.VLabel();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.lblClass = new HaimsPda.Controls.VLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPartName = new HaimsPda.Controls.VLabel();
            this.lblOutQtyCap = new HaimsPda.Controls.VLabel();
            this.lblOutQty = new HaimsPda.Controls.VLabel();
            this.lblPriceCap = new HaimsPda.Controls.VLabel();
            this.lblPrice = new HaimsPda.Controls.VLabel();
            this.lblCountCap = new HaimsPda.Controls.VLabel();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblDiffCap = new HaimsPda.Controls.VLabel();
            this.lblDiff = new HaimsPda.Controls.VLabel();
            this.btnPlus1 = new System.Windows.Forms.Button();
            this.btnMinus1 = new System.Windows.Forms.Button();
            this.btnPlus10 = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnPart = new System.Windows.Forms.Button();
            this._buttons = new System.Windows.Forms.Panel();
            this._fields.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _fields
            // 
            this._fields.BackColor = System.Drawing.Color.White;
            this._fields.Controls.Add(this.lblSeqCap);
            this._fields.Controls.Add(this.lblSeq);
            this._fields.Controls.Add(this.lblCntCap);
            this._fields.Controls.Add(this.lblCnt);
            this._fields.Controls.Add(this.lblWhCap);
            this._fields.Controls.Add(this.lblWh);
            this._fields.Controls.Add(this.lblZoneCap);
            this._fields.Controls.Add(this.lblZone);
            this._fields.Controls.Add(this.lblGubunCap);
            this._fields.Controls.Add(this.lblGubun);
            this._fields.Controls.Add(this.lblLocCap);
            this._fields.Controls.Add(this.lblLoc);
            this._fields.Controls.Add(this.lblPartCap);
            this._fields.Controls.Add(this.lblPrefix);
            this._fields.Controls.Add(this.txtPart);
            this._fields.Controls.Add(this.lblClass);
            this._fields.Controls.Add(this.btnSearch);
            this._fields.Controls.Add(this.lblPartName);
            this._fields.Controls.Add(this.lblOutQtyCap);
            this._fields.Controls.Add(this.lblOutQty);
            this._fields.Controls.Add(this.lblPriceCap);
            this._fields.Controls.Add(this.lblPrice);
            this._fields.Controls.Add(this.lblCountCap);
            this._fields.Controls.Add(this.txtCount);
            this._fields.Controls.Add(this.lblDiffCap);
            this._fields.Controls.Add(this.lblDiff);
            this._fields.Controls.Add(this.btnPlus1);
            this._fields.Controls.Add(this.btnMinus1);
            this._fields.Controls.Add(this.btnPlus10);
            this._fields.Controls.Add(this.btnZero);
            this._fields.Controls.Add(this.btnSkip);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(0, 0);
            this._fields.Name = "_fields";
            this._fields.Size = new System.Drawing.Size(480, 484);
            // 
            // lblSeqCap
            // 
            this.lblSeqCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblSeqCap.BackColor = System.Drawing.Color.White;
            this.lblSeqCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblSeqCap.ForeColor = System.Drawing.Color.Black;
            this.lblSeqCap.Location = new System.Drawing.Point(0, 2);
            this.lblSeqCap.Name = "lblSeqCap";
            this.lblSeqCap.Size = new System.Drawing.Size(110, 36);
            this.lblSeqCap.TabIndex = 30;
            this.lblSeqCap.Text = "재물조사번호";
            // 
            // lblSeq
            // 
            this.lblSeq.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblSeq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblSeq.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeq.ForeColor = System.Drawing.Color.Black;
            this.lblSeq.Location = new System.Drawing.Point(114, 2);
            this.lblSeq.Name = "lblSeq";
            this.lblSeq.Size = new System.Drawing.Size(150, 36);
            this.lblSeq.TabIndex = 31;
            // 
            // lblCntCap
            // 
            this.lblCntCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCntCap.BackColor = System.Drawing.Color.White;
            this.lblCntCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCntCap.ForeColor = System.Drawing.Color.Black;
            this.lblCntCap.Location = new System.Drawing.Point(268, 2);
            this.lblCntCap.Name = "lblCntCap";
            this.lblCntCap.Size = new System.Drawing.Size(80, 36);
            this.lblCntCap.TabIndex = 32;
            this.lblCntCap.Text = "ITEM수";
            // 
            // lblCnt
            // 
            this.lblCnt.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblCnt.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCnt.Location = new System.Drawing.Point(352, 2);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(124, 36);
            this.lblCnt.TabIndex = 33;
            // 
            // lblWhCap
            // 
            this.lblWhCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblWhCap.BackColor = System.Drawing.Color.White;
            this.lblWhCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblWhCap.ForeColor = System.Drawing.Color.Black;
            this.lblWhCap.Location = new System.Drawing.Point(0, 42);
            this.lblWhCap.Name = "lblWhCap";
            this.lblWhCap.Size = new System.Drawing.Size(56, 36);
            this.lblWhCap.TabIndex = 34;
            this.lblWhCap.Text = "창고";
            // 
            // lblWh
            // 
            this.lblWh.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblWh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblWh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblWh.ForeColor = System.Drawing.Color.Black;
            this.lblWh.Location = new System.Drawing.Point(60, 42);
            this.lblWh.Name = "lblWh";
            this.lblWh.Size = new System.Drawing.Size(50, 36);
            this.lblWh.TabIndex = 35;
            // 
            // lblZoneCap
            // 
            this.lblZoneCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblZoneCap.BackColor = System.Drawing.Color.White;
            this.lblZoneCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblZoneCap.ForeColor = System.Drawing.Color.Black;
            this.lblZoneCap.Location = new System.Drawing.Point(114, 42);
            this.lblZoneCap.Name = "lblZoneCap";
            this.lblZoneCap.Size = new System.Drawing.Size(56, 36);
            this.lblZoneCap.TabIndex = 36;
            this.lblZoneCap.Text = "Zone";
            // 
            // lblZone
            // 
            this.lblZone.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblZone.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblZone.ForeColor = System.Drawing.Color.Black;
            this.lblZone.Location = new System.Drawing.Point(174, 42);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(90, 36);
            this.lblZone.TabIndex = 37;
            // 
            // lblGubunCap
            // 
            this.lblGubunCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblGubunCap.BackColor = System.Drawing.Color.White;
            this.lblGubunCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblGubunCap.ForeColor = System.Drawing.Color.Black;
            this.lblGubunCap.Location = new System.Drawing.Point(268, 42);
            this.lblGubunCap.Name = "lblGubunCap";
            this.lblGubunCap.Size = new System.Drawing.Size(80, 36);
            this.lblGubunCap.TabIndex = 38;
            this.lblGubunCap.Text = "구분";
            // 
            // lblGubun
            // 
            this.lblGubun.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblGubun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblGubun.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblGubun.ForeColor = System.Drawing.Color.Black;
            this.lblGubun.Location = new System.Drawing.Point(352, 42);
            this.lblGubun.Name = "lblGubun";
            this.lblGubun.Size = new System.Drawing.Size(124, 36);
            this.lblGubun.TabIndex = 39;
            // 
            // lblLocCap
            // 
            this.lblLocCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblLocCap.BackColor = System.Drawing.Color.White;
            this.lblLocCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblLocCap.ForeColor = System.Drawing.Color.Black;
            this.lblLocCap.Location = new System.Drawing.Point(0, 82);
            this.lblLocCap.Name = "lblLocCap";
            this.lblLocCap.Size = new System.Drawing.Size(56, 40);
            this.lblLocCap.TabIndex = 40;
            this.lblLocCap.Text = "LOC";
            // 
            // lblLoc
            // 
            this.lblLoc.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblLoc.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblLoc.ForeColor = System.Drawing.Color.Black;
            this.lblLoc.Location = new System.Drawing.Point(60, 82);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(416, 40);
            this.lblLoc.TabIndex = 41;
            // 
            // lblPartCap
            // 
            this.lblPartCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPartCap.BackColor = System.Drawing.Color.White;
            this.lblPartCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartCap.ForeColor = System.Drawing.Color.Black;
            this.lblPartCap.Location = new System.Drawing.Point(0, 130);
            this.lblPartCap.Name = "lblPartCap";
            this.lblPartCap.Size = new System.Drawing.Size(56, 40);
            this.lblPartCap.TabIndex = 42;
            this.lblPartCap.Text = "부품";
            // 
            // lblPrefix
            // 
            this.lblPrefix.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(107)))), ((int)(((byte)(176)))));
            this.lblPrefix.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrefix.ForeColor = System.Drawing.Color.White;
            this.lblPrefix.Location = new System.Drawing.Point(60, 131);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(24, 40);
            this.lblPrefix.TabIndex = 43;
            this.lblPrefix.Text = "H";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtPart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtPart.Location = new System.Drawing.Point(88, 128);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(250, 46);
            this.txtPart.TabIndex = 0;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPartKeyDown);
            // 
            // lblClass
            // 
            this.lblClass.Align = HaimsPda.Controls.VAlign.MiddleCenter;
            this.lblClass.BackColor = System.Drawing.Color.LightGray;
            this.lblClass.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblClass.Location = new System.Drawing.Point(342, 131);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(54, 40);
            this.lblClass.TabIndex = 44;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(400, 130);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // lblPartName
            // 
            this.lblPartName.Align = HaimsPda.Controls.VAlign.MiddleLeft;
            this.lblPartName.BackColor = System.Drawing.Color.LightGray;
            this.lblPartName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPartName.Location = new System.Drawing.Point(4, 178);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(472, 34);
            this.lblPartName.TabIndex = 45;
            // 
            // lblOutQtyCap
            // 
            this.lblOutQtyCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblOutQtyCap.BackColor = System.Drawing.Color.White;
            this.lblOutQtyCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblOutQtyCap.ForeColor = System.Drawing.Color.Black;
            this.lblOutQtyCap.Location = new System.Drawing.Point(0, 218);
            this.lblOutQtyCap.Name = "lblOutQtyCap";
            this.lblOutQtyCap.Size = new System.Drawing.Size(90, 40);
            this.lblOutQtyCap.TabIndex = 46;
            this.lblOutQtyCap.Text = "출고대기";
            // 
            // lblOutQty
            // 
            this.lblOutQty.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblOutQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblOutQty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblOutQty.ForeColor = System.Drawing.Color.Black;
            this.lblOutQty.Location = new System.Drawing.Point(94, 218);
            this.lblOutQty.Name = "lblOutQty";
            this.lblOutQty.Size = new System.Drawing.Size(140, 40);
            this.lblOutQty.TabIndex = 47;
            // 
            // lblPriceCap
            // 
            this.lblPriceCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPriceCap.BackColor = System.Drawing.Color.White;
            this.lblPriceCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblPriceCap.ForeColor = System.Drawing.Color.Black;
            this.lblPriceCap.Location = new System.Drawing.Point(238, 218);
            this.lblPriceCap.Name = "lblPriceCap";
            this.lblPriceCap.Size = new System.Drawing.Size(70, 40);
            this.lblPriceCap.TabIndex = 48;
            this.lblPriceCap.Text = "단가";
            // 
            // lblPrice
            // 
            this.lblPrice.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblPrice.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.Black;
            this.lblPrice.Location = new System.Drawing.Point(312, 218);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(164, 40);
            this.lblPrice.TabIndex = 49;
            // 
            // lblCountCap
            // 
            this.lblCountCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblCountCap.BackColor = System.Drawing.Color.White;
            this.lblCountCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblCountCap.ForeColor = System.Drawing.Color.Black;
            this.lblCountCap.Location = new System.Drawing.Point(0, 266);
            this.lblCountCap.Name = "lblCountCap";
            this.lblCountCap.Size = new System.Drawing.Size(90, 46);
            this.lblCountCap.TabIndex = 50;
            this.lblCountCap.Text = "실셈수량";
            // 
            // txtCount
            // 
            this.txtCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(190)))));
            this.txtCount.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCount.Location = new System.Drawing.Point(94, 264);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(140, 46);
            this.txtCount.TabIndex = 2;
            this.txtCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnCountKeyDown);
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnCountKeyPress);
            // 
            // lblDiffCap
            // 
            this.lblDiffCap.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblDiffCap.BackColor = System.Drawing.Color.White;
            this.lblDiffCap.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular);
            this.lblDiffCap.ForeColor = System.Drawing.Color.Black;
            this.lblDiffCap.Location = new System.Drawing.Point(238, 266);
            this.lblDiffCap.Name = "lblDiffCap";
            this.lblDiffCap.Size = new System.Drawing.Size(70, 46);
            this.lblDiffCap.TabIndex = 51;
            this.lblDiffCap.Text = "차이";
            // 
            // lblDiff
            // 
            this.lblDiff.Align = HaimsPda.Controls.VAlign.MiddleRight;
            this.lblDiff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDiff.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.lblDiff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblDiff.Location = new System.Drawing.Point(312, 266);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(164, 46);
            this.lblDiff.TabIndex = 52;
            // 
            // btnPlus1
            // 
            this.btnPlus1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPlus1.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnPlus1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPlus1.Location = new System.Drawing.Point(4, 322);
            this.btnPlus1.Name = "btnPlus1";
            this.btnPlus1.Size = new System.Drawing.Size(112, 56);
            this.btnPlus1.TabIndex = 3;
            this.btnPlus1.Text = "+1";
            this.btnPlus1.Click += new System.EventHandler(this.OnPlus1);
            // 
            // btnMinus1
            // 
            this.btnMinus1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnMinus1.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnMinus1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMinus1.Location = new System.Drawing.Point(122, 322);
            this.btnMinus1.Name = "btnMinus1";
            this.btnMinus1.Size = new System.Drawing.Size(112, 56);
            this.btnMinus1.TabIndex = 4;
            this.btnMinus1.Text = "-1";
            this.btnMinus1.Click += new System.EventHandler(this.OnMinus1);
            // 
            // btnPlus10
            // 
            this.btnPlus10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPlus10.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
            this.btnPlus10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPlus10.Location = new System.Drawing.Point(240, 322);
            this.btnPlus10.Name = "btnPlus10";
            this.btnPlus10.Size = new System.Drawing.Size(112, 56);
            this.btnPlus10.TabIndex = 5;
            this.btnPlus10.Text = "+10";
            this.btnPlus10.Click += new System.EventHandler(this.OnPlus10);
            // 
            // btnZero
            // 
            this.btnZero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnZero.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnZero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnZero.Location = new System.Drawing.Point(358, 322);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(118, 56);
            this.btnZero.TabIndex = 6;
            this.btnZero.Text = "Clear";
            this.btnZero.Click += new System.EventHandler(this.OnZero);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnSkip.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnSkip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSkip.Location = new System.Drawing.Point(4, 386);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(472, 50);
            this.btnSkip.TabIndex = 7;
            this.btnSkip.Text = "SKIP (다음 품목)";
            this.btnSkip.Click += new System.EventHandler(this.OnSkip);
            // 
            // _buttons
            // 
            this._buttons.BackColor = System.Drawing.Color.White;
            this._buttons.Controls.Add(this.btnConfirm);
            this._buttons.Controls.Add(this.btnPart);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttons.Location = new System.Drawing.Point(0, 484);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(480, 52);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.btnConfirm.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfirm.Location = new System.Drawing.Point(3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(236, 44);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "확인(저장)";
            this.btnConfirm.Click += new System.EventHandler(this.OnConfirm);
            // 
            // btnPart
            // 
            this.btnPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.btnPart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnPart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPart.Location = new System.Drawing.Point(242, 4);
            this.btnPart.Name = "btnPart";
            this.btnPart.Size = new System.Drawing.Size(235, 44);
            this.btnPart.TabIndex = 11;
            this.btnPart.Text = "파트";
            this.btnPart.Click += new System.EventHandler(this.OnPart);
            // 
            // S302_Inventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._fields);
            this.Controls.Add(this._buttons);
            this.Name = "S302_Inventory";
            this.Size = new System.Drawing.Size(480, 536);
            this._fields.ResumeLayout(false);
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _fields;
        private System.Windows.Forms.Panel _buttons;
        private HaimsPda.Controls.VLabel lblSeqCap;
        private HaimsPda.Controls.VLabel lblSeq;
        private HaimsPda.Controls.VLabel lblCntCap;
        private HaimsPda.Controls.VLabel lblCnt;
        private HaimsPda.Controls.VLabel lblWhCap;
        private HaimsPda.Controls.VLabel lblWh;
        private HaimsPda.Controls.VLabel lblZoneCap;
        private HaimsPda.Controls.VLabel lblZone;
        private HaimsPda.Controls.VLabel lblGubunCap;
        private HaimsPda.Controls.VLabel lblGubun;
        private HaimsPda.Controls.VLabel lblLocCap;
        private HaimsPda.Controls.VLabel lblLoc;
        private HaimsPda.Controls.VLabel lblPartCap;
        private HaimsPda.Controls.VLabel lblPrefix;
        private System.Windows.Forms.TextBox txtPart;
        private HaimsPda.Controls.VLabel lblClass;
        private System.Windows.Forms.Button btnSearch;
        private HaimsPda.Controls.VLabel lblPartName;
        private HaimsPda.Controls.VLabel lblOutQtyCap;
        private HaimsPda.Controls.VLabel lblOutQty;
        private HaimsPda.Controls.VLabel lblPriceCap;
        private HaimsPda.Controls.VLabel lblPrice;
        private HaimsPda.Controls.VLabel lblCountCap;
        private System.Windows.Forms.TextBox txtCount;
        private HaimsPda.Controls.VLabel lblDiffCap;
        private HaimsPda.Controls.VLabel lblDiff;
        private System.Windows.Forms.Button btnPlus1;
        private System.Windows.Forms.Button btnMinus1;
        private System.Windows.Forms.Button btnPlus10;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnPart;
    }
}
