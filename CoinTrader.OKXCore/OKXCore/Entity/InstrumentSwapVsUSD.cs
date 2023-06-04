using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CoinTrader.OKXCore.Entity
{

    //获取币币市场以usdt计价的币对
    public class InstrumentSwapVsUSD: IInstrumentTable
    {
        Dictionary<string, InstrumentSwap> InstrumentTable = new Dictionary<string, InstrumentSwap>();
        Dictionary<string, InstrumentSwap> list = new Dictionary<string, InstrumentSwap>();

        private string quoteCcy = string.Empty;
        public InstrumentSwapVsUSD() { }
        public Dictionary<string, InstrumentSwap> GetAllInstrument()
        {
            return list;
        }

        public List<InstrumentBase> GetInstrumentList()
        {
            var baseList = new List<InstrumentBase>();
            baseList.AddRange(list.Values);
            return baseList;
        }


        public InstrumentSwap GetInstrument(string instrument)
        {
            instrument = instrument.ToUpper();

            return this.InstrumentTable.ContainsKey(instrument) ? this.InstrumentTable[instrument] : null;
        }

        public void ParseFromJson(JArray list, string quoteCcy)
        {
            this.quoteCcy = quoteCcy.ToUpper();
            foreach (var item in list)
            {
                InstrumentSwap instrument = new InstrumentSwap();
                instrument.ParseFromJson(item);

                if (string.Compare(instrument.SettleCcy, quoteCcy, true) == 0)
                {
                    this.list[instrument.CtValCcy.ToUpper()] = instrument;
                }

                this.InstrumentTable[instrument.InstrumentId] = instrument;
            }
        }

        public bool HasInstrumentByCoin(string coin)
        {
            string instId = string.Format("{0}-{1}-SWAP",coin.ToUpper(), quoteCcy);
            return this.HasInstrument(instId);
        }

        public bool HasInstrument(string instrumentId)
        {
            return this.InstrumentTable.ContainsKey(instrumentId);
        }
    }
}
