using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GDDST.DI.DataServiceWCF
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
            response.ErrorMessage = string.Empty;

            if (GDDST.DI.Driver.HostContainer.TcpServerHost == null)
            {
                response.ErrorMessage = string.Format("数据采集服务未启动", request.DeviceAddr);
                return response;
            }

            byte devAddr;
            if (!byte.TryParse(request.DeviceAddr, out devAddr))
            {
                response.ErrorMessage = string.Format("设备地址[{0}]无效", request.DeviceAddr);
                return response;
            }

            byte funcCode;
            if (!byte.TryParse(request.FunctionCode, out funcCode))
            {
                response.ErrorMessage = string.Format("功能代码[{0}]无效", request.FunctionCode);
                return response;
            }

            ushort startAddr;
            if (!ushort.TryParse(request.StartAddr, out startAddr))
            {
                response.ErrorMessage = string.Format("起始寄存器地址[{0}]无效", request.StartAddr);
                return response;
            }

            ushort regCount;
            if (!ushort.TryParse(request.RegCount, out regCount))
            {
                response.ErrorMessage = string.Format("读取寄存器数量[{0}]无效", request.RegCount);
                return response;
            }
            
            try
            {
                response.DataContent = GDDST.DI.Driver.HostContainer.TcpServerHost.RequestModbusRTUData(devAddr, funcCode, startAddr, regCount);
                response.DataLength = (regCount * 2).ToString();

            }
            catch (Exception ex)
            {
                response.DataContent = string.Empty;
                response.DataLength = "0";
                response.ErrorMessage = ex.Message;
            }

            return response;
            
        }
    }
}
