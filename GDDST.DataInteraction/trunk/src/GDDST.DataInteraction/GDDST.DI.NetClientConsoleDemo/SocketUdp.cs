using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GDDST.DI.NetClientConsoleDemo
{
    class SocketUdp
    {
        class SocketRemoteEndPoint
        {
            public Socket LocalSocket;
            public EndPoint RemoteEndPoint;
        }

        static public void Run()
        {
            Console.WriteLine("本地IP:");
            string ipStr = Console.ReadLine();

            IPAddress localIP;
            if (!IPAddress.TryParse(ipStr, out localIP))
            {
                Console.WriteLine("IP地址[{0}]无效。", ipStr);
                return;
            }


            Console.WriteLine("本地Port:");
            string portStr = Console.ReadLine();
            ushort localPort;
            if (!ushort.TryParse(portStr, out localPort))
            {
                Console.WriteLine("端口[{0}]无效。", portStr);
                return;
            }

            Console.WriteLine("远程IP:");
            ipStr = Console.ReadLine();

            IPAddress remoteIP;
            if (!IPAddress.TryParse(ipStr, out remoteIP))
            {
                Console.WriteLine("IP地址[{0}]无效。", ipStr);
                return;
            }


            Console.WriteLine("远程Port:");
            portStr = Console.ReadLine();
            ushort remotePort;
            if (!ushort.TryParse(portStr, out remotePort))
            {
                Console.WriteLine("端口[{0}]无效。", portStr);
                return;
            }

            IPEndPoint localEndPoint = new IPEndPoint(localIP, localPort);
            IPEndPoint remoteEndPoint = new IPEndPoint(remoteIP, remotePort);

            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socket.Bind(localEndPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("绑定本地地址失败：{0}", se.SocketErrorCode.ToString());
                return;
            }

            SocketRemoteEndPoint sre = new SocketRemoteEndPoint();
            sre.LocalSocket = socket;
            sre.RemoteEndPoint = remoteEndPoint;

            Thread thread = new Thread(new ParameterizedThreadStart(ProcessReceive));
            thread.IsBackground = true;
            thread.Start(sre);

            while (true)
            {
                Console.WriteLine("请输入信息：");
                string msg = Console.ReadLine();
                byte[] msgByte = System.Text.Encoding.UTF8.GetBytes(msg);
                try
                {
                    socket.SendTo(msgByte, 0, msgByte.Length, SocketFlags.None, remoteEndPoint);
                    Console.WriteLine("已发送");
                }
                catch (SocketException se)
                {
                    Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                    break;
                }

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
        }

        static private void ProcessReceive(object socket)
        {
            if (socket is SocketRemoteEndPoint)
            {
                ProcessReceive(socket as SocketRemoteEndPoint);
            }
        }

        static private void ProcessReceive(SocketRemoteEndPoint sre)
        {
            while (true)
            {
                byte[] recMsgByte = new byte[4096];
                int recLen;
                try
                {
                    recLen = sre.LocalSocket.ReceiveFrom(recMsgByte, 0, recMsgByte.Length, SocketFlags.None, ref sre.RemoteEndPoint);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                    break;
                }

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
        }
    }
}
