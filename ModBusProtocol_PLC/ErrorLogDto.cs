using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ModBusProtocol_PLC
{
    public class ErrorLogDto
    {
        
        public int ErrorId { get; set; }
        public string LogTime { get; set; }
        public string ErrorMsg { get; set; }
        public int ErrorCode { get; set; }
		public string IpAdrr { get; set; }
	}
}
