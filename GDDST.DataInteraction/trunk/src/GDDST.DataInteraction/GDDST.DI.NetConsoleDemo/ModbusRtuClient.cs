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
            #region 创建串口通信连接
            SerialPort serialPort = new SerialPort();
            Console.WriteLine("串行端口号：");
            serialPort.PortName = Console.ReadLine();

            Console.WriteLine("波特率：");
            string sBaudRate = Console.ReadLine();
            int iBaudRate;
            if (int.TryParse(sBaudRate, out iBaudRate))
            {
                serialPort.BaudRate = iBaudRate;
            }
            else
            {
                serialPort.BaudRate = 9600;
            }

            Console.WriteLine("数据位(7|8)：");
            string sDataBits = Console.ReadLine();
            switch (sDataBits)
            {
                case "7":
                    serialPort.DataBits = 7;
                    break;
                default:
                    serialPort.DataBits = 8;
                    break;
            }

            Console.WriteLine("校验方式(NONE|ODD|EVEN)：");
            string parity = Console.ReadLine();
            switch (parity.ToUpper().Trim())
            {
                case "NONE":
                    serialPort.Parity = Parity.None;
                    break;
                case "ODD":
                    serialPort.Parity = Parity.Odd;
                    break;
                case "EVEN":
                    serialPort.Parity = Parity.Even;
                    break;
                default:
                    serialPort.Parity = Parity.None;
                    break;
            }

            Console.WriteLine("停止位(0|1|2)：");
            string stopBits = Console.ReadLine();
            switch (stopBits.ToUpper().Trim())
            {
                case "0":
                    serialPort.StopBits = StopBits.None;
                    break;
                case "1":
                    serialPort.StopBits = StopBits.One;
                    break;
                case "2":
                    serialPort.StopBits = StopBits.Two;
                    break;
                default:
                    serialPort.StopBits = StopBits.One;
                    break;
            }

            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = -1;
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                Console.WriteLine(string.Format("已连接串行端口[{0}]\r\n", serialPort.PortName));

            }
            catch (Exception ex)
            {
                serialPort = null;
                Console.WriteLine(ex.Message);
                return;
            }
            #endregion

            while (true)
            {
                #region 生成Modbus RTU请求数据，并发送至设备
                string devAddr;
                byte bDevAddr;
                do
                {
                    Console.WriteLine("请输入设备地址码(1~128，默认1)：");
                    devAddr = Console.ReadLine();
                    if (devAddr.Trim() == string.Empty)
                    {
                        devAddr = "1";
                    }
                } while (!byte.TryParse(devAddr, out bDevAddr));


                string sFuncCode;
                byte bFuncCode;
                do
                {
                    Console.WriteLine("请输入功能码(1~128，默认3)：");
                    sFuncCode = Console.ReadLine();
                    if (sFuncCode.Trim() == string.Empty)
                    {
                        sFuncCode = "3";
                    }
                } while (!byte.TryParse(sFuncCode, out bFuncCode));


                string sStartAddr;
                ushort iStartAddr;
                do
                {
                    Console.WriteLine("请输入起始寄存器地址(0~65535)：");
                    sStartAddr = Console.ReadLine();
                } while (!ushort.TryParse(sStartAddr, out iStartAddr));

                string sRegCount;
                ushort iRegCount;
                do
                {
                    Console.WriteLine("请输入读取寄存器数量(1~128)：");
                    sRegCount = Console.ReadLine();
                } while (!ushort.TryParse(sRegCount, out iRegCount) || (iRegCount > 128));

                byte[] temp = new byte[6];
                temp[0] = bDevAddr;
                temp[1] = bFuncCode;

                byte[] bStartAddr = BitConverter.GetBytes(iStartAddr);
                temp[2] = bStartAddr[1];
                temp[3] = bStartAddr[0];

                byte[] bRegCount = BitConverter.GetBytes(iRegCount);
                temp[4] = bRegCount[1];
                temp[5] = bRegCount[0];

                uint iCRC16 = ModbusCRC16(temp, temp.Length);
                byte[] bCrc16 = BitConverter.GetBytes(iCRC16);

                byte[] modbusRtuReq = new byte[8];
                modbusRtuReq[0] = temp[0];
                modbusRtuReq[1] = temp[1];
                modbusRtuReq[2] = temp[2];
                modbusRtuReq[3] = temp[3];
                modbusRtuReq[4] = temp[4];
                modbusRtuReq[5] = temp[5];
                //CRC16 地位在前，高位在后
                modbusRtuReq[6] = bCrc16[0];
                modbusRtuReq[7] = bCrc16[1];
                

                try
                {
                    Console.WriteLine("正在发送 Modbus RTU 请求数据至Modbus设备...");
                    Console.WriteLine(string.Format("数据内容：{0}\r\n", BitConverter.ToString(modbusRtuReq)));
                    serialPort.Write(modbusRtuReq, 0, modbusRtuReq.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("发送 Modbus RTU 请求数据至Modbus设备时发生错误：{0}\r\n", e.Message);
                    continue;
                }
                #endregion

                #region 读取设备返回数据
                //延时，避免数据未返回就读取
                Thread.Sleep(1000);
                byte[] respDataLen = new byte[2];
                respDataLen[0] = modbusRtuReq[5];
                respDataLen[1] = modbusRtuReq[4];
                int iRespDataLen = BitConverter.ToUInt16(respDataLen, 0);
                Console.WriteLine("回复数据长度：{0}", iRespDataLen);
                byte[] modbusRtuResponse = new byte[5 + iRespDataLen * 2];
                try
                {
                    Console.WriteLine("正在从Modbus设备接收 Modbus RTU 回复数据...");

                    serialPort.Read(modbusRtuResponse, 0, modbusRtuResponse.Length);

                    Console.WriteLine(string.Format("数据内容：{0}\r\n", BitConverter.ToString(modbusRtuResponse)));
                }
                catch (Exception e)
                {
                    Console.WriteLine("从Modbus设备接收 Modbus RTU 回复数据时发生错误：{0}\r\n", e.Message);
                    
                    continue;
                }
                #endregion

                if (Console.KeyAvailable)
                {
                    break;
                }
            }

            serialPort.Close();

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
