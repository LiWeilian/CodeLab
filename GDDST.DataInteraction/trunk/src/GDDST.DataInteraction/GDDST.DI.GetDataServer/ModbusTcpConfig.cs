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
        public List<ModbusTcpItem> ModbusTcpItems { get; private set; }
        
        public ModbusTcpConfig(XmlNode serverCfgNode)
        {
            m_serverCfgNode = serverCfgNode;

            ServerIP = GetServerIP();
            ServerPort = GetServerPort();

            ModbusTcpItems = GetModbusItems();

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

        private XmlNode GetModbusTcpItemsNode()
        {
            return m_serverCfgNode.SelectSingleNode("items");
        }

        private List<ModbusTcpItem> GetModbusItems()
        {
            XmlNode itemsNode = GetModbusTcpItemsNode();

            List<ModbusTcpItem> items = new List<ModbusTcpItem>();
            if (itemsNode != null)
            {
                XmlNodeList itemNodeList = itemsNode.SelectNodes("item");
                foreach (XmlNode itemNode in itemNodeList)
                {
                    ModbusTcpItem item = new ModbusTcpItem();

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

                    if (itemNode.Attributes["func_code"] != null)
                    {
                        string sFuncCode = itemNode.Attributes["func_code"].Value;
                        byte tempFuncCode;
                        if (byte.TryParse(sFuncCode, out tempFuncCode))
                        {
                            item.FunctionCode = tempFuncCode;
                        }
                    }
                    if (item.FunctionCode == null)
                    {
                        continue;
                    }

                    if (itemNode.Attributes["start_addr"] != null)
                    {
                        string sStartAddr = itemNode.Attributes["start_addr"].Value;
                        ushort tempStartAddr;
                        if (ushort.TryParse(sStartAddr, out tempStartAddr))
                        {
                            item.StartAddr = tempStartAddr;
                        }
                    }
                    if (item.StartAddr == null)
                    {
                        continue;
                    }

                    if (itemNode.Attributes["reg_count"] != null)
                    {
                        string sRegCount = itemNode.Attributes["reg_count"].Value;
                        ushort tempRegCount;
                        if (ushort.TryParse(sRegCount, out tempRegCount))
                        {
                            item.RegCount = tempRegCount;
                        }
                    }
                    if (item.RegCount == null)
                    {
                        continue;
                    }

                    item.Length = 6;
                    item.Protocol = 0;
                    item.Identifier = m_identifier++;
                    ServiceLog.LogServiceMessage(item.ToString());
                    items.Add(item);
                }
            }
            
            return items;
        }
    }

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
}
