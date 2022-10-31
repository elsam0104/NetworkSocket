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
        public static Hashtable clientsList = new Hashtable();
        private static int userCnt = 0;
        private static Mutex mut = new Mutex();

        static void Main()
        {
            try
            {
                TcpListener serverSocket = new TcpListener(IPAddress.Any, 3434);
                TcpClient clientSocket = default(TcpClient);
                int counter = 0;
                byte[] bytesFrom = new byte[1024];
                string dataFromClient = "";

                serverSocket.Start(); //listen
                Console.WriteLine("C# Server Started...");

                while (true)
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    counter = userCnt;
                    userCnt++;

                    handleClient client = new handleClient();
                    clientsList.Add(counter, client);

                    client.startClient(clientSocket, clientsList, counter);
                }
                clientSocket.Close();
                serverSocket.Stop();
                Console.WriteLine("Exit");
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
            if (clientsList.ContainsKey(id))
            {
                handleClient hc = (handleClient)clientsList[id];
                socket = hc.clientSocket;
            }
            return socket;
        }
        public static void broadcast(string msg, string uName, bool flag)
        {
            mut.WaitOne();
            Byte[] broadcastBytes = null;

            if (flag == true) //클라이언트
            {
                broadcastBytes = Encoding.UTF8.GetBytes(uName + "$" + msg);
            }
            else //서버
            {
                broadcastBytes = Encoding.UTF8.GetBytes(msg);
            }

            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                handleClient hc = (handleClient)Item.Value;
                broadcastSocket = hc.clientSocket;

                NetworkStream broadcastStream = broadcastSocket.GetStream();
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
            mut.ReleaseMutex();
        }

        public static void UserAdd(string clientNo)
        {
            broadcast(clientNo + "Joined", "", false);
            Console.WriteLine(clientNo + "Joined chat room");
        }
        public static void UserLeft(int userID, string clientNo)
        {
            if (clientsList.ContainsKey(userID))
            {
                broadcast(clientNo + "Left", "", false);
                Console.WriteLine("Client Left: " + clientNo);

                TcpClient clientSocket = GetSocket(userID);

                clientsList.Remove(userID);
                clientSocket.Close();
            }
        }
    }
    public class handleClient
    {
        const string COMMAND_ENTER = "#ENTER#";
        const string COMMAND_HISTORY = "#HISTORY#";

        public TcpClient clientSocket;
        public int userID;
        public string clientID;

        private Hashtable clientsList;
        private bool noConnection = false;

        public void startClient(TcpClient inClientSocket,
            Hashtable cList, int userSerial)
        {
            clientSocket = inClientSocket;
            userID = userSerial;
            clientsList = cList;

            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        bool SocketConnected(Socket e)
        {
            bool part1 = e.Poll(1000, SelectMode.SelectRead);
            bool part2 = (e.Available == 0);
            if (part1 && part2)
            {
                return false;
            }
            else return true;
        }

        private void doChat()
        {
            byte[] bytesFrom = new byte[1024];
            string dataFromClient = "";
            NetworkStream networkStream = clientSocket.GetStream();

            while (!noConnection)
            {
                try
                {
                    int numBytesRead;
                    if (!SocketConnected(clientSocket.Client))
                    {
                        noConnection = true;
                    }
                    else
                    {
                        if (networkStream.DataAvailable)
                        {
                            dataFromClient = "";
                            while (networkStream.DataAvailable)
                            {
                                numBytesRead = networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                                dataFromClient = Encoding.UTF8.GetString(bytesFrom, 0, numBytesRead);

                                int idx = dataFromClient.IndexOf('$');

                                if (clientID == null && idx > 0)
                                {
                                    clientID = dataFromClient.Substring(0, idx);
                                    Program.UserAdd(clientID);
                                }
                                else if (idx > 0)
                                {
                                    dataFromClient = dataFromClient.Substring(0, dataFromClient.Length - 1);
                                    Console.WriteLine("From client - " + clientID + ": " + dataFromClient);
                                    Program.broadcast(dataFromClient, clientID, true);
                                }
                                else
                                {
                                    dataFromClient = "";
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    noConnection = true;
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
            Program.UserLeft(userID, clientID);
        }


    }
}