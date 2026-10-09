using System;
using System.Threading;
using System.Windows.Forms;

namespace HaimsPda.Ui
{
    public delegate object AsyncWork();
    public delegate void AsyncDone(object result, Exception error);

    /// <summary>
    /// .NET CF 에는 BackgroundWorker 가 없다.
    /// 작업 스레드에서 work() 를 돌리고 결과를 UI 스레드로 되던지는 최소 구현.
    ///
    ///   Async.Run(this,
    ///       delegate { return AuthService.Login(id, pw); },
    ///       delegate(object r, Exception e) { ... });
    ///
    /// done() 은 항상 UI 스레드에서 호출되며, work() 가 던진 예외는
    /// error 인자로 전달된다 (스레드에서 터뜨리지 않는다).
    /// </summary>
    public static class Async
    {
        private delegate void UiCall();

        public static void Run(Control owner, AsyncWork work, AsyncDone done)
        {
            if (owner == null || work == null) return;

            Thread t = new Thread(delegate()
            {
                object result = null;
                Exception error = null;

                try { result = work(); }
                catch (Exception ex) { error = ex; }

                if (done == null) return;

                object r = result;
                Exception e = error;
                try
                {
                    owner.Invoke(new UiCall(delegate { done(r, e); }));
                }
                catch
                {
                    // 폼이 이미 닫혔으면 무시
                }
            });

            t.IsBackground = true;
            t.Start();
        }
    }
}
