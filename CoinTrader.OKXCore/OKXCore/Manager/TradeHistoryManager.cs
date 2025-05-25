using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.VO;
using CoinTrader.Common.Database;
using CoinTrader.Common.Util;
using CoinTrader.Common.Classes;

namespace CoinTrader.OKXCore.Manager
{
    /// <summary>
    /// 历史记录查询
    /// </summary>
    public class TradeHistoryManager
    {
        private SqlDao dao = null;
        const int PageSize = 50;

        HashSet<string> LoadingTable = new HashSet<string>();
        Dictionary<string, long> maxIdTable = new Dictionary<string, long>();

        public bool IsLoading(string instId)
        {
            return this.LoadingTable.Contains(instId);
        }

        private string tableName = "";

        /// <summary>
        /// 
        /// </summary>
        public long GetMaxHistoryID(string key)
        {
            return this.maxIdTable.ContainsKey(key) ? this.maxIdTable[key] : 0;
        }

        private static readonly string CreateHistoryTableScript = @"CREATE TABLE [{0}] (
order_id BIGINT     PRIMARY KEY,
client_oid          STRING NOT NULL,
price               DECIMAL NOT NULL,
size                DECIMAL NOT NULL,
notional            DECIMAL NOT NULL,
instrument_id       STRING   NOT NULL,
inst_type           STRING   NOT NULL,
type STRING         NOT NULL,
side STRING         NOT NULL,
timestamp DATETIME  NOT NULL,
filled_size         DECIMAL NOT NULL,
filled_notional     DECIMAL NOT NULL,
order_type          INT NOT NULL,
state               INT NOT NULL,
price_avg           DECIMAL NOT NULL,
fee                 DECIMAL NOT NULL,
fee_currency        STRING NOT NULL,
update_time         DATETIME
);
";

        private TradeHistoryManager()
        {
          
        }

  

        public string DatabasePath
        {
            get;set;
        }
        public void SetAccount(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                tableName = "history_Default";// "DefaultUser";
            }
            else
            {
                tableName = "history_" + loginName;
            }

            dao = SqlDao.Instance;
            dao.DatabasePath = this.GetDatabasePath();

            if (!dao.TableIsExist(tableName))
            {
                dao.ExecuteNonQuery(string.Format(CreateHistoryTableScript, tableName));
            }

