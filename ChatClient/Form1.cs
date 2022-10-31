using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        bool stopRunning = false;

        private void msg() //텍스트 박스에 메시지 출력
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(msg));
            }
            else
                textBox2.Text = textBox2.Text + Environment.NewLine + " >> " + readData;
        }

        private void getMessage()
        {
            byte[] inStream = new byte[1024];
            string returnData = "";

            try
            {
                while(!stopRunning)
                {
                    serverStream = clientSocket.GetStream();
                    int buffsize = 0;
                    buffsize = clientSocket.ReceiveBufferSize;
                    int numBytesRead;

                    if(serverStream.DataAvailable)
                    {
                        returnData = "";
                        while(serverStream.DataAvailable)
                        {
                            numBytesRead = serverStream.Read(inStream, 0, inStream.Length);
                            returnData += Encoding.UTF8.GetString(inStream, 0, numBytesRead);
                        }
                        readData = returnData;
                        msg();
                    }
                }
            }
            catch(Exception ex)//서버연결 끊김
            {
                stopRunning = true;
            }
        }
        
        public Form1()
        {
            InitializeComponent();
            //this.FormClosed += Form1_FormClosed;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            readData = "Connected to Chat Server...";
            msg();
            clientSocket.Connect("localhost", 8888);
            serverStream = clientSocket.GetStream();

            byte[] outStream = Encoding.UTF8.GetBytes(textBox1.Text + '$');
            serverStream.Write(outStream, 0, outStream.Length); //send
            serverStream.Flush();

            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            byte[] outStream = Encoding.UTF8.GetBytes(textBox3.Text + '$');
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopRunning = true;
            serverStream.Close();
            clientSocket.Close();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void LogOutButton_Click(object sender, EventArgs e) //유저 로그아웃
        {
            stopRunning = true;
            serverStream.Close();
            clientSocket.Close();
        }


        private void ReportBtn_Click(object sender, EventArgs e)
        {
            byte[] outStream = Encoding.UTF8.GetBytes(ReportText.Text);

            textBox2.Text = textBox2.Text + Environment.NewLine +  outStream + "님을 신고 신청 하였습니다."; //그러나 여기까지밖에 만들지 못하였다....

            ReportText.ForeColor = Color.Gray;
            ReportText.Text = "Plz enter user's name...";
        }

        private void ReportText_TextChanged(object sender, EventArgs e)
        {
            ReportText.ForeColor = Color.Black;
        }

        private void ConsentButton_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
