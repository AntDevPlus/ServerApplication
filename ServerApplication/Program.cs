using System;
using System.Threading; 

namespace ServerApplication
{
    class Program
    {
        private static Thread threadConsole;
        private static bool ConsoleRunning;

        static void Main(string[] args)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();
            Network.instance.ServerStart();
        }

        private static void ConsoleThread()
        {
            string line;
            ConsoleRunning = true;

            while(ConsoleRunning)
            {
                line = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(line))
                {
                    ConsoleRunning = false;
                    return;
                } else {
                    if(line == "/stop")
                    {
                        ConsoleRunning = false;
                    }
                    else if(line.StartsWith("kick"))
                    {

                    }
                }
            }
        }
    }
}
