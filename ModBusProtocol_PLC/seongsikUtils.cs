using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Text.Json;

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
        public static Config getConfigData()
        {
            Config config = new Config();
            //Todo: config 데이터 가져오는 로직 작성 필요
            string fileAddr = "config.json";
            if (File.Exists(fileAddr))
            {
                string jsonString = File.ReadAllText(fileAddr);
                //MessageBox.Show(jsonString);
                config = JsonSerializer.Deserialize<Config>(jsonString);
                //MessageBox.Show(config.Ip);
            }
            else
            {
                MessageBox.Show("config.json 파일이 존재하지 않습니다.");
            }

            return config;
        }

        //AICPL8 데이터 가져오기
        public static string getDataFromAICPL8(string ip)
        {
            int port = getConfigData().Port;

            try
            {
				using (TcpClient client = new TcpClient(ip, port))
				using (NetworkStream stream = client.GetStream())
				{
					//클라이언트 응답 대기 시간 설정
					client.ReceiveTimeout = 2000; // 2초
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
            catch
            {
                throw new Exception("장비 연결 실패!");
			}
           
        }
        //쓰레드로 돌아갈 함수 
        public static void FunctionFotThread()
        { 
            
        }
    }
}