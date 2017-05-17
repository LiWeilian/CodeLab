using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GDDST.DI.GetDataServer
{
    class ServersConfig
    {
        private XmlDocument m_xmlDoc = null;

        public ServersConfig()
        {
            m_xmlDoc = new XmlDocument();
            try
            {
                m_xmlDoc.Load(string.Format("{0}\\ServersConfig.xml",
                    AppDomain.CurrentDomain.BaseDirectory));
            }
            catch (Exception ex)
            {
                m_xmlDoc = null;
                ServiceLog.LogServiceMessage(ex.Message);
            }
        }

        private XmlNode GetServersNode()
        {
            if (m_xmlDoc != null)
            {
                return m_xmlDoc.DocumentElement.SelectSingleNode("servers");
            } else
            {
                return null;
            }
        }

        private XmlNode GetClientNode(XmlNode serverNode)
        {
            return serverNode.SelectSingleNode("Client");
        }

        public List<DataServerSetting> GetServers()
        {
            List<DataServerSetting> servers = new List<DataServerSetting>();
            XmlNode serversNode = GetServersNode();
            if (serversNode != null)
            {
                XmlNodeList serverNodeList = serversNode.SelectNodes("server");
                foreach (XmlNode serverNode in serverNodeList)
                {
                    DataServerSetting ds = new DataServerSetting();
                    ds.DataProtocol = serverNode.Attributes["dataprotocol"] != null
                        ? serverNode.Attributes["dataprotocol"].Value : string.Empty;
                    ds.IP = serverNode.Attributes["ip"] != null
                        ? serverNode.Attributes["ip"].Value : string.Empty;
                    ds.Port = serverNode.Attributes["port"] != null
                        ? serverNode.Attributes["port"].Value : string.Empty;

                    XmlNode clientNode = GetClientNode(serverNode);
                    if (clientNode != null)
                    {
                        ds.ClientSetting.DBMS = clientNode.Attributes["dbms"] != null
                            ? clientNode.Attributes["dbms"].Value : string.Empty;
                        ds.ClientSetting.ServerName = clientNode.Attributes["servername"] != null
                            ? clientNode.Attributes["servername"].Value : string.Empty;
                        ds.ClientSetting.DatabaseName = clientNode.Attributes["databasename"] != null
                            ? clientNode.Attributes["databasename"].Value : string.Empty;
                        ds.ClientSetting.UserName = clientNode.Attributes["username"] != null
                            ? clientNode.Attributes["username"].Value : string.Empty;
                        ds.ClientSetting.Password = clientNode.Attributes["password"] != null
                            ? clientNode.Attributes["password"].Value : string.Empty;
                    }

                    servers.Add(ds);
                }
            }

            return servers;
        }

        public List<DataServerConfigDesc> GetServerConfigDescList()
        {
            List<DataServerConfigDesc> servers = new List<DataServerConfigDesc>();
            XmlNode serversNode = GetServersNode();
            if (serversNode != null)
            {
                XmlNodeList serverNodeList = serversNode.SelectNodes("server");
                foreach (XmlNode serverNode in serverNodeList)
                {
                    DataServerConfigDesc serverDesc = new DataServerConfigDesc(serverNode);
                    servers.Add(serverDesc);
                }
            }

            return servers;
        }
    }

    class DataServerConfigDesc
    {
        public XmlNode ServerConfigNode { get; private set; }
        public string ServerCommType { get; private set; }
        public String ServerName { get; private set; }

        public DataServerConfigDesc(XmlNode serverCfgNode)
        {
            ServerConfigNode = serverCfgNode;

            ServerCommType = GetServerType();

            ServerName = GetServerName();
        }

        private string GetServerType()
        {
            if (ServerConfigNode.Attributes["comm_type"] != null)
            {
                return ServerConfigNode.Attributes["comm_type"].Value;
            }

            return string.Empty;
        }

        private string GetServerName()
        {
            if (ServerConfigNode.Attributes["name"] != null)
            {
                return ServerConfigNode.Attributes["name"].Value;
            }

            return string.Empty;
        }
    }

    class DataServerSetting
    {
        public string DataProtocol { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public DataClientSetting ClientSetting { get; private set; }

        public DataServerSetting()
        {
            ClientSetting = new DataClientSetting();
        }
    }

    class DataClientSetting
    {
        public string DBMS { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
