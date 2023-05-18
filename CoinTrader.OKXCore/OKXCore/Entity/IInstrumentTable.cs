using Newtonsoft.Json.Linq;
 

namespace CoinTrader.OKXCore.Entity
{
    public interface IInstrumentTable
    {
         void ParseFromJson(JArray list, string quoteCcy);
    }
}
