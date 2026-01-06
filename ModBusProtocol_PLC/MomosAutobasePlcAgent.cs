using System;
using System.Collections.Generic;
using System.Text;

namespace ModBusProtocol_PLC
{
    internal class MomosAutobasePlcAgent
    {
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
		}
	}

    
}
