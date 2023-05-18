
using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.Common.Util;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "K线数据")]
    public class CandleMonitor : RESTMonitor,ICandleProvider
    {
        private bool loaded = false;
        private bool disposed = false;
        private bool skipOnce = false;
        private const int CandleCapacity = 300; //最大容量
        private long maxTimestamp = 0;//记录最后的K线时间
        private long minTimestamp = 0;//最早的K线时间
        private List<Candle> candles = new List<Candle>(CandleCapacity);
        private CandleGranularity granularity = CandleGranularity.M1;
        private static int NextDelay = 0;
        private static readonly int CDRND = 1000;//错峰请求的随机CD值 
        private bool historyLoading = false;
        private object historyLock = new object();

        public DateTime MaxTime => DateUtil.TimestampMSToDateTime(maxTimestamp);

        public DateTime MinTime => DateUtil.TimestampMSToDateTime(minTimestamp);

        public event EventHandler CandleLoaded;
        public override string CustomName
        {
            get
            {
                return string.Format("{0}({1})",this.InstrumentId, this.granularity);
            }
        }

        /// <summary>
        /// 币对
        /// </summary>
        public string InstrumentId { get; private set; }

        /// <summary>
        /// K线粒度
        /// </summary>
        public CandleGranularity Granularity
        {
            get { return granularity; }
            set
            {
                lock (this.candles)
                {
                    this.CandlesToPool();
                }

                granularity = value;
                this.Interval = Math.Min(5 * 60000, Convert.ToUInt32((Convert.ToUInt32(granularity) * 1000) / 3.0f));


                var api = this.api as CandleList;

                api.bar = Granularity2V5String(value);
                api.before = "";
                api.limit = CandleCapacity;
                this.AddCD(NextDelay);//增加随机CD避免阻塞// 
                this.skipOnce = true;
            }
        }

        public bool Loaded
        {
            get { return loaded; }
        }

        private static string Granularity2V5String(CandleGranularity granularity)
        {
            switch(granularity)
            {
                case CandleGranularity.M1:
                    return "1m";
                case CandleGranularity.M3:
                    return "3m";
                case CandleGranularity.M5:
                    return "5m";
                case CandleGranularity.M15:
                    return "15m";
                case CandleGranularity.M30:
                    return "30m";
                case CandleGranularity.H1:
                    return "1H";
                case CandleGranularity.H4:
                    return "4H";
                case CandleGranularity.H6:
                    return "6H";
                case CandleGranularity.H12:
                    return "12H";
                case CandleGranularity.D1:
                    return "1D";
                case CandleGranularity.Week1:
                    return "1W";
                case CandleGranularity.Month1:
                    return "1M";
                case CandleGranularity.Y1:
                    return "1Y";
                default:
                    return "1m";
            }
        }

        public void UpdateLastPrice(decimal price, DateTime time)
        {
            if(disposed) return;

            lock(this.candles)
            {
                var last = this.candles.Count > 0 ? this.candles[0] : null;
                uint interval = (uint)this.granularity;
                if (last != null)
                {
                    TimeSpan ts = time - last.Time;

                    if(ts.TotalSeconds > interval)
                    {
                        Candle candle = VOPool<Candle>.GetPool().Get();
                        candle.Open = candle.High = candle.Low = candle.Close = price;

                        int y, M, d, h, m;

                        y = time.Year;
                        M = time.Month;
                        d = time.Day;
                        h = time.Hour;
                        m = time.Minute;

                        switch (this.granularity)
                        {
                            case CandleGranularity.M1:
                                break;
                            case CandleGranularity.M3:
                                m -= m % 3;
                                break;
                            case CandleGranularity.M5:
                                m -= m % 5;
                                break;
                            case CandleGranularity.M15:
                                m -= m % 15;
                                break;
                            case CandleGranularity.M30:
                                m -= m % 30;
                                break;
                            case CandleGranularity.H1:
                                m = 0;
                                break;
                            case CandleGranularity.H4:
                                m = 0;
                                h -= h % 4;
                                break;
                            case CandleGranularity.H6:
                                m = 0;
                                h -= h % 6;
                                break;
                            case CandleGranularity.H12:
                                m = 0;
                                h -= h % 12;
                                break;
                            case CandleGranularity.D1:
                                h = 0;
                                m = 0;
                                break;
                            case CandleGranularity.Week1:
                                DateTime newTime = time;
                                newTime = newTime.AddDays(-(int)newTime.DayOfWeek);
                                y = newTime.Year;
                                M = newTime.Month;
                                d = newTime.Day;
                                h = 0;
                                m = 0;
                                break;
                            case CandleGranularity.Month1:
                                d = 1;h = 0;m = 0;
                                break;
                            case CandleGranularity.Y1:
                                M = 0; d = 1; h = 0; m = 0;
                                break;
                        }

                        candle.Time = new DateTime(y, M, d, h, m, 0);
                        candle.Confirm = false;
                        candle.Volume = 0;
                        this.candles.Insert(0, candle);
                    }
                    else
                    {
                        last.Close = price;
                        last.High = Math.Max(price, last.High);
                        last.Low = Math.Min(price, last.Low);
                    }
                }
            }
        }

        /// <summary>
        /// 加载更多历史数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public Task<bool> LoadMore(DateTime startDate)
        {
            return Task.Run(async () =>
            {
                if (disposed) return false;
                
                if (loaded)
                {
                    HistoryCandle api = new HistoryCandle()
                    {
                        bar = Granularity2V5String(granularity),
                        instId = this.InstrumentId,
                        limit = 100
                    };

                    List<Candle> tmp = new List<Candle>(api.limit);
                    var pool = VOPool<Candle>.GetPool();

                    while (MinTime > startDate)
                    {
                        api.after = this.minTimestamp.ToString();

                        var result = await api.Exec();

                        if (result.success)
                        {
                            var data = result.data as JArray;

                            if (data != null && data.Count > 0)
                            {
                                foreach (var d in data)
                                {
                                    var candle = pool.Get();
                                    ParseCandle(ref candle, d as JArray);
                                    minTimestamp = Math.Min(minTimestamp, candle.Timestamp);
                                    tmp.Add(candle);
                                }

                                lock (this.candles)
                                {
                                    candles.AddRange(tmp);
                                    tmp.Clear();
                                }

                                //API限制为200毫秒
                                Thread.Sleep(200);
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    return true;
                }
                return false;
            });
        }

        public CandleMonitor(string instrumentId, CandleGranularity granularity)
            : base(new CandleList(instrumentId, Granularity2V5String(granularity)), 800)
        {
            this.Granularity = granularity;
            this.InstrumentId = instrumentId;
            this.skipOnce = false;
            base.api.OnData += Api_OnData;

            NextDelay += 30;//解决大量同时创建并发问题阻塞的问题
        }

        private VOPool<Candle> CandlesToPool()
        {
            VOPool<Candle> pool = VOPool<Candle>.GetPool();

            pool.Put(this.candles);
            this.candles.Clear();

            return pool;
        }

        public void EachCandle(Func<Candle,bool> callback)
        {
            lock (candles)
            {
                foreach (var c in candles)
                {
                    if (callback(c))
                        break;
                }
            }
        }

        public override void Dispose()
        {
            disposed = true;
            this.CandlesToPool();
            loaded = false;
            base.Dispose();
        }

        private void ParseCandle(ref Candle candle, JArray arr)
        {
            candle.Timestamp = arr[0].Value<long>();
            candle.Time = DateUtil.TimestampMSToDateTime(candle.Timestamp);
            candle.Open = arr[1].Value<decimal>();
            candle.High = arr[2].Value<decimal>();
            candle.Low = arr[3].Value<decimal>();
            candle.Close = arr[4].Value<decimal>();
            candle.Volume = arr[5].Value<decimal>();
            candle.Confirm = arr[8].Value<int>() == 1;//k线已完结
        }

        private void Api_OnData(APIResult obj)
        {
            if (disposed) return;
            //增加随机CD避免阻塞
            // 错开大量K线数据需要请求时的并发
            this.AddRamdonCD(CDRND);
            NextDelay = 0;
            if (obj.code == 0)
            {
                JArray data = obj.data as JArray;

                lock (this.candles)
                {
                    if (this.skipOnce) //标记修改粒度后跳过一次回调， 避免收到脏数据
                    {
                        this.skipOnce = false;
                        return;
                    }

                    VOPool<Candle> pool = VOPool<Candle>.GetPool();// this.CandlesToPool();

                    #region 移除头部之前未完成的K线，包括本地生成的

                    while (candles.Count > 0 && !candles[0].Confirm)
                    {
                        pool.Put(candles[0]);
                        candles.RemoveAt(0);
                    }

                    #endregion

                    int index = 0;
                    JArray arr;
                    long ts;
                    Candle candle;

                    foreach (JToken jt in data)
                    {
                        candle = pool.Get();
                        arr = jt as JArray;
                        ParseCandle(ref candle,arr);
                        ts = candle.Timestamp;

                        if(candle.Confirm)
                            maxTimestamp = Math.Max(maxTimestamp, ts);

                        if(minTimestamp == 0)
                            minTimestamp = ts;
                        else
                            minTimestamp = Math.Min(minTimestamp, ts);

                        this.candles.Insert(index, candle);
                        index++;
                        //this.candles.Add(candle);
                    }

                    #region 移除尾部超出容量的K线
                    while(candles.Count > CandleCapacity)
                    {
                        var last = candles.Count - 1;
                        pool.Put(candles[last]);
                        candles.RemoveAt(last);
                    }
                    #endregion

                    var api = this.api as CandleList;
                    api.before = maxTimestamp > 0 ? maxTimestamp.ToString() : "";//更新API的最后K线时间
                }

                if (!loaded)
                {
                    this.CandleLoaded?.Invoke(this,null);
                    loaded = true;
                }
                this.Feed();
            }
        }
    }
}
