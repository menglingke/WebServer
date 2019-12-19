using MengLingke_006.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MengLingke_006
{
    public partial class Form1 : Form
    {
        private Socket socketWatch = null;
        private Thread threadWatch = null;
        private bool isEndService = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            socketWatch = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            socketWatch.Bind(new IPEndPoint(IPAddress.Parse(textBox1.Text),int.Parse(textBox2.Text)));
            socketWatch.Listen(10);
            threadWatch = new Thread(ListenClientConnect);
            threadWatch.IsBackground = true;
            threadWatch.Start(socketWatch);
            isEndService = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            btnStart.Enabled = false;
            ShowMessage("您已成功启动Web服务！");
        }

        private void ShowMessage(string msg)
        {
            if (txtStatus.InvokeRequired)
            {
                txtStatus.BeginInvoke(new Action<string>((str) =>
                {
                    txtStatus.AppendText(str + Environment.NewLine);
                }), msg);
            }
            else
            {
                txtStatus.AppendText(msg+Environment.NewLine);
            }
        }

        private void ListenClientConnect(object obj)
        {
            Socket socketListen = obj as Socket;
            while (!isEndService) 
            {
                Socket proxSocket = socketListen.Accept();
                byte[] data = new byte[1024 * 1024 * 2];
                int length = proxSocket.Receive(data,0,data.Length,SocketFlags.None);
                string requestText = Encoding.Default.GetString(data,0,length);
                HttpContext context = new HttpContext(requestText);
                HttpApplication application = new HttpApplication();
                application.ProcessRequest(context);
                ShowMessage(string.Format("{0} {1} from {2}", context.Request.HttpMethod, context.Request.Url, proxSocket.RemoteEndPoint.ToString()));
                proxSocket.Send(context.Response.GetResponseHeader());
                proxSocket.Send(context.Response.Body);
                proxSocket.Shutdown(SocketShutdown.Both);
                proxSocket.Close();

            }
        }
    }
}
