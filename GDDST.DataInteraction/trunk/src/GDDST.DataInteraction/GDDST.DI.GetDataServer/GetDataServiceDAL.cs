using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace GDDST.DI.GetDataServer
{
    abstract class GetDataServiceDAL
    {
        public abstract bool CreateConnection(out string errMsg);
    }

    class GetDataServiceDAL_ORA : GetDataServiceDAL
    {
        public override bool CreateConnection(out string errMsg)
        {
            errMsg = string.Empty;
            return false;
        }
    }

    class GetDataServiceDAL_MSSQL : GetDataServiceDAL
    {
        private MSSQLHelper m_sqlHelper;
        private string m_server;
        private string m_database;
        private string m_userName;
        private string m_password;
        public GetDataServiceDAL_MSSQL(string serverName, string dbName, string userName, string password)
        {
            this.m_server = serverName;
            this.m_database = dbName;
            this.m_userName = userName;
            this.m_password = password;
        }
        public override bool CreateConnection(out string errMsg)
        {
            errMsg = string.Empty;
            SqlConnectionStringBuilder sqlConnBuilder = new SqlConnectionStringBuilder();
            sqlConnBuilder.DataSource = this.m_server;
            sqlConnBuilder.InitialCatalog = this.m_database;
            sqlConnBuilder.UserID = this.m_userName;
            sqlConnBuilder.Password = this.m_password;
            this.m_sqlHelper = new MSSQLHelper(sqlConnBuilder.ConnectionString, out errMsg);
            try
            {
                this.m_sqlHelper.Connected = true;
                this.m_sqlHelper.Connected = false;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
    }
}
