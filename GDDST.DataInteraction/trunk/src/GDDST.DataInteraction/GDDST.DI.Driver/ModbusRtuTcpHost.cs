using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GDDST.DI.Driver
{
    public class ModbusRtuTcpHost
    {
        private ManualResetEvent m_timeoutObject = new ManualResetEvent(false);
        private Socket m_clientSocket = null;
        /// <summary>
        /// 请求Modbus RTU 数据，发生错误会抛出异常
        /// </summary>
        /// <param name="devAddr">设备地址</param>
        /// <param name="funcCode">功能代码</param>
        /// <param name="startAddr">起始地址</param>
        /// <param name="regCount">读取寄存器数</param>
        /// <returns>请求结果数据内容</returns>
        public string RequestModbusRTUData(byte devAddr, byte funcCode, 
            ushort startAddr, ushort regCount)
        {
            string mbRtuData = string.Empty;
            Socket serverSocket = null;
            try
            {                
                try
                {
                    ushort port = 6008;
                    IPAddress address = IPAddress.Parse("172.16.1.2");
                    IPEndPoint endPoint = new IPEndPoint(address, port);
                    serverSocket = new Socket(AddressFamily.InterNetwork,
                                               SocketType.Stream,
                                               ProtocolType.Tcp);
                    serverSocket.Bind(endPoint);

                    serverSocket.Listen(10);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                m_timeoutObject.Reset();

                AcceptStateObject stateObj = new AcceptStateObject();
                stateObj.ServerSocket = serverSocket;
                
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), stateObj);
                
                if (m_timeoutObject.WaitOne(1000, false) && m_clientSocket != null)
                {
                    //m_clientSocket = serverSocket.Accept();
                    if (m_clientSocket != null)
                    {
                        byte[] temp = new byte[6];
                        temp[0] = devAddr;
                        temp[1] = funcCode;

                        byte[] bStartAddr = BitConverter.GetBytes(startAddr);
                        temp[2] = bStartAddr[1];
                        temp[3] = bStartAddr[0];

                        byte[] bRegCount = BitConverter.GetBytes(regCount);
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
                        //CRC16 低位在前，高位在后
                        modbusRtuReq[6] = bCrc16[0];
                        modbusRtuReq[7] = bCrc16[1];

                        m_clientSocket.Send(modbusRtuReq);

                        Thread.Sleep(500);


                        byte[] modbusRtuResponse = new byte[5 + regCount * 2];
                        try
                        {
                            m_clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);
                            mbRtuData = BitConverter.ToString(modbusRtuResponse);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        m_clientSocket.Close();
                    }


                }
                else
                {
                    throw new Exception("未与数据源建立连接");
                }

            }
            finally
            {
                serverSocket.Close();
                serverSocket = null;
            }
            return mbRtuData;
        }

        private uint ModbusCRC16(byte[] modbusData, int length)
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
                    }
                    else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            m_clientSocket = null;
            try
            {
                AcceptStateObject stateObj = (AcceptStateObject)ar.AsyncState;
                m_clientSocket = stateObj.ServerSocket.EndAccept(ar);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                m_timeoutObject.Set();
            }
        }
    }

    class AcceptStateObject
    {
        public Socket ServerSocket { get; set; }
    }
}
