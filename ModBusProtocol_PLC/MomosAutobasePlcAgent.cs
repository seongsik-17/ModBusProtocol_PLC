using System;
using System.Collections.Generic;
using System.Text;

namespace ModBusProtocol_PLC
{
    internal class MomosAutobasePlcAgent
    {

        public delegate void DataReceivedHandler(string ip, int count, bool runstop);
        public event DataReceivedHandler DataReceived;

        public event EventHandler<(string ip, int count, bool runstop)> DataReceived2;

		string ip;
        int port;
        public MomosAutobasePlcAgent(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
		}

		//ModbusClient modbusClient = new ModbusClient();

        public void Start(string ip, int port)
        {
            Task.Run(DataProc);
		}
		void DataProc()
        {
            ModbusClient.ReadMultipleRegisters("192.168.0.100",502,0, 10);

            DataReceived?.Invoke(ip, 0, true);
            DataReceived2?.Invoke(this, (ip, 0, true));
		}
	}


    public class frmAgentTest : Form
	{
        List<MomosAutobasePlcAgent> agents = new List<MomosAutobasePlcAgent>();
        public void frmInit()
        {
            for (int i = 0; i < 10; i++)
            {
                MomosAutobasePlcAgent agent = new MomosAutobasePlcAgent("192.168.0." + (100 + i).ToString(), 502);
                agent.DataReceived += Agent_DataReceived;
                agent.DataReceived2 += Agent_DataReceived2;
				
				agents.Add(agent);
                agent.Start("192.168.0." + (100 + i).ToString(), 502);
			}

        }

        private void Agent_DataReceived2(object? sender, (string ip, int count, bool runstop) e)
        {
            Task.Run(() =>
            {
                string ip = e.ip;
                int count = e.count;
                bool runstop = e.runstop;
                //
            });
		}

        private void Agent_DataReceived(string ip, int count, bool runstop)
        {
            //
        }
    }


}
