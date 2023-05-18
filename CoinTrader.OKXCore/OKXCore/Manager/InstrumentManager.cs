
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.REST;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Manager
{
    public static class InstrumentManager
    {
        private static InstrumentSpotVsUSD spotInstrument = new InstrumentSpotVsUSD();
        private static InstrumentSwapVsUSD swapInstrument = new InstrumentSwapVsUSD();
        public static InstrumentSpotVsUSD SpotInstrument => spotInstrument;
        public static InstrumentSwapVsUSD SwapInstrument => swapInstrument;

        private static string UpdateInstrument<T>(IInstrumentTable instrument, string quoteCcy) 
            where T : RestAPIBase , new()
        {

            T api = new T();
            var result = api.ExecSync();

            string error = "";

            if (result.code == 0)
            {
                instrument.ParseFromJson(result.data as JArray,quoteCcy);
            }
            else
            {
                error = result.message;
            }

            return error;
        }

        public static  InstrumentBase GetInstrument(string instId)
        {
            InstrumentBase instrument = spotInstrument.GetInstrument(instId);

            if (instrument != null)
                return instrument;

            instrument = swapInstrument.GetInstrument(instId);

            if (instrument != null)
                return instrument;

            return null;
        }

        public static string UpdateInstrument(string quoteCcy)
        {
            string err = UpdateInstrument<SpotInstruments>(spotInstrument,quoteCcy);

            if (string.IsNullOrEmpty(err))
            {
                err = UpdateInstrument<SwapInstruments>(swapInstrument, quoteCcy);
            }

            return err;
        }

        public static Task<string> UpdateInstrumentAsync(string quoteCcy)
        {
            return Task.Run(() =>
            {
                return UpdateInstrument(quoteCcy);
            });
        }
    }
}
