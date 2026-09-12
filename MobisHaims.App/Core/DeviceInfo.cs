using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MobisHaims.Core
{
    /// <summary>
    /// 장비 제조사 추정값. 벤더 SDK(스캐너/IME) 분기용.
    /// OemInfo 문자열로 판정하므로 확정이 아니라 힌트다.
    ///
    /// 도입 예정 장비: M3 Mobile / Symbol / Zebra / 대신정보통신
    /// 개발 단계     : Emulator
    /// </summary>
    public enum DeviceVendor
    {
        Unknown = 0,
        Emulator,
        M3,          // M3 Mobile (구 MobileCompia)
        Symbol,      // Symbol / Motorola Solutions
        Zebra,       // Zebra (Symbol 계열 승계 - SDK 동일 계보)
        Daishin      // 대신정보통신
    }

    /// <summary>
    /// Windows CE / Windows Mobile 장비 정보 조회.
    ///
    /// 전부 coredll.dll 호출이라 장비 이미지에 따라 일부가 없을 수 있다.
    /// 모든 조회는 실패 시 빈 문자열/0 을 돌려주고 예외를 던지지 않는다.
    /// 값은 1회 조회 후 캐시한다(변하지 않는 값들).
    /// </summary>
    public static class DeviceInfo
    {
        #region native

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool SystemParametersInfo(
            uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

        // SystemParametersInfo 조회 코드 (winuser.h / pwinuser.h)
        private const uint SPI_GETPLATFORMTYPE = 257;   // "PocketPC", "Smartphone", "Palm PC" 등
        private const uint SPI_GETOEMINFO = 258;        // 제조사/모델 문자열
        private const uint SPI_GETPLATFORMNAME = 260;   // CE 5.0+ (미지원 장비 있음)
        private const uint SPI_GETPLATFORMMANUFACTURER = 262; // CE 5.0+ (미지원 장비 있음)

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }

        [DllImport("coredll.dll")]
        private static extern void GetSystemInfo(out SYSTEM_INFO si);

        [StructLayout(LayoutKind.Sequential)]
        private struct MEMORYSTATUS
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }

        [DllImport("coredll.dll")]
        private static extern void GlobalMemoryStatus(ref MEMORYSTATUS ms);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool KernelIoControl(
            uint dwIoControlCode, byte[] lpInBuf, int nInBufSize,
            byte[] lpOutBuf, int nOutBufSize, out int lpBytesReturned);

        private const uint IOCTL_HAL_GET_DEVICEID = 0x01010048;

        #endregion

        #region SystemParametersInfo 문자열 조회

        /// <summary>SPI 문자열 조회. 미지원 코드면 빈 문자열.</summary>
        private static string Spi(uint code)
        {
            try
            {
                StringBuilder sb = new StringBuilder(256);
                if (!SystemParametersInfo(code, (uint)sb.Capacity, sb, 0)) return "";
                return sb.ToString().Trim();
            }
            catch (MissingMethodException) { return ""; }
            catch (EntryPointNotFoundException) { return ""; }
            catch { return ""; }
        }

        private static string _oemInfo;
        /// <summary>제조사/모델 문자열. 예: "Bluebird BIP-6000", "Microsoft DeviceEmulator"</summary>
        public static string OemInfo
        {
            get
            {
                if (_oemInfo == null) _oemInfo = Spi(SPI_GETOEMINFO);
                return _oemInfo;
            }
        }

        private static string _platformType;
        /// <summary>플랫폼 종류. 예: "PocketPC", "Smartphone"</summary>
        public static string PlatformType
        {
            get
            {
                if (_platformType == null) _platformType = Spi(SPI_GETPLATFORMTYPE);
                return _platformType;
            }
        }

        private static string _platformName;
        /// <summary>플랫폼 이름 (CE 5.0+, 미지원 장비는 빈 문자열)</summary>
        public static string PlatformName
        {
            get
            {
                if (_platformName == null) _platformName = Spi(SPI_GETPLATFORMNAME);
                return _platformName;
            }
        }

        private static string _platformManufacturer;
        /// <summary>플랫폼 제조사 (CE 5.0+, 미지원 장비는 빈 문자열)</summary>
        public static string PlatformManufacturer
        {
            get
            {
                if (_platformManufacturer == null)
                    _platformManufacturer = Spi(SPI_GETPLATFORMMANUFACTURER);
                return _platformManufacturer;
            }
        }

        #endregion

        #region OS / CPU / 메모리

        /// <summary>OS 버전. Windows Mobile 6.x = CE 5.2</summary>
        public static Version OsVersion
        {
            get
            {
                try { return Environment.OSVersion.Version; }
                catch { return new Version(0, 0); }
            }
        }

        public static string CpuArchitecture
        {
            get
            {
                try
                {
                    SYSTEM_INFO si;
                    GetSystemInfo(out si);
                    switch (si.wProcessorArchitecture)
                    {
                        case 0: return "x86";
                        case 1: return "MIPS";
                        case 2: return "Alpha";
                        case 3: return "PPC";
                        case 4: return "SHX";
                        case 5: return "ARM";
                        case 6: return "IA64";
                        default: return "Unknown(" + si.wProcessorArchitecture + ")";
                    }
                }
                catch { return ""; }
            }
        }

        /// <summary>물리 메모리 총량 (바이트). 실패 시 0.</summary>
        public static uint TotalPhysicalBytes { get { return Mem(true); } }

        /// <summary>가용 물리 메모리 (바이트). 실패 시 0.</summary>
        public static uint AvailPhysicalBytes { get { return Mem(false); } }

        private static uint Mem(bool total)
        {
            try
            {
                MEMORYSTATUS ms = new MEMORYSTATUS();
                ms.dwLength = (uint)Marshal.SizeOf(ms);
                GlobalMemoryStatus(ref ms);
                return total ? ms.dwTotalPhys : ms.dwAvailPhys;
            }
            catch { return 0; }
        }

        #endregion

        #region 장비 고유 ID (KernelIoControl)

        private static string _deviceId;

        /// <summary>
        /// HAL 장비 고유 ID 를 hex 문자열로. 서버 로그/단말 식별에 쓴다.
        /// DEVICE_ID 구조체는 헤더 20바이트 뒤에 PresetID / PlatformID 가 붙는다.
        /// 미지원 장비는 빈 문자열.
        /// </summary>
        public static string DeviceId
        {
            get
            {
                if (_deviceId != null) return _deviceId;
                _deviceId = ReadDeviceId();
                return _deviceId;
            }
        }

        private static string ReadDeviceId()
        {
            try
            {
                byte[] buf = new byte[256];
                int cb;
                if (!KernelIoControl(IOCTL_HAL_GET_DEVICEID, null, 0, buf, buf.Length, out cb))
                    return "";
                if (cb < 20) return "";

                int presetOfs = BitConverter.ToInt32(buf, 4);
                int presetLen = BitConverter.ToInt32(buf, 8);
                int platOfs = BitConverter.ToInt32(buf, 12);
                int platLen = BitConverter.ToInt32(buf, 16);

                StringBuilder sb = new StringBuilder(64);
                AppendHex(sb, buf, presetOfs, presetLen, cb);
                AppendHex(sb, buf, platOfs, platLen, cb);
                return sb.ToString();
            }
            catch (MissingMethodException) { return ""; }
            catch (EntryPointNotFoundException) { return ""; }
            catch { return ""; }
        }

        private static void AppendHex(StringBuilder sb, byte[] buf, int ofs, int len, int cb)
        {
            if (ofs < 0 || len <= 0 || ofs + len > cb) return;
            for (int i = 0; i < len; i++) sb.Append(buf[ofs + i].ToString("X2"));
        }

        #endregion

        #region 제조사 판정 / 진단 문자열

        private static bool _vendorRead;
        private static DeviceVendor _vendor;

        /// <summary>
        /// OemInfo 문자열로 제조사를 추정한다. 벤더 SDK 분기용 힌트.
        /// 확정 판정이 필요하면 해당 벤더 DLL 존재 여부까지 같이 확인할 것.
        /// </summary>
        public static DeviceVendor Vendor
        {
            get
            {
                if (_vendorRead) return _vendor;
                _vendorRead = true;
                _vendor = Detect(OemInfo + " " + PlatformManufacturer + " " + PlatformName);
                return _vendor;
            }
        }

        // 제조사별 OemInfo/Platform 문자열 패턴 (전부 소문자로 비교).
        // 실기에서 Describe() 로 실제 OEM 문자열을 확인한 뒤 여기에 추가한다.
        private static readonly string[] PatM3 = {
            "m3 mobile", "mobilecompia", "m3sky", "m3 sky", "m3 orange",
            "m3 green", "m3 gray", "m3 plus", "m3 pocket", "m3-"
        };
        private static readonly string[] PatSymbol = {
            "symbol", "motorola", "mc30", "mc31", "mc32", "mc55", "mc65",
            "mc70", "mc75", "mc90", "mc91", "mc92", "workabout"
        };
        private static readonly string[] PatZebra = {
            "zebra", "tc51", "tc55", "tc56", "tc70", "tc75", "mc33", "mc330"
        };
        private static readonly string[] PatDaishin = {
            "daishin", "dsic", "대신정보통신", "대신"
        };
        private static readonly string[] PatEmulator = {
            "emulator", "deviceemulator", "microsoft devicee"
        };

        private static DeviceVendor Detect(string s)
        {
            if (s == null) return DeviceVendor.Unknown;
            string t = s.ToLower();

            // 에뮬레이터를 먼저 걸러야 한다. 개발 단계에서 오탐을 막는다.
            if (Match(t, PatEmulator)) return DeviceVendor.Emulator;

            if (Match(t, PatM3)) return DeviceVendor.M3;
            if (Match(t, PatZebra)) return DeviceVendor.Zebra;
            if (Match(t, PatSymbol)) return DeviceVendor.Symbol;
            if (Match(t, PatDaishin)) return DeviceVendor.Daishin;

            return DeviceVendor.Unknown;
        }

        private static bool Match(string lowered, string[] pats)
        {
            for (int i = 0; i < pats.Length; i++)
                if (lowered.IndexOf(pats[i]) >= 0) return true;
            return false;
        }

        /// <summary>
        /// Symbol / Zebra 계열 여부. 두 브랜드는 SDK 계보가 같아
        /// 스캐너/IME 처리를 한 갈래로 묶는다.
        /// </summary>
        public static bool IsZebraFamily
        {
            get { return Vendor == DeviceVendor.Symbol || Vendor == DeviceVendor.Zebra; }
        }

        /// <summary>개발용 에뮬레이터에서 실행 중인지. 스캐너 목(mock) 분기에 쓴다.</summary>
        public static bool IsEmulator
        {
            get { return Vendor == DeviceVendor.Emulator; }
        }

        /// <summary>진단/로그용 한 덩어리 문자열.</summary>
        public static string Describe()
        {
            StringBuilder sb = new StringBuilder(256);
            sb.Append("OEM       : ").Append(OemInfo).Append("\r\n");
            sb.Append("Platform  : ").Append(PlatformType);
            if (PlatformName.Length > 0) sb.Append(" / ").Append(PlatformName);
            if (PlatformManufacturer.Length > 0) sb.Append(" / ").Append(PlatformManufacturer);
            sb.Append("\r\n");
            sb.Append("Vendor    : ").Append(Vendor.ToString()).Append("\r\n");
            sb.Append("OS        : CE ").Append(OsVersion.ToString()).Append("\r\n");
            sb.Append("CPU       : ").Append(CpuArchitecture).Append("\r\n");
            sb.Append("RAM       : ").Append(AvailPhysicalBytes / 1024 / 1024).Append(" / ")
              .Append(TotalPhysicalBytes / 1024 / 1024).Append(" MB\r\n");

            try
            {
                System.Drawing.Rectangle b = Screen.PrimaryScreen.Bounds;
                sb.Append("Screen    : ").Append(b.Width).Append("x").Append(b.Height).Append("\r\n");
            }
            catch { }

            if (DeviceId.Length > 0) sb.Append("DeviceId  : ").Append(DeviceId).Append("\r\n");
            return sb.ToString();
        }

        #endregion
    }
}
