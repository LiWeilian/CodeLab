using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinSvcDemo
{
    public partial class WinSvcDemo : ServiceBase
    {
        public WinSvcDemo()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            FileStream fs = new FileStream(@"C:\WinSvcDemo.log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("WindowsService: Service Started at " + DateTime.Now.ToString() + "\n");

            sw.Flush();
            sw.Close();
            fs.Close();
        }

        protected override void OnStop()
        {
            FileStream fs = new FileStream(@"C:\WinSvcDemo.log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("WindowsService: Service Stopped at " + DateTime.Now.ToString() + "\n");
            sw.Flush();
            sw.Close();
            fs.Close();            
        }
    }
}
