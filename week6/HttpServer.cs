using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using HttpServer.Attributes;

namespace HttpServer
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
                Listening();
            }
            else
            {
                Console.WriteLine("Сервер уже запущен!");
            }
        }

        private async void Listening()
        {
            while (_listener.IsListening)
            {
                var _context = await _listener.GetContextAsync();
                if (!MethodHandler(_context)) FileHandler(_context);
            }
        }

        private void FileHandler(HttpListenerContext _context)
        {
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
        
        private bool MethodHandler(HttpListenerContext _httpContext)
        {
            // объект запроса
            HttpListenerRequest request = _httpContext.Request;

            // объект ответа
            HttpListenerResponse response = _httpContext.Response;

            if (_httpContext.Request.Url.Segments.Length < 2) return false;

            string controllerName = _httpContext.Request.Url.Segments[1].Replace("/", "");

            string[] strParams = _httpContext.Request.Url
                                    .Segments
                                    .Skip(2)
                                    .Select(s => s.Replace("/", ""))
                                    .ToArray();

            if (strParams.Length == 0 && request.HttpMethod == "POST")
            {
                Stream body = request.InputStream;
                Encoding encoding = request.ContentEncoding;
                StreamReader reader = new StreamReader(body, encoding);
                Console.WriteLine("получен post запрос, content length: {0}", request.ContentLength64);
                
                string s = reader.ReadToEnd();
                var paramList = new List<string>();
                foreach (string a in s.Split('&'))
                {
                    paramList.Add(a.Split('=')[1]);
                }

                strParams = paramList.ToArray();
                body.Close();
                reader.Close();
            }

            var assembly = Assembly.GetExecutingAssembly();

            var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpController))).FirstOrDefault(c => c.Name.ToLower() == controllerName.ToLower());

            if (controller == null) return false;

            var test = typeof(HttpController).Name;
            var method = controller.GetMethods()
                .FirstOrDefault(t => t.GetCustomAttributes(true)
                    .Any(attr => attr.GetType().Name == $"Http{_httpContext.Request.HttpMethod}") && t.GetParameters().Length == strParams.Length);

            if (method == null) return false;
            
            object? ret;

            if (strParams.Length == method.GetParameters().Length)
            {
                List<object> queryParams = new List<object>();
                bool BadRequest = false;
                try
                {
                    queryParams = method.GetParameters()
                        .Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
                        .ToList();
                }
                catch (FormatException)
                {
                    BadRequest = true;
                }

                if (!BadRequest) ret = method.Invoke(Activator.CreateInstance(controller), queryParams.ToArray());
                else
                {
                    ret = new string("Bad arguments!");
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                }

            }
            else
            {
                ret = new string("Argument count mismatch!");
                response.StatusCode = (int) HttpStatusCode.BadRequest;
            }

            if (ret != null && ret.ToString() == "Steam_redirect")
            {
                using (response)
                {
                    response.StatusCode = (int) HttpStatusCode.Redirect;
                    response.Headers.Set("Location","http://store.steampowered.com/"); 
                
                    return true;
                }
            }
            
            response.ContentType = "Application/json";

            byte[] buffer = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(ret));
            response.ContentLength64 = buffer.Length;

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
            
            return true;
        }
        
        private string GetExtension(string url)
        {
            string ext = Path.GetExtension(url);
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