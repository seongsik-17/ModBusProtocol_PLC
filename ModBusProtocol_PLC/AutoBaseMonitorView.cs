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
        public AutoBaseMonitorView()
        {
            InitializeComponent();
        }

        public void SetInformation(int totalCnt, bool status)
        {
            
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
        public void SetInformation(string ip)
        {
            label4.Text = ip;
		}
	}
}