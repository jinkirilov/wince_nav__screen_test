using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL430_W01_S01 : ds_PL430_01 첫 행 (다음 출고 지시 1건)</summary>
    public sealed class TransferOutItem
    {
        public bool Found;
        public string Locno = "";    // LOC_LOCNO
        public string Ptno = "";     // LOC_PTNO
        public string Lep = "";      // LOC_LEP
        public string AvlQty = "";   // LOC_AVLQT
        public string OutQty = "";   // LOC_OUTQT  출고대기
        public string CtlQty = "";   // LOC_CTLQT  (저장 파라미터로 그대로 돌려줌)
        public string DefQty = "";   // LOC_DEFQT  (저장 파라미터로 그대로 돌려줌)
    }

    /// <summary>
    /// [430] 실시간창고이전(출고) 서버 호출. 원본 : /ui/ws/plus/PL430_W01.xml (메뉴 1D03 / P143)
    ///
    ///   수불등급 : common:CODESEARCH MP/03 (AA 고순환 / AB 고+중 / BB 중순환 / CC 저순환 / CD 저+무 / DD 무이동) - HAR 실측
    ///   지시조회 : fn_Search -> plus:PL430_W01_S01 -> ds_PL430_01 (첫 행만 사용)
    ///              ZONE = 시작 LOC 앞 3자리, AISLE = 뒤 2자리, SEL_GRADE = 수불등급 코드
    ///              다음 건이면 NEXT_PTNO=Y + LEP + LOCNO + PTNO 추가
    ///   저장     : fn_Save -> plus:PL430_W01_I01 + plus:PL430_W01_U01 (TYPE=N, 파라미터만)
    ///
    /// S01 / 저장 모두 실측 전문이 없다 (HAR 에는 화면 진입만 있음). 컬럼명은 원본 JS 가 읽는 이름 그대로.
    /// </summary>
    public static class TransferOutService
    {
        public const string Pgm = "PL430_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ------------------------------------------------------------------
        // 수불등급 (MP/03)
        // ------------------------------------------------------------------
        public static ArrayList GetGrades()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("common:CODESEARCH");
            r.AddParam("CODNM_TYPE", "");
            r.AddParam("CODE_SQL", "AND ((CDM_LRG_GRP = 'MP' AND CDM_MID_GRP = '03' AND CDM_AGTCD_M IN ('COMM')))");
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "gfn_SearchComCode", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Code") return;
                    CodeItem c = new CodeItem();
                    c.Code = row["CODE"];
                    c.Name = row["NM"];
                    if (c.Code.Length > 0) list.Add(c);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // 지시 조회. next == true 면 (afterLoc, afterPtno) 다음 건
        // ------------------------------------------------------------------
        public static TransferOutItem Search(string whscd, string startLoc, string grade,
                                             bool next, string lep, string afterLoc, string afterPtno)
        {
            string loc5 = Loc.Key(startLoc);   // 원본은 5자리(ZONE 3 + AISLE 2)만 허용

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL430_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("ZONE", loc5.Length >= 3 ? loc5.Substring(0, 3) : loc5);
            r.AddParam("AISLE", loc5.Length >= 2 ? loc5.Substring(loc5.Length - 2) : loc5);
            r.AddParam("SEL_GRADE", grade);

            if (next)
            {
                r.AddParam("NEXT_PTNO", "Y");
                r.AddParam("LEP", lep);
                r.AddParam("LOCNO", Loc.Key(afterLoc));
                r.AddParam("PTNO", PartNo.Key(afterPtno));
            }
            AuthService.AddSessionCommon(r);

            TransferOutItem it = new TransferOutItem();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_PL430_01" || it.Found) return;
                    it.Found = true;
                    it.Locno = row["LOC_LOCNO"];
                    it.Ptno = row["LOC_PTNO"];
                    it.Lep = row["LOC_LEP"];
                    it.AvlQty = row["LOC_AVLQT"];
                    it.OutQty = row["LOC_OUTQT"];
                    it.CtlQty = row["LOC_CTLQT"];
                    it.DefQty = row["LOC_DEFQT"];
                });

            Check(res);
            return it;
        }

        // ------------------------------------------------------------------
        // 저장 : 원본 fn_Save. qty 는 화면의 대상수량, 나머지 LOC_* 는 조회값 그대로.
        // ------------------------------------------------------------------
        public static void Save(string whscd, TransferOutItem it, string qty)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL430_W01_I01");   // tit_AddSingleActionInfo = TYPE N
            r.AddSearch("plus:PL430_W01_U01");

            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            r.AddParam("LEP", it.Lep);
            r.AddParam("WHSCD", whscd);
            r.AddParam("LOCNO", Loc.Key(it.Locno));
            r.AddParam("QTY", qty);
            r.AddParam("LOC_AVLQT", it.AvlQty);
            r.AddParam("LOC_CTLQT", it.CtlQty);
            r.AddParam("LOC_DEFQT", it.DefQty);
            r.AddParam("LOC_OUTQT", it.OutQty);
            r.AddParam("USRID", UsrCol("USR_USRID"));
            AuthService.AddSessionCommon(r);

            string[] fail = new string[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Save", r.Build(),
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
