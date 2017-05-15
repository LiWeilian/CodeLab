using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GDDST.DI.NetServerConsoleDemo
{
    class SocketServerMultiThread
    {
        static private byte[] m_receiveBuffer;
        const int m_receiveBufferSize = 4096;

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
            try
            {
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
            }
            catch (Exception)
            {
                
            }
            serverSocket.Close();
        }


        static public void ProcessSocketClient(object client)
        {
            if (client != null && client is Socket)
            {
                ProcessSocketClientAsync(client as Socket);
            }
        }

        static public void ProcessSocketClient(Socket client)
        {
            IntPtr socketHandle = client.Handle;
            SocketError socketErr = SocketError.Success;
            try
            {
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

                    //string msg = string.Format("时间：{0}\r\n接收信息：{1}\r\n", DateTime.Now.ToString(), requestString);
                    string msg = requestString;
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
                }
                Console.WriteLine("客户端[{0}]连接断开，{1}", socketHandle, socketErr.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("连接出现异常：{0}", e.Message));
            }
            
        }

        static public void ProcessSocketClientAsync(Socket client)
        {
            IntPtr socketHandle = client.Handle;
            try
            {
                while (client != null && client.Connected)
                {
                    Thread.Sleep(1031);
                    try
                    {
                        StateObject recStateObj = new StateObject();
                        recStateObj.ClientSocket = client;
                        client.BeginReceive(recStateObj.Buffer, 0, StateObject.BufferSize, SocketFlags.None,
                            new AsyncCallback(ReceiveCallback), recStateObj);
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                        try
                        {
                            if (client.Connected)
                            {
                                client.Shutdown(SocketShutdown.Both);
                                client.Disconnect(false);
                            }
                            client.Close();
                        }
                        catch (Exception)
                        {
                            
                        }
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("连接出现异常：{0}", e.Message));
                        try
                        {
                            if (client.Connected)
                            {
                                client.Shutdown(SocketShutdown.Both);
                                client.Disconnect(false);
                            }
                            client.Close();
                        }
                        catch (Exception)
                        {

                        }
                        break;
                    }

                    
                    try
                    {
                        StateObject sendStateObj = new StateObject();
                        sendStateObj.ClientSocket = client;
                        System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                        string sendMsg = string.Format("Server: {0}\r\n", DateTime.Now);
                        sendStateObj.Buffer = utf8.GetBytes(sendMsg);
                        client.BeginSend(sendStateObj.Buffer, 0, sendStateObj.Buffer.Length, SocketFlags.None,
                            new AsyncCallback(SendCallback), sendStateObj);
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                        try
                        {
                            if (client.Connected)
                            {
                                client.Shutdown(SocketShutdown.Both);
                                client.Disconnect(false);
                            }
                            client.Close();
                        }
                        catch (Exception)
                        {

                        }
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("连接出现异常：{0}", e.Message));
                        try
                        {
                            if (client.Connected)
                            {
                                client.Shutdown(SocketShutdown.Both);
                                client.Disconnect(false);
                            }
                            client.Close();
                        }
                        catch (Exception)
                        {

                        }
                        break;
                    }
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Disconnect(false);
                    }
                    client.Close();
                }
                catch (Exception)
                {

                }
            }
            Console.WriteLine("客户端[{0}]连接断开", socketHandle);

        }

        static private void ReceiveCallback(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                StateObject stateObj = (StateObject)ar.AsyncState;
                clientSocket = stateObj.ClientSocket;
                int recLen = 0;
                if (clientSocket != null)
                {
                    recLen = clientSocket.EndReceive(ar);
                }

                if (recLen > 0)
                {
                    Console.WriteLine(string.Format("接收到客户端[{0}]信息：\r\n", clientSocket.Handle));
                    System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                    string msg = utf8.GetString(stateObj.Buffer, 0, recLen);
                    Console.WriteLine(string.Format("UTF-8：\r\n{0}\r\n",
                        msg));
                    msg = System.Text.Encoding.ASCII.GetString(stateObj.Buffer, 0, recLen);
                    Console.WriteLine(string.Format("ASCII：\r\n{0}\r\n",
                        msg));
                    msg = BitConverter.ToString(stateObj.Buffer);
                    Console.WriteLine(string.Format("Bytes：\r\n{0}\r\n",
                        msg));
                }
            }
            catch (Exception ex)
            {
                try
                {

                    if (clientSocket != null)
                    {
                        if (clientSocket.Connected)
                        {
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Disconnect(false);
                        }
                        clientSocket.Close();
                    }
                }
                catch (Exception)
                {

                }

                Console.WriteLine(string.Format("接收客户端数据时发生错误：{0}", 
                    ex.Message));
            }
            
        }

        static private void SendCallback(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                StateObject stateObj = (StateObject)ar.AsyncState;
                clientSocket = stateObj.ClientSocket;
                int sendLen = 0;
                if (clientSocket != null)
                {
                    sendLen = clientSocket.EndSend(ar);
                    Console.WriteLine("已发送数据至客户端[{0}]，长度：{1}", clientSocket.Handle, sendLen);
                }
            }
            
            catch (Exception ex)
            {
                try
                {

                    if (clientSocket != null)
                    {
                        if (clientSocket.Connected)
                        {
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Disconnect(false);
                        }
                        clientSocket.Close();
                    }
                }
                catch (Exception)
                {

                }

                Console.WriteLine(string.Format("发送数据至客户端数据时发生错误：{0}",
                    ex.Message));
            }
        }
    }


    class StateObject
    {
        public Socket ClientSocket { get; set; }

        public const int BufferSize = 1024;
        public byte[] Buffer = new byte[BufferSize];
    }
}
