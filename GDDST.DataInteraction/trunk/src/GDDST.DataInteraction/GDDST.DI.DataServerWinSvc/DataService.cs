using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using GDDST.DI.Driver;
using GDDST.DI.Utils;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace GDDST.DI.DataServerWinSvc
{
    public partial class DataService : ServiceBase
    {
        private ServiceHost serviceHost = null;
        public DataService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            
            //启动服务Socket
            try
            {
                string serversConfig = System.Configuration.ConfigurationManager.AppSettings["dataservers"];
                JavaScriptSerializer jss = new JavaScriptSerializer();
                //{SN:001,IP:172.16.1.2,PORT:6008}
                //{"SN":"001","IP":"172.16.1.2","PORT":"6008"}
                //DataServerConfig dsCfg = jss.Deserialize<DataServerConfig>("{'SN':'001','IP':'172.16.1.2','PORT':'6008'}");

                List<DataServerConfig> serverCfgList = jss.Deserialize<List<DataServerConfig>>(serversConfig);
                foreach (DataServerConfig serverCfg in serverCfgList)
                {
                    Console.WriteLine(serverCfg.SN);
                    Console.WriteLine(serverCfg.IP);
                    Console.WriteLine(serverCfg.Port.ToString());
                }
                /*
                string serverIp = System.Configuration.ConfigurationManager.AppSettings["server_ip"];
                string serverPort = System.Configuration.ConfigurationManager.AppSettings["server_port"];

                IPAddress ip;
                if (!IPAddress.TryParse(serverIp, out ip))
                {
                    ServiceLog.LogServiceMessage(string.Format("服务器IP地址[{0}]无效", serverIp));
                }

                ushort port;
                if (!ushort.TryParse(serverPort, out port))
                {
                    ServiceLog.LogServiceMessage(string.Format("服务器端口[{0}]无效", serverPort));
                }

                TCPServerHost tcpSvrHost = new TCPServerHost(ip, port);
                Thread thread = new Thread(new ThreadStart(tcpSvrHost.Run));
                thread.IsBackground = true;
                thread.Start();

                GDDST.DI.Driver.HostContainer.TcpServerHost = tcpSvrHost;
                */
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("启动数据采集服务时发生错误：{0}", ex.Message));
            }
            
            //启动WCF服务
            try
            {

                if (serviceHost != null)
                {
                    serviceHost.Close();
                    serviceHost = null;
                }

                serviceHost = new ServiceHost(typeof(GDDST.DI.DataServiceWCF.DataService));
                serviceHost.Open();
                Console.WriteLine("OnStart");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
        }

        protected override void OnStop()
        {
        }

        
    }
}
