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
    class ClientTest
    {
        private Socket m_clientSocket = null;
        private Thread m_clientThread = null;
        private bool m_isThreadStop = false;
        public void OnStart(string dataProtocol, string serverIP, string serverPort, 
            GetDataServiceDAL dal)
        {
            IPAddress ip;
            if (!IPAddress.TryParse(serverIP, out ip))
            {
                ServiceLog.LogServiceMessage(string.Format("IP地址[{0}]无效。", serverIP));
                return;
            }
            
            ushort port;
            if (!ushort.TryParse(serverPort, out port))
            {
                ServiceLog.LogServiceMessage(string.Format("端口[{0}]无效。", serverPort));
                return;
            }

            ServiceLog.LogServiceMessage(string.Format("开始连接[{0}:{1}]", serverIP, serverPort));
            m_clientSocket = CreateClientSocket(ip, port);
            if (m_clientSocket != null)
            {
                ServiceLog.LogServiceMessage(string.Format("连接[{0}:{1}]成功", serverIP, serverPort));

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

        }

        public void OnStop()
        {
            m_isThreadStop = true;
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
        }
    }
}
