using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace GDDST.DI.GetDataServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            /*
            //正式
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new GetDataService()
            };
            ServiceBase.Run(ServicesToRun);
            //正式
            */

            //测试
            GetDataService getDataSvc = new GetDataService();
            getDataSvc.OnStart();

            Console.ReadLine();
            //测试
        }
    }
}
