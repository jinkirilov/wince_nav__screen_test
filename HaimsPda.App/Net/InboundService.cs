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
    /// 입고 커밋 1단계(fn_Save) 응답.
    ///   PL100_W01_S03 -> ds_PL100_03 / VCHNO        증표번호
    ///   PL100_W01_S05 -> ds_PL100_05 / *_FALG       업체집계내역(매입) 상태
    /// </summary>
    public sealed class CommitPrepare
    {
        public string Vchno = "";        // 새로 채번된 입고증표번호
        public string EndMtFlag = "";    // END_MT_FALG     월마감여부
        public string VndMtFlag = "";    // VND_MT_FALG     해당월 발생여부(거래처)
        public string VndDtFlag = "";    // VND_DT_FALG     해당일자 발생여부(거래처)
        public string LstValMpFlag = ""; // LST_VAL_MP_FALG 미지급금 마이너스
    }

    /// <summary>
    /// 입고 커밋에 필요한 값 묶음.
    /// 대부분 조회(ds_Output01) 레코드에서 그대로 옮겨 온 것이다.
    /// </summary>
    public sealed class CommitInput
    {
        public string Lep = "";
        public string Ptno = "";
        public string Whscd = "M";

        public string WsfStat = "";      // N.미처리 1.도착보고 2.분류 3.입고처리
        public string WsfSalecst = "";   // 원가
        public string WsfQt = "";        // 할당수량
        public string WsfId = "";        // 할당구분
        public string WsfVndmn = "";     // 거래처 메인코드
        public string WsfVndsb = "";     // 거래처 서브코드
        public string WsfVchno = "";     // 할당번호
        public string WsfVchno1 = "";    // 입고증표번호(이미 분류된 건)
        public string WsfCarcd = "";     // 차종코드
        public string WsfNoargQty = "";  // 재고무수량
        public string WsfDt = "";        // 할당일자
        public string WsfCasno = "";     // CASE 번호
        public string WsfReqcd = "";     // 할당코드
        public string WsfItscd = "";     // 지원센터코드
        public string Locno = "";        // LOC
        public string MinusHk = "";      // 마이너스재고 여부

        public string VapSysdt = "";
        public string VapSysdtL = "";
        public string Vchym = "";

        /// <summary>미수령 수량 / 사유코드. 미수령 화면이 없으면 0 / 빈값.</summary>
        public int CtlQty;
        public string CtlCd = "";

        /// <summary>할당구분 M 이면 부가세 1.1 / 원가율 1.0, 아니면 반대</summary>
        public string VatRate { get { return (WsfId == "M") ? "1.1" : "1.0"; } }
        public string CstRate { get { return (WsfId == "M") ? "1.0" : "1.1"; } }

        /// <summary>이미 분류(2)된 건은 증표를 새로 따지 않는다.</summary>
        public bool IsClassified { get { return WsfStat == "2"; } }

        public static CommitInput From(Row a)
        {
            CommitInput c = new CommitInput();
            c.WsfStat = a["WSF_STAT"];
            c.WsfSalecst = a["WSF_SALECST"];
            c.WsfQt = a["WSF_WSFQT"];
            c.WsfId = a["WSF_WSFID"];
            c.WsfVndmn = a["WSF_VNDMN"];
            c.WsfVndsb = a["WSF_VNDSB"];
            c.WsfVchno = a["WSF_VCHNO"];
            c.WsfVchno1 = a["WSF_VCHNO_1"];
            c.WsfCarcd = a["WSF_CARCD"];
            c.WsfNoargQty = a["WSF_NOARG_QTY"];
            c.WsfDt = a["WSF_WSFDT"];
            c.WsfCasno = a["WSF_CASNO"];
            c.WsfReqcd = a["WSF_REQCD"];
            c.WsfItscd = a["WSF_ITSCD"];
            c.Locno = a["LOC_LOCNO"];
            c.MinusHk = a["MINUS_HK"];
            return c;
        }
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

        // ==================================================================
        // 입고 커밋 — 원본 fn_Save -> fn_Save_After -> fn_SaveAfter2
        //
        // 두 번의 서버 호출로 끝난다.
        //   1) CommitPrepare : 증표번호 채번(S03) + 업체집계 상태 조회(S05)
        //   2) Commit        : 1)의 결과에 따라 INSERT/UPDATE 문들을 한 번에 실행
        //
        // 두 번째 호출은 ds_cmd 에 SQL 을 여러 개 쌓아 순서대로 돌린다.
        // 순서가 곧 실행 순서이므로 원본이 추가한 차례를 그대로 지킨다.
        // ==================================================================

        /// <summary>
        /// 1단계. 이미 분류(WSF_STAT=="2")된 건은 증표를 새로 따지 않으므로
        /// 원본도 조회 없이 넘어간다. 그 경우 호출자가 이 단계를 건너뛴다.
        /// </summary>
        public static CommitPrepare CommitPrepareCall(CommitInput c)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL100_W01_S03");   // 증표번호
            r.AddSearch("plus:PL100_W01_S05");   // 업체집계내역(매입) 상태

            r.AddParam("VAP_SYSDT", c.VapSysdt);
            r.AddParam("VAP_SYSDT_L", c.VapSysdtL);
            r.AddParam("WSF_WSFQT", c.WsfQt);
            r.AddParam("WSF_SALECST", c.WsfSalecst);
            r.AddParam("WSF_STAT", c.WsfStat);
            r.AddParam("WSFID", c.WsfId);
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(c.Ptno));
            r.AddParam("LEP", c.Lep);
            r.AddParam("CUR_STAT", "3");
            r.AddParam("CTLQT", c.CtlQty.ToString());
            r.AddParam("VAT_RATE", c.VatRate);
            r.AddParam("CST_RATE", c.CstRate);
            r.AddParam("VCHCD", "1");
            r.AddParam("VCHYM", c.Vchym);
            r.AddParam("WSF_WSFID", c.WsfId);
            r.AddParam("WSF_VNDMN", c.WsfVndmn);
            r.AddParam("WSF_VNDSB", c.WsfVndsb);
            AuthService.AddSessionCommon(r);

            CommitPrepare p = new CommitPrepare();
            bool[] got03 = new bool[1];
            bool[] got05 = new bool[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Save", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_PL100_03" && !got03[0])
                    {
                        got03[0] = true;
                        p.Vchno = row["VCHNO"];
                    }
                    else if (ds == "ds_PL100_05" && !got05[0])
                    {
                        got05[0] = true;
                        p.EndMtFlag = row["END_MT_FALG"];
                        p.VndMtFlag = row["VND_MT_FALG"];
                        p.VndDtFlag = row["VND_DT_FALG"];
                        p.LstValMpFlag = row["LST_VAL_MP_FALG"];
                    }
                });

            Check(res);
            return p;
        }

        /// <summary>
        /// 2단계. 실제 쓰기.
        ///
        /// 분류 완료건(WSF_STAT=="2")은 창고/LOC 갱신만 하고,
        /// 그 외에는 업체집계(매입) 상태에 따라 수불 마스터/세부내역을 새로 만든다.
        /// </summary>
        public static void Commit(CommitInput c, CommitPrepare p)
        {
            if (p == null) p = new CommitPrepare();

            string ptno = PartNo.Key(c.Ptno);
            string whs = (c.Whscd == null || c.Whscd.Length == 0) ? "M" : c.Whscd;
            string today = DateTime.Now.ToString("yyyyMMdd");

            TitRequest r = new TitRequest();

            // ---- 실행할 SQL 을 원본 순서대로 쌓는다 -----------------------
            if (c.IsClassified)
            {
                r.AddSearch("plus:PL100_W01_U02");   // 수불마스터 창고코드
                r.AddSearch("plus:PL100_W01_U03");   // 수불세부 창고/LOC
            }
            else
            {
                if (p.EndMtFlag == "N")
                    throw new HaimsException(CommonCache.Msg("MP542", "VAP 작업 후 진행하십시오."));

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

            if (c.MinusHk == "Y")
                r.AddSearch("plus:PL100_W01_P02");    // 마이너스재고 보정 프로시저

            // ---- 파라미터 ------------------------------------------------
            // 웹은 같은 id 를 여러 번 넣어도 마지막 값 하나만 남는다. 여기서도 한 번씩만 넣는다.
            r.AddParam("AGTCD", Agt);
            r.AddParam("USRID", Usr);
            r.AddParam("PGM", "PL140");
            r.AddParam("CUR_STAT", "3");
            r.AddParam("LEP", c.Lep);
            r.AddParam("PTNO", ptno);
            r.AddParam("WHSCD", whs);
            r.AddParam("LOCNO", c.Locno);
            r.AddParam("CTLQT", c.CtlQty.ToString());
            r.AddParam("CTLCD", c.CtlCd);
            r.AddParam("WSF_CARCD", c.WsfCarcd);
            r.AddParam("LST_VAL_MP_FALG", c.IsClassified ? "" : p.LstValMpFlag);

            if (!c.IsClassified)
                r.AddParam("HOLO_NO", "MOBILEPDA_140");

            r.AddParam("VAP_SYSDT", c.VapSysdt);
            r.AddParam("VAP_SYSDT_L", c.VapSysdtL);
            r.AddParam("WSF_VNDMN", c.WsfVndmn);
            r.AddParam("WSF_VNDSB", c.WsfVndsb);
            r.AddParam("WSF_WSFQT", c.WsfQt);
            r.AddParam("WSF_SALECST", c.WsfSalecst);
            r.AddParam("WSF_WSFID", c.WsfId);
            r.AddParam("WSF_VCHNO", c.WsfVchno);
            r.AddParam("WSF_STAT", c.WsfStat);
            r.AddParam("WSF_SNDDT", today);
            r.AddParam("WSF_WSFDT", c.WsfDt);
            r.AddParam("VAT_RATE", c.VatRate);
            r.AddParam("CST_RATE", c.CstRate);
            r.AddParam("USR_AGTCD_H", UsrCol("USR_AGTCD_H"));
            r.AddParam("USR_AGTCD_K", UsrCol("USR_AGTCD_K"));

            // 이동단가(ds_PL100_04)는 원본에서 조회가 주석 처리돼 있어 늘 빈 값이 나간다.
            // 서버가 빈 값을 전제로 동작하므로 그대로 빈 값을 보낸다.
            string[] cal = new string[] {
                "CAL_AVLQT", "CAL_AVPRC", "CAL_INPD_QT", "CAL_INV_AMT", "CAL_ODAMT",
                "CAL_ODQT_ITS", "CAL_ODAMT_MBS", "CAL_ODQT", "CAL_ODAMT_ITS",
                "CAL_ODQT_MBS", "CAL_CTLQT", "CAL_NOIVC_QT"
            };
            for (int i = 0; i < cal.Length; i++) r.AddParam(cal[i], "");

            // 증표번호. 분류 완료건은 이미 받아 둔 번호를, 아니면 방금 채번한 번호를 쓴다.
            string vchnoSrc = c.IsClassified ? c.WsfVchno1 : p.Vchno;
            r.AddParam("WSF_VCHNO_1", p.Vchno);
            r.AddParam("VCHYM", Mid(vchnoSrc, 1, 6));
            r.AddParam("VCHSEQ", Tail(vchnoSrc, 7));

            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Save_After", r.Build(), null);

            if (res.IsError)
                throw new HaimsException("시스템 오류가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        /// <summary>증표번호에서 년월을 뗀다(웹의 substr(1,6)).</summary>
        private static string Mid(string s, int start, int len)
        {
            if (s == null || s.Length < start + len) return "";
            return s.Substring(start, len);
        }

        /// <summary>증표번호에서 일련번호를 뗀다(웹의 substr(7)).</summary>
        private static string Tail(string s, int start)
        {
            if (s == null || s.Length <= start) return "";
            return s.Substring(start);
        }


        private static int ToInt(string s)
        {
            if (s == null || s.Length == 0) return 0;
            try { return int.Parse(s); } catch { return 0; }
        }
    }
}
