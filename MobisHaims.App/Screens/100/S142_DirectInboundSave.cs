using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [142] 직입고저장 : 업체 선택 -> 부번 조회 -> 목록에 담기(최대 5) -> 직입고 저장
    // 원본 웹화면 : /ui/ws/plus/PL142_W01.xml
    //
    //   fn_OnLoad : 창고/계열/업체 콤보 채우기
    //   fn_Search       (PL142_W01_S01 + PL140_W01_S04)
    //   fn_VapSearch    (PL142_W01_S02)  업체 여신정보
    //   InsertList                        목록에 한 줄 추가
    //   fn_Save         (PL142_W01_P01)  -> fn_SaveGbn (PL142_W01_U01)
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S142_DirectInboundSave.Designer.cs 에서 관리한다.
    public sealed partial class S142_DirectInboundSave : ScreenBase
    {
        private const string MP_OK = "MP101";      // 정상 조회되었습니다
        private const string MP_SAVED = "MP102";   // 정상 저장되었습니다
        private const string MP_NOPART = "MP303";  // 부품번호를 입력하십시오
        private const string MP_DUP = "MP316";     // 처리된 부품번호입니다
        private const string MP_NOQTY = "MP613";   // 수량을 입력하십시오
        private const string MP_NOSEL = "MP545";   // 선택된 데이터가 없습니다

        /// <summary>목록에 담긴 DirectInboundRow</summary>
        private readonly ArrayList _rows = new ArrayList();

        /// <summary>직전 조회 결과(hidGrd1 대응). InsertList 가 이걸 쓴다.</summary>
        private DirectPartInfo _part;

        /// <summary>선택된 업체의 여신정보(hidGrd2 대응). 저장 헤더에 실린다.</summary>
        private VapInfo _vap;

        private bool _busy;

        public override int ScreenNo { get { return ScreenId.DirectInboundSave; } }
        public override string ScreenName { get { return "직입고저장"; } }

        public S142_DirectInboundSave()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        // ------------------------------------------------------------------
        // 진입 : 콤보 채우기 (fn_OnLoad)
        // ------------------------------------------------------------------
        public override void OnEnter(NavArgs args)
        {
            ClearAll();
            LoadCombos();
        }

        private void LoadCombos()
        {
            // 업체는 로그인 때 받아 둔 공통코드(ds_ven)에서 바로 채운다 (fn_setCustInfo)
            FillVendorMain();

            Begin("초기 정보를 불러오는 중...");
            Async.Run(this,
                delegate
                {
                    ArrayList[] r = new ArrayList[2];
                    r[0] = InboundService.GetWarehouses();
                    r[1] = DirectInboundService.GetLeps();
                    return r;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    ArrayList[] a = (ArrayList[])r;

                    FillCombo(cboWhs, a[0]);
                    Select(cboWhs, "M");            // fn_Init : selWHS = "M"

                    FillCombo(cboLep, a[1]);
                    if (cboLep.Items.Count > 0) cboLep.SelectedIndex = 0;

                    End("업체를 선택하세요.", MsgLevel.Info);
                    cboVndMn.Focus();
                });
        }

        private void FillVendorMain()
        {
            cboVndMn.Items.Clear();
            cboVndSb.Items.Clear();

            ArrayList mains = CommonCache.MainVendors();
            for (int i = 0; i < mains.Count; i++)
                cboVndMn.Items.Add(mains[i]);

            if (mains.Count == 0)
                Msg("업체 정보가 없습니다. 다시 로그인하십시오.", MsgLevel.Warn);
        }

        private static void FillCombo(ComboBox cbo, ArrayList items)
        {
            cbo.Items.Clear();
            for (int i = 0; i < items.Count; i++) cbo.Items.Add(items[i]);
        }

        private static void Select(ComboBox cbo, string value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (cbo.Items[i].ToString() == value) { cbo.SelectedIndex = i; return; }
            }
            if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
        }

        // ------------------------------------------------------------------
        // 업체 선택 (fn_setCustInfo2 -> fn_VapSearch)
        // ------------------------------------------------------------------
        private VendorInfo MainVendor
        {
            get { return (VendorInfo)cboVndMn.SelectedItem; }
        }

        private VendorInfo SubVendor
        {
            get { return (VendorInfo)cboVndSb.SelectedItem; }
        }

        private void OnVndMnChanged(object sender, EventArgs e)
        {
            if (_busy) return;

            VendorInfo mn = MainVendor;
            cboVndSb.Items.Clear();
            _vap = null;
            if (mn == null) return;

            ArrayList subs = CommonCache.SubVendors(mn.VndMn);
            for (int i = 0; i < subs.Count; i++) cboVndSb.Items.Add(subs[i]);

            // 웹도 첫 건을 자동 선택하고 곧바로 여신조회를 한다
            if (cboVndSb.Items.Count > 0) cboVndSb.SelectedIndex = 0;
        }

        private void OnVndSbChanged(object sender, EventArgs e)
        {
            if (_busy) return;
            SearchVap();
        }

        private void SearchVap()
        {
            VendorInfo mn = MainVendor;
            VendorInfo sb = SubVendor;
            if (mn == null || sb == null) return;

            string vndMn = mn.VndMn;
            string vndSb = sb.VndSb;

            _vap = null;
            Begin("업체 정보 조회중...");

            Async.Run(this,
                delegate { return DirectInboundService.SearchVap(vndMn, vndSb); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    _vap = (VapInfo)r;
                    if (_vap == null)
                    {
                        ReportText("해당 부품정보가 없습니다.", MsgLevel.Warn);
                        return;
                    }

                    End("부번을 스캔/입력하세요.", MsgLevel.Info);
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 부번 조회 (fn_SearchList -> fn_Search)
        // ------------------------------------------------------------------
        public override void OnScan(MobisHaims.Devices.ScanData data)
        {
            txtPart.Text = PartNo.Display(data.Text);
            SearchPart();
        }

        private void OnPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; SearchPart(); }
        }

        private void SearchPart()
        {
            if (_busy) return;

            string key = PartNo.Key(txtPart.Text);
            if (key.Length == 0)
            {
                Report(MP_NOPART, "부품번호를 입력하십시오.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (MainVendor == null)
            {
                ReportText("업체를 선택하세요.", MsgLevel.Warn);
                cboVndMn.Focus();
                return;
            }
            if (_vap == null)
            {
                ReportText("업체를 다시 선택하십시오.", MsgLevel.Warn);
                cboVndMn.Focus();
                return;
            }

            string lep = LepValue;

            // fn_SearchList : 이미 담긴 (계열 + 부번) 이면 다시 담지 않는다
            for (int i = 0; i < _rows.Count; i++)
            {
                DirectInboundRow row = (DirectInboundRow)_rows[i];
                if (row["LEP"] == lep && row["TRS_PTNO"] == key)
                {
                    Report(MP_DUP, "이미 처리된 부품번호입니다.", MsgLevel.Warn);
                    txtPart.Focus();
                    txtPart.SelectAll();
                    return;
                }
            }

            txtPart.Text = PartNo.Display(txtPart.Text);

            string ptno = key;
            string whs = WhsValue;

            VendorInfo mn = MainVendor;
            VendorInfo sb = SubVendor;
            string vndMn = mn.VndMn;
            string vndSb = (sb == null) ? "" : sb.VndSb;

            _part = null;
            Begin("조회중...");

            Async.Run(this,
                delegate { return DirectInboundService.Search(vndMn, vndSb, lep, ptno, whs); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    _part = (DirectPartInfo)r;
                    if (_part == null)
                    {
                        ReportText("해당 부품정보가 없습니다.", MsgLevel.Warn);
                        txtPart.Focus();
                        txtPart.SelectAll();
                        return;
                    }

                    lblPartName.Text = _part.PtNm;
                    txtPrice.Text = _part.SalPrc;   // fn_Search_After : TRS_SAL_PRC
                    txtQty.Text = "";

                    End(CommonCache.Msg(MP_OK, "정상 조회되었습니다."), MsgLevel.Success);
                    txtQty.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 목록에 담기 (InsertList)
        // ------------------------------------------------------------------
        private void OnPriceKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.Handled = true; txtQty.Focus(); }
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            InsertRow();
        }

        private void InsertRow()
        {
            if (_busy) return;

            if (PartNo.Key(txtPart.Text).Length == 0)
            {
                Report(MP_NOPART, "부품번호를 입력하십시오.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (_part == null)
            {
                ReportText("부품을 조회하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }

            string price = txtPrice.Text.Trim();
            if (price.Length == 0 || DirectInboundRow.ParseInt(price) == 0)
            {
                ReportText("단가에 공백이나 0을 입력할 수 없습니다.", MsgLevel.Warn);
                txtQty.Focus();
                return;
            }

            string qty = txtQty.Text.Trim();
            if (qty.Length == 0 || DirectInboundRow.ParseInt(qty) == 0)
            {
                Report(MP_NOQTY, "수량을 입력하십시오.", MsgLevel.Warn);
                txtQty.Focus();
                return;
            }

            if (_rows.Count >= DirectInboundService.MaxRows)
            {
                ReportText("일괄입고는 최대 " + DirectInboundService.MaxRows + "까지 가능합니다.", MsgLevel.Warn);
                txtQty.Focus();
                return;
            }

            _rows.Add(DirectInboundRow.From(_part, WhsValue, qty, price, chkAdjust.Checked));
            RefreshGrid();

            // 다음 부번 입력을 위해 입력칸을 비운다 (InsertList 뒷부분)
            txtPart.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            lblPartName.Text = "";
            chkAdjust.Checked = true;
            _part = null;

            if (_rows.Count >= DirectInboundService.MaxRows)
            {
                Msg("최대 " + DirectInboundService.MaxRows + "건입니다.", MsgLevel.Info);
                if (Ask("일괄입고는 최대 " + DirectInboundService.MaxRows
                        + "까지입니다.\r\n직입고 처리하시겠습니까?"))
                    DoSave();
                return;
            }

            Msg(_rows.Count + "건 담김. 다음 부번을 입력하세요.", MsgLevel.Info);
            txtPart.Focus();
        }

        private void RefreshGrid()
        {
            lstRows.BeginUpdate();
            try
            {
                lstRows.Items.Clear();
                for (int i = 0; i < _rows.Count; i++)
                {
                    DirectInboundRow r = (DirectInboundRow)_rows[i];
                    ListViewItem it = new ListViewItem((i + 1).ToString());
                    it.SubItems.Add(PartNo.Display(r["TRS_PTNO"]));
                    it.SubItems.Add(Loc.Display(r["LOC_LOCNO"]));
                    it.SubItems.Add(r["TRS_SALQT"]);
                    it.SubItems.Add(r["SAL_AMT"]);
                    it.Tag = r;
                    lstRows.Items.Add(it);
                }
            }
            finally { lstRows.EndUpdate(); }
        }

        // ------------------------------------------------------------------
        // 저장 (fn_Save -> fn_SaveGbn)
        // ------------------------------------------------------------------
        private void OnSave(object sender, EventArgs e)
        {
            if (_busy) return;

            if (_rows.Count == 0)
            {
                ReportText("부품을 조회하세요.", MsgLevel.Warn);
                txtPart.Focus();
                return;
            }
            if (MainVendor == null)
            {
                ReportText("업체를 선택하세요.", MsgLevel.Warn);
                cboVndMn.Focus();
                return;
            }
            if (_vap == null)
            {
                ReportText("업체를 다시 선택하십시오.", MsgLevel.Warn);
                cboVndMn.Focus();
                return;
            }

            if (Ask("직입고 처리하시겠습니까?")) DoSave();
        }

        private void DoSave()
        {
            VendorInfo mn = MainVendor;
            VendorInfo sb = SubVendor;
            string vndMn = mn.VndMn;
            string vndSb = (sb == null) ? "" : sb.VndSb;

            ArrayList rows = _rows;
            VapInfo vap = _vap;

            Begin("저장중...");

            Async.Run(this,
                delegate
                {
                    DirectSaveResult s = DirectInboundService.Save(rows, vndMn, vndSb, vap);

                    // 웹도 저장 직후 이어서 PDA 처리표시를 남긴다.
                    // 여기서 실패해도 입고 자체는 이미 커밋된 상태다.
                    if (s.HasVoucher) DirectInboundService.SaveGbn(s.VchYm, s.VchSeq);
                    return s;
                },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    DirectSaveResult s = (DirectSaveResult)r;

                    // fn_Save_After : 목록과 입력만 비우고 업체/여신은 그대로 둔다
                    _rows.Clear();
                    RefreshGrid();
                    txtPart.Text = "";
                    txtPrice.Text = "";
                    txtQty.Text = "";
                    lblPartName.Text = "";
                    chkAdjust.Checked = true;
                    _part = null;
                    Select(cboWhs, "M");

                    string msg = CommonCache.Msg(MP_SAVED, "정상 저장되었습니다.");
                    if (s.HasVoucher) msg += "  (" + s.VchYm + "-" + s.VchSeq + ")";

                    End(msg, MsgLevel.Success);
                    MessageBox.Show(msg, "[142] " + ScreenName);
                    txtPart.Focus();
                });
        }

        // ------------------------------------------------------------------
        // 행삭제 (fn_OnBtnDel) / 지움 (fn_Clear)
        // ------------------------------------------------------------------
        private void OnDel(object sender, EventArgs e)
        {
            if (_busy) return;

            if (lstRows.SelectedIndices.Count == 0)
            {
                Report(MP_NOSEL, "선택된 데이터가 없습니다.", MsgLevel.Warn);
                return;
            }

            _rows.RemoveAt(lstRows.SelectedIndices[0]);
            RefreshGrid();
            Msg("삭제했습니다. 남은 " + _rows.Count + "건", MsgLevel.Info);
            txtPart.Focus();
        }

        private void OnClear(object sender, EventArgs e)
        {
            if (_busy) return;
            if (!Ask("입력한 내용을 모두 지우시겠습니까?")) return;

            ClearAll();
            FillVendorMain();
            Select(cboWhs, "M");
            Msg("초기화", MsgLevel.Info);
            cboVndMn.Focus();
        }

        private void ClearAll()
        {
            _rows.Clear();
            _part = null;
            _vap = null;
            txtPart.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            lblPartName.Text = "";
            chkAdjust.Checked = true;
            lstRows.Items.Clear();
        }

        // ------------------------------------------------------------------
        private string LepValue
        {
            get { return (cboLep.SelectedItem == null) ? "" : cboLep.SelectedItem.ToString(); }
        }

        private string WhsValue
        {
            get { return (cboWhs.SelectedItem == null) ? "M" : cboWhs.SelectedItem.ToString(); }
        }

        private bool Ask(string text)
        {
            return MessageBox.Show(text, "[142] " + ScreenName,
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        private void Begin(string msg)
        {
            _busy = true;
            Cursor.Current = Cursors.WaitCursor;
            SetButtons(false);
            Msg(msg, MsgLevel.Info);
        }

        private void End(string msg, MsgLevel lv)
        {
            _busy = false;
            Cursor.Current = Cursors.Default;
            SetButtons(true);
            Msg(msg, lv);
        }

        /// <summary>MP101 이 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[142] " + ScreenName);
        }

        private void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, "[142] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnSave.Enabled = on;
            btnDel.Enabled = on;
            btnClear.Enabled = on;
            cboVndMn.Enabled = on;
            cboVndSb.Enabled = on;
        }
    }
}
