using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDemo.RestSvcLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class DataService : IDataService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public ModbusRTUResponseBody RequestModbusRTUData(ModbusRTURequestBody request)
        {
            ModbusRTUResponseBody response = new ModbusRTUResponseBody();
            response.DeviceAddr = request.DeviceAddr;
            response.FunctionCode = request.FunctionCode;
            response.ErrorMessage = string.Format("Test: {0}", DateTime.Now);
            response.DataLength = "0";
            response.DataContent = "";

            return response;
        }

        public string JSONData(string id)
        {
            return Data(id);
        }

        private string Data(string id)
        {
            // logic
            return "Data: " + id;
        }
    }
}
