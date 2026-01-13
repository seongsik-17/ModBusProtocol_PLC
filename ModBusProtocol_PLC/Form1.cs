using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ModBusProtocol_PLC
{
    public partial class Form1 : Form
    {
        private List<TestAgent> agents = new List<TestAgent>();

        public void frmInit()
        {
            for (int i = 0; i < config.Ip.Count; i++)
            {
                TestAgent agent = new TestAgent(config.Ip[i], config.Port);
                agent.DataReceived += Agent_DataReceived;
                agents.Add(agent);
                agent.Start();
            }
            //TestAgent agent = new TestAgent("10.8.38.236", 13890);
            //ReceivedDataDto data = new ReceivedDataDto();

            //agent.DataReceived += Agent_DataReceived;
            //agents.Add(agent);
            //agent.Start();
        }

        private void Agent_DataReceived(object? sender, (string ip, int count, bool runstop) e)
        {
            string ip = e.ip;
            int count = e.count;
            bool runstop = e.runstop;
            this.Invoke(new Action(() =>
            {
                if (_viewMap.ContainsKey(ip))
                {
                    _viewMap[ip].SetInformation(count, runstop);
					//AutoBaseMonitorView view = 
                    

				}
            }));
        }

        //처음 로딩 시 Config 파일에 있는 모든 항목 View 생성하기
        public static AutoBaseMonitorView[] createAllAutoBaseMonitorViews()
        {
            //config 데이터에서 ip 리스트 가져오기
            List<string> ipList = new List<string>();
            foreach (string ip in config.Ip)
            {
                ipList.Add(ip);
            }
            AutoBaseMonitorView[] monitorViews = new AutoBaseMonitorView[ipList.Count];
            //Views 생성
            for (int i = 0; i < ipList.Count; i++)
            {
                AutoBaseMonitorView view = new AutoBaseMonitorView(ipList[i]);
                view.Size = new Size(441, 299);
                _viewMap.Add(ipList[i], view);
                monitorViews[i] = view;
                view.SetInformation(ipList[i]);
            }
            return monitorViews;
        }

        public Form1()
        {
            InitializeComponent();
        }

        //CancellationTokenSource
        private CancellationTokenSource _cts = new CancellationTokenSource();

        //AutoBaseMonitorView 리스트 전역 선언
        private AutoBaseMonitorView[] _AbmvList = createAllAutoBaseMonitorViews();

        //ip랑 view랑 매핑
        private static Dictionary<string, AutoBaseMonitorView> _viewMap = new Dictionary<string, AutoBaseMonitorView>();

        //config 파일 데이터 전역 선언
        private static Config config = seongsiksUtils.getConfigData();

        private void button1_Click(object sender, EventArgs e)
        {
            ////view 만들기 테스트
            //AutoBaseMonitorView view = new AutoBaseMonitorView();
            //int width = (flowLayoutPanel1.ClientSize.Width - 40) / 3;
            //view.Size = new Size(441, 299);
            ////view.SetInformation(config.Ip[0], 104, true);
            //flowLayoutPanel1.Controls.Add(view);
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

            for (int i = 0; i < _AbmvList.Length; i++)
            {
                AutoBaseMonitorView view = _AbmvList[i];
                int width = (flowLayoutPanel1.ClientSize.Width - 40) / 3;
                view.Size = new Size(width, 299);
                flowLayoutPanel1.Controls.Add(view);
            }
        }

        //START 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            frmInit();
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