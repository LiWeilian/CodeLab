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
            List<DataServerSetting> dataServers = m_svrCfg.GetServers();
            foreach (DataServerSetting dataServer in dataServers)
            {
                DataClient client = new DataClient();

                client.OnStart(dataServer.DataProtocol, dataServer.IP, dataServer.Port, null);
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
