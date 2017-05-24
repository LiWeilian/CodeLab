using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFDemo.WinSvc
{
    public partial class CalculatorWinSvc : ServiceBase
    {
        private ServiceHost serviceHost = null;
        public CalculatorWinSvc()
        {
            InitializeComponent();
        }

        //protected override void OnStart(string[] args)
        public void OnStart()
        {
            try
            {

                if (serviceHost != null)
                {
                    serviceHost.Close();
                    serviceHost = null;
                }

                serviceHost = new ServiceHost(typeof(CalculatorService));
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
            if(serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
