using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GDDST.DI.NetConsoleDemo
{
    class ModbusTcpClient
    {
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

            IPEndPoint endPoint = new IPEndPoint(ip, port);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("正在连接[{0}:{1}]...", ip, port);

            try
            {
                clientSocket.Connect(endPoint);
                Console.WriteLine("连接成功[{0}]。\r\n", clientSocket.Handle);
            }
            catch (SocketException se)
            {
                Console.WriteLine("连接失败：" + se.SocketErrorCode.ToString());
                return;
            }

            while (true)
            {
                Console.WriteLine("请输入设备地址：");
                string sDevAddr = Console.ReadLine();
                byte iDevAddr;
                if (!byte.TryParse(sDevAddr, out iDevAddr))
                {
                    Console.WriteLine("设备地址[{0}]无效。", sDevAddr);
                    return;
                }
                
                Console.WriteLine("请输入功能代码（读线圈：1，写单个线圈：5，写多个线圈：15，读多寄存器：3，写单个寄存器：6，写多个寄存器：16）：");
                string sFuntCode = Console.ReadLine();
                byte iFuntCode;
                if (!byte.TryParse(sFuntCode, out iFuntCode))
                {
                    Console.WriteLine("设备地址[{0}]无效。", sDevAddr);
                    return;
                } else
                {
                    switch (iFuntCode)
                    {
                        case 1:
                            Console.WriteLine("请输入起始线圈地址：");
                            string sRegAddr01 = Console.ReadLine();
                            ushort iRegAddr01;
                            if (!ushort.TryParse(sRegAddr01, out iRegAddr01))
                            {
                                Console.WriteLine("起始线圈地址[{0}]无效。", sRegAddr01);
                                return;
                            }

                            Console.WriteLine("请输入线圈数量：");
                            string sRegCount01 = Console.ReadLine();
                            ushort iRegCount01;
                            if (!ushort.TryParse(sRegCount01, out iRegCount01))
                            {
                                Console.WriteLine("线圈数量[{0}]无效。", sRegCount01);
                                return;
                            }

                            /*
                             * 01~02：传输标识
                             * 03~04：协议标识
                             * 05~06：此后字节长度
                             * 07：设备标识
                             * 08：功能码
                             * 09~10：起始地址
                             * 11~12：读取线圈数量
                             */
                            //byte[] plc_send01 = new byte[12] { 0x00, 0xAE, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x00, 0x01, 0x00, 0x10 };

                            //只读  
                            byte[] plc_send01 = new byte[12];
                            plc_send01[0] = 0;
                            plc_send01[1] = 1;
                            plc_send01[2] = 0;
                            plc_send01[3] = 0;
                            plc_send01[4] = 0;
                            plc_send01[5] = 6;
                            plc_send01[6] = iDevAddr;
                            plc_send01[7] = 1;
                            byte[] bRegAddr = BitConverter.GetBytes(iRegAddr01);
                            plc_send01[8] = bRegAddr[1];
                            plc_send01[9] = bRegAddr[0];
                            byte[] bRegCount = BitConverter.GetBytes(iRegCount01);
                            plc_send01[10] = bRegCount[1];
                            plc_send01[11] = bRegCount[0];
                            
                            Console.WriteLine("请求内容：");
                            string send01 = System.Text.Encoding.UTF8.GetString(plc_send01, 0, plc_send01.Length);
                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(plc_send01)));
                            Console.WriteLine("");
                            try
                            {
                                //clientSocket.Send(msgByte_send, msgByte_send.Length, SocketFlags.None);
                                clientSocket.Send(plc_send01, plc_send01.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            Console.WriteLine("");
                            Console.WriteLine("正在等待接收信息...");
                            byte iResultLen01 = (byte)(iRegCount01 / 8);
                            if (iRegCount01 % 8 > 0)
                            {
                                iResultLen01++;
                            }
                            Console.WriteLine("数据长度：{0}", iResultLen01);

                            /*
                             * 01~02：传输标识
                             * 03~04：协议标识
                             * 05~06：此后字节长度
                             * 07：设备标识
                             * 08：功能码
                             * 09：返回结果字节数
                             * 10~：返回结果
                             */
                            byte[] recMsgByte01 = new byte[9 + iResultLen01];
                            int recLen01;
                            string mbTcpData01 = string.Empty;
                            try
                            {
                                //Thread.Sleep(5000);
                                recLen01 = clientSocket.Receive(recMsgByte01, recMsgByte01.Length, SocketFlags.None);

                                mbTcpData01 = BitConverter.ToString(recMsgByte01, 9, iResultLen01).Replace("-", string.Empty);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            string recMsg01 = System.Text.Encoding.UTF8.GetString(recMsgByte01, 0, recLen01);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("ASCII: {0}", recMsg01));
                            Console.WriteLine("");

                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(recMsgByte01)));
                            Console.WriteLine("");

                            for (int i = 0; i < iResultLen01; i++)
                            {
                                string sByte = mbTcpData01.Substring(i * 2, 2);
                                byte bByte = Convert.ToByte(sByte, 16);
                                string bin = Convert.ToString(bByte, 2).PadLeft(8, '0');
                                Console.WriteLine(bin);
                            }
                            Console.WriteLine("");

                            break;
                        case 5:
                            break;
                        case 15:
                            /*
                             * 01：传输标志Hi
                             * 02：传输标志Lo
                             * 03、04：协议标志
                             * 05、06：此位置以后字节数
                             * 07：单元标志
                             * 08：功能代码，0x0F表示写多个线圈
                             * 09、10：起始线圈地址
                             * 11、12：线圈数量
                             * 13：数据字节数
                             * 14~：数据
                             */

                            Console.WriteLine("请输入起始线圈地址：");
                            string sRegAddr15 = Console.ReadLine();
                            ushort iRegAddr15;
                            if (!ushort.TryParse(sRegAddr15, out iRegAddr15))
                            {
                                Console.WriteLine("起始线圈地址[{0}]无效。", sRegAddr15);
                                return;
                            }

                            Console.WriteLine("请输入线圈数量：");
                            string sRegCount15 = Console.ReadLine();
                            ushort iRegCount15;
                            if (!ushort.TryParse(sRegCount15, out iRegCount15))
                            {
                                Console.WriteLine("线圈数量[{0}]无效。", sRegCount15);
                                return;
                            }

                            byte dataLen15 = (byte)(iRegCount15 / 8);
                            if (iRegCount15 % 8 > 0)
                            {
                                dataLen15++;
                            }

                            byte[] bInputDatas15 = new byte[dataLen15];

                            string sInputValues15 = string.Empty;

                            for (int i = 0; i < iRegCount15; i++)
                            {
                                Console.WriteLine("请输入第{0}个线圈输入值：", i + 1);
                                string sInput15 = Console.ReadLine();
                                int iInput15;
                                while (!int.TryParse(sInput15, out iInput15) || (iInput15 != 0 && iInput15 != 1))
                                {
                                    Console.WriteLine("输入数值[{0}]无效：", sInput15);
                                    Console.WriteLine("请输入第{0}个线圈输入值：", i + 1);
                                    sInput15 = Console.ReadLine();
                                }

                                sInputValues15 = sInputValues15 + sInput15;

                                //bInputDatas15[iRegCount15 - 1 / 8] = bInputDatas15[iRegCount15 - 1 / 8]
                            }

                            string[] sInputBytes = new string[dataLen15];

                            for (int i = 0; i < dataLen15; i++)
                            {
                                int strLen;
                                if (iRegCount15 - i * 8 < 8)
                                {
                                    strLen = iRegCount15 - i * 8;
                                } else
                                {
                                    strLen = 8;
                                }
                                sInputBytes[i] = sInputValues15.Substring(i * 8, strLen);
                            }

                            for (int i = 0; i < dataLen15; i++)
                            {
                                sInputBytes[i] = getReverse(sInputBytes[i]);

                                Console.WriteLine(sInputBytes[i].PadLeft(8, '0'));
                                //Console.WriteLine(Convert.ToByte(sInputBytes[i], 2));
                                bInputDatas15[i] = Convert.ToByte(sInputBytes[i], 2);
                            }

                            byte[] plc_send15 = new byte[13 + dataLen15];
                            plc_send15[0] = 0;
                            plc_send15[1] = 1;
                            plc_send15[2] = 0;
                            plc_send15[3] = 0;
                            ushort iLen15 = (ushort)(7 + dataLen15);
                            byte[] bLen15 = BitConverter.GetBytes(iLen15);
                            plc_send15[4] = bLen15[1];
                            plc_send15[5] = bLen15[0];
                            plc_send15[6] = iDevAddr;
                            plc_send15[7] = 15;
                            byte[] bRegAddr15 = BitConverter.GetBytes(iRegAddr15);
                            plc_send15[8] = bRegAddr15[1];
                            plc_send15[9] = bRegAddr15[0];
                            byte[] bRegCount15 = BitConverter.GetBytes(iRegCount15);
                            plc_send15[10] = bRegCount15[1];
                            plc_send15[11] = bRegCount15[0];
                            byte byteCount15 = (byte)dataLen15;
                            plc_send15[12] = byteCount15;

                            for (int i = 0; i < bInputDatas15.Length; i++)
                            {
                                plc_send15[13 + i] = bInputDatas15[i];
                            }

                            Console.WriteLine("请求内容：");
                            string send15 = System.Text.Encoding.UTF8.GetString(plc_send15, 0, plc_send15.Length);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(plc_send15)));
                            Console.WriteLine("");
                            try
                            {
                                //clientSocket.Send(msgByte_send, msgByte_send.Length, SocketFlags.None);
                                clientSocket.Send(plc_send15, plc_send15.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            Console.WriteLine("");
                            Console.WriteLine("正在等待接收信息...");
                            byte[] recMsgByte15 = new byte[12];
                            int recLen15;
                            try
                            {
                                //Thread.Sleep(5000);
                                recLen15 = clientSocket.Receive(recMsgByte15, recMsgByte15.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            string recMsg15 = System.Text.Encoding.UTF8.GetString(recMsgByte15, 0, recLen15);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("ASCII: {0}", recMsg15));
                            Console.WriteLine("");

                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(recMsgByte15)));
                            Console.WriteLine("");

                            break;
                        case 3:
                            Console.WriteLine("请输入起始寄存器地址：");
                            string sRegAddr03 = Console.ReadLine();
                            ushort iRegAddr03;
                            if (!ushort.TryParse(sRegAddr03, out iRegAddr03))
                            {
                                Console.WriteLine("起始寄存器地址[{0}]无效。", sRegAddr03);
                                return;
                            }

                            Console.WriteLine("请输入寄存器数量：");
                            string sRegCount03 = Console.ReadLine();
                            ushort iRegCount03;
                            if (!ushort.TryParse(sRegCount03, out iRegCount03))
                            {
                                Console.WriteLine("寄存器数量[{0}]无效。", sRegCount03);
                                return;
                            }

                            //读取从0x29寄存器开始1个寄存器的值
                            //byte[] plc_send = new byte[12] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, 0x00, 0x29, 0x00, 0x01 };

                            //只读  
                            byte[] plc_send03 = new byte[12];
                            plc_send03[0] = 0;
                            plc_send03[1] = 1;
                            plc_send03[2] = 0;
                            plc_send03[3] = 0;
                            plc_send03[4] = 0;
                            plc_send03[5] = 6;
                            plc_send03[6] = iDevAddr;
                            plc_send03[7] = 3;
                            byte[] bRegAddr03 = BitConverter.GetBytes(iRegAddr03);
                            plc_send03[8] = bRegAddr03[1];
                            plc_send03[9] = bRegAddr03[0];
                            byte[] bRegCount03 = BitConverter.GetBytes(iRegCount03);
                            plc_send03[10] = bRegCount03[1];
                            plc_send03[11] = bRegCount03[0];

                            Console.WriteLine("请求内容：");
                            string send03 = System.Text.Encoding.UTF8.GetString(plc_send03, 0, plc_send03.Length);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(plc_send03)));
                            Console.WriteLine("");
                            try
                            {
                                //clientSocket.Send(msgByte_send, msgByte_send.Length, SocketFlags.None);
                                clientSocket.Send(plc_send03, plc_send03.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            Console.WriteLine("");
                            Console.WriteLine("正在等待接收信息...");
                            byte[] recMsgByte03 = new byte[9 + iRegCount03 * 2];
                            int recLen03;
                            try
                            {
                                //Thread.Sleep(5000);
                                recLen03 = clientSocket.Receive(recMsgByte03, recMsgByte03.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            string recMsg03 = System.Text.Encoding.UTF8.GetString(recMsgByte03, 0, recLen03);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("ASCII: {0}", recMsg03));
                            Console.WriteLine("");

                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(recMsgByte03)));
                            Console.WriteLine("");
                            break;
                        case 6:
                            Console.WriteLine("请输入寄存器地址：");
                            string sRegAddr06 = Console.ReadLine();
                            ushort iRegAddr06;
                            if (!ushort.TryParse(sRegAddr06, out iRegAddr06))
                            {
                                Console.WriteLine("寄存器地址[{0}]无效。", sRegAddr06);
                                return;
                            }

                            Console.WriteLine("请输入数值：");
                            string sInputValue06 = Console.ReadLine();
                            ushort iInputValue06;
                            if (!ushort.TryParse(sInputValue06, out iInputValue06))
                            {
                                Console.WriteLine("输入数值[{0}]无效：", sInputValue06);
                                return;
                            }

                            

                            //将单个值0x0003写入寄存器0x01
                            //byte[] plc_send06 = new byte[12] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x01, 0x00, 0x03 };
                            //byte[] plc_send06 = new byte[12] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x01, 0x28, 0x00, 0x01 };

                            byte[] plc_send06 = new byte[12];
                            plc_send06[0] = 0;
                            plc_send06[1] = 1;
                            plc_send06[2] = 0;
                            plc_send06[3] = 0;
                            plc_send06[4] = 0;
                            plc_send06[5] = 6;
                            plc_send06[6] = iDevAddr;
                            plc_send06[7] = 6;
                            byte[] bRegAddr06 = BitConverter.GetBytes(iRegAddr06);
                            plc_send06[8] = bRegAddr06[1];
                            plc_send06[9] = bRegAddr06[0];
                            byte[] inputValue06Byte = BitConverter.GetBytes(iInputValue06);
                            plc_send06[10] = inputValue06Byte[1];
                            plc_send06[11] = inputValue06Byte[0];

                            Console.WriteLine("请求内容：");
                            string send06 = System.Text.Encoding.UTF8.GetString(plc_send06, 0, plc_send06.Length);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(plc_send06)));
                            Console.WriteLine("");
                            try
                            {
                                //clientSocket.Send(msgByte_send, msgByte_send.Length, SocketFlags.None);
                                clientSocket.Send(plc_send06, plc_send06.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            Console.WriteLine("");
                            Console.WriteLine("正在等待接收信息...");
                            byte[] recMsgByte06 = new byte[12];
                            int recLen06;
                            try
                            {
                                //Thread.Sleep(5000);
                                recLen06 = clientSocket.Receive(recMsgByte06, recMsgByte06.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            string recMsg06 = System.Text.Encoding.UTF8.GetString(recMsgByte06, 0, recLen06);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("ASCII: {0}", recMsg06));
                            Console.WriteLine("");

                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(recMsgByte06)));
                            Console.WriteLine("");

                            break;
                        case 16:
                            
                            Console.WriteLine("请输入起始寄存器地址：");
                            string sRegAddr16 = Console.ReadLine();
                            ushort iRegAddr16;
                            if (!ushort.TryParse(sRegAddr16, out iRegAddr16))
                            {
                                Console.WriteLine("起始寄存器地址[{0}]无效。", sRegAddr16);
                                return;
                            }

                            Console.WriteLine("请输入寄存器数量：");
                            string sRegCount16 = Console.ReadLine();
                            ushort iRegCount16;
                            if (!ushort.TryParse(sRegCount16, out iRegCount16))
                            {
                                Console.WriteLine("寄存器数量[{0}]无效。", sRegCount16);
                                return;
                            }

                            byte[] inputValues16Byte = new byte[iRegCount16 * 2];

                            for (int i = 0; i < iRegCount16; i++)
                            {
                                Console.WriteLine("请输入第{0}个寄存器输入值：", i + 1);
                                string sInputValue16 = Console.ReadLine();
                                ushort iInputValue16;
                                while (!ushort.TryParse(sInputValue16, out iInputValue16))
                                {
                                    Console.WriteLine("输入数值[{0}]无效：", sInputValue16);
                                    Console.WriteLine("请输入第{0}个寄存器输入值：", i + 1);
                                    sInputValue16 = Console.ReadLine();
                                }

                                byte[] inputValue16Byte = BitConverter.GetBytes(iInputValue16);
                                inputValues16Byte[i * 2] = inputValue16Byte[1];
                                inputValues16Byte[i * 2 + 1] = inputValue16Byte[0];
                            }

                            //将两个值0x0001,0x0002写入起始寄存器0x0001
                            /*
                             * 01：传输标志Hi
                             * 02：传输标志Lo
                             * 03、04：协议标志
                             * 05、06：此位置以后字节数
                             * 07：单元标志
                             * 08：功能代码，0x10表示写多个寄存器
                             * 09、10：起始寄存器
                             * 11、12：寄存器数量
                             * 13：数据字节数
                             * 14~：数据
                             */
                            //byte[] plc_send = new byte[17] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x0B, 0x01, 0x10, 0x00, 0x01, 0x00, 0x02, 0x04, 0x00, 0x01, 0x00, 0x02 };


                            byte[] plc_send16 = new byte[13 + iRegCount16 * 2];
                            plc_send16[0] = 0;
                            plc_send16[1] = 1;
                            plc_send16[2] = 0;
                            plc_send16[3] = 0;
                            ushort iLen16 = (ushort)(9 + iRegCount16 * 2);
                            byte[] bLen16 = BitConverter.GetBytes(iLen16);
                            plc_send16[4] = bLen16[1];
                            plc_send16[5] = bLen16[0];
                            plc_send16[6] = iDevAddr;
                            plc_send16[7] = 16;
                            byte[] bRegAddr16 = BitConverter.GetBytes(iRegAddr16);
                            plc_send16[8] = bRegAddr16[1];
                            plc_send16[9] = bRegAddr16[0];
                            byte[] bRegCount16 = BitConverter.GetBytes(iRegCount16);
                            plc_send16[10] = bRegCount16[1];
                            plc_send16[11] = bRegCount16[0];
                            byte byteCount = (byte)(iRegCount16 * 2);
                            plc_send16[12] = byteCount;

                            for (int i = 0; i < inputValues16Byte.Length; i++)
                            {
                                plc_send16[13 + i] = inputValues16Byte[i];
                            }

                            Console.WriteLine("请求内容：");
                            string send16 = System.Text.Encoding.UTF8.GetString(plc_send16, 0, plc_send16.Length);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(plc_send16)));
                            Console.WriteLine("");
                            try
                            {
                                //clientSocket.Send(msgByte_send, msgByte_send.Length, SocketFlags.None);
                                clientSocket.Send(plc_send16, plc_send16.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("发送消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            Console.WriteLine("");
                            Console.WriteLine("正在等待接收信息...");
                            byte[] recMsgByte16 = new byte[12 + iRegCount16 * 2];
                            int recLen16;
                            try
                            {
                                //Thread.Sleep(5000);
                                recLen16 = clientSocket.Receive(recMsgByte16, recMsgByte16.Length, SocketFlags.None);
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("接收消息失败：{0}", se.SocketErrorCode.ToString());
                                break;
                            }

                            string recMsg16 = System.Text.Encoding.UTF8.GetString(recMsgByte16, 0, recLen16);
                            //string recMsg = System.Text.Encoding.ASCII.GetString(recMsgByte, 0, recLen);
                            Console.WriteLine(string.Format("ASCII: {0}", recMsg16));
                            Console.WriteLine("");

                            Console.WriteLine(string.Format("Bytes: {0}", BitConverter.ToString(recMsgByte16)));
                            Console.WriteLine("");
                            break;
                        default:
                            Console.WriteLine(string.Format("不支持此功能代码[{0}]", iFuntCode));
                            break;
                    }
                }

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
        }

        public static string getReverse(string pStr)
        {
            string str = "";
            char[] strTemp = pStr.ToCharArray();
            Array.Reverse(strTemp);
            string[] strArr = Array.ConvertAll<char, string>(strTemp, delegate (char c) { return c.ToString(); });
            str = string.Join("", strArr);

            return str;
        }
    }
}
