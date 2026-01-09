using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ModBusProtocol_PLC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //CancellationTokenSource
        private CancellationTokenSource _cts = new CancellationTokenSource();

        //AutoBaseMonitorView 리스트 전역 선언
        private AutoBaseMonitorView[] _AbmvList = seongsiksUtils.createAllAutoBaseMonitorViews();

        //ip랑 view랑 매핑
        private Dictionary<string, AutoBaseMonitorView> _viewMap = new Dictionary<string, AutoBaseMonitorView>();

        //config 파일 데이터 전역 선언
        private Config config = seongsiksUtils.getConfigData();

        private void button1_Click(object sender, EventArgs e)
        {
            //view 만들기 테스트
            AutoBaseMonitorView view = new AutoBaseMonitorView();
            int width = (flowLayoutPanel1.ClientSize.Width - 40) / 3;
            view.Size = new Size(441, 299);
            //view.SetInformation(config.Ip[0], 104, true);
            flowLayoutPanel1.Controls.Add(view);
        }

        //데이터 가져오기
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //textBox3.AppendText(seongsiksUtils.getDataFromAICPL8(comboBox1.Text) + "\r\n");
                string msg = seongsiksUtils.FunctionForThread("10.8.38.236");
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            foreach (string ip in config.Ip)
            {
                comboBox1.Items.Add(ip);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        //START 버튼

        private void button3_Click(object sender, EventArgs e)
        {
            TcpClient client = null;
            //Todo: 이 부분도 분리 가능한지 구상 필요
            for (int i = 0; i < _AbmvList.Length; i++)
            {
                AutoBaseMonitorView view = _AbmvList[i];
                int width = (flowLayoutPanel1.ClientSize.Width - 40) / 3;
                view.Size = new Size(width, 299);
                _viewMap.Add(config.Ip[i], view);
                flowLayoutPanel1.Controls.Add(view);
            }
        }

        //STOP 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            //정지 기능 구현 필요
            _cts.Cancel();
        }

        //CLEAR 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            //모든 view에서 Clear()
        }
    }
}