using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    public static class APIUrl
    {
        public static readonly string UrlRoot = "https://www.okx.com";
        public static readonly string Account = UrlRoot + "/api/v5/account/config";

        public static readonly string AsstetsBalance = UrlRoot + "/api/v5/asset/balances";//资金账户余额
        public static readonly string Instruments = UrlRoot + "/api/v5/public/instruments"; //获取可交易币对的列表信息 instType=SPOT代表币币
  
        //  /v3/c2c/tradingOrders/books?quoteCurrency=cny&baseCurrency={0}&side=all&paymentMethod=all&userType=all&showTrade=false&receivingAds=false&showFollow=false&showAlreadyTraded=false&isAbleFilter=false
        public static readonly string FundingRate = UrlRoot + "/api/v5/public/funding-rate";//永续合约费率查询
                                                                                                      // public static readonly string CTCAmount = UrlRoot + "/api/v5/account/balance?ccy={0}";//获取账号余额  
        public static readonly string TradingBalance = UrlRoot + "/api/v5/account/balance";//获取交易账户余额  
        public static readonly string TimeService = UrlRoot + "/api/v5/public/time";
        public static readonly string Candle = UrlRoot + "/api/v5/market/candles";
        public static readonly string HistoryCandle = UrlRoot + "/api/v5/market/history-candles";//历史走势数据
        public static readonly string Transfer = UrlRoot + "/api/v5/asset/transfer"; //资金划转
        public static readonly string StrategyEntrust = UrlRoot + "/api/v5/trade/order-algo";//策略委托下单 
        public static readonly string CancelStrategyEntrust = UrlRoot + "/api/v5/trade/cancel-algos"; //撤销策略委托
        public static readonly string SetLever = UrlRoot + "/api/v5/account/set-leverage";//设置杠杆倍数
        public static readonly string Orders = UrlRoot + "/api/v5/trade/orders-pending";

        /// <summary>
        /// 查询持仓
        /// </summary>
        public static readonly string Position = UrlRoot + "/api/v5/account/positions";

        /// <summary>
        /// 调整保证金
        /// </summary>
        public static readonly string MarginBalance = UrlRoot + "/api/v5/account/position/margin-balance";
        public static readonly string OrderCreate = UrlRoot + "/api/v5/trade/order";//创建订单
        public static readonly string OrderModify = UrlRoot + "/api/v5/trade/amend-order"; //修改挂掉
        public static readonly string OrderQuery = OrderCreate;
        public static readonly string OrderCancel = UrlRoot + "/api/v5/trade/cancel-order"; //{0} order id
        public static readonly string BatchOrdersCreate = UrlRoot + "/api/v5/trade/batch-orders"; //批量创建订单
        public static readonly string BatchOrdersModify = UrlRoot + "/api/v5/trade/amend-batch-orders"; //批量修改订单
        public static readonly string BatchOrdersCancel = UrlRoot + "/api/v5/trade/cancel-batch-orders";//批量删除订单

        public static readonly string History = UrlRoot + "/api/v5/trade/orders-history-archive";//历史订单

        //public static readonly string CTCDepth = "/api/spot/v3/instruments/{0}-{1}/book?size={2}&depth={3}"; //币对的深度信息 
    }
}
