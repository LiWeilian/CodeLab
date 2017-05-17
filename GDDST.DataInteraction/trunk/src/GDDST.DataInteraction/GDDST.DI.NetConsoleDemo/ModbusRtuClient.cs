using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace GDDST.DI.NetConsoleDemo
{
    class ModbusRtuClient
    {
        static public void Run()
        {
            SerialPort sp = new SerialPort();
            Console.WriteLine("端口号：");
            sp.PortName = Console.ReadLine();
            sp.BaudRate = 9600;
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            sp.ReadTimeout = 5000;
            sp.WriteTimeout = 1000;
            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }

            }
            catch (Exception ex)
            {
                sp = null;
                Console.WriteLine(ex.Message);
                return;
            }
            
            while (true)
            {
                //Thread.Sleep(1000);
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();

                byte[] m = { 0x01, 0x03, 0x00, 0x00, 0x00, 0x0A, 0xc5, 0xcd };
                Console.WriteLine(string.Format("Write:\r\n{0}", BitConverter.ToString(m)));
                sp.Write(m, 0, m.Length);

                Thread.Sleep(1000);
                byte[] b = new byte[128];
                sp.Read(b, 0, b.Length);
                Console.WriteLine(string.Format("Write:\r\n{0}", BitConverter.ToString(b)));

                if (Console.KeyAvailable)
                {
                    break;
                }
            }

            sp.Close();

        }
    }
    
}
