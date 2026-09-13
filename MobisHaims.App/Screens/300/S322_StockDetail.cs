using System;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Nav;

namespace MobisHaims.Screens
{
    // [322] 재고세부내역 : [320]/[321] 에서 선택한 부품 한 건의 수량 내역을 보여준다.
    // 원본 웹화면 : /ui/ws/plus/PL320_P01.xml (320 의 상세내역 팝업)
    //
    //   OnEnter(LEP/PTNO/LOCNO...) -> fn_Search(plus:PL320_W01_S02) -> ds_PartForLoc 1건
    //
    // 입력이 없는 표시 전용 화면이다. LOC/부번은 읽기전용으로 넘어온 값만 보여준다.
    // 좌표/크기/색/폰트/TabIndex 는 전부 S322_StockDetail.Designer.cs 에서 관리한다.
    public sealed partial class S322_StockDetail : ScreenBase
    {
        private const string MP_OK = "MP101";       // 정상 조회되었습니다
        private const string MP_NOSTOCK = "MP333";  // 해당부품에 대한 재고정보가 없습니다

        private string _lep = "H";
        private string _ptno = "";

        public override int ScreenNo { get { return ScreenId.StockDetail; } }
        public override string ScreenName { get { return "재고세부내역"; } }

        public S322_StockDetail()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            ClearAll();

            string lep = (args == null) ? null : args.GetString("LEP");
            if (lep != null && lep.Length > 0) _lep = lep;
            lblPrefix.Text = _lep;

            _ptno = PartNo.Key(Str((args == null) ? null : args.GetString("PTNO")));
            txtPart.Text = PartNo.Display(_ptno);
            txtLoc.Text = Loc.Display(Str((args == null) ? null : args.GetString("LOCNO")));
            lblClass.Text = Str((args == null) ? null : args.GetString("CLASS"));
            lblPartName.Text = Str((args == null) ? null : args.GetString("PTNM"));

            if (_ptno.Length == 0)
            {
                Msg("부품 정보가 없습니다.", MsgLevel.Warn);
                return;
            }

            Search();
        }

        private static string Str(string s) { return (s == null) ? "" : s; }

        // ------------------------------------------------------------------
        private void Search()
        {
            string lep = _lep;
            string ptno = _ptno;

            Begin("조회중...");
            Async.Run(this,
                delegate { return LocDetailService.Search(lep, ptno); },
                delegate(object r, Exception ex)
                {
                    if (Fail(ex)) return;

                    LocPartDetail d = (LocPartDetail)r;
                    if (!d.Found)
                    {
                        Report(MP_NOSTOCK, "해당 부품에 대한 재고정보가 없습니다.", MsgLevel.Warn);
                        return;
                    }

                    if (d.Grade.Length > 0) lblClass.Text = d.Grade;
                    if (d.PartName.Length > 0) lblPartName.Text = d.PartName;

                    txtAvlQty.Text = d.AvlQty;
                    txtPrice.Text = d.Price;
                    txtAms.Text = d.Ams;
                    txtSftQty.Text = d.SftQty;
                    txtInpdQty.Text = d.InpdQty;
                    txtSalQty.Text = d.SalQty;
                    txtOsdQty.Text = d.OsdQty;
                    txtFault.Text = d.Fault;
                    txtVhc.Text = d.VhcKind;

                    Report(MP_OK, "정상 조회되었습니다.", MsgLevel.Success);
                });
        }

        // ------------------------------------------------------------------
        // 버튼
        // ------------------------------------------------------------------
        private void OnControl(object sender, EventArgs e)
        {
            Msg("OS&D(212) 연결 예정 - " + PartNo.Display(_ptno), MsgLevel.Info);
        }

        private void OnWealth(object sender, EventArgs e)
        {
            NavArgs a = new NavArgs();
            a.Set("LEP", _lep);
            a.Set("PTNO", _ptno);
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            a.Set("LOCNO", Loc.Key(txtLoc.Text));
            Shell.Navigate(ScreenId.LocInventory, a);   // [301] 재물조사(LOC)
        }

        private void OnClear(object sender, EventArgs e)
        {
            ClearAll();
            Msg("초기화", MsgLevel.Info);
        }

        // ------------------------------------------------------------------
        private void Begin(string msg)
        {
            Cursor.Current = Cursors.WaitCursor;
            SetButtons(false);
            Msg(msg, MsgLevel.Info);
        }

        private void End(string msg, MsgLevel lv)
        {
            Cursor.Current = Cursors.Default;
            SetButtons(true);
            Msg(msg, lv);
        }

        /// <summary>MP101 이 아니면 푸터에 더해 MessageBox 도 띄운다.</summary>
        private void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != MP_OK) MessageBox.Show(text, "[322] " + ScreenName);
        }

        private bool Fail(Exception ex)
        {
            if (ex == null) return false;
            End(ex.Message, MsgLevel.Error);
            MessageBox.Show(ex.Message, "[322] " + ScreenName);
            return true;
        }

        private void SetButtons(bool on)
        {
            btnControl.Enabled = on;
            btnWealth.Enabled = on;
            btnClear.Enabled = on;
        }

        private void ClearAll()
        {
            txtAvlQty.Text = ""; txtPrice.Text = "";
            txtAms.Text = ""; txtSftQty.Text = "";
            txtInpdQty.Text = ""; txtSalQty.Text = "";
            txtOsdQty.Text = ""; txtFault.Text = "";
            txtVhc.Text = "";
        }
    }
}
