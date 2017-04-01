using System;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.NetServerConsoleDemo
{
    class TcpServer
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

            IPAddress address = ip;
            IPEndPoint endPoint = new IPEndPoint(address, port);
            TcpListener newserver = new TcpListener(endPoint);

            newserver.Start();
            Console.WriteLine("开始监听...");

            while (true)
            {
                TcpClient newclient = newserver.AcceptTcpClient();
                Console.WriteLine("已经建立连接！");

                NetworkStream ns = newclient.GetStream();

                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                byte[] request = new byte[4096];
                int length = ns.Read(request, 0, 4096);
                string requestString = utf8.GetString(request, 0, length);
                Console.WriteLine(requestString);

                string statusLine = "HTTP/1.1 200 OK\r\n";
                byte[] statusLineBytes = utf8.GetBytes(statusLine);
                string responseBody = "<html><head><title>测试tcplistener</title></head><body><h1>HAHAHAHAHAH</h1></body></html>";
                byte[] responseBodyBytes = utf8.GetBytes(responseBody);
                string responseHeader = string.Format("Content-Type: text/html;charset=UTF-8\r\nContent-Length: {0}\r\n", responseBody.Length);
                byte[] responseHeaderBytes = utf8.GetBytes(responseHeader);

                ns.Write(statusLineBytes, 0, statusLineBytes.Length);
                ns.Write(responseHeaderBytes, 0, responseHeaderBytes.Length);
                ns.Write(new byte[] { 13, 10 }, 0, 2);
                ns.Write(responseBodyBytes, 0, responseBodyBytes.Length);

                newclient.Close();

                if (Console.KeyAvailable)
                {
                    break;
                }
            }

            newserver.Stop();
        }
    }
}
