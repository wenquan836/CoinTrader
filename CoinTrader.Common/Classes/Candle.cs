using CoinTrader.Common.Interface;
using System;

namespace CoinTrader.Common.Classes
{
    public class Candle:IPoolObject
    {
        public DateTime Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }

        public long Timestamp { get; set; }

        public bool Confirm { get; set; }

        public void CopyTo(Candle target)
        {
            target.Time = this.Time;
            target.Open = this.Open;
            target.High = this.High;
            target.Low = this.Low;
            target.Close = this.Close;
            target.Volume = this.Volume;
            target.Confirm = this.Confirm;
            target.Timestamp = this.Timestamp;
        }

        public void CopyFrom(Candle source)
        {
            this.Time = source.Time;
            this.Open = source.Open;
            this.High = source.High;
            this.Low = source.Low;
            this.Close = source.Close;
            this.Volume = source.Volume;
            this.Confirm = source.Confirm;
            this.Timestamp = source.Timestamp;
        }

        public void PoolReserve()
        {
            this.Time = DateTime.Now;
            this.Open = 0;
            this.High = 0;
            this.Low = 0;
            this.Close = 0;
            this.Volume = 0;
            this.Confirm = false;
            this.Timestamp = 0;
        }
    }
}
