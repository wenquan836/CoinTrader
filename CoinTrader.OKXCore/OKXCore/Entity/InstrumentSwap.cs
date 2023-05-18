using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{
    public class InstrumentSwap : InstrumentBase
    {

        /// <summary>
        /// 
        /// </summary>
        public string SettleCcy { get; private set; }


        /// <summary>
        /// 面值币种
        /// </summary>
        public string CtValCcy { get; private set; }

        /// <summary>
        /// 合约面值
        /// </summary>
        public decimal CtVal { get; private set; }
        /// <summary>
        /// 合约乘数
        /// </summary>
        public decimal CtMult { get; private set; }

        /// <summary>
        /// 标的指数
        /// </summary>
        public string ULY { get; private set; }

        /// <summary>
        /// 最高杠杆倍数
        /// </summary>
        public uint Lever { get; private set; }

        /// <summary>
        /// linear：正向合约
        ///inverse：反向合约
        /// </summary>
        public string CtType { get; private set; }

        public override string AmountFormat
        {
            get
            {
                if (_amountFormat == null)
                    _amountFormat = CtVal >= 1 ? "0" : (new Regex("[^0\\\\.]")).Replace(this.CtVal.ToString(), "0");// .Replace("1", "0");

                return _amountFormat;
            }
        }
        public override void ParseFromJson(JToken data)
        {
            this.SettleCcy = data.Value<string>("settleCcy");
            this.CtValCcy = data.Value<string>("ctValCcy");
            this.Lever = data.Value<uint>("lever");
            this.CtType = data.Value<string>("ctType");
            this.CtVal = data.Value<decimal>("ctVal");
            this.CtMult = data.Value<decimal>("ctMult");
            this.ULY = data.Value<string>("uly");

            base.ParseFromJson(data);
        }
    }
}
