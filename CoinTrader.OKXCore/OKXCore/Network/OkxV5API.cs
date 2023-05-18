using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using System.Threading;
using CoinTrader.Common;
using CoinTrader.Common;
using CoinTrader.Common.Util;

namespace CoinTrader.OKXCore.Network
{
    /// <summary>
    /// 其中 op 的取值为 1--subscribe 订阅； 2-- unsubscribe 取消订阅 ；3--login 登录
    /// </summary>
    public enum APIOperate
    {
        Login       = 3,   //登录
        Subscribe   = 1,//订阅
        Unsubscribe = 2,//取消订阅
    }

    public class OkxV5API<T> where T : IWebSocket, new()
    {
        private const int retryDelayTime = 5000; //重新连接的等待时间，毫秒
        private IWebSocket socketProxy = null;
        private string address = "";
        private bool isPrivate = false;
        private bool closeBySelf = false;

        /// <summary>
        /// 自动重新建立连接
        /// </summary>
        protected bool AutoReconnect
        {
            get;set;
        }

        public event Action<string, string, JArray> OnData = null;

        /// <summary>
        /// 连接成功并登录
        /// </summary>
        public event Action<bool, string> OnLogin = null;

        /// <summary>
        /// 连接失败或者退出登录
        /// </summary>
        public event Action OnLogout = null;

 
        public OkxV5API(bool isPrivate, string address)
        {
            
            this.isPrivate = isPrivate;
            this.CreateSocket(0);
            this.address = address;
            this.AutoReconnect = true;
        }
        private async void CreateSocket(int delayTime = 0)
        {
            Action create = () =>
            {
                if(delayTime > 0)
                    Thread.Sleep(delayTime);
                
                T socket = new T();

                socket.OnClose += this.OnClose;
                socket.OnError += this.OnError;
                socket.OnMessage += this.OnMessage;
                socket.OnOpen += this.OnOpen;

                this.socketProxy = socket;
                this.Connect(address);
            };

            await Task.Run(create);
        }
    
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="args">参数</param>
        public void Unsubscribe(params string[] args)
        {
            
            JObject json = GetCommand(APIOperate.Unsubscribe, args);
            this.Send(json);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="args">参数</param>
        public void Unsubscribe(string channel, string instId)
        {
            JObject token = new JObject();
            token["channel"] = channel;
            if(! string.IsNullOrEmpty( instId))
                token["instId"] = instId;
            
            //token["instType"] = "SPOT";

            JObject json = GetCommand(APIOperate.Unsubscribe, token);
            this.Send(json);
        }

        public void Subscribe(params string[] args)
        {
            JObject json = GetCommand(APIOperate.Subscribe, args);
            this.Send(json);
        }

        public void Subscribe(string channel,string instId)
        {
            JObject token = new JObject();
            token["channel"] = channel;
            if(!string.IsNullOrEmpty( instId))
                token["instId"] = instId;
            //token["instType"] = "SPOT";

            JObject json = GetCommand(APIOperate.Subscribe, token);
            this.Send(json);
        }

        /// <summary>
        /// 解压缩数据流 
        /// </summary>
        /// <param name="baseBytes"></param>
        /// <returns></returns>
        private static string Decompress(byte[] baseBytes)
        {
            using (var decompressedStream = new MemoryStream())
            using (var compressedStream = new MemoryStream(baseBytes))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private Queue<JObject> CommandPool = new Queue<JObject>();

        private JObject GetCommand(APIOperate operate, params object[] args)
        {
            JObject json;

            const string key_args = "args";
            const string key_op = "op";

            if (CommandPool.Count > 0)
            {
                json = CommandPool.Dequeue();
                (json[key_args] as JArray).Clear();
            }
            else
            {
                json = new JObject();
                json[key_args] = new JArray();
            }

            json[key_op] = operate.ToString().ToLower();
            JArray jargs = json[key_args] as JArray;

            foreach (object obj in args)
            {
                jargs.Add(obj);
            }
            return json;
        }

        private JObject GetCommand(APIOperate operate,params JToken[] args)
        {
            JObject json = null;

            const string key_args = "args";
            const string key_op = "op";

            if (CommandPool.Count > 0)
            {
                json = CommandPool.Dequeue();
                (json[key_args] as JArray).Clear();
            }
            else
            {
                json = new JObject();
                json[key_args] = new JArray();
            }

            json[key_op] = operate.ToString().ToLower();
            JArray jargs = json[key_args] as JArray;

            foreach(JToken a in args)
            {
                jargs.Add(a);
            }

            return json;
        }

        /// <summary>
        /// 发送登录请求
        /// </summary>
        private void SendLogin()
        {
            if (isPrivate)//如果是私有的，就需要先登录
            {
                var apiInfo = APIStorage.APIInfo;
                long timestamp = DateUtil.GetServerTimestampSec();
                string sign = Crypt.GenareteSign(apiInfo.SecretKey, timestamp.ToString(), "GET", "/users/self/verify");
                JObject data = new JObject();

                data["apiKey"] = apiInfo.ApiKey;
                data["passphrase"] = apiInfo.Passphrase;
                data["timestamp"] = timestamp;
                data["sign"] = sign;
            
                JObject json = GetCommand(APIOperate.Login, data);
                this.Send(json.ToString());
            }
            else
            {
                this.IsLogin = true;
                this.OnLogin?.Invoke(this.IsLogin,"");
            }
        }

        private void OnOpen()
        {
            isAlive = true;
            SendLogin();
        }

        private void Disconnected(string msg, bool reconnect)
        {
            IsLogin = false;
            isAlive = false;
            this.Cleanup();

            Logger.Instance.Log(LogType.Error, msg);

            OnLogout?.Invoke();
            if (reconnect)
                CreateSocket(retryDelayTime);//意外断开，重建socket连接
        }

        private void Connect(string address)
        {
            this.IsLogin = false;

            this.socketProxy.ConnectAsync(address);

        }

        private void OnError(string msg)
        {
            Disconnected(msg,true);
        }

        private void OnClose()
        {
            Disconnected("WebSocket disconnected", !closeBySelf && this.AutoReconnect);  
        }

        /**
         * 发送心跳
         * 
         */
        public void SendPing()
        {
            if (this.IsAlive)
                this.socketProxy.SendAsync("ping");
        }

        public void Send(string data)
        {
            if(this.IsAlive)
                this.socketProxy.SendAsync(data);
        }

        public bool IsLogin
        {
            get;
            private set;
        }

        public bool isAlive = false;

        /// <summary>
        /// 是否已连上并登录成功
        /// </summary>
        public bool IsAlive => isAlive;
        private void OnMessage(byte[] data, int count)
        {
            string str = Encoding.UTF8.GetString(data, 0, count).Trim();// Decompress(data);
            if (str == "pong")
            {
                return;
            }

            JObject json;
            try
            {
                json = JObject.Parse(str);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                return;
            }
            if (json.ContainsKey("event"))
            {
                string evt = json["event"].Value<string>();
                switch (evt)
                {
                    case "error":
                        Logger.Instance.Log(LogType.Error, string.Format("{0}:{1}", json["code"].Value<string>(), json["msg"].Value<string>()));
                        break;
                    case "login":
                        this.IsLogin = json.Value<int>("code") == 0;
                        string msg = json.Value<string>("msg");
                        this.OnLogin?.Invoke(this.IsLogin,msg);
                        break;
                }
            }
            else // if (json.ContainsKey(Table_table))
            {
                JObject args = json["arg"] as JObject;
                JArray datas = json["data"] as JArray;

                string channel = args.Value<string>("channel");
                string instId = args.Value<string>("instId");

                this.OnData?.Invoke(channel, instId, datas);
            }
            //{"event":"error"," message":"","errorCode":""} //错误格式
        }
 
        public void Send(JObject json)
        {
            string str = json.ToString();
            this.Send(str);
            CommandPool.Enqueue(json);
        }

        private void Cleanup()
        {
            if (socketProxy != null)
            {
                socketProxy.OnClose -= this.OnClose;
                socketProxy.OnError -= this.OnError;
                socketProxy.OnMessage -= this.OnMessage;
                socketProxy.OnOpen -= this.OnOpen;
                this.socketProxy = null;
            }
        }
        public void Close()
        {

            closeBySelf = true;
            if(this.socketProxy != null)
                this.socketProxy.CloseAsync();
        }
    }
}
