using System;
using System.Collections;

namespace HaimsPda.Net
{
    // ======================================================================
    // 재물조사 3종 공용 서버 호출
    //   [310] 재물조사대상조회  PL310_W01.xml (서버 메뉴 P131)
    //   [302] 재물조사          PL302_W01.xml (서버 메뉴 P192)  <- 310 에서 선택해 진입
    //   [301] 재물조사(LOC)     PL301_W01.xml (서버 메뉴 P188)
    //
    // 세 화면 모두 원본이 HAIMS_MOBILE_ACTION 으로 보낸다.
    // HAR 실측 없음 (원본 XML 소스에서 옮김). 저장 전문은 실기 확인 필요.
    // ======================================================================

    /// <summary>재물조사 구분. 원본 selIscRsGubun / ISC_RS_GUBUN</summary>
    public static class InvGubun
    {
        public const string All = "";
        public const string Occasional = "S";  // 수시
        public const string Regular = "R";     // 정기
        public const string Annual = "H";      // 연간
        public const string Special = "T";     // 특별
        public const string Grade = "G";       // 등급

        public static readonly string[] Codes = { "", "S", "R", "H", "T", "G" };
        public static readonly string[] Names = { "", "수시", "정기", "연간", "특별", "등급" };

        public static string Name(string code)
        {
            code = (code == null) ? "" : code.Trim();
            for (int i = 1; i < Codes.Length; i++) if (Codes[i] == code) return Names[i];
            return code;
        }

        public static string Code(string name)
        {
            name = (name == null) ? "" : name.Trim();
            for (int i = 1; i < Names.Length; i++) if (Names[i] == name || Codes[i] == name) return Codes[i];
            return "";
        }

        /// <summary>
        /// 원본의 CREDT 규칙. date 는 yyyyMMdd.
        ///   연간 : yyyy   특별 : yyyy + "0001"   등급 : yyyyMM   그 외 : yyyyMMdd
        /// </summary>
        public static string Credt(string code, string date)
        {
            date = (date == null) ? "" : date.Replace("-", "").Trim();
            if (code == Annual) return Left(date, 4);
            if (code == Special) return Left(date, 4) + "0001";
            if (code == Grade) return Left(date, 6);
            return date;
        }

        private static string Left(string s, int n)
        {
            return (s.Length <= n) ? s : s.Substring(0, n);
        }
    }

    /// <summary>재물조사 완료구분. 원본 selCust / STAT</summary>
    public static class InvStat
    {
        public static readonly string[] Codes = { "", "C", "P", "Y", "D" };
        public static readonly string[] Names = { "", "생성", "진행중", "완료", "삭제" };

        public static string Name(string code)
        {
            code = (code == null) ? "" : code.Trim();
            for (int i = 1; i < Codes.Length; i++) if (Codes[i] == code) return Names[i];
            return code;
        }
    }

    /// <summary>[310] ds_LocInfo 1행</summary>
    public sealed class InvTargetRow
    {
        public string Seq = "";     // ISC_SEQ       재물조사번호
        public string Cnt = "";     // ISC_CNT       건수
        public string Stat = "";    // STAT          완료
        public string Gubun = "";   // ISC_RS_GUBUN  구분 (코드)
        public string Zone = "";    // ISC_ZONE      ZONE
    }

    /// <summary>[310] -> [302] 로 넘기는 작업 키 (원본 ds_LinkInfo DATE/SALENO/WHSCD/SELECT/ISC_ZONE)</summary>
    public sealed class InvJob
    {
        public string Date = "";    // 생성일자 yyyyMMdd (원본 GV_CREDT)
        public string Seq = "";     // 재물조사번호
        public string Whscd = "";
        public string Gubun = "";   // 구분 코드 (S/R/H/T/G)
        public string Zone = "";

        public string Credt { get { return InvGubun.Credt(Gubun, Date); } }
    }

    /// <summary>[302] dsOutPDA_310_S01 첫 레코드 (지금 셀 품목 1건)</summary>
    public sealed class InvItem
    {
        public bool Found;
        public string Lep = "";       // ISR_LEP
        public string Ptno = "";      // ISR_PTNO
        public string PartName = "";  // INV_PTNM_A
        public string Grade = "";     // UWMG_GRADE
        public string ItemCnt = "";   // ITEM_CNT      남은 품목수
        public string Amt = "";       // ISR_AMT       단가
        public string InvAvlQt = "";  // INV_AVLQT     재고수량 (원본 화면에 표시 안 함)
        public string LocAvlQt = "";  // LOC_AVLQT     LOC수량  (원본 화면에 표시 안 함, 차이수량 기준)
        public string Whscd = "";     // ISR_WHSCD
        public string Zone = "";      // ISR_ZONE
        public string Locno = "";     // LOC_LOCNO
        public string SalQt = "";     // TRS_SALQT     출고대기
        public string LocnoSeq = "";  // ISR_LOCNO_SEQ (연간/등급 다음 품목 조회 키)
    }

