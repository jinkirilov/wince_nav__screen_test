using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL431_W01_S01 : ds_Output01 1행 (구창고에서 출고된 이전 대상)</summary>
    public sealed class TransferInItem
    {
        public string Lep = "";       // LOH_LEP
        public string Ptno = "";      // LOH_PTNO
        public string Grade = "";     // UWMG_GRADE
        public string PartName = "";  // INV_PTNM_A
        public string WhsFr = "";     // LOH_WHSFR  구창고
        public string LocFr = "";     // LOH_LOCFR  구창고 LOC
        public string Qty = "";       // LOH_QTY    대상수량
        public string LocTo = "";     // LOH_LOCTO  (지정돼 있으면) 신창고 LOC

        /// <summary>계열 선택 팝업 문구</summary>
        public override string ToString()
        {
            return Lep + "  " + PartNo.Display(Ptno) + "  " + WhsFr + " " + Loc.Display(LocFr) + "  " + Qty;
        }
    }

    /// <summary>
    /// [431] 실시간창고이전(입고) 서버 호출. 원본 : /ui/ws/plus/PL431_W01.xml (메뉴 1D04 / P144)
    ///
    ///   조회 : fn_SearchFromPTNO -> plus:PL431_W01_S01 -> ds_Output01 (계열별 여러 행 가능)
    ///   저장 : fn_SaveLoc        -> plus:PL431_W01_U01 + plus:PL431_W01_U02
    ///          원본은 tit_AddSearchActionInfo 로 쌓는다(TYPE=N). 파라미터는 LOCNO(신 LOC) / AGTCD / PTNO / LEP /
    ///          WHSCD(= LOH_WHSFR, 구창고 코드 그대로) / USRID. 수량은 보내지 않는다(이전분 전량).
    ///
    /// 조회/저장 모두 실측 전문이 없다. 컬럼명은 원본 JS 가 읽는 이름 그대로.
    /// </summary>
    public static class TransferInService
    {
        public const string Pgm = "PL431_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        public static ArrayList Search(string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL431_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchFromPTNO", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output01") return;
                    TransferInItem it = new TransferInItem();
                    it.Lep = row["LOH_LEP"];
                    it.Ptno = row["LOH_PTNO"];
                    it.Grade = row["UWMG_GRADE"];
                    it.PartName = row["INV_PTNM_A"];
                    it.WhsFr = row["LOH_WHSFR"];
                    it.LocFr = row["LOH_LOCFR"];
                    it.Qty = row["LOH_QTY"];
                    it.LocTo = row["LOH_LOCTO"];
                    if (it.Ptno.Length > 0) list.Add(it);
                });

            Check(res);
            return list;
        }

        public static void Save(TransferInItem it, string toLoc)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL431_W01_U01");
            r.AddSearch("plus:PL431_W01_U02");
            r.AddParam("LOCNO", Loc.Key(toLoc));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            r.AddParam("LEP", it.Lep);
            r.AddParam("WHSCD", it.WhsFr);
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