            this.UpdateMaxHistoryOrderID();
        }

        public List<TradeOrder> GetHistoryOrders(string instId, DateTime startTime,DateTime endTime)
        {
            startTime = startTime.ToUniversalTime();
            endTime = endTime.ToUniversalTime();
            
            List<TradeOrder> result = new List<TradeOrder>();
            var data = SqlDao.Instance.ExecuteDatatable( string.Format("select * from {0} where instrument_id = '{1}' and timestamp >= '{2}' and timestamp < '{3}' order by timestamp desc", tableName,instId, startTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss")));

            var pool = Pool<TradeOrder>.GetPool();
            foreach(DataRow row in data.Rows)
            {
                TradeOrder order = pool.Get();
                order.ParseFromDataRow(row);
                result.Add(order);
            }

            return result;
        }

        bool ItemIsExist(long id)
        {
            string sql = string.Format("select count(*) from {0} where order_id = {1}", tableName, id);
            return dao.ExecuteScalar<long>(sql) > 0;
        }

        private long GetLastIdByDate(DateTime date)
        {
            string sql = string.Format("select max(order_id) from {0} where timestamp < '{1}'", tableName, date.ToString("yyyy-MM-dd"));
            return dao.ExecuteScalar<long>(sql);
        }

        /// <summary>
        /// 重新同步最新的三个月现货数据, 完全同步， 解决历史记录缺失问题
        /// </summary>
        public Task<HistoryLoadResult> ResyncSwapHistory(string instId, int days)
        {
            return this.ResyncHistory(instId, InstrumentType.Swap, days);
        }

        /// <summary>
        /// 重新同步最新的三个月现货数据, 完全同步， 解决历史记录缺失问题
        /// </summary>
        public Task<HistoryLoadResult> ResyncSpotHistory(string instId,int days)
        {
            return this.ResyncHistory(instId, InstrumentType.Spot, days);
        }

        public Task<HistoryLoadResult> ResyncHistory(string instId, string instType, int days)
        {
            string key = instId;

            if (LoadingTable.Contains(key))
                return null;

            long lastID = 0;
            if (days > 0)
            {
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                date = date.AddDays(-days).ToUniversalTime();
                lastID = GetLastIdByDate(date);
            }

            return LoadFromLastID(instId, instType, lastID);
        }

        private Task<HistoryLoadResult> LoadFromLastID(string instId,string instType, long lastID)
        {
            
            LoadingTable.Add(instId);
            decimal sizeFactor = 1;

            if(instType == InstrumentType.Swap)
            {
                InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);
                
                if(instrument != null)
                    sizeFactor = instrument.CtVal;
            }

            return Task.Run<HistoryLoadResult>(() =>
            {
                HistoryLoadResult ret = new HistoryLoadResult();

                StringBuilder sb = new StringBuilder();

                while (true)
                {
                    HistoryRecord api = new HistoryRecord(instId, CTCHistoryOrderState.FullDeal, lastID, 100);
                    api.instType = instType;
                    var result = api.ExecSync();

                    if (result.code == 0)
                    {
                        JArray items = result.data as JArray;

                        bool hasRecord = false;

                        foreach (JObject jo in items)
                        {
                            long orderID = 0;
                            string sql = "";

                            if (this.CheckAndJsonToSql(jo, sizeFactor,out sql, out orderID))
                            {
                                if (!ItemIsExist(orderID))
                                {
                                    sb.Append(sql);
                                }

                                hasRecord = true;
                            }
                            
                            lastID = Math.Max(lastID, orderID);
                        }

                        if (!hasRecord)
                        {
                            break;
                        }


                        int loadThreadCount = this.LoadingTable.Count;

                        Thread.Sleep(RandomUtil.Range(loadThreadCount * 300, loadThreadCount * 400));
                    }
                    else
                    {
                        ret.Error = true;
                        ret.ErrorMessage = result.message;
                        break;
                    }
                }

                if (!ret.Error && sb.Length > 0)
                {
                    ret.RowCount = dao.ExecuteNonQuery(sb.ToString(), true);
                    this.UpdateMaxHistoryOrderID();
                }

                this.LoadingTable.Remove(instId);

                return ret;
            });
        }

        private bool CheckAndJsonToSql(JObject obj,decimal sizeFactor, out string sql,out long id)
        {
            string key_OrderID = "ordId";
            string key_ClientID = "clOrdId";
            string key_Size = "sz";
            string key_Price = "px";
            string key_InstrumentId = "instId";
            string key_FillSize = "accFillSz";
            string key_FeeCurrency = "feeCcy";
            string key_PriceAvg = "avgPx";
            string key_Fee = "fee";
            string key_Side = "side";
            string key_Type = "ordType";
            string key_instType = "instType";
            string key_lever = "lever";//杠杆倍数
            string state = "";
            string orderTime = "";
            string updateTime = "";
            sql = "";

            orderTime = DateUtil.TimestampMSToDateTime(obj["cTime"].Value<long>()).ToString("yyyy-MM-dd HH:mm:ss");
            updateTime = DateUtil.TimestampMSToDateTime(obj["uTime"].Value<long>()).ToString("yyyy-MM-dd HH:mm:ss");


            string val = obj["state"].Value<string>();

            state = "" + (int)CTCOrderState.Uncomplete;

            if (val == "canceled")
            {
                state = "" + (int)CTCOrderState.Canceled;
            }
            else if (val == "filled")
            {
                state = "" + (int)CTCOrderState.AllCompleted;
            }

            long orderID = obj[key_OrderID].Value<long>();

            id = orderID;
       

            decimal fillSize = obj.Value<decimal>(key_FillSize) * sizeFactor;
            decimal size = obj.Value<decimal>(key_Size) * sizeFactor;

            if (fillSize == 0)
            {
                return false;
            }

            sql = string.Format(@"insert into {0} (order_id,client_oid, price,size,notional,instrument_id,inst_type, type,side,timestamp,filled_size,filled_notional,order_type,state,price_avg,fee,fee_currency,update_time) values(
                            {1},'{2}',{3},{4},{5},'{6}','{7}','{8}','{9}','{10}',{11},{12},{13},{14},{15},{16},'{17}','{18}');"
            , tableName
            , obj[key_OrderID].Value<string>()
            , string.IsNullOrEmpty(obj[key_ClientID].Value<string>()) ? "" : obj[key_ClientID].Value<string>()
            , string.IsNullOrEmpty(obj[key_Price].Value<string>()) ? "0" : obj[key_Price].Value<string>()
            , size
            , "0" // string.IsNullOrEmpty(jo["notional"].Value<string>()) ? "0" : jo["notional"].Value<string>()
            , obj[key_InstrumentId].Value<string>()
            , obj[key_instType].Value<string>()
            , obj[key_Type].Value<string>()
            , obj[key_Side].Value<string>()
            , orderTime
            , fillSize
            , 0 // jo["filled_notional"].Value<string>()
            , "0"
            , state
            , obj[key_PriceAvg].Value<string>()
            , string.IsNullOrEmpty(obj[key_Fee].Value<string>()) ? "0" : obj[key_Fee].Value<string>()
            , obj[key_FeeCurrency].Value<string>()
            , updateTime
            );

            return true;
        }
            

        public Task<HistoryLoadResult> LoadHistory(string instId,string instType)
        {
            string key = instId;

            if (LoadingTable.Contains(key))
                return null;

            
            long lastID = GetMaxHistoryID(key);
            return LoadFromLastID(instId,instType, lastID);
        }

        public Task<HistoryLoadResult> LoadHistory(string instId)
        {
            return this.LoadHistory(instId, InstrumentType.Spot);
        }


        /// <summary>
        /// 获取所有有交易记录的数字货币类型
        /// </summary>
        /// <param name="side">买入或卖出</param>
        /// <returns></returns>
        public List<string> GetAllCurrencyTypes(string side)
        {
            string symbol = "symbol";
            string sql = "select distinct " + symbol + " from " + this.tableName + " where 1 = 1 ";

            if (side == Side.Buy || side == Side.Sell)
            {
                sql += "and type = '" + side + "'";
            }


            DataTable dt = dao.ExecuteDatatable(sql);
            List<string> currencies = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                currencies.Add(r[symbol].ToString());
            }

            return currencies;
        }

        public DataTable GetStatByMonth(string side, decimal totalDivide)
        {
            string sql = GenerateSql(false, side, totalDivide);

            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;

        }

        public DataTable GetStatByMonthWithCurrency(string side, string currency, decimal totalDivide)
        {
            string sql = this.GenerateSqlWithSymbol(currency, false, side, totalDivide);
            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
        }

        public DataTable QueryOrders(string side, DateTime startDate, DateTime endData, IList<string> currencies, string status, int pageIndex, int pageSize, out long count)
        {
            StringBuilder conditions = new StringBuilder(" where 1 = 1 ");


            if (!string.IsNullOrEmpty(side))
            {
                conditions.AppendFormat(" and type = '{0}'", side.Trim());
            }

            if (!string.IsNullOrEmpty(status))
            {
                conditions.AppendFormat(" and orderStatus = '{0}'", status.Trim());
            }

            if (currencies != null && currencies.Count > 0)
            {
                conditions.AppendFormat(" and symbol in('{0}')", string.Join("','", currencies));
            }

            conditions.AppendFormat(" and createdDate >='{0:yyyy-MM-dd HH:mm:ss}' and createdDate <'{1:yyyy-MM-dd HH:mm:ss}'", startDate, endData);
            string sqlForCount = string.Format("select count(*) from {0} {1}", tableName, conditions);
            count = dao.ExecuteScalar<long>(sqlForCount);
            pageIndex = Math.Min(pageIndex, (int)(count / pageSize) + ((count > 0 && count % pageSize > 0) ? 1 : 0));
            pageIndex = Math.Max(pageIndex, 0);
            string sql = string.Format("select * from {0} {1} order by createdDate desc limit {2},{3} ", tableName, conditions, pageIndex * pageSize, pageSize);


            return dao.ExecuteDatatable(sql);
        }

        public DataTable GetStatByDay(string side, decimal totalDivide)
        {
            string sql = GenerateSql(true, side, totalDivide);
            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
        }

        public DataTable GetStatByDayWithCurrency(string side, string currency, decimal totalDivide)
        {
            string sql = this.GenerateSqlWithSymbol(currency, true, side, totalDivide);
            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
        }


        private string GenerateSql(bool isDay, string side, decimal totalDivide)
        {
            string sql = string.Format("select type,orderStatus,strftime('{0}', createdDate) as 'time',  sum(orderTotal) /{1} as total from {2} group by type,orderStatus, time having type = '{3}' and orderStatus='{4}';"
                , isDay ? "%Y-%m-%d" : "%Y-%m"
                , totalDivide
                , tableName
                , side
                , OrderStatus.Completed
                );

            return sql;
        }

        private string GenerateSqlWithSymbol(string currency, bool isDay, string side, decimal totalDivide)
        {
            string sql = string.Format("select type,orderStatus,symbol,strftime('{0}', createdDate) as 'time',sum(amount) as amounts, sum(orderTotal)/{1} as total from {2} group by type,orderStatus,symbol, time having type = '{3}' and orderStatus='{4}' and symbol = '{5}';"
            , isDay ? "%Y-%m-%d" : "%Y-%m"
            , totalDivide
            , tableName
            , side
            , OrderStatus.Completed
            , currency
            );

            return sql;
        }

        public void UpdateMaxHistoryOrderID()
        {
            var res = dao.ExecuteDatatable( string.Format( "select instrument_id, max(order_id) as m from {0} group by instrument_id" , tableName));
            
            foreach(DataRow r in res.Rows)
            {
                var key = r["instrument_id"].ToString();
                var id = Convert.ToInt64(r["m"]);

                this.maxIdTable[key] = id;
            }
        }

        private string GetDatabasePath()
        {
            return Path.Combine(AppContext.BaseDirectory, "history.db");
            //return Path.Combine(Application.StartupPath, );
        }
        

        /// <summary>
        /// 获取指定时间之前的最后历史价格
        /// </summary>
        /// <param name="currency">币种</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public decimal GetPriceWithTime(string currency, DateTime time)
        {
            string sql = string.Format(@"select exchangeRate from {0} where symbol = ""{1}"" and type=""buy"" and createdDate < ""{2}"" order by publicOrderId desc limit 1 ", tableName, currency, time.ToString("yyyy-MM-dd HH:mm:ss"));

            return dao.ExecuteScalar<decimal>(sql);
        }

        private static TradeHistoryManager _instance = null;
        public static TradeHistoryManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TradeHistoryManager();

                return _instance;
            }
        }
    }
}
