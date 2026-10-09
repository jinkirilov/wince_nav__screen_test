using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>[322] 통제등록 저장 입력값</summary>
    public sealed class OsdInput
    {
        public string Lep = "";
        public string Ptno = "";     // 표기/원본 아무거나 (내부에서 Key 로 변환)
        public string Whscd = "";    // CIH_WHSFL / WHSCD
        public string Locno = "";    // 표기/원본 아무거나 (내부에서 Key 로 변환)
        public string ObjQty = "";   // CIH_OBJ_QT  LOC수량
        public string OsdQty = "";   // CIH_OSD_QT  OS/D처리수량
        public string DoQty = "";    // CIH_DO_QT   수정후수량
        public string ResCd = "";    // CIH_RESCD   사유 (MP/41)
    }

    /// <summary>
    /// [322] 통제등록(OS&amp;D) 서버 호출. 원본 : /ui/ws/plus/PL212_W01.xml (메뉴 P138)
    ///
    ///   사유 콤보 : gfn_SearchComCode -> common:CODESEARCH (MP/41) -> ds_Code
    ///   저장      : fn_SaveDEFQT -> plus:PL211_W01_I01 -> U01 -> U02
    ///
    /// 저장은 tit_AddSingleActionInfo 3건이고 입력 데이터셋 없이 파라미터만 보낸다.
    /// ds_cmd 레코드는 조회와 같은 TYPE=N / EXEC_TYPE=B 라 AddSearch 를 그대로 쓴다.
    /// 응답은 SQL 마다 _none 데이터셋(MSG_CODE)이 붙고 S 면 성공.
    /// (HAR 2026-10-05 실측 : 저장 후 가용 3->2, INV_DEFQT 0->1, LOC 2->1)
    /// </summary>
    public static class OsdService
    {
        public const string Pgm = "PL212_W01.xml";

        private const string ReasonSql =
            "AND ((CDM_LRG_GRP = 'MP' AND CDM_MID_GRP = '41' AND CDM_AGTCD_M IN ('COMM')))";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ------------------------------------------------------------------
        // 사유 콤보 : MP/41 (01 수량부족 ~ 09 개선전자재)
        // ------------------------------------------------------------------
        public static ArrayList GetReasons()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("common:CODESEARCH");
            r.AddParam("CODNM_TYPE", "");
            r.AddParam("CODE_SQL", ReasonSql);
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
        // 저장 : fn_SaveDEFQT
        // ------------------------------------------------------------------
        public static void Save(OsdInput i)
        {
            if (i == null) throw new ArgumentNullException("i");

            string usr = UsrCol("USR_USRID");
            string agt = UsrCol("USR_AGTCD");
            string ptno = PartNo.Key(i.Ptno);
            string loc = Loc.Key(i.Locno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL211_W01_I01");
            r.AddSearch("plus:PL211_W01_U01");
            r.AddSearch("plus:PL211_W01_U02");

            r.AddParam("CIH_AGTCD", agt);
            r.AddParam("CIH_LEP", i.Lep);
            r.AddParam("CIH_PTNO", ptno);
            r.AddParam("CIH_WHSFL", i.Whscd);
            r.AddParam("CIH_LOCNO", loc);
            r.AddParam("CIH_OBJ_QT", i.ObjQty);
            r.AddParam("CIH_OSD_QT", i.OsdQty);
            r.AddParam("CIH_DO_QT", i.DoQty);
            r.AddParam("CIH_GUBUN", "S");
            r.AddParam("CIH_STAT", "C");
            r.AddParam("CIH_REFNO", "Inventory");
            r.AddParam("CIH_DT", DateTime.Now.ToString("yyyyMMdd"));
            r.AddParam("CIH_RESCD", i.ResCd);
            r.AddParam("CIH_INS_ID", usr);
            r.AddParam("CIH_UPD_ID", usr);
            r.AddParam("CIH_ACT_QT", "0");
            r.AddParam("CIH_REFNO_3", "");
            r.AddParam("CIH_REMARKS", "");

            r.AddParam("USRID", usr);
            r.AddParam("AGTCD", agt);
            r.AddParam("LEP", i.Lep);
            r.AddParam("PTNO", ptno);
            r.AddParam("WHSCD", i.Whscd);
            AuthService.AddSessionCommon(r);

            string[] fail = new string[1];   // null = 성공

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SaveDEFQT", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "_none" || fail[0] != null) return;
                    if (row["MSG_CODE"] != "S") fail[0] = row["MSG"];
                });

            Check(res);

            if (fail[0] != null)
                throw new HaimsException(fail[0].Length > 0 ? fail[0] : "저장중 에러가 발생하였습니다.");
        }
    }
}
