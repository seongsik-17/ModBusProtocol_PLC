using System;
using System.Collections.Generic;
using System.Text;

namespace ModBusProtocol_PLC
{
    internal class AICPL8Driver
    {
        public byte[] TCPHoldingRegisterReader()
        {
            return new byte[0]; 
        }
        public byte[] RTUHoldingRegisterReader()
        {
            return new byte[0]; 
        }
    }
}
