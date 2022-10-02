using System.Net;
using System;
using System.IO;
using System.Threading;

namespace NetConsoleApp
{
    public class HttpServer : IDisposable
    {
        private readonly int _port;
        private readonly HttpListener _listener;
        private bool isRunning;

        public HttpServer(int port)
        {
            _port = port;
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{_port}/google/");
        }

        public void Start()
        {
            if (!isRunning)
            {
                Console.WriteLine("Идет запуск сервера...");
                _listener.Start();
                isRunning = true;
                Console.WriteLine($"Сервер запущен на порту {_port}"); 
                BeginListening();
            }
            else
            {
                Console.WriteLine("Сервер уже запущен!");
            }
        }

        private void BeginListening()
        {
            if (_listener.IsListening)
            {
                var _context = _listener.GetContext();
                HttpListenerRequest request = _context.Request;
                HttpListenerResponse response = _context.Response;

                byte[] buffer;

                if (File.Exists("google.html"))
                {
                    buffer = File.ReadAllBytes("google.html");
                }
                else
                {
                    var responseString = "Error 404 - File Not Found";
                    buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                }

                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                
                BeginListening();
            }
        }

        public void Restart()
        {
            Console.WriteLine("Перезапуск сервера..");
            _listener.Stop();
            isRunning = false;
            _listener.Start();
            Console.WriteLine("Сервер запущен");
            BeginListening();
        }

        public void Stop()
        {
            Console.WriteLine("Остановка сервера..");
            _listener.Stop();
            isRunning = false;
            Console.WriteLine("Сервер остановлен");
        }

        public void Dispose()
        {
            _listener.Close();
        }
    }
}