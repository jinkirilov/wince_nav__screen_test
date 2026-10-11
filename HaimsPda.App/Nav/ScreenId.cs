namespace HaimsPda.Nav
{
    // 요구사항의 화면ID 체계. JUMP 입력값(정수)과 1:1 매핑된다.
    public static class ScreenId
    {
        public const int Main = 0;                 // [000] 메인메뉴

        // 입고관리 [100]
        // 번호는 원본 웹화면 PL***_W01.xml 과 1:1 로 맞춘다(HAR 확인).
        public const int InboundMenu = 100;        // 입고메뉴
        public const int SiteInboundClassify = 120;// 일반입고분류      (PL120_W01)
        public const int ShopInboundClassify = 121;// 전문점입고분류    (PL121_W01)
        public const int WaitPartInquiry = 130;    // 대기품목조회      (PL130_W01)
        public const int WaitAllocSelect = 1301;   // 입고대기품목 선택 (PL130_P01, 메뉴 P118) — 모달 팝업, 120/121 에서 사용
        public const int ReserveList = 131;        // 예약내역          (PL131_W01)
        public const int NotRecv = 132;            // 미수령등록        (PL132_W01, 메뉴 P119) — 모달 팝업, 140/120/121 에서 사용
        public const int InboundSave = 140;        // 입고저장          (PL140_W01)
        public const int AllocSelect = 1401;       // 할당내역 선택     (PL140_P01, 메뉴 P179) — 모달 팝업, 셸 등록 없음
        public const int SortInboundSave = 141;    // 정렬입고저장      (PL141_W01, 메뉴 1A07)
        public const int DirectInboundSave = 142;  // 직입고저장        (PL142_W01)

        // 출고관리 [200]
        // 번호는 원본 PL2**_W01.xml 과 1:1 (서버 메뉴 1B00 / P12x·P18x 기준).
        // 메뉴 버튼은 201/203/204/220/221/250/251/261 8개. 나머지는 하위 화면이다.
        public const int OutboundMenu = 200;
        public const int PickTarget = 201;         // 피킹대상/처리          (PL201_W01, P126)
        public const int PickDetail = 202;         // 피킹세부내역           (PL202_W01, P171) — 201 하위
        public const int PickTargetShop = 203;     // 피킹대상조회(전문점)   (PL203_W01, P182) — 전문점 전용
        public const int PickTargetShop2 = 204;    // 피킹대상(전문점)       (PL204_W01, P183) — 전문점 전용
        public const int Picking = 210;            // 피킹실적처리           (PL210_W01, P121) — 201 하위
        public const int PickReturn = 211;         // 반품(OS&D 피킹)        (PL211_W01, P172) — 210/280 하위
        public const int SlipPicking = 220;        // 피킹(판매증표출고)     (PL220_W01, P122)
        public const int SlipSelect = 221;         // 판매증표선택           (PL221_W01, P184)
        public const int ShopPicking = 230;        // 피킹실적처리(전문점)   (PL230_W01, P123)
        public const int LocPicking = 240;         // LOC별 피킹실적처리     (PL240_W01, P124)
        public const int TagClassify = 250;        // 피킹분류(TAG)          (PL250_W01, P125) — 전문점 전용
        public const int CaseClassify = 251;       // 배송분류(CASE)         (PL251_W01, P198)
        public const int SlipClassify = 260;       // 배송분류(판매증표)     (PL260_W01, P185) — 261 하위
        public const int SlipClassifySelect = 261; // 피킹분류(증표)         (PL261_W01, P186)
        public const int SlipClassifyDetail = 262; // 증표분류세부           (PL262_W01, P191) — 260 하위
        public const int ListPicking = 280;        // 리스트피킹             (PL280_W01, P147) — 201 하위
        public const int ShopListPicking = 281;    // 리스트피킹(전문점)     (PL281_W01, P187)

        // 재고관리 [300]
        public const int StockMenu = 300;
        public const int LocInventory = 301;       // 재물조사(LOC)
        public const int Inventory = 302;          // 재물조사
        public const int InventoryTarget = 310;    // 재물조사대상조회
        public const int StockByLoc = 320;         // LOC별재고조회
        public const int StockByPart = 321;        // 파트별재고조회
        public const int OsdControl = 322;         // 통제등록(OS&D)     (원본 PL212_W01, 메뉴 P138)
        public const int StockDetail = 3201;       // 재고세부내역      (원본 PL320_P01 팝업, 322에서 이동)
        public const int PartMoveHist = 323;       // 부품수불이력조회
        public const int PartInfo = 324;           // 부품정보조회
        public const int StockAdjust = 330;        // 재고조정 (서버 메뉴 P190 기준)

        // LOC관리 [400]
        public const int LocMenu = 400;
        // 번호는 원본 PL4**_W01.xml 과 1:1 (HAR 2026-10-05 10:44 확인, 서버 메뉴 1D00/P140)
        public const int LocMove = 401;            // LOC재고이동           (PL401_W01, P145)
        public const int LocRegister = 410;        // LOC등록               (PL410_W01, P141)
        public const int PartLocSort = 420;        // 부품LOC정렬           (PL420_W01, P142)
        public const int WhTransferOut = 430;      // 실시간창고이전(출고) (PL430_W01, P143)
        public const int WhTransferIn = 431;       // 실시간창고이전(입고) (PL431_W01, P144)
        public const int WhMigrateOut = 440;       // 실시간창고이관(출고) (PL440_W01, P180)
        public const int WhMigrateIn = 441;        // 실시간창고이관(입고) (PL441_W01, P181)

        // 배송관리 [500]
        public const int DeliveryMenu = 500;        // 배송메뉴

        // 조회관리 [600]
        public const int InquiryMenu = 600;
        public const int CaseInquiry = 603;        // CASE내역조회
        public const int TagInquiry = 604;         // TAG내역조회

        // 팔레트관리 [800]
        public const int PalletMenu = 800;    
    }
}
