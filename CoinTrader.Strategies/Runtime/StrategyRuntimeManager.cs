using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Strategies.Runtime
{
    public sealed class StrategyRuntimeManager
    {
        private class RuntimeItem
        {
            public ITradeStrategyRuntime runtime;
            public int referenceCount;
        }

        Dictionary<string, RuntimeItem> runtimeCache = new Dictionary<string, RuntimeItem>();
        private StrategyRuntimeManager() { }


        private string GenerateKey(string instId, bool emulation)
        {
            return $"{instId}_runtime" + (emulation ? "_emulation" : "");
        }

        private ITradeStrategyRuntime CreateRuntime(string instId, bool forEmulation)
        {
            ITradeStrategyRuntime runtime = null;

            bool isSwap = instId.EndsWith("-swap", StringComparison.OrdinalIgnoreCase);

            if (forEmulation)
            {
                if (isSwap)
                {
                   
                }
                else
                {
                    runtime = new StrategyEmulatorRuntime();
                }
            }
            else
            {
                if (isSwap)
                {
                    runtime = new SwapStrategyRuntime();
                }
                else
                {
                    runtime = new SpotStrategyRuntime();
                }
            }

            return runtime;
        }

        public ITradeStrategyRuntime GetRuntime(string instId, bool isEmulation)
        {
            string key = GenerateKey(instId, isEmulation);

            lock(this)
            {
                if(runtimeCache.TryGetValue(key,out var runtimeItem))
                {
                    runtimeItem.referenceCount++;
                    return runtimeItem.runtime;
                }

                var runtime = CreateRuntime(instId, isEmulation);

                if (runtime.Init(instId))
                {
                    runtimeItem = new RuntimeItem();
                    runtimeItem.referenceCount = 1;
                    runtimeItem.runtime = runtime;
                    runtimeCache.Add(key, runtimeItem);
                    return runtime;
                }
            }

            return null;
        }

        public void ReleaseRuntime(ITradeStrategyRuntime runtime)
        {
            if(runtime == null)
                throw new ArgumentNullException("runtime");

            var key = GenerateKey(runtime.InstId,runtime.IsEmulator);

            lock(this)
            {
                if (runtimeCache.TryGetValue(key, out var runtimeItem))
                {
                    runtimeItem.referenceCount--;
                    if (runtimeItem.referenceCount <= 0)
                    {
                        runtimeItem.runtime.Dispose();
                        runtimeCache.Remove(key);
                    }
                }
            }
        }

        private static StrategyRuntimeManager instance;
        private static object lockObj = new object();
        public static StrategyRuntimeManager Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(lockObj)
                    {
                        if(instance == null)
                            instance = new StrategyRuntimeManager();

                    }
                }

                return instance;
            }
        }
    }
}
