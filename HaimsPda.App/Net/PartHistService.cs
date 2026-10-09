using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>[323] 수불내역 1행 (ds_List)</summary>
    public sealed class PartHistRow
    {
        public string Gubun = "";     // TRS_VCHNM  구분 (입고/출고 ...)
        public string Date = "";      // TRS_SALDT  날짜
        public string Whscd = "";     // TRS_WHSFL  창고
        public string Qty = "";       // TRS_SALQT  수량
        public string Vendor = "";    // VEN_VNDNM  거래처
    }

    /// <summary>[323] 조회 결과 (ds_List + ds_HisInfo + ds_RegLocInfo)</summary>
    public sealed class PartHistResult
    {
        public readonly ArrayList Rows = new ArrayList();   // PartHistRow

        // ds_HisInfo 첫 레코드
        public bool HasInfo;
        public string PartName = "";  // INV_PTNM_A
        public string Grade = "";     // UWMG_GRADE
        public string PrevQty = "";   // HIS_INVQT  전월
        public string TrsCnt = "";    // TRS_CNT    수불수량
        public string CurQty = "";    // CURRQT     현재고
        public string Cl = "";        // UDPM_CLASS
        public string Price = "";     // INV_AVPRC  단가

        // ds_RegLocInfo
        public string RegWhs = "";    // 모든 LOC_WHSCD 를 "," 로 이은 것 (등록창고)
        public string StdWh = "";     // LOC_LOCFL='Y' 인 행의 LOC_WHSCD
        public string StdLoc = "";    // LOC_LOCFL='Y' 인 행의 LOC_LOCNO
    }

    /// <summary>
    /// [323] 부품수불이력조회 서버 호출. 원본 : /ui/ws/plus/PL323_W01.xml (서버 메뉴 P134)
    ///
    ///   fn_SearchLep : plus:PL300_W01_S01 -> ds_Output01 / INV_LEP
    ///   fn_Search    : plus:PL323_W01_S01 + S04 + S06 -> ds_List / ds_HisInfo / ds_RegLocInfo
    ///
    /// 두 호출 모두 원본이 HAIMS_COMM_ACTION 으로 보낸다.
    /// 조회 전용 화면이라 쓰기 호출은 없다. HAR 실측 없음.
    /// </summary>
    public static class PartHistService
    {
        public const string Pgm = "PL323_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ------------------------------------------------------------------
        // fn_SearchLep
        // ------------------------------------------------------------------
        public static ArrayList SearchLep(string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL300_W01_S01");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r, AuthService.ActionComm);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLep", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01") return;
                    string v = row["INV_LEP"];
                    if (v.Length == 0) v = row["LEP"];
                    if (v.Length > 0 && !list.Contains(v)) list.Add(v);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // fn_Search
        //   baseDate : 조회일 yyyyMMdd
        //   BASE_BYM : 원본 shiftTime(basedt, 0, -1, 0, 0) -> 한 달 전 yyyyMMddHHmm (시분은 0000)
        // ------------------------------------------------------------------
        public static PartHistResult Search(string lep, string ptno, DateTime baseDate)
        {
            string key = PartNo.Key(ptno);
            string agt = UsrCol("USR_AGTCD");

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL323_W01_S01");
            r.AddSearch("plus:PL323_W01_S04");
            r.AddSearch("plus:PL323_W01_S06");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("INV_AGTCD", agt);
            r.AddParam("INV_PTNO", key);
            r.AddParam("INV_LEP", lep);
            r.AddParam("BASE_BYM", BaseBym(baseDate));
            r.AddParam("BASEDT", baseDate.ToString("yyyyMMdd"));
            r.AddParam("AGTCD", agt);
            r.AddParam("PTNO", key);
            r.AddParam("LEP", lep);
            r.AddParam("LOC_AGTCD", agt);
            r.AddParam("LOC_LEP", lep);
            r.AddParam("LOC_PTNO", key);
            AuthService.AddSessionCommon(r, AuthService.ActionComm);

            PartHistResult sr = new PartHistResult();
            System.Text.StringBuilder whs = new System.Text.StringBuilder();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_List")
                    {
                        PartHistRow h = new PartHistRow();
                        h.Gubun = row["TRS_VCHNM"];
                        h.Date = row["TRS_SALDT"];
                        h.Whscd = row["TRS_WHSFL"];
                        h.Qty = row["TRS_SALQT"];
                        h.Vendor = row["VEN_VNDNM"];
                        sr.Rows.Add(h);
                    }
                    else if (ds == "ds_HisInfo" && !sr.HasInfo)
                    {
                        sr.HasInfo = true;
                        sr.PartName = row["INV_PTNM_A"];
                        sr.Grade = row["UWMG_GRADE"];
                        sr.PrevQty = row["HIS_INVQT"];
                        sr.TrsCnt = row["TRS_CNT"];
                        sr.CurQty = row["CURRQT"];
                        sr.Cl = row["UDPM_CLASS"];
                        sr.Price = row["INV_AVPRC"];
                    }
                    else if (ds == "ds_RegLocInfo")
                    {
                        // 원본 fn_SetWhs : 창고는 전부 이어 붙이고, 표준 LOC 는 LOC_LOCFL='Y' 행
                        if (whs.Length > 0) whs.Append(",");
                        whs.Append(row["LOC_WHSCD"]);
                        if (row["LOC_LOCFL"] == "Y" && sr.StdLoc.Length == 0)
                        {
                            sr.StdWh = row["LOC_WHSCD"];
                            sr.StdLoc = row["LOC_LOCNO"];
                        }
                    }
                });

            Check(res);
            sr.RegWhs = whs.ToString();
            return sr;
        }

        /// <summary>
        /// 원본 shiftTime(yyyyMMdd, 0, -1, 0, 0). JS Date.setMonth 는 일자를 그대로 두고 넘치면
        /// 다음 달로 굴린다(3/31 -> 2/31 -> 3/3). 같은 결과가 나오도록 1일 기준으로 달을 빼고 일자를 더한다.
        /// </summary>
        public static string BaseBym(DateTime d)
        {
            DateTime t = new DateTime(d.Year, d.Month, 1).AddMonths(-1).AddDays(d.Day - 1);
            return t.ToString("yyyyMMdd") + "0000";
        }
    }
}
