using CoinTrader.Common.Classes;
using System;

namespace CoinTrader.Common.Interface
{
    public interface ICandleProvider
    {
        /// <summary>
        /// 循环每一个蜡烛图节点，直到返回true停止
        /// </summary>
        /// <param name="callback">如果这个回调返回true将不再继续</param>
        void EachCandle(Func<Candle,bool> callback);
        bool Loaded { get; }
        event EventHandler CandleLoaded;
    }
}
