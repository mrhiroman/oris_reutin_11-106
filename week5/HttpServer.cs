using System.Net;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Text.Json;

namespace NetConsoleApp
{
    public class HttpServer : IDisposable
    {
        private ServerSetting _setting;
        private readonly HttpListener _listener;
        private bool isRunning;

        public HttpServer()
        {
            _listener = new HttpListener();
        }

        public void Start()
        {
            if (!isRunning)
            {
                _setting = JsonSerializer.Deserialize<ServerSetting>(File.ReadAllBytes("./settings.json"));
                _listener.Prefixes.Clear();
                _listener.Prefixes.Add($"http://localhost:{_setting.Port}/");
                Console.WriteLine("Идет запуск сервера...");
                _listener.Start();
                isRunning = true;
                Console.WriteLine($"Сервер запущен на порту {_setting.Port}"); 
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

                if (Directory.Exists(_setting.Site))
                {
                    string fileUrl = _context.Request.RawUrl.Replace("%20", " ");
                    buffer = GetFile(fileUrl);

                    if (buffer == null)
                    {
                        response.Headers.Set("Content-Type", "text/plain");
                        response.StatusCode = (int) HttpStatusCode.NotFound;

                        string err = "404 - Not Found";
                        buffer = Encoding.UTF8.GetBytes(err);
                    }
                    else
                    {
                        response.Headers.Set("Content-Type", GetExtension(fileUrl));
                    }
                }
                else
                {
                    response.Headers.Set("Content-Type", "text/plain");
                    response.StatusCode = (int) HttpStatusCode.NotFound;

                    string err = $"404 - No Directory {_setting.Site}";
                    buffer = Encoding.UTF8.GetBytes(err);
                }

                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                
                BeginListening();
            }
        }

        private byte[] GetFile(string url)
        {
            byte[] buffer = null;
            var filePath = _setting.Site + url;
            if (Directory.Exists(filePath))
            {
                filePath = filePath + "index.html";
                if (File.Exists(filePath))
                {
                    buffer = File.ReadAllBytes(filePath);
                }
            }
            else if (File.Exists(filePath))
            {
                buffer = File.ReadAllBytes(filePath);
            }

            return buffer;
        }
        
        private string GetExtension(string url)
        {
            string ext = System.IO.Path.GetExtension(url);
            if (Directory.Exists(_setting.Site + url)) ext = ".html";
            switch (ext)
            {
                case ".html":
                    return "text/html";
                case ".css":
                    return "text/css";
                case ".js":
                    return "text/javascript";
                case ".png":
                    return "image/png";
                case ".svg":
                    return "image/svg+xml";
            }
            return "text/plain";
        }
        

        public void Restart()
        {
            Console.WriteLine("Перезапуск сервера..");
            Stop();
            Start();
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