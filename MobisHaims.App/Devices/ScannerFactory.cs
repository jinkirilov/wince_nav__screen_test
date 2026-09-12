using System;
using System.Windows.Forms;
using MobisHaims.Core;

namespace MobisHaims.Devices
{
    /// <summary>
    /// 장비에 맞는 IScanner 를 만들어 준다.
    ///
    /// [선택 순서]
    ///  1) DeviceInfo.Vendor 로 하드웨어 드라이버를 먼저 시도한다.
    ///  2) Open() 이 실패하면(SDK 미참조/미지원 모델) 키보드 웨지로 폴백한다.
    ///  3) 에뮬레이터에서는 웨지를 쓴다(호스트 키보드로 타이핑 가능).
    ///
    /// 화면 코드는 이 팩토리 결과의 Scanned 만 구독하면 되고,
    /// 어떤 경로로 값이 들어왔는지 알 필요가 없다.
    /// </summary>
    public static class ScannerFactory
    {
        /// <summary>
        /// owner 는 이벤트 마샬링과 웨지 키 후킹에 쓰인다.
        /// 셸(ShellForm)처럼 앱 수명과 같은 폼을 넘긴다.
        /// </summary>
        public static IScanner Create(Form owner)
        {
            IScanner hw = CreateHardware(owner);
            if (hw != null)
            {
                if (hw.Open()) return hw;
                try { hw.Dispose(); } catch { }
            }

            WedgeScanner wedge = new WedgeScanner(owner);
            wedge.Open();
            return wedge;
        }

        /// <summary>벤더별 하드웨어 드라이버. 해당 없으면 null.</summary>
        private static IScanner CreateHardware(Form owner)
        {
            switch (DeviceInfo.Vendor)
            {
                case DeviceVendor.Symbol:
                case DeviceVendor.Zebra:
                    return new SymbolScanner(owner);

                case DeviceVendor.M3:
                    return new M3Scanner(owner);

                case DeviceVendor.Daishin:
                    // 대신정보통신 SDK 확인 전. 웨지로 폴백한다.
                    return null;

                case DeviceVendor.Emulator:
                default:
                    return null;
            }
        }

        /// <summary>진단용. 실제로 어떤 구현이 잡혔는지 확인할 때.</summary>
        public static string Describe(IScanner s)
        {
            if (s == null) return "Scanner   : (none)";
            return "Scanner   : " + s.Name + (s.IsOpen ? " (open)" : " (closed)");
        }
    }
}
