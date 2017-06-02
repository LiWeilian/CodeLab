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
    public class TCPServerHost
    {
        public string ServerID { get; set; }
        private IPAddress server_ip;
        private ushort server_port;
        private Socket serverSocket = null;
        private Socket clientSocket = null;
        private string clientSocketEndPointInfo = string.Empty;
        private uint waitTime = 500;
        public TCPServerHost(string server_id, IPAddress server_ip,
            ushort server_port, uint waitTime)
        {
            ServerID = server_id;
            this.server_ip = server_ip;
            this.server_port = server_port;
            this.waitTime = waitTime;
        }

        public void Run()
        {
            serverSocket = null;
            clientSocket = null;
            try
            {
                ServiceLog.LogServiceMessage(string.Format("正在启动数据采集服务[{0} {1}:{2}]", ServerID, server_ip, server_port));
                IPEndPoint endPoint = new IPEndPoint(server_ip, server_port);
                serverSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);
                ServiceLog.LogServiceMessage(string.Format("启动数据采集服务[{0} {1}:{2}]成功，正在接收连接...", ServerID, server_ip, server_port));
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("启动数据采集服务[{0} {1}:{2}]发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
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

            byte[] modbusRtuResponse = new byte[5 + regCount * 2];
            try
            {
                ServiceLog.LogServiceMessage(string.Format("请求报文：{0}\r\n发送端：[{1} {2}:{3}]\r\n接收端：{4}",
                    BitConverter.ToString(modbusRtuReq), ServerID, server_ip, server_port, clientSocketEndPointInfo));

                clientSocket.Send(modbusRtuReq);           
                clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);

                ServiceLog.LogServiceMessage(string.Format("回应报文：{0}\r\n发送端：{1}\r\n接收端：[{2} {3}:{4}]",
                    BitConverter.ToString(modbusRtuResponse), clientSocketEndPointInfo, ServerID, server_ip, server_port));

                mbRtuData = BitConverter.ToString(modbusRtuResponse, 3, regCount * 2).Replace("-", string.Empty);
                respCRC = BitConverter.ToString(modbusRtuResponse, 3 + regCount * 2, 2).Replace("-", string.Empty);
            }
            catch (Exception ex)
            {
                //可能客户端连接断开，重试一次
                ServiceLog.LogServiceMessage(string.Format("数据采集服务[{0} {1}:{2}]与[{3}]连接发生错误：{4}\r\n重试连接", 
                    ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                if (clientSocket != null && clientSocket.Connected)
                {
                    clientSocket.Disconnect(false);
                }
                clientSocket = null;
                
                try
                {

                    Thread.Sleep((int)waitTime);
                    ServiceLog.LogServiceMessage(string.Format("请求报文：{0}\r\n发送端：[{1} {2}:{3}]\r\n接收端：{4}",
                        BitConverter.ToString(modbusRtuReq), ServerID, server_ip, server_port, clientSocketEndPointInfo));

                    clientSocket.Send(modbusRtuReq);
                    clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);

                    ServiceLog.LogServiceMessage(string.Format("回应报文：{0}\r\n发送端：{1}\r\n接收端：[{2} {3}:{4}]",
                        BitConverter.ToString(modbusRtuResponse), clientSocketEndPointInfo, ServerID, server_ip, server_port));


                    mbRtuData = BitConverter.ToString(modbusRtuResponse, 3, regCount * 2).Replace("-", string.Empty);
                    respCRC = BitConverter.ToString(modbusRtuResponse, 3 + regCount * 2, 2).Replace("-", string.Empty);
                }
                catch (Exception)
                {
                    ServiceLog.LogServiceMessage(string.Format("数据采集服务[{0} {1}:{2}]与[{3}]连接发生错误：{4}",
                        ServerID, server_ip, server_port, clientSocketEndPointInfo, ex.Message));
                    if (clientSocket != null && clientSocket.Connected)
                    {
                        clientSocket.Disconnect(false);
                    }
                    clientSocket = null;
                    throw new Exception(ex.Message);
                }
                
            }


            return mbRtuData;
        }

        private Socket AcceptConnection()
        {
            if (clientSocket == null || !clientSocket.Connected)
            {
                try
                {
                    clientSocket = serverSocket.Accept();
                    ServiceLog.LogServiceMessage(string.Format("数据采集服务[{0} {1}:{2}]接收到连接[{3}]", ServerID, server_ip, server_port, clientSocket.RemoteEndPoint));
                    HostContainer.AddTcpServerHost(this);
                    clientSocketEndPointInfo = clientSocket.RemoteEndPoint.ToString();
                    return clientSocket;
                }
                catch (Exception ex)
                {
                    clientSocket = null;
                    ServiceLog.LogServiceMessage(string.Format("数据采集服务[{0} {1}:{2}]接收连接发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                    return null;
                }
            } else
            {
                return clientSocket;
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
