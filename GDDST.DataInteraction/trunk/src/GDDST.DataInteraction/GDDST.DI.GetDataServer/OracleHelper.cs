using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace GDDST.DI.GetDataServer
{
    class OracleHelper : DatabaseHelper
    {
        public OracleHelper(string connStr, out string errMsg)
        {
            if (!InitializeConnection(connStr, out errMsg))
            {
                base.m_dbConn = null;
            }
        }
        protected override bool InitializeConnection(string connStr, out string errMsg)
        {
            errMsg = string.Empty;

            try
            {
                base.m_dbConn = new OracleConnection();
                base.m_dbConn.ConnectionString = connStr;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = "打开数据连接时出现错误：" + ex.Message;
                return false;
            }
        }

        public override bool ExecuteNonQuery(string clause, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                if (!OpenConnection(out errMsg))
                {
                    return false;
                }

                OracleCommand cmd = new OracleCommand(clause, (OracleConnection)base.m_dbConn);
                cmd.ExecuteNonQuery();
                base.m_dbConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        public override DataTable QueryRecords(string clause, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            try
            {
                if (!OpenConnection(out errMsg))
                {
                    return null;
                }

                OracleCommand cmd = new OracleCommand(clause, (OracleConnection)base.m_dbConn);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                errMsg = "查询数据时出现错误：" + ex.Message;
                return null;
            }

            return dt;
        }
    }
}
