using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.NetServerConsoleDemo
{
    public delegate void ProcessRequestHandler(
            string page,
            string query,
            System.IO.TextWriter writer);
    class HttpServer2
    {
        static private void ProcessRequest(string page,
                                           string query,
                                           System.IO.TextWriter writer)
        {
            System.Web.Hosting.SimpleWorkerRequest worker =
                new System.Web.Hosting.SimpleWorkerRequest(page, query, writer);
            System.Web.HttpRuntime.ProcessRequest(worker);
        }
        static public void Run()
        {
            if (!HttpListener.IsSupported)
            {
                throw new System.InvalidOperationException(
                    "使用 HttpListener 必须为 Windows XP SP2 或 Server 2003 以上系统！");
            }

            WebAppHost appHost = System.Web.Hosting.ApplicationHost.CreateApplicationHost(
                            typeof(WebAppHost),
                            "/",
                            new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\..\\").FullName)
                            as WebAppHost;

            string[] prefixes = new string[] { "http://localhost:50004/" };

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

                HttpListenerResponse response = context.Response;

                using (System.IO.TextWriter writer
                    = new System.IO.StreamWriter(response.OutputStream))
                {
                    string path = System.IO.Path.GetFileName(request.Url.AbsolutePath);

                    Console.WriteLine("PATH: " + path);
                    Console.WriteLine("QUERY: " + request.Url.Query);

                    System.IO.StringWriter sw = new System.IO.StringWriter();

                    appHost.ProcessRequest(
                        path,
                        request.Url.Query,
                        sw);

                    string content = sw.ToString();

                    sw.Close();

                    response.ContentLength64
                        = System.Text.Encoding.UTF8.GetByteCount(content);
                    response.ContentType = "text/html; charset=UTF-8";

                    //string filename =  "测试.txt";
                    //response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", filename));

                    writer.Write(content);

                    Console.WriteLine("\r\nProcess OK.\r\n");
                }

                if (Console.KeyAvailable)
                {
                    break;
                }
            }

            listener.Stop();
        }
    }
}
