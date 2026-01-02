using System.Net.Sockets;

namespace ModBusProtocol_PLC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private string ip = comboBox1.Text;
        //private int port = seongsiksUtils.getConfigData().Port;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //TcpClient client = new TcpClient(ip, port);
                string currentDir = System.IO.Directory.GetCurrentDirectory();
                textBox3.Text = "연결성공" + currentDir;
            }
            catch (Exception ex)
            {
                textBox3.Text = "연결실패";
                MessageBox.Show(ex.Message);
            }
        }

        //데이터 가져오기
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox3.AppendText(seongsiksUtils.getDataFromAICPL8(comboBox1.Text) + "\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Config config = seongsiksUtils.getConfigData();
            foreach (string ip in config.Ip)
            {
                comboBox1.Items.Add(ip);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}