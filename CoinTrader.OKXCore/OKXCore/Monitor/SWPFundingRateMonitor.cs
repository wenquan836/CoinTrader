using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.REST;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "合约费率查询")]
    public class SWPFundingRateMonitor : RESTMonitor
    {
        public FundingRate FundingRate
        {
            get;
            private set;
        }

        string instId;
        public string InstId
        {
            get
            {
                return this.instId;
            }
            set
            {
                this.FundingRate = new FundingRate();
                this.instId = value;
                var api = this.api as SwapFundingRate;
                api.instId = value;
                this.ForceUpdate();
            }
        }


        public SWPFundingRateMonitor(string instId) : base(new SwapFundingRate(instId), 120000)
        {
            this.FundingRate = new FundingRate();
            this.FundingRate.InstId = instId;
            AddRamdonCD(1000);
            this.CustomName = instId;
            this.instId = instId;
        }

        protected override void OnDataUpdate(JToken data)
        {
            var arr = data as JArray;
            this.FundingRate.ParseFromJson(arr[0]);
            base.OnDataUpdate(data);
            Feed();
        }
    }
}
