using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.GetDataServer
{
    class DataClient
    {
        const int m_bufferSize = 1024;
        private string m_dataProtocol = string.Empty;
        private IPAddress m_serverIP = null;
        private ushort m_serverPort = 0;
        private Socket m_clientSocket = null;
        private Thread m_clientThread = null;
        private bool m_isThreadStop = false;
        public void OnStart(string dataProtocol, string serverIP, string serverPort, 
            GetDataServiceDAL dal)
        {
            if (!IPAddress.TryParse(serverIP, out m_serverIP))
            {
                ServiceLog.LogServiceMessage(string.Format("IP地址[{0}]无效。", serverIP));
                return;
            }
            
            if (!ushort.TryParse(serverPort, out m_serverPort))
            {
                ServiceLog.LogServiceMessage(string.Format("端口[{0}]无效。", serverPort));
                return;
            }

            
            try
            {
                m_clientThread = new Thread(new ThreadStart(Run));
                m_clientThread.IsBackground = true;
                m_clientThread.Start();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void OnStop()
        {
            m_isThreadStop = true;
        }

        private void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        private Socket CreateClientSocket(IPAddress serverIP, ushort serverPort)
        {
            Socket clientSocket = null;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(serverIP, serverPort);
                clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(endPoint);

                return clientSocket;
            }
            catch (Exception e)
            {
                ServiceLog.LogServiceMessage(string.Format("连接[{0}:{1}]失败：{2}", 
                    serverIP, serverPort, e.Message));
                return null;
            }
        }

        private void Run()
        {
            m_isThreadStop = false;

            ServiceLog.LogServiceMessage(string.Format("开始连接[{0}:{1}]", m_serverIP, m_serverPort));
            m_clientSocket = CreateClientSocket(m_serverIP, m_serverPort);
            if (m_clientSocket != null)
            {
                ServiceLog.LogServiceMessage(string.Format("连接[{0}:{1}]成功", m_serverIP, m_serverPort));
                try
                {

                    while (m_clientSocket != null && m_clientSocket.Connected)
                    {
                        Thread.Sleep(47);


                        SendAsync(m_clientSocket, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss\r\n"));

                        StateObject stateObj = new StateObject();
                        stateObj.ClientSocket = m_clientSocket;

                        DisplayMessage("");
                        DisplayMessage("开始接收信息...");
                        m_clientSocket.BeginReceive(stateObj.Buffer, 0, StateObject.BufferSize,
                            0, new AsyncCallback(ReceiveCallback), stateObj);

                    }
                }
                catch (Exception e)
                {
                    DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                }

                DisplayMessage("结束...");
            }
        }

        private void SendAsync(Socket clientSocket, string msg)
        {
            try
            {

                if (clientSocket != null && clientSocket.Connected)
                {
                    DisplayMessage("");
                    DisplayMessage(string.Format("开始发送信息：{0}", msg));
                    byte[] sendBytes = new byte[1024];
                    sendBytes = Encoding.UTF8.GetBytes(msg);
                    DisplayMessage(string.Format("开始发送信息，信息长度：{0}", sendBytes.Length));
                    clientSocket.BeginSend(sendBytes, 0, sendBytes.Length,
                        0, new AsyncCallback(SendCallback), clientSocket);
                }
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = (Socket)ar.AsyncState;
                if (clientSocket != null)
                    clientSocket.EndSend(ar);
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                DisplayMessage("");
                DisplayMessage("正在接收信息...");

                int byteRead = -1;
                StateObject stateObj = (StateObject)ar.AsyncState;
                clientSocket = stateObj.ClientSocket;
                if (clientSocket != null)
                {
                    byteRead = clientSocket.EndReceive(ar);
                } else
                {
                    DisplayMessage("客户端连接无效");
                }

                if (byteRead > 0)
                {
                    string msg = Encoding.Default.GetString(stateObj.Buffer, 0, byteRead);

                    DisplayMessage(string.Format("接收到来自[{0}:{1}]的数据：\r\n{2}", m_serverIP, m_serverPort, msg));

                    //clientSocket.BeginReceive(stateObj.Buffer, 0, StateObject.BufferSize, 
                    //    0, new AsyncCallback(ReceiveCallback), stateObj);
                }
            }
            catch (SocketException se)
            {
                DisplayMessage(string.Format("套接字连接出现异常：\r\n{0},\r\n错误代码：{1}", se.Message, se.SocketErrorCode));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
            catch (Exception e)
            {
                DisplayMessage(string.Format("连接出现异常：{0}", e.Message));
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                }
                catch (Exception)
                {

                }
                return;
            }
        }
    }
}
