
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.REST;

namespace CoinTrader.OKXCore.Manager
{
    public class AccountManager
    {
        private static Account _current;
        public static Account Current
        {
            get
            {
                return _current;
            }

            private set
            {
                _current = value;

                if(_current.AcctLv > 1) //带合约的账户
                {
                    PositionManager.Instance.StartUpdate();//开始更新合约账户的持仓
                }
                else
                {
                    PositionManager.Instance.EndUpdate();//不带合约， 关闭合约更新
                }

                TradeHistoryManager.Instance.SetAccount(_current.UId.ToString());
            }
        }

        /// <summary>
        /// 是否是模拟账户
        /// </summary>
        public static bool IsSimulated => APIStorage.APIInfo.IsSimulated;

        /// <summary>
        /// 设置杠杆倍数
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="posSide">方向</param>
        /// <param name="mode">保证金模式</param>
        /// <param name="lever">倍数</param>

        public static void SetLever(string instId, PositionType posSide, SwapMarginMode mode, uint lever)
        {

            string strSide = PositionSide.Long;

            switch (posSide)
            {
                case PositionType.Long:
                    strSide = PositionSide.Long;
                    break;
                case PositionType.Short:
                    strSide = PositionSide.Short;
                    break;
            }

            string strMode = MarginMode.Isolated;

            switch (mode)
            {
                case SwapMarginMode.Isolated:
                    strMode = CoinTrader.OKXCore.MarginMode.Isolated;
                    break;
                case SwapMarginMode.Cross:
                    strMode = CoinTrader.OKXCore.MarginMode.Cross;
                    break;
            }

            SetLever(instId, strSide, strMode, lever);
        }
 

        /// <summary>
        /// 设置杠杆倍数
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="posSide"></param>
        /// <param name="lever"></param>
        /// <param name="mode"></param>
        public static async void SetLever(string instId, string posSide,string mode, uint lever)
        {
            var api = new SetLever();

            api.instId = instId;
            api.lever = lever.ToString();
            if(mode == CoinTrader.OKXCore.MarginMode.Isolated)
                api.posSide = posSide;
            api.mgnMode = string.Compare( mode, CoinTrader.OKXCore.MarginMode.Cross) == 0 ? CoinTrader.OKXCore.MarginMode.Cross : CoinTrader.OKXCore.MarginMode.Isolated;
            var result = await  api.Exec();
        }

        public static Account GetAccountByAPI(string secretKey, string apiKey,string passphrase,bool isSimulated)
        {
            var accountApi = new AccountConfig();

            accountApi.SetAPI(secretKey, apiKey, passphrase, isSimulated);
            var result = accountApi.ExecSync();

            if (result.code == 0)
            {
                JArray arr = result.data as JArray;
                var account = new Account();
                account.ParseFromJson(arr[0]);
                return account;
            }

            return null;
        }

        public static void UpdateAccount(Account account)
        {
            Current = account;
        }

        public static bool UpdateAccount()
        {
            var accountApi = new AccountConfig();
            var result = accountApi.ExecSync();

            if (result.code == 0)
            {
                JArray arr = result.data as JArray;
                var account = new Account();
                account.ParseFromJson(arr[0]);
                Current = account;
            }

            return result.code == 0;
        }
    }
}
