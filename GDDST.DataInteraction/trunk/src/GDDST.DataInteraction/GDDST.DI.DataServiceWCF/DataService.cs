using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GDDST.DI.Driver;
using System.Web.Script.Serialization;

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
            response.ServerID = request.ServerID;
            response.DeviceAddr = request.DeviceAddr;
            response.FunctionCode = request.FunctionCode;
            response.ErrorMessage = string.Empty;
            response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            ModbusRtuTcpServerHost tcpServerHost = HostContainer.GetModbusRtuTcpServerHostByServerID(request.ServerID);
            if (tcpServerHost == null)
            {
                response.ErrorMessage = string.Format("数据采集服务未启动或未成功连接数据源");
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
                string respCRC = string.Empty;
                response.DataContent = tcpServerHost.RequestModbusRTUData(devAddr, funcCode, startAddr, regCount, out respCRC);
                response.DataLength = (regCount * 2).ToString();
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.ResponseCRC = respCRC;
            }
            catch (Exception ex)
            {
                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = ex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            
            return response;
            
        }

        public ModbusTCPResponseBody RequestModbusTCPData(ModbusTCPRequestBody request)
        {
            ModbusTCPResponseBody response = new ModbusTCPResponseBody();
            response.ServerID = request.ServerID;
            response.DeviceAddr = request.DeviceAddr;
            response.FunctionCode = request.FunctionCode;
            response.ErrorMessage = string.Empty;
            response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            ModbusTcpClientHost mbTcpClientHost = HostContainer.GetModbusTcpClientHostByServerID(request.ServerID);
            if (mbTcpClientHost == null)
            {
                response.ErrorMessage = string.Format("数据采集服务未启动或未成功连接至Modbus TCP数据服务器");
                response.Status = "0";
                return response;
            }

            response.DeviceAddr = mbTcpClientHost.DevAddr.ToString();

            ushort startAddr;
            if (!ushort.TryParse(request.StartAddr, out startAddr))
            {
                response.ErrorMessage = string.Format("起始地址[{0}]无效", request.StartAddr);
                response.Status = "0";
                return response;
            }

            ushort regCount;
            if (!ushort.TryParse(request.RegCount, out regCount))
            {
                response.ErrorMessage = string.Format("读取地址数量[{0}]无效", request.RegCount);
                response.Status = "0";
                return response;
            }

            string returnFormat = "string";
            if (request.ReturnFormat != null && request.ReturnFormat.ToLower().Trim() == "json")
            {
                returnFormat = "json";
            }

            byte functCode;
            if (!byte.TryParse(request.FunctionCode, out functCode))
            {
                response.ErrorMessage = string.Format("功能代码[{0}]无效", request.FunctionCode);
                response.Status = "0";
                return response;
            }            

            try
            {
                //string respCRC = string.Empty;
                switch (functCode)
                {
                    case 1:
                        response.DataContent = mbTcpClientHost.RequestModbusTcpCoilStatus(startAddr, regCount, returnFormat);
                        response.DataLength = (regCount * 2).ToString();
                        break;
                    case 3:
                        response.DataContent = mbTcpClientHost.RequestModbusTcpData(startAddr, regCount, returnFormat);
                        response.DataLength = (regCount * 2).ToString();
                        break;
                    default:
                        break;
                }
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = "1";
            }
            catch (Exception ex)
            {
                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = ex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = "0";
            }

            return response;
        }

        public ModbusTCPResponseBody RequestModbusTCPMultiCoilData(ModbusTCPRequestBody request)
        {
            request.FunctionCode = "1";

            return RequestModbusTCPData(request);
        }

        public ModbusTCPResponseBody RequestModbusTCPMultiRegData(ModbusTCPRequestBody request)
        {
            request.FunctionCode = "3";

            return RequestModbusTCPData(request);
        }

        public ModbusTCPResponseBody WriteModbusTCPCoilStatus(ModbusTCPWriteDataBody writeInfo)
        {
            ModbusTCPResponseBody response = new ModbusTCPResponseBody();
            response.ServerID = writeInfo.ServerID;
            response.DeviceAddr = writeInfo.DeviceAddr;
            response.ErrorMessage = string.Empty;
            response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            response.DataContent = string.Empty;


            ModbusTcpClientHost mbTcpClientHost = HostContainer.GetModbusTcpClientHostByServerID(writeInfo.ServerID);
            if (mbTcpClientHost == null)
            {
                response.ErrorMessage = string.Format("数据采集服务未启动或未成功连接至Modbus TCP数据服务器");
                response.Status = "0";
                return response;
            }

            response.DeviceAddr = mbTcpClientHost.DevAddr.ToString();

            try
            {
                mbTcpClientHost.WriteModbusTCPCoilStatus(writeInfo.WriteData);
                response.Status = "1";
            }
            catch (Exception ex)
            {
                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = ex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = "0";
            }
            return response;
        }

        public ModbusTCPResponseBody WriteModbusTCPData(ModbusTCPWriteDataBody writeInfo)
        {
            ModbusTCPResponseBody response = new ModbusTCPResponseBody();
            response.ServerID = writeInfo.ServerID;
            response.DeviceAddr = writeInfo.DeviceAddr;
            response.ErrorMessage = string.Empty;
            response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            response.DataContent = string.Empty;


            ModbusTcpClientHost mbTcpClientHost = HostContainer.GetModbusTcpClientHostByServerID(writeInfo.ServerID);
            if (mbTcpClientHost == null)
            {
                response.ErrorMessage = string.Format("数据采集服务未启动或未成功连接至Modbus TCP数据服务器");
                response.Status = "0";
                return response;
            }

            response.DeviceAddr = mbTcpClientHost.DevAddr.ToString();

            try
            {
                mbTcpClientHost.WriteModbusTCPData(writeInfo.WriteData);
                response.Status = "1";
            }
            catch (Exception ex)
            {
                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = ex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = "0";
            }
            return response;
        }
    }
}
