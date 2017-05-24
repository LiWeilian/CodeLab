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
using WCFDemo.RestSvcLib;

namespace WCFDemo.WinSvcHost
{
    public partial class WCFWinSvc : ServiceBase
    {
        private ServiceHost serviceHost = null;
        public WCFWinSvc()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            try
            {

                if (serviceHost != null)
                {
                    serviceHost.Close();
                    serviceHost = null;
                }

                serviceHost = new ServiceHost(typeof(DataService));
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
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
