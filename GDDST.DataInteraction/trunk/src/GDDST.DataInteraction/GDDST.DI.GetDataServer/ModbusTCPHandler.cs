using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.GetDataServer
{
    class ModbusTCPHandler : CommHandler
    {
        private ModbusTcpConfig m_config = null;

        const int m_bufferSize = 1024;
        private string m_dataProtocol = string.Empty;
        private IPAddress m_serverIP = null;
        private ushort m_serverPort = 0;
        private Socket m_clientSocket = null;
        private Thread m_clientThread = null;
        private bool m_isThreadStop = false;

        public ModbusTCPHandler(ModbusTcpConfig config)
        {
            m_config = config;
        }

        public override void OnStart()
        {
            if (!IPAddress.TryParse(m_config.ServerIP, out m_serverIP))
            {
                IPAddress[] ips = Dns.GetHostAddresses(m_config.ServerIP);
                if (ips.Length == 0)
                {
                    ServiceLog.LogServiceMessage(string.Format("IP地址[{0}]无效。", m_config.ServerIP));
                    return;
                } else
                {
                    foreach (IPAddress ip in ips)
                    {
                        if (!ip.IsIPv6LinkLocal && !ip.IsIPv6Multicast && !ip.IsIPv6SiteLocal)
                        {
                            m_serverIP = ip;
                            break;
                        }
                    }
                    if (m_serverIP == null)
                    {
                        ServiceLog.LogServiceMessage(string.Format("IP地址[{0}]无效。", m_config.ServerIP));
                        return;
                    }
                }
                
            }

            m_serverPort = m_config.ServerPort;


            try
            {
                m_clientThread = new Thread(new ThreadStart(Run));
                m_clientThread.IsBackground = true;
                m_clientThread.Start();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public override void OnStop()
        {
            m_isThreadStop = true;
        }

        private void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        private Socket CreateClientSocket(IPAddress serverIP, ushort serverPort)
        {
            Socket clientSocket = null;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(serverIP, serverPort);
                clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(endPoint);

                return clientSocket;
            }
            catch (Exception e)
            {
                ServiceLog.LogServiceMessage(string.Format("连接[{0}:{1}]失败：{2}",
                    serverIP, serverPort, e.Message));
                return null;
            }
        }

        private void Run()
        {
            m_isThreadStop = false;

            ServiceLog.LogServiceMessage(string.Format("开始连接[{0}:{1}]", m_serverIP, m_serverPort));
            m_clientSocket = CreateClientSocket(m_serverIP, m_serverPort);
            if (m_clientSocket != null)
            {
                ServiceLog.LogServiceMessage(string.Format("连接[{0}:{1}]成功", m_serverIP, m_serverPort));
                try
                {

                    while (m_clientSocket != null && m_clientSocket.Connected)
                    {
                        Thread.Sleep(2047);
                        DisplayMessage(m_config.ModbusOperations.Count().ToString());
                        //SendAsync(m_clientSocket, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss\r\n"));
                        foreach (ModbusTcpOperation operation in m_config.ModbusOperations)
                        {
                            DisplayMessage(operation.StartAddr.ToString());

                            byte[] plc_send = new byte[12];
                            byte[] identifier = BitConverter.GetBytes(operation.Identifier);
                            byte[] protocol = BitConverter.GetBytes(operation.Protocol);
                            byte[] length = BitConverter.GetBytes(operation.Length);
                            byte[] startAddr = BitConverter.GetBytes((ushort)operation.StartAddr);
                            byte[] regCount = BitConverter.GetBytes((ushort)operation.RegCount);

                            plc_send[0] = identifier[1];
                            plc_send[1] = identifier[0];
                            plc_send[2] = protocol[1];
                            plc_send[3] = protocol[0];
                            plc_send[4] = length[1];
                            plc_send[5] = length[0];
                            plc_send[6] = (byte)operation.DeviceAddr;
                            plc_send[7] = (byte)operation.FunctionCode;
                            plc_send[8] = startAddr[1];
                            plc_send[9] = startAddr[0];
                            plc_send[10] = regCount[1];
                            plc_send[11] = regCount[0];

                            SendAsync(m_clientSocket, plc_send);

                            StateObject stateObj = new StateObject();
                            stateObj.ClientSocket = m_clientSocket;

                            DisplayMessage("");
                            DisplayMessage("开始接收信息...");
                            m_clientSocket.BeginReceive(stateObj.Buffer, 0, StateObject.BufferSize,
                                0, new AsyncCallback(ReceiveCallback), stateObj);
                        }
                    }
                }
                catch (Exception e)
                {
                    DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                }

                DisplayMessage("结束...");
            }
        }

        private void SendAsync(Socket clientSocket, string msg)
        {
            try
            {

                if (clientSocket != null && clientSocket.Connected)
                {
                    DisplayMessage("");
                    DisplayMessage(string.Format("开始发送信息：{0}", msg));
                    byte[] sendBytes = new byte[1024];
                    sendBytes = Encoding.UTF8.GetBytes(msg);
                    DisplayMessage(string.Format("开始发送信息，信息长度：{0}", sendBytes.Length));
                    clientSocket.BeginSend(sendBytes, 0, sendBytes.Length,
                        0, new AsyncCallback(SendCallback), clientSocket);
                }
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }

        private void SendAsync(Socket clientSocket, byte[] msg)
        {
            try
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    DisplayMessage("");
                    DisplayMessage(string.Format("开始发送数据：{0}", BitConverter.ToString(msg)));
                    DisplayMessage(string.Format("开始发送数据，信息长度：{0}", msg.Length));
                    clientSocket.BeginSend(msg, 0, msg.Length,
                        0, new AsyncCallback(SendCallback), clientSocket);
                }
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = (Socket)ar.AsyncState;
                if (clientSocket != null)
                    clientSocket.EndSend(ar);
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                DisplayMessage("");
                DisplayMessage("正在接收信息...");

                int byteRead = -1;
                StateObject stateObj = (StateObject)ar.AsyncState;
                clientSocket = stateObj.ClientSocket;
                if (clientSocket != null)
                {
                    byteRead = clientSocket.EndReceive(ar);
                }
                else
                {
                    DisplayMessage("客户端连接无效");
                }

                if (byteRead > 0)
                {
                    string msg = Encoding.Default.GetString(stateObj.Buffer, 0, byteRead);

                    DisplayMessage(string.Format("接收到来自[{0}:{1}]的数据：\r\n{2}", m_serverIP, m_serverPort, msg));

                    //解析接收到的数据信息
                    Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(stateObj.Buffer)));
                    Console.WriteLine("");

                    byte[] identifier = new byte[2];
                    identifier[0] = stateObj.Buffer[1];
                    identifier[1] = stateObj.Buffer[0];

                    byte[] protocol = new byte[2];
                    protocol[0] = stateObj.Buffer[3];
                    protocol[1] = stateObj.Buffer[2];

                    byte[] length = new byte[2];
                    length[0] = stateObj.Buffer[5];
                    length[1] = stateObj.Buffer[4];

                    byte devAddr = stateObj.Buffer[6];
                    byte funcCode = stateObj.Buffer[7];

                    byte datalen = stateObj.Buffer[8];

                    ushort[] values = new ushort[datalen/2];

                    int dataIdx = 9;

                    string sValue = string.Empty;

                    for (int i = 0; i < datalen; i+=2)
                    {
                        byte[] value = new byte[2];
                        value[0] = stateObj.Buffer[dataIdx + 1];
                        value[1] = stateObj.Buffer[dataIdx];

                        values[i/2] = BitConverter.ToUInt16(value, 0);

                        sValue += string.Format("Value: {0}\r\n", values[i/2]);

                        dataIdx += 2;
                    }

                    DisplayMessage(string.Format("Identifier: {0}, Data Length: {1}\r\n{2}", BitConverter.ToUInt16(identifier, 0), datalen, sValue));


                    //返回结果 -> 对应请求 -> 寄存器与值配对
                    ModbusTcpResult mbTcpResult = new ModbusTcpResult();
                    mbTcpResult.Identifier = BitConverter.ToUInt16(identifier, 0);
                    mbTcpResult.Protocol = BitConverter.ToUInt16(protocol, 0);
                    mbTcpResult.Length = BitConverter.ToUInt16(length, 0);
                    mbTcpResult.DeviceAddr = devAddr;
                    mbTcpResult.FunctionCode = funcCode;
                    mbTcpResult.ResultDataLength = datalen;
                    mbTcpResult.ResultData = values;
                    mbTcpResult.DataAcqTime = DateTime.Now;
                    //
                    List<ModbusTcpDataEntity> mbTcpDataEntities = GetModbusTcpData(mbTcpResult);


                    //clientSocket.BeginReceive(stateObj.Buffer, 0, StateObject.BufferSize, 
                    //    0, new AsyncCallback(ReceiveCallback), stateObj);
                }
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }

        private List<ModbusTcpDataEntity> GetModbusTcpData(ModbusTcpResult mbTcpResult)
        {
            List<ModbusTcpDataEntity> mbTcpDataList = new List<ModbusTcpDataEntity>();

            //查找对应的operation
            var ops = from n in m_config.ModbusOperations
                      where n.Identifier == mbTcpResult.Identifier
                      select n;
            ModbusTcpOperation mbTcpOp = null;
            foreach (ModbusTcpOperation op in ops)
            {
                mbTcpOp = op;
            }

            if (mbTcpOp != null)
            {
                for (int i = 0; i < mbTcpOp.RegCount; i++)
                {
                    ushort currentAddr = (ushort)(mbTcpOp.StartAddr + i);
                    ModbusTcpConfigItem mbTcpCfgItem = GetModbusTcpConfigItemByRegAddr(currentAddr);
                    if (mbTcpCfgItem != null)
                    {
                        ModbusTcpDataEntity dataEntity = new ModbusTcpDataEntity();
                        dataEntity.RID = Guid.NewGuid().ToString();
                        dataEntity.Device_Addr = mbTcpCfgItem.DeviceAddr.ToString();
                        dataEntity.Station = m_config.ServerName;
                        dataEntity.Sensor_Type = "-1";
                        dataEntity.Sensor_Name = mbTcpCfgItem.Name;
                        dataEntity.Ori_Value = mbTcpResult.ResultData[i];
                        dataEntity.Trans_Value = dataEntity.Ori_Value * mbTcpCfgItem.Multiplier;
                        dataEntity.Trans_Unit = mbTcpCfgItem.Unit;
                        dataEntity.DataAcqTime = mbTcpResult.DataAcqTime;

                        mbTcpDataList.Add(dataEntity);
                    }
                }
            }

            return mbTcpDataList;
        }

        private ModbusTcpConfigItem GetModbusTcpConfigItemByRegAddr(ushort regAddr)
        {
            var items = from n in m_config.ModbusTcpItems
                        where n.RegAddr == regAddr
                        select n;
            ModbusTcpConfigItem mbTcpCfgItem = null;
            foreach (ModbusTcpConfigItem item in items)
            {
                mbTcpCfgItem = item;
            }

            return mbTcpCfgItem;
        }
    }
}
