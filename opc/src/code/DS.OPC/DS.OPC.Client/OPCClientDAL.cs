using System;
using System.Collections.Generic;
using System.Text;
using OPCAutomation;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace DS.OPC.Client
{
    abstract class OPCClientDAL
    {
        public abstract bool ClearItems(string tableName, out string errMsg);
        public abstract bool InsertItem(string tableName, OPCClientItemEntity item, out string errMsg);
        public abstract bool DeleteItem(string tablename, OPCClientItemEntity item, out string errMsg);
        public abstract bool CreateConnection(out string errMsg);
        public abstract string QueryItemDataStatus(string tablename, OPCClientItemEntity item, out string errMsg);
    }

    class OPCClientMSSQLDAL : OPCClientDAL
    {
        private OPCMSSQLHelper m_sqlHelper;
        private string m_server;
        private string m_database;
        private string m_userName;
        private string m_password;
        public OPCClientMSSQLDAL(string serverName, string dbName, string userName, string password)
        {
            this.m_server = serverName;
            this.m_database = dbName;
            this.m_userName = userName;
            this.m_password = password;
        }

        public override bool ClearItems(string tableName, out string errMsg)
        {
            this.m_sqlHelper.Connected = true;
            if (!this.m_sqlHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            string deleteStr = "DELETE FROM " + tableName;
            if (!this.m_sqlHelper.ExecuteNonQuery(deleteStr, out errMsg))
            {
                return false;
            }

            return true;
        }

        public override bool DeleteItem(string tablename, OPCClientItemEntity item, out string errMsg)
        {
            errMsg = string.Empty;
            this.m_sqlHelper.Connected = true;
            if (!this.m_sqlHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            string deleteStr = "DELETE FROM " + tablename + " WHERE ";
            string whereClause = string.Empty;

            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if (itemProp.IsEntityIdentity)
                {
                    switch (itemProp.DataType)
                    {
                        case OPCClientDBFieldMapping.EnumDataType.INT:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.CHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.BINARY:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        default:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                    }
                }
            }

            if (whereClause != string.Empty)
            {
                deleteStr = deleteStr + whereClause;
                return this.m_sqlHelper.ExecuteNonQuery(deleteStr, out errMsg);
            } else
            {
                return true;
            }
        }



        public override bool InsertItem(string tableName, OPCClientItemEntity item, out string errMsg)
        {
            this.m_sqlHelper.Connected = true;
            if (!this.m_sqlHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            string insertStr = "INSERT INTO " + tableName + "(";
            
            string cols = string.Empty;

            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if(!itemProp.AutoInc)
                {
                    cols = cols == string.Empty 
                        ? itemProp.Name : cols + "," + itemProp.Name;
                }
            }

            insertStr = insertStr + cols + ") VALUES(";

            string values = string.Empty;
            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if(!itemProp.AutoInc)
                {
                    switch (itemProp.DataType)
                    {
                        case OPCClientDBFieldMapping.EnumDataType.INT:
                            values = values == string.Empty
                                ? itemProp.Value : values + "," + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                            values = values == string.Empty
                                ? itemProp.Value : values + "," + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.CHAR:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.BINARY:
                            values = values == string.Empty
                                ? itemProp.Value : values + "," + itemProp.Value;
                            break;
                        default:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                    }
                }
                
            }

            insertStr = insertStr + values + ")";

            return this.m_sqlHelper.ExecuteNonQuery(insertStr, out errMsg);
        }

        public override bool CreateConnection(out string errMsg)
        {
            errMsg = string.Empty;
            SqlConnectionStringBuilder sqlConnBuilder = new SqlConnectionStringBuilder();
            sqlConnBuilder.DataSource = this.m_server;
            sqlConnBuilder.InitialCatalog = this.m_database;
            sqlConnBuilder.UserID = this.m_userName;
            sqlConnBuilder.Password = this.m_password;
            this.m_sqlHelper = new OPCMSSQLHelper(sqlConnBuilder.ConnectionString, out errMsg);
            try
            {
                this.m_sqlHelper.Connected = true;
                this.m_sqlHelper.Connected = false;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                OPCLog.Error(string.Format("创建 MS SQL Server 数据库连接时发生错误：{0}", errMsg));
                return false;
            }
        }

        public override string QueryItemDataStatus(string tablename, OPCClientItemEntity item, out string errMsg)
        {
            errMsg = string.Empty;
            this.m_sqlHelper.Connected = true;
            if (!this.m_sqlHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return string.Empty;
            }

            string queryStr = "SELECT OUT_OF FROM " + tablename + " WHERE ";
            string whereClause = string.Empty;

            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if (itemProp.IsEntityIdentity)
                {
                    switch (itemProp.DataType)
                    {
                        case OPCClientDBFieldMapping.EnumDataType.INT:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.CHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.BINARY:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        default:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                    }
                }
            }

            if (whereClause != string.Empty)
            {
                queryStr = queryStr + whereClause;
                DataTable dt = this.m_sqlHelper.QueryRecords(queryStr, out errMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["OUT_OF"].ToString();
                } else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }

    class OPCClientOracleDAL : OPCClientDAL
    {
        private string m_oraSvrName;
        private string m_userName;
        private string m_password;
        private OPCOracleSQLHelper m_oraHelper;
        public OPCClientOracleDAL(string oraSvrName, string userName, string password)
        {
            this.m_oraSvrName = oraSvrName;
            this.m_userName = userName;
            this.m_password = password;
        }
        public override bool ClearItems(string tableName, out string errMsg)
        {
            this.m_oraHelper.Connected = true;
            if (!this.m_oraHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            string deleteStr = "DELETE FROM " + tableName;
            if (!this.m_oraHelper.ExecuteNonQuery(deleteStr, out errMsg))
            {
                return false;
            }

            return true;
        }

        public override bool DeleteItem(string tablename, OPCClientItemEntity item, out string errMsg)
        {
            errMsg = string.Empty;
            this.m_oraHelper.Connected = true;
            if (!this.m_oraHelper.Connected)
            {
                errMsg = "未连接到数据库";
                OPCLog.Error(errMsg);
                return false;
            }

            string deleteStr = "DELETE FROM " + tablename + " WHERE ";
            string whereClause = string.Empty;

            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if (itemProp.IsEntityIdentity)
                {
                    switch (itemProp.DataType)
                    {
                        case OPCClientDBFieldMapping.EnumDataType.INT:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.CHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                            string datetime = "TO_DATE('" + itemProp.Value + "', 'yyyy-mm-dd hh24:mi:ss')";
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + datetime : whereClause + " AND " + itemProp.Name + "=" + datetime;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.BINARY:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        default:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                    }
                }
            }

            if (whereClause != string.Empty)
            {
                deleteStr = deleteStr + whereClause;

                return this.m_oraHelper.ExecuteNonQuery(deleteStr, out errMsg);
            }
            else
            {
                
                return true;
            }
        }

        public override bool InsertItem(string tableName, OPCClientItemEntity item, out string errMsg)
        {
            this.m_oraHelper.Connected = true;
            if (!this.m_oraHelper.Connected)
            {
                errMsg = "未连接到数据库";
                OPCLog.Error(errMsg);
                return false;
            }

            string insertStr = "INSERT INTO " + tableName + "(";

            string cols = string.Empty;

            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if (!itemProp.AutoInc)
                {
                    cols = cols == string.Empty
                        ? itemProp.Name : cols + "," + itemProp.Name;
                }
            }

            insertStr = insertStr + cols + ") VALUES(";

            string values = string.Empty;
            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if (!itemProp.AutoInc)
                {
                    switch (itemProp.DataType)
                    {
                        case OPCClientDBFieldMapping.EnumDataType.INT:
                            values = values == string.Empty
                                ? itemProp.Value : values + "," + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                            values = values == string.Empty
                                ? itemProp.Value : values + "," + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.CHAR:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                            string date = "TO_DATE('" + itemProp.Value + "', 'yyyy-mm-dd hh24:mi:ss')";
                            values = values == string.Empty
                                ? date : values + ", " + date;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.BINARY:
                            values = values == string.Empty
                                ? itemProp.Value : values + "," + itemProp.Value;
                            break;
                        default:
                            values = values == string.Empty
                                ? "'" + itemProp.Value + "'" : values + ",'" + itemProp.Value + "'";
                            break;
                    }
                }

            }

            insertStr = insertStr + values + ")";

            return this.m_oraHelper.ExecuteNonQuery(insertStr, out errMsg);
        }

        public override bool CreateConnection(out string errMsg)
        {
            OracleConnectionStringBuilder oraConnBuilder = new OracleConnectionStringBuilder();
            oraConnBuilder.DataSource = this.m_oraSvrName;
            oraConnBuilder.UserID = this.m_userName;
            oraConnBuilder.Password = this.m_password;
            this.m_oraHelper = new OPCOracleSQLHelper(oraConnBuilder.ConnectionString, out errMsg);
            try
            {
                this.m_oraHelper.Connected = true;
                this.m_oraHelper.Connected = false;
                return true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                OPCLog.Error(string.Format("创建 Oracle 数据库连接时发生错误：{0}", errMsg));
                return false;
            }
        }

        public override string QueryItemDataStatus(string tablename, OPCClientItemEntity item, out string errMsg)
        {
            errMsg = string.Empty;
            this.m_oraHelper.Connected = true;
            if (!this.m_oraHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return String.Empty;
            }

            string queryStr = "SELECT OUT_OF FROM " + tablename + " WHERE ";
            string whereClause = string.Empty;

            foreach (OPCClientItemProperty itemProp in item.Properties)
            {
                if (!itemProp.IsEntityIdentity)
                {
                    switch (itemProp.DataType)
                    {
                        case OPCClientDBFieldMapping.EnumDataType.INT:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.CHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                            string datetime = "TO_DATE('" + itemProp.Value + "', 'yyyy-mm-dd hh24:mi:ss')";
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + datetime : whereClause + " AND " + itemProp.Name + "=" + datetime;
                            break;
                        case OPCClientDBFieldMapping.EnumDataType.BINARY:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "=" + itemProp.Value : whereClause + " AND " + itemProp.Name + "=" + itemProp.Value;
                            break;
                        default:
                            whereClause = whereClause == string.Empty
                                ? itemProp.Name + "='" + itemProp.Value + "'" : whereClause + " AND " + itemProp.Name + "='" + itemProp.Value + "'";
                            break;
                    }
                }
            }

            if (whereClause != string.Empty)
            {
                queryStr = queryStr + whereClause;
                DataTable dt = this.m_oraHelper.QueryRecords(queryStr, out errMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["OUT_OF"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
