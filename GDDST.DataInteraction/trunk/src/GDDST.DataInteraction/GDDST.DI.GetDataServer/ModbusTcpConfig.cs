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
        public string ServerIP { get; }
        public ushort ServerPort { get; }
        public List<ModbusTcpOperation> ModbusOperations { get; private set; }
        public List<ModbusTcpConfigItem> ModbusItems { get; private set; }
        
        public ModbusTcpConfig(XmlNode serverCfgNode)
        {
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
    }

    class ModbusTcpConfigItem
    {
        public byte DeviceAddr { get; set; }
        public byte RegAddr { get; set; }
        public byte Name { get; set; }
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
    }

    class ModbusTcpDataEntity
    {
        public string RID { get; set; }
        public string DEVICE_ADDR { get; set; }
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
