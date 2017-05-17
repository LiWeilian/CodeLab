using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace GDDST.DI.GetDataServer
{
    class ModbusTcpDAL_MSSQL : GetDataServiceDAL
    {
        private MSSQLHelper m_sqlHelper;
        private string m_server;
        private string m_database;
        private string m_userName;
        private string m_password;
        public ModbusTcpDAL_MSSQL(string serverName, string dbName, string userName, string password)
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

        public override bool InsertData(DataEntity data, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {

                this.m_sqlHelper.Connected = true;
            }
            catch (Exception)
            {
            }
            if (!this.m_sqlHelper.Connected)
            {
                errMsg = "未连接到数据库";
                return false;
            }

            ModbusTcpDataEntity mbTcpData = (ModbusTcpDataEntity)data;

            

            string insertSql = string.Format(@"INSERT INTO {0}(RID, STATION,DEVICE_ADDR,SENSOR_TYPE,SENSOR_NAME,ORI_VALUE, VALUE, UNIT, DTIME) 
                    VALUES('{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}')", 
                    "appuser.dbo.MODBUSTCP_DATA_HISTORY",
                    mbTcpData.RID,
                    mbTcpData.Station,
                    mbTcpData.Device_Addr,
                    mbTcpData.Sensor_Type,
                    mbTcpData.Sensor_Name,
                    mbTcpData.Ori_Value,
                    mbTcpData.Trans_Value,
                    mbTcpData.Trans_Unit,
                    mbTcpData.DataAcqTime.ToString());

            return false;
        }
    }
}
