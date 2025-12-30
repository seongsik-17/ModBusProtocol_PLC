using System.Net.Sockets;

namespace ModBusProtocol_PLC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string ip = "10.8.38.236";
        private int port = 13890;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(ip, port);
                textBox1.Text = "연결성공";
            }
            catch (Exception ex)
            {
                textBox1.Text = "연결실패";
                MessageBox.Show(ex.Message);
            }
        }

        //데이터 가져오기
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = seongsiksUtils.getDataFromAICPL8();
		}
    }
}