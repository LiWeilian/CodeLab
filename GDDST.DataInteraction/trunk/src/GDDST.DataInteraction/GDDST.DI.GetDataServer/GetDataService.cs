using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;

namespace GDDST.DI.GetDataServer
{
    /// <summary>
    /// 作为客户端与远程服务器连接，获取数据，根据配置，可创建多个连接线程，连接多个服务器。
    /// </summary>
    public partial class GetDataService : ServiceBase
    {
        private ServiceLog m_svcLog;
        private ServersConfig m_svrCfg = null;

        private List<CommHandler> m_commHandlers = new List<CommHandler>();
        public GetDataService()
        {
            InitializeComponent();
        }

        //protected override void OnStart(string[] args)
        public void OnStart()
        {
            ServiceLog.LogServiceMessage("启动服务");

            //读取配置，获取服务器列表，每个服务器创建一个对应的客户端连接
            m_svrCfg = new ServersConfig();
            List<DataServerConfigDesc> dataServers = m_svrCfg.GetServerConfigDescList();
            foreach (DataServerConfigDesc dataServer in dataServers)
            {
                ServiceLog.LogServiceMessage(dataServer.ServerType);
                switch (dataServer.ServerType.ToUpper())
                {
                    case "MODBUSTCP":
                        ModbusTcpConfig mbTcpCfg = new ModbusTcpConfig(dataServer.ServerConfigNode);
                        if (mbTcpCfg != null)
                        {
                            ModbusTCPHandler mbTcpHandler = new ModbusTCPHandler(mbTcpCfg);
                            m_commHandlers.Add(mbTcpHandler);
                            mbTcpHandler.OnStart();
                        }
                        break;
                    default:
                        break;
                }
                //通讯太频繁会出问题
                Thread.Sleep(10);
            }
        }

        protected override void OnStop()
        {
            ServiceLog.LogServiceMessage("停止服务");
        }

        private GetDataServiceDAL CreateGetDataServiceDAL(DataClientSetting dcs)
        {
            switch (dcs.DBMS.ToUpper().Trim())
            {
                case "MSSQLSERVER":
                    return new GetDataServiceDAL_MSSQL(dcs.ServerName,
                        dcs.DatabaseName, dcs.UserName, dcs.Password);
                case "ORACLE":
                    return null;
                default:
                    return null;
            }
        }
    }
}
