using System;
using System.Net;
using System.Net.Sockets;
using ServerApplication;

namespace ServerApplication
{
    class Network
    {
        public TcpListener ServerSocket;
        public static Network instance = new Network();
        public static Client[] Clients = new Client[100];

        public void ServerStart()
        {
            for(int i = 1; i < 100; i++)
            {
                Clients[i] = new Client();
            }

            ServerSocket = new TcpListener(IPAddress.Any, 2409);
            ServerSocket.Start();
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[INFO] Le serveur est en ligne");
            Console.ForegroundColor = ConsoleColor.White;
        }

        void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = ServerSocket.EndAcceptTcpClient(result);
            client.NoDelay = false;
            ServerSocket.BeginAcceptTcpClient(OnClientConnect, null);

            for (int i = 1; i < 100; i++)
            {
                if (Clients[i].Index == 0)
                {
                    Clients[i].Socket = client;
                    Clients[i].Index = i;
                    Clients[i].IP = client.Client.RemoteEndPoint.ToString();
                    Clients[i].Start();
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Incoming Connection from " + Clients[i].IP + " || Index: " + i);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    //SendWelcomeMessages
                    return;
                }
            }
        }
    }
}
