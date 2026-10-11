using System;
using System.Collections;
using System.Text;

namespace HaimsPda.Net
{
    /// <summary>
    /// 응답 레코드 1건을 통째로 들고 다니는 행. 원본 화면이 숨은 그리드(hidGrd)에 응답을 그대로 넣고
    /// 컬럼 이름으로 꺼내 쓰는 구조라, 컬럼을 하나하나 옮기지 않고 그대로 보관한다.
    /// 없는 컬럼은 빈 문자열.
    /// </summary>
    public sealed class OutRow
    {
        private readonly Hashtable _v = new Hashtable();

        public string this[string col]
        {
            get { object o = _v[col]; return (o == null) ? "" : (string)o; }
            set { _v[col] = (value == null) ? "" : value; }
        }

        internal static OutRow From(Row r)
        {
            OutRow o = new OutRow();
            foreach (string k in r.Columns) o._v[k] = r[k];
            return o;
        }
    }

    /// <summary>
    /// 출고(200) 화면 공용 서버 호출.
    ///
    ///   [201] 피킹대상조회   PL201_W01  S01
    ///   [202]/[210]/[280]    PL210_W01_S02 (피킹 라인) — 201 세부와 210/280 조회가 같은 SQL
    ///   [210]/[280] 선점     PL200_W01_S01 (판정) / U01 (선점) / U03 (해제)
    ///   [210]/[280] 저장     PL210_W01_U01 (+ 할당번호 있으면 PL200_W01_U02)
    ///   [210]/[280] 반품     PL100_W01_P01 (xmlHeader / xmlDetail) — 211 에서 수량을 받아 저장 직전에 실행
    ///   [220] 판매증표출고   PL220_W01_S01 / U01 (+ PL200_W01_U02)
    ///   [221] 판매증표선택   PL221_W01_S01 / S02 / S03
    ///   [261]→[260]→[262]    PL260_W01_S01 / S02 / S03 / U01 / S05
    ///   [251] 배송분류(CASE) PL251_W01_S01 / S03 / S05 / S06 / U01_PROC / I01_PROC / U02_PROC
    ///
    /// 실측(HAR 2026-09-20) : 201 조회, 210 조회·선점·저장(U01) 까지. 나머지 저장 전문은 원본 소스 기준.
    /// 전부 HAIMS_MOBILE_ACTION.
    /// </summary>
    public static class OutboundService
    {
        public const string Pgm201 = "PL201_W01.xml";
        public const string Pgm202 = "PL202_W01.xml";
        public const string Pgm210 = "PL210_W01.xml";
        public const string Pgm211 = "PL211_W01.xml";
        public const string Pgm220 = "PL220_W01.xml";
        public const string Pgm221 = "PL221_W01.xml";
        public const string Pgm251 = "PL251_W01.xml";
        public const string Pgm260 = "PL260_W01.xml";
        public const string Pgm261 = "PL261_W01.xml";
        public const string Pgm262 = "PL262_W01.xml";
        public const string Pgm280 = "PL280_W01.xml";

        private static string UsrCol(string col)
        {
            UserInfo u = Session.User; return u == null ? "" : u[col];
        }

        private static string Agt { get { return UsrCol("USR_AGTCD"); } }
        private static string Usr { get { return UsrCol("USR_USRID"); } }

        private static void Check(TitResult res)
        {
            if (res.IsError) throw new HaimsException(res.ErrorMsg);
        }

        /// <summary>
        /// 단문 실행(UPDATE/INSERT/PROC) 공용. 응답 _none(또는 none) 데이터셋의 MSG_CODE 가 S 가 아니면 실패.
        /// failMsg 가 있으면 실패 문구를 그것으로 바꾼다 (원본이 MP108 등 고정 문구를 띄우는 화면).
        /// </summary>
        private static void Exec(string pgm, string fn, TitRequest r, string failMsg)
        {
            string[] fail = new string[1];

            TitResult res = HaimsHttp.PostStream(pgm, fn, r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "_none" && ds != "none") return;
                    if (fail[0] != null || !row.Has("MSG_CODE")) return;
                    if (row["MSG_CODE"] != "S") fail[0] = row["MSG"];
                });

