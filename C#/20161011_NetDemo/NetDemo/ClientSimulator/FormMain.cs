using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientSimulator
{
    public partial class FormMain : Form
    {
        private Socket mainSocket;
        public FormMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            InitializeSocket();
        }

        private void WriteRunMessage(string type, string msg)
        {
            txtRunMsg.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\r\n"));
            txtRunMsg.AppendText(string.Format("[{0}]{1}\r\n", type, msg));
            txtRunMsg.AppendText("\r\n");
        }

        private void InitializeSocket()
        {
            try
            {
                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                WriteRunMessage("信息", "已创建Socket对象");
            }
            catch (Exception ex)
            {
                WriteRunMessage("异常", ex.Message);
            }
            finally
            {

            }
        }

        private void DisconnectSocket()
        {
            if (mainSocket.Connected)
            {
                mainSocket.Disconnect(false);
            }
        }

        private void ReceiveMessage(object obj)
        {
            if (obj is Socket)
            {
                Socket socket = obj as Socket;
                while (true)
                {
                    byte[] buffer = new byte[4096];
                    int recLen = 0;
                    try
                    {
                        recLen = socket.Receive(buffer, buffer.Length, SocketFlags.None);

                        System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                        string recStr = utf8.GetString(buffer, 0, recLen);

                        txtRecMsg.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\r\n"));
                        txtRecMsg.AppendText(string.Format("[接收消息]{0}\r\n", recStr));
                    }
                    catch (SocketException se)
                    {
                        WriteRunMessage("异常", se.SocketErrorCode.ToString());
                        switch (se.SocketErrorCode)
                        {
                            case SocketError.Success:
                                continue;
                            default:
                                return;
                        }
                    }
                }
            }            
            
        }

        private void btnConnToServer_Click(object sender, EventArgs e)
        {
            /*
             * SN:,
             * 
             */
            try
            {
                btnConnToServer.Enabled = false;
                Application.DoEvents();

                DisconnectSocket();
                string ipStr = txtServerIP.Text;
                IPAddress ip;
                if (!IPAddress.TryParse(ipStr, out ip))
                {
                    WriteRunMessage("错误", string.Format("IP地址[{0}]无效。", ipStr));
                    return;
                }

                string portStr = txtServerPort.Text;
                ushort port;
                if (!ushort.TryParse(portStr, out port))
                {
                    WriteRunMessage("错误", string.Format("端口[{0}]无效。", portStr));
                    return;
                }

                IPEndPoint endPoint = new IPEndPoint(ip, port);
                try
                {
                    WriteRunMessage("信息", string.Format("正在连接[{0}:{1}]...", ip, port));
                    mainSocket.Connect(endPoint);
                    WriteRunMessage("信息", string.Format("已连接。[{0}]", mainSocket.Handle));
                    try
                    {
                        WriteRunMessage("信息", "开始接收消息...");
                        Thread thread = new Thread(new ParameterizedThreadStart(ReceiveMessage));
                        thread.IsBackground = true;
                        thread.Start(mainSocket);
                    }
                    catch (Exception ex)
                    {
                        WriteRunMessage("异常", ex.Message);
                    }

                }
                catch (SocketException se)
                {
                    WriteRunMessage("异常", se.SocketErrorCode.ToString());
                    return;
                }
            }
            finally
            {
                Thread.Sleep(100);
                btnConnToServer.Enabled = true;
            }
            
        }

        private void btnSendToServer_Click(object sender, EventArgs e)
        {
            try
            {
                btnSendToServer.Enabled = false;
                Application.DoEvents();

                string msg = string.Format("{0}\r\n", txtSendMsg.Text);
                byte[] msgByte = System.Text.Encoding.UTF8.GetBytes(msg);
                try
                {
                    WriteRunMessage("信息", "开始发送消息...");
                    mainSocket.Send(msgByte, msgByte.Length, SocketFlags.None);
                    WriteRunMessage("信息", "已发送消息");
                }
                catch (SocketException se)
                {
                    WriteRunMessage("异常", se.SocketErrorCode.ToString());
                    return;
                }
            }
            finally
            {
                Thread.Sleep(100);
                btnSendToServer.Enabled = true;
            }
            
        }
    }
}
