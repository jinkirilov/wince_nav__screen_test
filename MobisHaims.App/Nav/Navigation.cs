using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MobisHaims.Core;

namespace MobisHaims.Nav
{
    // 화면 전환 파라미터(부모->자식). FORWARD_* / RESULT_* 키 네임스페이스를 문자열로 관리.
    public sealed class NavArgs
    {
        public static readonly NavArgs Empty = new NavArgs();
        private readonly Dictionary<string, object> _d = new Dictionary<string, object>();

        public NavArgs Set(string k, object v) { _d[k] = v; return this; }
        public bool Has(string k) { return _d.ContainsKey(k); }
        public object Get(string k) { object v; return _d.TryGetValue(k, out v) ? v : null; }
        public string GetString(string k) { object v = Get(k); return v == null ? null : v.ToString(); }
        public int GetInt(string k, int def)
        {
            object v = Get(k);
            if (v == null) return def;
            try { return Convert.ToInt32(v); } catch { return def; }
        }
    }

    // 공정별 메뉴(헤더 햄버거 드로어) 항목
    public sealed class ProcessMenuItem
    {
        public string Text;
        public int TargetScreenId;
        public ProcessMenuItem(string text, int target) { Text = text; TargetScreenId = target; }
    }



    // 화면번호 -> 화면 생성자 매핑 테이블(JUMP 핵심)
    public delegate ScreenBase ScreenCreator();

    public sealed class ScreenRegistry
    {
        private readonly Dictionary<int, ScreenCreator> _map = new Dictionary<int, ScreenCreator>();
        public void Register(int id, ScreenCreator c) { _map[id] = c; }
        public bool Contains(int id) { return _map.ContainsKey(id); }
        public ScreenBase Create(int id) { ScreenCreator c; return _map.TryGetValue(id, out c) ? c() : null; }
    }

    // 네비게이션 스택 + 뒤로가기 + JUMP 관리
    public sealed class NavigationManager
    {
        private readonly Panel _host;
        private readonly ScreenRegistry _reg;
        private readonly IShellContext _shell;
        private readonly Stack<ScreenBase> _stack = new Stack<ScreenBase>();

        public event EventHandler CurrentChanged;

        public NavigationManager(Panel host, ScreenRegistry reg, IShellContext shell)
        {
            _host = host; _reg = reg; _shell = shell;
        }

        public ScreenBase Current { get { return _stack.Count > 0 ? _stack.Peek() : null; } }
        public int Depth { get { return _stack.Count; } }

        public void Navigate(int id, NavArgs args)
        {
            ScreenBase next = _reg.Create(id);
            if (next == null)
            {
                _shell.ShowMessage("[" + id.ToString("D3") + "] 화면이 등록되지 않았습니다.", MsgLevel.Error);
                return;
            }
            if (Current != null) { Current.OnLeave(); Current.Visible = false; }

            next.Attach(_shell);
            next.Dock = DockStyle.Fill;
            _host.SuspendLayout();
            _host.Controls.Add(next);
            next.BringToFront();
            _host.ResumeLayout();

            _stack.Push(next);
            next.OnEnter(args == null ? NavArgs.Empty : args);
            Raise();
        }

        public bool GoBack()
        {
            if (_stack.Count <= 1) return false;
            ScreenBase top = Current;
            if (!top.OnBack()) return false;

            _stack.Pop();
            top.OnLeave();
            _host.Controls.Remove(top);
            top.Dispose();   // WM 메모리 회수 필수

            ScreenBase cur = Current;
            if (cur != null) { cur.Visible = true; cur.BringToFront(); }
            Raise();
            return true;
        }

        private void Raise() { if (CurrentChanged != null) CurrentChanged(this, EventArgs.Empty); }
    }
}
