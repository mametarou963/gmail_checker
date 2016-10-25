using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day9_gmail_checker
{
    public partial class Form1 : Form
    {
        private int count;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 初期化
            OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
            // Gmail接続
            client.Connect("pop.gmail.com", 995, true);
            // ログイン
            client.Authenticate(textBox1.Text, textBox2.Text);

            int messageCount = client.GetMessageCount();
            if(count == -1)
            {
                count = messageCount;
            }

            for(int i = messageCount;i > count; i--)
            {
                textBox3.Text += client.GetMessage(i).Headers.Subject + "\r\n";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "メールが届きました";
                notifyIcon1.BalloonTipText = client.GetMessage(i).Headers.Subject;
                notifyIcon1.ShowBalloonTip(3000);
            }

            count = messageCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
