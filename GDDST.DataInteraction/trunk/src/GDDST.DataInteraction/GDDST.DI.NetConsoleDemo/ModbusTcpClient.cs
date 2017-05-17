using System;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.NetConsoleDemo
{
    class ModbusTcpClient
    {
        static public void Run()
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

            IPEndPoint endPoint = new IPEndPoint(ip, port);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("正在连接[{0}:{1}]...", ip, port);

            try
            {
                clientSocket.Connect(endPoint);
                Console.WriteLine("连接成功[{0}]。\r\n", clientSocket.Handle);
            }
            catch (SocketException se)
            {
                Console.WriteLine("连接失败：" + se.SocketErrorCode.ToString());
                return;
            }

            while (true)
            {
                Console.WriteLine("请输入设备地址：");
                string sDevAddr = Console.ReadLine();
                byte iDevAddr;
                if (!byte.TryParse(sDevAddr, out iDevAddr))
                {
                    Console.WriteLine("设备地址[{0}]无效。", sDevAddr);
                    return;
                }

                Console.WriteLine("请输入起始寄存器地址：");
                string sRegAddr = Console.ReadLine();
                ushort iRegAddr;
                if (!ushort.TryParse(sRegAddr, out iRegAddr))
                {
                    Console.WriteLine("起始寄存器地址[{0}]无效。", sRegAddr);
                    return;
                }

                Console.WriteLine("请输入寄存器数量：");
                string sRegCount = Console.ReadLine();
                ushort iRegCount;
                if (!ushort.TryParse(sRegCount, out iRegCount))
                {
                    Console.WriteLine("寄存器数量[{0}]无效。", sRegCount);
                    return;
                }

                
                //读取从0x29寄存器开始1个寄存器的值
                //byte[] plc_send = new byte[12] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, 0x00, 0x29, 0x00, 0x01 };
                //将单个值0x0001写入寄存器0x29
                //byte[] plc_send = new byte[12] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x29, 0x00, 0x01 };
                
                byte[] plc_send = new byte[12];
                plc_send[0] = 0;
                plc_send[1] = 1;
                plc_send[2] = 0;
                plc_send[3] = 0;
                plc_send[4] = 0;
                plc_send[5] = 6;
                plc_send[6] = iDevAddr;
                plc_send[7] = 3;
                byte[] bRegAddr = BitConverter.GetBytes(iRegAddr);
                plc_send[8] = bRegAddr[1];
                plc_send[9] = bRegAddr[0];
                byte[] bRegCount = BitConverter.GetBytes(iRegCount);
                plc_send[10] = bRegCount[1];
                plc_send[11] = bRegCount[0];
                
                for (int i = 0; i < plc_send.Length; i++)
                {
                    Console.WriteLine(plc_send[i]);
                }
                try
                {
                    //clientSocket.Send(msgByte_send, msgByte_send.Length, SocketFlags.None);
                    clientSocket.Send(plc_send, plc_send.Length, SocketFlags.None);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                    break;
                }

                Console.WriteLine("");
                Console.WriteLine("正在等待接收信息...");
                byte[] recMsgByte = new byte[1024];
                int recLen;
                try
                {
                    recLen = clientSocket.Receive(recMsgByte, recMsgByte.Length, SocketFlags.None);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                    break;
                }

                string recMsg = System.Text.Encoding.UTF8.GetString(recMsgByte, 0, recLen);
                //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                Console.WriteLine(string.Format("ASCII: {0}", recMsg));
                Console.WriteLine("");

                Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(recMsgByte)));
                Console.WriteLine("");

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
        }
    }
}
