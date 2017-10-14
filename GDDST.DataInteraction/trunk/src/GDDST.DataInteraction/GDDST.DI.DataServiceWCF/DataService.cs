using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Web.Script.Serialization;

using GDDST.DI.Driver;
using GDDST.DI.Utils;

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
                response.Status = "0";
                return response;
            }

            byte devAddr;
            if (!byte.TryParse(request.DeviceAddr, out devAddr))
            {
                response.ErrorMessage = string.Format("设备地址[{0}]无效", request.DeviceAddr);
                response.Status = "0";
                return response;
            }

            byte funcCode;
            if (!byte.TryParse(request.FunctionCode, out funcCode))
            {
                response.ErrorMessage = string.Format("功能代码[{0}]无效", request.FunctionCode);
                response.Status = "0";
                return response;
            }

            ushort startAddr;
            if (!ushort.TryParse(request.StartAddr, out startAddr))
            {
                response.ErrorMessage = string.Format("起始寄存器地址[{0}]无效", request.StartAddr);
                response.Status = "0";
                return response;
            }

            ushort regCount;
            if (!ushort.TryParse(request.RegCount, out regCount))
            {
                response.ErrorMessage = string.Format("读取寄存器数量[{0}]无效", request.RegCount);
                response.Status = "0";
                return response;
            }
            
            try
            {
                string respCRC = string.Empty;
                response.DataContent = tcpServerHost.RequestModbusRTUData(devAddr, funcCode, startAddr, regCount, out respCRC);
                response.DataLength = (regCount * 2).ToString();
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.ResponseCRC = respCRC;
                response.Status = "1";
            }
            catch (Exception ex)
            {
                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = ex.Message;
                response.Status = "0";
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
                response.Status = "3";
                return response;
            }

            response.DeviceAddr = mbTcpClientHost.DevAddr.ToString();

            string returnFormat = "string";
            if (request.ReturnFormat != null && request.ReturnFormat.ToLower().Trim() == "json")
            {
                returnFormat = "json";
            }

            byte functCode;
            if (!byte.TryParse(request.FunctionCode, out functCode))
            {
                response.ErrorMessage = string.Format("功能代码[{0}]无效", request.FunctionCode);
                response.Status = "2";
                return response;
            }
            

            List<ModbusTCPRequestAddress> addrs = new List<ModbusTCPRequestAddress>();

            if (request.RequestAddrs != null)
            {
                try
                {

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    //ModbusTCPRequestAddress addr = (ModbusTCPRequestAddress)jss.Deserialize(request.RequestAddrs, typeof(ModbusTCPRequestAddress));
                    //addrs.Add(addr);
                    addrs = jss.Deserialize<List<ModbusTCPRequestAddress>>(request.RequestAddrs);
                }
                catch (Exception)
                {
                }
                /*
                for (int i = 0; i < request.RequestAddrs.Length; i++)
                {
                    ModbusTCPRequestAddress addr = new ModbusTCPRequestAddress();
                    addr.StartAddr = request.RequestAddrs[i].StartAddr;
                    addr.RegCount = request.RequestAddrs[i].RegCount;
                    addrs.Add(addr);
                }
                */
            } else
            {
                ModbusTCPRequestAddress addr = new ModbusTCPRequestAddress();
                addr.StartAddr = request.StartAddr;
                addr.RegCount = request.RegCount;
                addrs.Add(addr);

                /*
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
                */
            }          

            try
            {
                //string respCRC = string.Empty;

                string dataContent = string.Empty;
                string tempDataContent = string.Empty;
                int dataLen = 0;
                int tempDataLen = 0;

                foreach (ModbusTCPRequestAddress addr in addrs)
                {
                    switch (functCode)
                    {
                        case 1:
                            tempDataContent = mbTcpClientHost.RequestModbusTcpCoilStatus(addr.StartAddr, addr.RegCount, returnFormat);
                            tempDataLen = addr.RegCount * 2;
                            break;
                        case 3:
                            tempDataContent = mbTcpClientHost.RequestModbusTcpData(addr.StartAddr, addr.RegCount, returnFormat);
                            tempDataLen = addr.RegCount * 2;
                            break;
                        default:
                            break;
                    }

                    if (dataContent == string.Empty)
                    {
                        dataContent = tempDataContent;
                    } else
                    {
                        dataContent = dataContent + "," + tempDataContent;
                    }

                    dataLen = dataLen + tempDataLen;
                }

                response.DataContent = dataContent;
                response.DataLength = dataLen.ToString();
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = "1";
            }
            catch (CustomException cex)
            {
                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = cex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = ((int)cex.ExceptionCode).ToString();
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

        public ModbusTCPResponseBody RequestModbusTCPMultiCoilDataWithText(string request)
        {
            throw new NotImplementedException();
        }

        public ModbusTCPResponseBody RequestModbusTCPMultiRegData(ModbusTCPRequestBody request)
        {
            request.FunctionCode = "3";

            return RequestModbusTCPData(request);
        }

        public ModbusTCPResponseBody RequestModbusTCPMultiRegDataWithText(object request)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                ModbusTCPRequestBody requestBody = (ModbusTCPRequestBody)jss.Deserialize((string)request, typeof(ModbusTCPRequestBody));

                if (requestBody != null)
                {
                    requestBody.FunctionCode = "3";
                    return RequestModbusTCPData(requestBody);
                }
                else
                {
                    ModbusTCPResponseBody response = new ModbusTCPResponseBody();
                    response.ServerID = string.Empty;
                    response.DeviceAddr = string.Empty;
                    response.ErrorMessage = "传入参数格式无法解析";
                    response.Status = "2";
                    response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    response.DataContent = string.Empty;

                    return response;
                }
            }
            catch (Exception ex)
            {
                ModbusTCPResponseBody response = new ModbusTCPResponseBody();
                response.ServerID = string.Empty;
                response.DeviceAddr = string.Empty;
                response.ErrorMessage = string.Format("传入参数格式无法解析，错误信息：{0}", ex.Message);
                response.Status = "2";
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.DataContent = string.Empty;

                return response;
            }
            
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
            catch (CustomException cex)
            {

                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = cex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = ((int)cex.ExceptionCode).ToString();
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
            catch (CustomException cex)
            {

                response.DataContent = null;
                response.DataLength = "0";
                response.ErrorMessage = cex.Message;
                response.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                response.Status = ((int)cex.ExceptionCode).ToString();
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
