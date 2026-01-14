using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace ModBusProtocol_PLC
{
    public static class seongsiksUtils
    {
        private static Config config = getConfigData();
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
            }
            else
            {
                MessageBox.Show("config.json 파일이 존재하지 않습니다.");
            }

            return config;
        }
		#region 
		//AICPL8 데이터 가져오기
		public static string getDataFromAICPL8(string ip)
        {
            int port = config.Port;

            try
            {
                using (TcpClient client = new TcpClient(ip, port))
                using (NetworkStream stream = client.GetStream())
                {
                    //클라이언트 응답 대기 시간 설정
                    client.ReceiveTimeout = 2000;
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
		#endregion
		//쓰레드로 돌아갈 함수
		public static ResultDataDto FunctionForThread(string ip)
        {
            //TcpClient client = new TcpClient();
            //NetworkStream stream = null;
            //try
            //{
            //    client.Connect(ip, config.Port);
            //    stream = client.GetStream();
            //}
            //catch
            //{
            //    //에러 로그 로직 작성 필요 
            //    throw new Exception("장비 연결 실패!");
            //}
            //// Modbus 요청 패킷 생성 (예: 읽기 명령)
            //byte[] request = AICPL8Driver.ReadMultipleRegisterReaderTCP(ip,config.Port,0,2);
            //stream.Write(request, 0, request.Length);
            //// 응답 받기
            //byte[] response = new byte[256];
            //int bytesRead = stream.Read(response, 0, response.Length);
            
            //// 응답 처리 (예: 데이터 출력)
            //int data = combineBytesToInt(response[9], response[10]);
            ////textBox3.Text = data.ToString();

            //data가 0으로 노이즈가 생기면 continue
            //string returnData = data.ToString();

            byte[] result = AICPL8Driver.ReadMultipleRegisterReaderTCP(ip,config.Port,0,2);

            int cnt = combineBytesToInt(result[9],result[10]);
            int runstop = combineBytesToInt(result[11],result[12]);

            ResultDataDto data = new ResultDataDto();
            data.Count = cnt;
            if(runstop == 0)
            { data.Runstop = false; }
            else { data.Runstop = true; }


                return data;
        }

       

        
    }
}