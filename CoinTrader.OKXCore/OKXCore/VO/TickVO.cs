

namespace CoinTrader.OKXCore.VO
{
    /// <summary>
    /// 报价
    /// </summary>
    public class TickVO
    {
        public string InstId { get; private set; }
        public decimal Ask { get; private set; }
        public decimal Bid { get; private set; }

        public void SetData(string instId,decimal ask,decimal bid)
        {
            this.InstId = InstId;
            this.Ask = ask;
            this.Bid = bid;
        }
    }
}
