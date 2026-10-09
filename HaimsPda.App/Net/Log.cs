using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HaimsPda.Net
{
    public enum LogLevel
    {
        Off = 0,
        Summary = 1,   // URL / 소요시간 / ErrorCode / 레코드 건수
        Full = 2       // 요청·응답 본문까지 (개발 중에만)
    }

    /// <summary>
    /// 송수신 로그. System.Diagnostics.Debug 로 내보내므로
    /// VS2008 [출력] 창(디버그)에 그대로 표시된다.
    ///
    /// [주의]
    ///  - Debug.WriteLine 은 DEBUG 심볼이 있을 때만 컴파일된다(Release 에서는 사라진다).
    ///  - 장비에서 디버그 채널은 느리다. Full 은 개발 중에만 쓰고,
    ///    현장 배포본은 Summary 이하로 둔다.
    ///  - 본문은 BodyCap 까지만 찍는다. 메뉴/공통코드 응답은 수백 KB 가 될 수 있다.
    /// </summary>
    public static class Log
    {
        public static LogLevel Level = LogLevel.Full;

        /// <summary>본문 로그 최대 길이(문자). 넘으면 잘라서 찍는다.</summary>
        public static int BodyCap = 65536;   // 8192 -> 개발 중 상향

        /// <summary>한 줄 최대 길이. 디버그 채널이 긴 줄을 자르는 장비가 있어 쪼갠다.</summary>
        public static int LineCap = 512;

        public static bool IsFull { get { return Level >= LogLevel.Full; } }
        public static bool IsOn { get { return Level > LogLevel.Off; } }

        public static void Write(string text)
        {
            if (!IsOn || text == null) return;
            Emit(text);
        }

        /// <summary>본문 등 긴 문자열. LineCap 단위로 쪼개서 찍는다.</summary>
        public static void WriteBody(string tag, string body)
        {
            if (!IsFull) return;
            if (body == null) body = "";

            bool cut = body.Length > BodyCap;
            string s = cut ? body.Substring(0, BodyCap) : body;

            Emit("--- " + tag + " (" + body.Length + " chars"
                 + (cut ? ", " + BodyCap + " 까지 표시" : "") + ") ---");

            int i = 0;
            while (i < s.Length)
            {
                int n = Math.Min(LineCap, s.Length - i);
                Emit(s.Substring(i, n));
                i += n;
            }
            Emit("--- " + tag + " end ---");
        }

        private static void Emit(string line)
        {
            try { Debug.WriteLine(line); }
            catch { }
        }
    }

    /// <summary>
    /// 파서가 읽어가는 문자를 그대로 엿보며 버퍼에 모은다.
    ///
    /// 응답을 한 번 더 읽거나 통째로 string 으로 만들지 않는다.
    /// 버퍼는 cap 까지만 채우므로 대용량 응답에서도 메모리가 늘지 않는다.
    /// </summary>
    internal sealed class TeeTextReader : TextReader
    {
        private readonly TextReader _inner;
        private readonly StringBuilder _sb;
        private readonly int _cap;

        public TeeTextReader(TextReader inner, StringBuilder sb, int cap)
        {
            _inner = inner;
            _sb = sb;
            _cap = cap;
        }

        public override int Peek() { return _inner.Peek(); }

        public override int Read()
        {
            int c = _inner.Read();
            if (c >= 0 && _sb.Length < _cap) _sb.Append((char)c);
            return c;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            int n = _inner.Read(buffer, index, count);
            if (n > 0 && _sb.Length < _cap)
            {
                int take = n;
                if (_sb.Length + take > _cap) take = _cap - _sb.Length;
                if (take > 0) _sb.Append(buffer, index, take);
            }
            return n;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _inner.Dispose();
            base.Dispose(disposing);
        }
    }
}
