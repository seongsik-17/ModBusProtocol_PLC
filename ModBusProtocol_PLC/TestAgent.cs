using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModBusProtocol_PLC
{
    internal class TestAgent
    {
        public event EventHandler<(string ip, int count, bool runstop)> DataReceived;

        private Config config = seongsiksUtils.getConfigData();
        private string ip;
        private int port;

        public TestAgent(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Start()
        {
            if (!seongsiksUtils.ConnectionTimer(ip, config.Port))
            {
                throw new Exception();
            }
            Task.Run(DataProc);
        }

        public void DataProc()
        {
            int loopCount = 0;
            int prevCount = 0;
            bool prevStatus = false;
            int errorCnt = 0;
            ResultDataDto resultData = new ResultDataDto();

            try
            {
                var lastData = DbController.SelectOne(ip);
                if (lastData != null)
                {
                    prevCount = lastData.count;
                    prevStatus = lastData.runstop;
                }
                else
                {
                    prevCount = 0;
                    prevStatus = false;
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
                    resultData = seongsiksUtils.FunctionForThread(ip);
                    int currentCount = resultData.Count;
                    bool currentStatus = resultData.Runstop;
					//데이터 변환을 감지한 순간 모종의 사유로 카운터가 초기화 될 경우 DB에서 값을 가져옴
					if (prevCount != 0 && currentCount == 0)
					{
						prevCount = DbController.SelectOne(ip).count;

                        continue;
					}

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
                    errorCnt = 0;
                }
                catch (Exception ex)
                {
                    //데이터 게더링 실패로그 남기기 + 5회 재시도
                    if (errorCnt > 5)
                    {
                        ErrorLogDto error = new ErrorLogDto();
                        error.IpAdrr = ip;
                        error.ErrorMsg = "데이터 게더링 실패! " + ex.Message;
                        error.LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        DbController.WriteErrorLog(error);

                        throw new Exception();
                    }

                    errorCnt++;
                    continue;
                }

                Thread.Sleep(config.SetInterval * 1000); // confing.SetInterval * 1000
            }
        }
    }
}