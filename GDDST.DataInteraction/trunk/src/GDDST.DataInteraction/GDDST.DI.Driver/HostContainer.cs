using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.Driver
{
    public class HostContainer
    {
        private static List<TCPServerHost> tcpServerHostList = new List<TCPServerHost>();
        public static bool AddTcpServerHost(TCPServerHost host)
        {
            TCPServerHost t = GetTcpServerHostByServerID(host.ServerID);
            if (t == null)
            {
                tcpServerHostList.Add(host);
                return true;
            } else
            {
                return false;
            }
        }

        public static TCPServerHost GetTcpServerHostByServerID(string id)
        {
            var r = from s in tcpServerHostList
                    where s.ServerID == id
                    select s;
            foreach (TCPServerHost item in r)
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
