using System;

namespace MobisHaims.Devices
{
    /// <summary>
    /// 에뮬레이터/단위테스트용. 하드웨어 없이 Simulate() 로 스캔을 흘려넣는다.
    ///
    /// 개발 단계에서 스캔 흐름(조회 -> 수량 -> 저장)을 화면 없이 검증하거나,
    /// 에뮬레이터에서 버튼 하나로 고정 바코드를 쏘아볼 때 쓴다.
    /// </summary>
    public sealed class MockScanner : IScanner
    {
        private bool _open;
        private bool _enabled = true;

        public string Name { get { return "Mock"; } }
        public bool IsOpen { get { return _open; } }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public event ScanEventHandler Scanned;

        public bool Open() { _open = true; return true; }
        public void Close() { _open = false; }

        /// <summary>스캔 발생을 흉내낸다.</summary>
        public void Simulate(string text) { Simulate(text, "MOCK"); }

        public void Simulate(string text, string symbology)
        {
            if (!_open || !_enabled) return;
            ScanEventHandler h = Scanned;
            if (h == null) return;
            try { h(this, new ScanData(text, symbology)); }
            catch { }
        }

        public void Dispose() { Close(); }
    }
}
