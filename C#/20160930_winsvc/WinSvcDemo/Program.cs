using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinSvcDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //**********正式服务-开始**************
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WinSvcDemo()
            };
            ServiceBase.Run(ServicesToRun);
            //**********正式服务-结束**************

            //**********调试-开始**************
            //WinSvcDemo winSvc = new WinSvcDemo();
            //winSvc.OnStart();
            //ServiceBase.Run(winSvc);
            //**********调试-结束**************
        }
    }
}
