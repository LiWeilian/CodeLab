using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GDDST.DI.Driver
{
    public class TCPServerHost
    {
        private IPAddress server_ip;
        private ushort server_port;
        private Socket clientSocket = null;
        public TCPServerHost(IPAddress server_ip, 
            ushort server_port)
        {
            this.server_ip = server_ip;
            this.server_port = server_port;
        }

        public void Run()
        {
            IPEndPoint endPoint = new IPEndPoint(server_ip, server_port);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);

            try
            {
                //while (true)
                //{
                //    Socket clientSocket = serverSocket.Accept();

                //}
                clientSocket = serverSocket.Accept();
            }
            catch (Exception)
            {

            }
        }

        public string RequestModbusRTUData(byte devAddr, 
            byte funcCode, 
            ushort startAddr, 
            ushort regCount)
        {
            string mbRtuData = string.Empty;

            if (clientSocket == null || !clientSocket.Connected)
            {
                return mbRtuData;
            }

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

            clientSocket.Send(modbusRtuReq);

            Thread.Sleep(500);


            byte[] modbusRtuResponse = new byte[5 + regCount * 2];
            try
            {
                clientSocket.Receive(modbusRtuResponse, modbusRtuResponse.Length, SocketFlags.None);
                mbRtuData = BitConverter.ToString(modbusRtuResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
    }
}
