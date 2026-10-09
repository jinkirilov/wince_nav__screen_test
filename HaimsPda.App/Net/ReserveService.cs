using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>ds_List 한 줄 — 업체별 예약 내역</summary>
    public sealed class ReserveRow
    {
        public string VndMn = "";   // RSV_VNDMN  업체
        public string VndNm = "";   // VEN_VNDNM  업체명
        public string Qty = "";     // RSVQT      수량
        public string GrtNo = "";   // RSV_GRTNO  차량코드
    }

    /// <summary>[131] 조회 결과 — 목록과 수량 합계</summary>
    public sealed class ReserveResult
    {
        public readonly ArrayList Rows = new ArrayList();
        public int TotalQty;

        public bool HasAny { get { return Rows.Count > 0; } }
    }

    /// <summary>
    /// [131] 예약내역 (원본 PL131_W01.xml).
    ///
    ///   fn_SearchLep   plus:PL131_W01_S01 -> ds_Output01 / RSV_LEP
    ///   fn_SearchClass plus:PL100_W01_S02 -> ds_Output01 / INV_PTNM_A, UWMG_GRADE
    ///   fn_Search      plus:PL131_W01_S02 -> ds_List
    ///
    /// 조회 전용 화면이라 저장 경로가 없다.
    /// </summary>
    public static class ReserveService
    {
        public const string Pgm = "PL131_W01.xml";

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
        // fn_SearchLep : 부번 -> 예약이 걸린 계열 목록
        //
        // 응답 컬럼은 RSV_LEP 다. 웹은 응답 XML 의 문자열을 통째로 치환해서
        // LEP 로 바꿔 쓰는데, 여기서는 그냥 RSV_LEP 를 읽는다.
        // ------------------------------------------------------------------
        public static ArrayList SearchLep(string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL131_W01_S01");
            r.AddParam("RSV_AGTCD", Agt);
            r.AddParam("RSV_PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLep", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01") return;
                    string v = row["RSV_LEP"];
                    if (v.Length == 0) v = row["LEP"];
                    if (v.Length > 0) list.Add(v);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // fn_SearchClass : 품명 / 등급
        //
        // SQL 은 [140] 과 같은 plus:PL100_W01_S02 지만 pgm 이 달라 별도로 둔다.
        // ------------------------------------------------------------------
        public static ClassInfo SearchClass(string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL100_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ClassInfo info = new ClassInfo();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchClass", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01" || info.Found) return;
                    info.Found = true;
                    info.Grade = row["UWMG_GRADE"];
                    info.PartName = row["INV_PTNM_A"];
                    info.VapSysdt = row["VAP_SYSDT"];
                    info.VapSysdtL = row["VAP_SYSDT_L"];
                    info.Vchym = row["VCHYM"];
                });

            Check(res);
            return info;
        }

        // ------------------------------------------------------------------
        // fn_Search : 업체별 예약 목록
        // ------------------------------------------------------------------
        public static ReserveResult Search(string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL131_W01_S02");
            r.AddParam("RSV_AGTCD", Agt);
            r.AddParam("RSV_LEP", lep);
            r.AddParam("RSV_PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ReserveResult result = new ReserveResult();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List") return;

                    ReserveRow rr = new ReserveRow();
                    rr.VndMn = row["RSV_VNDMN"];
                    rr.VndNm = row["VEN_VNDNM"];
                    rr.Qty = row["RSVQT"];
                    rr.GrtNo = row["RSV_GRTNO"];

                    result.Rows.Add(rr);
                    result.TotalQty += ParseInt(rr.Qty);
                });

            Check(res);
            return result;
        }

        private static int ParseInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }
    }
}
