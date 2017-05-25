using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GDDST.DI.Utils
{
    public class ServiceLog
    {
        private static string m_logFileName = string.Empty;

        public static void LogServiceMessage(string msg)
        {
            if (m_logFileName == string.Empty)
            {
                string dir = string.Format("{0}\\log", AppDomain.CurrentDomain.BaseDirectory);
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
                sw.WriteLine(string.Format("{0}\r\n{1}\r\n", DateTime.Now, msg));
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }
    }
}
