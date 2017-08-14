using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web.Script.Serialization;

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
        private int retryTimes = 3;
        public byte DevAddr { get; set; }
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
        public ModbusTcpClientHost(string server_id, IPAddress server_ip, ushort server_port, byte devAddr, uint waitTime, int retryTimes)
        {
            ServerID = server_id;
            this.server_ip = server_ip;
            this.server_port = server_port;
            this.DevAddr = devAddr;
            this.waitTime = waitTime;
            this.retryTimes = retryTimes;
        }

        public void Run()
        {
            try
            {
                ServiceLog.Info(string.Format("正在建立 Modbus TCP 数据采集连接[{0} {1}:{2}]", ServerID, server_ip, server_port));
                clientSocket = ConnectToServer();
                ServiceLog.Info(string.Format("建立 Modbus TCP 数据采集连接[{0} {1}:{2}]成功...", ServerID, server_ip, server_port));
                HostContainer.AddModbusTcpClientHost(this);
            }
            catch (Exception ex)
            {
                ServiceLog.Error(string.Format("建立 Modbus TCP 数据采集连接[{0} {1}:{2}]发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                return;
            }
        }

        private Socket ConnectToServer()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(server_ip, server_port);
                Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(endPoint);
                return socket;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ReconnectToServer()
        {
            try
            {
                ServiceLog.Info(string.Format("正在重新建立 Modbus TCP 数据采集连接[{0} {1}:{2}]", ServerID, server_ip, server_port));
                clientSocket = ConnectToServer();
                ServiceLog.Info(string.Format("重新建立 Modbus TCP 数据采集连接[{0} {1}:{2}]成功...", ServerID, server_ip, server_port));
            }
            catch (Exception ex)
            {
                ServiceLog.Error(string.Format("重新建立 Modbus TCP 数据采集连接[{0} {1}:{2}]发生错误：{3}", ServerID, server_ip, server_port, ex.Message));
                return;
            }
        }

        private string CheckExceptionCode(byte[] recvBytes)
        {
            string errMsg = string.Empty;
            if (recvBytes.Length < 9)
            {

            } else
            {
                if (recvBytes[5] == 0x03 && recvBytes[6] == this.DevAddr)
                {
                    switch (recvBytes[7])
                    {
                        case 0x81:
                        case 0x83:
                        case 0x8f:
                        case 0x90:
                            switch (recvBytes[8])
                            {
                                case 0x01:
                                    errMsg = "错误代码[0x01]，错误的请求类型";
                                    break;
                                case 0x02:
                                    errMsg = "错误代码[0x02]，访问了非法地址";
                                    break;
                                default:
                                    errMsg = "其他错误";
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    
                }
            }

            return errMsg;
        }

        private string CheckIsNullData(byte[] recvBytes)
        {
            string errMsg = string.Empty;
            for (int i = 0; i < recvBytes.Length; i++)
            {
                if (recvBytes[i] != 0)
                {
                    return string.Empty;
                }
            }

            errMsg = "返回数据为空";
            return errMsg;
        }

        public string RequestModbusTcpData(ushort startAddr,
            ushort regCount,
            string returnFormat)
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
            mbTcpSend[6] = this.DevAddr;
            //功能代码
            mbTcpSend[7] = 3;
            //起始寄存器
            byte[] bStartAddr = BitConverter.GetBytes(startAddr);
            mbTcpSend[8] = bStartAddr[1];
            mbTcpSend[9] = bStartAddr[0];
            //寄存器数量
            byte[] bRegCount = BitConverter.GetBytes(regCount);
            mbTcpSend[10] = bRegCount[1];
            mbTcpSend[11] = bRegCount[0];

            int retryTimes = 0;

            while (retryTimes <= this.retryTimes)
            {
                try
                {
                    ServiceLog.Debug(string.Format("正在发送请求到 Modbus TCP 服务器[{0} {1}:{2}]\r\n请求报文内容：{3}",
                        ServerID, server_ip, server_port, BitConverter.ToString(mbTcpSend)));
                    clientSocket.Send(mbTcpSend, mbTcpSend.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    ServiceLog.Error(string.Format("发送请求到 Modbus TCP 服务器[{0} {1}:{2}]连接发生错误：{3}\r\n请求报文内容：{4}",
                            ServerID, server_ip, server_port, ex.Message, BitConverter.ToString(mbTcpSend)));
                    if (retryTimes >= this.retryTimes)
                    {
                        throw new Exception(string.Format("发送请求到 Modbus TCP 服务器时发生错误：{0}", ex.Message));
                    }
                    retryTimes++;

                    ReconnectToServer();

                    continue;
                }

                byte[] mbTcpRecv = new byte[9 + regCount * 2];
                try
                {                    
                    clientSocket.Receive(mbTcpRecv, mbTcpRecv.Length, SocketFlags.None);
                    ServiceLog.Debug(string.Format("接收到 Modbus TCP 服务器[{0} {1}:{2}]回应\r\n回应报文内容：{3}",
                        ServerID, server_ip, server_port, BitConverter.ToString(mbTcpRecv)));
                }
                catch (Exception ex)
                {
                    ServiceLog.Error(string.Format("接收 Modbus TCP 服务器[{0} {1}:{2}]回应时发生错误：{3}",
                        ServerID, server_ip, server_port, ex.Message));
                    if (retryTimes >= this.retryTimes)
                    {
                        throw new Exception(string.Format("接收 Modbus TCP 服务器回应时发生错误：{0}", ex.Message));
                    }
                    retryTimes++;

                    ReconnectToServer();

                    continue;
                }

                //判断是否返回异常代码
                string errMsg = CheckExceptionCode(mbTcpRecv);
                if (errMsg != string.Empty)
                {
                    throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                }

                errMsg = CheckIsNullData(mbTcpRecv);
                if (errMsg != string.Empty)
                {
                    throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                }
                //

                mbTcpData = BitConverter.ToString(mbTcpRecv, 9, regCount * 2).Replace("-", string.Empty);

                break;
            }

            try
            {
                if (returnFormat.ToLower().Trim() == "json")
                {
                    string mbTcpDataJson = string.Empty;
                    for (int i = 0; i < regCount; i++)
                    {
                        if (i == 0)
                        {
                            mbTcpDataJson = string.Format("\"{0}\":\"{1}\"", startAddr + i, mbTcpData.Substring(i*4, 4));
                        }
                        else
                        {
                            mbTcpDataJson = string.Format("{0},\"{1}\":\"{2}\"", mbTcpDataJson, startAddr + i, mbTcpData.Substring(i*4, 4));
                        }
                    }

                    mbTcpData = string.Format("{{{0}}}", mbTcpDataJson);
                    //mbTcpData = mbTcpDataJson;
                }
            }
            catch (Exception ex)
            {
                ServiceLog.Error(string.Format("转换 Modbus TCP 服务器[{0} {1}:{2}]回应数据\r\n{3}\r\n时发生错误：{4}",
                        ServerID, server_ip, server_port, mbTcpData, ex.Message));
                if (retryTimes >= this.retryTimes)
                {
                    throw new Exception(string.Format("转换 Modbus TCP 服务器回应数据时发生错误：{0}", ex.Message));
                }
            }
            

            return mbTcpData;
        }

        public string RequestModbusTcpCoilStatus(ushort startAddr,
            ushort coilCount,
            string returnFormat)
        {
            string mbTcpStatus = string.Empty;

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
            mbTcpSend[6] = this.DevAddr;
            //功能代码
            mbTcpSend[7] = 1;
            //起始寄存器
            byte[] bStartAddr = BitConverter.GetBytes(startAddr);
            mbTcpSend[8] = bStartAddr[1];
            mbTcpSend[9] = bStartAddr[0];
            //寄存器数量
            byte[] bCoilCount = BitConverter.GetBytes(coilCount);
            mbTcpSend[10] = bCoilCount[1];
            mbTcpSend[11] = bCoilCount[0];

            byte iResultLen01 = (byte)(coilCount / 8);
            if (coilCount % 8 > 0)
            {
                iResultLen01++;
            }

            int retryTimes = 0;

            while (retryTimes <= this.retryTimes)
            {
                try
                {
                    ServiceLog.Debug(string.Format("正在发送请求到 Modbus TCP 服务器[{0} {1}:{2}]\r\n请求报文内容：{3}",
                        ServerID, server_ip, server_port, BitConverter.ToString(mbTcpSend)));
                    clientSocket.Send(mbTcpSend, mbTcpSend.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    ServiceLog.Error(string.Format("发送请求到 Modbus TCP 服务器[{0} {1}:{2}]连接发生错误：{3}\r\n请求报文内容：{4}",
                            ServerID, server_ip, server_port, ex.Message, BitConverter.ToString(mbTcpSend)));
                    if (retryTimes >= this.retryTimes)
                    {
                        throw new Exception(string.Format("发送请求到 Modbus TCP 服务器时发生错误：{0}", ex.Message));
                    }
                    retryTimes++;

                    ReconnectToServer();

                    continue;
                }

                byte[] mbTcpRecv = new byte[9 + iResultLen01];
                try
                {
                    clientSocket.Receive(mbTcpRecv, mbTcpRecv.Length, SocketFlags.None);
                    ServiceLog.Debug(string.Format("接收到 Modbus TCP 服务器[{0} {1}:{2}]回应\r\n回应报文内容：{3}",
                        ServerID, server_ip, server_port, BitConverter.ToString(mbTcpRecv)));

                }
                catch (Exception ex)
                {
                    ServiceLog.Error(string.Format("接收 Modbus TCP 服务器[{0} {1}:{2}]回应时发生错误：{3}",
                        ServerID, server_ip, server_port, ex.Message));
                    if (retryTimes >= this.retryTimes)
                    {
                        throw new Exception(string.Format("接收 Modbus TCP 服务器回应时发生错误：{0}", ex.Message));
                    }
                    retryTimes++;

                    ReconnectToServer();

                    continue;
                }

                //判断是否返回异常代码
                string errMsg = CheckExceptionCode(mbTcpRecv);
                if (errMsg != string.Empty)
                {
                    throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                }

                errMsg = CheckIsNullData(mbTcpRecv);
                if (errMsg != string.Empty)
                {
                    throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                }
                //

                mbTcpStatus = BitConverter.ToString(mbTcpRecv, 9, iResultLen01).Replace("-", string.Empty);

                break;
            }

            try
            {
                
                if (returnFormat.ToLower().Trim() == "json")
                {
                    string statusArray = string.Empty;
                    for (int i = 0; i < iResultLen01; i++)
                    {
                        string sByte = mbTcpStatus.Substring(i * 2, 2);
                        byte bByte = Convert.ToByte(sByte, 16);
                        string status = Convert.ToString(bByte, 2).PadLeft(8, '0');

                        statusArray = statusArray + getReverse(status);
                    }

                    string mbTcpStatusJson = string.Empty;
                    for (int i = 0; i < coilCount; i++)
                    {
                        if (i == 0)
                        {
                            mbTcpStatusJson = string.Format("\"{0}\":\"{1}\"", startAddr + i, statusArray.Substring(i, 1));
                        }
                        else
                        {
                            mbTcpStatusJson = string.Format("{0},\"{1}\":\"{2}\"", mbTcpStatusJson, startAddr + i, statusArray.Substring(i, 1));
                        }
                    }

                    mbTcpStatus = string.Format("{{{0}}}", mbTcpStatusJson);
                }
                
            }
            catch (Exception ex)
            {
                ServiceLog.Error(string.Format("转换 Modbus TCP 服务器[{0} {1}:{2}]回应数据\r\n{3}\r\n时发生错误：{4}",
                        ServerID, server_ip, server_port, mbTcpStatus, ex.Message));
                if (retryTimes >= this.retryTimes)
                {
                    throw new Exception(string.Format("转换 Modbus TCP 服务器回应数据时发生错误：{0}", ex.Message));
                }
            }

            return mbTcpStatus;
        }

        public void WriteModbusTCPData(string writeData)
        {
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> jarr = js.Deserialize<Dictionary<string, object>>(writeData);
            foreach (KeyValuePair<string, object> j in jarr)
            {
                ushort startAddr;
                if (!ushort.TryParse(j.Key, out startAddr))
                {
                    return;
                }

                ushort regCount;
                switch (j.Value.ToString().Length)
                {
                    case 4:
                        regCount = 1;
                        break;
                    case 8:
                        regCount = 2;
                        break;
                    default:
                        return;
                }

                byte[] inputValues16Byte = new byte[regCount * 2];
                for (int i = 0; i < inputValues16Byte.Length; i++)
                {
                    string sValue = j.Value.ToString().Substring(i * 2, 2);
                    inputValues16Byte[i] = Convert.ToByte(sValue, 16);
                }

                /*
                * 00：传输标志Hi
                * 01：传输标志Lo
                * 02、03：协议标志
                * 04、05：此位置以后字节数
                * 06：单元标志
                * 07：功能代码，0x10表示写多个寄存器
                * 08、9：起始寄存器
                * 10、11：寄存器数量
                * 12：数据字节数
                * 13~：数据
                */

                byte[] plc_send16 = new byte[13 + regCount * 2];
                //标识符
                ushort identifier = GetIdentifier();
                byte[] bIdentifier = BitConverter.GetBytes(identifier);
                plc_send16[0] = bIdentifier[1];
                plc_send16[1] = bIdentifier[0];
                plc_send16[2] = 0;
                plc_send16[3] = 0;
                plc_send16[4] = 0;
                plc_send16[5] = (byte)(7 + regCount * 2);
                plc_send16[6] = this.DevAddr;
                plc_send16[7] = 16;
                byte[] bRegAddr16 = BitConverter.GetBytes(startAddr);
                plc_send16[8] = bRegAddr16[1];
                plc_send16[9] = bRegAddr16[0];
                byte[] bRegCount16 = BitConverter.GetBytes(regCount);
                plc_send16[10] = bRegCount16[1];
                plc_send16[11] = bRegCount16[0];
                byte byteCount = (byte)(regCount * 2);
                plc_send16[12] = byteCount;

                for (int i = 0; i < inputValues16Byte.Length; i++)
                {
                    plc_send16[13 + i] = inputValues16Byte[i];
                }

                for (int i = 0; i < plc_send16.Length; i++)
                {
                    Console.WriteLine(plc_send16[i]);
                }
                /*
                try
                {
                    clientSocket.Send(plc_send16, plc_send16.Length, SocketFlags.None);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                    break;
                }
                */

                int retryTimes = 0;

                while (retryTimes <= this.retryTimes)
                {
                    try
                    {
                        ServiceLog.Debug(string.Format("正在写入数据到 Modbus TCP 服务器[{0} {1}:{2}]\r\n报文内容：{3}",
                            ServerID, server_ip, server_port, BitConverter.ToString(plc_send16)));
                        clientSocket.Send(plc_send16, plc_send16.Length, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        ServiceLog.Error(string.Format("写入数据到 Modbus TCP 服务器[{0} {1}:{2}]连接发生错误：{3}\r\n报文内容：{4}",
                                ServerID, server_ip, server_port, ex.Message, BitConverter.ToString(plc_send16)));
                        if (retryTimes >= this.retryTimes)
                        {
                            throw new Exception(string.Format("写入数据到 Modbus TCP 服务器时发生错误：{0}", ex.Message));
                        }
                        retryTimes++;

                        ReconnectToServer();

                        continue;
                    }

                    byte[] recMsgByte16 = new byte[12];
                    int recLen16;
                    try
                    {
                        //Thread.Sleep(5000);
                        recLen16 = clientSocket.Receive(recMsgByte16, recMsgByte16.Length, SocketFlags.None);
                        ServiceLog.Debug(string.Format("接收到 Modbus TCP 服务器[{0} {1}:{2}]数据\r\n报文内容：{3}",
                            ServerID, server_ip, server_port, BitConverter.ToString(recMsgByte16)));
                    }
                    catch (SocketException se)
                    {
                        ServiceLog.Debug(string.Format("接收消息失败：{0}", se.SocketErrorCode.ToString()));
                        break;
                    }

                    //判断是否返回异常代码

                    string errMsg = CheckExceptionCode(recMsgByte16);
                    if (errMsg != string.Empty)
                    {
                        throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                    }
                    errMsg = CheckIsNullData(recMsgByte16);
                    if (errMsg != string.Empty)
                    {
                        throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                    }
                    break;
                }
            }
        }

        public void WriteModbusTCPCoilStatus(string writeData)
        {
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> jarr = js.Deserialize<Dictionary<string, object>>(writeData);
            foreach (KeyValuePair<string, object> j in jarr)
            {
                ushort startAddr;
                if (!ushort.TryParse(j.Key, out startAddr))
                {
                    return;
                }

                ushort coilCount = 1;
                byte coilStatus;
                if (!byte.TryParse(j.Value.ToString(), out coilStatus) || (coilStatus != 0 && coilStatus != 1))
                {
                    return;
                }

                /*
                * 01：传输标志Hi
                * 02：传输标志Lo
                * 03、04：协议标志
                * 05、06：此位置以后字节数
                * 07：单元标志
                * 08：功能代码，0x0F表示写多个线圈
                * 09、10：起始线圈地址
                * 11、12：线圈数量
                * 13：数据字节数
                * 14~：数据
                */

                byte[] plc_send15 = new byte[14];
                //标识符
                ushort identifier = GetIdentifier();
                byte[] bIdentifier = BitConverter.GetBytes(identifier);
                plc_send15[0] = bIdentifier[1];
                plc_send15[1] = bIdentifier[0];
                plc_send15[2] = 0;
                plc_send15[3] = 0;
                ushort iLen15 = 8;
                byte[] bLen15 = BitConverter.GetBytes(iLen15);
                plc_send15[4] = bLen15[1];
                plc_send15[5] = bLen15[0];
                plc_send15[6] = this.DevAddr;
                plc_send15[7] = 15;
                byte[] bRegAddr15 = BitConverter.GetBytes(startAddr);
                plc_send15[8] = bRegAddr15[1];
                plc_send15[9] = bRegAddr15[0];
                byte[] bCoilCount15 = BitConverter.GetBytes(coilCount);
                plc_send15[10] = bCoilCount15[1];
                plc_send15[11] = bCoilCount15[0];
                byte byteCount15 = 1;
                plc_send15[12] = byteCount15;

                plc_send15[13] = coilStatus;


                int retryTimes = 0;

                while (retryTimes <= this.retryTimes)
                {
                    try
                    {
                        ServiceLog.Debug(string.Format("正在写入数据到 Modbus TCP 服务器[{0} {1}:{2}]\r\n报文内容：{3}",
                            ServerID, server_ip, server_port, BitConverter.ToString(plc_send15)));
                        clientSocket.Send(plc_send15, plc_send15.Length, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        ServiceLog.Error(string.Format("写入数据到 Modbus TCP 服务器[{0} {1}:{2}]连接发生错误：{3}\r\n报文内容：{4}",
                                ServerID, server_ip, server_port, ex.Message, BitConverter.ToString(plc_send15)));
                        if (retryTimes >= this.retryTimes)
                        {
                            throw new Exception(string.Format("写入数据到 Modbus TCP 服务器时发生错误：{0}", ex.Message));
                        }
                        retryTimes++;

                        ReconnectToServer();

                        continue;
                    }

                    byte[] recMsgByte15 = new byte[12];
                    int recLen15;
                    try
                    {
                        //Thread.Sleep(5000);
                        recLen15 = clientSocket.Receive(recMsgByte15, recMsgByte15.Length, SocketFlags.None);
                        ServiceLog.Debug(string.Format("接收到 Modbus TCP 服务器[{0} {1}:{2}]数据\r\n报文内容：{3}",
                            ServerID, server_ip, server_port, BitConverter.ToString(recMsgByte15)));
                    }
                    catch (SocketException se)
                    {
                        ServiceLog.Debug(string.Format("接收消息失败：{0}", se.SocketErrorCode.ToString()));
                        break;
                    }

                    //判断是否返回异常代码
                    string errMsg = CheckExceptionCode(recMsgByte15);
                    if (errMsg != string.Empty)
                    {
                        throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                    }
                    errMsg = CheckIsNullData(recMsgByte15);
                    if (errMsg != string.Empty)
                    {
                        throw new Exception(string.Format("接收到 Modbus TCP 服务器返回的错误：{0}", errMsg));
                    }
                    break;
                }
            }
        }

        public string getReverse(string pStr)
        {
            string str = "";
            char[] strTemp = pStr.ToCharArray();
            Array.Reverse(strTemp);
            string[] strArr = Array.ConvertAll<char, string>(strTemp, delegate (char c) { return c.ToString(); });
            str = string.Join("", strArr);

            return str;
        }
    }
}
