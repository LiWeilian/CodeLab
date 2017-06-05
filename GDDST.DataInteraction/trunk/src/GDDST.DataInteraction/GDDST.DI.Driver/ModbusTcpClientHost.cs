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
    public class ModbusTcpClientHost
    {
        public string ServerID { get; set; }
        private IPAddress server_ip;
        private ushort server_port;
        private uint waitTime = 500;
        private Socket clientSocket = null;
        private ushort identifier = 0;
        private ushort GetIdentifier()
        {
            if (identifier < 65535)
            {
                identifier++;
            } else
            {
                identifier = 1;
            }
            return identifier;
        }
        public ModbusTcpClientHost(string server_id, IPAddress server_ip, ushort server_port, uint waitTime)
        {
            ServerID = server_id;
            this.server_ip = server_ip;
            this.server_port = server_port;
            this.waitTime = waitTime;
        }

        public void Run()
        {
            try
            {
                ServiceLog.LogServiceMessage(string.Format("正在建立 Modbus TCP 数据采集连接[{0} {1}:{2}]", ServerID, server_ip, server_port));
                IPEndPoint endPoint = new IPEndPoint(server_ip, server_port);
                clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect(endPoint);
                ServiceLog.LogServiceMessage(string.Format("建立 Modbus TCP 数据采集连接[{0} {1}:{2}]成功...", ServerID, server_ip, server_port));
                HostContainer.AddModbusTcpClientHost(this);
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("建立 Modbus TCP 数据采集连接[{0} {1}:{2}]发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                return;
            }
        }

        public string RequestModbusTcpData(byte devAddr,
            byte funcCode,
            ushort startAddr,
            ushort regCount)
        {
            string mbTcpData = string.Empty;

            byte[] mbTcpSend = new byte[12];
            //标识符
            ushort identifier = GetIdentifier();
            byte[] bIdentifier = BitConverter.GetBytes(identifier);
            mbTcpSend[0] = bIdentifier[1];
            mbTcpSend[1] = bIdentifier[0];
            //Modbus Tcp 协议
            mbTcpSend[2] = 0;
            mbTcpSend[3] = 0;
            //请求报文长度
            mbTcpSend[4] = 0;
            mbTcpSend[5] = 6;
            //设备地址
            mbTcpSend[6] = devAddr;
            //功能代码
            mbTcpSend[7] = funcCode;
            //起始寄存器
            byte[] bStartAddr = BitConverter.GetBytes(startAddr);
            mbTcpSend[8] = bStartAddr[1];
            mbTcpSend[9] = bStartAddr[0];
            //寄存器数量
            byte[] bRegCount = BitConverter.GetBytes(regCount);
            mbTcpSend[10] = bRegCount[1];
            mbTcpSend[11] = bRegCount[0];

            try
            {
                ServiceLog.LogServiceMessage(string.Format("正在发送请求到 Modbus TCP 服务器[{0} {1}:{2}]\r\n请求报文内容：{3}",
                    ServerID, server_ip, server_port, BitConverter.ToString(mbTcpSend)));
                clientSocket.Send(mbTcpSend, mbTcpSend.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("发送请求到 Modbus TCP 服务器[{0} {1}:{2}]连接发生错误：{3}\r\n请求报文内容：{4}",
                        ServerID, server_ip, server_port, ex.Message, BitConverter.ToString(mbTcpSend)));
                throw new Exception(string.Format("发送请求到 Modbus TCP 服务器时发生错误：{0}", ex.Message));
            }

            try
            {
                byte[] mbTcpRecv = new byte[9 + regCount * 2];
                clientSocket.Receive(mbTcpRecv, mbTcpRecv.Length, SocketFlags.None);
                ServiceLog.LogServiceMessage(string.Format("接收到 Modbus TCP 服务器[{0} {1}:{2}]回应\r\n回应报文内容：{3}",
                    ServerID, server_ip, server_port, BitConverter.ToString(mbTcpRecv)));

                mbTcpData = BitConverter.ToString(mbTcpRecv, 9, regCount * 2).Replace("-", string.Empty);
            }
            catch (Exception ex)
            {
                ServiceLog.LogServiceMessage(string.Format("接收 Modbus TCP 服务器[{0} {1}:{2}]回应时发生错误：{3}",
                    ServerID, server_ip, server_port, ex.Message));
                throw new Exception(string.Format("接收 Modbus TCP 服务器回应时发生错误：{0}", ex.Message));
            }
            return mbTcpData;
        }
    }
}
