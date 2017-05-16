using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.NetConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("输入程序类型(server|client)：");
                string appType = Console.ReadLine();
                switch (appType.ToUpper().Trim())
                {
                    case "SERVER":
                        Console.WriteLine("输入服务器类型（socket|socketmt|tcp|http|http2）：");
                        string serverType = Console.ReadLine();
                        switch (serverType.ToUpper().Trim())
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
                        break;
                    case "CLIENT":
                        Console.WriteLine("输入客户端类型（socket|tcp|socketudp|modbus）：");
                        string clientType = Console.ReadLine();
                        switch (clientType.ToUpper().Trim())
                        {
                            case "SOCKET":
                                SocketClient.Run();
                                break;
                            case "TCP":
                                TcpClient.Run();
                                break;
                            case "SOCKETUDP":
                                SocketUdp.Run();
                                break;
                            case "MODBUS":
                                ModbusClient.Run();
                                break;
                            default:
                                break;
                        }
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
