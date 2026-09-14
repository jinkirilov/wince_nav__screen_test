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
