using System;
using System.Collections.Generic;
using System.Text;

namespace ModBusProtocol_PLC
{
    public class Config
    {
        public string ErrorDB {  get; set; }
        public int SetInterval { get; set; }
        public string DbPath { get; set; }
        public List<string> Ip { get; set; }
		public int Port { get; set; }
	}
}
