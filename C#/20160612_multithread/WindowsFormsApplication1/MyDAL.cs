using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace WindowsFormsApplication1
{
    class MyDAL
    {
        private SQLHelper m_sqlHelper;
        private string m_server;
        private string m_database;
        private string m_userName;
        private string m_password;

        public MyDAL(string serverName, string dbName, string userName, string password)
        {
            this.m_server = serverName;
            this.m_database = dbName;
            this.m_userName = userName;
            this.m_password = password;
        }

        public bool CreateConnection(out string errMsg)
        {
            OracleConnectionStringBuilder oraConnBuilder = new OracleConnectionStringBuilder();
            oraConnBuilder.DataSource = this.m_server;
            oraConnBuilder.UserID = this.m_userName;
            oraConnBuilder.Password = this.m_password;
            this.m_sqlHelper = new SQLHelper(oraConnBuilder.ConnectionString, out errMsg);
            try
            {
                this.m_sqlHelper.Connected = true;
                this.m_sqlHelper.Connected = false;
                return true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }

        public bool InsertData(Cat cat, out string errMsg)
        {
            this.m_sqlHelper.Connected = true;
            if (!this.m_sqlHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            string insertSQL = string.Format("INSERT INTO TB_CAT(ID,CREATEDATE,DESCRIPTION,NAME) VALUES(COMMON_SEQ.NEXTVAL, TO_DATE('{0}', 'yyyy-mm-dd hh24:mi:ss'), '{1}', '{2}')", 
                cat.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"), cat.Description, cat.Name);
            return this.m_sqlHelper.ExecuteNonQuery(insertSQL, out errMsg);
        }
    }
}
