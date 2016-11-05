using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetServerDemo
{
    class SocketServerMultiThread
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
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);
            Console.WriteLine("开始监听，端口号：{0}.", endPoint.Port);
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("接收连接：[{0}] [{1}]", clientSocket.Handle, clientSocket.RemoteEndPoint);
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessSocketClient));
                thread.IsBackground = true;
                thread.Start(clientSocket);

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            serverSocket.Close();
        }


        static public void ProcessSocketClient(object client)
        {
            if (client != null && client is Socket)
            {
                ProcessSocketClient(client as Socket);
            }
        }

        static public void ProcessSocketClient(Socket client)
        {
            IntPtr socketHandle = client.Handle;
            SocketError socketErr = SocketError.Success;
            while (client.Connected)
            {
                byte[] buffer = new byte[4096];
                int recLen = 0;
                try
                {
                    recLen = client.Receive(buffer, buffer.Length, SocketFlags.None);
                }
                catch (SocketException se)
                {
                    socketErr = se.SocketErrorCode;
                    break;
                }
                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                string requestString = utf8.GetString(buffer, 0, recLen);

                string msg = string.Format("时间：{0}\r\n接收信息：{1}\r\n", DateTime.Now.ToString(), requestString);
                Console.WriteLine("客户端: " + client.Handle);
                Console.WriteLine(msg);
                Console.WriteLine("");

                try
                {
                    client.Send(utf8.GetBytes(msg));
                }
                catch (SocketException se)
                {
                    socketErr = se.SocketErrorCode;
                    break;
                }
                

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            Console.WriteLine("客户端[{0}]连接断开，{1}", socketHandle, socketErr.ToString());
            /*
            for (int i = 0; i < 10; i++)
            {
                client.Send(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString() + "\r\n"));
                Thread.Sleep(1000);
            }
            */
        }
    }
}
