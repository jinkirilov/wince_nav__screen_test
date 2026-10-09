using System;
using System.Collections;
using System.Text;

namespace HaimsPda.Net
{
    /// <summary>hidGrd1 1건 — 조회된 부품 정보. InsertList 가 이걸 그대로 grd 로 옮긴다.</summary>
    public sealed class DirectPartInfo
    {
        public string Lep = "";
        public string PtNo = "";
        public string CarCd = "";
        public string WhsFl = "";
        public string LocNo = "";
        public string SalQt = "";
        public string SalPrc = "";
        public string SalAmt = "";
        public string AvlQt = "";
        public string AftPtNo = "";
        public string SuCd = "";
        public string InvMerge = "";
        public string AdjtYn = "";
        public string AdjtCalQty = "";
        public string AdjtProcQty = "";
        public string PtNm = "";
        public string StockDesc = "";
    }

    /// <summary>hidGrd2 1건 — 업체 여신/잔액 정보. 저장 헤더에 그대로 실린다.</summary>
    public sealed class VapInfo
    {
        public string SalAmt = "";
        public string ColAmt = "";
        public string DcAmt = "";
        public string CountAmt = "";
        public string BalAmt = "";
        public string LstBalAmt = "";
        public string LimitAmt = "";
        public string TtlAmt = "";
    }

    /// <summary>
    /// grd 한 줄. 저장 전문의 xmlDetail 은 이 컬럼 순서 그대로 나가야 한다
    /// (웹 grd.getRangeXML 이 그리드 컬럼 정의 순서로 직렬화하기 때문).
    /// </summary>
    public sealed class DirectInboundRow
    {
        public static readonly string[] Columns = new string[] {
            "RNUM", "LEP", "TRS_PTNO", "TRS_CARCD", "TRS_WHSFL", "LOC_LOCNO",
            "TRS_SALQT", "TRS_SAL_PRC", "SAL_AMT", "INV_AVLQT",
            "UDPM_AFT_PTNO", "UDPM_SU_CD", "UDPM_INV_MERGE",
            "TRS_ADJT_YN", "TRS_ADJT_CAL_QTY", "TRS_ADJT_PROC_QTY",
            "HPM_PTNM_A", "STOCK_DESC", "TRS_RETID", "PTNO_HIDDEN", "HIDDEN_LEP"
        };

        private readonly Hashtable _v = new Hashtable();

        public string this[string col]
        {
            get { object o = _v[col]; return (o == null) ? "" : (string)o; }
            set { _v[col] = (value == null) ? "" : value; }
        }

        /// <summary>InsertList 와 같은 매핑.</summary>
        public static DirectInboundRow From(DirectPartInfo p, string whsCd,
                                          string qty, string price, bool adjust)
        {
            DirectInboundRow r = new DirectInboundRow();
            r["LEP"] = p.Lep;
            r["TRS_PTNO"] = p.PtNo;
            r["TRS_CARCD"] = p.CarCd;
            r["TRS_WHSFL"] = whsCd;                 // 화면 창고 콤보값을 쓴다
            r["LOC_LOCNO"] = p.LocNo;
            r["TRS_SALQT"] = qty;
            r["TRS_SAL_PRC"] = price;
            r["SAL_AMT"] = Mul(price, qty);
            r["INV_AVLQT"] = p.AvlQt;
            r["UDPM_AFT_PTNO"] = p.AftPtNo;
            r["UDPM_SU_CD"] = p.SuCd;
            r["UDPM_INV_MERGE"] = p.InvMerge;
            r["TRS_ADJT_YN"] = adjust ? "Y" : "N";
            r["TRS_ADJT_CAL_QTY"] = p.AdjtCalQty;
            r["TRS_ADJT_PROC_QTY"] = p.AdjtProcQty;
            r["HPM_PTNM_A"] = p.PtNm;
            r["STOCK_DESC"] = p.StockDesc;
            r["TRS_RETID"] = "N";
            r["PTNO_HIDDEN"] = p.PtNo;
            r["HIDDEN_LEP"] = p.Lep;
            return r;
        }

        private static string Mul(string a, string b)
        {
            try { return (ParseInt(a) * ParseInt(b)).ToString(); }
            catch { return "0"; }
        }

