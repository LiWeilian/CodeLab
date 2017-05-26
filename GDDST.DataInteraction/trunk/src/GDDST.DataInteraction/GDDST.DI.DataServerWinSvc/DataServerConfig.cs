using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDDST.DI.DataServerWinSvc
{
    class DataServerConfig
    {
        public string ServerID { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
    }

    class DataServerConfigList
    {
        public List<DataServerConfig> ServerConfigs { get; set; }
    }
}
