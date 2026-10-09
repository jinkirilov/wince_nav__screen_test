using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL410_W01_S02 : ds_PartForLoc 1건</summary>
    public sealed class LocRegInfo
    {
        public bool Found;
        public string DoFlag = "";    // DO_FLAG      I = 신규(I01) / U = 변경(U01)
        public string Lep = "";       // LOC_LEP
        public string Locno = "";     // LOC_LOCNO    현재 LOC (계열 Lep)
        public string AvlQty = "";    // LOC_AVLQT
        public string Locno2 = "";    // LOC_LOCNO2   반대 계열(H<->K) LOC
        public string AvlQty2 = "";   // LOC_AVLQT2
        public string Grade = "";     // UWMG_GRADE   수불등급
        public string PartName = "";  // INV_PTNM_A
        public string InQty = "";     // WSF_SNDDT_X  입고대기(D/I)
        public string OutQty = "";    // INV_OUTPD_QT 출고대기(D/O)
    }

    /// <summary>
    /// [410] LOC등록 서버 호출. 원본 : /ui/ws/plus/PL410_W01.xml (메뉴 1D01 / P141)
    ///
    ///   조회 : fn_Search -> plus:PL410_W01_S02 -> ds_PartForLoc (HAR 2026-10-05 10:44 실측)
    ///   저장 : fn_Save   -> DO_FLAG 가 I 면 plus:PL410_W01_I01, U 면 plus:PL410_W01_U01
    ///                       그 다음 항상 plus:PL410_W01_I02. 둘 다 TYPE=N, 파라미터만 (저장 실측 없음)
    ///
    /// 원본에 있지만 쓰지 않는 것 : fn_SearchLep(PL410_W01_S05, 호출부 주석 처리),
    ///                             fn_SaveInv(PL100_W01_I07, 호출하는 곳 없음)
    /// </summary>
    public static class LocRegisterService
    {
        public const string Pgm = "PL410_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        public static LocRegInfo Search(string whscd, string lep, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL410_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("LEP", lep);
            AuthService.AddSessionCommon(r);

            LocRegInfo info = new LocRegInfo();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_PartForLoc" || info.Found) return;
                    info.Found = true;
                    info.DoFlag = row["DO_FLAG"];
                    info.Lep = row["LOC_LEP"];
                    info.Locno = row["LOC_LOCNO"];
                    info.AvlQty = row["LOC_AVLQT"];
                    info.Locno2 = row["LOC_LOCNO2"];
                    info.AvlQty2 = row["LOC_AVLQT2"];
                    info.Grade = row["UWMG_GRADE"];
                    info.PartName = row["INV_PTNM_A"];
                    info.InQty = row["WSF_SNDDT_X"];
                    info.OutQty = row["INV_OUTPD_QT"];
                });

            Check(res);
            return info;
        }

        /// <summary>원본 fn_Save. fromLoc 은 현재 LOC (없으면 빈 값), qty 는 가용재고.</summary>
        public static void Save(string doFlag, string whscd, string lep, string ptno,
                                string toLoc, string fromLoc, string qty)
        {
            string sql;
            if (doFlag == "I") sql = "plus:PL410_W01_I01";
            else if (doFlag == "U") sql = "plus:PL410_W01_U01";
            else throw new HaimsException("등록할 수 없는 부품입니다. (DO_FLAG=" + doFlag + ")");

            TitRequest r = new TitRequest();
            r.AddSearch(sql);                     // tit_AddSingleActionInfo = TYPE N
            r.AddSearch("plus:PL410_W01_I02");

            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("TO_LOCNO", Loc.Key(toLoc));
            r.AddParam("USRID", UsrCol("USR_USRID"));
            r.AddParam("FROM_LOCNO", Loc.Key(fromLoc));
            r.AddParam("QTY", qty);
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
