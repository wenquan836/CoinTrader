using System;
using System.IO;

namespace CoinTrader.Common
{
    public enum LogType
    {
        Info = 1,
        Error = 2,
        Debug = 3,
        Exception = 4
    }

    public class Log
    {
        public LogType Type;
        public string Content;
        public DateTime Time;

        public Log(LogType type,string content)
        {
            Type = type;
            Content = content != null ? content : string.Empty;
            Time = DateTime.Now;
        }

        public override string ToString()
        {
            string tp = "";
            switch(Type)
            {
                case LogType.Error:
                    tp = "Error";
                    break;
                case LogType.Info:
                    tp = " Info";
                    break;
                case LogType.Debug:
                    tp = "Debug";
                    break;
                case LogType.Exception:
                    tp = "Exception";
                    break;
            }
            
            return string.Format("[{0}] {1}: {2}", Time.ToString("G"), tp, Content);
        }
    }
    public class Logger
    {
        StreamWriter writer = null;
        static Logger _instance = null;
        public Action<Log> NewLog = null;
        string filePath = "";
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (filePath == value)
                    return;

                if(writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }

                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        writer = new StreamWriter(value, true);
                        writer.AutoFlush = true;
                    }
                    catch(Exception ex)
                    {
                        Log(LogType.Error, string.Format("无法创建或打开日志文件{0}: {1}", value, ex.Message));
                    }
                }

                filePath = value;
            }
        }
 
        private void WriteToFile(Log l)
        {
            if (writer != null)
            {
                try
                {
                    writer.WriteLine(l.ToString());
                }
                catch
                {
                   
                }
            }
        }
        public void Close()
        {
            if(writer == null)
            {
                var w = writer;
                writer = null;
                w.Close();
                w.Dispose();
            }
        }

        public void LogError(string log)
        {
            this.Log(LogType.Error, log);
        }

        public void LogException(Exception ex)
        {
            this.Log(LogType.Exception, string.Format("{0}\r\n{1}", ex.StackTrace, ex.Message));
        }

        public void LogInfo(string log)
        {
            this.Log(LogType.Info, log);
        }

        public void LogDebug(string content)
        {
            this.Log(LogType.Debug,  content);
        }

        public void Log(LogType type, string content )
        {
            Log l = new Log(type, content);

            if ((type == LogType.Error || type == LogType.Exception || type == LogType.Debug) && !string.IsNullOrEmpty(this.FilePath))
            {
                this.WriteToFile(l);
            }

            NewLog?.Invoke(l);
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }

                return _instance;
            }
        }
    }
}
