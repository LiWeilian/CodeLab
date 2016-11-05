using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class MyBLL
    {
        private MyDAL m_dal;
        private string m_remark;
        private static int m_remark2 = 0;

        public MyBLL()
        {
            this.m_remark = DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        public bool InsertTestData(out string errMsg)
        {
            errMsg = string.Empty;
            /*
            if (this.m_dal == null)
            {
                this.m_dal = new MyDAL("orcl", "", "appuser", "APPUSER");

                if (!this.m_dal.CreateConnection(out errMsg))
                {
                    this.m_dal = null;
                    return false;
                }
            }



            for (int i = 0; i < 100; i++)
            {
                Cat cat = new Cat();
                cat.CreateDate = DateTime.Now;
                cat.Description = this.m_remark;
                cat.Name = "CAT_" + i.ToString();

                if (!this.m_dal.InsertData(cat, out errMsg))
                {
                    return false;
                }
                Thread.Sleep(10);
            }
            */

            Thread thread = new Thread(new ParameterizedThreadStart(InsertDataThread));
            DatabaseConnectionInfo connInfo = new DatabaseConnectionInfo();
            connInfo.ServerName = "orcl";
            connInfo.DatabaseName = "";
            connInfo.UserName = "appuser";
            connInfo.Password = "APPUSER";
            thread.Start((object)connInfo);

            return true;
        }

        private static void InsertDataThread(object connInfo)
        {
            DatabaseConnectionInfo dbConnInfo = (DatabaseConnectionInfo)connInfo;
            MyDAL dal = new MyDAL(dbConnInfo.ServerName, dbConnInfo.DatabaseName, 
                dbConnInfo.UserName, dbConnInfo.Password);

            string errMsg;
            if (!dal.CreateConnection(out errMsg))
            {
                return;
            }

            //string remark = DateTime.Now.ToString("yyyyMMddhhmmss");
            m_remark2++;
            string remark = m_remark2.ToString();
            string logFile = Application.StartupPath + remark + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";

            for (int i = 0; i < 100; i++)
            {
                Cat cat = new Cat();
                cat.CreateDate = DateTime.Now;
                cat.Description = remark;
                cat.Name = "CAT_" + i.ToString();

                if (!dal.InsertData(cat, out errMsg))
                {
                    LogMessage(logFile, errMsg);
                }
                Thread.Sleep(50);
            }
        }

        private static void LogMessage(string logfile, string logMsg)
        {
            FileStream fs = File.Exists(logfile) ?
                new FileStream(logfile, FileMode.Append, FileAccess.Write) :
                new FileStream(logfile, FileMode.Create, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sw.WriteLine(logMsg);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }

    class DatabaseConnectionInfo
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
