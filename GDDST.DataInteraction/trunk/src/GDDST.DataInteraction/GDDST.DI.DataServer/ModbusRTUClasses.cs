using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDDST.DI.DataServer
{
    public class ModbusRTURequestBody
    {
        public string DeviceAddr { get; set; }
        public string FunctionCode { get; set; }
        public string StartAddr { get; set; }
        public string RegCount { get; set; }
    }

    public class ModbusRTUResponseBody
    {
        public string DeviceAddr { get; set; }
        public string FunctionCode { get; set; }
        public string DataLength { get; set; }
        public string DataContent { get; set; }
        public string ErrorMessage { get; set; }
    }
}