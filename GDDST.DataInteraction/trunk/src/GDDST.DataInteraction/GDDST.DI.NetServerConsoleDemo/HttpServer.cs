using System;
using System.Net;

namespace GDDST.DI.NetServerConsoleDemo
{
    class HttpServer
    {
        static public void Run()
        {
            if (!HttpListener.IsSupported)
            {
                throw new System.InvalidOperationException(
                    "使用 HttpListener 必须为 Windows XP SP2 或 Server 2003 以上系统！");
            }
            else
            {
                string[] prefixes = new string[] { "http://localhost:50003/" };

                HttpListener listener = new HttpListener();

                foreach (string s in prefixes)
                {
                    listener.Prefixes.Add(s);
                }

                listener.Start();
                Console.WriteLine("监听中...");

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();

                    HttpListenerRequest request = context.Request;

                    Console.WriteLine("{0} {1} HTTP/1.1", request.HttpMethod, request.RawUrl);
                    Console.WriteLine("Accept {0}", string.Join(",", request.AcceptTypes));
                    Console.WriteLine("Accept-Language:{0}", string.Join(",", request.UserLanguages));
                    Console.WriteLine("User-Agent:{0}", request.UserAgent);
                    Console.WriteLine("Accept-Encoding", request.Headers["Accept-Encodiing"]);
                    Console.WriteLine("Connection:{0}", request.KeepAlive ? "Keep-Alive" : "Close");
                    Console.WriteLine("Host:{0}", request.UserHostName);
                    Console.WriteLine("Pragma:{0}", request.Headers["Pragma"]);

                    HttpListenerResponse response = context.Response;

                    string responseString =
                        @"<html>
                            <head><title>测试HttpListener</title></head>
                            <body><h1>ahfhfhahahdh</h1></body>
                          </html>";

                    System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

                    response.ContentLength64 = utf8.GetByteCount(responseString);
                    response.ContentType = "text/html; charset=UTF-8";

                    System.IO.Stream output = response.OutputStream;
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                    writer.Write(responseString);

                    writer.Close();

                    if (Console.KeyAvailable)
                    {
                        break;
                    }
                }

                listener.Stop();
            }
        }
    }
}
