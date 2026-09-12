using System;

namespace MobisHaims.Devices
{
    /// <summary>스캔 1건.</summary>
    public sealed class ScanData
    {
        public readonly string Text;        // 디코딩된 바코드 문자열
        public readonly string Symbology;   // 심볼로지 이름 (모르면 "")
        public readonly DateTime At;

        public ScanData(string text, string symbology)
        {
            Text = (text == null) ? "" : text;
            Symbology = (symbology == null) ? "" : symbology;
            At = DateTime.Now;
        }
    }

    public delegate void ScanEventHandler(object sender, ScanData data);

    /// <summary>
    /// 바코드 스캐너 공통 계약.
    ///
    /// 구현체는 두 부류다.
    ///  - 하드웨어 트리거형 : 벤더 SDK 로 트리거 이벤트를 직접 받는다(SymbolScanner 등).
    ///                       포커스와 무관하게 Scanned 가 올라온다.
    ///  - 키보드 웨지형     : 스캔값이 키 입력으로 들어온다(WedgeScanner).
    ///                       SDK 없이 동작하므로 기본값이자 최후 수단.
    ///
    /// 화면 코드는 어느 쪽인지 몰라도 되도록 Scanned 이벤트만 본다.
    /// </summary>
    public interface IScanner : IDisposable
    {
        /// <summary>구현체 이름(진단/로그용).</summary>
        string Name { get; }

        /// <summary>Open() 이 성공해 스캔을 받을 수 있는 상태인지.</summary>
        bool IsOpen { get; }

        /// <summary>스캔값 수신. 항상 UI 스레드에서 올라온다.</summary>
        event ScanEventHandler Scanned;

        /// <summary>장치를 연다. 실패해도 예외를 던지지 않고 false 를 돌려준다.</summary>
        bool Open();

        /// <summary>장치를 닫는다. 여러 번 불러도 안전하다.</summary>
        void Close();

        /// <summary>
        /// 수신 일시중지/재개. 팝업이 떠 있는 동안 등에 쓴다.
        /// Close() 와 달리 장치를 놓지 않는다.
        /// </summary>
        bool Enabled { get; set; }
    }
}
