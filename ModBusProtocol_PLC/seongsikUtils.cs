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

        //쓰레드로 돌아갈 함수
        public static string FunctionForThread(string ip)
        {
            TcpClient client = new TcpClient();
            NetworkStream stream = null;
            try
            {
                client.Connect(ip, getConfigData().Port);
                stream = client.GetStream();
            }
            catch
            {
                throw new Exception("장비 연결 실패!");
            }
            // Modbus 요청 패킷 생성 (예: 읽기 명령)
            byte[] request = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, 0x00, 0x64, 0x00, 0x01 };
            stream.Write(request, 0, request.Length);
            // 응답 받기
            byte[] response = new byte[256];
            int bytesRead = stream.Read(response, 0, response.Length);
            // 응답 처리 (예: 데이터 출력)
            int data = combineBytesToInt(response[9], response[10]);
            //textBox3.Text = data.ToString();

            string returnData = data.ToString();

            return returnData;
        }

        //생성된 뷰를 업데이트 해주는 함수
        public static void UpdatetoBaseMonitorView(AutoBaseMonitorView view, string ip, int count, bool runstop)
        {
            Task.Run(() =>
            {
                view.Invoke(new Action(() =>
{
    view.SetInformation(count, runstop);
}));
            });
        }

        //값 변경이 감지된 경우 변경된 값을 업데이트 해주기
        public static void UpdateChangedValue(string ip, AutoBaseMonitorView view)
        {
            TcpClient client = new TcpClient();
            NetworkStream stream = null;
            try
            {
                client.Connect(ip, getConfigData().Port);
                stream = client.GetStream();
            }
            catch
            {
                throw new Exception("장비 연결 실패!");
            }
            string data = FunctionForThread(ip);
            int totalCnt = int.Parse(data);
            //UI 스레드에서 업데이트
        }
    }
}