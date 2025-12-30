using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ModBusProtocol_PLC
{
    public static class seongsiksUtils
    {
        //바이트 합치기
        public static int combineBytesToInt(byte highByte, byte lowByte)
        {
            return (highByte << 8) | lowByte;
        }
        //config 데이터 가져오기
        private static Config getConfigData()
        {
            Config config = new Config();
            return config;
		}

		//AICPL8 데이터 가져오기
		public static string getDataFromAICPL8()
        {
            string ip = "10.8.38.236";
            int port = 13890;

			using (TcpClient client = new TcpClient(ip, port))
            using (NetworkStream stream = client.GetStream())
            {
                // Modbus 요청 패킷 생성 (예: 읽기 명령)
                byte[] request = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, 0x00, 0x64, 0x00, 0x01 };
                stream.Write(request, 0, request.Length);
                // 응답 받기
                byte[] response = new byte[256];
                int bytesRead = stream.Read(response, 0, response.Length);
                // 응답 처리 (예: 데이터 출력)
                int data = combineBytesToInt(response[9], response[10]);
                //textBox3.Text = data.ToString();
                client.Close();
                stream.Close();

                return data.ToString();
            }
        }
    }
}