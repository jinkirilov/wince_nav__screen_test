using System;
using System.Windows.Forms;

namespace MobisHaims.Devices
{
    /// <summary>
    /// Symbol / Motorola / Zebra 계열 하드웨어 트리거 스캐너.
    ///
    /// [빌드]
    ///  이 파일은 SDK 가 없어도 컴파일된다. 실제 코드는 SYMBOL_SDK 조건부다.
    ///  SDK 를 붙일 때:
    ///   1) 프로젝트에 Symbol.Barcode.dll (그리고 필요 시 Symbol.dll) 참조 추가
    ///   2) 프로젝트 속성 > 빌드 > 조건부 컴파일 기호에 SYMBOL_SDK 추가
    ///  두 가지를 하지 않으면 Open() 이 false 를 돌려주고 팩토리가 웨지로 폴백한다.
    ///
    /// [SDK 호출 흐름 - 참고]
    ///   reader = new Symbol.Barcode.Reader();
    ///   readerData = new Symbol.Barcode.ReaderData(
    ///                    ReaderDataTypes.Text, ReaderDataLengths.MaximumLabel);
    ///   reader.Actions.Enable();
    ///   reader.ReadNotify += OnReadNotify;
    ///   reader.Actions.Read(readerData);        // 1회 읽기 요청 (읽을 때마다 재요청)
    ///   // OnReadNotify 에서 reader.GetNextReaderData() 로 결과를 꺼내고 다시 Read()
    /// </summary>
    public sealed class SymbolScanner : IScanner
    {
        private readonly Control _uiOwner;   // 워커 스레드 -> UI 스레드 마샬링용
        private bool _open;
        private bool _enabled = true;

        public SymbolScanner(Control uiOwner)
        {
            _uiOwner = uiOwner;
        }

        public string Name { get { return "Symbol/Zebra"; } }
        public bool IsOpen { get { return _open; } }
        public event ScanEventHandler Scanned;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
#if SYMBOL_SDK
                try
                {
                    if (_reader == null) return;
                    if (value) _reader.Actions.Enable();
                    else _reader.Actions.Disable();
                }
                catch { }
#endif
            }
        }

        private delegate void RaiseCall(string text, string symbology);

        /// <summary>워커 스레드에서 와도 UI 스레드로 넘겨 이벤트를 올린다.</summary>
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
                catch { return; }   // 폼이 이미 닫힘
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

#if SYMBOL_SDK
        private Symbol.Barcode.Reader _reader;
        private Symbol.Barcode.ReaderData _readerData;

        public bool Open()
        {
            if (_open) return true;
            try
            {
                _reader = new Symbol.Barcode.Reader();
                _reader.Actions.Enable();
                _reader.ReadNotify += new EventHandler(OnReadNotify);
                StartRead();
                _open = true;
                return true;
            }
            catch
            {
                Close();
                return false;
            }
        }

        private void StartRead()
        {
            _readerData = new Symbol.Barcode.ReaderData(
                Symbol.Barcode.ReaderDataTypes.Text,
                Symbol.Barcode.ReaderDataLengths.MaximumLabel);
            _reader.Actions.Read(_readerData);
        }

        private void OnReadNotify(object sender, EventArgs e)
        {
            try
            {
                Symbol.Barcode.ReaderData rd = _reader.GetNextReaderData();
                if (rd != null && rd.Result == Symbol.Results.SUCCESS)
                {
                    if (_enabled)
                        Raise(rd.Text, rd.Type.ToString());
                }
                StartRead();   // 다음 스캔 대기
            }
            catch { }
        }

        public void Close()
        {
            try
            {
                if (_reader != null)
                {
                    _reader.ReadNotify -= new EventHandler(OnReadNotify);
                    _reader.Actions.Flush();
                    _reader.Actions.Disable();
                    _reader.Dispose();
                }
            }
            catch { }
            finally { _reader = null; _readerData = null; _open = false; }
        }
#else
        /// <summary>SDK 미참조 빌드. 팩토리가 이 false 를 보고 웨지로 폴백한다.</summary>
        public bool Open() { return false; }

        public void Close() { _open = false; }
#endif

        public void Dispose() { Close(); }
    }
}
