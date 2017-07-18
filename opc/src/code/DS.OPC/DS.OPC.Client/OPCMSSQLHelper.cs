using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DS.OPC.Client
{
    class OPCMSSQLHelper : OPCDatabaseHelper
    {
        public OPCMSSQLHelper(string connStr, out string errMsg)
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
                base.m_dbConn = new SqlConnection();
                base.m_dbConn.ConnectionString = connStr;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = "打开数据连接时出现错误：" + ex.Message;
                OPCLog.Error(errMsg);
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
                
                SqlCommand cmd = new SqlCommand(clause, (SqlConnection)base.m_dbConn);
                cmd.ExecuteNonQuery();
                base.m_dbConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                OPCLog.Error(string.Format("执行SQL语句\r\n{0}\r\n时发生错误：{1}", clause, ex.Message));
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

                SqlCommand cmd = new SqlCommand(clause, (SqlConnection)base.m_dbConn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                errMsg = "查询数据时出现错误：" + ex.Message;
                OPCLog.Error(errMsg);
                return null;
            }

            return dt;
        }
    }
}
