using System;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.Threading;

namespace GDDST.DI.NetConsoleDemo
{
    class ModbusRtuTcpClient
    {
        public static void Run()
        {
            #region 创建串口通信连接
            SerialPort serialPort = new SerialPort();
            Console.WriteLine("串行端口号：");
            serialPort.PortName = Console.ReadLine();

            Console.WriteLine("波特率：");
            string sBaudRate = Console.ReadLine();
            int iBaudRate;
            if (int.TryParse(sBaudRate, out iBaudRate))
            {
                serialPort.BaudRate = iBaudRate;
            } else
            {
                serialPort.BaudRate = 9600;
            }

            Console.WriteLine("数据位(7|8)：");
            string sDataBits = Console.ReadLine();
            switch (sDataBits)
            {
                case "7":
                    serialPort.DataBits = 7;
                    break;
                default:
                    serialPort.DataBits = 8;
                    break;
            }

            Console.WriteLine("校验方式(NONE|ODD|EVEN)：");
            string parity = Console.ReadLine();
            switch (parity.ToUpper().Trim())
            {
                case "NONE":
                    serialPort.Parity = Parity.None;
                    break;
                case "ODD":
                    serialPort.Parity = Parity.Odd;
                    break;
                case "EVEN":
                    serialPort.Parity = Parity.Even;
                    break;
                default:
                    serialPort.Parity = Parity.None;
                    break;
            }

            Console.WriteLine("停止位(0|1|2)：");
            string stopBits = Console.ReadLine();
            switch (stopBits.ToUpper().Trim())
            {
                case "0":
                    serialPort.StopBits = StopBits.None;
                    break;
                case "1":
                    serialPort.StopBits = StopBits.One;
                    break;
                case "2":
                    serialPort.StopBits = StopBits.Two;
                    break;
                default:
                    serialPort.StopBits = StopBits.One;
                    break;
            }

            Console.WriteLine("串口写读时间间隔(默认1000ms)：");
            string sInterval = Console.ReadLine();
            uint iInterval;
            if (!uint.TryParse(sInterval, out iInterval))
            {
                iInterval = 1000;
            }

            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                Console.WriteLine(string.Format("已连接串行端口[{0}]\r\n", serialPort.PortName));

            }
            catch (Exception ex)
            {
                serialPort = null;
                Console.WriteLine(ex.Message);
                return;
            }
            
            #endregion

            #region 连接TCP通信服务器
            Console.WriteLine("Server IP:");
            string ipStr = Console.ReadLine();

            IPAddress ip;
            while (!IPAddress.TryParse(ipStr, out ip))
            {
                Console.WriteLine("IP地址[{0}]无效。", ipStr);
                Console.WriteLine("Server IP:");
                ipStr = Console.ReadLine();
            }


            Console.WriteLine("Server Port:");
            string portStr = Console.ReadLine();
            ushort port;
            while (!ushort.TryParse(portStr, out port))
            {
                Console.WriteLine("端口[{0}]无效。", portStr);
                Console.WriteLine("Server Port:");
                portStr = Console.ReadLine();
            }

            Socket clientSocket = null;

            while (clientSocket == null || !clientSocket.Connected)
            {
                try
                {
                    IPEndPoint endPoint = new IPEndPoint(ip, port);
                    clientSocket = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    Console.WriteLine("正在连接[{0}:{1}]...", ip, port);

                    clientSocket.Connect(endPoint);
                    if (clientSocket.Connected)
                    {
                        Console.WriteLine("连接成功[{0} {1}]。\r\n", clientSocket.LocalEndPoint, clientSocket.Handle);
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine("连接失败：" + se.SocketErrorCode.ToString());
                    //return;
                }
                #endregion
                try
                {
                    while (clientSocket.Connected)
                    {
                        serialPort.DiscardOutBuffer();
                        serialPort.DiscardInBuffer();
                        //serialPort.DiscardOutBuffer();
                        Console.WriteLine("正在从TCP服务端接收 Modbus RTU 请求数据...");
                        byte[] modbusRtuReq = new byte[8];
                        try
                        {
                            clientSocket.Receive(modbusRtuReq, modbusRtuReq.Length, SocketFlags.None);

                            Console.WriteLine(string.Format("请求报文：{0}\r\n", BitConverter.ToString(modbusRtuReq)));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("接收 Modbus RTU 请求数据时发生错误：{0}\r\n", e.Message);
                            ResponseErrorToTcpServer(clientSocket);
                            continue;
                        }

                        byte[] reqDataLen = new byte[2];
                        reqDataLen[0] = modbusRtuReq[5];
                        reqDataLen[1] = modbusRtuReq[4];
                        int iReqDataLen = BitConverter.ToUInt16(reqDataLen, 0);
                        if (iReqDataLen == 0)
                        {
                            clientSocket.Disconnect(false);
                            break;
                        }
                        try
                        {
                            Console.WriteLine("正在发送 Modbus RTU 请求数据至Modbus设备...");
                            //if (!serialPort.IsOpen)
                            //{
                            //    serialPort.Open();
                            //}
                            serialPort.Write(modbusRtuReq, 0, modbusRtuReq.Length);
                            //serialPort.DiscardInBuffer();
                            //serialPort.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("发送 Modbus RTU 请求数据至Modbus设备时发生错误：{0}\r\n", e.Message);
                            ResponseErrorToTcpServer(clientSocket);
                            continue;
                        }

                        Thread.Sleep((int)iInterval);
                        byte[] respDataLen = new byte[2];
                        respDataLen[0] = modbusRtuReq[5];
                        respDataLen[1] = modbusRtuReq[4];
                        int iRespDataLen = BitConverter.ToUInt16(respDataLen, 0);
                        Console.WriteLine("回应数据长度：{0}", iRespDataLen);
                        byte[] modbusRtuResponse = new byte[5 + iRespDataLen * 2];
                        try
                        {
                            Console.WriteLine("正在从Modbus设备接收 Modbus RTU 回复数据...");
                            //if (!serialPort.IsOpen)
                            //{
                            //    serialPort.Open();
                            //}
                            serialPort.Read(modbusRtuResponse, 0, modbusRtuResponse.Length);
                            //serialPort.Close();
                            //serialPort.DiscardOutBuffer();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("从Modbus设备接收 Modbus RTU 回复数据时发生错误：{0}\r\n", e.Message);
                            ResponseErrorToTcpServer(clientSocket);
                            continue;
                        }

                        try
                        {
                            Console.WriteLine("正在发送 Modbus RTU 回复数据至TCP服务端...");
                            Console.WriteLine(string.Format("回应报文：{0}\r\n", BitConverter.ToString(modbusRtuResponse)));
                            clientSocket.Send(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("发送 Modbus RTU 回复数据至TCP服务端时发生错误：{0}\r\n", e.Message);
                            ResponseErrorToTcpServer(clientSocket);
                            continue;
                        }
                        Thread.Sleep(100);
                    }
                }
                catch(Exception ex)
                {
                    clientSocket = null;
                }
            }
            
        }

        private static void ResponseErrorToTcpServer(Socket clientSocket)
        {
            if (clientSocket.Connected)
            {
                byte[] error = { 0x00};
                try
                {
                    clientSocket.Send(error, 1, SocketFlags.None);
                }
                catch
                {

                }
            }
        }
    }
}
