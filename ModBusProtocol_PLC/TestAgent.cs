using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModBusProtocol_PLC
{
    internal class TestAgent
    {
        public event EventHandler<(string ip, int count, bool runstop)> DataReceived;

        private string ip;
        private int port;

        public TestAgent(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Start()
        {
            Task.Run(DataProc);
        }

        //DB에 가동 비가동 정보 어떤방식으로 넣을지 고민
        public void DataProc()
        {
            int prevCount = 0;
            bool prevStatus = false;

            try
            {
                var lastData = DbController.SelectOne(ip);
                if (lastData != null)
                {
                    prevCount = lastData.count;
                    prevStatus = lastData.runstop;
                }
            }
            catch (Exception ex)
            {
                //
                MessageBox.Show("DataProc()");
            }

            while (true)
            {
                try
                {
                    //ResultData를 통해 카운트와 가동/비가동 정보를 가져오는 것에 맞춰 구조를 변경해야한다.
                    string rawData = seongsiksUtils.FunctionForThread(ip);

                    if (int.TryParse(rawData, out int currentCount))
                    {
                        bool currentStatus = true;

                        if (prevCount != currentCount || prevStatus != currentStatus)
                        {
                            DataReceived?.Invoke(this, (this.ip, currentCount, currentStatus));
                            ReceivedDataDto newData = new ReceivedDataDto()
                            {
                                ip = this.ip,
                                count = currentCount,
                                runstop = currentStatus,
                                receivedTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            DbController.InsertData(newData);
                            prevCount = currentCount;
                            prevStatus = currentStatus;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //데이터 게더링 실패로그 남기기 + 5회 재시도
                    MessageBox.Show(ex.Message);
                }

                //Thread.Sleep(2000); // 2초 대기
            }
        }
    }
}