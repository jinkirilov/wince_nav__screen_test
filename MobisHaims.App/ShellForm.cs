using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MobisHaims.Controls;
using MobisHaims.Core;
using MobisHaims.Data;
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

        public ShellForm()
        {
            InitializeComponent();

            _session = new SessionContext();
            _if = new IfClient(_session.ServerUrl, _session.MockMode);

            _registry = new ScreenRegistry();
            RegisterScreens();

            _nav = new NavigationManager(_content, _registry, this);
            _nav.CurrentChanged += delegate { SyncHeader(); };

            DoLogin();
            _nav.Navigate(ScreenId.Main, NavArgs.Empty);
        }

        // 스타터에서는 구현된 3개 화면만 등록. 나머지는 JUMP/버튼 시 "미등록" 안내.
        private void RegisterScreens()
        {
            _registry.Register(ScreenId.Main, delegate { return new S000_MainMenu(); });
            _registry.Register(ScreenId.InboundMenu, delegate { return new S100_InboundMenu(); });
            _registry.Register(ScreenId.SiteInboundClassify, delegate { return new S120_SiteInboundClassify(); });
        }

        private void DoLogin()
        {
            IfMessage res = _if.Send("IF_LOGIN", null);
            if (res.IsSuccess)
            {
                _session.UserId = res.ItemStr("userId");
                _session.UserName = res.ItemStr("userName");
                _session.OrgName = res.ItemStr("orgName");
                _session.WhCode = res.ItemStr("whCode");
            }
            else { _session.UserName = "게스트"; _session.OrgName = "-"; }
        }

        private void SyncHeader()
        {
            ScreenBase cur = _nav.Current;
            if (cur == null) return;
            _header.SetTitle(cur.ScreenNo, cur.ScreenName);
            // 메인화면 진입 시 작업자(소속)를 상시 노출
            if (cur.ScreenNo == ScreenId.Main)
                ShowMessage(_session.WorkerDisplay, MsgLevel.Info);
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
    }
}
