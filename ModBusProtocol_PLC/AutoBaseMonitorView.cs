using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModBusProtocol_PLC
{
    public partial class AutoBaseMonitorView : UserControl
    {
        string ip;
      
		public AutoBaseMonitorView(string ip)
        {
            InitializeComponent();
            this.ip = ip;
		}

        public void SetInformation(int totalCnt, bool status)
        {
            SetInformation();
			label5.Text = totalCnt.ToString();
            label6.Text = status ? "연결됨" : "연결안됨";
            if (status)
            {
                label6.ForeColor = Color.Green;
            }
            else
            {
                label6.ForeColor = Color.Red;
            }
        }
        public void SetInformation()
        {
            label4.Text = ip;
		}
	}
}