using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    //获取可交易的币对信息列表
    public class Instruments : RestAPIBase
    {
        public Instruments(string instType) : base(APIUrl.Instruments)
        {
            //this.NeedAuthentication = false;
            this.instType = instType;
        }

        public string instType { get; set; }
    }

    /// <summary>
    /// 现货交易对
    /// </summary>
    public class SpotInstruments : Instruments
    {
        public SpotInstruments()
            : base( InstrumentType.Spot)
        {

        }
    }

    /// <summary>
    /// 合约交易对
    /// </summary>
    public class SwapInstruments : Instruments
    {
        public SwapInstruments()
            : base(InstrumentType.Swap)
        {
        }
    }
}
