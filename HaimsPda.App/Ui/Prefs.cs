using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace HaimsPda.Ui
{
    /// <summary>
    /// Login.xml 의 localStorage(USERID_SAVE_YN / USR_USRID_AUTO) 대체.
    /// 실행 파일과 같은 폴더에 prefs.txt 로 저장한다.
    /// </summary>
    public static class Prefs
    {
        public const string KeySaveYn = "USERID_SAVE_YN";
        public const string KeyUserId = "USR_USRID_AUTO";

        // 호스트 설정 화면(HostForm)에서 쓴다
        public const string KeyHostUrl = "HOST_URL";
        public const string KeyHostTimeout = "HOST_TIMEOUT_SEC";

        private static Hashtable _map;
        private static string _path;

        private static string Path_
        {
            get
            {
                if (_path == null)
                {
                    // CF 에서는 GetName().CodeBase 가 null 이라 모듈 경로를 쓴다
                    string exe = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                    string dir = System.IO.Path.GetDirectoryName(exe);
                    _path = System.IO.Path.Combine(dir, "prefs.txt");
                }
                return _path;
            }
        }

        private static void Load()
        {
            if (_map != null) return;
            _map = new Hashtable();
            try
            {
                if (!File.Exists(Path_)) return;
                using (StreamReader sr = new StreamReader(Path_, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        int p = line.IndexOf('=');
                        if (p <= 0) continue;
                        _map[line.Substring(0, p)] = line.Substring(p + 1);
                    }
                }
            }
            catch { }
        }

        public static string Get(string key)
        {
            Load();
            object v = _map[key];
            return (v == null) ? "" : (string)v;
        }

        public static void Set(string key, string value)
        {
            Load();
            _map[key] = (value == null) ? "" : value;
            Save();
        }

        private static void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path_, false, Encoding.UTF8))
                    foreach (DictionaryEntry e in _map)
                        sw.WriteLine(e.Key + "=" + e.Value);
            }
            catch { }
        }
    }
}
