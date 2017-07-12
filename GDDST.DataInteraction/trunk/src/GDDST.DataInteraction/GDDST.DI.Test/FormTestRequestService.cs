using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace GDDST.DI.Test
{
    public partial class FormTestRequestService : Form
    {
        private bool canRequest = true;
        public FormTestRequestService()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int interval = 0;
                if(!int.TryParse(txtInterval.Text, out interval))
                {
                    interval = 0;
                }

                if (interval > 0)
                {
                    btnSend.Enabled = false;
                    btnStop.Enabled = true;
                }

                while (canRequest)
                {
                    //string postData = "{\"ServerID\":\"GDDST001\",\"DeviceAddr\":\"1\",\"FunctionCode\":\"3\",\"StartAddr\":\"0\",\"RegCount\":\"10\"}";
                    string postData = txtRequestBody.Text;
                    byte[] postDataByte = Encoding.UTF8.GetBytes(postData);

                    //"http://172.16.1.2:8734/gddstdataserver/dataservice/modbustcp"
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(txtURL.Text);
                    request.Method = "POST";
                    request.ContentType = "application/json;charset=utf-8";
                    request.ContentLength = postData.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postDataByte, 0, postDataByte.Length);
                    }

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream responseStream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(responseStream);
                    txtResponse.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss\r\n"));
                    txtResponse.AppendText(sr.ReadToEnd());
                    txtResponse.AppendText("\r\n\r\n");

                    if (interval <= 0)
                    {
                        break;
                    } else
                    {
                        Thread.Sleep(interval);
                    }
                    Application.DoEvents();
                }
            }
            finally
            {
                btnSend.Enabled = true;
                btnStop.Enabled = false;
                canRequest = true;
            }
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            canRequest = false;
        }
    }
}
