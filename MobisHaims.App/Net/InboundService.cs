using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL100_W01_S02 : 등급 / 품명 / 전표년월</summary>
    public sealed class ClassInfo
    {
        public bool Found;
        public string Grade = "";      // UWMG_GRADE
        public string PartName = "";   // INV_PTNM_A
        public string VapSysdt = "";   // VAP_SYSDT
        public string VapSysdtL = "";  // VAP_SYSDT_L
        public string Vchym = "";      // VCHYM
    }

    /// <summary>PL140_W01_S02 / S03 / S05 묶음 결과</summary>
    public sealed class InboundSearchResult
    {
        public readonly ArrayList Allocs = new ArrayList();  // ds_Output01 (Row)
        public Row Screen;                                   // ds_Output02 첫 레코드
        public string MinusQty = "0";                        // ds_Minus / MINUS_QTY

        public bool HasAlloc { get { return Allocs.Count > 0; } }
        public Row FirstAlloc { get { return Allocs.Count > 0 ? (Row)Allocs[0] : null; } }

        public string Screen_(string col) { return Screen == null ? "" : Screen[col]; }
    }

    /// <summary>PL140_W01_S07 / S08 : 저장 전 중복입고 검증</summary>
    public sealed class SaveCheckResult
    {
        public int TotalCnt;      // ds_Output08 / TOTAL_CNT  (이미 입고된 건수)
        public bool HasOutput07;
        public int TotCnt;        // ds_Output07 / TOT_CNT
        public int VchnoCnt;      // ds_Output07 / WSF_VCHNO_CNT
    }

    /// <summary>
    /// [140] 입고저장 서버 호출.
    ///
    /// 원본 : /ui/ws/plus/PL140_W01.xml 의 fn_* 함수들.
    /// 모든 호출이 POST /Main?pgm=PL140_W01.xml&amp;function={함수명} 형태이며,
    /// 응답은 HaimsHttp.PostStream 이 레코드 단위로 흘려준다(_dsForSqlLog 는 버림).
    /// </summary>
    public static class InboundService
    {
        public const string Pgm = "PL140_W01.xml";

        private static string Agt
        {
            get { UserInfo u = Session.User; return u == null ? "" : u["USR_AGTCD"]; }
        }

        private static string Usr
        {
            get { UserInfo u = Session.User; return u == null ? "" : u["USR_USRID"]; }
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
        // 창고 콤보 : plus.js gfn_SearchWHS_Plus / plus:PL000_W01_S01
        // ------------------------------------------------------------------
        public static ArrayList GetWarehouses()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL000_W01_S01");
            r.AddParam("USR_USRID", Usr);
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
        // 부번 -> 계열(LEP) 조회 : fn_SearchLep / plus:PL140_W01_S01
        // 응답 컬럼은 WSF_LEP (웹은 XML 을 LEP 로 치환해서 씀)
        // ------------------------------------------------------------------
        public static ArrayList SearchLep(string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL140_W01_S01");
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLep", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01") return;
                    string v = row["WSF_LEP"];
                    if (v.Length == 0) v = row["LEP"];
                    if (v.Length > 0) list.Add(v);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // 등급 / 품명 : fn_SearchClass / plus:PL100_W01_S02
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
        // 화면 조회 : fn_Search
        //   plus:PL140_W01_S02 -> ds_Output01 (할당리스트)
        //   plus:PL140_W01_S03 -> ds_Output02 (화면정보)
        //   plus:PL140_W01_S05 -> ds_Minus    (무재고 수량)
        // ------------------------------------------------------------------
        public static InboundSearchResult Search(string lep, string ptno, string whscd)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL140_W01_S02");
            r.AddSearch("plus:PL140_W01_S03");
            r.AddSearch("plus:PL140_W01_S05");
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("WHSCD", whscd);
            AuthService.AddSessionCommon(r);

            InboundSearchResult sr = new InboundSearchResult();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_Output01") sr.Allocs.Add(row);
                    else if (ds == "ds_Output02" && sr.Screen == null) sr.Screen = row;
                    else if (ds == "ds_Minus")
                    {
                        string q = row["MINUS_QTY"];
                        if (q.Length > 0) sr.MinusQty = q;
                    }
                });

            Check(res);
            return sr;
        }

        // ------------------------------------------------------------------
        // 저장 전 검증 : fn_SaveChk
        //   plus:PL140_W01_S07 -> ds_Output07
        //   plus:PL140_W01_S08 -> ds_Output08
        // wsfVchnoList 는 "'A','B'" 형태로 따옴표까지 포함해 보낸다(원본 JS 동일).
        // ------------------------------------------------------------------
        public static SaveCheckResult SaveCheck(string ptno, string wsfVchnoList,
                                                string vchym, string vndmn, string vndsb)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL140_W01_S07");
            r.AddSearch("plus:PL140_W01_S08");
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("WSF_VCHNO", wsfVchnoList);
            r.AddParam("VCHYM", vchym);
            r.AddParam("WSF_VNDMN", vndmn);
            r.AddParam("WSF_VNDSB", vndsb);
            AuthService.AddSessionCommon(r);

            SaveCheckResult c = new SaveCheckResult();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SaveChk", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_Output08") c.TotalCnt = ToInt(row["TOTAL_CNT"]);
                    else if (ds == "ds_Output07" && !c.HasOutput07)
                    {
                        c.HasOutput07 = true;
                        c.TotCnt = ToInt(row["TOT_CNT"]);
                        c.VchnoCnt = ToInt(row["WSF_VCHNO_CNT"]);
                    }
                });

            Check(res);
            return c;
        }

        // ------------------------------------------------------------------
        // 재고 등록 : fn_SaveInv / plus:PL140_W01_I02
        // 등급 조회에 결과가 없을 때 원본이 자동으로 부르는 쓰기 호출이다.
        // ------------------------------------------------------------------
        public static void SaveInv(string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL140_W01_I02");
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("USRID", Usr);
            AuthService.AddSessionCommon(r);

            Check(HaimsHttp.PostStream(Pgm, "fn_SaveInv", r.Build(), null));
        }

        /// <summary>할당번호 목록을 "'A','B'" 형태로 만든다 (fn_SaveChk 파라미터 규격).</summary>
        public static string QuoteVchnoList(ArrayList allocs)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < allocs.Count; i++)
            {
                Row row = (Row)allocs[i];
                if (sb.Length > 0) sb.Append(",");
                sb.Append("'").Append(row["WSF_VCHNO"]).Append("'");
            }
            return sb.ToString();
        }

        private static int ToInt(string s)
        {
            if (s == null || s.Length == 0) return 0;
            try { return int.Parse(s); } catch { return 0; }
        }
    }
}
