using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL420_W01_S01 : ds_List 1행</summary>
    public sealed class LocSortRow
    {
        public string Lep = "";       // LOC_LEP
        public string Ptno = "";      // LOC_PTNO
        public string Whscd = "";     // LOC_WHSCD
        public string AvlQty = "";    // LOC_AVLQT
        public string Locno = "";     // LOC_LOCNO
        public string Grade = "";     // UWMG_GRADE
        public string PartName = "";  // INV_PTNM_A

        /// <summary>계열 선택 팝업에 보이는 문구</summary>
        public override string ToString()
        {
            return Lep + "  " + PartNo.Display(Ptno) + "  " + Whscd + "  " + Loc.Display(Locno) + "  재고 " + AvlQty;
        }
    }

    /// <summary>
    /// [420] 부품LOC정렬 서버 호출. 원본 : /ui/ws/plus/PL420_W01.xml (메뉴 1D02 / P142)
    ///
    ///   조회 : fn_Search -> plus:PL420_W01_S01 -> ds_List (HAR 2026-10-05 10:44 실측, 계열별로 여러 행)
    ///
    /// 이 화면은 저장이 없다. 입고장에서 저장 처리한 부품을 목록에 모아 LOC 순으로 정렬하고,
    /// 셀에 넣을 때 부번 -> LOC 를 스캔해 목록에서 지워 가는 작업용 체크리스트다.
    /// </summary>
    public static class LocSortService
    {
        public const string Pgm = "PL420_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        public static ArrayList Search(string whscd, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL420_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List") return;
                    LocSortRow s = new LocSortRow();
                    s.Lep = row["LOC_LEP"];
                    s.Ptno = row["LOC_PTNO"];
                    s.Whscd = row["LOC_WHSCD"];
                    s.AvlQty = row["LOC_AVLQT"];
                    s.Locno = row["LOC_LOCNO"];
                    s.Grade = row["UWMG_GRADE"];
                    s.PartName = row["INV_PTNM_A"];
                    if (s.Ptno.Length > 0) list.Add(s);
                });

            if (res.IsError) throw new HaimsException(res.ErrorMsg);
            return list;
        }
    }
}
