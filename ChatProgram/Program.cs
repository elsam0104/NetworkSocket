using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

namespace ChatProgram
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

                    clientList.Add(counter, client);

                    client.startClient(clientSocket, clientList, counter);
                }
                clientSocket.Close();
                serverSocket.Stop();
                Console.WriteLine("Exit..");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static TcpClient GetSocket(int id)
        {
            TcpClient socket = null;
            if (clientList.ContainsKey(id))
            {
                HandleClient hc = clientList[id] as HandleClient;
                socket = hc.clientSocket;
            }
            return socket;
        }

        public static void Broadcast(string msg, string uName, bool isClient, bool isHide = false, object userId = null)
        {
            mutex.WaitOne();
            byte[] broadcastBytes = null;

            if (isClient == true)
            {
                broadcastBytes = Encoding.UTF8.GetBytes(uName + "$ " + msg);
            }
            else if (isHide)
            {
                broadcastBytes = Encoding.UTF8.GetBytes(uName + "님의 귓속말 >> " + msg);
            }
            else
            {
                broadcastBytes = Encoding.UTF8.GetBytes("[[공지사항 안내]]  " + msg);
            }

            TcpClient broadcastSocket;

            if (isHide)
            {
                foreach (DictionaryEntry Item in clientList)
                {
                    if (Item.Key == userId)
                    {
                        HandleClient hc = Item.Value as HandleClient;
                        broadcastSocket = hc.clientSocket;

                        NetworkStream broadcastStream = broadcastSocket.GetStream();

                        broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                        broadcastStream.Flush();
                    }
                }
                mutex.ReleaseMutex();
            }
            else
            {
                foreach (DictionaryEntry Item in clientList)
                {
                    HandleClient hc = Item.Value as HandleClient;
                    broadcastSocket = hc.clientSocket;

                    NetworkStream broadcastStream = broadcastSocket.GetStream();

                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                    broadcastStream.Flush();
                }
                mutex.ReleaseMutex();
            }
        }

        public static void UserAdd(string clientNo)
        {
            Broadcast(clientNo + "님이 접속하셨습니다. 미풍양속을 헤치는 발언은 재제대상이 됩니다.", "", false);
            Console.WriteLine(clientNo + "님이 접속하셨습니다.");
        }

        public static void UserLeft(int userId, string clientId)
        {
            if (clientList.Contains(userId))
            {
                Broadcast(clientId + "$#Left#", clientId, false);
                Console.WriteLine("Client Left:" + clientId);

                TcpClient clientSocket = GetSocket(userId);

                clientList.Remove(userId);
                clientSocket.Close();
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
            bool part2 = (s.Available == 0);
            if (part1 && part2)
            {
                return false; // 연결 실패
            }
            return true; //연결 성공
        }
        private string Cut(string dataFromClient)
        {
            //재제
            dataFromClient = dataFromClient.Replace("서린", "위대하신 서린 선생님");
            dataFromClient = dataFromClient.Replace("서린쌤", "위대하신 서린 선생님");
            dataFromClient = dataFromClient.Replace("네트워크쌤", "위대하신 서린 선생님");
            dataFromClient = dataFromClient.Replace("ㅅㄹㅆ", "위대하신 서린 선생님");
            dataFromClient = dataFromClient.Replace("ㅅㄹ", "위대하신 서린 선생님");
            dataFromClient = dataFromClient.Replace("ㄴㅌㅇㅋㅆ", "위대하신 서린 선생님");

            dataFromClient = dataFromClient.Replace("시발", "정말");
            dataFromClient = dataFromClient.Replace("ㅅㅂ", "이런");
            dataFromClient = dataFromClient.Replace("병신", "나쁜 아이");
            dataFromClient = dataFromClient.Replace("ㅄ", "나쁜 아이");
            dataFromClient = dataFromClient.Replace("ㅂㅅ", "나쁜 아이");
            return dataFromClient;
        }
        private void doChat()
        {
            byte[] bytesForm = new byte[1024];
            string dataFromClient = "";
            NetworkStream networkStream = clientSocket.GetStream();

            while (!noConnection)
            {
                try
                {
                    int numBytesRead;
                    if (!socketConnected(clientSocket.Client))
                        noConnection = true;
                    else
                    {
                        if (networkStream.DataAvailable)
                        {
                            dataFromClient = "";
                            while (networkStream.DataAvailable)
                            {
                                numBytesRead = networkStream.Read(bytesForm, 0, bytesForm.Length);
                                dataFromClient = Encoding.UTF8.GetString(bytesForm, 0, numBytesRead);
                            }
                            dataFromClient = Cut(dataFromClient);
                            int idx = dataFromClient.IndexOf("$");
                            
                            if (ClientId == null && idx > 0)
                            {
                                ClientId = dataFromClient.Substring(0, idx);
                                foreach(DictionaryEntry item in clientList)
                                {
                                    if (ClientId == item.Key as string)
                                    {
                                        noConnection = true;
                                        Program.Broadcast("중복 아이디입니다.", ClientId, true, true,ClientId);
                                        continue;
                                    }
                                }
                                Program.UserAdd(ClientId);
                            }
                            else if (idx > 1)
                            {
                                dataFromClient = dataFromClient.Substring(0, dataFromClient.Length - 1);
                                Console.WriteLine("From Client - " + ClientId + " : " + dataFromClient);
                                if (dataFromClient[0] == '쉿')
                                {
                                    object key = null;
                                    foreach (DictionaryEntry Item in clientList)
                                    {
                                        if(dataFromClient.Contains(Item.Key.ToString()))
                                        {
                                            key = Item.Key;
                                        }
                                    }
                                        Program.Broadcast(dataFromClient, ClientId, true, true,key);
                                }
                                else
                                    Program.Broadcast(dataFromClient, ClientId, true);
                            }
                            else
                            {
                                dataFromClient = "";
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    noConnection = true;
                    Console.WriteLine("Error :" + e.ToString());
                }
            }
            Program.UserLeft(userID, ClientId);
        }
    }
}
