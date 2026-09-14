namespace MobisHaims.Nav
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
        public const int ReserveList = 131;        // 예약내역          (PL131_W01)
        public const int InboundSave = 140;        // 입고저장          (PL140_W01)
        public const int SortInboundSave = 141;    // 정렬입고저장      (PL141_W01, 원본 미확보)
        public const int DirectInboundSave = 142;  // 직입고저장        (PL142_W01)

        // 출고관리 [200]
        public const int OutboundMenu = 200;
        public const int PickTargetAgency = 201;   // 피킹대상조회(대리점)
        public const int PickTargetShop = 203;     // 전문점 피킹대상 조회
        public const int PickTargetRoute = 204;    // 전문점 노선/지역 피킹대상조회
        public const int PickResult = 210;         // 피킹실적처리
        public const int ReturnProc = 211;         // 반품처리
        public const int PickBySlip = 220;         // 피킹(판매증표출고)
        public const int SlipSelect = 221;         // 판매증표선택
        public const int ListPick = 230;           // 리스트피킹처리
        public const int LocPickTag = 240;         // 배송분류(TAG)/LOC피킹
        public const int PickClassifyTag = 250;    // 피킹분류(TAG)
        public const int SlipClassifyCheck = 260;  // 판매증표분류확인(배송분류)

        // 재고관리 [300]
        public const int StockMenu = 300;
        public const int LocInventory = 301;       // 재물조사(LOC)
        public const int Inventory = 302;          // 재물조사
        public const int InventoryTarget = 310;    // 재물조사대상조회
        public const int StockByLoc = 320;         // LOC별재고조회
        public const int StockByPart = 321;        // 파트별재고조회
        public const int StockDetail = 322;        // 재고세부내역
        public const int PartMoveHist = 323;       // 부품수불이력조회
        public const int PartInfo = 324;           // 부품정보조회
        public const int StockAdjust = 330;        // 재고조정 (서버 메뉴 P190 기준)

        // LOC관리 [400]
        public const int LocMenu = 400;
        public const int LocMove = 401;            // LOC재고이동
        public const int LocRegister = 410;        // LOC등록
        public const int PartLocSort = 420;        // 부품LOC정렬
        public const int WhTransfer = 430;         // 창고이전(출고/입고)
        public const int WhMigrate = 440;          // 창고이관(출고/입고)

        // 배송관리 [500]
        public const int DeliveryMenu = 500;        // 배송메뉴

        // 조회관리 [600]
        public const int InquiryMenu = 600;
        public const int CaseInquiry = 603;        // CASE내역조회
        public const int TagInquiry = 604;         // TAG내역조회
    }
}
