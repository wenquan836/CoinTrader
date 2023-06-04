using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using Brotli;
using CoinTrader.Common.Util;

namespace CoinTrader.Common
{
    public static class Http
    {
        public static string Method_Get = "GET";
        public static string Method_Post = "POST";
        public static string Method_Delete = "DELETE";

        public static float Timeout
        {
            get;set;
        }

        public static Task<JObject> HttpGet(string url, Dictionary<string, string> head = null)
        {
            Task<JObject> task = Task.Run(() => { return HttpSend(url, Method_Get, null, head); });
            return task;
        }

        public static Task<JObject> HttpPost(string url, JObject data, Dictionary<string, string> head = null)
        {
            Task<JObject> task = Task.Run(() => { return HttpSend(url, Method_Post, data.ToString(), head); });
            return task;
        }

        public static Task<JObject> HttpPost(string url, string data, Dictionary<string, string> head = null)
        {
            Task<JObject> task = Task.Run(() => { return HttpSend(url, Method_Post, data, head); });
            return task;
        }

        public static string HttpUrlEncode(string str)
        {
            return WebUtility.UrlEncode(str);
        }

        public static string HttpUrlDecode(string str)
        {
            return WebUtility.UrlDecode(str);
        }

        private static int requestTimeout = 3000;
        public static int RequestTimeOut
        {
            get { return requestTimeout; }

            set { requestTimeout = value; }
        }

        const string DefualtUserAgent = ""; //"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";


        private static Encoding _encoding = Encoding.UTF8;
        public static Encoding RequestEncoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        public static JObject HttpSend(string url, string method, string post, Dictionary<string, string> head, string userAgent = null, string referer = null, CookieContainer cookies = null)
        {
            HttpWebRequest requet;
            long startTime = DateUtil.GetTimestampMS();
            int code = 0;
            string message;

            try
            {
                requet = HttpWebRequest.Create(url) as HttpWebRequest;
            }
            catch (Exception ex)
            {
                int state = (int)HttpStatusCode.BadRequest;
                code = state;
                message = ex.Message;
                goto do_break;
            }

            requet.Accept = "application/json";

            if (head != null)
            {
                foreach (var kv in head)
                {
                    requet.Headers.Add(kv.Key, kv.Value);
                }
            }

            requet.Timeout = RequestTimeOut;
            requet.Method = method;

            //requet.Proxy = null;
            //requet.KeepAlive = false;
            //requet.CookieContainer.SetCookies("");

            if (!string.IsNullOrEmpty(referer))
                requet.Referer = referer;

            if (!string.IsNullOrEmpty(userAgent))
                requet.UserAgent = userAgent;
            else
                requet.UserAgent = DefualtUserAgent;

            //if (cookies != null)
            //    requet.CookieContainer = cookies;


            if (!string.IsNullOrEmpty(post) && method == Method_Post)
            {
                requet.ContentType = "application/json";

                try
                {
                    using (StreamWriter writer = new StreamWriter(requet.GetRequestStream(), RequestEncoding))//<--
                    {
                        writer.Write(post);
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    goto do_break;
                }
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)requet.GetResponse();
                Stream stream = response.GetResponseStream();

                string contentEncoding = !string.IsNullOrEmpty(response.ContentEncoding) ? response.ContentEncoding.ToLower() : "";
                string content = "";
                HttpStatusCode status = response.StatusCode;

                if (contentEncoding.Contains("gzip"))
                {
                    using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(gzipStream, Encoding.UTF8))
                        {
                            content = reader.ReadToEnd();
                        }
                    }
                }
                else if (contentEncoding.Contains("deflate"))
                {
                    using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(deflateStream, Encoding.UTF8))
                        {
                            content = reader.ReadToEnd();
                        }
                    }
                }
                else if (contentEncoding.Contains("br") || contentEncoding.Contains("brotli"))
                {
                    using (BrotliStream brStream = new BrotliStream(stream, CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(brStream, Encoding.UTF8))
                        {
                            content = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        content = sr.ReadToEnd();
                    }
                }

                JToken result = null;
                try
                {
                    result = JToken.Parse(content);
                }
                catch (Exception ex)
                {
                    code = (int)status;
                    message = ex.Message;
                }

                if (!(result is JObject) || result["data"] == null)// result["data"] == null)
                {
                    var n = new JObject
                    {
                        ["code"] = 0,
                        ["data"] = result
                    };
                    result = n;
                }

                response.Close();

                long now = DateUtil.GetTimestampMS();
                Timeout = (now - startTime) / 1000.0f;
                return result as JObject;
            }
            catch (WebException ex)
            {
                HttpWebResponse wrs = (HttpWebResponse)ex.Response;
                string responseText = ex.Message;
                if (wrs != null && wrs.ContentLength > 0)
                {
                    Stream stream = wrs.GetResponseStream();

                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        try
                        {
                            responseText = sr.ReadToEnd();
                        }
                        catch
                        {

                        }
                    }
                }
                int state = (int)HttpStatusCode.NotFound;

                if (wrs != null)
                    state = (int)wrs.StatusCode;
                code = state;
                message = responseText;
            }
            catch (Exception ex)
            {
                int state = (int)HttpStatusCode.BadRequest;

                code = state;
                message = ex.Message;
            }
            finally
            {

            }

            do_break:
            var responseJson = new JObject();
            responseJson["code"] = code;
            responseJson["msg"] = message;
            return responseJson;

        }
    }
}
