using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.text;
using System.Collections;

namespace ConsoleApplication
{
    class Program
    {
        public static Hashtable clientList = new Hashtable();
        private static int userCount = 0;
        private static Mutex mutex = new Mutex();

        static void Main(string[] args)
        {
            try
            {
                TcpListener serverSocket = new TcpListener(IPAddress.Any, 8888); //포트번호는 암거나
                TcpClient clientSocket = default(TcpClient);
                int counter = 0;
                byte[] bytesFrom = new byte[1024];
                string dataFormClient = "";

                serverSocket.Start();   //listen
                Console.WriteLine("C# Server Started...");
                counter = 0;
                while (true)
                {
                    counter++;
                    clientSocket = serverSocket.AcceptTcpClient();
                    counter = userCount++;

                    HandleClient client = new HandleClient();
                    clientList.Add(counter, clientSocket);

                    client.startClient(clientSocket, clientList, counter);
                }
                clientSocket.Close();
                serverSocket.Close();
                Console.WriteLine("Exit..");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class HandleClient
    {
        const string COMMAND_ENTER = "#ENTER#";
        const string COMMAND_HISTORY = "#HISTORY#";

        public TcpClient clientSocket;
        public int userID;
        public string ClientId;

        private Hashtable clientList;
        private bool noConnection = false;

        public void startClient(TcpClient inClientSocket, Hashtable cList, int userSerial)
        {
            userID = userSerial;
            clientSocket = inClientSocket;
            clientList = cList;

            Thread ctTread = new Thread(doChat);
            ctTread.Start();
        }

        bool socketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = s.Poll(s.Available == 0);
            if (part1 && part2)
            {
                return false; // 연결 실패
            }
            return true; //연결 성공
        }
    }
}
