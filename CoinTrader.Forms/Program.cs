using CefSharp;
using CefSharp.WinForms;
using CoinTrader.Forms.Command;
using System;
using System.Net;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Initialize();
            Coroutine.Init();
            WindowManager.Instance.OpenWindow<WinStartup>();
            //WindowManager.Instance.OpenWindow<WebDashboard>();
            //WindowManager.Instance.OpenWindow<WinGATest>();

            Application.Run();
        }

        private static void InitCefSharp()
        {
            var settings = new CefSettings
            {
                Locale = "zh-CN",
                AcceptLanguageList = "zh-CN,zh;q=0.9,en;q=0.8",
                CachePath = AppDomain.CurrentDomain.BaseDirectory + "chromium\\caches\\",
                PersistSessionCookies = true,
                PersistUserPreferences = true,
            };
         
            CefSharpSettings.WcfEnabled = true; //js互相调用需要这个设置为true
            Cef.Initialize(settings);
        }

        /**
         * 初始化
         */
        private static void Initialize()
        {
            InitCefSharp();
            ServicePointManager.DefaultConnectionLimit = 1024;//设置最大并发连接数
            CommandRegister.RegisterAllCommand();
        }
    }
}
