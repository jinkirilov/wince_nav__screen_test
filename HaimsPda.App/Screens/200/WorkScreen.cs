using System;
using System.Collections;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;
using HaimsPda.Core;
using HaimsPda.Nav;

namespace HaimsPda.Screens
{
    /// <summary>
    /// 업무화면 공용 도우미 (출고 200 화면부터 사용).
    /// 기존 화면마다 복사해 쓰던 Begin/End/Report/Fail/Confirm 을 한곳에 모았다.
    ///
    /// VS2008 디자이너가 파생 화면을 열 수 있도록 abstract 로 만들지 않는다.
    /// 디자인 요소(컨트롤)는 없다.
    /// </summary>
    public class WorkScreen : ScreenBase
    {
        protected bool Busy;

        /// <summary>메시지창 제목 "[210] 피킹실적처리"</summary>
        protected string Caption
        {
            get { return "[" + ScreenNo.ToString("D3") + "] " + ScreenName; }
        }

        /// <summary>바쁠 때 버튼을 잠그고 풀어 준다. 화면이 override 한다.</summary>
        protected virtual void SetButtons(bool on) { }

        protected void Begin(string msg)
        {
            Busy = true;
            Cursor.Current = Cursors.WaitCursor;
            SetButtons(false);
            if (msg != null && msg.Length > 0) Msg(msg, MsgLevel.Info);
        }

        protected void End(string msg, MsgLevel lv)
        {
            Busy = false;
            Cursor.Current = Cursors.Default;
            SetButtons(true);
            if (msg != null && msg.Length > 0) Msg(msg, lv);
        }

        /// <summary>MP101/MP102(정상) 은 푸터에만, 나머지는 메시지창도 띄운다.</summary>
        protected void Report(string code, string fallback, MsgLevel lv)
        {
            string text = CommonCache.Msg(code, fallback);
            End(text, lv);
            if (code != "MP101" && code != "MP102") MessageBox.Show(text, Caption);
        }

        protected void ReportText(string text, MsgLevel lv)
        {
            End(text, lv);
            MessageBox.Show(text, Caption);
        }

        protected bool Fail(Exception ex)
        {
            if (ex == null) return false;
            ReportText(ex.Message, MsgLevel.Error);
            return true;
        }

        protected bool Confirm(string text)
        {
            return MessageBox.Show(text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        protected bool ConfirmMsg(string code, string fallback)
        {
            return Confirm(CommonCache.Msg(code, fallback));
        }

        /// <summary>작업 스레드에서 work 를 돌리고 UI 스레드에서 done. Begin 은 여기서 한다.</summary>
        protected void Run(string msg, AsyncWork work, AsyncDone done)
        {
            Begin(msg);
            Async.Run(this, work, done);
        }

        // ------------------------------------------------------------------
        protected static string Arg(NavArgs a, string key)
        {
            string s = (a == null) ? null : a.GetString(key);
            return (s == null) ? "" : s.Trim();
        }

        protected static int ToInt(string s) { return OutboundService.ToInt(s); }

        /// <summary>표시용 정수. "3.00" -> "3"</summary>
        protected static string Num(string s) { return ToInt(s).ToString(); }

        protected static string Ymd(DateTime d) { return d.ToString("yyyyMMdd"); }

        protected static bool TryDate(string s, out DateTime d)
        {
            d = DateTime.Today;
            if (s == null) return false;
            s = s.Replace("-", "").Trim();
            if (s.Length < 8) return false;
            try
            {
                d = new DateTime(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(4, 2)),
                                 int.Parse(s.Substring(6, 2)));
                return true;
            }
            catch { return false; }
        }

        protected static void FillCombo(ComboBox cbo, ArrayList items, string select)
        {
            cbo.Items.Clear();
            for (int i = 0; i < items.Count; i++) cbo.Items.Add(items[i]);
            if (cbo.Items.Count == 0) return;
            cbo.SelectedIndex = 0;
            SelectByText(cbo, select);
        }

        protected static void SelectByText(ComboBox cbo, string text)
        {
            if (text == null || text.Length == 0) return;
            for (int i = 0; i < cbo.Items.Count; i++)
                if (cbo.Items[i].ToString() == text) { cbo.SelectedIndex = i; return; }
        }

        protected static string ComboText(ComboBox cbo, string def)
        {
            return (cbo.SelectedIndex < 0) ? def : cbo.Items[cbo.SelectedIndex].ToString();
        }

        /// <summary>콤보 항목이 CodeItem 이면 코드, 아니면 문자열</summary>
        protected static string ComboCode(ComboBox cbo)
        {
            if (cbo.SelectedIndex < 0) return "";
            object o = cbo.Items[cbo.SelectedIndex];
            CodeItem c = o as CodeItem;
            return (c != null) ? c.Code : o.ToString();
        }

        protected static void SelectByCode(ComboBox cbo, string code)
        {
            if (code == null) return;
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                CodeItem c = cbo.Items[i] as CodeItem;
                string v = (c != null) ? c.Code : cbo.Items[i].ToString();
                if (v == code) { cbo.SelectedIndex = i; return; }
            }
        }

        /// <summary>날짜 구간 (원본 getDayInterval) — 일 수 차이</summary>
        protected static int DayGap(DateTime from, DateTime to)
        {
            return (int)(to.Date - from.Date).TotalDays;
        }
    }
}
