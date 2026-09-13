using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL320_W01_S01 : ds_LocInfo 1행 (LOC 안에 들어 있는 부품)</summary>
    public sealed class LocPartRow
    {
        public string Pos = "";       // POS         구역(표준/피킹)
        public string Lep = "";       // LOC_LEP     계열
        public string Ptno = "";      // LOC_PTNO    부품번호
        public string AvlQty = "";    // LOC_AVLQT   수량
        public string CarCd = "";     // HPM_CARCD   차종
        public string Ams = "";       // INV_3AMS    AMS
        public string Class_ = "";    // UDPM_CLASS  수불코드 (숨김 컬럼)
    }

    /// <summary>PL320_W01_S02 : ds_PartForLoc 1건 (상세내역 팝업)</summary>
    public sealed class LocPartDetail
    {
        public bool Found;
        public string Grade = "";      // UWMG_GRADE   수불코드
        public string PartName = "";   // INV_PTNM_A   품명
        public string AvlQty = "";     // INV_AVLQT    재고수량
        public string Ams = "";        // INV_3AMS     AMS
        public string SftQty = "";     // INV_SFTQT    안전
        public string InpdQty = "";    // INV_INPD_QT  저장대기
        public string SalQty = "";     // TRS_SALQT    출고대기
        public string OsdQty = "";     // CIH_OSD_QT   통제재고
        public string Fault = "";      // FAULT        미수령
        public string Price = "";      // INV_AVPRC    단가
        public string VhcKind = "";    // UDPM_VHC_KND 차종
    }

    /// <summary>
    /// [320] LOC별재고 서버 호출. 원본 : /ui/ws/plus/PL320_W01.xml
    ///
    /// fn_LocSearch -> plus:PL320_W01_S01 -> ds_LocInfo
    ///
    /// POS 규칙 (원본 그대로)
    ///   표준유 : POS="L", LOC 는 앞 9자리만 보낸다
    ///   표준무 : LOC 9자리면 POS="L" (POS컬럼 표시), 아니면 POS="P" (POS컬럼 숨김)
    /// SEL_GUBUN : 재고유="P" / 재고무=""
    /// </summary>
    public static class LocStockService
    {
        public const string Pgm = "PL320_W01.xml";

        public const string GubunHasStock = "P";
        public const string GubunAll = "";

        public const string PosStd = "L";
        public const string PosPick = "P";

        private static string Agt
        {
            get { UserInfo u = Session.User; return u == null ? "" : u["USR_AGTCD"]; }
        }

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ------------------------------------------------------------------
        // 창고 콤보 : plus.js gfn_SearchWHS_Plus / plus:PL000_W01_S01 -> ds_Output
        // ------------------------------------------------------------------
        public static ArrayList GetWarehouses()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL000_W01_S01");
            r.AddParam("USR_USRID", UsrCol("USR_USRID"));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "gfn_SearchWHS_Plus", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output") return;
                    string v = row["LOC_WHSCD"];
                    if (v.Length > 0) list.Add(v);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // LOC 조회 : fn_LocSearch / plus:PL320_W01_S01
        // ------------------------------------------------------------------
        public static ArrayList Search(string whscd, string locno, string pos, string selGubun)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL320_W01_S01");
            r.AddParam("LOC_AGTCD", Agt);
            r.AddParam("LOC_WHSCD", whscd);
            r.AddParam("LOC_LOCNO", Loc.Key(locno));
            r.AddParam("SEL_GUBUN", selGubun);
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("POS", pos);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_LocSearch", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_LocInfo") return;
                    LocPartRow p = new LocPartRow();
                    p.Pos = row["POS"];
                    p.Lep = row["LOC_LEP"];
                    p.Ptno = row["LOC_PTNO"];
                    p.AvlQty = row["LOC_AVLQT"];
                    p.CarCd = row["HPM_CARCD"];
                    p.Ams = row["INV_3AMS"];
                    p.Class_ = row["UDPM_CLASS"];
                    list.Add(p);
                });

            Check(res);
            return list;
        }
    }
}

namespace HaimsPda.Net
{
    /// <summary>
    /// [322] 재고세부내역 서버 호출. 원본 : /ui/ws/plus/PL320_P01.xml (320 의 팝업)
    ///
    /// fn_Search -> plus:PL320_W01_S02 -> ds_PartForLoc (1건)
    ///
    /// 원본은 tit_CallService 의 액션명이 HAIMS_MOBILE_ACTION 이지만,
    /// 실제 전문에는 pgm/function 과 SQL_ID 만 실려 나가므로 우리 쪽에서는 구분이 없다.
    /// LOC 은 파라미터로 보내지 않는다 (부번+계열로만 조회).
    /// </summary>
    public static class LocDetailService
    {
        public const string Pgm = "PL320_P01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        public static LocPartDetail Search(string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL320_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("LEP", lep);
            AuthService.AddSessionCommon(r);

            LocPartDetail d = new LocPartDetail();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_PartForLoc") return;
                    if (d.Found) return;               // 원본도 record[1] 만 쓴다

                    d.Found = true;
                    d.Grade = row["UWMG_GRADE"];
                    d.PartName = row["INV_PTNM_A"];
                    d.AvlQty = row["INV_AVLQT"];
                    d.Ams = row["INV_3AMS"];
                    d.SftQty = row["INV_SFTQT"];
                    d.InpdQty = row["INV_INPD_QT"];
                    d.SalQty = row["TRS_SALQT"];
                    d.OsdQty = row["CIH_OSD_QT"];
                    d.Fault = row["FAULT"];
                    d.Price = row["INV_AVPRC"];
                    d.VhcKind = row["UDPM_VHC_KND"];
                });

            if (res.IsError) throw new HaimsException(res.ErrorMsg);
            return d;
        }
    }
}
