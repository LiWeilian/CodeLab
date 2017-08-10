using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDDST.DI.DataServiceWCF
{
    public class ModbusRTURequestBody
    {
        public string ServerID { get; set; }
        public string DeviceAddr { get; set; }
        public string FunctionCode { get; set; }
        public string StartAddr { get; set; }
        public string RegCount { get; set; }
    }

    public class ModbusRTUResponseBody
    {
        public string ServerID { get; set; }
        public string DeviceAddr { get; set; }
        public string FunctionCode { get; set; }
        public string DataLength { get; set; }
        public string DataContent { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseTime { get; set; }
        public string ResponseCRC { get; set; }
    }

    public class ModbusTCPRequestBody
    {
        public string ServerID { get; set; }
        public string DeviceAddr { get; set; }
        public string FunctionCode { get; set; }
        public string StartAddr { get; set; }
        public string RegCount { get; set; }
        /// <summary>
        /// 返回类型，string或json，默认string
        /// </summary>
        public string ReturnFormat { get; set; }
    }

    public class ModbusTCPResponseBody
    {
        public string ServerID { get; set; }
        public string DeviceAddr { get; set; }
        public string FunctionCode { get; set; }
        public string DataLength { get; set; }
        public string DataContent { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseTime { get; set; }
    }

    public class ModbusTCPWriteDataBody
    {
        public string ServerID { get; set; }
        public string DeviceAddr { get; set; }


        /// <summary>
        /// 需要写入的数据，JSON格式{起始寄存器1:值1,起始寄存器2:值2...}，根据值的长度判断写入多少个寄存器。
        /// </summary>
        public string WriteData { get; set; }
    }
}
