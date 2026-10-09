using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>
    /// [130] 대기품목 한 줄.
    ///
    /// 원본 그리드는 한 레코드를 2줄로 펼쳐 보여준다(헤더가 "일자/수량" 처럼 슬래시로 묶여 있다).
    /// CF ListView 는 1레코드 1줄이라 여기서 평평하게 편다.
    /// </summary>
    public sealed class WaitPartRow
    {
        public string Date = "";     // WSF_SNDDT   일자
        public string Qty = "";      // WSF_WSFQT   수량
        public string Vchno = "";    // WSF_VCHNO   할당번호
        public string CaseNo = "";   // WSF_CASNO   CASE(입고대기) / 입고번호(저장대기)
        public string Loc = "";      // LOC_LOCNO
        public string Stat = "";     // WSF_STAT    입고상태
        public string ReqCd = "";    // WSF_REQCD   할당코드(입고대기) / 분류자(저장대기)
        public string Vchno1 = "";   // WSF_VCHNO_1 입증표
        public string Vchno2 = "";   // WSF_VCHNO_2 출증표(예)
        public string RsvNo = "";    // WSF_RSVNO   예약번호
        public string WsfId = "";    // WSF_WSFID   할당구분
        public string VndMn = "";    // WSF_VNDMN   거래처 메인코드
        public string VndSb = "";    // WSF_VNDSB   거래처 서브코드
    }

    /// <summary>[130] 조회 결과 — 목록 + 할당수량 합계</summary>
    public sealed class WaitPartResult
    {
        public readonly ArrayList Rows = new ArrayList();
        public int TotalQty;

        public bool HasAny { get { return Rows.Count > 0; } }
    }

    /// <summary>
    /// [130] 대기품목조회 (원본 PL130_W01.xml).
    ///
    /// 라디오(rdoCheck)로 두 모드가 완전히 갈린다. SQL 도 응답 데이터셋도 다르다.
    ///
    ///   입고대기(IN)   fn_SearchLep plus:PL100_W01_S01
    ///                  fn_Search    plus:PL130_W01_S02          -> ds_List
    ///   저장대기(SAVE) fn_SearchLep plus:PL140_W01_S01
    ///                  fn_Search    plus:PL140_W01_S02 + S03    -> ds_Output01
    ///
    /// 조회 전용 화면이다.
    /// </summary>
    public static class WaitPartService
    {
        public const string Pgm = "PL130_W01.xml";

        /// <summary>rdoCheck 값</summary>
        public const string ModeIn = "IN";      // 입고대기
        public const string ModeSave = "SAVE";  // 저장대기

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
        // fn_SearchLep : 부번 -> 계열 목록
        //
        // 모드에 따라 SQL 이 다르다. 응답은 둘 다 ds_Output01 / WSF_LEP.
        // ------------------------------------------------------------------
        public static ArrayList SearchLep(string mode, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch(IsIn(mode) ? "plus:PL100_W01_S01" : "plus:PL140_W01_S01");
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
        // fn_SearchClass : 등급 (원본은 품명은 안 쓰고 등급만 채운다)
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
        // fn_Search : 목록
        // ------------------------------------------------------------------
        public static WaitPartResult Search(string mode, string lep, string ptno)
        {
            string key = PartNo.Key(ptno);
            bool isIn = IsIn(mode);

            TitRequest r = new TitRequest();

            if (isIn)
            {
                r.AddSearch("plus:PL130_W01_S02");
                r.AddParam("WSF_AGTCD", Agt);
                r.AddParam("WSF_LEP", lep);
                r.AddParam("WSF_PTNO", key);
                r.AddParam("WSFID", "M");
            }
            else
            {
                r.AddSearch("plus:PL140_W01_S02");
                r.AddSearch("plus:PL140_W01_S03");
                r.AddParam("AGTCD", Agt);
                r.AddParam("LEP", lep);
                r.AddParam("PTNO", key);
                r.AddParam("WSFID", "M");
            }

            AuthService.AddSessionCommon(r);

            // 모드마다 목록이 실려 오는 데이터셋이 다르다
            string target = isIn ? "ds_List" : "ds_Output01";

            WaitPartResult result = new WaitPartResult();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != target) return;

                    WaitPartRow w = isIn ? FromIn(row) : FromSave(row);
                    result.Rows.Add(w);
                    result.TotalQty += ParseInt(row["WSF_WSFQT"]);
                });

            Check(res);
            return result;
        }

        /// <summary>입고대기(ds_List) — 컬럼명이 그대로다.</summary>
        private static WaitPartRow FromIn(Row row)
        {
            WaitPartRow w = new WaitPartRow();
            w.Date = row["WSF_SNDDT"];
            w.Qty = row["WSF_WSFQT"];
            w.Vchno = row["WSF_VCHNO"];
            w.CaseNo = row["WSF_CASNO"];
            w.Loc = row["LOC_LOCNO"];
            w.Stat = row["WSF_STAT"];
            w.ReqCd = row["WSF_REQCD"];
            w.Vchno1 = row["WSF_VCHNO_1"];
            w.Vchno2 = row["WSF_VCHNO_2"];
            w.RsvNo = row["WSF_RSVNO"];
            w.WsfId = row["WSF_WSFID"];
            w.VndMn = row["WSF_VNDMN"];
            w.VndSb = row["WSF_VNDSB"];
            return w;
        }

        /// <summary>
        /// 저장대기(ds_Output01) — 원본이 응답 XML 의 태그명을 치환해서 쓴다.
        ///
        ///   WSF_SODT     -> WSF_SNDDT   (일자)
        ///   WSF_VCHNO_1  -> WSF_CASNO   (입고번호가 CASE 자리에 들어간다)
        ///   USR_USRNM    -> WSF_REQCD   (분류자)
        ///   WSF_CASNO    -> WSF_CASNO_Temp  (원래 CASE 는 밀려나 화면에 안 쓰인다)
        ///
        /// 여기서는 태그를 바꾸지 않고 읽을 때 옮겨 담는다.
        /// </summary>
        private static WaitPartRow FromSave(Row row)
        {
            WaitPartRow w = new WaitPartRow();
            w.Date = row["WSF_SODT"];
            w.Qty = row["WSF_WSFQT"];
            w.Vchno = row["WSF_VCHNO"];
            w.CaseNo = row["WSF_VCHNO_1"];   // 입고번호
            w.Loc = row["LOC_LOCNO"];
            w.Stat = row["WSF_STAT"];
            w.ReqCd = row["USR_USRNM"];      // 분류자
            w.Vchno1 = row["WSF_VCHNO_1"];
            w.Vchno2 = row["WSF_VCHNO_2"];
            w.RsvNo = row["WSF_RSVNO"];
            w.WsfId = row["WSF_WSFID"];
            w.VndMn = row["WSF_VNDMN"];
            w.VndSb = row["WSF_VNDSB"];
            return w;
        }

        private static bool IsIn(string mode)
        {
            return mode == null || mode.Length == 0 || mode == ModeIn;
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
