========================================================
 HAIMS PLUS PDA - 스타터 솔루션 (VS2008 / WM6.5 / .NET CF 3.5)
========================================================

[열기/빌드]
1) Visual Studio 2008 + Windows Mobile 6 Professional SDK + .NET CF 3.5 설치 확인
2) MobisHaims.sln 열기
3) 대상: "Windows Mobile 6 Professional SDK (ARMV4I)" 에뮬레이터 또는 실기기
4) F5(배포/실행). 기본은 MockMode=true 이므로 데몬 없이 화면 동작 확인 가능

[아키텍처]
- ShellForm (단일 Form)
    Header(Top) : ☰ 햄버거 / [화면번호]화면명 / X
    Content(Fill): 업무화면(UserControl)을 스왑 → Form 생성/파괴 비용 회피
    Footer(Bottom): JUMP / 상태점 / 처리결과·작업자(소속)
- 업무화면은 Form이 아니라 ScreenBase(UserControl) 상속
- 화면전환은 NavigationManager(Stack) + ScreenRegistry(화면번호→생성자)
    · Navigate(id, args) / GoBack() / JUMP(번호 직접이동)
    · 뒤로가기 시 화면 Dispose() → WM 메모리 회수

[폴더]
  Ui/       Theme.cs        색상·폰트 중앙정의
  Core/     AppTypes.cs     MsgLevel / SessionContext / IShellContext
  Nav/      ScreenId.cs     전체 화면번호 상수
            Navigation.cs   NavArgs/ScreenBase/Registry/NavigationManager
  Data/     Json.cs         CF3.5용 경량 JSON(직렬화/파싱)
            IfClient.cs     데몬 /if 통신(header+item+tdata, Mock/HTTP)
  Controls/ HeaderControl.cs, FooterControl.cs(+JumpForm)
  Screens/  S000_MainMenu / S100_InboundMenu / S120_SiteInboundClassify

[구현된 화면]
  [000] 메인메뉴        - 5개 관리메뉴 진입
  [100] 입고메뉴        - 6개 버튼(사업소입고분류→[120] 연결)
  [120] 사업소입고분류  - 부번 스캔→IF_INB_PART 조회→수량입력→IF_INB_CLASSIFY_SAVE 저장
  * 미등록 화면 버튼/JUMP는 "미등록 화면" 안내로 graceful 처리

[데몬 연동 방법]
  1) Core/AppTypes.cs 의 SessionContext
       ServerUrl = "http://<데몬IP>:8080"
       MockMode  = false   ← 실제 /if 호출로 전환
  2) 통신 규격: POST {ServerUrl}/if , body =
       { "header": { ifId, ifVer, ifSenderGrp, ifSender, ifReceiverGrp,
                     ifReceiver, ifTrackingId, ifDateTime, ifResult, ifFailMsg },
         "item": { ... }, "tdata": [ { ... } ] }
  3) 성공 판정: header.ifResult == "S"
  4) 신규 전문(ifId) 추가 시 화면에서 Shell.If.Send(ifId, item, tdata) 호출만 하면 됨
     (Mock 검증이 필요하면 Data/IfClient.cs MockIfTransport 의 switch에 case 추가)

[신규 화면 추가 절차]
  1) Nav/ScreenId.cs 에 화면번호 상수 추가
  2) Screens/Sxxx_Name.cs 작성 (ScreenBase 상속, ScreenNo/ScreenName override)
  3) ShellForm.RegisterScreens() 에 _registry.Register(id, () => new Sxxx()) 추가
  → 메뉴 버튼 / JUMP 자동 동작

[스캐너(Zebra/Symbol) 연동 지점]
  S120 의 txtPart KeyDown(Enter) 자리에서 LookupPart() 호출.
  실제 기기에서는 Symbol.Barcode(리더 API) 또는 키보드웨지(스캔+Enter) 방식으로
  txtPart 에 값 주입 후 Enter 이벤트로 동일 로직 재사용.

[주의(.NET CF 3.5 제약 반영)]
  - TableLayout/FlowLayoutPanel 미지원 → 수동 좌표 배치(OnResize)
  - JSON 내장 직렬화기 없음 → Json.cs 자체 구현 사용
  - Button은 CF에서 BackColor/ForeColor 반영됨(색상 버튼 OK)
  - 화면 레이아웃 좌표는 QVGA 기준 근사값. 실기기 해상도에 맞게 미세조정 필요.
