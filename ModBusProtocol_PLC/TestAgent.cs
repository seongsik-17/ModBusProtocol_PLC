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
                MessageBox.Show("DataProc()");
            }

            while (true)
            {
                try
                {
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
                    //로그 남기기
                }

                Thread.Sleep(2000); // 2초 대기
            }
        }
    }
}