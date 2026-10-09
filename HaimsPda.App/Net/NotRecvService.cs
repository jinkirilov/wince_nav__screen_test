using System;
using System.Collections;

namespace HaimsPda.Net
{
    /// <summary>
    /// [132] 미수령등록 결과. 원본이 ds_LinkInfo 에 실어 돌려주던 값.
    ///   CAL_QT      -> InQty    입고수량 (= 입고대상수량 - 미수령수량)
    ///   INPUT_QT    -> NarQty   미수령수량  (커밋의 CTLQT)
    ///   REASON_CODE -> ReasonCd 미수령사유  (커밋의 CTLCD, 공통코드 MP/41)
    /// </summary>
    public sealed class NotRecvResult
    {
        public int NarQty;
        public int InQty;
        public string ReasonCd = "";
        public string ReasonNm = "";

        /// <summary>원본 GV_Control : 수량과 사유가 모두 있어야 미수령으로 본다.</summary>
        public bool IsSet { get { return NarQty > 0 && ReasonCd.Length > 0; } }
    }

    /// <summary>
    /// [132] 미수령등록 서버 호출. 원본 : /ui/ws/plus/PL132_W01.xml (메뉴 P119)
    ///
    /// 화면 자체는 서버에 쓰지 않는다. 사유 콤보만 조회하고,
    /// 실제 기록은 호출한 화면의 커밋에 PL100_W01_I05(미수령내역) + I06 이 붙어 이뤄진다.
    /// </summary>
    public static class NotRecvService
    {
        public const string Pgm = "PL132_W01.xml";

        private const string ReasonSql =
            "AND ((CDM_LRG_GRP = 'MP' AND CDM_MID_GRP = '41' AND CDM_AGTCD_M IN ('COMM')))";

        /// <summary>미수령 사유 콤보 : gfn_SearchComCode([["MP","41","ds_MP41","MP41"]])</summary>
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

            if (res.IsError) throw new HaimsException(res.ErrorMsg);
            return list;
        }
    }
}
