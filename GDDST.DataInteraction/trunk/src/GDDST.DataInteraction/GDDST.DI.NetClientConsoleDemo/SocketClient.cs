using System;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.NetClientConsoleDemo
{
    class SocketClient
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
                Console.WriteLine("请输入信息：");
                string msg = Console.ReadLine();
                byte[] msgByte = System.Text.Encoding.UTF8.GetBytes(msg);
                byte[] msgByte_send = new byte[msgByte.Length + 1];
                for (int i = 0; i < msgByte.Length; i++)
                {
                    msgByte_send[i] = msgByte[i];
                }
                msgByte_send[msgByte_send.Length - 1] = 0;
                /*
                for (int i = 0; i < msgByte_send.Length; i++)
                {
                    Console.WriteLine(msgByte_send[i]);
                }
                */

                //byte[] plc_send = new byte[12] {0x15, 0x01, 0x00, 0x00, 0x06, 0x00, 0xff, 0x03, 0x29, 0x00, 0x03, 0x00};
                byte[] plc_send = new byte[12] { 0x15, 0x01, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x00, 0x29, 0x00, 0x03 };
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
                byte[] recMsgByte = new byte[4096];
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