    /// <summary>[301] dsOutPDA_301_S01 1행 (LOC 안 부품)</summary>
    public sealed class InvLocRow
    {
        public string Lep = "";       // LOC_LEP
        public string Ptno = "";      // LOC_PTNO
        public string AvlQt = "";     // LOC_AVLQT   재고
        public string LocFl = "";     // LOC_LOCFL   통합
        public string Price = "";     // INV_AVPRC   단가
        public string Merge = "";     // INV_MERGE
        public string PartName = "";  // INV_PTNM_A
        public string SalQt = "";     // TRS_SALQT   출고대기
        public string Class_ = "";    // UDPM_CLASS
        public string SkipFlag = "";  // SKIP_FLAG
    }

    /// <summary>[301] LOC 조회 결과</summary>
    public sealed class InvLocResult
    {
        public readonly ArrayList Rows = new ArrayList();   // InvLocRow
        public ArrayList Locs;                               // string (LOC_LOCNO). S06 을 안 보냈으면 null
    }

    /// <summary>[301]/[302] 이상코드 (dsOutPDA_310_S04)</summary>
    public sealed class InvReason
    {
        public string Code = "";      // CDM_COD_ID
        public string Name = "";      // CDM_CODNM

        public override string ToString() { return Code + "  " + Name; }
    }

    /// <summary>[301] 저장 구분 판정 (PL301_W01_S05 -> dsOutPDA_310_S05)</summary>
    public sealed class InvSaveChk
    {
        public bool Found;
        public string AdjFg = "";     // CCH_ADJFG
        public string InsDtm = "";    // CCH_INS_DTM
    }

    public static class InventoryService
    {
        public const string Pgm310 = "PL310_W01.xml";
        public const string Pgm302 = "PL302_W01.xml";
        public const string Pgm301 = "PL301_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ==================================================================
        // [310] fn_Search
        //   특별/등급 : PL310_W01_S21 (AGTCD, CREDT, RS_GUBUN, STAT)  — 창고/사용자 조건 없음
        //   그 외     : PL310_W01_S11 (AGTCD, CREDT, WHSCD, USERID, RS_GUBUN, STAT)
        // ==================================================================
        public static ArrayList SearchTargets(string whscd, DateTime date, string gubun, string stat)
        {
            gubun = (gubun == null) ? "" : gubun;
            stat = (stat == null) ? "" : stat;
            string credt = InvGubun.Credt(gubun, date.ToString("yyyyMMdd"));

            TitRequest r = new TitRequest();
            if (gubun == InvGubun.Special || gubun == InvGubun.Grade)
            {
                r.AddSearch("plus:PL310_W01_S21");
                r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
                r.AddParam("CREDT", credt);
                r.AddParam("RS_GUBUN", gubun);
                r.AddParam("STAT", stat);
            }
            else
            {
                r.AddSearch("plus:PL310_W01_S11");
                r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
                r.AddParam("CREDT", credt);
                r.AddParam("WHSCD", whscd);
                r.AddParam("USERID", UsrCol("USR_USRID"));
                r.AddParam("RS_GUBUN", gubun);
                r.AddParam("STAT", stat);
            }
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm310, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_LocInfo") return;
                    InvTargetRow t = new InvTargetRow();
                    t.Seq = row["ISC_SEQ"];
                    t.Cnt = row["ISC_CNT"];
                    t.Stat = row["STAT"];
                    t.Gubun = row["ISC_RS_GUBUN"];
                    t.Zone = row["ISC_ZONE"];
                    list.Add(t);
                });

