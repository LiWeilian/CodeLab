using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CommunicationAppDemo
{
    public partial class FormCommunication : Form
    {
        public FormCommunication()
        {
            InitializeComponent();
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
    }
}
