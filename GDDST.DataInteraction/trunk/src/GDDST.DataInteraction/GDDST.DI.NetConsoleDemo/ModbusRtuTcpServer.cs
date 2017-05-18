using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.NetConsoleDemo
{
    class ModbusRtuTcpServer
    {
        public static void Run()
        {
            Console.WriteLine("IP:");
            string ipStr = Console.ReadLine();

            IPAddress ip;
            if (!IPAddress.TryParse(ipStr, out ip))
            {
                Console.WriteLine("IP地址[{0}]无效。", ipStr);
                return;
            }


            Console.WriteLine("Port:");
            string portStr = Console.ReadLine();
            ushort port;
            if (!ushort.TryParse(portStr, out port))
            {
                Console.WriteLine("端口[{0}]无效。", portStr);
                return;
            }
            //IPAddress address = IPAddress.Parse("127.0.0.1");
            IPAddress address = ip;
            IPEndPoint endPoint = new IPEndPoint(address, port);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream,
                                       ProtocolType.Tcp);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);
            Console.WriteLine("开始监听，端口号：{0}.", endPoint.Port);

            //单客户端连接测试
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("接收连接：[{0}] [{1}]", clientSocket.Handle, clientSocket.RemoteEndPoint);


            while (true)
            {
                #region 生成Modbus RTU请求数据，发送至TCP客户端
                string devAddr;
                byte bDevAddr;
                do
                {
                    Console.WriteLine("请输入设备地址码(1~128，默认1)：");
                    devAddr = Console.ReadLine();
                    if (devAddr.Trim() == string.Empty)
                    {
                        devAddr = "1";
                    }
                } while (!byte.TryParse(devAddr, out bDevAddr));


                string sFuncCode;
                byte bFuncCode;
                do
                {
                    Console.WriteLine("请输入功能码(1~128，默认3)：");
                    sFuncCode = Console.ReadLine();
                    if (sFuncCode.Trim() == string.Empty)
                    {
                        sFuncCode = "3";
                    }
                } while (!byte.TryParse(sFuncCode, out bFuncCode));


                string sStartAddr;
                ushort iStartAddr;
                do
                {
                    Console.WriteLine("请输入起始寄存器地址(0~65535)：");
                    sStartAddr = Console.ReadLine();
                } while (!ushort.TryParse(sStartAddr, out iStartAddr));

                string sRegCount;
                ushort iRegCount;
                do
                {
                    Console.WriteLine("请输入读取寄存器数量(1~128)：");
                    sRegCount = Console.ReadLine();
                } while (!ushort.TryParse(sRegCount, out iRegCount) || (iRegCount > 128));

                byte[] temp = new byte[6];
                temp[0] = bDevAddr;
                temp[1] = bFuncCode;

                byte[] bStartAddr = BitConverter.GetBytes(iStartAddr);
                temp[2] = bStartAddr[1];
                temp[3] = bStartAddr[0];

                byte[] bRegCount = BitConverter.GetBytes(iRegCount);
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
                //CRC16 地位在前，高位在后
                modbusRtuReq[6] = bCrc16[0];
                modbusRtuReq[7] = bCrc16[1];

                Console.WriteLine("正在发送数据...");
                Console.WriteLine(string.Format("数据内容：{0}\r\n", BitConverter.ToString(modbusRtuReq)));
                clientSocket.Send(modbusRtuReq);
                #endregion

                #region 从TCP客户端接收Modbus RTU数据
                Console.WriteLine("正在接收数据...");
                Thread.Sleep(500);

                byte[] modbusRtuResponse = new byte[5 + iRegCount * 2];
                try
                {
                    clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);

                    Console.WriteLine(string.Format("数据内容：{0}\r\n", BitConverter.ToString(modbusRtuResponse)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("接收数据时发生错误：{0}\r\n", ex.Message));
                }
                #endregion

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            clientSocket.Close();
            serverSocket.Close();
        }

        private static uint ModbusCRC16(byte[] modbusData, int length)
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
