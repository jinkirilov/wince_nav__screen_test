using System;
using System.Windows.Forms;
using HaimsPda.Net;
using HaimsPda.Ui;

namespace HaimsPda
{
    static class Program
    {
        [MTAThread]
        static void Main()
        {
            // 1) 로그인. 취소/실패면 그대로 종료한다.
            using (LoginForm login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK) return;
            }

            if (!Session.IsLoggedIn) return;

            // 2) 셸 진입. 메인메뉴부터 시작한다.
            Application.Run(new MobisHaims.ShellForm());
        }
    }
}
