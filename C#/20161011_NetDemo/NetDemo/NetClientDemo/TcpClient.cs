using System;
using System.Net;
using System.Net.Sockets;

namespace NetClientDemo
{
    class TcpClient
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

            System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(AddressFamily.InterNetwork);
            try
            {
                Console.WriteLine("正在连接[{0}:{1}]...", ip, port);
                tcpClient.Connect(endPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("连接失败：" + se.SocketErrorCode.ToString());
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("连接失败：" + e.Message);
                return;
            }

            NetworkStream ns = null;

            while (true)
            {
                if (ns == null)
                {
                    ns = tcpClient.GetStream();
                }

                Console.WriteLine("请输入信息：");
                string msg = Console.ReadLine();
                byte[] msgByte = System.Text.Encoding.UTF8.GetBytes(msg);
                
                try
                {
                    //ns = tcpClient.GetStream();
                    ns.Write(msgByte, 0, msgByte.Length);
                    ns.Flush();
                }
                catch (SocketException se)
                {
                    Console.WriteLine("发送消息失败：" + se.SocketErrorCode.ToString());
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("发送消息失败：" + e.Message);
                    return;
                }

                Console.WriteLine("");
                Console.WriteLine("正在等待接收信息...");
                
                byte[] recMsgByte = new byte[4096];
                int recLen;
                try
                {
                    //ns = tcpClient.GetStream();
                    recLen = ns.Read(recMsgByte, 0, recMsgByte.Length);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("接收消息失败：" + se.SocketErrorCode.ToString());
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("接收消息失败：" + e.Message);
                    return;
                }

                string recMsg = System.Text.Encoding.UTF8.GetString(recMsgByte, 0, recLen);
                Console.WriteLine(recMsg);
                Console.WriteLine("");

                //int int32 = BitConverter.ToInt32(recMsgByte, 0);
                //string hexStr = "0x" + Convert.ToString(int32, 16);

                //Console.WriteLine(hexStr);

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
        }
    }
}
