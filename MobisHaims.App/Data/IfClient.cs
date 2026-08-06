using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MobisHaims.Data
{
    // 표준 인터페이스 헤더 (daemon과 합의된 필드)
    public sealed class IfHeader
    {
        public string ifId, ifVer = "1.0", ifSenderGrp, ifSender, ifReceiverGrp, ifReceiver;
        public string ifTrackingId, ifDateTime, ifResult, ifFailMsg;

        public Dictionary<string, object> ToMap()
        {
            Dictionary<string, object> m = new Dictionary<string, object>();
            m["ifId"] = ifId; m["ifVer"] = ifVer;
            m["ifSenderGrp"] = ifSenderGrp; m["ifSender"] = ifSender;
            m["ifReceiverGrp"] = ifReceiverGrp; m["ifReceiver"] = ifReceiver;
            m["ifTrackingId"] = ifTrackingId; m["ifDateTime"] = ifDateTime;
            m["ifResult"] = ifResult; m["ifFailMsg"] = ifFailMsg;
            return m;
        }

        public static IfHeader FromMap(Dictionary<string, object> m)
        {
            IfHeader h = new IfHeader();
            if (m == null) return h;
            h.ifId = S(m, "ifId"); h.ifVer = S(m, "ifVer");
            h.ifSenderGrp = S(m, "ifSenderGrp"); h.ifSender = S(m, "ifSender");
            h.ifReceiverGrp = S(m, "ifReceiverGrp"); h.ifReceiver = S(m, "ifReceiver");
            h.ifTrackingId = S(m, "ifTrackingId"); h.ifDateTime = S(m, "ifDateTime");
            h.ifResult = S(m, "ifResult"); h.ifFailMsg = S(m, "ifFailMsg");
            return h;
        }
        private static string S(Dictionary<string, object> m, string k)
        { object v; return m.TryGetValue(k, out v) && v != null ? v.ToString() : null; }
    }

    // 요청/응답 공통 메시지: header + item(단건) + tdata(다건)
    public sealed class IfMessage
    {
        public IfHeader header = new IfHeader();
        public Dictionary<string, object> item = new Dictionary<string, object>();
        public List<Dictionary<string, object>> tdata = new List<Dictionary<string, object>>();

        public bool IsSuccess { get { return header != null && header.ifResult == "S"; } }

        public string ItemStr(string key)
        { object v; return item != null && item.TryGetValue(key, out v) && v != null ? v.ToString() : ""; }

        public string ToJson()
        {
            Dictionary<string, object> root = new Dictionary<string, object>();
            root["header"] = header.ToMap();
            root["item"] = item;
            List<object> arr = new List<object>();
            foreach (Dictionary<string, object> row in tdata) arr.Add(row);
            root["tdata"] = arr;
            return Json.Write(root);
        }

        public static IfMessage FromJson(string json)
        {
            IfMessage msg = new IfMessage();
            Dictionary<string, object> map = Json.Parse(json) as Dictionary<string, object>;
            if (map == null) return msg;
            object h; if (map.TryGetValue("header", out h)) msg.header = IfHeader.FromMap(h as Dictionary<string, object>);
            object it; if (map.TryGetValue("item", out it) && it is Dictionary<string, object>) msg.item = (Dictionary<string, object>)it;
            object td;
            if (map.TryGetValue("tdata", out td) && td is List<object>)
            {
                foreach (object row in (List<object>)td)
                {
                    Dictionary<string, object> r = row as Dictionary<string, object>;
                    if (r != null) msg.tdata.Add(r);
                }
            }
            return msg;
        }
    }

    public interface IIfTransport { string Post(string url, string json); }

    // 실제 데몬 HTTP POST /if
    public sealed class HttpIfTransport : IIfTransport
    {
        public int TimeoutMs = 15000;
        public string Post(string url, string json)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json; charset=utf-8";
            req.Timeout = TimeoutMs;
            byte[] body = Encoding.UTF8.GetBytes(json);
            req.ContentLength = body.Length;
            using (Stream s = req.GetRequestStream()) { s.Write(body, 0, body.Length); }
            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }
    }

    // 데몬 미연동 상태에서 화면을 검증하기 위한 목(Mock) 응답
    public sealed class MockIfTransport : IIfTransport
    {
        public string Post(string url, string json)
        {
            IfMessage req = IfMessage.FromJson(json);
            IfMessage res = new IfMessage();
            res.header = req.header;
            res.header.ifResult = "S";
            res.header.ifSenderGrp = "DAEMON"; res.header.ifReceiverGrp = "PDA";

            switch (req.header.ifId)
            {
                case "IF_LOGIN":
                    res.item["userId"] = "ryu";
                    res.item["userName"] = "류현진";
                    res.item["orgName"] = "테스트상사";
                    res.item["whCode"] = "W01";
                    break;

                case "IF_INB_PART":   // [120] 부번 조회
                    res.item["partNo"] = req.ItemStr("partNo");
                    res.item["partName"] = "테스트부품";
                    res.item["reserveQty"] = "0";
                    res.item["assignQty"] = "0";
                    res.item["assignCnt"] = "0";
                    res.item["loc"] = "A-01-01";
                    res.item["curStock"] = "0";
                    res.item["notRecv"] = "0";
                    break;

                case "IF_INB_CLASSIFY_SAVE":  // [120] 저장
                    res.item["savedQty"] = req.ItemStr("qty");
                    break;

                default:
                    res.header.ifResult = "E";
                    res.header.ifFailMsg = "Mock 미정의 ifId: " + req.header.ifId;
                    break;
            }
            return res.ToJson();
        }
    }

    // 화면에서 사용하는 통신 진입점
    public sealed class IfClient
    {
        private readonly IIfTransport _http = new HttpIfTransport();
        private readonly IIfTransport _mock = new MockIfTransport();

        public string ServerUrl;
        public bool MockMode = true;
        public string SenderId = "PDA01";

        public IfClient(string serverUrl, bool mockMode) { ServerUrl = serverUrl; MockMode = mockMode; }

        public IfMessage Send(string ifId, Dictionary<string, object> item, List<Dictionary<string, object>> tdata)
        {
            IfMessage req = new IfMessage();
            req.header.ifId = ifId;
            req.header.ifSenderGrp = "PDA"; req.header.ifSender = SenderId;
            req.header.ifReceiverGrp = "DAEMON"; req.header.ifReceiver = "HAIMS";
            req.header.ifTrackingId = Guid.NewGuid().ToString("N");
            req.header.ifDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (item != null) req.item = item;
            if (tdata != null) req.tdata = tdata;

            string url = (ServerUrl != null ? ServerUrl.TrimEnd('/') : "") + "/if";
            IIfTransport transport = MockMode ? _mock : _http;
            try
            {
                string resJson = transport.Post(url, req.ToJson());
                return IfMessage.FromJson(resJson);
            }
            catch (Exception ex)
            {
                IfMessage err = new IfMessage();
                err.header.ifId = ifId;
                err.header.ifResult = "E";
                err.header.ifFailMsg = "통신오류: " + ex.Message;
                return err;
            }
        }

        public IfMessage Send(string ifId, Dictionary<string, object> item) { return Send(ifId, item, null); }
    }
}
