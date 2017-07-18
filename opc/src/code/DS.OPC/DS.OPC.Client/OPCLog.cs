using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DS.OPC.Client
{
    class OPCLog
    {
        public enum LogLevel
        {
            Fatal,
            Error,
            Warn,
            Info,
            Debug
        }

        public static LogLevel ConfigLogLevel { get; set; }
        private static string m_logFileName = string.Empty;


        public static void LogServiceMessage(string msg, LogLevel logLevel)
        {
            //ConfigLogLevel = GetConfigLogLevel();
            if (logLevel > ConfigLogLevel)
            {
                return;
            }
            try
            {
                //Console.WriteLine(msg);
                if (m_logFileName == string.Empty)
                {
                    string dir = string.Format("{0}\\..\\log", AppDomain.CurrentDomain.BaseDirectory);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    if (Directory.Exists(dir))
                    {
                        m_logFileName = string.Format("{0}\\Service_{1}.log", dir,
                            DateTime.Now.ToString("yyyyMMddhhmmss"));
                    }
                }

                if (m_logFileName == string.Empty)
                {
                    return;
                }
                else
                {
                    FileStream fs;
                    if (File.Exists(m_logFileName))
                    {
                        fs = new FileStream(m_logFileName, FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        fs = new FileStream(m_logFileName, FileMode.Create, FileAccess.Write);
                    }

                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(string.Format("{0}\r\n日志级别：{1}\r\n{2}\r\n", DateTime.Now, logLevel, msg));
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
            catch
            {

            }
        }

        public static void Fatal(string msg)
        {
            LogServiceMessage(msg, LogLevel.Fatal);
        }

        public static void Error(string msg)
        {
            LogServiceMessage(msg, LogLevel.Error);
        }

        public static void Warn(string msg)
        {
            LogServiceMessage(msg, LogLevel.Warn);
        }

        public static void Info(string msg)
        {
            LogServiceMessage(msg, LogLevel.Info);
        }

        public static void Debug(string msg)
        {
            LogServiceMessage(msg, LogLevel.Debug);
        }

        public static LogLevel TranslateLogLevel(string logLevel)
        {
            switch (logLevel.ToUpper().Trim())
            {
                case "FATAL":
                    return LogLevel.Fatal;
                case "ERROR":
                    return LogLevel.Error;
                case "WARN":
                    return LogLevel.Warn;
                case "INFO":
                    return LogLevel.Info;
                case "DEBUG":
                    return LogLevel.Debug;
                default:
                    return LogLevel.Info;
            }
        }
    }
}
