using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GDDST.DI.GetDataServer
{
    class ModbusTcpConfig
    {
        private ushort m_identifier = 1;
        private XmlNode m_serverCfgNode = null;
        public string ServerName { get; private set; }
        /// <summary>
        /// 服务器通信类型，当前是MODBUSTCP
        /// </summary>
        public string ServerCommType { get; private set; }
        public string ServerIP { get; }
        public ushort ServerPort { get; }

        /// <summary>
        /// Modbus TCP 操作列表
        /// </summary>
        public List<ModbusTcpOperation> ModbusOperations { get; private set; }

        /// <summary>
        /// Modbus TCP 数据项列表
        /// </summary>
        public List<ModbusTcpConfigItem> ModbusTcpItems { get; private set; }

        /// <summary>
        /// 数据库连接设置
        /// </summary>
        public ModbusTcpDatabaseConfig DatabaseConfig { get; private set; }

        public ModbusTcpConfig(string serverName, string serverCommType, XmlNode serverCfgNode)
        {
            ServerName = serverName;
            ServerCommType = serverCommType;
            m_serverCfgNode = serverCfgNode;

            ServerIP = GetServerIP();
            ServerPort = GetServerPort();

            ModbusOperations = GetModbusTcpOperations();

        }

        private string GetServerIP()
        {
            XmlNode connNode = m_serverCfgNode.SelectSingleNode("connection");
            if (connNode != null)
            {
                if (connNode.Attributes["ip"] != null)
                {
                    return connNode.Attributes["ip"].Value;
                }
            }

            return string.Empty;
        }

        private ushort GetServerPort()
        {

            XmlNode connNode = m_serverCfgNode.SelectSingleNode("connection");
            if (connNode != null)
            {
                if (connNode.Attributes["port"] != null)
                {
                    string sPort = connNode.Attributes["port"].Value;
                    ushort iPort;
                    if (ushort.TryParse(sPort, out iPort))
                    {
                        return iPort;
                    }
                }
            }

            return 502;
        }

        private XmlNode GetModbusTcpConfigItemsNode()
        {
            return m_serverCfgNode.SelectSingleNode("items");
        }

        private List<ModbusTcpConfigItem> GetModbusTcpConfigItems()
        {
            List<ModbusTcpConfigItem> items = new List<ModbusTcpConfigItem>();

            XmlNode itemsNode = GetModbusTcpConfigItemsNode();
            if (itemsNode != null)
            {
                XmlNodeList itemNodeList = itemsNode.SelectNodes("item");
                foreach (XmlNode itemNode in itemNodeList)
                {
                    ModbusTcpConfigItem item = new ModbusTcpConfigItem();

                    if (itemNode.Attributes["device_addr"] != null)
                    {
                        string sDeviceAddr = itemNode.Attributes["device_addr"].Value;
                        byte tempDeviceAddr;
                        if (byte.TryParse(sDeviceAddr, out tempDeviceAddr))
                        {
                            item.DeviceAddr = tempDeviceAddr;
                        }
                    }
                    if (item.DeviceAddr == null)
                    {
                        continue;
                    }

                    if (itemNode.Attributes["addr"] != null)
                    {
                        string sAddr = itemNode.Attributes["addr"].Value;
                        ushort tempAddr;
                        if (ushort.TryParse(sAddr, out tempAddr))
                        {
                            item.RegAddr = tempAddr;
                        }
                    }
                    if (item.RegAddr == null)
                    {
                        continue;
                    }

                    if (itemNode.Attributes["name"] != null)
                    {
                        item.Name = itemNode.Attributes["name"].Value;
                    } else
                    {
                        item.Name = string.Empty;
                    }

                    item.Multiplier = 1.0;
                    if (itemNode.Attributes["multiplier"] != null)
                    {
                        string sMultiplier = itemNode.Attributes["multiplier"].Value;
                        double dMultiplier;
                        if (double.TryParse(sMultiplier, out dMultiplier))
                        {
                            item.Multiplier = dMultiplier;
                        }
                    }

                    if (itemNode.Attributes["unit"] != null)
                    {
                        item.Unit = itemNode.Attributes["unit"].Value;
                    }
                    else
                    {
                        item.Unit = string.Empty;
                    }
                }
            }

            return items;
        }

        private XmlNode GetModbusTcpOperationsNode()
        {
            return m_serverCfgNode.SelectSingleNode("operations");
        }

        private List<ModbusTcpOperation> GetModbusTcpOperations()
        {
            List<ModbusTcpOperation> ops = new List<ModbusTcpOperation>();

            XmlNode opsNode = GetModbusTcpOperationsNode();
            if (opsNode != null)
            {
                XmlNodeList opNodeList = opsNode.SelectNodes("operation");
                foreach (XmlNode opNode in opNodeList)
                {
                    ModbusTcpOperation op = new ModbusTcpOperation();

                    if (opNode.Attributes["device_addr"] != null)
                    {
                        string sDeviceAddr = opNode.Attributes["device_addr"].Value;
                        byte tempDeviceAddr;
                        if (byte.TryParse(sDeviceAddr, out tempDeviceAddr))
                        {
                            op.DeviceAddr = tempDeviceAddr;
                        }
                    }
                    if (op.DeviceAddr == null)
                    {
                        continue;
                    }

                    if (opNode.Attributes["func_code"] != null)
                    {
                        string sFuncCode = opNode.Attributes["func_code"].Value;
                        byte tempFuncCode;
                        if (byte.TryParse(sFuncCode, out tempFuncCode))
                        {
                            op.FunctionCode = tempFuncCode;
                        }
                    }
                    if (op.FunctionCode == null)
                    {
                        continue;
                    }

                    if (opNode.Attributes["start_addr"] != null)
                    {
                        string sStartAddr = opNode.Attributes["start_addr"].Value;
                        ushort tempStartAddr;
                        if (ushort.TryParse(sStartAddr, out tempStartAddr))
                        {
                            op.StartAddr = tempStartAddr;
                        }
                    }
                    if (op.StartAddr == null)
                    {
                        continue;
                    }

                    if (opNode.Attributes["reg_count"] != null)
                    {
                        string sRegCount = opNode.Attributes["reg_count"].Value;
                        ushort tempRegCount;
                        if (ushort.TryParse(sRegCount, out tempRegCount))
                        {
                            op.RegCount = tempRegCount;
                        }
                    }
                    if (op.RegCount == null)
                    {
                        continue;
                    }

                    op.Length = 6;
                    op.Protocol = 0;
                    op.Identifier = m_identifier++;
                    ServiceLog.LogServiceMessage(op.ToString());
                    ops.Add(op);
                }
            }

            return ops;
        }

        private XmlNode GetDatabaseConfigNode()
        {
            return m_serverCfgNode.SelectSingleNode("database");
        }
        private ModbusTcpDatabaseConfig GetDatabaseConfig()
        {
            ModbusTcpDatabaseConfig dbCfg = new ModbusTcpDatabaseConfig();
            XmlNode dbCfgNode = GetDatabaseConfigNode();
            if (dbCfgNode != null)
            {
                string dbmsType = XmlUtils.GetNodeAttributeValueString(dbCfgNode,"dbms");
            }

            return dbCfg;
        }
    }

    class ModbusTcpConfigItem
    {
        public byte? DeviceAddr { get; set; }
        public ushort? RegAddr { get; set; }
        public string Name { get; set; }
        public double Multiplier { get; set; }
        public string Unit { get; set; }
    }

    class ModbusTcpOperation
    {
        public ushort Identifier { get; set; }
        public ushort Protocol { get; set; }
        public ushort Length { get; set; }
        public byte? DeviceAddr { get; set; }
        public byte? FunctionCode { get; set; }
        public ushort? StartAddr { get; set; }
        public ushort? RegCount { get; set; }

        public override string ToString()
        {
            return string.Format("Identifier:{0}, Protocol:{1}, Length:{2}, DeviceAddress:{3}, FunctionCode:{4}, StartAddress:{5}, RegisterCount:{6}",
                Identifier, Protocol, Length, DeviceAddr, FunctionCode, StartAddr, RegCount);
        }
    }

    class ModbusTcpResult
    {
        public ushort Identifier { get; set; }
        public ushort Protocol { get; set; }
        public ushort Length { get; set; }
        public byte DeviceAddr { get; set; }
        public byte FunctionCode { get; set; }
        public byte ResultDataLength { get; set; }
        public ushort[] ResultData { get; set; }
        public DateTime DataAcqTime { get; set; }
    }

    enum DBMSType
    {
        MSSQL,
        Oracle
    }

    class DatabaseConnectionInfo
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    class ModbusTcpDatabaseConfig
    {
        public DBMSType DbmsType { get; set; }
        public DatabaseConnectionInfo ConnectionInfo { get; set; }
        public string RealtimeDataTable { get; set; }
        public string HistoryDataTable { get; set; }
    }
    /*
    class ModbusTcpItem
    {
        public ushort Identifier { get; set; }
        public ushort Protocol { get; set; }
        public ushort Length { get; set; }
        public byte? DeviceAddr { get; set; }
        public byte? FunctionCode { get; set; }
        public ushort? StartAddr { get; set; }
        public ushort? RegCount { get; set; }

        public override string ToString()
        {
            return string.Format("Identifier:{0}, Protocol:{1}, Length:{2}, DeviceAddress:{3}, FunctionCode:{4}, StartAddress:{5}, RegisterCount:{6}",
                Identifier, Protocol, Length, DeviceAddr, FunctionCode, StartAddr, RegCount);
        }
    }
    */
}
