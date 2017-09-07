using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using GDDST.DI.Utils;

namespace GDDST.DI.Driver
{
    public class ModbusRtuTcpServerHost
    {
        public string ServerID { get; set; }
        private IPAddress server_ip;
        private ushort server_port;
        private Socket serverSocket = null;
        private Socket clientSocket = null;
        private string clientSocketEndPointInfo = string.Empty;
        private uint waitTime = 500;
        private int retryTimes = 3;
        public ModbusRtuTcpServerHost(string server_id, IPAddress server_ip,
            ushort server_port, uint waitTime, int retryTimes)
        {
            ServerID = server_id;
            this.server_ip = server_ip;
            this.server_port = server_port;
            this.waitTime = waitTime;
            this.retryTimes = retryTimes;
        }

        public void Run()
        {
            serverSocket = null;
            clientSocket = null;
            try
            {
                ServiceLog.Info(string.Format("正在启动 Modbus RTU 数据采集服务[{0} {1}:{2}]", ServerID, server_ip, server_port));
                IPEndPoint endPoint = new IPEndPoint(server_ip, server_port);
                serverSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);
                ServiceLog.Info(string.Format("启动 Modbus RTU 数据采集服务[{0} {1}:{2}]成功，正在接收连接...", ServerID, server_ip, server_port));
            }
            catch (Exception ex)
            {
                ServiceLog.Error(string.Format("启动 Modbus RTU 数据采集服务[{0} {1}:{2}]发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                return;
            }
            
            while (true)
            {
                Thread.Sleep(100);
                clientSocket = AcceptConnection();
            }
            
        }

        public string RequestModbusRTUData(byte devAddr, 
            byte funcCode, 
            ushort startAddr, 
            ushort regCount,
            out string respCRC)
        {
            string mbRtuData = string.Empty;
            respCRC = string.Empty;

            byte[] temp = new byte[6];
            temp[0] = devAddr;
            temp[1] = funcCode;

            byte[] bStartAddr = BitConverter.GetBytes(startAddr);
            temp[2] = bStartAddr[1];
            temp[3] = bStartAddr[0];

            byte[] bRegCount = BitConverter.GetBytes(regCount);
            temp[4] = bRegCount[1];
            temp[5] = bRegCount[0];

            uint iCRC16 = ModbusCRC16(temp, temp.Length);
            byte[] bCrc16 = BitConverter.GetBytes(iCRC16);

            byte[] modbusRtuReq = new byte[8];
            modbusRtuReq[0] = temp[0];
            modbusRtuReq[1] = temp[1];
            modbusRtuReq[2] = temp[2];
            modbusRtuReq[3] = temp[3];
            modbusRtuReq[4] = temp[4];
            modbusRtuReq[5] = temp[5];
            //CRC16 低位在前，高位在后
            modbusRtuReq[6] = bCrc16[0];
            modbusRtuReq[7] = bCrc16[1];

            int retryTimes = 0;

            while (retryTimes <= this.retryTimes)
            {
                try
                {
                    ServiceLog.Debug(string.Format("请求报文：{0}\r\n发送端：[{1} {2}:{3}]\r\n接收端：{4}",
                    BitConverter.ToString(modbusRtuReq), ServerID, server_ip, server_port, clientSocketEndPointInfo));

                    clientSocket.Send(modbusRtuReq);
                }
                catch (Exception ex)
                {
                    ServiceLog.Error(string.Format("数据采集服务[{0} {1}:{2}]发送请求到[{3}]时连接发生错误：{4}\r\n重试连接",
                        ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                    if (retryTimes >= this.retryTimes)
                    {
                        throw new Exception(string.Format("数据采集服务[{0} {1}:{2}]发送请求到[{3}]时连接发生错误：{4}",
                            ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                    }

                    retryTimes++;
                    ReconnectToServer();
                    continue;
                }

                byte[] modbusRtuResponse = new byte[5 + regCount * 2];
                try
                {
                    clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);

                    ServiceLog.Debug(string.Format("回应报文：{0}\r\n发送端：{1}\r\n接收端：[{2} {3}:{4}]",
                        BitConverter.ToString(modbusRtuResponse), clientSocketEndPointInfo, ServerID, server_ip, server_port));

                    mbRtuData = BitConverter.ToString(modbusRtuResponse, 3, regCount * 2).Replace("-", string.Empty);
                    respCRC = BitConverter.ToString(modbusRtuResponse, 3 + regCount * 2, 2).Replace("-", string.Empty);
                }
                catch (Exception ex)
                {
                    ServiceLog.Error(string.Format("数据采集服务[{0} {1}:{2}]接收[{3}]回应时连接发生错误：{4}\r\n重试连接",
                        ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                    if (retryTimes >= this.retryTimes)
                    {
                        throw new Exception(string.Format("数据采集服务[{0} {1}:{2}]接收[{3}]回应时连接发生错误：{4}",
                            ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                    }

                    retryTimes++;
                    ReconnectToServer();
                    continue;
                }

                break;
            }   
            /*
            byte[] modbusRtuResponse = new byte[5 + regCount * 2];
            try
            {
                ServiceLog.Debug(string.Format("请求报文：{0}\r\n发送端：[{1} {2}:{3}]\r\n接收端：{4}",
                    BitConverter.ToString(modbusRtuReq), ServerID, server_ip, server_port, clientSocketEndPointInfo));

                clientSocket.Send(modbusRtuReq);           
                clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);

                ServiceLog.Debug(string.Format("回应报文：{0}\r\n发送端：{1}\r\n接收端：[{2} {3}:{4}]",
                    BitConverter.ToString(modbusRtuResponse), clientSocketEndPointInfo, ServerID, server_ip, server_port));

                mbRtuData = BitConverter.ToString(modbusRtuResponse, 3, regCount * 2).Replace("-", string.Empty);
                respCRC = BitConverter.ToString(modbusRtuResponse, 3 + regCount * 2, 2).Replace("-", string.Empty);
            }
            catch (Exception ex)
            {
                //可能客户端连接断开，重试一次
                ServiceLog.Error(string.Format("数据采集服务[{0} {1}:{2}]与[{3}]连接发生错误：{4}\r\n重试连接", 
                    ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                if (clientSocket != null && clientSocket.Connected)
                {
                    clientSocket.Disconnect(false);
                }
                clientSocket = null;
                
                try
                {

                    Thread.Sleep((int)waitTime);
                    ServiceLog.Debug(string.Format("请求报文：{0}\r\n发送端：[{1} {2}:{3}]\r\n接收端：{4}",
                        BitConverter.ToString(modbusRtuReq), ServerID, server_ip, server_port, clientSocketEndPointInfo));

                    clientSocket.Send(modbusRtuReq);
                    clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);

                    ServiceLog.Debug(string.Format("回应报文：{0}\r\n发送端：{1}\r\n接收端：[{2} {3}:{4}]",
                        BitConverter.ToString(modbusRtuResponse), clientSocketEndPointInfo, ServerID, server_ip, server_port));


                    mbRtuData = BitConverter.ToString(modbusRtuResponse, 3, regCount * 2).Replace("-", string.Empty);
                    respCRC = BitConverter.ToString(modbusRtuResponse, 3 + regCount * 2, 2).Replace("-", string.Empty);
                }
                catch (Exception)
                {
                    string errMsg = string.Format("数据采集服务[{0} {1}:{2}]与[{3}]连接发生错误：{4}",
                        ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message);
                    ServiceLog.Error(errMsg);
                    if (clientSocket != null && clientSocket.Connected)
                    {
                        clientSocket.Disconnect(false);
                    }
                    clientSocket = null;
                    string exceptMsg = string.Format("数据采集服务与远程数据服务器连接发生错误：{0}",
                        ex.Message);
                    throw new Exception(errMsg);
                }
                
            }
            */

            return mbRtuData;
        }

        private Socket AcceptConnection()
        {
            if (clientSocket == null || !clientSocket.Connected)
            {
                try
                {
                    clientSocket = serverSocket.Accept();
                    ServiceLog.Info(string.Format("数据采集服务[{0} {1}:{2}]接收到连接[{3}]", ServerID, server_ip, server_port, clientSocket.RemoteEndPoint));
                    HostContainer.AddTcpServerHost(this);
                    clientSocketEndPointInfo = clientSocket.RemoteEndPoint.ToString();
                    return clientSocket;
                }
                catch (Exception ex)
                {
                    clientSocket = null;
                    ServiceLog.Error(string.Format("数据采集服务[{0} {1}:{2}]接收连接发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                    return null;
                }
            } else
            {
                return clientSocket;
            }
        }

        private void ReconnectToServer()
        {
            try
            {
                ServiceLog.Info(string.Format("正在重新建立 Modbus TCP 数据采集连接[{0} {1}:{2}]", ServerID, server_ip, server_port));
                clientSocket = AcceptConnection();
                ServiceLog.Info(string.Format("重新建立 Modbus TCP 数据采集连接[{0} {1}:{2}]成功...", ServerID, server_ip, server_port));
            }
            catch (Exception ex)
            {
                ServiceLog.Error(string.Format("重新建立 Modbus TCP 数据采集连接[{0} {1}:{2}]发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                return;
            }
        }

        private uint ModbusCRC16(byte[] modbusData, int length)
        {
            uint crc16 = 0xFFFF;

            for (int i = 0; i < length; i++)
            {
                crc16 ^= modbusData[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc16 & 0x01) == 1)
                    {
                        crc16 = (crc16 >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }
    }
}
