using CoinTrader.Common.Util;
using Newtonsoft.Json.Linq;
using System;


namespace CoinTrader.OKXCore.Entity
{
    public class FundingRate
    {
       public string InstType { get; set; }
        public double Rate { get; set; }
        public string InstId { get; set; }

        public double NextRate { get; set; }

        public DateTime Time { get; set; }

        public DateTime NextTime { get; set; }


        public void ParseFromJson(JToken json)
        {
            this.InstType = json.Value<string>("instType");
            this.InstId = json.Value<string>("instId");
            this.Rate = json.Value<float>("fundingRate");
            this.NextRate = json.Value<float>("nextFundingRate");
            this.Time = DateUtil.TimestampMSToDateTime(json.Value<long>("fundingTime"));
            this.NextTime = DateUtil.TimestampMSToDateTime(json.Value<long>("nextFundingTime"));
        }
    }
}
