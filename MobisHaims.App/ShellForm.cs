using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MobisHaims.Controls;
using MobisHaims.Core;
using MobisHaims.Data;
using MobisHaims.Devices;
using MobisHaims.Nav;
using MobisHaims.Screens;

namespace MobisHaims
{
    // 애플리케이션 셸: Header(Top) + Content(Fill) + Footer(Bottom).
    // 화면(UserControl)을 Content 패널에 스왑하며, 공통 상/하단은 여기서만 관리한다.
    public sealed partial class ShellForm : Form, IShellContext
    {
        private readonly ScreenRegistry _registry;
        private readonly NavigationManager _nav;
        private readonly SessionContext _session;
        private readonly IfClient _if;
        private IScanner _scanner;

        public ShellForm()
        {
            InitializeComponent();

            _session = new SessionContext();
            _if = new IfClient(_session.ServerUrl, _session.MockMode);

            _registry = new ScreenRegistry();
            RegisterScreens();

            _nav = new NavigationManager(_content, _registry, this);
            _nav.CurrentChanged += delegate { SyncHeader(); };

            // 스캐너는 앱 수명과 같이 간다. 벤더 드라이버 -> 실패 시 키보드 웨지.
            _scanner = ScannerFactory.Create(this);
            _scanner.Scanned += new ScanEventHandler(OnScanned);

            DoLogin();
            _nav.Navigate(ScreenId.Main, NavArgs.Empty);
        }

        // 스캔은 현재 화면에만 전달한다. 화면은 ScreenBase.OnScan 을 override 하면 된다.
        private void OnScanned(object sender, ScanData data)
        {
            ScreenBase cur = _nav.Current;
            if (cur == null) return;
            try { cur.OnScan(data); }
            catch (Exception ex) { ShowMessage(ex.Message, MsgLevel.Error); }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_scanner != null)
            {
                try
                {
                    _scanner.Scanned -= new ScanEventHandler(OnScanned);
                    _scanner.Dispose();
                }
                catch { }
                _scanner = null;
            }
            base.OnClosed(e);
        }

        // 스타터에서는 구현된 3개 화면만 등록. 나머지는 JUMP/버튼 시 "미등록" 안내.
        private void RegisterScreens()
        {
            _registry.Register(ScreenId.Main, delegate { return new S000_MainMenu(); });
            _registry.Register(ScreenId.InboundMenu, delegate { return new S100_InboundMenu(); });
            _registry.Register(ScreenId.SiteInboundClassify, delegate { return new S120_SiteInboundClassify(); });
            _registry.Register(ScreenId.InboundSave, delegate { return new S140_InboundSave(); });
            _registry.Register(ScreenId.StockMenu, delegate { return new S300_StockMenu(); });
            _registry.Register(ScreenId.StockByLoc, delegate { return new S320_LocStock(); });
            _registry.Register(ScreenId.StockByPart, delegate { return new S321_PartStock(); });
            _registry.Register(ScreenId.StockDetail, delegate { return new S322_StockDetail(); });
            _registry.Register(ScreenId.PartInfo, delegate { return new S324_PartInfo(); });
        }

        // 로그인은 Program.Main 의 LoginForm 에서 이미 끝났다.
        // 여기서는 HAIMS 세션값을 셸 세션으로 옮겨 담기만 한다.
        private void DoLogin()
        {
            HaimsPda.Net.UserInfo u = HaimsPda.Net.Session.User;
            _session.LoginAt = DateTime.Now;

            if (u == null)
            {
                _session.UserName = "게스트";
                _session.OrgName = "-";
                _footer.SetDefaultText(_session.UserDisplay);
                return;
            }

            _session.UserId = u.UserId;
            _session.UserName = u.UserNm;
            _session.OrgName = u.AgtNm;
            _session.WhCode = u.AgtCd;

            // 푸터 상시 문구 : 대리점명 사용자명 접속시각
            _footer.SetDefaultText(_session.UserDisplay);
        }

        private void SyncHeader()
        {
            ScreenBase cur = _nav.Current;
            if (cur == null) return;
            _header.SetTitle(cur.ScreenNo, cur.ScreenName);
            // 메인화면 진입 시 로그인 정보를 다시 노출
            if (cur.ScreenNo == ScreenId.Main)
                _footer.ShowDefault();
        }

        private void OnMenu()
        {
            ScreenBase cur = _nav.Current;
            ProcessMenuItem[] items = cur != null ? cur.ProcessMenu : null;
            if (items == null || items.Length == 0) { ShowMessage("공정 메뉴 없음", MsgLevel.Info); return; }
            // 실제로는 Drawer(ContextMenu) 표시. 스타터에서는 개수만 안내.
            ShowMessage("공정 메뉴 " + items.Length + "개", MsgLevel.Info);
        }

        private void OnClose()
        {
            if (_nav.Depth > 1) _nav.GoBack();   // 상위 화면으로 복귀
            else this.Close();                   // 메인에서 X = 종료
        }

        private void OnJump(int no)
        {
            if (!_registry.Contains(no)) { ShowMessage("[" + no.ToString("D3") + "] 미등록 화면", MsgLevel.Warn); return; }
            _nav.Navigate(no, NavArgs.Empty);
        }

        private void OnHeaderMenuClicked(object sender, EventArgs e) { OnMenu(); }
        private void OnHeaderCloseClicked(object sender, EventArgs e) { OnClose(); }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 하드웨어 ESC/Back 키로 이전화면 복귀
            if (e.KeyCode == Keys.Escape && _nav.Depth > 1) { _nav.GoBack(); e.Handled = true; }
            base.OnKeyDown(e);
        }

        // ---------------- IShellContext ----------------
        public void Navigate(int screenId, NavArgs args) { _nav.Navigate(screenId, args); }
        public void GoBack() { _nav.GoBack(); }
        public void ShowMessage(string text, MsgLevel level) { _footer.SetMessage(text, level); }
        public SessionContext Session { get { return _session; } }
        public IfClient If { get { return _if; } }
        public IScanner Scanner { get { return _scanner; } }
    }
}
