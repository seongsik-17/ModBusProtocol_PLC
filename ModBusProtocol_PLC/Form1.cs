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

        //private string ip = comboBox1.Text;
        //private int port = seongsiksUtils.getConfigData().Port;

        //Task 제어
        private Dictionary<string, CancellationTokenSource> _runningTasks = new Dictionary<string, CancellationTokenSource>();

        //private CancellationTokenSource _cts = new CancellationTokenSource();

        //모니터링 중인 IP 리스트
        private List<string> monitoringIPs = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //TcpClient client = new TcpClient(ip, port);
                //string currentDir = System.IO.Directory.GetCurrentDirectory();
                //textBox3.Text = "연결성공" + currentDir;
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

        //START 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            string ip = comboBox1.Text;
            int port = seongsiksUtils.getConfigData().Port;
            if (monitoringIPs.Contains(ip))
            {
                MessageBox.Show("이미 모니터링 중인 IP입니다.");
                return;
            }
            //모니터링 중인 IP 리스트에 추가
            if (!monitoringIPs.Contains(ip))
            {
                monitoringIPs.Add(ip);
            }
            if (_runningTasks.ContainsKey(ip))
            {
                MessageBox.Show("이미 모니터링 중인 IP입니다.");
                return;
            }
            //cts 생성
            CancellationTokenSource _cts = new CancellationTokenSource();
            //모니터링 중인 IP 리스트에 추가(cts 관리용)
            _runningTasks.Add(ip, _cts);

            //모니터링 주기 설정 (ms)
            int setTime = 2000;
            //새탭 생성
            TextBox logMessage = seongsiksUtils.createNewTab(ip, tabControl1);

            Task.Run(async () =>
            {
                TcpClient client = null;
                NetworkStream stream = null;
                int cntError = 0;

                while (!_cts.IsCancellationRequested)
                {
                    client = new TcpClient();
                    var connectTask = client.ConnectAsync(ip, port);
					if (await Task.WhenAny(connectTask, Task.Delay(3000)) != connectTask)
                    {
                        throw new Exception("연결 시간 초과");
					}
                    stream = client.GetStream();
                    string updateString = null;

                    try
                    {
                        updateString = seongsiksUtils.FunctionFotThread(client, stream);
                        //오류 카운터 갱신
                        cntError = 0;

                        //탭 데이터 갱신
                        logMessage.Invoke(new Action(() =>
                        {
                            string curTime = DateTime.Now.ToString("HH:mm:ss");
                            logMessage.AppendText($"[{curTime}] {ip} " + updateString + "\r\n");
                        }));

                        //텍스트박스 갱신
                        //textBox3.Invoke(new Action(() =>
                        //{
                        //    string curTime = DateTime.Now.ToString("HH:mm:ss");
                        //    textBox3.AppendText($"[{curTime}] {ip} " + updateString + "\r\n");
                        //}));

                        await Task.Delay(setTime, _cts.Token);
                    }
                    catch (Exception ex)
                    {
                        //Todo: TCP연결이 실패했을 경우 발생하는 이벤트 작성 필요(로그 생성 및 DB 삽입)
                        if (_cts == null || _cts.IsCancellationRequested)
                        {
                            //_cts가 취소된 경우 루프 종료
                            return;
                        }

                        //오류 횟수를 카운트 하고 10초 간격으로 5회 이상 재 시도에도 연결 실패시 fail로 간주
                        if (cntError > 2)
                        {
                            //오류가 3회 이상 발생한 케이스
                            MessageBox.Show(ex.Message);
                            return;
                        }
                        cntError++;
                        await Task.Delay(1000);
                        continue;
                    }
                    finally
                    {
                        stream?.Close();
                        client?.Close();
                    }
                }
            });
        }

        //STOP 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            monitoringIPs.Remove(comboBox1.Text);
            if (_runningTasks.ContainsKey(comboBox1.Text))
            {
                //취소 신호 보내기
                _runningTasks[comboBox1.Text].Cancel();
                _runningTasks.Remove(comboBox1.Text);
            }
            else
            {
                MessageBox.Show("모니터링 중인 IP가 아닙니다.");
            }
        }

        //CLEAR 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            TextBox currentTabTextBox = tabControl1.SelectedTab.Controls[0] as TextBox;
            currentTabTextBox.Clear();
        }
    }
}