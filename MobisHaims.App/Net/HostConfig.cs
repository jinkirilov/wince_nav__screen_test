using System;
using HaimsPda.Ui;

namespace HaimsPda.Net
{
    /// <summary>
    /// 서버 주소 / 타임아웃 설정. prefs.txt 에 저장하고 HaimsHttp 에 반영한다.
    ///
    /// 장비마다 운영/개발 서버가 다르고 현장에서 바꿔야 하므로
    /// 소스 상수가 아니라 설정 파일로 뺀다. 값이 없으면 Default* 를 쓴다.
    /// </summary>
    public static class HostConfig
    {
        public const string DefaultUrl = "http://pda.haims.co.kr/Main";
        public const int DefaultTimeoutSec = 20;

        public const int MinTimeoutSec = 5;
        public const int MaxTimeoutSec = 120;

        /// <summary>현재 설정값(저장된 값 또는 기본값)</summary>
        public static string Url
        {
            get
            {
                string v = Prefs.Get(Prefs.KeyHostUrl).Trim();
                return (v.Length == 0) ? DefaultUrl : v;
            }
        }

        public static int TimeoutSec
        {
            get { return ParseSec(Prefs.Get(Prefs.KeyHostTimeout)); }
        }

        /// <summary>앱 시작 시 1회. 저장된 설정을 HaimsHttp 에 밀어 넣는다.</summary>
        public static void Load()
        {
            Apply(Url, TimeoutSec);
        }

        /// <summary>저장 없이 지금 통신에만 반영한다(접속확인용).</summary>
        public static void Apply(string url, int timeoutSec)
        {
            HaimsHttp.BaseUrl = url;
            HaimsHttp.TimeoutMs = Clamp(timeoutSec) * 1000;
        }

        public static void Save(string url, int timeoutSec)
        {
            Prefs.Set(Prefs.KeyHostUrl, url);
            Prefs.Set(Prefs.KeyHostTimeout, Clamp(timeoutSec).ToString());
            Apply(url, timeoutSec);
        }

        /// <summary>기본값으로 되돌린다(저장된 값을 지운다).</summary>
        public static void Reset()
        {
            Prefs.Set(Prefs.KeyHostUrl, "");
            Prefs.Set(Prefs.KeyHostTimeout, "");
            Apply(DefaultUrl, DefaultTimeoutSec);
        }

        /// <summary>
        /// 입력 보정. 현장에서 "pda.haims.co.kr" 만 치는 경우가 많다.
        /// - 스킴이 없으면 http:// 를 붙인다 (CF 3.5 는 TLS 1.2 미지원이라 https 는 쓸 수 없다)
        /// - 호스트만 있고 경로가 없으면 /Main 을 붙인다
        /// - 경로가 이미 있으면 건드리지 않는다
        /// </summary>
        public static string Normalize(string input)
        {
            string s = (input == null) ? "" : input.Trim();
            if (s.Length == 0) return DefaultUrl;

            if (s.IndexOf("://") < 0) s = "http://" + s;

            int p = s.IndexOf("://") + 3;
            int slash = s.IndexOf('/', p);

            if (slash < 0) return s + "/Main";              // 호스트만 입력
            if (slash == s.Length - 1) return s + "Main";   // 끝이 "/" 로만 끝남
            return s;
        }

        /// <summary>https 는 CF 3.5 에서 핸드셰이크가 실패한다. 저장 전에 경고용으로 쓴다.</summary>
        public static bool IsHttps(string url)
        {
            return url != null && url.Trim().ToLower().StartsWith("https://");
        }

        private static int ParseSec(string s)
        {
            if (s == null) return DefaultTimeoutSec;
            s = s.Trim();
            if (s.Length == 0) return DefaultTimeoutSec;
            try { return Clamp(int.Parse(s)); }
            catch { return DefaultTimeoutSec; }
        }

        private static int Clamp(int sec)
        {
            if (sec < MinTimeoutSec) return MinTimeoutSec;
            if (sec > MaxTimeoutSec) return MaxTimeoutSec;
            return sec;
        }
    }
}
