using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CoinTrader.OKXCore.Entity
{
    public interface IInstrumentTable
    {
         void ParseFromJson(JArray list, string quoteCcy);

         List<InstrumentBase> GetInstrumentList();


    }
}
