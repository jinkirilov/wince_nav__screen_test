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
            // 0) 저장된 호스트 설정(prefs.txt)을 통신에 반영한다. 없으면 기본값.
            HostConfig.Load();

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
