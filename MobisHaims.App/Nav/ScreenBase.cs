using System;
using System.Windows.Forms;
using HaimsPda.Ui;
using MobisHaims.Core;
using MobisHaims.Devices;

namespace MobisHaims.Nav
{
    // 모든 업무화면의 베이스. Form이 아닌 UserControl로 만들어 셸의 ContentPanel에 스왑한다.
    //
    // [해상도 대응]
    //  - 좌표/크기/색/폰트는 각 화면의 Designer.cs (VS2008 디자이너)에서만 관리한다.
    //  - 디자이너 기준 해상도는 ShellForm._content @ VGA = 480 x 484 (192dpi).
    //  - QVGA(96dpi) 축소는 ShellForm 의 AutoScaleMode.Dpi 가 컨트롤 트리 전체에 적용한다.
    //  - 따라서 화면 코드에서 Bounds 를 다시 계산하면 이중 축소가 되므로 하지 않는다.
    public class ScreenBase : UserControl
    {
        protected IShellContext Shell;

        public virtual int ScreenNo { get { return 0; } }
        public virtual string ScreenName { get { return ""; } }
        public virtual ProcessMenuItem[] ProcessMenu { get { return null; } }

        public void Attach(IShellContext shell)
        {
            Shell = shell;
            // 이 프로그램은 한글 입력이 없다. 화면의 모든 입력창을 영문 모드로 고정한다.
            Ime.AttachAll(this);
            OnAttached();
        }
        protected virtual void OnAttached() { }

        public virtual void OnEnter(NavArgs args) { }   // 화면 진입
        public virtual void OnLeave() { }               // 화면 이탈
        public virtual bool OnBack() { return true; }   // false 반환 시 뒤로가기 취소

        /// <summary>
        /// 바코드 스캔 수신. 셸이 현재 화면에만 전달한다.
        /// 화면은 구독/해지를 신경 쓸 필요 없이 이것만 override 하면 된다.
        /// </summary>
        public virtual void OnScan(ScanData data) { }

        protected void Msg(string t, MsgLevel lv) { if (Shell != null) Shell.ShowMessage(t, lv); }

        // 생성자에서는 Component.DesignMode를 신뢰할 수 없으므로 Site로 판정한다.
        protected bool IsDesignMode
        {
            get { return this.Site != null && this.Site.DesignMode; }
        }
    }
}
