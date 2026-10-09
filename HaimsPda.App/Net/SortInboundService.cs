using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>
    /// [141] 정렬입고저장 작업목록의 한 줄 (원본 grd 1행 + 그 부번의 hidGrd 행들).
    ///
    /// 원본은 화면용 grd 와 커밋용 hidGrd 두 그리드를 LEP+PTNO 로 묶어 썼다.
    /// 여기서는 한 객체에 같이 담는다.
    /// </summary>
    public sealed class SortItem
    {
        public string Lep = "";
        public string Ptno = "";       // 구분자 없는 키
        public string Grade = "";      // 등급 (원본 CLASS)
        public string PartName = "";
        public string Loc = "";        // 할당 첫 레코드의 LOC_LOCNO
        public string Qty = "";        // 표시용 수량 (단건 = 할당수량, 다건 = 선택 합계)
        public string CurInv = "";     // 가용재고 (ds_Output02 CURINV)

        // 등급 조회(PL100_W01_S02) 값. 원본은 hidGrd 의 각 행에 복사해 두었다.
        public string VapSysdt = "";
        public string VapSysdtL = "";
        public string Vchym = "";
        public string MinusHk = "";    // ds_Output01 첫 레코드 MINUS_HK

        /// <summary>커밋할 할당 레코드(Row). 원본 hidGrd 의 해당 부번 행들.</summary>
        public ArrayList Allocs = new ArrayList();

        public bool HasLoc { get { return Loc.Length > 0 && Loc != "M"; } }
    }

    /// <summary>[141] 조회 결과 (PL140_W01_S02 / S03)</summary>
    public sealed class SortSearchResult
    {
        public readonly ArrayList Allocs = new ArrayList();  // ds_Output01
        public Row Screen;                                   // ds_Output02 첫 레코드
        public string Screen_(string col) { return Screen == null ? "" : Screen[col]; }
        public Row FirstAlloc { get { return Allocs.Count > 0 ? (Row)Allocs[0] : null; } }
    }

    /// <summary>
    /// [141] 정렬입고저장 서버 호출.
    ///
    /// 원본 : /ui/ws/plus/PL141_W01.xml (메뉴 1A07).
    /// 계열/등급 조회는 140 과 같은 SQL 이라 InboundService 를 그대로 쓰고,
    /// 여기에는 141 에서만 다른 것들만 둔다.
    ///
    /// 140 과 커밋이 다른 점(원본 fn_Save 기준)
    ///   - 저장 전 중복검증(S07/S08)이 없다
    ///   - 증표 채번(fn_PreSearch)의 파라미터가 다르다 : VCHCD/VCHYM/AGTCD + 업체/VAP 일자
    ///   - LOCNO 는 할당 레코드가 아니라 작업자가 스캔한 LOC 다
    ///   - VCHYM 은 등급조회 VCHYM 앞 6자리, VCHSEQ 는 증표번호 끝 5자리
    ///   - PGM=PL141, HOLO_NO=MOBILEPDA_141. 분류완료(2) 건은 원본이 PGM 을 보내지 않는다
    ///   - 이동단가(CAL_*), WSF_SNDDT, WSF_WSFDT 를 보내지 않는다
    /// </summary>
    public static class SortInboundService
    {
        public const string Pgm = "PL141_W01.xml";

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
        // 재고 등록 : fn_SaveInv / plus:PL141_W01_I02
        // 등급 조회 결과가 없을 때 원본이 자동으로 부른다. 끝나면 등급을 다시 조회한다.
        // ------------------------------------------------------------------
        public static void SaveInv(string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL141_W01_I02");
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("USRID", Usr);
            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SaveInv", r.Build(), null);
            if (res.IsError)
                throw new HaimsException("시스템에러가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        // ------------------------------------------------------------------
        // 화면 조회 : fn_Search
        //   plus:PL140_W01_S02 -> ds_Output01 (할당리스트)
        //   plus:PL140_W01_S03 -> ds_Output02 (할당건수 / 가용재고)
        // ------------------------------------------------------------------
        public static SortSearchResult Search(string lep, string ptno, string whscd)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL140_W01_S02");
            r.AddSearch("plus:PL140_W01_S03");
            r.AddParam("AGTCD", Agt);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("WHSCD", whscd);
            AuthService.AddSessionCommon(r);

            SortSearchResult sr = new SortSearchResult();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_Output01") sr.Allocs.Add(row);
                    else if (ds == "ds_Output02" && sr.Screen == null) sr.Screen = row;
                });

            Check(res);
            return sr;
        }

        // ------------------------------------------------------------------
        // LOC 없음 + "아니오" : fn_SearchCheckInUp -> _After -> _After2
        //   1) plus:PL410_W01_S03 -> ds_Loc / DO_FLAG
        //   2) DO_FLAG=I 면 PL141_W01_I01, 아니면 PL410_W01_U01. 이어서 PL410_W01_I02
        // 기본 LOC "M" 으로 등록해 두는 쓰기 호출이다.
        // ds_Loc 이 비면 원본도 아무것도 하지 않는다.
        // ------------------------------------------------------------------
        public static bool RegisterDefaultLoc(string whscd, string lep, string ptno)
        {
            string key = PartNo.Key(ptno);

            TitRequest q = new TitRequest();
            q.AddSearch("plus:PL410_W01_S03");
            q.AddParam("AGTCD", Agt);
            q.AddParam("WHSCD", whscd);
            q.AddParam("LEP", lep);
            q.AddParam("PTNO", key);
            AuthService.AddSessionCommon(q);

            string[] flag = new string[1];
            bool[] got = new bool[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchCheckInUp", q.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Loc" || got[0]) return;
                    got[0] = true;
                    flag[0] = row["DO_FLAG"];
                });
            Check(res);

            if (!got[0]) return false;

            TitRequest r = new TitRequest();
            if (flag[0] == "I") r.AddSearch("plus:PL141_W01_I01");
            else r.AddSearch("plus:PL410_W01_U01");
            r.AddSearch("plus:PL410_W01_I02");

            r.AddParam("AGTCD", Agt);
            r.AddParam("WHSCD", whscd);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", key);
            r.AddParam("FROM_LOCNO", "");
            r.AddParam("TO_LOCNO", "M");
            r.AddParam("QTY", "0");
            r.AddParam("USRID", Usr);
            AuthService.AddSessionCommon(r);

            TitResult res2 = HaimsHttp.PostStream(Pgm, "fn_SearchCheckInUp_After", r.Build(), null);
            if (res2.IsError)
                throw new HaimsException("시스템 오류가 발생하였습니다. 시스템 관리자에게 문의하세요.");
            return true;
        }

        // ==================================================================
        // 입고 커밋 — 원본 fn_Save 의 for 루프 한 바퀴 = 할당 레코드 1건
        // ==================================================================

        /// <summary>
        /// fn_PreSearch : 증표번호(S03) + 업체집계 상태(S05).
        /// 분류완료(WSF_STAT=2) 건은 부르지 않는다.
        /// 두 데이터셋 중 하나라도 비면 원본은 MP555(증표번호 조회 오류)로 멈춘다.
        /// </summary>
        public static CommitPrepare Prepare(Row a, SortItem it)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL100_W01_S03");
            r.AddSearch("plus:PL100_W01_S05");
            r.AddParam("VCHCD", "1");
            r.AddParam("VCHYM", Left6(it.Vchym));
            r.AddParam("AGTCD", Agt);
            r.AddParam("WSF_WSFID", a["WSF_WSFID"]);
            r.AddParam("WSF_VNDMN", a["WSF_VNDMN"]);
            r.AddParam("WSF_VNDSB", a["WSF_VNDSB"]);
            r.AddParam("VAP_SYSDT", it.VapSysdt);
            r.AddParam("VAP_SYSDT_L", it.VapSysdtL);
            AuthService.AddSessionCommon(r);

            CommitPrepare p = new CommitPrepare();
            bool[] got = new bool[2];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_PreSearch", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_PL100_03" && !got[0])
                    {
                        got[0] = true;
                        p.Vchno = row["VCHNO"];
                    }
                    else if (ds == "ds_PL100_05" && !got[1])
                    {
                        got[1] = true;
                        p.EndMtFlag = row["END_MT_FALG"];
                        p.VndMtFlag = row["VND_MT_FALG"];
                        p.VndDtFlag = row["VND_DT_FALG"];
                        p.LstValMpFlag = row["LST_VAL_MP_FALG"];
                    }
                });
            Check(res);

            if (!got[0] || !got[1] || p.Vchno.Trim().Length == 0)
                throw new HaimsException(CommonCache.Msg("MP555", "증표번호 조회 오류입니다."));
            if (p.EndMtFlag == "N")
                throw new HaimsException(CommonCache.Msg("MP542", "VAP 작업 후 진행하십시오."));
            return p;
        }

        /// <summary>
        /// 할당 레코드 1건 커밋.
        /// locno 는 작업자가 스캔한 LOC (구분자 유무 상관없음).
        /// </summary>
        public static void Commit(Row a, SortItem it, string whscd, string locno, CommitPrepare p)
        {
            bool classified = (a["WSF_STAT"] == "2");
            string wsfId = a["WSF_WSFID"];
            string vat = (wsfId == "M") ? "1.1" : "1.0";
            string cst = (wsfId == "M") ? "1.0" : "1.1";
            string whs = (whscd == null || whscd.Length == 0) ? "M" : whscd;
            string vchno = classified ? a["WSF_VCHNO_1"] : p.Vchno;

            TitRequest r = new TitRequest();

            // ---- SQL : 원본이 쌓는 순서 그대로 ------------------------------
            if (classified)
            {
                r.AddSearch("plus:PL100_W01_U02");   // 수불마스터 창고코드
                r.AddSearch("plus:PL100_W01_U03");   // 수불세부 창고/LOC
            }
            else
            {
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
                r.AddSearch("plus:PL100_W01_I01");   // 수불마스터
                r.AddSearch("plus:PL100_W01_I02");   // 수불세부내역
            }
            r.AddSearch("plus:PL100_W01_U01");        // 재고 Master
            r.AddSearch("plus:PL100_W01_U04");        // Location Master
            r.AddSearch("plus:PL140_W01_U01");        // 할당내역
            if (it.MinusHk == "Y")
                r.AddSearch("plus:PL100_W01_P02");    // 마이너스재고 보정

            // ---- 파라미터 : 원본에서 같은 id 는 마지막 값만 남는다 ----------
            r.AddParam("AGTCD", Agt);
            r.AddParam("USRID", Usr);
            r.AddParam("CUR_STAT", "3");
            r.AddParam("CTLQT", "0");                  // 미수령 없음(원본 GV_NoInputQty 초기값)
            r.AddParam("WHSCD", whs);
            r.AddParam("LEP", it.Lep);
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            r.AddParam("LOCNO", Loc.Key(locno));
            r.AddParam("WSF_VCHNO", a["WSF_VCHNO"]);
            r.AddParam("WSF_STAT", a["WSF_STAT"]);
            r.AddParam("WSF_WSFQT", a["WSF_WSFQT"]);
            r.AddParam("WSF_SALECST", a["WSF_SALECST"]);
            r.AddParam("WSF_WSFID", wsfId);
            r.AddParam("WSFID", wsfId);
            r.AddParam("VAT_RATE", vat);
            r.AddParam("CST_RATE", cst);
            r.AddParam("VAP_SYSDT", it.VapSysdt);
            r.AddParam("VCHYM", Left6(it.Vchym));
            r.AddParam("WSF_VCHNO_1", vchno);
            r.AddParam("VCHSEQ", Right5(vchno));

            if (!classified)
            {
                r.AddParam("PGM", "PL141");
                r.AddParam("VAP_SYSDT_L", it.VapSysdtL);
                r.AddParam("WSF_VNDMN", a["WSF_VNDMN"]);
                r.AddParam("WSF_VNDSB", a["WSF_VNDSB"]);
                r.AddParam("USR_AGTCD_H", UsrCol("USR_AGTCD_H"));
                r.AddParam("USR_AGTCD_K", UsrCol("USR_AGTCD_K"));
                r.AddParam("WSF_CARCD", a["WSF_CARCD"]);
                r.AddParam("HOLO_NO", "MOBILEPDA_141");
                // 원본은 VND_MT_FALG 가 N 이 아닐 때(U06 경로)만 넣는다
                if (p.VndMtFlag != "N")
                    r.AddParam("LST_VAL_MP_FALG", p.LstValMpFlag);
            }

            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Save", r.Build(), null);
            if (res.IsError)
                throw new HaimsException("시스템 장애가 발생하였습니다. 시스템 관리자에게 문의하세요");
        }

        /// <summary>원본 gfn_Left(VCHYM, 6)</summary>
        private static string Left6(string s)
        {
            if (s == null) return "";
            return s.Length > 6 ? s.Substring(0, 6) : s;
        }

        /// <summary>원본 gfn_Right(증표번호, 5)</summary>
        private static string Right5(string s)
        {
            if (s == null) return "";
            s = s.Trim();
            return s.Length > 5 ? s.Substring(s.Length - 5) : s;
        }
    }
}
