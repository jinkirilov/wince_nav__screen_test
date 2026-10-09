using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL321_W01_S01 : 파트별 재고 헤더</summary>
    public sealed class PartStockInfo
    {
        public bool Found;
        public string Grade = "";      // UWMG_GRADE  수불코드
        public string PartName = "";   // INV_PTNM_A  품명
        public string AvlQty = "";     // INV_AVLQT   재고
        public string Price = "";      // INV_AVPRC   단가
        public string DefQty = "";     // INV_DEFQT   결함
        public string DoQty = "";      // INV_DOQT    D/O
        public string Ams = "";        // INV_3AMS    AMS
        public string SftQty = "";     // INV_SFTQT   안전
        public string VhcKind = "";    // UDPM_VHC_KND 차종
        public string StdInQty = "";   // WSF_STD_INQT 저장대기
    }

    /// <summary>PL321_W01_S02 : LOC 별 재고 1행</summary>
    public sealed class LocStockRow
    {
        public string Whscd = "";      // LOC_WHSCD
        public string Locno = "";      // LOC_LOCNO
        public string AvlQty = "";     // LOC_AVLQT
    }

    /// <summary>fn_SearchPTNO 결과</summary>
    public sealed class PartStockResult
    {
        public readonly PartStockInfo Info = new PartStockInfo();
        public readonly ArrayList Locs = new ArrayList();   // LocStockRow
        public bool HasAny { get { return Info.Found || Locs.Count > 0; } }
    }

    /// <summary>
    /// [321] 파트별재고 서버 호출. 원본 : /ui/ws/plus/PL321_W01.xml
    ///
    /// SEL_GUBUN 은 화면의 재고유/재고무 토글이다.
    ///   "재고유" -> "P" (재고 있는 것만),  "재고무" -> "" (전체)
    /// </summary>
    public static class StockService
    {
        public const string Pgm = "PL321_W01.xml";

        public const string GubunHasStock = "P";
        public const string GubunAll = "";

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
        // 부번 -> 계열(LEP) : fn_SearchLep / plus:PL321_W01_S03
        // 응답 컬럼은 INV_LEP (웹은 XML 을 LEP 로 치환해서 씀)
        // ------------------------------------------------------------------
        public static ArrayList SearchLep(string ptno, string selGubun)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL321_W01_S03");
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("SEL_GUBUN", selGubun);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLep", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01") return;
                    string v = row["INV_LEP"];
                    if (v.Length == 0) v = row["LEP"];
                    if (v.Length > 0) list.Add(v);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // 본조회 : fn_SearchPTNO
        //   plus:PL321_W01_S01 -> dsOutPDA_321_S01 (헤더 1건)
        //   plus:PL321_W01_S02 -> dsOutPDA_321_S02 (LOC 목록)
        // ------------------------------------------------------------------
        public static PartStockResult SearchPart(string lep, string ptno, string selGubun)
        {
            string key = PartNo.Key(ptno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL321_W01_S01");
            r.AddSearch("plus:PL321_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("INV_AGTCD", Agt);
            r.AddParam("INV_PTNO", key);
            r.AddParam("INV_LEP", lep);
            r.AddParam("LOC_AGTCD", Agt);
            r.AddParam("LOC_PTNO", key);
            r.AddParam("LOC_LEP", lep);
            r.AddParam("SEL_GUBUN", selGubun);
            AuthService.AddSessionCommon(r);

            PartStockResult sr = new PartStockResult();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchPTNO", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "dsOutPDA_321_S01")
                    {
                        if (sr.Info.Found) return;
                        sr.Info.Found = true;
                        sr.Info.Grade = row["UWMG_GRADE"];
                        sr.Info.PartName = row["INV_PTNM_A"];
                        sr.Info.AvlQty = row["INV_AVLQT"];
                        sr.Info.Price = row["INV_AVPRC"];
                        sr.Info.DefQty = row["INV_DEFQT"];
                        sr.Info.DoQty = row["INV_DOQT"];
                        sr.Info.Ams = row["INV_3AMS"];
                        sr.Info.SftQty = row["INV_SFTQT"];
                        sr.Info.VhcKind = row["UDPM_VHC_KND"];
                        sr.Info.StdInQty = row["WSF_STD_INQT"];
                    }
                    else if (ds == "dsOutPDA_321_S02")
                    {
                        LocStockRow l = new LocStockRow();
                        l.Whscd = row["LOC_WHSCD"];
                        l.Locno = row["LOC_LOCNO"];
                        l.AvlQty = row["LOC_AVLQT"];
                        sr.Locs.Add(l);
                    }
                });

            Check(res);
            return sr;
        }
    }
}
