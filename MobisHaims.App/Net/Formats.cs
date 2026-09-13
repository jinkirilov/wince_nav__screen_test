using System;

namespace HaimsPda.Net
{
    /// <summary>
    /// 부번 / LOC 표기 규칙.
    ///
    /// 원본 웹의 plus.js 공통함수를 그대로 옮긴 것이다.
    ///   PartNo.Display  <- gfn_DispPTNO_Plus
    ///   Loc.Display     <- gfn_DispLOC_Plus
    ///   Loc.IsValid     <- gfn_CheckLoc
    ///
    /// 원칙 : 화면과 리스트에는 Display() 로 구분자를 넣어 보여주고,
    ///        서버로 나가는 값은 반드시 Key() 로 구분자를 걷어낸다.
    /// </summary>
    public static class PartNo
    {
        /// <summary>
        /// 화면 표기용.
        ///   18자 절단 -> 16자리째가 숫자면 15자로 절단
        ///   -> 6번째 이후 첫 공백에서 절단 -> 대문자 -> "5자리 + 공백 + 나머지"
        /// 예) 8681125500 -> 86811 25500
        /// </summary>
        public static string Display(string v)
        {
            if (v == null) return "";
            v = v.Trim();
            if (v.Length > 18) v = v.Substring(0, 18);

            // 수량이 붙은 바코드 대응 : 16번째가 숫자면 뒤를 버린다
            if (v.Length >= 16 && v[15] >= '0' && v[15] <= '9')
                v = v.Substring(0, 15);

            if (v.Length > 6)
            {
                int p = v.IndexOf(' ', 6);
                if (p > -1) v = v.Substring(0, p);
            }

            v = v.ToUpper();

            if (v.Length > 5)
                v = v.Substring(0, 5) + " " + v.Substring(5).Replace(" ", "");

            return v;
        }

        /// <summary>서버 전송용. 공백 제거 + 대문자.</summary>
        public static string Key(string v)
        {
            if (v == null) return "";
            return v.Replace(" ", "").Trim().ToUpper();
        }
    }

    /// <summary>LOC 표기 규칙. 화면에는 하이픈을 넣고, 서버로는 빼고 보낸다.</summary>
    public static class Loc
    {
        /// <summary>
        /// 화면 표기용. 하이픈·공백을 먼저 걷어낸 뒤 길이로 나눈다.
        ///   10자리 : 3-2-2-2-1   A01-01-01-01-A
        ///    9자리 : 3-2-2-2     A01-01-01-01
        ///    8자리 : 2-2-2-2     02-02-02-01
        /// 그 외 길이는 대문자로만 바꿔 돌려준다.
        /// 이미 하이픈이 들어온 값을 다시 넣어도 결과는 같다.
        /// </summary>
        public static string Display(string v)
        {
            v = Key(v);

            if (v.Length == 10)
                return v.Substring(0, 3) + "-" + v.Substring(3, 2) + "-"
                     + v.Substring(5, 2) + "-" + v.Substring(7, 2) + "-" + v.Substring(9);

            if (v.Length == 9)
                return v.Substring(0, 3) + "-" + v.Substring(3, 2) + "-"
                     + v.Substring(5, 2) + "-" + v.Substring(7, 2);

            if (v.Length == 8)
                return v.Substring(0, 2) + "-" + v.Substring(2, 2) + "-"
                     + v.Substring(4, 2) + "-" + v.Substring(6, 2);

            return v;
        }

        /// <summary>서버 전송용. 하이픈·공백 제거 + 대문자.</summary>
        public static string Key(string v)
        {
            if (v == null) return "";
            return v.Replace("-", "").Replace(" ", "").Trim().ToUpper();
        }

        /// <summary>
        /// LOC 형식 검사 (gfn_CheckLoc).
        /// 하이픈을 뺀 뒤 앞 2자리 숫자, 3번째 영문, 4~9번째 숫자, 10번째 숫자.
        /// 빈 값과 "0000000000" 은 통과시킨다.
        /// </summary>
        public static bool IsValid(string v)
        {
            v = Key(v);

            if (v.Length == 0) return true;
            if (v == "0000000000") return true;
            if (v.Length < 10) return false;

            if (!IsDigits(v.Substring(0, 2))) return false;
            if (!IsLetter(v[2])) return false;
            if (!IsDigits(v.Substring(3, 6))) return false;
            if (!IsDigits(v.Substring(9, 1))) return false;

            return true;
        }

        private static bool IsDigits(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (s[i] < '0' || s[i] > '9') return false;
            return s.Length > 0;
        }

        private static bool IsLetter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }
    }
}
