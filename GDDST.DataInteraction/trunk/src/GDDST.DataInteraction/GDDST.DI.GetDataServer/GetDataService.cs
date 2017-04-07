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

namespace GDDST.DI.GetDataServer
{
    /// <summary>
    /// 作为客户端与远程服务器连接，获取数据，根据配置，可创建多个连接线程，连接多个服务器。
    /// </summary>
    public partial class GetDataService : ServiceBase
    {
        private ServiceLog m_svcLog;
        public GetDataService()
        {
            InitializeComponent();
        }

        //protected override void OnStart(string[] args)
        public void OnStart()
        {
            ServiceLog.LogServiceMessage("启动服务");

            //读取配置，获取服务器列表，每个服务器创建一个对应的客户端连接

        }

        protected override void OnStop()
        {
            ServiceLog.LogServiceMessage("停止服务");
        }
    }
}
