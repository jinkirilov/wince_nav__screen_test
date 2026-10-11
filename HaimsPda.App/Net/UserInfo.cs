using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>Login.xml 의 global/userInfo 에 대응</summary>
    public sealed class UserInfo
    {
        private readonly Hashtable _v = new Hashtable();

        public string this[string key]
        {
            get { object o = _v[key]; return (o == null) ? "" : (string)o; }
            set { _v[key] = value; }
        }

        public string UserId { get { return this["USR_USRID"]; } }
        public string UserNm { get { return this["USR_USRNM"]; } }
        public string AgtCd { get { return this["USR_AGTCD"]; } }
        public string AgtNm { get { return this["USR_AGTNM"]; } }
        public string SessionNo { get { return this["SSN_SESSION"]; } set { this["SSN_SESSION"] = value; } }
        public string SysDate { get { return this["SYSDATE"]; } set { this["SYSDATE"] = value; } }
        public string Version { get { return this["VERSION"]; } }

        private static readonly string[] Fields = new string[] {
            "USR_AGTCD","USR_AGTCD_H","USR_AGTCD_K","USR_AGTNM",
            "USR_APPR1","USR_APPR2","USR_APPR3","USR_APPR4",
            "USR_BRNAR_H","USR_BRNAR_K","USR_BRNCD_H","USR_BRNCD_H_NM",
            "USR_BRNCD_K","USR_BRNCD_K_NM","USR_HK","USR_HK_D",
            "USR_MSTFL","USR_USRFL","USR_USRGR","USR_USRID","USR_USRNM","USR_USRTY",
            "USR_PDA_PIC_USE","USR_PDA_RCV_FL","USR_PDA_PIC_FL","USR_PDA_SCAN_FL",
            "USR_PDA_CASE_USE","USR_ZONE_VIRT",
            "USR_WHSM_FL","USR_WHSA_FL","USR_WHSB_FL","USR_WHSC_FL","USR_WHSD_FL",
            "USR_STDPRC_FL","USR_STD_LOC","USR_MINUS_FL","USR_PRTFL","USR_PRTUH",
            "USR_LOCPRT","USR_TEPRT","USR_OISANG",
            "STOCK_USEYN","LAST_LOG_DATE","SYSTEM_TIME","VERSION","DEL_YN","USR_SMARTPW"
        };

        /// <summary>필요한 필드만 골라 담는다. 응답 Row 는 호출 후 버려진다.</summary>
        public static UserInfo From(Row row)
        {
            UserInfo u = new UserInfo();
            for (int i = 0; i < Fields.Length; i++)
                u[Fields[i]] = row[Fields[i]];
            u["ORG_USR_USRID"] = u["USR_USRID"];
            return u;
        }
    }

    public static class Session
    {
        public static UserInfo User;
        public static string ServerName = "";
        public static bool IsLoggedIn { get { return User != null; } }
        public static void Clear() { User = null; }
    }

    /// <summary>
    /// 대리점 / 전문점 구분. 원본 /ui/ws/main/Sub.xml 메뉴 클릭 처리 :
    ///   USR_USRTY == "A"  -> 대리점. 203·204·250 (1B10·1B11·1B05) 차단 "대리점은 사용할 수 없는 메뉴입니다."
    ///   USR_USRTY != "A"  -> 전문점. 121 (1A05) 차단 "전문점은 사용할 수 없는 메뉴입니다."
    /// 개발 중에는 Enforce = false 로 두어 구분 없이 모든 메뉴를 연다.
    /// </summary>
    public static class UserKind
    {
        /// <summary>false : 개발 모드. 메뉴를 구분 없이 모두 연다. 운영 배포 때 true 로 바꾼다.</summary>
        public static bool Enforce = false;

        public static bool IsAgency
        {
            get { UserInfo u = Session.User; return u != null && u["USR_USRTY"].Trim() == "A"; }
        }

        /// <summary>전문점 전용 메뉴를 막아야 하면 안내 문구, 아니면 null</summary>
        public static string ShopOnlyBlock()
        {
            return (Enforce && IsAgency) ? "대리점은 사용할 수 없는 메뉴입니다." : null;
        }

        /// <summary>대리점 전용 메뉴를 막아야 하면 안내 문구, 아니면 null</summary>
        public static string AgencyOnlyBlock()
        {
            return (Enforce && !IsAgency) ? "전문점은 사용할 수 없는 메뉴입니다." : null;
        }
    }

    /// <summary>
    /// 로그인 시 내려오는 업체코드(ds_ven) 1건.
    /// VNDSB 가 "00000" 이면 주업체, 아니면 그 주업체에 딸린 부업체다.
    /// </summary>
    public sealed class VendorInfo
    {
        public readonly string VndMn;   // 주업체코드
        public readonly string VndSb;   // 부업체코드
        public readonly string Name;    // 업체명

        public VendorInfo(string mn, string sb, string nm)
        {
            VndMn = mn; VndSb = sb; Name = nm;
        }

        /// <summary>웹 콤보 표기와 동일하게 "코드|업체명"</summary>
        public override string ToString()
        {
            return (VndSb == "00000" ? VndMn : VndSb) + "|" + Name;
        }
    }
}
