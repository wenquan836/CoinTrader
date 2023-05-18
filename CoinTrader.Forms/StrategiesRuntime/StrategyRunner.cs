using CefSharp.DevTools.Database;
using CoinTrader.Forms.Strategies;
using CoinTrader.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Forms.StrategiesRuntime
{
    
    internal class StrategyRunner
    {
       private readonly Dictionary<string,List<StrategyBase>> runningStrategy = new Dictionary<string, List<StrategyBase>>();

        private StrategyRunner()
        {

        }

        /// <summary>
        /// 运行策略
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="group"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool RunStrategy(string instId,StrategyGroup group, bool runImmediately, out string  error)
        {
            error = string.Empty;

            var list = new List<StrategyBase>();

            foreach (var g in group.strategies)
            {
                try
                {
                    var strategy = Activator.CreateInstance(g) as StrategyBase;
                    list.Add(strategy);
                }
                catch(Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }

            StopStrategiesByInstId(instId);

            runningStrategy[instId] = list;

            foreach(var s in list)
            {
                try
                {
                    s.Init(instId);
                }
                catch(Exception ex)
                {
                    error = ex.Message;
                    break;
                }

                if (runImmediately)
                    s.Enable = true;
            }

            if(!string.IsNullOrEmpty(error))
            {
                StopStrategiesByInstId(instId);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 停止策略在指定的instId上
        /// </summary>
        /// <param name="instId"></param>
        public void StopStrategiesByInstId(string instId)
        {
            if (runningStrategy.TryGetValue(instId, out var list))
            {
                runningStrategy.Remove(instId);
                //Task.Run(() =>
                {
                    foreach (var g in list)
                    {
                        g.Dispose();
                    }
                }//);
            }
        }

        /// <summary>
        /// 停止所有
        /// </summary>
        public void StopAll()
        {
            var keys = new List<string>( this.runningStrategy.Keys);
            
            foreach(var key in keys)
            {
                this.StopStrategiesByInstId(key);
            }

            runningStrategy.Clear();
        }

        /// <summary>
        /// 返回当前正在执行的策略
        /// </summary>
        /// <param name="instId"></param>
        /// <returns></returns>
        public List<StrategyBase> GetStrategiesByInstId(string instId)
        {
            var list = new List<StrategyBase>();

            if(runningStrategy.TryGetValue(instId, out var strategy))
            {
                list.AddRange(strategy);
            }

            return list;
        }
 
        public static StrategyRunner Instance = new StrategyRunner ();
    }
}
