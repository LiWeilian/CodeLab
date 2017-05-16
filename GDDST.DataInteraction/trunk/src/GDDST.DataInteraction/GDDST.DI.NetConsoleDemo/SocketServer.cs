using System;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.NetConsoleDemo
{
    class SocketServer
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
            Socket socket = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream,
                                       ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
            Console.WriteLine("开始监听，端口号：{0}.", endPoint.Port);
            while (true)
            {
                Socket client = socket.Accept();
                Console.WriteLine(client.RemoteEndPoint);
                byte[] buffer = new byte[4096];
                int length = client.Receive(buffer, buffer.Length, SocketFlags.None);
                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                string requestString = utf8.GetString(buffer, 0, length);
                Console.WriteLine(requestString);

                string statusLine = "HTTP/1.1 200 OK\r\n";
                byte[] statusLineBytes = utf8.GetBytes(statusLine);
                string responseBody = "<html><head><title>测试</title></head><body><h1>HAHAHAHAHAH</h1></body></html>";
                byte[] responseBodyBytes = utf8.GetBytes(responseBody);
                string responseHeader = string.Format("Content-Type: text/html;charset=UTF-8\r\nContent-Length: {0}\r\n", responseBody.Length);
                byte[] responseHeaderBytes = utf8.GetBytes(responseHeader);

                client.Send(statusLineBytes);
                client.Send(responseHeaderBytes);
                client.Send(new byte[] { 13, 10 });
                client.Send(responseBodyBytes);
                client.Close();

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            socket.Close();
        }
    }
}