            Check(res);
            return list;
        }

        // ==================================================================
        // [302] fn_Search : PL302_W01_S31 -> dsOutPDA_310_S01 (첫 레코드만 쓴다)
        //   ptno      : null 이면 다음 품목 (원본 fn_Search(false)), 있으면 그 부번 (fn_Search(true))
        //   locnoSeq  : 연간/등급에서 직전 조회의 ISR_LOCNO_SEQ (원본 SearChYn=="Y" 일 때만). 없으면 null
        //   locno     : 연간에서 직전 조회의 LOC_LOCNO
        // ==================================================================
        public static InvItem SearchItem(InvJob job, string ptno, string locnoSeq, string locno)
        {
            string g = job.Gubun;
            string credt = job.Credt;

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL302_W01_S31");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            if (g == InvGubun.Grade)
            {
                r.AddParam("CREDT", credt);
                r.AddParam("RS_GUBUN", InvGubun.Grade);
                if (locnoSeq != null) r.AddParam("ISR_LOCNO_SEQ", locnoSeq);
            }
            r.AddParam("SEQ", job.Seq);
            r.AddParam("WHSCD", job.Whscd);
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            if (g != InvGubun.Grade) r.AddParam("ISC_ZONE", job.Zone);

            if (g == InvGubun.Annual)
            {
                r.AddParam("CREDT", credt);
                r.AddParam("RS_GUBUN", InvGubun.Annual);
                if (locnoSeq != null)
                {
                    r.AddParam("ISR_LOCNO_SEQ", locnoSeq);
                    r.AddParam("LOC_LOCNO", (locno == null) ? "" : Loc.Key(locno));
                }
            }
            else if (g == InvGubun.Special)
            {
                r.AddParam("CREDT", credt);
                r.AddParam("RS_GUBUN", InvGubun.Special);
            }
            else if (g == InvGubun.Occasional || g == InvGubun.Regular)
            {
                r.AddParam("CREDT", credt);
            }

            if (ptno != null) r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            InvItem it = new InvItem();

            TitResult res = HaimsHttp.PostStream(Pgm302, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "dsOutPDA_310_S01" || it.Found) return;
                    it.Found = true;
                    it.Lep = row["ISR_LEP"];
                    it.Ptno = row["ISR_PTNO"];
                    it.PartName = row["INV_PTNM_A"];
                    // 원본은 UWMG_GRADE 를 읽지만 실제 응답에는 UDPM_CLASS 만 온다 (HAR 2026-10-11 14:13)
                    it.Grade = row["UWMG_GRADE"];
                    if (it.Grade.Length == 0) it.Grade = row["UDPM_CLASS"];
                    it.ItemCnt = row["ITEM_CNT"];
                    it.Amt = row["ISR_AMT"];
                    it.InvAvlQt = row["INV_AVLQT"];
                    it.LocAvlQt = row["LOC_AVLQT"];
                    it.Whscd = row["ISR_WHSCD"];
                    it.Zone = row["ISR_ZONE"];
                    it.Locno = row["LOC_LOCNO"];
                    it.SalQt = row["TRS_SALQT"];
                    it.LocnoSeq = row["ISR_LOCNO_SEQ"];
                });

            Check(res);
            return it;
        }

        // ==================================================================
        // [302] fn_Save : PL302_W01_U21 + PL302_W01_U02 (한 번에)
        //
        // 원본은 U21 용 파라미터를 넣은 뒤 U02 용을 다시 넣는다. tit_AddInputParamData 는
        // 같은 id 를 덮어쓰므로 두 SQL 이 같은 값을 받는다. 그대로 재현한다 (SetParam).
        // 특히 특별은 SEQ 가 마지막에 "001" 로 덮여 U21 도 "001" 로 나간다.
        //
        //   result : 차이 없음 "Y" / 이상코드 등록 "N"
        // ==================================================================
        public static void SaveItem(InvJob job, InvItem it, string doQt, string osdQt,
                                    string rescd, string result)
        {
            string g = job.Gubun;
            string agt = UsrCol("USR_AGTCD");
            string usr = UsrCol("USR_USRID");
            string whs = (it.Whscd.Length > 0) ? it.Whscd : job.Whscd;

            TitRequest r = new TitRequest();

            // --- U21
            r.AddSearch("plus:PL302_W01_U21");
            r.SetParam("DO_QT", doQt);
            r.SetParam("OSD_QT", osdQt);
            r.SetParam("RESCD", rescd);
            r.SetParam("WHSCD", whs);
            r.SetParam("RESULT", result);
            r.SetParam("USRID", usr);
            r.SetParam("AGTCD", agt);
            SetCredt(r, g, job);
            r.SetParam("SEQ", job.Seq);
            r.SetParam("LEP", it.Lep);
            r.SetParam("PTNO", PartNo.Key(it.Ptno));

            // --- U02
            r.AddSearch("plus:PL302_W01_U02");
            r.SetParam("OBJ_QT", it.LocAvlQt);
            r.SetParam("WHSCD", whs);
            r.SetParam("AMT", it.Amt);
            r.SetParam("USRID", usr);
            r.SetParam("AGTCD", agt);
            SetCredt(r, g, job);
            r.SetParam("SEQ", (g == InvGubun.Special) ? "001" : job.Seq);

            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm302, "fn_Save", r.Build(), null);
            if (res.IsError)
                throw new HaimsException("시스템 오류가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        private static void SetCredt(TitRequest r, string g, InvJob job)
        {
            // 원본 분기 순서 : 연간(CREDT,RS_GUBUN) / 특별(CREDT,RS_GUBUN) / 등급(RS_GUBUN,CREDT) / 그 외(CREDT)
            if (g == InvGubun.Grade)
            {
                r.SetParam("RS_GUBUN", InvGubun.Grade);
                r.SetParam("CREDT", job.Credt);
            }
            else if (g == InvGubun.Annual || g == InvGubun.Special)
            {
                r.SetParam("CREDT", job.Credt);
                r.SetParam("RS_GUBUN", g);
            }
            else
            {
                r.SetParam("CREDT", job.Credt);
            }
        }

        // ==================================================================
        // [301]/[302] 이상코드 : PL301_W01_S04 -> dsOutPDA_310_S04
        // ==================================================================
        public static ArrayList GetReasons(string pgm)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL301_W01_S04");
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(pgm, "fn_getCode", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "dsOutPDA_310_S04") return;
                    InvReason c = new InvReason();
                    c.Code = row["CDM_COD_ID"];
                    c.Name = row["CDM_CODNM"];
                    list.Add(c);
                });

            Check(res);
            return list;
        }

        // ==================================================================
        // [301] fn_getList : PL301_W01_S01 (+ S06 : 이어서 셀 LOC 목록)
        // ==================================================================
        public static InvLocResult SearchLoc(string whscd, string locno, bool withLocList)
        {
            string agt = UsrCol("USR_AGTCD");
            string loc = Loc.Key(locno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL301_W01_S01");
            r.AddParam("LOC_AGTCD", agt);
            r.AddParam("LOC_WHSCD", whscd);
            r.AddParam("LOC_LOCNO", loc);
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            if (withLocList)
            {
                r.AddSearch("plus:PL301_W01_S06");
                r.AddParam("AGTCD", agt);
                r.AddParam("LOCNO", loc);
                r.AddParam("WHSCD", whscd);
            }
            AuthService.AddSessionCommon(r);

            InvLocResult sr = new InvLocResult();
            if (withLocList) sr.Locs = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm301, "fn_getList", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "dsOutPDA_301_S01")
                    {
                        InvLocRow p = new InvLocRow();
                        p.Lep = row["LOC_LEP"];
                        p.Ptno = row["LOC_PTNO"];
                        p.AvlQt = row["LOC_AVLQT"];
                        p.LocFl = row["LOC_LOCFL"];
                        p.Price = row["INV_AVPRC"];
                        p.Merge = row["INV_MERGE"];
                        p.PartName = row["INV_PTNM_A"];
                        p.SalQt = row["TRS_SALQT"];
                        p.Class_ = row["UDPM_CLASS"];
                        p.SkipFlag = row["SKIP_FLAG"];
                        sr.Rows.Add(p);
                    }
                    else if (ds == "dsOutPDA_301_S06" && sr.Locs != null)
                    {
                        string v = row["LOC_LOCNO"];
                        if (v.Length > 0) sr.Locs.Add(v);
                    }
                });

            Check(res);
            return sr;
        }

        // ==================================================================
        // [301] fn_SaveChk : PL301_W01_S05 -> dsOutPDA_310_S05
        // ==================================================================
        public static InvSaveChk SaveCheck(string whscd, string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL301_W01_S05");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("WHSFL", whscd);
            AuthService.AddSessionCommon(r);

            InvSaveChk c = new InvSaveChk();

            TitResult res = HaimsHttp.PostStream(Pgm301, "fn_SaveChk", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "dsOutPDA_310_S05" || c.Found) return;
                    c.Found = true;
                    c.AdjFg = row["CCH_ADJFG"];
                    c.InsDtm = row["CCH_INS_DTM"];
                });

            Check(res);
            return c;
        }

        // ==================================================================
        // [301] fn_Save : update ? PL301_W01_U01 : PL301_W01_I01
        // ==================================================================
        public static void SaveLoc(bool update, string whscd, string locno, InvLocRow p,
                                   string cntQt, string cchDt, string rescd)
        {
            TitRequest r = new TitRequest();
            r.AddSearch(update ? "plus:PL301_W01_U01" : "plus:PL301_W01_I01");
            r.AddParam("CCH_AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("CCH_LEP", p.Lep);
            r.AddParam("CCH_PTNO", PartNo.Key(p.Ptno));
            r.AddParam("CCH_WHSFL", whscd);
            r.AddParam("CCH_LOCNO", Loc.Key(locno));
            r.AddParam("CCH_LOCQT", p.AvlQt);
            r.AddParam("CCH_DT", cchDt);
            r.AddParam("CCH_CNTQT", cntQt);
            r.AddParam("CCH_RESCD", rescd);
            r.AddParam("USERID", UsrCol("USR_USRID"));
            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm301, "fn_Save", r.Build(), null);
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }
    }
}