        internal static int ParseInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try { return int.Parse(s); }
            catch { return 0; }
        }
    }

    /// <summary>저장 결과 전표번호</summary>
    public sealed class DirectSaveResult
    {
        public string VchYm = "";
        public string VchSeq = "";
        public bool HasVoucher { get { return VchYm.Length > 0 && VchSeq.Length > 0; } }
    }

    /// <summary>
    /// [142] 직입고저장 (원본 PL142_W01.xml).
    ///
    /// 이 화면이 프로젝트에서 처음으로 저장(커밋)을 하는 화면이다.
    /// 저장은 조회와 전문 구조가 다르다 —
    ///   ds_cmd 의 TYPE 이 "M"(tit_AddMultiActionInfo) 이고,
    ///   프로시저 입력을 ds_PL142_Input 이라는 별도 데이터셋으로 싣는다.
    ///   그 안의 xmlHeader / xmlDetail 은 XML 문자열을 통째로 값에 넣은 것이다.
    /// </summary>
    public static class DirectInboundService
    {
        public const string Pgm = "PL142_W01.xml";

        /// <summary>fn_SaveGbn 이 보내는 고정값</summary>
        private const string HoloNo = "MOBILEPDA_142";

        /// <summary>웹의 "일괄입고는 최대 5까지"</summary>
        public const int MaxRows = 5;

        private static string Agt
        {
            get { UserInfo u = Session.User; return u == null ? "" : u["USR_AGTCD"]; }
        }

        private static string Usr
        {
            get { UserInfo u = Session.User; return u == null ? "" : u["USR_USRID"]; }
        }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        // ------------------------------------------------------------------
        // gfn_SearchLEP_Plus : 계열 콤보
        //
        // SQL_ID 는 common:HS00_W01_S05 다(HAR 요청 원문 확인).
        // 파라미터는 AGTCD 하나, 응답은 ds_Lep / INV_LEP.
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
        // fn_VapSearch : plus:PL142_W01_S02 -> ds_OutPL142_W01_S02
        // 업체를 고르면 그 업체의 여신/잔액을 받아 둔다. 저장 헤더에 실린다.
        // ------------------------------------------------------------------
        public static VapInfo SearchVap(string vndMn, string vndSb)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL142_W01_S02");
            r.AddParam("AGTCD", Agt);
            r.AddParam("VNDMN", vndMn);
            r.AddParam("VNDSB", vndSb);
            AuthService.AddSessionCommon(r);

            VapInfo[] hit = new VapInfo[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_VapSearch", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_OutPL142_W01_S02" || hit[0] != null) return;
                    VapInfo v = new VapInfo();
                    v.SalAmt = row["VAP_SAL_AMT"];
                    v.ColAmt = row["VAP_COL_AMT"];
                    v.DcAmt = row["VAP_DCAMT"];
                    v.CountAmt = row["VAP_COUNT_AMT"];
                    v.BalAmt = row["VAP_BAL_AMT"];
                    v.LstBalAmt = row["VAP_LST_BAL_AMT"];
                    v.LimitAmt = row["VEN_LIMIT_AMT"];
                    v.TtlAmt = row["VAP_TTL_AMT"];
                    hit[0] = v;
                });

            Check(res);
            return hit[0];
        }

        // ------------------------------------------------------------------
        // fn_Search : plus:PL142_W01_S01 + plus:PL140_W01_S04
        //
        // S01 은 파라미터가 아니라 SQL 조각(테이블 함수 호출)을 통째로 넘긴다.
        // 웹이 그렇게 만들어 보내므로 형태를 그대로 재현한다.
        // ------------------------------------------------------------------
        public static DirectPartInfo Search(string vndMn, string vndSb,
                                          string lep, string ptno, string whsCd)
        {
            string key = PartNo.Key(ptno);

            StringBuilder sql = new StringBuilder(160);
            sql.Append("TABLE(KACPTFLE.FNSSLW002R('")
               .Append(Agt).Append("','")
               .Append(vndMn).Append("','")
               .Append(vndSb).Append("','")
               .Append(lep).Append("','")
               .Append(key).Append("'))A");

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL142_W01_S01");
            r.AddParam("SQL", sql.ToString());

            r.AddSearch("plus:PL140_W01_S04");
            r.AddParam("AGTCD", Agt);
            r.AddParam("WHSCD", whsCd);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", key);

            AuthService.AddSessionCommon(r);

            DirectPartInfo[] hit = new DirectPartInfo[1];
            string[] locNo = new string[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Search", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_OutPL142_W01_S01")
                    {
                        if (hit[0] != null) return;
                        DirectPartInfo p = new DirectPartInfo();
                        p.Lep = row["LEP"];
                        p.PtNo = row["TRS_PTNO"];
                        p.CarCd = row["TRS_CARCD"];
                        p.WhsFl = row["TRS_WHSFL"];
                        p.LocNo = row["LOC_LOCNO"];
                        p.SalQt = row["TRS_SALQT"];
                        p.SalPrc = row["TRS_SAL_PRC"];
                        p.SalAmt = row["SAL_AMT"];
                        p.AvlQt = row["INV_AVLQT"];
                        p.AftPtNo = row["UDPM_AFT_PTNO"];
                        p.SuCd = row["UDPM_SU_CD"];
                        p.InvMerge = row["UDPM_INV_MERGE"];
                        p.AdjtYn = row["TRS_ADJT_YN"];
                        p.AdjtCalQty = row["TRS_ADJT_CAL_QTY"];
                        p.AdjtProcQty = row["TRS_ADJT_PROC_QTY"];
                        p.PtNm = row["HPM_PTNM_A"];
                        p.StockDesc = row["STOCK_DESC"];
                        hit[0] = p;
                    }
                    else if (ds == "ds_Output01")
                    {
                        // 140_S04 가 돌려주는 LOC. 웹은 이 값으로 S01 의 LOC 를 덮어쓴다.
                        if (locNo[0] == null) locNo[0] = row["LOC_LOCNO"];
                    }
                });

            Check(res);

            DirectPartInfo r0 = hit[0];
            if (r0 != null && locNo[0] != null && locNo[0].Length > 0)
                r0.LocNo = locNo[0];

            return r0;
        }

        // ------------------------------------------------------------------
        // fn_Save : plus:PL142_W01_P01 (TYPE = M)
        // ------------------------------------------------------------------
        public static DirectSaveResult Save(ArrayList rows, string vndMn, string vndSb, VapInfo vap)
        {
            if (rows == null || rows.Count == 0)
                throw new HaimsException("저장할 내역이 없습니다.");

            // RNUM 은 1부터 다시 매기고, 합계는 구입단가의 합이다(웹 로직 그대로).
            int total = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                DirectInboundRow row = (DirectInboundRow)rows[i];
                row["RNUM"] = (i + 1).ToString();
                total += DirectInboundRow.ParseInt(row["TRS_SAL_PRC"]);
            }

            string ym = DateTime.Now.ToString("yyyyMM");
            string header = BuildHeaderXml(vndMn, vndSb, vap, total, ym);
            string detail = BuildDetailXml(rows);

            TitRequest r = new TitRequest();
            r.AddInputDataset("ds_PL142_Input",
                new string[] { "xmlHeader", "xmlDetail", "USRID", "AGTCD" },
                new string[] { header, detail, Usr, Agt });
            r.AddEmptyDataset("ds_PL142_Output");
            r.AddMulti("plus:PL142_W01_P01");
            AuthService.AddSessionCommon(r);

            DirectSaveResult[] hit = new DirectSaveResult[1];

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_Save", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_PL142_Output" || hit[0] != null) return;
                    DirectSaveResult s = new DirectSaveResult();
                    s.VchYm = row["OUTVCHYM"];
                    s.VchSeq = row["OUTVCHSEQ"];
                    hit[0] = s;
                });

            if (res.IsError)
                throw new HaimsException("시스템 장애가 발생하였습니다. 시스템 관리자에게 문의하세요.");

            return (hit[0] == null) ? new DirectSaveResult() : hit[0];
        }

        // ------------------------------------------------------------------
        // fn_SaveGbn : plus:PL142_W01_U01
        // 저장으로 받은 전표번호에 PDA 처리 표시를 남긴다. 저장 직후 이어서 부른다.
        // ------------------------------------------------------------------
        public static void SaveGbn(string vchYm, string vchSeq)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL142_W01_U01");
            r.AddParam("AGTCD", Agt);
            r.AddParam("TRS_VCHYM", vchYm);
            r.AddParam("TRS_VCHSEQ", vchSeq);
            r.AddParam("TRS_HOLO_NO", HoloNo);
            AuthService.AddSessionCommon(r);

            TitResult res = HaimsHttp.PostStream(Pgm, "fn_SaveGbn", r.Build(), null);

            if (res.IsError)
                throw new HaimsException("시스템 장애가 발생하였습니다. 시스템 관리자에게 문의하세요.");
        }

        // ------------------------------------------------------------------
        // 저장 전문 조립
        // ------------------------------------------------------------------

        /// <summary>
        /// gfn_GetRsvHeaderStr 대응. 키 순서까지 웹과 맞춘다
        /// (웹은 Hashtable.keys() 순서라 보장이 없지만, 서버가 이름으로 읽으므로 무해하다).
        /// </summary>
        private static string BuildHeaderXml(string vndMn, string vndSb,
                                             VapInfo vap, int total, string ym)
        {
            if (vap == null) vap = new VapInfo();
            string t = total.ToString();

            StringBuilder sb = new StringBuilder(768);
            sb.Append("<HEADER><record>");
            Put(sb, "VNDCD_MN", vndMn);
            Put(sb, "VNDCD_SB", vndSb);
            Put(sb, "HIDDEN_TRF_VCHSEQ", "");
            Put(sb, "HIDDEN_TRF_VCHYM", "");
            Put(sb, "INS_USRID", "");
            Put(sb, "JOB_TYPE", "I");
            Put(sb, "LST_PRTDTTM", "");
            Put(sb, "TRF_CNT", "");
            Put(sb, "TRS_REFNO", "");
            Put(sb, "TRF_SAL_AMT", t);
            Put(sb, "TRF_SAL_AMT_BAL", t);
            Put(sb, "TRF_SAL_AMT_CRD", "0");
            Put(sb, "TRF_SAL_AMT_CSH", "0");
            Put(sb, "TRF_UPD_ID", "");
            Put(sb, "VAP_BAL_AMT", vap.BalAmt);
            Put(sb, "VAP_LST_BAL_AMT", vap.LstBalAmt);
            Put(sb, "VAP_LST_BAL_AMT2", "");
            Put(sb, "VAP_SAL_AMT", vap.SalAmt);
            Put(sb, "VAP_TTL_AMT", vap.TtlAmt);
            Put(sb, "VCHNO_CD", "1");
            Put(sb, "VCHNO_SEQ", "");
            Put(sb, "VCHNO_YM", ym);
            Put(sb, "VNDNM", "");
            Put(sb, "AMT_TYPE_BAL", "Y");
            Put(sb, "AMT_TYPE_CRD", "");
            Put(sb, "AMT_TYPE_CSH", "");
            Put(sb, "JOBTYPE", "I");
            Put(sb, "TRF_SALCD", "IA");
            Put(sb, "CRD_NM", "");
            Put(sb, "TRF_CRDTY", "");
            Put(sb, "TRF_CRD_DC_RATE", "");
            Put(sb, "TRF_REMARK", "");
            sb.Append("</record></HEADER>");
            return sb.ToString();
        }

        /// <summary>
        /// grd.getRangeXML(0, n) 직렬화 대응.
        ///
        /// 그리드는 baseNode="dataset" / repeatNode="record" 로 선언돼 있고,
        /// getArrXML 이 각 record 에 status / statusValue / id 를 붙인다.
        /// 화면에서 insertRow 로 만든 행이므로 상태는 C(신규) = 2 다.
        /// 웹은 마지막에 TRS_ADJT_YN 의 Y/N 을 1/0 으로 바꿔 보낸다.
        /// </summary>
        private static string BuildDetailXml(ArrayList rows)
        {
            StringBuilder sb = new StringBuilder(2048);
            sb.Append("<dataset>");

            for (int i = 0; i < rows.Count; i++)
            {
                DirectInboundRow row = (DirectInboundRow)rows[i];

                sb.Append("<record status=\"2\" statusValue=\"C\" id=\"")
                  .Append(i).Append("\">");

                for (int c = 0; c < DirectInboundRow.Columns.Length; c++)
                {
                    string col = DirectInboundRow.Columns[c];
                    string val = row[col];

                    if (col == "TRS_ADJT_YN")
                        val = (val == "Y") ? "1" : "0";

                    Put(sb, col, val);
                }

                sb.Append("</record>");
            }

            sb.Append("</dataset>");
            return sb.ToString();
        }

        /// <summary>
        /// 값에 개행/탭이 섞이면 웹도 제거하고 보낸다(replace(/\n/g)/replaceAll('\t')).
        /// 꺾쇠는 여기서 escape 하지 않는다 — TitRequest 가 값을 실을 때 한 번만 escape 한다.
        /// </summary>
        private static void Put(StringBuilder sb, string name, string value)
        {
            sb.Append("<").Append(name).Append(">");
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    char ch = value[i];
                    if (ch == '\r' || ch == '\n' || ch == '\t') continue;
                    sb.Append(ch);
                }
            }
            sb.Append("</").Append(name).Append(">");
        }
    }
}
