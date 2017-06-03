using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.Driver
{
    public class HostContainer
    {
        private static List<ModbusRtuTcpServerHost> tcpServerHostList = new List<ModbusRtuTcpServerHost>();
        private static List<ModbusTcpClientHost> modbusTcpClientHostList = new List<ModbusTcpClientHost>();
        public static bool AddTcpServerHost(ModbusRtuTcpServerHost host)
        {
            ModbusRtuTcpServerHost t = GetModbusRtuTcpServerHostByServerID(host.ServerID);
            if (t == null)
            {
                tcpServerHostList.Add(host);
                return true;
            } else
            {
                return false;
            }
        }

        public static ModbusRtuTcpServerHost GetModbusRtuTcpServerHostByServerID(string id)
        {
            var r = from s in tcpServerHostList
                    where s.ServerID == id
                    select s;
            foreach (ModbusRtuTcpServerHost item in r)
            {
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }

        public static bool AddModbusTcpClientHost(ModbusTcpClientHost host)
        {
            ModbusTcpClientHost h = GetModbusTcpClientHostByServerID(host.ServerID);
            if (h == null)
            {
                modbusTcpClientHostList.Add(host);
                return true;
            } else
            {
                return false;
            }
        }

        public static ModbusTcpClientHost GetModbusTcpClientHostByServerID(string id)
        {
            var r = from s in modbusTcpClientHostList
                    where s.ServerID == id
                    select s;
            foreach (ModbusTcpClientHost item in r)
            {
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
