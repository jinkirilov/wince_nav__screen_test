using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>
    /// 로그인 및 로그인 직후 공통 데이터 로딩.
    ///
    /// 모든 호출이 HaimsHttp.PostStream 을 쓴다. 응답을 통째로 메모리에 올리지 않고
    /// 레코드 1건씩 콜백으로 받아 필요한 것만 남긴다(32MB 슬롯 대응).
    /// </summary>
    public static class AuthService
    {
        private const string ActionMobile = "HAIMS_MOBILE_ACTION";
        private const string ActionComm = "HAIMS_COMM_ACTION";

        /// <summary>로그인 화면에서 붙는 공통 파라미터 (document.title == 'LoginPage' 분기)</summary>
        private static void AddLoginCommon(TitRequest r)
        {
            r.AddParam("actionName", ActionMobile);
            r.AddParam("cmd", "execute");
            r.AddParam("_USR_USRID", "");
            r.AddParam("_USR_AGTCD", "");
            r.AddParam("_USR_USRGR", "");
            r.AddParam("_USR_MINUS_FL", "");
            r.AddParam("_USR_PRTFL", "");
            r.AddParam("_USR_WHSM_FL", "");
            r.AddParam("_USR_WHSA_FL", "");
            r.AddParam("_USR_WHSB_FL", "");
            r.AddParam("_USR_WHSC_FL", "");
            r.AddParam("_USR_WHSD_FL", "");
            r.AddParam("_USR_STDPRC_FL", "");
            r.AddParam("_USR_STD_LOC", "");
            r.AddParam("_ORG_USR_USRID", "");
            r.AddParam("_MACHINE", "3");
            r.AddParam("_SESSION_NO", HaimsHttp.NewSessionNo());
        }

        /// <summary>로그인 후 화면에서 붙는 공통 파라미터</summary>
        private static void AddSessionCommon(TitRequest r)
        {
            UserInfo u = Session.User;
            r.AddParam("actionName", ActionMobile);
            r.AddParam("cmd", "execute");
            r.AddParam("_USR_USRID", u == null ? "" : u["USR_USRID"]);
            r.AddParam("_USR_AGTCD", u == null ? "" : u["USR_AGTCD"]);
            r.AddParam("_USR_USRGR", u == null ? "" : u["USR_USRGR"]);
            r.AddParam("_USR_MINUS_FL", u == null ? "" : u["USR_MINUS_FL"]);
            r.AddParam("_USR_PRTFL", u == null ? "" : u["USR_PRTFL"]);
            r.AddParam("_USR_WHSM_FL", u == null ? "" : u["USR_WHSM_FL"]);
            r.AddParam("_USR_WHSA_FL", u == null ? "" : u["USR_WHSA_FL"]);
            r.AddParam("_USR_WHSB_FL", u == null ? "" : u["USR_WHSB_FL"]);
            r.AddParam("_USR_WHSC_FL", u == null ? "" : u["USR_WHSC_FL"]);
            r.AddParam("_USR_WHSD_FL", u == null ? "" : u["USR_WHSD_FL"]);
            r.AddParam("_USR_STDPRC_FL", u == null ? "" : u["USR_STDPRC_FL"]);
            r.AddParam("_USR_STD_LOC", u == null ? "" : u["USR_STD_LOC"]);
            r.AddParam("_ORG_USR_USRID", u == null ? "" : u["ORG_USR_USRID"]);
            r.AddParam("_MACHINE", "3");
            r.AddParam("_SESSION_NO", u == null ? HaimsHttp.NewSessionNo() : u.SessionNo);
        }

        /// <summary>fn_Init - 서버명(_ServerName) 조회</summary>
        public static string GetServerName()
        {
            TitRequest r = new TitRequest();
            r.AddParam("_DummyCall", "Y");
            r.AddParam("actionName", ActionComm);
            r.AddParam("cmd", "execute");
            r.AddParam("_USR_USRID", "");
            r.AddParam("_MACHINE", "3");
            r.AddParam("_SESSION_NO", HaimsHttp.NewSessionNo());

            TitResult res = HaimsHttp.PostStream("Login.xml", "fn_Init", r.Build(), null);
            return res.Param("_ServerName");
        }

        /// <summary>fn_Login</summary>
        public static UserInfo Login(string userId, string password)
        {
            string hash = Crypto.Sha256Hex(password);

            TitRequest r = new TitRequest();
            r.AddSearch("common:HS00_W01_S01");
            r.AddParam("USER_ID", userId);
            r.AddParam("INIT_PW", hash);
            r.AddParam("USER_SMARTPW", hash);
            r.AddParam("_TIT_LOGIN_REQ_YN", "Y");
            r.AddParam("_MAC_USIM", "");   // 브라우저도 빈 값으로 전송함
            AddLoginCommon(r);

            // dsUserInfo 첫 레코드만 잡는다. _dsForSqlLog(응답의 대부분)는 흘려보낸다.
            UserInfo[] hit = new UserInfo[1];

            TitResult res = HaimsHttp.PostStream("Login.xml", "fn_Login", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "dsUserInfo" || hit[0] != null) return;
                    hit[0] = UserInfo.From(row);
                });

            if (res.IsError) throw new HaimsException(res.ErrorMsg);

            UserInfo u = hit[0];
            if (u == null) throw new HaimsException("사용자 아이디가 존재하지 않습니다.");

            if (u["USR_SMARTPW"] != "Y")
                throw new HaimsException("비밀번호가 틀립니다.");

            // DEL_YN 은 'Y' 가 정상, 'N' 이 삭제됨 (직관과 반대)
            if (u["DEL_YN"] == "N")
                throw new HaimsException("삭제된 사용자 계정입니다.");

            u.SessionNo = res.Param("_SESSION_NO").Replace("-", "");
            u.SysDate = DateTime.Now.ToString("yyyyMMdd");
            return u;
        }

        /// <summary>fn_LogSave - 접속 로그 기록. 실패해도 로그인은 진행한다.</summary>
        public static void SaveLoginLog(UserInfo u)
        {
            try
            {
                TitRequest r = new TitRequest();
                r.AddSearch("common:LOG_PDA_I01");
                r.AddSearch("common:HS00_W01_U01");
                r.AddParam("USRID", u.UserId);
                r.AddParam("USR_USRID", u.UserId);
                r.AddParam("SCRID", "LOGIN");
                r.AddParam("USRIP", "");
                r.AddParam("USRMAC", "MOBILEPDA");
                AddLoginCommon(r);

                HaimsHttp.PostStream("Login.xml", "fn_LogSave", r.Build(), null);
            }
            catch { /* 원본 JS 도 실패를 무시함 */ }
        }

        /// <summary>fn_CheckIdPass - 비밀번호 변경 팝업의 현재 비밀번호 확인</summary>
        public static bool CheckPassword(string userId, string password)
        {
            string hash = Crypto.Sha256Hex(password);

            TitRequest r = new TitRequest();
            r.AddSearch("common:HS00_W01_S01");
            r.AddParam("USER_ID", userId);
            r.AddParam("INIT_PW", hash);
            r.AddParam("USER_SMARTPW", hash);
            r.AddParam("_TIT_LOGIN_REQ_YN", "Y");
            r.AddParam("_MAC_USIM", "");
            AddLoginCommon(r);

            string[] smartPw = new string[1];
            bool[] found = new bool[1];

            HaimsHttp.PostStream("Login.xml", "fn_CheckIdPass", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "dsUserInfo" || found[0]) return;
                    found[0] = true;
                    smartPw[0] = row["USR_SMARTPW"];
                });

            if (!found[0]) throw new HaimsException("사용자정보가 없습니다.");
            return smartPw[0] != "N";
        }

        /// <summary>fn_ChgPass - 비밀번호 변경</summary>
        public static void ChangePassword(string userId, string newPassword)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("common:UPDATE_PDA_PASS_U01");
            r.AddParam("USER_ID", userId);
            r.AddParam("PDA_CHANGE_PW", Crypto.Sha256Hex(newPassword));
            AddLoginCommon(r);

            TitResult res = HaimsHttp.PostStream("Login.xml", "fn_ChgPass", r.Build(), null);

            if (res.IsError)
                throw new HaimsException("시스템 오류가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        /// <summary>
        /// getMenuAndMessageAndCommonCode - 메뉴 / 메시지 / 공통코드 / 업체코드.
        ///
        /// 응답이 가장 큰 호출이다. 레코드를 통째로 보관하지 않고
        /// 화면에서 실제로 쓰는 컬럼만 뽑아 캐시에 넣는다.
        /// </summary>
        public static void LoadCommonData()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("common:HS00_W01_S02");   // 메뉴
            r.AddSearch("common:HS00_W01_S03");   // 메시지
            r.AddSearch("common:HS00_W01_S04");   // 공통코드
            r.AddSearch("common:HS00_W01_S06");   // 업체코드
            AddSessionCommon(r);

            CommonCache.Clear();

            TitResult res = HaimsHttp.PostStream(
                "Login.xml", "getMenuAndMessageAndCommonCode", r.Build(),
                new RecordCallback(CommonCache.Accept));

            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }
    }

    /// <summary>
    /// 공통 데이터 캐시.
    ///
    /// Accept() 가 레코드 1건마다 불리고, 필요한 값만 꺼내 담은 뒤 Row 는 버려진다.
    /// 실제 데이터셋 id 와 컬럼명은 실기 응답을 확인한 뒤 맞춘다
    /// (아래 이름은 HS00_W01_S02~S06 의 통상 명명 기준).
    /// </summary>
    public static class CommonCache
    {
        public static readonly Hashtable Menu = new Hashtable();      // 화면번호 -> 화면명
        public static readonly Hashtable Message = new Hashtable();   // 메시지ID -> 메시지
        public static readonly Hashtable Code = new Hashtable();      // "그룹|코드" -> 코드명
        public static readonly Hashtable Vendor = new Hashtable();    // 업체코드 -> 업체명

        public static void Clear()
        {
            Menu.Clear(); Message.Clear(); Code.Clear(); Vendor.Clear();
        }

        internal static void Accept(string ds, Row row)
        {
            if (ds == null) return;

            // 응답의 대부분을 차지하는 SQL 로그는 즉시 버린다
            if (ds == "_dsForSqlLog") return;

            if (ds.IndexOf("Menu") >= 0)
            {
                string id = First(row, "SCR_ID", "MENU_ID", "SCRID");
                if (id.Length > 0) Menu[id] = First(row, "SCR_NM", "MENU_NM", "SCRNM");
            }
            else if (ds.IndexOf("Message") >= 0 || ds.IndexOf("Msg") >= 0)
            {
                string id = First(row, "MSG_ID", "MSGID");
                if (id.Length > 0) Message[id] = First(row, "MSG_CONT", "MSG_NM", "MSG");
            }
            else if (ds.IndexOf("Code") >= 0 && ds.IndexOf("Vendor") < 0)
            {
                string grp = First(row, "CD_GRP", "GRP_CD", "CDGRP");
                string cd = First(row, "CD_ID", "CD", "CDID");
                if (cd.Length > 0) Code[grp + "|" + cd] = First(row, "CD_NM", "CDNM", "CD_NAME");
            }
            else if (ds.IndexOf("Vendor") >= 0 || ds.IndexOf("Agt") >= 0)
            {
                string cd = First(row, "AGT_CD", "VEN_CD", "CUST_CD");
                if (cd.Length > 0) Vendor[cd] = First(row, "AGT_NM", "VEN_NM", "CUST_NM");
            }
        }

        /// <summary>후보 컬럼명 중 값이 있는 첫 번째를 돌려준다(실기 확인 전 완충).</summary>

        private static string First(Row row, params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                string v = row[names[i]];
                if (v.Length > 0) return v;
            }
            return "";
        }


        public static string Msg(string id)
        {
            object o = Message[id];
            return (o == null) ? id : (string)o;
        }

        public static string CodeName(string group, string code)
        {
            object o = Code[group + "|" + code];
            return (o == null) ? code : (string)o;
        }
    }
}
