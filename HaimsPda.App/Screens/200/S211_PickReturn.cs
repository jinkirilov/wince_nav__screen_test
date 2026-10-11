using System;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    // [211] 반품 (OS&D 피킹) : [210]/[280] 의 지금 라인에서 반품 수량을 받아 출고 수량을 계산해 돌려준다.
    // 원본 웹화면 : /ui/ws/plus/PL211_W01.xml (서버 메뉴 P172)
    //
    //   서버 호출 없음. 반품수량 입력 -> fn_CheckQty (0/공백 MP613, 출고대상보다 크면 MP559)
    //   -> 출고수량 = 출고대상 - 반품 -> 확인(원본은 출고수량 칸 Enter) -> 호출 화면으로 QTY / OSNDQTY 를 들고 복귀
    //   실제 반품전표(PL100_W01_P01)는 호출 화면이 [저장] 할 때 만든다.
    //
    // 원본과 다르게 한 부분
    //   - 원본은 출고수량 칸에서 Enter 를 쳐야 돌아간다. 여기서는 [확인] 버튼도 둔다.
    //
    // 좌표/크기/색/폰트/TabIndex 는 전부 S211_PickReturn.Designer.cs 에서 관리한다.
    public sealed partial class S211_PickReturn : WorkScreen
    {
        private NavArgs _link;

        public override int ScreenNo { get { return ScreenId.PickReturn; } }
        public override string ScreenName { get { return "반품"; } }

        public S211_PickReturn()
        {
            InitializeComponent();
            if (IsDesignMode) return;
        }

        public override void OnEnter(NavArgs args)
        {
            _link = args;
            string lep = Arg(args, "LEP");
            lblPrefix.Text = lep.Length > 0 ? lep : "H";
            lblPart.Text = PartNo.Display(Arg(args, "PTNO"));
            lblClass.Text = Arg(args, "CLASS");
            lblPartName.Text = Arg(args, "PTNM");
            lblSaleNo.Text = Arg(args, "SALENO");
            lblLoc.Text = Loc.Display(Arg(args, "LOCNO"));
            lblSalqt.Text = Num(Arg(args, "EXPECTQTY"));
            txtOsndqt.Text = "";
            lblOutqt.Text = "";

            Msg("반품 수량을 입력하세요.", MsgLevel.Info);
            txtOsndqt.Focus();
        }

        public override void OnScan(HaimsPda.Devices.ScanData data)
        {
            Msg("이 화면은 스캔 입력을 사용하지 않습니다.", MsgLevel.Info);
        }

        private void OnNumKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == '\r') return;
            if (e.KeyChar < '0' || e.KeyChar > '9') e.Handled = true;
        }

        private void OnOsndKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            CheckQty();
        }

        /// <summary>원본 fn_CheckQty</summary>
        private bool CheckQty()
        {
            int sal = ToInt(lblSalqt.Text);
            string s = txtOsndqt.Text.Trim();
            if (s.Length == 0 || ToInt(s) == 0)
            {
                Report("MP613", "수량에 공백이나 0을 입력할 수 없습니다.", MsgLevel.Warn);
                txtOsndqt.Focus();
                return false;
            }
            int osnd = ToInt(s);
            if (sal < osnd)
            {
                Report("MP559", "반품수량이 출고대상수량보다 많습니다.", MsgLevel.Warn);
                txtOsndqt.Focus();
                txtOsndqt.SelectAll();
                return false;
            }
            lblOutqt.Text = (sal - osnd).ToString();
            Msg("출고수량 " + lblOutqt.Text + ". [확인] 하면 피킹 화면으로 돌아갑니다.", MsgLevel.Info);
            return true;
        }

        // 원본 inptOutqt Enter : QTY / OSNDQTY 를 링크에 넣고 이전 화면으로
        private void OnOk(object sender, EventArgs e)
        {
            if (!CheckQty()) return;
            NavArgs r = new NavArgs();
            r.Set("QTY", lblOutqt.Text);
            r.Set("OSNDQTY", ToInt(txtOsndqt.Text).ToString());
            Shell.GoBack(r);
        }

        private void OnStock(object sender, EventArgs e)
        {
            NavArgs a = new NavArgs();
            a.Set("LEP", lblPrefix.Text);
            a.Set("PTNO", PartNo.Key(lblPart.Text));
            a.Set("PTNM", lblPartName.Text);
            a.Set("CLASS", lblClass.Text);
            a.Set("LOCNO", Loc.Key(lblLoc.Text));
            Shell.Navigate(ScreenId.StockByPart, a);
        }

        // 원본 fn_Init 는 부품까지 다 지워 다시 넣을 방법이 없다. 입력값만 지운다.
        private void OnClear(object sender, EventArgs e)
        {
            if (!ConfirmMsg("MP503", "화면을 지우시겠습니까?")) return;
            txtOsndqt.Text = "";
            lblOutqt.Text = "";
            txtOsndqt.Focus();
        }

        protected override void SetButtons(bool on)
        {
            btnOk.Enabled = on;
            btnStock.Enabled = on;
            btnClear.Enabled = on;
        }
    }
}
