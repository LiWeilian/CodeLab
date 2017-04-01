using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace GDDST.DI.NetServerConsoleDemo
{
    class WebAppHost : MarshalByRefObject
    {
        public void ProcessRequest(
            string page,
            string query,
            System.IO.TextWriter writer)
        {
            System.Web.Hosting.SimpleWorkerRequest worker
                = new System.Web.Hosting.SimpleWorkerRequest(
                    page,
                    query,
                    writer);

            System.Web.HttpRuntime.ProcessRequest(worker);
        }
    }
}
