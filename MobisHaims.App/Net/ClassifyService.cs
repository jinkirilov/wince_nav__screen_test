using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>
    /// [120] 일반입고분류 / [121] 전문점입고분류 구분.
    ///
    /// 두 화면은 원본 소스가 거의 같다. 실제로 갈리는 것은 아래 네 가지뿐이다.
    ///   WSFID        "M"(모비스) / ""(전문점)
    ///   재고등록 SQL  PL120_W01_I06 / PL121_W01_I01
    ///   HOLO_NO      MOBILEPDA_120 / MOBILEPDA_121
    ///   PGM          PDA120 / PL121
    /// 할당정보 갱신은 둘 다 PL120_W01_U01 을 쓴다(121 도 120 것을 쓴다).
    /// </summary>
    public sealed class ClassifyMode
    {
        public readonly int ScreenNo;
        public readonly string Name;
        public readonly string Pgm;
        public readonly string Wsfid;
        public readonly string SaveInvSql;
        public readonly string HoloNo;
        public readonly string PgmParam;
        public readonly bool ShowVendor;

        private ClassifyMode(int no, string name, string pgm, string wsfid,
                             string saveInvSql, string holoNo, string pgmParam, bool showVendor)
        {
            ScreenNo = no; Name = name; Pgm = pgm; Wsfid = wsfid;
            SaveInvSql = saveInvSql; HoloNo = holoNo; PgmParam = pgmParam; ShowVendor = showVendor;
        }

        public static readonly ClassifyMode Site = new ClassifyMode(
            120, "일반입고분류", "PL120_W01.xml", "M",
            "plus:PL120_W01_I06", "MOBILEPDA_120", "PDA120", false);

        public static readonly ClassifyMode Shop = new ClassifyMode(
            121, "전문점입고분류", "PL121_W01.xml", "",
            "plus:PL121_W01_I01", "MOBILEPDA_121", "PL121", true);
    }

    /// <summary>PL120_W01_S02(ds_Output02) + PL130_W01_S02(ds_List) 묶음</summary>
    public sealed class ClassifySearchResult
    {
        public Row Screen;                                  // ds_Output02 첫 레코드
        public readonly ArrayList Allocs = new ArrayList(); // ds_List (Row)

        public bool HasScreen { get { return Screen != null; } }
        public bool HasAlloc { get { return Allocs.Count > 0; } }
        public Row FirstAlloc { get { return Allocs.Count > 0 ? (Row)Allocs[0] : null; } }

        public string S(string col) { return Screen == null ? "" : Screen[col]; }
    }

    /// <summary>PL130_W01_S04 : 분류 전 중복 검증</summary>
    public sealed class ClassifyCheckResult
    {
        public bool Found;
        public int TotCnt;      // TOT_CNT
        public int VchnoCnt;    // WSF_VCHNO_CNT
    }

    /// <summary>
    /// [120] 일반입고분류 / [121] 전문점입고분류 서버 호출.
    ///
    ///   fn_SearchLep   plus:PL100_W01_S01        부번 -> 계열
    ///   fn_SearchClass plus:PL100_W01_S02        품명 / 등급 / VAP 기준일
    ///   fn_SaveInv     모드별 재고등록 SQL        등급이 없을 때 자동 호출
    ///   fn_Search      plus:PL120_W01_S02 + plus:PL130_W01_S02
    ///   fn_SaveChk     plus:PL130_W01_S04        분류 전 검증
    ///   fn_Save        plus:PL100_W01_S03 + S05  증표채번 + 업체집계 상태
    ///   fn_SaveAfter   쓰기 묶음                  CUR_STAT = "2"(분류)
    ///
    /// 커밋 구조는 [140] 과 닮았지만 CUR_STAT 이 "2" 이고,
    /// LOC 마스터(U04)와 마이너스 보정(P02)이 원본에서 주석 처리돼 있어 빠진다.
    /// </summary>
    public static class ClassifyService
    {
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
        // fn_SearchLep : 부번 -> 계열 목록 (응답 컬럼 WSF_LEP)
        // ------------------------------------------------------------------
        public static ArrayList SearchLep(ClassifyMode m, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL100_W01_S01");
            r.AddParam("AGTCD", Agt);
            r.AddParam("WSF_WSFID", m.Wsfid);
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_SearchLep", r.Build(),
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
        // fn_SearchClass : 품명 / 등급 / VAP 기준일 / 전표년월
        // ------------------------------------------------------------------
        public static ClassInfo SearchClass(ClassifyMode m, string lep, string ptno)
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

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_SearchClass", r.Build(),
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
        // fn_SaveInv : 등급 조회에 결과가 없을 때 재고를 만들어 준다 (쓰기)
        // ------------------------------------------------------------------
        public static void SaveInv(ClassifyMode m, string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch(m.SaveInvSql);
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("USRID", Usr);
            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_SaveInv", r.Build(), null);
            if (res.IsError)
                throw new HaimsException("시스템에러가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        // ------------------------------------------------------------------
        // fn_Search : 화면정보(ds_Output02) + 할당목록(ds_List)
        // ------------------------------------------------------------------
        public static ClassifySearchResult Search(ClassifyMode m, string lep, string ptno)
        {
            string key = PartNo.Key(ptno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL120_W01_S02");   // ds_Output02
            r.AddSearch("plus:PL130_W01_S02");   // ds_List

            r.AddParam("WSFID", m.Wsfid);
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", key);
            r.AddParam("WSF_AGTCD", Agt);
            r.AddParam("WSF_LEP", lep);
            r.AddParam("WSF_PTNO", key);
            AuthService.AddSessionCommon(r);

            ClassifySearchResult sr = new ClassifySearchResult();

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_Output02") { if (sr.Screen == null) sr.Screen = row; }
                    else if (ds == "ds_List") sr.Allocs.Add(row);
                });

            Check(res);
            return sr;
        }

        // ------------------------------------------------------------------
        // fn_SaveChk : plus:PL130_W01_S04 -> ds_SaveChk
        // ------------------------------------------------------------------
        public static ClassifyCheckResult SaveCheck(ClassifyMode m, string lep, string ptno,
                                                    ArrayList allocs)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL130_W01_S04");
            r.AddParam("WSF_AGTCD", Agt);
            r.AddParam("WSF_PTNO", PartNo.Key(ptno));
            r.AddParam("WSF_LEP", lep);
            r.AddParam("WSF_VCHNO", InboundService.QuoteVchnoList(allocs));
            r.AddParam("WSFID_TEMP", m.Wsfid);
            AuthService.AddSessionCommon(r);

            ClassifyCheckResult c = new ClassifyCheckResult();

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_SaveChk", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_SaveChk" || c.Found) return;
                    c.Found = true;
                    c.TotCnt = ToInt(row["TOT_CNT"]);
                    c.VchnoCnt = ToInt(row["WSF_VCHNO_CNT"]);
                });

            Check(res);
            return c;
        }

        // ------------------------------------------------------------------
        // fn_Save : 증표채번(S03) + 업체집계 상태(S05)
        // ------------------------------------------------------------------
        public static CommitPrepare CommitPrepareCall(ClassifyMode m, CommitInput c)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL100_W01_S03");
            r.AddSearch("plus:PL100_W01_S05");

            r.AddParam("WSF_WSFQT", c.WsfQt);
            r.AddParam("WSF_SALECST", c.WsfSalecst);
            r.AddParam("WSF_STAT", c.WsfStat);
            r.AddParam("WSFID", c.WsfId);
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(c.Ptno));
            r.AddParam("LEP", c.Lep);
            r.AddParam("CUR_STAT", "2");             // [140] 은 "3", 분류는 "2"
            r.AddParam("CTLQT", c.CtlQty.ToString());
            r.AddParam("VAT_RATE", c.VatRate);
            r.AddParam("CST_RATE", c.CstRate);
            r.AddParam("VCHCD", "1");
            r.AddParam("VCHYM", c.Vchym);
            r.AddParam("WSF_WSFID", c.WsfId);
            r.AddParam("WSF_VNDMN", c.WsfVndmn);
            r.AddParam("WSF_VNDSB", c.WsfVndsb);
            r.AddParam("VAP_SYSDT", c.VapSysdt);
            r.AddParam("VAP_SYSDT_L", c.VapSysdtL);
            AuthService.AddSessionCommon(r);

            CommitPrepare p = new CommitPrepare();
            bool[] g3 = new bool[1];
            bool[] g5 = new bool[1];

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_Save", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_PL100_03" && !g3[0]) { g3[0] = true; p.Vchno = row["VCHNO"]; }
                    else if (ds == "ds_PL100_05" && !g5[0])
                    {
                        g5[0] = true;
                        p.EndMtFlag = row["END_MT_FALG"];
                        p.VndMtFlag = row["VND_MT_FALG"];
                        p.VndDtFlag = row["VND_DT_FALG"];
                        p.LstValMpFlag = row["LST_VAL_MP_FALG"];
                    }
                });

            Check(res);
            if (!g5[0])
                throw new HaimsException(CommonCache.Msg("MP532", "할당정보가 없는 부품번호입니다."));

            return p;
        }

        // ------------------------------------------------------------------
        // fn_SaveAfter : 실제 쓰기
        //
        // [140] 과 달리 LOC 마스터(U04)와 마이너스 보정(P02)은 원본에서
        // 주석 처리돼 있어 넣지 않는다. 할당정보 갱신 SQL 도 PL120_W01_U01 이다.
        // ------------------------------------------------------------------
        public static void Commit(ClassifyMode m, CommitInput c, CommitPrepare p)
        {
            if (p == null) p = new CommitPrepare();

            if (p.EndMtFlag == "N")
                throw new HaimsException(CommonCache.Msg("MP542", "VAP 작업 후 진행하십시오."));

            string ptno = PartNo.Key(c.Ptno);
            string today = DateTime.Now.ToString("yyyyMMdd");

            TitRequest r = new TitRequest();

            // ---- SQL 을 원본 순서대로 -------------------------------------
            if (p.VndMtFlag == "N")
            {
                r.AddSearch("plus:PL100_W01_I03");
                r.AddSearch("plus:PL100_W01_I04");
            }
            else
            {
                if (p.VndDtFlag == "N") r.AddSearch("plus:PL100_W01_I03");
                else r.AddSearch("plus:PL100_W01_U05");

                r.AddSearch("plus:PL100_W01_U06");
            }

            r.AddSearch("plus:PL100_W01_U01");   // 재고 Master
            r.AddSearch("plus:PL120_W01_U01");   // 할당정보 (121 도 120 것을 쓴다)
            r.AddSearch("plus:PL100_W01_I01");   // 수불마스터
            r.AddSearch("plus:PL100_W01_I02");   // 수불세부내역

            // ---- 파라미터 ------------------------------------------------
            r.AddParam("AGTCD", Agt);
            r.AddParam("USRID", Usr);
            r.AddParam("PGM", m.PgmParam);
            r.AddParam("CUR_STAT", "2");
            r.AddParam("HOLO_NO", m.HoloNo);
            r.AddParam("LEP", c.Lep);
            r.AddParam("PTNO", ptno);
            r.AddParam("LOCNO", c.Locno);
            r.AddParam("CTLQT", c.CtlQty.ToString());
            r.AddParam("CTLCD", c.CtlCd);

            // 원본은 화면 창고 콤보가 아니라 할당 레코드의 LOC_WHSCD 를 보낸다
            r.AddParam("WHSCD", c.Whscd);

            r.AddParam("VAP_SYSDT", today);          // 여기서는 오늘 날짜다([140] 과 다름)
            r.AddParam("VAP_SYSDT_L", c.VapSysdtL);
            if (p.VndMtFlag != "N")
                r.AddParam("LST_VAL_MP_FALG", p.LstValMpFlag);

            r.AddParam("WSF_VNDMN", c.WsfVndmn);
            r.AddParam("WSF_VNDSB", c.WsfVndsb);
            r.AddParam("WSF_WSFQT", c.WsfQt);
            r.AddParam("WSF_SALECST", c.WsfSalecst);
            r.AddParam("WSF_WSFID", c.WsfId);
            r.AddParam("WSF_VCHNO", c.WsfVchno);
            r.AddParam("WSF_STAT", c.WsfStat);
            r.AddParam("WSF_SNDDT", today);
            r.AddParam("WSF_WSFDT", c.WsfDt);
            r.AddParam("WSF_CARCD", c.WsfCarcd);
            r.AddParam("VAT_RATE", c.VatRate);
            r.AddParam("CST_RATE", c.CstRate);
            r.AddParam("USR_AGTCD_H", UsrCol("USR_AGTCD_H"));
            r.AddParam("USR_AGTCD_K", UsrCol("USR_AGTCD_K"));

            // 이동단가(ds_PL100_04) 조회는 원본에서 주석 처리돼 있어 늘 빈 값이 나간다
            string[] cal = new string[] {
                "CAL_AVLQT", "CAL_AVPRC", "CAL_INPD_QT", "CAL_INV_AMT", "CAL_ODAMT",
                "CAL_ODQT_ITS", "CAL_ODAMT_MBS", "CAL_ODQT", "CAL_ODAMT_ITS",
                "CAL_ODQT_MBS", "CAL_CTLQT", "CAL_NOIVC_QT"
            };
            for (int i = 0; i < cal.Length; i++) r.AddParam(cal[i], "");

            r.AddParam("WSF_VCHNO_1", p.Vchno);
            r.AddParam("VCHYM", Mid(p.Vchno, 1, 6));
            r.AddParam("VCHSEQ", Tail(p.Vchno, 7));

            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(m.Pgm, "fn_SaveAfter", r.Build(), null);

            if (res.IsError)
                throw new HaimsException(CommonCache.Msg("MP108",
                    "시스템 오류가 발생하였습니다. 시스템 관리자에게 문의하세요."));
        }

        private static string Mid(string s, int start, int len)
        {
            if (s == null || s.Length < start + len) return "";
            return s.Substring(start, len);
        }

        private static string Tail(string s, int start)
        {
            if (s == null || s.Length <= start) return "";
            return s.Substring(start);
        }

        private static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }
    }
}
