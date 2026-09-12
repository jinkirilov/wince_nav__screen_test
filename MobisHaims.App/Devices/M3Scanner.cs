using System;
using System.Windows.Forms;

namespace MobisHaims.Devices
{
    /// <summary>
    /// M3 Mobile 하드웨어 트리거 스캐너 자리.
    ///
    /// M3 는 모델/이미지에 따라 스캔 라이브러리가 갈린다(구 MobileCompia 계열 포함).
    /// 실기에서 아래를 확인한 뒤 M3_SDK 블록을 채운다.
    ///   1) \Windows 또는 SDK 폴더의 스캔 관련 DLL 이름
    ///   2) 관리 코드 래퍼(.NET CF) 제공 여부. 없으면 네이티브 P/Invoke
    ///   3) 스캔 결과 전달 방식(이벤트 / 콜백 / 윈도우 메시지)
    ///
    /// 채우기 전까지 Open() 은 false 를 돌려주고, 팩토리가 웨지로 폴백한다.
    /// M3 도 웨지 모드 설정이 가능하므로 폴백만으로도 업무는 돌아간다.
    /// </summary>
    public sealed class M3Scanner : IScanner
    {
        private readonly Control _uiOwner;
        private bool _open;
        private bool _enabled = true;

        public M3Scanner(Control uiOwner) { _uiOwner = uiOwner; }

        public string Name { get { return "M3"; } }
        public bool IsOpen { get { return _open; } }
        public event ScanEventHandler Scanned;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

#if M3_SDK
        public bool Open()
        {
            // TODO: M3 스캔 라이브러리 초기화 + 콜백 등록 -> Raise() 호출
            return false;
        }

        public void Close() { _open = false; }
#else
        public bool Open() { return false; }
        public void Close() { _open = false; }
#endif

        private delegate void RaiseCall(string text, string symbology);

        private void Raise(string text, string symbology)
        {
            if (text == null || text.Length == 0) return;
            if (_uiOwner != null)
            {
                try
                {
                    if (_uiOwner.InvokeRequired)
                    {
                        _uiOwner.Invoke(new RaiseCall(RaiseCore), new object[] { text, symbology });
                        return;
                    }
                }
                catch { return; }
            }
            RaiseCore(text, symbology);
        }

        private void RaiseCore(string text, string symbology)
        {
            ScanEventHandler h = Scanned;
            if (h == null) return;
            try { h(this, new ScanData(text, symbology)); }
            catch { }
        }

        public void Dispose() { Close(); }
    }
}
