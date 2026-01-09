using System;
using System.Collections.Generic;
using System.Text;

namespace ModBusProtocol_PLC
{
    public class ReceivedDataDto
    {
        public string ip { get; set; }
        public int count { get; set; }
        public bool runstop { get; set; }
        public string receivedTimeStamp { get; set; }
    }
}