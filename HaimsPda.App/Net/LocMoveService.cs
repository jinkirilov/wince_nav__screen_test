using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL401_W01_S01 : LOC 안의 부품 1행 (ds_Output01)</summary>
    public sealed class LocMoveItem
    {
        public string Lep = "";      // LOC_LEP
        public string Ptno = "";     // LOC_PTNO
        public string AvlQty = "";   // LOC_AVLQT  가능수량
        public string SalQty = "";   // SALQT      출고대기
        public string Class_ = "";   // UDPM_CLASS 수불등급
        public string Grade = "";    // UWMG_GRADE

        public int Avl { get { return ToInt(AvlQty); } }
        public int Sal { get { return ToInt(SalQty); } }

        public static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }

        /// <summary>계열/내역 선택 팝업에 보이는 문구</summary>
        public override string ToString()
        {
            return Lep + "  " + PartNo.Display(Ptno) + "  " + Class_ + "  재고 " + AvlQty + " / 출고 " + SalQty;
        }
    }

    /// <summary>
    /// [401] LOC재고이동 서버 호출. 원본 : /ui/ws/plus/PL401_W01.xml (메뉴 1D05 / P145)
    ///
    ///   LOC 조회   : fn_SearchLocFr        -> plus:PL401_W01_S01 -> ds_Output01
    ///   체인파트   : fn_Search_ChainPartAf -> plus:PL411_W01_S04 -> ds_List03 (C_PART='Y' 행)
    ///   이동 저장  : fn_SaveLoc            -> plus:PL401_W01_U01 + plus:PL401_W01_I01 (TYPE=N, 파라미터만)
    ///
    /// [원본 버그 - 따라 하지 않음]
    ///   fn_Search_ChainPartBe 가 콜백으로 자기 자신을 넘겨서 PTNO='[object Element]' -> 'undefined' 로
    ///   PL411_W01_S05 를 무한 재호출한다 (HAR 2026-10-05 10:44 에 수백 건). 결과도 쓰지 않으므로 S05 는 부르지 않는다.
    ///
    /// [ALL 모드] 원본 fn_SaveLoc2 는 TYPE=M + 입력 데이터셋(dsInPDA_401_U01 / _none) 이지만 실측이 없다.
    ///   U01/I01 이 받는 컬럼(LOC_LEP/LOC_PTNO, LEP/PTNO/QTY + 공통 파라미터)은 싱글과 같으므로
    ///   검증된 구조인 싱글 저장을 품목 수만큼 반복한다 (화면 쪽에서 반복).
    /// </summary>
    public static class LocMoveService
    {
        public const string Pgm = "PL401_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ------------------------------------------------------------------
        // LOC 조회 : 그 LOC 에 들어 있는 부품 목록. 0건이면 원본은 MP313(정확한 로케이션 코드가 아닙니다)
        // ------------------------------------------------------------------
        public static ArrayList SearchLoc(string whscd, string locno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL401_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("LOC_AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("LOC_WHSCD", whscd);
            r.AddParam("LOC_LOCNO", Loc.Key(locno));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLocFr", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01") return;
                    LocMoveItem it = new LocMoveItem();
                    it.Lep = row["LOC_LEP"];
                    it.Ptno = row["LOC_PTNO"];
                    it.AvlQty = row["LOC_AVLQT"];
                    it.SalQty = row["SALQT"];
                    it.Class_ = row["UDPM_CLASS"];
                    it.Grade = row["UWMG_GRADE"];
                    if (it.Ptno.Length > 0) list.Add(it);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // 체인파트 : C_PART='Y' 인 행의 후속부번(UDPM_AFT_PTNO)과 수량.
        // 원본은 있으면 [411] 체인파트 선택(P146) 화면으로 넘어가지만 그 화면 원본은 미확보.
        // 여기서는 "부번  수량" 문자열 목록만 돌려주고 화면이 안내만 한다.
        // ------------------------------------------------------------------
        public static ArrayList GetChainParts(string whscd, string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL411_W01_S04");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("LEP", lep);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search_ChainPartAf", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List03") return;
                    if (row["C_PART"] != "Y") return;
                    list.Add(PartNo.Display(row["UDPM_AFT_PTNO"]) + "  (" + row["LOC_AVLQT"] + ")");
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // 이동 저장 : 원본 fn_SaveLoc (싱글). 멀티/ALL 도 품목마다 이걸 부른다.
        // ------------------------------------------------------------------
        public static void Move(string whscd, string fromLoc, string toLoc, string lep, string ptno, string qty)
        {
            string agt = UsrCol("USR_AGTCD");
            string from = Loc.Key(fromLoc);
            string to = Loc.Key(toLoc);
            string key = PartNo.Key(ptno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL401_W01_U01");   // tit_AddSingleActionInfo = TYPE N
            r.AddSearch("plus:PL401_W01_I01");

            r.AddParam("LOC_AGTCD", agt);
            r.AddParam("LOC_WHSCD", whscd);
            r.AddParam("LOC_LOCNO", from);
            r.AddParam("LOC_LOCNO_TEMP", to);
            r.AddParam("LOC_PTNO", key);
            r.AddParam("LOC_LEP", lep);

            r.AddParam("FROM_LOCNO", from);
            r.AddParam("AGTCD", agt);
            r.AddParam("PTNO", key);
            r.AddParam("LEP", lep);
            r.AddParam("WHSCD", whscd);
            r.AddParam("TO_LOCNO", to);
            r.AddParam("QTY", qty);
            r.AddParam("USRID", UsrCol("USR_USRID"));
            AuthService.AddSessionCommon(r);

            string[] fail = new string[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SaveLoc", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "_none" || fail[0] != null) return;
                    if (row.Has("MSG_CODE") && row["MSG_CODE"] != "S") fail[0] = row["MSG"];
                });

            Check(res);

            if (fail[0] != null)
                throw new HaimsException(fail[0].Length > 0 ? fail[0] : "저장중 에러가 발생하였습니다.");
        }
    }
}
