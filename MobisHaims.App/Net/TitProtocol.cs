using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace HaimsPda.Net
{
    public class HaimsException : Exception
    {
        public HaimsException(string msg) : base(msg) { }
    }

    public static class Crypto
    {
        public static string Sha256Hex(string s)
        {
            if (s == null) s = "";
            byte[] h = SHA256ManagedSimple.ComputeHash(Encoding.UTF8.GetBytes(s));
            StringBuilder sb = new StringBuilder(64);
            for (int i = 0; i < h.Length; i++) sb.Append(h[i].ToString("x2"));
            return sb.ToString();
        }
    }

    /// <summary>ref/root 요청 XML 빌더 (WebSquare.js tit_* 함수군 대응)</summary>
    public sealed class TitRequest
    {
        private readonly StringBuilder _params = new StringBuilder();
        private readonly StringBuilder _cmds = new StringBuilder();

        private const string ColInfo =
            "<colinfo id=\"TX_NAME\" size=\"100\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"TYPE\" size=\"10\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"SQL_ID\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"KEY_SQL_ID\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"KEY_INCREMENT\" size=\"10\" summ=\"default\" type=\"INT\"/>" +
            "<colinfo id=\"CALLBACK_SQL_ID\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"INSERT_SQL_ID\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"UPDATE_SQL_ID\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"DELETE_SQL_ID\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"SAVE_FLAG_COLUMN\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"USE_INPUT\" size=\"1\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"USE_ORDER\" size=\"1\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"KEY_ZERO_LEN\" size=\"10\" summ=\"default\" type=\"INT\"/>" +
            "<colinfo id=\"BIZ_NAME\" size=\"100\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"PAGE_NO\" size=\"10\" summ=\"default\" type=\"INT\"/>" +
            "<colinfo id=\"PAGE_SIZE\" size=\"10\" summ=\"default\" type=\"INT\"/>" +
            "<colinfo id=\"READ_ALL\" size=\"1\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"EXEC_TYPE\" size=\"1\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"EXEC\" size=\"1\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"FAIL\" size=\"1\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"FAIL_MSG\" size=\"200\" summ=\"default\" type=\"STRING\"/>" +
            "<colinfo id=\"EXEC_CNT\" size=\"1\" summ=\"default\" type=\"INT\"/>" +
            "<colinfo id=\"MSG\" size=\"200\" summ=\"default\" type=\"STRING\"/>";

        public TitRequest AddParam(string id, string value)
        {
            _params.Append("<param id=\"").Append(id).Append("\" type=\"STRING\">")
                   .Append(Esc(value)).Append("</param>");
            return this;
        }

        /// <summary>tit_AddSearchActionInfo</summary>
        public TitRequest AddSearch(string sqlId)
        {
            return AddRecord(sqlId, "N");
        }

        /// <summary>tit_AddMultiActionInfo</summary>
        public TitRequest AddMulti(string sqlId)
        {
            return AddRecord(sqlId, "M");
        }

        private TitRequest AddRecord(string sqlId, string type)
        {
            _cmds.Append("<record>")
                 .Append("<BIZ_NAME/><CALLBACK_SQL_ID/><DELETE_SQL_ID/><EXEC/><EXEC_CNT/>")
                 .Append("<EXEC_TYPE>B</EXEC_TYPE><FAIL/><FAIL_MSG/><INSERT_SQL_ID/>")
                 .Append("<KEY_INCREMENT>0</KEY_INCREMENT><KEY_SQL_ID/>")
                 .Append("<KEY_ZERO_LEN>0</KEY_ZERO_LEN><MSG/><PAGE_NO/><PAGE_SIZE/>")
                 .Append("<READ_ALL/><SAVE_FLAG_COLUMN/>")
                 .Append("<SQL_ID>").Append(Esc(sqlId)).Append("</SQL_ID>")
                 .Append("<TX_NAME/><TYPE>").Append(type).Append("</TYPE>")
                 .Append("<UPDATE_SQL_ID/><USE_INPUT/><USE_ORDER/>")
                 .Append("</record>");
            return this;
        }

        public string Build()
        {
            StringBuilder sb = new StringBuilder(4096);
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><params>");
            sb.Append(_params);
            sb.Append("</params>");
            if (_cmds.Length > 0)
            {
                sb.Append("<dataset id=\"ds_cmd\">");
                sb.Append(ColInfo);
                sb.Append(_cmds);
                sb.Append("</dataset>");
            }
            sb.Append("</root>");
            return sb.ToString();
        }

        private static string Esc(string s)
        {
            if (s == null || s.Length == 0) return "";
            StringBuilder sb = new StringBuilder(s.Length + 16);
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '&') sb.Append("&amp;");
                else if (c == '<') sb.Append("&lt;");
                else if (c == '>') sb.Append("&gt;");
                else sb.Append(c);
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 레코드 1건. 컬럼명 -> 값.
    /// 없는 컬럼과 빈 요소(&lt;X/&gt;)는 모두 빈 문자열로 돌려준다.
    /// </summary>
    public sealed class Row
    {
        private readonly Hashtable _v = new Hashtable();

        public string this[string column]
        {
            get { object o = _v[column]; return (o == null) ? "" : (string)o; }
        }

        public bool Has(string column) { return _v.Contains(column); }
        public ICollection Columns { get { return _v.Keys; } }
        internal void Set(string column, string value) { _v[column] = value; }
    }

    /// <summary>레코드 1건이 파싱될 때마다 호출된다. 여기서 필요한 것만 남기고 버린다.</summary>
    public delegate void RecordCallback(string datasetId, Row row);

    /// <summary>
    /// 응답의 params 부분과 오류 상태.
    /// dataset 은 담지 않는다 (RecordCallback 으로 흘려보낸다).
    /// </summary>
    public sealed class TitResult
    {
        private readonly Hashtable _params = new Hashtable();

        public string Param(string id)
        {
            object o = _params[id];
            return (o == null) ? "" : (string)o;
        }

        internal void SetParam(string id, string value) { _params[id] = value; }

        public string ErrorCode { get { return Param("ErrorCode"); } }

        public bool IsError
        {
            get { string c = ErrorCode; return (c.Length > 0 && c != "0"); }
        }

        public string ErrorMsg
        {
            get
            {
                string m = Param("ErrorMsg");
                return (m.Length == 0 || m == "OK") ? "서버 오류가 발생했습니다." : m;
            }
        }
    }


    /// <summary>POST /Main 전송</summary>
    public static class HaimsHttp
    {
        // 앱(PDA) 클라이언트는 HTTP 사용 -> CF 3.5 의 TLS 1.2 미지원 회피
        public static string BaseUrl = "http://pda.haims.co.kr/Main";
        public static int TimeoutMs = 20000;

        private static Encoding _resEnc;
        private static Encoding ResponseEncoding
        {
            get
            {
                if (_resEnc == null)
                {
                    // 서버 응답은 EUC-KR. 949(CP949/UHC)가 상위호환이며 한글 WM 에 항상 존재.
                    try { _resEnc = Encoding.GetEncoding(949); }
                    catch
                    {
                        try { _resEnc = Encoding.GetEncoding(51949); }
                        catch { _resEnc = Encoding.UTF8; }
                    }
                }
                return _resEnc;
            }
        }

        /// <summary>
        /// 요청을 보내고 응답을 스트리밍 파싱한다.
        ///
        /// 응답 전체를 string 이나 XmlDocument 로 올리지 않는다.
        /// 네트워크 스트림 -> XmlTextReader 로 바로 넘기고, 레코드 1건이 완성될 때마다
        /// onRecord 로 던진 뒤 즉시 버린다. 메모리 사용량이 레코드 1건 수준으로 유지되므로
        /// 32MB 프로세스 슬롯에서 메뉴/공통코드 같은 대용량 응답도 안전하다.
        ///
        /// onRecord 는 null 이어도 된다(params 만 필요한 경우).
        /// </summary>
        public static TitResult PostStream(string pgm, string function, string body,
                                           RecordCallback onRecord)
        {
            string url = BaseUrl + "?pgm=" + pgm + "&function=" + function;

            Log.Write(">>> POST " + url);
            Log.WriteBody("REQ", body);

            int t0 = Environment.TickCount;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "text/xml";     // 브라우저와 동일하게 charset 미지정
            req.Accept = "text/xml";
            req.KeepAlive = false;
            req.Timeout = TimeoutMs;

            byte[] buf = Encoding.UTF8.GetBytes(body);
            req.ContentLength = buf.Length;

            using (Stream s = req.GetRequestStream())
                s.Write(buf, 0, buf.Length);

            TitResult result = new TitResult();

            // 데이터셋별 레코드 건수 (Summary 로그용)
            Hashtable counts = Log.IsOn ? new Hashtable() : null;
            RecordCallback sink = onRecord;
            if (counts != null) sink = new CountingSink(counts, onRecord).Accept;

            StringBuilder tee = Log.IsFull ? new StringBuilder(Log.BodyCap) : null;
            int status = 0;

            try
            {
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                using (Stream ns = res.GetResponseStream())
                // StreamReader 로 먼저 감싸면 XML 선언의 EUC-KR 을 CF 가 해석하지 않아도 된다
                using (StreamReader sr = new StreamReader(ns, ResponseEncoding))
                {
                    status = (int)res.StatusCode;

                    TextReader src = sr;
                    if (tee != null) src = new TeeTextReader(sr, tee, Log.BodyCap);

                    using (XmlTextReader r = new XmlTextReader(src))
                    {
                        r.WhitespaceHandling = WhitespaceHandling.None;
                        Parse(r, result, sink);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("<<< FAIL " + function + " : " + ex.Message);
                throw;
            }
            finally
            {
                if (tee != null) Log.WriteBody("RES", tee.ToString());
            }

            if (Log.IsOn)
            {
                StringBuilder sb = new StringBuilder(128);
                sb.Append("<<< ").Append(status).Append(" ").Append(function);
                sb.Append(" ").Append(Environment.TickCount - t0).Append("ms");
                sb.Append(" err=").Append(result.ErrorCode);
                string em = result.Param("ErrorMsg");
                if (em.Length > 0) sb.Append("(").Append(em).Append(")");
                foreach (DictionaryEntry e in counts)
                    sb.Append(" ").Append(e.Key).Append("=").Append(e.Value);
                Log.Write(sb.ToString());
            }

            return result;
        }

        /// <summary>데이터셋별 레코드 건수를 세면서 원래 콜백으로 넘긴다.</summary>
        private sealed class CountingSink
        {
            private readonly Hashtable _counts;
            private readonly RecordCallback _next;

            public CountingSink(Hashtable counts, RecordCallback next)
            {
                _counts = counts; _next = next;
            }

            public void Accept(string ds, Row row)
            {
                string k = (ds == null) ? "(none)" : ds;
                object o = _counts[k];
                _counts[k] = (o == null) ? 1 : ((int)o) + 1;
                if (_next != null) _next(ds, row);
            }
        }


        private static void Parse(XmlTextReader r, TitResult result, RecordCallback onRecord)
        {
            string dsId = null;
            string paramId = null;
            Row row = null;
            string col = null;

            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    string name = r.Name;

                    if (name == "param")
                    {
                        paramId = r.GetAttribute("id");
                        // 빈 요소면 Text 노드가 오지 않으므로 여기서 확정한다
                        if (r.IsEmptyElement && paramId != null)
                        {
                            result.SetParam(paramId, "");
                            paramId = null;
                        }
                    }
                    else if (name == "dataset")
                    {
                        dsId = r.GetAttribute("id");
                    }
                    else if (name == "record")
                    {
                        row = new Row();
                        col = null;
                    }
                    else if (name == "colinfo" || name == "root" || name == "params")
                    {
                        // 스키마/컨테이너는 버린다
                    }
                    else if (row != null)
                    {
                        col = name;
                        if (r.IsEmptyElement) { row.Set(col, ""); col = null; }
                    }
                }
                else if (r.NodeType == XmlNodeType.Text || r.NodeType == XmlNodeType.CDATA)
                {
                    // 빈 값이 개행+공백으로 오므로 항상 Trim
                    if (row != null && col != null) row.Set(col, r.Value.Trim());
                    else if (paramId != null) result.SetParam(paramId, r.Value.Trim());
                }
                else if (r.NodeType == XmlNodeType.EndElement)
                {
                    string name = r.Name;

                    if (name == "param") paramId = null;
                    else if (name == "record")
                    {
                        if (row != null && onRecord != null) onRecord(dsId, row);
                        row = null;      // 여기서 버린다 = 메모리 상수 유지
                        col = null;
                    }
                    else if (name == "dataset") dsId = null;
                    else if (row != null) col = null;
                }
            }
        }

        /// <summary>_SESSION_NO = yyyyMMdd + epoch millis (gfn_getToday() + Date.getTime())</summary>
        public static string NewSessionNo()
        {
            long ms = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return DateTime.Now.ToString("yyyyMMdd") + ms.ToString();
        }
    }
}
