
using System;

namespace CoinTrader.Common.Util
{
    public static class DateUtil
    {
        private static readonly DateTime DATE_TIME_1970 = new DateTime(1970, 1, 1);

        /// <summary>
        /// 服务器时间和本地时间的偏移量，需要校时
        /// </summary>
        private static long serverTimeOffset = 0;
        public static string DateTime2ISO8601(DateTime time)
        {
            return time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static string GetServerTimeISO8601()
        {
            DateTime now = DateTime.Now;
            now = now.AddMilliseconds(serverTimeOffset);

            return DateTime2ISO8601(now);
        }

        public static string GetServerUTCTimeISO8601()
        {
            DateTime now = GetServerUTCDateTime();
            return DateTime2ISO8601(now);
        }

        public static DateTime GetServerUTCDateTime()
        {
            DateTime now = DateTime.UtcNow;
            now = now.AddMilliseconds(serverTimeOffset);
            return now;
        }

        /// <summary>
        /// 更新服务器时间
        /// </summary>
        /// <param name="timestampMS"></param>
        public static void SetServerTimestamp(long timestampMS)
        {
            var local = GetTimestampMS();
            serverTimeOffset = timestampMS - local;
        }
        public static long GetServerUTCTimestampSec()
        {
            long local = GetServerUTCTimestampMS();
            return local / 1000;
        }
        public static long GetServerUTCTimestampMS()
        {
            DateTime utc = DateTime.UtcNow;

            long local = GetTimestampMS(utc);
            return local + serverTimeOffset;
        }
        public static DateTime GetServerDateTime()
        {
            DateTime now = DateTime.Now;
            now = now.AddMilliseconds(serverTimeOffset);

            return now;
        }

        public static long GetServerTimestampMS()
        {
            long local = GetTimestampMS();
            return local + serverTimeOffset;
        }

        public static long GetServerTimestampSec()
        {
            long ms = GetServerTimestampMS();
            return ms / 1000;
        }

        /// <summary>
        /// 返回当前毫秒时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimestampMS()
        {
            return GetTimestampMS(DateTime.Now);
        }

        /// <summary>
        /// 返回毫秒时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimestampMS(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DATE_TIME_1970); // 当地时区
            return (long)(time - startTime).TotalMilliseconds; // 相差毫秒数
        }

        /// <summary>
        /// 返回秒时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimestampSec(DateTime time)
        {
            return GetTimestampMS(time) / 1000;
        }

        public static DateTime TimestampSecToDateTime(long timestamp)
        {
            return DATE_TIME_1970.AddSeconds(timestamp);
        }

        public static DateTime TimestampMSToDateTime(long timestamp)
        {
            return DATE_TIME_1970.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// 返回秒时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimestampSec()
        {
            return GetTimestampSec(DateTime.Now);
        }

        public static DateTime UtcToLocalTime(DateTime utcTime)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(utcTime);
        }

        public static DateTime LocalTimeToUtc(DateTime localTime)
        {
            return TimeZone.CurrentTimeZone.ToUniversalTime(localTime);
        }
    }
}
