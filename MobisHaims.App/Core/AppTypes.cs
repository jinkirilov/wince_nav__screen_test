using System;
using MobisHaims.Data;
using MobisHaims.Devices;
using MobisHaims.Nav;

namespace MobisHaims.Core
{
    // 처리결과 메시지 수준(푸터 색상 구분에 사용)
    public enum MsgLevel { Info, Success, Warn, Error }

    // 로그인 세션(작업자/소속/창고/서버)
    public class SessionContext
    {
        public string UserId;
        public string UserName;    // 예: 류현진
        public string OrgName;     // 예: 테스트상사
        public string WhCode;      // 창고코드
        public string ServerUrl = "http://192.168.0.10:8080"; // 데몬 주소
        public bool MockMode = true;   // true: 목데이터로 화면검증, false: 실제 /if 호출
        public DateTime LoginAt = DateTime.Now;   // 로그인 성공 시각

        // 푸터 상시 노출 문구. 원본 웹(gfn_setUserInfoToScreen_Plus)과 같은 형식.
        //   "테스트대리점 최상 2026-09-13 10:16"
        public string UserDisplay
        {
            get
            {
                LoginAt = DateTime.Now;
                return (OrgName == null ? "-" : OrgName)
                     + " " + (UserName == null ? "-" : UserName)
                     + " " + LoginAt.ToString("yyyy-MM-dd HH:mm");
            }
        }

        // 푸터에 상시 노출되는 작업자(소속) 표기
        public string WorkerDisplay
        {
            get { return (UserName == null ? "-" : UserName) + "(" + (OrgName == null ? "-" : OrgName) + ")"; }
        }
    }

    // 각 화면이 셸(Shell)에 요청하는 공통 서비스 계약
    public interface IShellContext
    {
        void Navigate(int screenId, NavArgs args);
        void GoBack();
        void ShowMessage(string text, MsgLevel level); // 푸터 갱신
        SessionContext Session { get; }
        IfClient If { get; }                            // 데몬 /if 통신
        IScanner Scanner { get; }                       // 바코드 스캐너(웨지/하드웨어 공통)
    }
}
