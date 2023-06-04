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
            
            WindowManager.Instance.OpenWindow<WinStartup>();

            Application.Run();
        }

        /// <summary>
        /// 内置浏览器内核初始化
        /// </summary>
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

        /// <summary>
        /// 程序初始化
        /// </summary>
        private static void Initialize()
        {
            InitCefSharp(); //浏览器内核初始化
            ServicePointManager.DefaultConnectionLimit = 1024;//设置最大并发连接数
            Coroutine.Init(); //协助程序初始化
            CommandRegister.RegisterAllCommand();
        }
    }
}
