using System;
using System.Net;
using System.IO;
using System.Threading;

namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer();
            Thread s = new Thread(new ThreadStart(server.Start));
            s.Start();
            Run();
            
            void Run()
            {
                Thread s = new Thread(new ThreadStart(server.Start));
                Thread rs = new Thread(new ThreadStart(server.Restart));
                switch (Console.ReadLine())
                {
                    case "start":
                        s.Start();
                        break;
                    case "restart":
                        rs.Start();
                        break;
                    case "stop":
                        server.Stop();
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
                Run();
            }
        }
    }
}