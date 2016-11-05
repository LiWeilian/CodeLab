using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;

namespace WindowsFormsApplication1
{
    class SQLHelper
    {
        private OracleConnection m_dbConn;

        public SQLHelper(string connStr, out string errMsg)
        {
            if (!InitialDatabaseConnection(connStr, out errMsg))
            {
                this.m_dbConn = null;
            }
        }

        public bool Connected
        {
            get { return this.m_dbConn != null && this.m_dbConn.State == ConnectionState.Open; }
            set
            {
                if (this.m_dbConn != null)
                {
                    if (value && this.m_dbConn.State == ConnectionState.Closed)
                    {
                        try
                        {
                            this.m_dbConn.Open();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("打开数据库连接时出现错误：" + ex.Message);
                        }
                    }
                    else
                        if (!value && this.m_dbConn.State == ConnectionState.Open)
                        {
                            try
                            {
                                this.m_dbConn.Close();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("关闭数据库连接时出现错误：" + ex.Message);
                            }
                        }
                }
                else
                {
                    throw new Exception("数据库连接未设置");
                }
            }

        }

        private bool OpenConnection(out string errMsg)
        {
            errMsg = string.Empty;

            if (this.m_dbConn == null)
            {
                errMsg = "数据库连接未设置";
                return false;
            }

            if (this.m_dbConn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    this.m_dbConn.Open();
                }
                catch (Exception ex)
                {
                    errMsg = "打开数据库连接失败：" + ex.Message;
                    return false;
                }

            }

            if (this.m_dbConn.State == System.Data.ConnectionState.Closed)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            return true;
        }

        private bool InitialDatabaseConnection(string connStr, out string errMsg)
        {
            errMsg = string.Empty;

            try
            {
                this.m_dbConn = new OracleConnection();
                this.m_dbConn.ConnectionString = connStr;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = "打开数据连接时出现错误：" + ex.Message;
                return false;
            }
        }

        public bool ExecuteNonQuery(string clause, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                if (!OpenConnection(out errMsg))
                {
                    return false;
                }

                OracleCommand cmd = new OracleCommand(clause, (OracleConnection)this.m_dbConn);
                cmd.ExecuteNonQuery();
                this.m_dbConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        public DataTable QueryRecords(string clause, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            try
            {
                if (!OpenConnection(out errMsg))
                {
                    return null;
                }

                OracleCommand cmd = new OracleCommand(clause, (OracleConnection)this.m_dbConn);

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
