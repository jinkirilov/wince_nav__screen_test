using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>
    /// [330] PL330_W01_S01 -> ds_PartForLoc 첫 레코드.
    /// 저장에 그대로 실어 보내는 값들이다(원본 m_str* 전역변수).
    /// </summary>
    public sealed class AdjustInfo
    {
        public bool Found;
        public string Avprc = "";     // AVPRC     이동평균단가
        public string Carcd = "";     // CARCD     차종
        public string CurDate = "";   // CUR_DATE
        public string CurTime = "";   // CUR_TIME
        public string CurYm = "";     // CUR_YM    (원본도 저장에는 쓰지 않는다)
        public string TrfVchno = "";  // TRF_VCHNO 수불 증표번호
        public string Vchseq = "";    // VCHSEQ
    }

    /// <summary>[330] 저장 입력값</summary>
    public sealed class AdjustInput
    {
        public string Lep = "";
        public string Ptno = "";      // 표기/원본 아무거나 (내부에서 Key 로 변환)
        public string Whscd = "";
        public string Locno = "";     // 표기/원본 아무거나 (내부에서 Key 로 변환)
        public string AdjQty = "";    // ADJQT  조정처리수량 (음수 가능)
    }

    /// <summary>
    /// [330] 재고조정 서버 호출. 원본 : /ui/ws/plus/PL330_W01.xml (서버 메뉴 P190)
    ///
    ///   fn_Search : plus:PL330_W01_S01 -> ds_PartForLoc
    ///               INV_ZERO_PLUS = LOC수량 0 에서 증가 조정이면 "Y", 아니면 "N"
    ///   fn_Save   : plus:PL330_W01_I01 -> I02 -> U01 -> U02 (tit_AddSingleActionInfo 4건, 파라미터만)
    ///
    /// 저장 전문은 HAR 실측이 없다(캡처 때 저장하지 않음). 실기 확인 필요.
    /// </summary>
    public static class StockAdjustService
    {
        public const string Pgm = "PL330_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        /// <summary>원본 getUserInfo("STOCK_USEYN") : 헤임즈 HS05 에서 HV02(재고임의수정) 권한이 있어야 Y</summary>
        public static bool HasPermission
        {
            get { return UsrCol("STOCK_USEYN").Trim() == "Y"; }
        }

        // ------------------------------------------------------------------
        // fn_Search
        // ------------------------------------------------------------------
        public static AdjustInfo Search(string lep, string ptno, string zeroPlus)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL330_W01_S01");
            r.AddParam("INV_ZERO_PLUS", zeroPlus);
            r.AddParam("USR_USRTY", UsrCol("USR_USRTY"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            AdjustInfo info = new AdjustInfo();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_PartForLoc" || info.Found) return;
                    info.Found = true;
                    info.Avprc = row["AVPRC"];
                    info.Carcd = row["CARCD"];
                    info.CurDate = row["CUR_DATE"];
                    info.CurTime = row["CUR_TIME"];
                    info.CurYm = row["CUR_YM"];
                    info.TrfVchno = row["TRF_VCHNO"];
                    info.Vchseq = row["VCHSEQ"];
                });

            Check(res);
            return info;
        }

        // ------------------------------------------------------------------
        // fn_Save
        // ------------------------------------------------------------------
        public static void Save(AdjustInput i, AdjustInfo info)
        {
            if (i == null) throw new ArgumentNullException("i");
            if (info == null) throw new ArgumentNullException("info");

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL330_W01_I01");
            r.AddSearch("plus:PL330_W01_I02");
            r.AddSearch("plus:PL330_W01_U01");
            r.AddSearch("plus:PL330_W01_U02");

            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("USRID", UsrCol("USR_USRID"));
            r.AddParam("WHSCD", (i.Whscd == null || i.Whscd.Length == 0) ? "M" : i.Whscd);
            r.AddParam("USR_AGTCD_H", UsrCol("USR_AGTCD_H"));
            r.AddParam("USR_AGTCD_K", UsrCol("USR_AGTCD_K"));
            r.AddParam("PTNO", PartNo.Key(i.Ptno));
            r.AddParam("LEP", i.Lep);
            r.AddParam("LOCNO", Loc.Key(i.Locno));
            r.AddParam("ADJQT", i.AdjQty.Trim());
            r.AddParam("AVPRC", info.Avprc);
            r.AddParam("CARCD", info.Carcd);
            r.AddParam("CUR_DATE", info.CurDate);
            r.AddParam("CUR_TIME", info.CurTime);
            r.AddParam("TRF_VCHNO", info.TrfVchno);
            r.AddParam("VCHSEQ", info.Vchseq);
            r.AddParam("VCHYM", Mid(info.TrfVchno, 1, 6));   // 원본 m_strTrfVchno.substr(1, 6)
            r.AddParam("HOLO_NO", "MOBILEPDA_330");
            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Save", r.Build(), null);
            if (res.IsError)
                throw new HaimsException("시스템 오류가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        private static string Mid(string s, int start, int len)
        {
            if (s == null || s.Length < start + len) return "";
            return s.Substring(start, len);
        }
    }
}
