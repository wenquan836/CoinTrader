using Newtonsoft.Json.Linq;


namespace CoinTrader.OKXCore.VO
{
    /// <summary>
    /// 用于传输socket接收到的数据
    /// </summary>
    public class SocketDataVO
    {
        public string Channel { get; private set; }
        public string InstId { get; private set; }
        public JArray Data { get; private set; }

        public void SetDatas(string channel, string instId, JArray data)
        {
            this.Channel = channel;
            this.InstId = instId;
            this.Data = data;
        }
    }
}
