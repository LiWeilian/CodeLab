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
            StartModbusRtuTcpServers();

            StartModbusTcpClients();

            StartWCFServer();            
        }

        protected override void OnStop()
        {
        }

        private void StartModbusRtuTcpServers()
        {
            //启动ModbusRTUTCP服务Socket
            try
            {
                string serversConfig = System.Configuration.ConfigurationManager.AppSettings["modbusrtutcp"];
                string sWaitTime = System.Configuration.ConfigurationManager.AppSettings["interval"];
                uint waitTime;
                if (!uint.TryParse(sWaitTime, out waitTime))
                {
                    waitTime = 500;
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                //{SN:001,IP:172.16.1.2,PORT:6008}
                //{"SN":"001","IP":"172.16.1.2","PORT":"6008"}
                //DataServerConfig dsCfg = jss.Deserialize<DataServerConfig>("{'SN':'001','IP':'172.16.1.2','PORT':'6008'}");

                List<DataServerConfig> serverCfgList = jss.Deserialize<List<DataServerConfig>>(serversConfig);
                foreach (DataServerConfig serverCfg in serverCfgList)
                {
                    Thread.Sleep(1000);
                    if (serverCfg.ServerID.Trim() == string.Empty)
                    {
                        ServiceLog.LogServiceMessage(string.Format("服务器标识符无效", serverCfg.ServerID));
                        continue;
                    }
                    IPAddress ip;
                    if (!IPAddress.TryParse(serverCfg.IP, out ip))
                    {
                        ServiceLog.LogServiceMessage(string.Format("服务器IP地址[{0}]无效", serverCfg.IP));
                        continue;
                    }

                    ushort port;
                    if (!ushort.TryParse(serverCfg.Port, out port))
                    {
                        ServiceLog.LogServiceMessage(string.Format("服务器端口[{0}]无效", serverCfg.Port));
                        continue;
                    }
                    try
                    {
                        ModbusRtuTcpServerHost tcpSvrHost = new ModbusRtuTcpServerHost(serverCfg.ServerID, ip, port, waitTime);
                        Thread thread = new Thread(new ThreadStart(tcpSvrHost.Run));
                        thread.IsBackground = true;
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        ServiceLog.LogServiceMessage(string.Format("启动数据采集服务[{0} {1}:{2}]线程时发生错误：{3}", serverCfg.ServerID, serverCfg.IP, serverCfg.Port, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("获取数据采集服务配置时发生错误：{0}", ex.Message));
            }
        }

        private void StartModbusTcpClients()
        {
            try
            {
                string serversConfig = System.Configuration.ConfigurationManager.AppSettings["modbustcp"];
                string sWaitTime = System.Configuration.ConfigurationManager.AppSettings["interval"];
                uint waitTime;
                if (!uint.TryParse(sWaitTime, out waitTime))
                {
                    waitTime = 500;
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<DataServerConfig> serverCfgList = jss.Deserialize<List<DataServerConfig>>(serversConfig);
                foreach (DataServerConfig serverCfg in serverCfgList)
                {
                    Thread.Sleep(1000);
                    if (serverCfg.ServerID.Trim() == string.Empty)
                    {
                        ServiceLog.LogServiceMessage(string.Format("服务器标识符无效", serverCfg.ServerID));
                        continue;
                    }
                    IPAddress ip;
                    if (!IPAddress.TryParse(serverCfg.IP, out ip))
                    {
                        ServiceLog.LogServiceMessage(string.Format("服务器IP地址[{0}]无效", serverCfg.IP));
                        continue;
                    }

                    ushort port;
                    if (!ushort.TryParse(serverCfg.Port, out port))
                    {
                        ServiceLog.LogServiceMessage(string.Format("服务器端口[{0}]无效", serverCfg.Port));
                        continue;
                    }
                    try
                    {
                        ModbusTcpClientHost mbTcpClientHost = new ModbusTcpClientHost(serverCfg.ServerID,
                            ip, port, waitTime);
                        Thread thread = new Thread(new ThreadStart(mbTcpClientHost.Run));
                        thread.IsBackground = true;
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        ServiceLog.LogServiceMessage(string.Format("启动数据采集服务[{0} {1}:{2}]线程时发生错误：{3}", serverCfg.ServerID, serverCfg.IP, serverCfg.Port, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("获取数据采集服务配置时发生错误：{0}", ex.Message));
            }

        }

        private void StartWCFServer()
        {
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
                ServiceLog.LogServiceMessage(string.Format("启动WCF服务成功"));
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("启动WCF服务时发生错误：", ex.Message));

            }
        }
    }
}
