using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ModBusProtocol_PLC
{
    internal class AICPL8Driver
    {
        public enum ModbusFunctionCode : byte
        {
            ReadCoils = 0x01,
            ReadHoldingRegisters = 0x03,
            WriteSingleCoil = 0x05,
            WriteSingleRegister = 0x06,
            WriteMultipleRegisters = 0x10
        }
		
		//TCP 요청 배열 생성 함수
		public static byte[] ReadMultipleRegisterReaderTCP(string ip, int port, int start_addr, int quantity)
        {
            int errorCnt = 0;
			byte[] reciverData = new byte[256];

			byte[] request = new byte[12];
            //Transaction ID
            request[0] = 0x00;
            request[1] = 0x01;
            //Protocol ID
            request[2] = 0x00;
            request[3] = 0x00;
            //Length
            request[4] = 0x00;
            request[5] = 0x06;
            //Unit ID
            request[6] = 0x01;
            //Function Code
            request[7] = (byte)ModbusFunctionCode.ReadHoldingRegisters;
            //Start_addr
            request[8] = (byte)(start_addr >> 8);
            request[9] = (byte)(start_addr & 0xFF);
            //Quantity
            request[10] = (byte)(quantity >> 8);
            request[11] = (byte)(quantity & 0xFF);

            TcpClient client = new TcpClient();
            try
            {
                client.Connect(ip, port);
                NetworkStream stream = client.GetStream();

                stream.Write(request, 0, request.Length);

                byte[]response = new byte[256];

                int byteRead = stream.Read(response, 0, response.Length);

                if (byteRead == 0)
                {
                    errorCnt++;
                }
                if (errorCnt > 5)
                {
                    ErrorLogDto errorLog = new ErrorLogDto();
                    errorLog.IpAdrr = ip;
                    errorLog.ErrorMsg = "ModBus통신 실패!";
                    errorLog.LogTime = DateTime.Now.ToString("yyyy년-MM월-dd일 HH:mm:ss");
					DbController.WriteErrorLog(errorLog);

                    return null;
                }
                //데이터가 복수로 들어오면 {response[9], response[10]},{response[11], response[12]}...
                
                Array.Copy(response,reciverData, byteRead);
                //카운트 초기화
                errorCnt = 0;
                client.Close();

            }
            catch (Exception ex)
            {
                ErrorLogDto log = new ErrorLogDto();
                log.IpAdrr = ip;
                log.ErrorMsg = ex.Message;
                log.LogTime = DateTime.Now.ToString("yyyy년-MM월-dd일 HH:mm:ss");
                DbController.WriteErrorLog(log);
            }

            return reciverData;
        }

        //RTU request Function
        public byte[] ReadMultipleRegisterReaderRTU()
        {
            return new byte[0];
        }
    }
}