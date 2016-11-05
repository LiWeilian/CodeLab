using System;

namespace NetClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("输入客户端类型（socket|tcp|socketudp）：");
                string input = Console.ReadLine();
                switch (input.ToUpper().Trim())
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
