using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>PL440_W01_S02 : ds_List02 1행 (이관 출고할 부품의 LOC 재고)</summary>
    public sealed class MigrateOutItem
    {
        public string Lep = "";      // LOC_LEP
        public string Ptno = "";
        public string Locno = "";    // LOC_LOCNO
        public string AvlQty = "";   // LOC_AVLQT
        public string SalQty = "";   // SALQT     출고대기
        public string AvPrc = "";    // INV_AVPRC 평균단가 (저장 파라미터)
        public string VLocno = "";   // VLOCNO    (저장 파라미터)

        public override string ToString()
        {
            return Lep + "  " + PartNo.Display(Ptno) + "  " + Loc.Display(Locno) + "  재고 " + AvlQty;
        }
    }

    /// <summary>
    /// [440] 실시간창고이관(출고) 서버 호출. 원본 : /ui/ws/plus/PL440_W01.xml (메뉴 1D06 / P180)
    ///
    ///   LOC 확인   : fn_SearchLoc     -> plus:PL440_W01_S01 -> ds_List01 (LOC_YN = N 이면 MP592)
    ///   부품 조회  : fn_Search        -> plus:PL440_W01_S02 -> ds_List02 (계열별 여러 행)
    ///   0건일 때   : fn_Out_Search    -> plus:PL440_W01_S04 -> ds_List04 (이미 이관 출고된 건수 확인)
    ///   증표 채번  : fn_SearchInvoice -> plus:PL440_W01_S03 -> ds_Output02 / VCHNO
    ///   저장       : fn_ConfirmQt     -> plus:PL440_W01_I01 + I02 + U01 (TYPE=N, 파라미터만)
    ///
    /// 원본 fn_Out_Search 는 tit_ClearActionInfo 없이 S04 를 덧붙여 S02 까지 다시 실행한다. 여기서는 S04 만 보낸다.
    /// 모든 호출이 실측 없음 (HAR 에는 화면 진입만).
    /// </summary>
    public static class MigrateOutService
    {
        public const string Pgm = "PL440_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        /// <summary>LOC 확인. 반환 : null = 없는 LOC, "N" = 사용 불가(LOC_YN=N), 그 외 = 정상</summary>
        public static string CheckLoc(string whscd, string locno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL440_W01_S01");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("LOCNO", Loc.Key(locno));
            AuthService.AddSessionCommon(r);

            string[] yn = new string[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLoc", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List01" || yn[0] != null) return;
                    yn[0] = row["LOC_YN"];
                });

            Check(res);
            return yn[0];
        }

        public static ArrayList Search(string whscd, string ptno)
        {
            string key = PartNo.Key(ptno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL440_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("WHSCD", whscd);
            r.AddParam("PTNO", key);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List02") return;
                    MigrateOutItem it = new MigrateOutItem();
                    it.Lep = row["LOC_LEP"];
                    it.Ptno = key;
                    it.Locno = row["LOC_LOCNO"];
                    it.AvlQty = row["LOC_AVLQT"];
                    it.SalQty = row["SALQT"];
                    it.AvPrc = row["INV_AVPRC"];
                    it.VLocno = row["VLOCNO"];
                    list.Add(it);
                });

            Check(res);
            return list;
        }

        /// <summary>S02 가 0건일 때 : 이미 이관 출고된(입고 대기) 건수</summary>
        public static int CountPendingOut(string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL440_W01_S04");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);

            int[] n = new int[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Out_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_List04") n[0]++;
                });

            Check(res);
            return n[0];
        }

        /// <summary>증표 채번(S03) 후 저장(I01 + I02 + U01). 채번 실패면 예외.</summary>
        public static void Save(string whscd, MigrateOutItem it, string qty)
        {
            string agt = UsrCol("USR_AGTCD");

            // 1) 증표번호 (원본 fn_SearchInvoice)
            TitRequest q = new TitRequest();
            q.AddSearch("plus:PL440_W01_S03");
            q.AddParam("IVT_AGTCD", agt);
            AuthService.AddSessionCommon(q);

            string[] vch = new string[1];
            TitResult qr = HaimsHttp.PostStream(Pgm, "fn_SearchInvoice", q.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_Output02" && vch[0] == null) vch[0] = row["VCHNO"];
                });
            Check(qr);
            if (vch[0] == null || vch[0].Length == 0) throw new HaimsException("증표번호를 받지 못했습니다.");

            // 2) 저장 (원본 fn_ConfirmQt)
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL440_W01_I01");
            r.AddSearch("plus:PL440_W01_I02");
            r.AddSearch("plus:PL440_W01_U01");
            r.AddParam("AGTCD", agt);
            r.AddParam("VCHNO", vch[0]);
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            r.AddParam("LEP", it.Lep);
            r.AddParam("QTY", qty);
            r.AddParam("WHSCD", whscd);
            r.AddParam("INV_AVPRC", it.AvPrc);
            r.AddParam("USRID", UsrCol("USR_USRID"));
            r.AddParam("LOCNO", Loc.Key(it.Locno));
            r.AddParam("VLOCNO", it.VLocno);
            AuthService.AddSessionCommon(r);

            string[] fail = new string[1];
            TitResult res = HaimsHttp.PostStream(Pgm, "fn_ConfirmQt", r.Build(),
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

    /// <summary>PL441_W01_S01 : ds_List02 1행 (이관 출고된 입고 대상)</summary>
    public sealed class MigrateInItem
    {
        public string Lep = "";       // IVT_LEP
        public string Ptno = "";
        public string Grade = "";     // UWMG_GRADE
        public string PartName = "";  // INV_PTNM_A
        public string FromWh = "";    // IVT_FM_IVTID  출고 창고
        public string FromLoc = "";   // IVT_FM_LOC
        public string Qty = "";       // IVT_TRF_QT    이관 수량
        public string Vchno = "";     // IVT_VCHNO
        public string ToLoc = "";     // TO_LOCNO      (입고 창고에 등록된 LOC)

        public override string ToString()
        {
            return Lep + "  " + PartNo.Display(Ptno) + "  " + FromWh + " " + Loc.Display(FromLoc) + "  " + Qty;
        }
    }

    /// <summary>PL441_W01_S02 결과</summary>
    public sealed class MigrateInLocCheck
    {
        public string Locno = "";       // LOC_LOCNO   이 창고에 이미 등록된 LOC (없으면 빈 값)
        public string WhscdCheck = "";  // WHSCD_CHECK N 이면 MP589
        public string LocnoCheck = "";  // LOCNO_CHECK (원본은 읽기만 하고 쓰지 않음)
    }

    /// <summary>
    /// [441] 실시간창고이관(입고) 서버 호출. 원본 : /ui/ws/plus/PL441_W01.xml (메뉴 1D07 / P181)
    ///
    ///   조회     : fn_SearchFromPTNO -> plus:PL441_W01_S01 -> ds_List02 (TO_WHSCD = 입고 창고)
    ///   LOC 확인 : fn_SearchLoc      -> plus:PL441_W01_S02 -> ds_List01
    ///   LOC 등록 : fn_InsertLoc      -> plus:PL441_W01_I01 (입고 창고에 LOC 가 없을 때)
    ///   저장     : fn_SaveLoc        -> plus:PL441_W01_U01 ~ U04 (TYPE=N, 파라미터만)
    ///
    /// 모든 호출이 실측 없음.
    /// </summary>
    public static class MigrateInService
    {
        public const string Pgm = "PL441_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        private static void CheckNone(TitResult res, string[] fail)
        {
            Check(res);
            if (fail[0] != null)
                throw new HaimsException(fail[0].Length > 0 ? fail[0] : "저장중 에러가 발생하였습니다.");
        }

        public static ArrayList Search(string toWh, string ptno)
        {
            string key = PartNo.Key(ptno);

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL441_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("PTNO", key);
            r.AddParam("TO_WHSCD", toWh);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchFromPTNO", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List02") return;
                    MigrateInItem it = new MigrateInItem();
                    it.Lep = row["IVT_LEP"];
                    it.Ptno = key;
                    it.Grade = row["UWMG_GRADE"];
                    it.PartName = row["INV_PTNM_A"];
                    it.FromWh = row["IVT_FM_IVTID"];
                    it.FromLoc = row["IVT_FM_LOC"];
                    it.Qty = row["IVT_TRF_QT"];
                    it.Vchno = row["IVT_VCHNO"];
                    it.ToLoc = row["TO_LOCNO"];
                    list.Add(it);
                });

            Check(res);
            return list;
        }

        public static MigrateInLocCheck CheckLoc(string toWh, MigrateInItem it)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL441_W01_S02");
            r.AddParam("IVT_FM_IVTID", it.FromWh);
            r.AddParam("WHSCD", toWh);
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("IVT_LEP", it.Lep);
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            AuthService.AddSessionCommon(r);

            MigrateInLocCheck c = new MigrateInLocCheck();
            bool[] got = new bool[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SearchLoc", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_List01" || got[0]) return;
                    got[0] = true;
                    c.Locno = row["LOC_LOCNO"];
                    c.WhscdCheck = row["WHSCD_CHECK"];
                    c.LocnoCheck = row["LOCNO_CHECK"];
                });

            Check(res);
            return c;
        }

        /// <summary>입고 창고에 LOC 등록 (원본 fn_InsertLoc)</summary>
        public static void InsertLoc(string toWh, MigrateInItem it, string toLoc)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL441_W01_I01");
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("WHSCD", toWh);
            r.AddParam("LEP", it.Lep);
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            r.AddParam("TO_LOCNO", Loc.Key(toLoc));
            AuthService.AddSessionCommon(r);

            string[] fail = new string[1];
            TitResult res = HaimsHttp.PostStream(Pgm, "fn_InsertLoc", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "_none" || fail[0] != null) return;
                    if (row.Has("MSG_CODE") && row["MSG_CODE"] != "S") fail[0] = row["MSG"];
                });
            CheckNone(res, fail);
        }

        /// <summary>입고 저장 (원본 fn_SaveLoc)</summary>
        public static void Save(string toWh, MigrateInItem it, string toLoc, string qty)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL441_W01_U01");
            r.AddSearch("plus:PL441_W01_U02");
            r.AddSearch("plus:PL441_W01_U03");
            r.AddSearch("plus:PL441_W01_U04");
            r.AddParam("WHSCD", toWh);
            r.AddParam("LOCNO", Loc.Key(toLoc));
            r.AddParam("USRID", UsrCol("USR_USRID"));
            r.AddParam("AGTCD", UsrCol("USR_AGTCD"));
            r.AddParam("IVT_VCHNO", it.Vchno);
            r.AddParam("IVT_TRF_QT", qty);
            r.AddParam("IVT_FM_IVTID", it.FromWh);
            r.AddParam("IVT_LEP", it.Lep);
            r.AddParam("PTNO", PartNo.Key(it.Ptno));
            AuthService.AddSessionCommon(r);

            string[] fail = new string[1];
            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SaveLoc", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "_none" || fail[0] != null) return;
                    if (row.Has("MSG_CODE") && row["MSG_CODE"] != "S") fail[0] = row["MSG"];
                });
            CheckNone(res, fail);
        }
    }
}
