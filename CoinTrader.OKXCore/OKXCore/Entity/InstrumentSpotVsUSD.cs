using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{

    //获取币币市场以usdt计价的币对
    public class InstrumentSpotVsUSD: IInstrumentTable
    {
        Dictionary<string, InstrumentSpot> InstrumentTable = new Dictionary<string, InstrumentSpot>();
        Dictionary<string, InstrumentSpot> list = new Dictionary<string, InstrumentSpot>();
        private string quoteCcy = string.Empty;

        public InstrumentSpotVsUSD() { }

        public Dictionary<string,InstrumentSpot> GetAllInstrument()
        {
            return this.list;
        }

        public InstrumentSpot GetInstrument(string instId)
        {
            instId = instId.ToUpper();

            return this.InstrumentTable.ContainsKey(instId) ? this.InstrumentTable[instId] : null;
        }



        public void ParseFromJson(JArray list, string quoteCcy)
        {
            this.quoteCcy = quoteCcy;
            foreach (var item in list)
            {
                InstrumentSpot instrument = new InstrumentSpot();
                instrument.ParseFromJson(item);

                if (string.Compare(item["quoteCcy"].Value<string>(), quoteCcy, true) == 0)
                {
                    this.list[instrument.BaseCurrency.ToUpper()] = instrument;
                }

                this.InstrumentTable[instrument.InstrumentId] = instrument;
            }
        }

        public bool HasInstrument(string instrumentId)
        {
            return this.InstrumentTable.ContainsKey(instrumentId);
        }
    }
}
