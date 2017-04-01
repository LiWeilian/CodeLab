using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.NetServerConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("输入服务器类型（socket|socketmt|tcp|http|http2）：");
                string input = Console.ReadLine();
                switch (input.ToUpper().Trim())
                {
                    case "SOCKET":
                        SocketServer.Run();
                        break;
                    case "SOCKETMT":
                        SocketServerMultiThread.Run();
                        break;
                    case "TCP":
                        TcpServer.Run();
                        break;
                    case "HTTP":
                        HttpServer.Run();
                        break;
                    case "HTTP2":
                        HttpServer2.Run();
                        break;
                    default:
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
