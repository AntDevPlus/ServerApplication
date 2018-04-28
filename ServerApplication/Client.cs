using System;
using System.Net.Sockets;

namespace ServerApplication
{
    class Client
    {
        public int Index;
        public string IP;
        public TcpClient Socket;
        public NetworkStream myStream;
        private byte[] readBuff;
        public void Start()
        {
            Socket.SendBufferSize = 4096;
            Socket.ReceiveBufferSize = 4096;
            myStream = Socket.GetStream();
            Array.Resize(ref readBuff, Socket.ReceiveBufferSize);
            myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceivedData, null);
        }

        public void CloseConnection()
        {
            Socket.Close();
            Socket = null;
            Console.WriteLine("Player disconnected: " + IP);  

        }

        void OnReceivedData(IAsyncResult result)
        {
            try
            {
                int ReadBytes = myStream.EndRead(result);
                if(Socket == null)
                {
                    return;
                }
                if(ReadBytes <= 0)
                {
                    CloseConnection();
                    return;
                }

                byte[] newBytes = null;
                Array.Resize(ref newBytes, ReadBytes);
                Buffer.BlockCopy(readBuff, 0, newBytes, 0, ReadBytes);

                //HangleData

                if(Socket == null)
                {
                    return;                
                }

                myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, OnReceivedData, null);
            }
            catch(Exception ex)
            {
                CloseConnection();
                return;
            }
        }
    }
}