            if (res.IsError)
                throw new HaimsException(failMsg != null ? failMsg : res.ErrorMsg);
            if (fail[0] != null)
                throw new HaimsException(failMsg != null ? failMsg
                    : (fail[0].Length > 0 ? fail[0] : "저장중 에러가 발생하였습니다."));
        }

        private static ArrayList Rows(string pgm, string fn, TitRequest r, string dataset)
        {
            ArrayList list = new ArrayList();
            TitResult res = HaimsHttp.PostStream(pgm, fn, r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == dataset) list.Add(OutRow.From(row));
                });
            Check(res);
            return list;
        }

        // ==================================================================
        // 표기 / 조립 도우미
        // ==================================================================

        /// <summary>"202608-00005" -> "20260800005" (원본 gfn_Left(6) + gfn_Right(5))</summary>
        public static string VchKey(string vchno)
        {
            if (vchno == null) return "";
            string v = vchno.Trim();
            if (v.Length < 11) return v.Replace("-", "");
            return v.Substring(0, 6) + v.Substring(v.Length - 5);
        }

        /// <summary>IN 목록 : 'a','b' (원본이 클라이언트에서 조립해 보낸다. 이슈 #20)</summary>
        public static string InList(ArrayList keys)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keys.Count; i++)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append("'").Append((string)keys[i]).Append("'");
            }
            return sb.ToString();
        }

        // ==================================================================
        // 공통 콤보
        // ==================================================================

        /// <summary>판매구분 MT/07 (gfn_SearchComCode). HAR : 5건, 코드 2(계약)부터</summary>
        public static ArrayList GetSalgbCodes(string pgm)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("common:CODESEARCH");
            r.AddParam("CODNM_TYPE", "");
            r.AddParam("CODE_SQL",
                "AND ((CDM_LRG_GRP = 'MT' AND CDM_MID_GRP = '07' AND CDM_AGTCD_M IN ('COMM')))");
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();
            TitResult res = HaimsHttp.PostStream(pgm, "gfn_SearchComCode", r.Build(),
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

        public static ArrayList GetWarehouses(string pgm)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL000_W01_S01");
            r.AddParam("USR_USRID", Usr);
            AuthService.AddSessionCommon(r);

            ArrayList list = new ArrayList();
            TitResult res = HaimsHttp.PostStream(pgm, "gfn_SearchWHS_Plus", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds != "ds_Output") return;
                    string v = row["LOC_WHSCD"];
                    if (v.Length > 0) list.Add(v);
                });
            Check(res);
            return list;
        }

        // ==================================================================
        // [201] 피킹대상 업체별 집계 : PL201_W01_S01 -> ds_List
        //   TRS_VNDMN / VEN_VNDNM / PIC_CNT / TRS_SALDT / PIC_VND_YN
        // ==================================================================
        public static ArrayList SearchPickTargets(string whs, string salgb, string fromDt, string toDt)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL201_W01_S01");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("TRS_SALGB", salgb);
            r.AddParam("USERID", Usr);
            r.AddParam("TRS_SALDT", toDt);
            r.AddParam("FROM_SALDT", fromDt);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm201, "fn_Search", r, "ds_List");
        }

        // ==================================================================
        // [202]/[210]/[280] 피킹 라인 : PL210_W01_S02 -> ds_Output02 (LOC, 부번 순)
        // ==================================================================
        public static ArrayList SearchPickLines(string pgm, string fn, string whs, string salgb,
                                                string fromDt, string toDt, string vndmn)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL210_W01_S02");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_SALDT", toDt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TRS_VNDMN", vndmn);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("TRS_SALGB", salgb);
            r.AddParam("USERID", Usr);
            AuthService.AddSessionCommon(r);
            return Rows(pgm, fn, r, "ds_Output02");
        }

        // ==================================================================
        // 전문점 (203/204/230/240/281) — 노선(RUTCD)·지역(AREA) 조건
        // ==================================================================
        public const string Pgm203 = "PL203_W01.xml";
        public const string Pgm204 = "PL204_W01.xml";
        public const string Pgm230 = "PL230_W01.xml";
        public const string Pgm240 = "PL240_W01.xml";
        public const string Pgm250 = "PL250_W01.xml";
        public const string Pgm281 = "PL281_W01.xml";

        /// <summary>노선/지역 콤보 : PL203_W01_S02 -> ds_List (RUT_RUTCD / RUT_RUTNM / GUBUN : R=노선, A=지역)</summary>
        public static ArrayList SearchLineArea(string pgm)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL203_W01_S02");
            r.AddParam("AGTCD", Agt);
            AuthService.AddSessionCommon(r);
            return Rows(pgm, "fn_SearchLineArea", r, "ds_List");
        }

        /// <summary>
        /// 조회 조건 하나 (원본 tit_AddInputParamData 를 순서대로 쌓은 것).
        /// 203/204 는 노선·지역 선택 조합에 따라 SEL_NO / RUTCD / AREA 를 넣었다 뺐다 한다.
        /// 화면이 원본 분기대로 채워서 넘긴다. 값이 null 인 키는 보내지 않는다.
        /// </summary>
        public sealed class LineCond
        {
            public string SelNo;   // Y / T / R / A
            public string Rutcd;
            public string Area;
        }

        private static void AddCond(TitRequest r, LineCond c)
        {
            if (c == null) return;
            if (c.SelNo != null) r.AddParam("SEL_NO", c.SelNo);
            if (c.Rutcd != null) r.AddParam("RUTCD", c.Rutcd);
            if (c.Area != null) r.AddParam("AREA", c.Area);
        }

        /// <summary>[203] 노선·지역별 건수 : PL203_W01_S01 -> ds_List (SCD_RUTCD / LOC_PIC_AREA / PIC_CNT)</summary>
        public static ArrayList SearchShopLines(string whs, string fromDt, string toDt, LineCond c)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL203_W01_S01");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_SALDT", toDt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("USERID", Usr);
            AddCond(r, c);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm203, "fn_Search", r, "ds_List");
        }

        /// <summary>[204] 업체별 건수 : PL204_W01_S01 -> ds_List (TRS_VNDMN / PIC_CNT / VEN_VNDNM / TRS_SALDT / SCD_RUT_SEQ / PIC_VND_YN)</summary>
        public static ArrayList SearchShopTargets(string whs, string fromDt, string toDt, LineCond c)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL204_W01_S01");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_SALDT", toDt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("USERID", Usr);
            AddCond(r, c);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm204, "fn_Search", r, "ds_List");
        }

        /// <summary>
        /// [202(전문점)/230/281] 피킹 라인 : PL230_W01_S01 -> ds_Output02.
        /// 210 의 PL210_W01_S02 에 AREA / RUTCD 가 붙는다. salgb 가 null 이면 TRS_SALGB 를 보내지 않는다(204 세부).
        /// </summary>
        public static ArrayList SearchShopPickLines(string pgm, string fn, string whs, string salgb,
                                                    string fromDt, string toDt, string vndmn,
                                                    string area, string line)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL230_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_SALDT", toDt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TRS_VNDMN", vndmn);
            r.AddParam("TRS_WHSFL", whs);
            if (salgb != null) r.AddParam("TRS_SALGB", salgb);
            r.AddParam("USERID", Usr);
            r.AddParam("AREA", area);
            r.AddParam("RUTCD", line == "NONE" ? "" : line);
            AuthService.AddSessionCommon(r);
            return Rows(pgm, fn, r, "ds_Output02");
        }

        /// <summary>
        /// [240] LOC별 피킹 라인 : PL240_W01_S01 -> ds_List03.
        /// vndmn 은 204 에서 고른 업체 IN 목록('a','b') 그대로.
        /// </summary>
        public static ArrayList SearchLocPickLines(string whs, string salgb, string fromDt, string toDt,
                                                   string vndIn, string area, string line)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL240_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("TRS_VNDMN", vndIn);
            r.AddParam("TRS_SALGB", salgb);
            r.AddParam("USERID", Usr);
            r.AddParam("TRS_SALDT", toDt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("AREA", area);
            r.AddParam("RUTCD", line == "NONE" ? "" : line);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm240, "fn_Search", r, "ds_List03");
        }

        /// <summary>[240] TAG 확인 : PL240_W01_S02 -> ds_List03 첫 건. 없으면 null</summary>
        public static OutRow SearchLocTag(string tag)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL240_W01_S02");
            r.AddParam("USRID", Usr);
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_TEMP_TAG", tag);
            AuthService.AddSessionCommon(r);
            ArrayList rows = Rows(Pgm240, "fn_SearchTag", r, "ds_List03");
            return rows.Count == 0 ? null : (OutRow)rows[0];
        }

        /// <summary>
        /// [240] 저장 : PL240_W01_U01 + U02.
        /// 원본은 할당번호가 있으면 USERID/AGTCD/TRS_WSFNO/PTNO/LEP 를 더 넣지만 PL200_W01_U02 를 쌓지 않는다.
        /// 파라미터만 그대로 따라 보낸다.
        /// </summary>
        public static void SaveLocPicking(string vchIn, string ptno, string lep, string picDt, string picTm,
                                          string tag, string salCnt, string salQt, string wsfIn)
        {
            string p = PartNo.Key(ptno);
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL240_W01_U01");
            r.AddSearch("plus:PL240_W01_U02");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("VCHNO", vchIn);
            r.AddParam("TRS_VCHNO", vchIn);
            r.AddParam("TRS_PTNO", p);
            r.AddParam("TRS_LEP", lep);
            r.AddParam("TRS_PICID", Usr);
            r.AddParam("TRS_PDA_PICDT", picDt);
            r.AddParam("TRS_PDA_PICTM", picTm);
            r.AddParam("TRS_TEMP_TAG", tag);
            r.AddParam("SAL_CNT", salCnt);
            r.AddParam("SALQT", salQt);
            r.AddParam("USRID", Usr);
            if (wsfIn.Length > 0)
            {
                r.AddParam("USERID", Usr);
                r.AddParam("AGTCD", Agt);
                r.AddParam("TRS_WSFNO", wsfIn);
                r.AddParam("PTNO", p);
                r.AddParam("LEP", lep);
            }
            AuthService.AddSessionCommon(r);
            Exec(Pgm240, "fn_SavePicking", r, null);
        }

        // ==================================================================
        // [250] 피킹분류(TAG)
        // ==================================================================

        /// <summary>TAG : PL250_W01_S03 -> ds_Output03 (USE_YN / TAG_PTNO_ICNT / TAG_PTNO_SCNT). 없으면 null</summary>
        public static OutRow SearchClassTag(string tag)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL250_W01_S03");
            r.AddParam("USRID", Usr);
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_TEMP_TAG", tag);
            AuthService.AddSessionCommon(r);
            ArrayList rows = Rows(Pgm250, "fn_Search_Tag", r, "ds_Output03");
            return rows.Count == 0 ? null : (OutRow)rows[0];
        }

        /// <summary>부번 : PL250_W01_S01 -> ds_Output01 (업체별 행)</summary>
        public static ArrayList SearchClassPart(string ptno, string picDt, string tag)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL250_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("PICDT", picDt);
            r.AddParam("TRS_TEMP_TAG", tag);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm250, "fn_Search", r, "ds_Output01");
        }

        /// <summary>
        /// 저장 : PL250_W01_U01 + U02 (+ TAG 마지막 건이면 U02 한 번 더, TAG_STAT=N).
        /// 원본 그대로 U02 를 두 번 쌓는다. 파라미터는 공유라 첫 U02 도 TAG_STAT=N 을 받는다.
        /// </summary>
        public static void SaveClassTag(string lep, string ptno, string tag, int qty, int cnt, bool closeTag)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL250_W01_U01");
            r.AddSearch("plus:PL250_W01_U02");
            r.AddParam("USRID", Usr);
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_LEP", lep);
            r.AddParam("TRS_PTNO", PartNo.Key(ptno));
            r.AddParam("TRS_TEMP_TAG", tag);
            r.AddParam("TRS_SALQT", qty.ToString());
            r.AddParam("TRS_CNT", cnt.ToString());
            if (closeTag)
            {
                r.AddSearch("plus:PL250_W01_U02");
                r.AddParam("TAG_STAT", "N");
            }
            AuthService.AddSessionCommon(r);
            Exec(Pgm250, "fn_Save", r, null);
        }

        // ==================================================================
        // 선점 (plus.js fn_Search_Preoccupy / fn_Preoccupy)
        // ==================================================================

        /// <summary>PL200_W01_S01 -> ds_List/PREOCC. 결과가 없으면 null (원본 "선점대상이 없습니다")</summary>
        public static string SearchPreoccupy(string pgm, string vchIn, string ptno, string lep)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL200_W01_S01");
            r.AddParam("USERID", Usr);
            r.AddParam("AGTCD", Agt);
            r.AddParam("VCHNO", vchIn);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("LEP", lep);
            AuthService.AddSessionCommon(r);

            string[] hit = new string[1];
            TitResult res = HaimsHttp.PostStream(pgm, "fn_Search_Preoccupy", r.Build(),
                delegate(string ds, Row row)
                {
                    if (ds == "ds_List" && hit[0] == null) hit[0] = row["PREOCC"];
                });
            Check(res);
            return hit[0];
        }

        /// <summary>claim=true : PL200_W01_U01 (선점), false : PL200_W01_U03 (해제). 성공 여부만 돌려준다.</summary>
        public static bool Preoccupy(string pgm, bool claim, string vchIn, string ptno, string lep)
        {
            TitRequest r = new TitRequest();
            r.AddSearch(claim ? "plus:PL200_W01_U01" : "plus:PL200_W01_U03");
            r.AddParam("USERID", Usr);
            r.AddParam("AGTCD", Agt);
            r.AddParam("VCHNO", vchIn);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("LEP", lep);
            AuthService.AddSessionCommon(r);
            try { Exec(pgm, "fn_Preoccupy", r, null); return true; }
            catch (HaimsException) { return false; }
        }

        // ==================================================================
        // 피킹 저장 : PL210_W01_U01 / PL220_W01_U01 (+ PL200_W01_U02)
        //   sqlU01 : "plus:PL210_W01_U01" 또는 "plus:PL220_W01_U01"
        //   vchIn  : 'yyyyMMsssss','...' (210 은 VCHNO 와 TRS_VCHNO 에 같은 값, 220 은 TRS_VCHNO 만)
        //   picDt / picTm : 원본은 화면을 연 시각(dateStr/timeStr)을 보낸다 (HAR 확인)
        // ==================================================================
        public sealed class PickSave
        {
            public string SqlU01 = "plus:PL210_W01_U01";
            public bool SendVchno = true;   // 210/280 : VCHNO 도 보냄
            public string VchIn = "";
            public string Vndmn = "";
            public string Whsfl = "";
            public string Grtno = "";
            public string Ptno = "";
            public string Lep = "";
            public string PicDt = "";
            public string PicTm = "";
            public string WsfIn = "";       // 'a','b' — 비어 있으면 U02 생략 (ITS피킹만)
        }

        public static void SavePicking(string pgm, PickSave s)
        {
            string ptno = PartNo.Key(s.Ptno);

            TitRequest r = new TitRequest();
            r.AddSearch(s.SqlU01);
            r.SetParam("TRS_AGTCD", Agt);
            if (s.SendVchno) r.SetParam("VCHNO", s.VchIn);
            r.SetParam("TRS_VCHNO", s.VchIn);
            r.SetParam("TRS_VNDMN", s.Vndmn);
            r.SetParam("TRS_WHSFL", s.Whsfl);
            r.SetParam("TRS_GRTNO", s.Grtno);
            r.SetParam("TRS_PTNO", ptno);
            r.SetParam("TRS_LEP", s.Lep);
            r.SetParam("TRS_PICID", Usr);
            r.SetParam("TRS_PDA_PICDT", s.PicDt);
            r.SetParam("TRS_PDA_PICTM", s.PicTm);
            if (s.WsfIn.Length > 0)
            {
                r.AddSearch("plus:PL200_W01_U02");
                r.SetParam("USERID", Usr);
                r.SetParam("AGTCD", Agt);
                r.SetParam("TRS_WSFNO", s.WsfIn);
                r.SetParam("PTNO", ptno);
                r.SetParam("LEP", s.Lep);
            }
            AuthService.AddSessionCommon(r);
            Exec(pgm, "fn_SavePicking", r, null);
        }

        // ==================================================================
        // 반품 : plus.js gfn_SaveReturn -> PL100_W01_P01 (xmlHeader / xmlDetail)
        //   row : 피킹 라인 (TRS_SALQT 는 211 에서 받은 출고수량, CNCL_SALQT 는 반품수량으로 바꿔 둔 것)
        //   반환 : OUTAGTCD (비어 있으면 원본은 MP593 안내)
        // ==================================================================
        public static string SaveReturn(string pgm, OutRow row)
        {
            string vch = row["TRS_VCHNO"];
            string ym = Left(vch, 6), seq = Right(vch, 5);
            long amt = (long)ToInt(row["TRS_SALQT"]) * ToInt(row["SAL_PRC"]);

            // 원본 Hashtable 키 순서 그대로 (서버는 이름으로 읽는다)
            string[] h = {
                "VNDCD_MN", row["TRS_VNDMN"], "VNDCD_SB", row["TRS_VNDSB"], "VNDCD_WM", "", "VNDCD_WS", "",
                "LOC_ORD", "", "REM", "", "GRT_GRTNO", "",
                "HIDDEN_TRF_VCHSEQ", seq, "HIDDEN_TRF_VCHYM", ym,
                "INS_USRID", Usr, "JOB_TYPE", "D", "LSCNO", "", "LST_PRTDTTM", "", "PRT_TYPE", "", "PRT_YN", "Y",
                "TRF_CNT", "4", "TRF_GRTNO", row["TRS_GRTNO"], "TRF_GRTSEQ", "", "TRF_GRTYM", "", "TRF_INS_ID", "",
                "TRF_PRT_CNT", "1", "TRF_REFNO", "", "TRF_REMARK", "",
                "TRF_SAL_AMT", amt.ToString(), "TRF_SAL_AMT_BAL", amt.ToString(),
                "TRF_SAL_AMT_CRD", "0", "TRF_SAL_AMT_CSH", "0", "TRF_SND_SNDTM", "",
                "VAR_BAL_AMT", "0", "VAR_LST_BAL_AMT", "0", "VAR_SAL_AMT", "0", "VAR_TTL_AMT", "0",
                "VCHNO_CD", "2", "VCHNO_SEQ", seq, "VCHNO_YM", ym,
                "VEN_LIMIT_AMT", row["VEN_LIMIT_AMT"], "VNDNM", "", "VNDWNM", "",
                "AMT_TYPE_BAL", "", "AMT_TYPE_CRD", "", "AMT_TYPE_CSH", "",
                "JOBTYPE_D", "", "JOBTYPE_I", "", "JOBTYPE_R", "", "JOBTYPE_U", "",
                "PDA_USE", "N", "PRINT", "0", "PRINTTYPE", "0", "TRFT_TIME_STAMP", "",
                "TRF_SALCD", "SY", "TRF_SALGB", row["TRS_SALGB"], "TRF_SALTY", row["TRF_SALTY"],
                "TRF_WHSCD", row["TRS_WHSFL"], "CRD_NM", "", "TRF_CRDTY", "", "TRF_CRD_DC_RATE", "0"
            };
            string[] d = {
                "CHK", "", "SEQ", "", "RNUM", "1", "LEP", row["TRS_LEP"], "TRS_RETID", "Y", "RETID_HIDDEN", "",
                "PTNO_HIDDEN", row["TRS_PTNO"], "TRS_PTNO", row["TRS_PTNO"], "PTNO_POPUP", "", "HPM_PTNM_A", "",
                "TRS_CARCD", row["CARCD"], "TRS_WHSFL", row["TRS_WHSFL"], "INV_AVLQT", row["INV_AVLQT"],
                "TRS_SALQT", row["TRS_SALQT"], "TRS_SAL_PRC", row["TRS_SAL_PRC"], "TRS_APP_PRC", row["APP_PRC"],
                "SAL_AMT", amt.ToString(), "INV_CTLQT", "0", "SPEC_YN", "N", "TRS_STDPRC", row["STDPRC"],
                "SALE_TYPE", "", "PRICE_POPUP", "", "INV_RSVQT", "", "TRST_RSVNO", "", "PRICE_RULE_DESC", "",
                "STOCK_DESC", "", "TRS_AGTCD", Agt, "TRS_VCHYM", ym, "TRS_VCHSEQ", seq, "TRS_VCHCD", "2",
                "TRS_GRTNO", row["TRS_GRTNO"], "CNCL_SALQT", row["CNCL_SALQT"]
            };

            StringBuilder hx = new StringBuilder("<HEADER><record>");
            for (int i = 0; i < h.Length; i += 2) Tag(hx, h[i], h[i + 1]);
            hx.Append("</record></HEADER>");

            StringBuilder dx = new StringBuilder("<dataset><record status=\"1\" statusValue=\"U\" id=\"0\">");
            for (int i = 0; i < d.Length; i += 2) Tag(dx, d[i], d[i + 1]);
            dx.Append("</record></dataset>");

            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL100_W01_P01");
            r.AddParam("xmlHeader", hx.ToString());
            r.AddParam("xmlDetail", dx.ToString());
            r.AddParam("USRID", Usr);
            r.AddParam("AGTCD", Agt);
            AuthService.AddSessionCommon(r);

            string[] outAgt = new string[] { "" };
            TitResult res = HaimsHttp.PostStream(pgm, "gfn_SaveReturn", r.Build(),
                delegate(string ds, Row row2)
                {
                    if (ds == "ds_HT01_Output" && outAgt[0].Length == 0) outAgt[0] = row2["OUTAGTCD"];
                });
            Check(res);
            return outAgt[0];
        }

        private static void Tag(StringBuilder sb, string k, string v)
        {
            sb.Append('<').Append(k).Append('>').Append(v).Append("</").Append(k).Append('>');
        }

        // ==================================================================
        // [220] 판매증표 라인 : PL220_W01_S01 -> ds_List01
        // ==================================================================
        public static ArrayList SearchSlipLines(string vchYm, string vchSeq)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL220_W01_S01");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_VCHYM", vchYm);
            r.AddParam("TRS_VCHSEQ", vchSeq);
            r.AddParam("USERID", Usr);
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            AuthService.AddSessionCommon(r);
            return Rows(Pgm220, "fn_SearchVchno", r, "ds_List01");
        }

        // ==================================================================
        // [221] 판매증표선택
        //   업체 : S02 -> ds_List (TRS_VNDMN / VEN_VNDNM)
        //   정비 : S03 -> ds_List_WM (TRF_VNDCD_WM / VEN_VNDNM)
        //   증표 : S01 -> ds_List (TRS_VCHNO / TRS_CNT / VEN_VNDNM)
        // ==================================================================
        public static ArrayList SearchSlipVendors(string whs, string prtDt, bool wm)
        {
            TitRequest r = new TitRequest();
            r.AddSearch(wm ? "plus:PL221_W01_S03" : "plus:PL221_W01_S02");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("USERID", Usr);
            r.AddParam("TRS_FST_PRTDT", prtDt);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm221, wm ? "fn_Search_VenList2" : "fn_Search_VenList", r, wm ? "ds_List_WM" : "ds_List");
        }

        public static ArrayList SearchSlips(string whs, string prtDt, string vndmn, string vndmn2)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL221_W01_S01");
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_WHSFL", whs);
            r.AddParam("TRS_FST_PRTDT", prtDt);
            r.AddParam("USERID", Usr);
            r.AddParam("TRS_VNDMN", vndmn);
            r.AddParam("TRS_VNDMN2", vndmn2);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm221, "fn_SearchVchno", r, "ds_List");
        }

        // ==================================================================
        // [261] 피킹분류(증표) 선택
        //   업체 : PL260_W01_S01 -> ds_List (TRS_VNDMN / VEN_VNDNM)
        //   증표 : PL260_W01_S02 -> ds_List (TRS_VCHNO / TRS_CNT / TRS_SALGB)
        // ==================================================================
        public static ArrayList SearchClassifyVendors(string whs, string fromDt, string toDt)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL260_W01_S01");
            r.AddParam("AGTCD", Agt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TO_SALDT", toDt);
            r.AddParam("WHSCD", whs);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm261, "fn_Search_VenList", r, "ds_List");
        }

        public static ArrayList SearchClassifySlips(string whs, string fromDt, string toDt, string vndmn)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL260_W01_S02");
            r.AddParam("AGTCD", Agt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TO_SALDT", toDt);
            r.AddParam("WHSCD", whs);
            r.AddParam("VNDMN", vndmn);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm261, "fn_Search", r, "ds_List");
        }

        // ==================================================================
        // [260] 배송분류(판매증표)
        //   부번 : PL260_W01_S03 -> ds_List 첫 건 (TRS_LEP / INV_PTNM_A / TRS_SALQT / PIC_CNT / TRS_SALGB)
        //   저장 : PL260_W01_U01 (조회 빌더로 보내는 UPDATE. 응답 데이터셋 이름이 "none" — 실측)
        // ==================================================================
        public static OutRow SearchClassifyPart(string fromDt, string toDt, string vndmn, string vchIn, string ptno)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL260_W01_S03");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", Agt);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TO_SALDT", toDt);
            r.AddParam("VNDMN", vndmn);
            r.AddParam("VCHYM_SEQ", vchIn);
            r.AddParam("PTNO", PartNo.Key(ptno));
            AuthService.AddSessionCommon(r);
            ArrayList rows = Rows(Pgm260, "fn_Search", r, "ds_List");
            return rows.Count == 0 ? null : (OutRow)rows[0];
        }

        public static void SaveClassify(string vchIn, string lep, string ptno, string vndmn,
                                        string fromDt, string toDt)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL260_W01_U01");
            r.AddParam("USRID", Usr);
            r.AddParam("AGTCD", Agt);
            r.AddParam("VCHYM_SEQ", vchIn);
            r.AddParam("LEP", lep);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("VNDMN", vndmn);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TO_SALDT", toDt);
            AuthService.AddSessionCommon(r);
            Exec(Pgm260, "fn_Save", r, null);
        }

        /// <summary>[262] PL260_W01_S05 -> ds_List_260_S05 (TRS_LEP / TRS_PTNO / TRS_SALQT / VCHNO)</summary>
        public static ArrayList SearchClassifyDetail(string whs, string fromDt, string toDt, string vndmn, string vchIn)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL260_W01_S05");
            r.AddParam("AGTCD", Agt);
            r.AddParam("WHSCD", whs);
            r.AddParam("FROM_SALDT", fromDt);
            r.AddParam("TO_SALDT", toDt);
            r.AddParam("VNDMN", vndmn);
            r.AddParam("VCHYM_SEQ", vchIn);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm262, "fn_Search", r, "ds_List_260_S05");
        }

        // ==================================================================
        // [251] 배송분류(CASE)
        // ==================================================================

        /// <summary>TAG : S03 -> ds_Output03 (USE_YN / TAG_PTNO_ICNT / TAG_PTNO_SCNT). 없으면 null</summary>
        public static OutRow SearchTag(string tag)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL251_W01_S03");
            r.AddParam("USRID", Usr);
            r.AddParam("TRS_AGTCD", Agt);
            r.AddParam("TRS_TEMP_TAG", tag);
            AuthService.AddSessionCommon(r);
            ArrayList rows = Rows(Pgm251, "fn_Search_Tag", r, "ds_Output03");
            return rows.Count == 0 ? null : (OutRow)rows[0];
        }

        /// <summary>부번 : S01 -> ds_Output01 (업체별 행. TRS_LEP / TRS_VNDMN / TRS_SALQT / TRS_VCHNO / TRS_CNT ...)</summary>
        public static ArrayList SearchTagPart(string ptno, string picDt, string tag)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL251_W01_S01");
            r.AddParam("BRNCD_H", UsrCol("USR_BRNCD_H"));
            r.AddParam("BRNCD_K", UsrCol("USR_BRNCD_K"));
            r.AddParam("AGTCD", Agt);
            r.AddParam("PTNO", PartNo.Key(ptno));
            r.AddParam("PICDT", picDt);
            r.AddParam("TRS_TEMP_TAG", tag);
            AuthService.AddSessionCommon(r);
            return Rows(Pgm251, "fn_SearchPtno", r, "ds_Output01");
        }

        /// <summary>업체의 다음 CASE 번호 : S05 -> ds_Output05/CAS_NO</summary>
        public static string SearchCaseNo(string vndmn)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL251_W01_S05");
            r.AddParam("USRID", Usr);
            r.AddParam("CAS_AGTCD", Agt);
            r.AddParam("CAS_VENMN", vndmn);
            AuthService.AddSessionCommon(r);
            ArrayList rows = Rows(Pgm251, "fn_Search_CaseNo", r, "ds_Output05");
            return rows.Count == 0 ? "" : ((OutRow)rows[0])["CAS_NO"];
        }

        /// <summary>CASE 검증 : S06 -> ds_Output04 (USE_YN / TAG_PTNO_CNT). 없으면 null</summary>
        public static OutRow SearchCase(string vndmn, string tag, string caseNo)
        {
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL251_W01_S06");
            r.AddParam("USRID", Usr);
            r.AddParam("CAS_AGTCD", Agt);
            r.AddParam("CAS_VENMN", vndmn);
            r.AddParam("TRS_TEMP_TAG", tag);
            r.AddParam("CAS_NO", caseNo);
            AuthService.AddSessionCommon(r);
            ArrayList rows = Rows(Pgm251, "fn_Search_Case", r, "ds_Output04");
            return rows.Count == 0 ? null : (OutRow)rows[0];
        }

        public sealed class CaseSave
        {
            public string Lep = "";
            public string Ptno = "";
            public string Tag = "";
            public string Vndmn = "";
            public string SalQt = "";     // 행의 TRS_SALQT (CSD_PTNO_QTY)
            public string Cnt = "";       // 원본 m_nCntCust — 두 번째 TRS_SALQT 로 덮어써진다
            public string CaseNo = "";
            public bool CloseTag;         // U02_PROC (TAG_STAT=N) 추가
        }

        /// <summary>U01_PROC + I01_PROC (+ U02_PROC). 원본 순서대로 파라미터를 덮어쓴다.</summary>
        public static void SaveCase(CaseSave s)
        {
            string ptno = PartNo.Key(s.Ptno);
            TitRequest r = new TitRequest();
            r.AddSearch("plus:PL251_W01_U01_PROC");
            r.AddSearch("plus:PL251_W01_I01_PROC");
            r.SetParam("USRID", Usr);
            r.SetParam("TRS_AGTCD", Agt);
            r.SetParam("CSD_AGTCD", Agt);
            r.SetParam("CSD_WRKER", Usr);
            r.SetParam("CSD_INS_ID", Usr);
            r.SetParam("CSD_UPD_ID", Usr);
            r.SetParam("TRS_LEP", s.Lep);
            r.SetParam("TRS_PTNO", ptno);
            r.SetParam("TRS_TEMP_TAG", s.Tag);
            r.SetParam("TRS_VNDMN", s.Vndmn);
            r.SetParam("CSD_TAGNO", s.Tag);
            r.SetParam("TRS_SALQT", s.SalQt);
            r.SetParam("TRS_SALQT", s.Cnt);
            if (s.CloseTag)
            {
                r.AddSearch("plus:PL251_W01_U02_PROC");
                r.SetParam("TAG_STAT", "N");
            }
            r.SetParam("CSD_NO", s.CaseNo);
            r.SetParam("CSD_PTNO_QTY", s.SalQt);
            r.SetParam("CSD_PTNO", ptno);
            r.SetParam("CSD_VENMN", s.Vndmn);
            // 원본은 서버 시각(getCurrentServerDate). 단말 시각으로 대신한다.
            r.SetParam("CSD_USE_YMDTMS", DateTime.Now.ToString("yyyyMMddHHmmss"));
            AuthService.AddSessionCommon(r);
            Exec(Pgm251, "fn_Save", r, null);
        }

        // ==================================================================
        public static int ToInt(string s)
        {
            if (s == null) return 0;
            s = s.Trim().Replace(",", "");
            if (s.Length == 0) return 0;
            try
            {
                return (int)double.Parse(s, System.Globalization.NumberStyles.Float,
                                         System.Globalization.CultureInfo.InvariantCulture);
            }
            catch { return 0; }
        }

        private static string Left(string s, int n) { s = (s == null) ? "" : s; return s.Length <= n ? s : s.Substring(0, n); }
        private static string Right(string s, int n) { s = (s == null) ? "" : s; return s.Length <= n ? s : s.Substring(s.Length - n); }
    }
}
