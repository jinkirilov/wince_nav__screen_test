using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL324_W01_S02 : ds_List 1행</summary>
    public sealed class PartInfoRow
    {
        public string Ptno = "";      // LOC_PTNO
        public string Locno = "";     // LOC_LOCNO
        public string AvlQty = "";    // LOC_AVLQT
        public string Price = "";     // INV_AVPRC
        public string PartName = "";  // INV_PTNM_A
        public string Grade = "";     // UWMG_GRADE
        public string VhcKind = "";   // UDPM_VHC_KND
        public string InvQty = "";    // INVQTY
        public string Lep = "";       // LOC_LEP
        public string Class_ = "";    // UDPM_CLASS (수불코드)
    }

    /// <summary>코드 콤보 1건</summary>
    public sealed class CodeItem
    {
        public string Code = "";
        public string Name = "";
        public override string ToString() { return (Name.Length > 0) ? Name : Code; }
    }

    /// <summary>
    /// [324] 부품정보조회 서버 호출. 원본 : /ui/ws/plus/PL324_W01.xml
    ///
    /// 페이징은 커서 방식이다. 화면에 보이는 첫 행/마지막 행의 부품번호를 기준으로
    /// COND(비교연산자) + NEXT(N/B) 를 바꿔 다시 조회한다.
    /// </summary>
    public static class PartInfoService
    {
        public const string Pgm = "PL324_W01.xml";

        private static string Agt
        {
            get { UserInfo u = Session.User; return u == null ? "" : u["USR_AGTCD"]; }
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
        // 계열 콤보 : plus.js gfn_SearchLEP_Plus / common:HS00_W01_S05 -> ds_Lep / INV_LEP
        // ------------------------------------------------------------------
        public static ArrayList GetLeps()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("common:HS00_W01_S05");
            r.AddParam("AGTCD", Agt);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "gfn_SearchLEP_Plus", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Lep") return;
                    string v = row["INV_LEP"];
                    if (v.Length > 0) list.Add(v);
                });

            Check(res);
            return list;
        }

        // ------------------------------------------------------------------
        // 창고 콤보 : plus.js gfn_SearchWHS_Plus / plus:PL000_W01_S01 -> ds_Output / LOC_WHSCD
        // ------------------------------------------------------------------
        public static ArrayList GetWarehouses()
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL000_W01_S01");
            r.AddParam("USR_USRID", UsrCol("USR_USRID"));
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
        // 차종 콤보 : fn_SearchComCode / common:CODESEARCH (HAIMS_COMM_ACTION)
        //   CDM_LRG_GRP='UA', CDM_MID_GRP='04', CDM_AGTCD_M in ('COMM', 대리점)
        // ------------------------------------------------------------------
        public static ArrayList GetCarCodes()
        {
            string sql = "AND ((CDM_LRG_GRP = 'UA' AND CDM_MID_GRP = '04'"
                       + " AND CDM_AGTCD_M IN ('COMM', '" + Agt + "')))";

            TitRequest r = new TitRequest();
            r.AddSearch("common:CODESEARCH");
            r.AddParam("CODNM_TYPE", "");
            r.AddParam("CODE_SQL", sql);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchComCode", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Code") 
                        return;
                    CodeItem c = new CodeItem();
                    c.Code = row["CODE"];
                    c.Name = row["NM"];
                    if (c.Code.Length > 0) list.Add(c);
                });

            Check(res);
            return list;
        }

        /// <summary>조회 방향</summary>
        public enum Dir { First, Next, Prev }

        // ------------------------------------------------------------------
        // 본조회 : fn_SearchPTNO / fn_SearchCarPTNO — 둘 다 plus:PL324_W01_S02
        //   vhcKnd 가 비어 있으면 차종 조건을 붙이지 않는다(fn_SearchPTNO 와 동일).
        // ------------------------------------------------------------------
        public static ArrayList Search(string lep, string ptno, string whscd,
                                       string vhcKnd, Dir dir)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL324_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", Agt);
            r.AddParam("WHSCD", whscd);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));

            // COND 는 SQL 비교연산자 문자열이다. 원본 JS 는 &gt; 로 이스케이프해 보내지만
            // TitRequest.Esc 가 같은 일을 하므로 여기서는 원문자를 넣는다.
            switch (dir)
            {
                case Dir.Next: r.AddParam("COND", ">"); r.AddParam("NEXT", "N"); break;
                case Dir.Prev: r.AddParam("COND", "<"); r.AddParam("NEXT", "B"); break;
                default: r.AddParam("COND", ">="); r.AddParam("NEXT", "N"); break;
            }

            string fn = "fn_SearchPTNO";
            if (vhcKnd != null && vhcKnd.Length > 0)
            {
                r.AddParam("VHC_KND", vhcKnd);
                fn = "fn_SearchCarPTNO";
            }

            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, fn, r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List") return;
                    PartInfoRow p = new PartInfoRow();
                    p.Ptno = row["LOC_PTNO"];
                    p.Locno = row["LOC_LOCNO"];
                    p.AvlQty = row["LOC_AVLQT"];
                    p.Price = row["INV_AVPRC"];
                    p.PartName = row["INV_PTNM_A"];
                    p.Grade = row["UWMG_GRADE"];
                    p.VhcKind = row["UDPM_VHC_KND"];
                    p.InvQty = row["INVQTY"];
                    p.Lep = row["LOC_LEP"];
                    p.Class_ = row["UDPM_CLASS"];
                    list.Add(p);
                });

            Check(res);
            return list;
        }
    }
}
