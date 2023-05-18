using CoinTrader.Common;
using CoinTrader.Common.Extend;
using CoinTrader.Common.Util;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{
 
    /// <summary>
    /// 持仓
    /// </summary>
    public class Position
    {
        public Position()
        {

        }

        public string InstId { get; set; }


        string instName = "";
        public string InstName
        {
            get
            {
                if(string.IsNullOrEmpty(instName))
                {
                    instName = this.InstId.Replace("-SWAP","");
                }

                return instName;
            }
        }

        public string InstType { get; set; }

        /// <summary>
        /// 杠杆倍数
        /// </summary>
        public float Lever { get; set; }

        /// <summary>
        /// 保证金模式
        ///cross：全仓
        ///isolated：逐仓
        /// </summary>
        public string MgnMode { get; set; }

        public string MarginModeName
        {
            get
            {
                return this.MgnMode == MarginMode.Isolated ? "逐仓" : "全仓";
            }
        }

        public long PosId { get; set; }

        /// <summary>
        /// 持仓数量
        /// </summary>
        public decimal Pos { get; set; }

        public string PosSideName { get; set; }

        /// <summary>
        /// 持仓方向
        /// </summary>
        public string PosSide { get; set; }

        public PositionType SideType
        {
            get
            {
                return this.PosSide == PositionSide.Long? PositionType.Long: PositionType.Short;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UTime { get; set; }

        public decimal BaseBal { get; set; }

        public decimal QuoteBal { get; set; }

       public string PosCcy { get; set; }

        /// <summary>
        /// 可平仓数量
        /// </summary>
        public decimal AvailPos { get; set; }

        /// <summary>
        /// 开仓均价
        /// </summary>
        public decimal AvgPx { get; set; }

        /// <summary>
        /// 未实现收益
        /// </summary>
        public decimal Upl { get; set; }

        /// <summary>
        /// 未实现收益率
        /// </summary>
        public double UplRatio { get; set; }

        /// <summary>
        /// 预估强平价
        /// </summary>
        public decimal LiqPx { get; set; }

        /// <summary>
        /// 标记价格
        /// </summary>
        public decimal MarkPx { get; set; }

        /// <summary>
        /// 保证金余额
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// 保证金率
        /// </summary>
        public double MgnRatio { get; set; }

        /// <summary>
        /// 维持保证金
        /// </summary>
        public decimal MMR { get; set; }

        /// <summary>
        /// 利息
        /// </summary>
        public decimal Interest { get; set; }

        /// <summary>
        /// 最新成交ID
        /// </summary>
        public long TradeId { get; set; }

        /// <summary>
        /// 信号区
        ///分为5档，从1到5，数字越小代表adl强度越弱
        /// </summary>
        public int Adl { get; set; }

        /// <summary>
        /// 占用保证金币种
        /// </summary>
        public string Ccy { get; set; }

        /// <summary>
        /// 最新成交价
        /// </summary>
        public decimal Last { get; set; }

 
        public void ParseFromJson(JToken json)
        {
            long oldId = this.PosId;
            this.PosId = json.Value<long>("posId");

            if(oldId != this.PosId)
            {
                this.InstId = json.Value<string>("instId");
                this.instName = this.InstId.Replace("-SWAP", "");            
                this.PosSide = json.Value<string>("posSide");
                this.Ccy = json.Value<string>("ccy");
                this.InstType = json.Value<string>("instType");
                this.CTime = DateUtil.TimestampMSToDateTime( json.Value<long>("cTime"));
                this.PosSideName = PosSide == "short" ? "空头" : "多头";            
                this.MgnMode = json.Value<string>("mgnMode");
                this.PosCcy = json.Value<string>("posCcy");
            }
            
            
            this.Lever = json.Value<float>("lever");
            
            this.Pos   = json.Value<decimal>("pos");
            this.UTime = DateUtil.TimestampMSToDateTime(json.Value<long>("uTime"));
            //this.BaseBal = json.Value<decimal>("baseBal");
            //this.QuoteBal = json.Value<decimal>("quoteBal");
            ///this.PosCcy = json.Value<string>("posCcy");
            ///
            this.AvailPos = json.Value<decimal>("availPos");
            this.AvgPx = json.Value<decimal>("avgPx");
            this.Upl = json.Value<decimal>("upl");
            this.UplRatio = json.Value<double>("uplRatio");

            this.LiqPx = json.ValueWithDefault<decimal>("liqPx");
            this.MarkPx =  json.ValueWithDefault<decimal>("markPx");

            
            this.Margin = json.ValueWithDefault<decimal>("margin");
            this.MgnRatio = json.ValueWithDefault<double>("mgnRatio");
            this.Interest = json.ValueWithDefault<decimal>("interest");
            this.MMR = json.ValueWithDefault<decimal>("mmr");
            this.TradeId = json.ValueWithDefault<long>("tradeId");
            this.Adl = json.ValueWithDefault<int>("adl");
            this.Last = json.ValueWithDefault<decimal>("last");
            
            //this.UsdPx = json.Value<decimal>("usdPx");
        }
    }
}

/*
 "adl":"1",
        "availPos":"1",
        "avgPx":"2566.31",
        "cTime":"1619507758793",
        "ccy":"ETH",
        "deltaBS":"",
        "deltaPA":"",
        "gammaBS":"",
        "gammaPA":"",
        "imr":"",
        "instId":"ETH-USD-210430",
        "instType":"FUTURES",
        "interest":"0",
        "last":"2566.22",
        "usdPx":"",
        "lever":"10",
        "liab":"",
        "liabCcy":"",
        "liqPx":"2352.8496681818233",
        "markPx":"2353.849",
        "margin":"0.0003896645377994",
        "mgnMode":"isolated",
        "mgnRatio":"11.731726509588816",
        "mmr":"0.0000311811092368",
        "notionalUsd":"2276.2546609009605",
        "optVal":"",
        "pTime":"1619507761462",
        "pos":"1",
        "posCcy":"",
        "posId":"307173036051017730",
        "posSide":"long",
        "thetaBS":"",
        "thetaPA":"",
        "tradeId":"109844",
        "quoteBal": "0",
        "baseBal": "0",
        "uTime":"1619507761462",
        "upl":"-0.0000009932766034",
        "uplRatio":"-0.0025490556801078",
        "vegaBS":"",
        "vegaPA":"" 
 * */

