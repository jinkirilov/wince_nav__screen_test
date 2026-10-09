using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace HaimsPda.Ui
{
    /// <summary>
    /// 로그인 화면 이미지 로더.
    /// 1순위: 임베디드 리소스 (빌드 작업을 '포함 리소스'로 지정)
    /// 2순위: 실행 파일 폴더의 images\ 하위 파일
    /// 둘 다 없으면 null 을 돌려주고 호출부가 알아서 건너뛴다.
    /// </summary>
    public static class Res
    {
        public const string Bg = "lo_bg.png";
        public const string Logo = "lo_img_logo.png";
        public const string Lock = "lo_img_lock.png";
        public const string Car = "lo_img_car.png";

        private static readonly Hashtable _cache = new Hashtable();

        public static Bitmap Get(string name)
        {
            if (_cache.Contains(name)) return (Bitmap)_cache[name];

            Bitmap bmp = LoadEmbedded(name);
            if (bmp == null) bmp = LoadFile(name);

            _cache[name] = bmp;
            return bmp;
        }

        private static Bitmap LoadEmbedded(string name)
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string[] all = asm.GetManifestResourceNames();
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i].ToLower().EndsWith(name.ToLower()))
                    {
                        using (Stream s = asm.GetManifestResourceStream(all[i]))
                            return new Bitmap(s);
                    }
                }
            }
            catch { }
            return null;
        }

        private static Bitmap LoadFile(string name)
        {
            try
            {
                // CF 에서는 GetName().CodeBase 가 null 이라 모듈 경로를 쓴다
                string exe = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                string dir = Path.GetDirectoryName(exe);
                string p = Path.Combine(Path.Combine(dir, "images"), name);
                if (File.Exists(p)) return new Bitmap(p);
                p = Path.Combine(dir, name);
                if (File.Exists(p)) return new Bitmap(p);
            }
            catch { }
            return null;
        }

        public static void Clear()
        {
            foreach (DictionaryEntry e in _cache)
                if (e.Value != null) ((Bitmap)e.Value).Dispose();
            _cache.Clear();
        }
    }
}
