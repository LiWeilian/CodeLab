using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace GDDST.DI.GetDataServer
{
    abstract class DatabaseHelper
    {
        protected DbConnection m_dbConn = null;

        //public string ErrorMessage { get; protected set; }

        protected abstract bool InitializeConnection(string connStr, out string errMsg);

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

        protected bool OpenConnection(out string errMsg)
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

        public abstract bool ExecuteNonQuery(string clause, out string errMsg);

        public abstract DataTable QueryRecords(string clause, out string errMsg);
    }
}
