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

            Console.WriteLine("校验方式(NONE|ODD|EVEN)：");
            string parity = Console.ReadLine();
            switch (parity.ToUpper().Trim())
            {
                case "NONE":
                    sp.Parity = Parity.None;
                    break;
                case "ODD":
                    sp.Parity = Parity.Odd;
                    break;
                case "EVEN":
                    sp.Parity = Parity.Even;
                    break;
                default:
                    sp.Parity = Parity.None;
                    break;
            }

            Console.WriteLine("停止位(0|1|2)：");
            string stopBits = Console.ReadLine();
            switch (stopBits.ToUpper().Trim())
            {
                case "0":
                    sp.StopBits = StopBits.None;
                    break;
                case "1":
                    sp.StopBits = StopBits.One;
                    break;
                case "2":
                    sp.StopBits = StopBits.Two;
                    break;
                default:
                    sp.StopBits = StopBits.One;
                    break;
            }
            
            sp.ReadTimeout = 500;
            sp.WriteTimeout = -1;
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

                byte[] m = { 0x01, 0x03, 0x00, 0x01, 0x00, 0x02 };
                uint crc16 = ModbusCRC16(m, m.Length);

                byte[] crc16b = BitConverter.GetBytes(crc16);

                byte[] rtuData = new byte[8];
                rtuData[0] = m[0];
                rtuData[1] = m[1];
                rtuData[2] = m[2];
                rtuData[3] = m[3];
                rtuData[4] = m[4];
                rtuData[5] = m[5];
                rtuData[6] = crc16b[0];
                rtuData[7] = crc16b[1];

                Console.WriteLine(string.Format("Write:\r\n{0}", BitConverter.ToString(rtuData)));
                sp.Write(rtuData, 0, rtuData.Length);

                Thread.Sleep(1000);
                byte[] b = new byte[9];
                sp.Read(b, 0, b.Length);
                Console.WriteLine(string.Format("Read:\r\n{0}", BitConverter.ToString(b)));

                if (Console.KeyAvailable)
                {
                    break;
                }
            }

            sp.Close();

        }

        private static uint ModbusCRC16(byte[] modbusData, int length)
        {
            uint crc16 = 0xFFFF;

            for (int i = 0; i < length; i++)
            {
                crc16 ^= modbusData[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc16 & 0x01) == 1)
                    {
                        crc16 = (crc16 >> 1) ^ 0xA001;
                    } else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }
    }
    
}
