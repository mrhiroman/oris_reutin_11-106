using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using HttpServer.Attributes;
using HttpServer.Models;
using Scriban;

namespace HttpServer
{
    public class HttpServer : IDisposable
    {
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
                var setting = JsonSerializer.Deserialize<ServerSetting>(File.ReadAllBytes("./settings.json"));
                StaticSetting.Port = setting.Port;
                StaticSetting.Site = setting.Site;
                StaticSetting.ConnectionString = setting.ConnectionString;
                StaticSetting.TemplateFolder = setting.TemplateFolder;
                _listener.Prefixes.Clear();
                _listener.Prefixes.Add($"http://localhost:{StaticSetting.Port}/");
                Console.WriteLine("Идет запуск сервера...");
                _listener.Start();
                isRunning = true;
                Console.WriteLine($"Сервер запущен на порту {StaticSetting.Port}"); 
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

            if (Directory.Exists(StaticSetting.Site))
            {
                string fileUrl = _context.Request.RawUrl.Replace("%20", " ");
                buffer = GetFile(fileUrl);

                if (buffer == null)
                {
                    response.Headers.Set("Content-Type", "text/html");
                    response.StatusCode = (int) HttpStatusCode.NotFound;

                    buffer = GetFile("/404.html");
                }
                else
                {
                    response.Headers.Set("Content-Type", GetExtension(fileUrl));
                }
            }
            else
            {
                response.Headers.Set("Content-Type", "text/html");
                response.StatusCode = (int) HttpStatusCode.NotFound;

                buffer = GetFile("/404.html");
            }

            response.ContentLength64 = buffer.Length;

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private byte[] GetFile(string url)
        {
            byte[] buffer = null;
            var filePath = StaticSetting.Site + url;
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

            byte[] buffer;
            Stream output;

            if (_httpContext.Request.Url.Segments.Length < 2) return false;

            string controllerName = _httpContext.Request.Url.Segments[1].Replace("/", "");

            string[] strParams = _httpContext.Request.Url
                                    .Segments
                                    .Skip(2)
                                    .Select(s => s.Replace("/", ""))
                                    .ToArray();
            
            var controllerInfo = strParams.ToList().ToArray();

            if (strParams.Length < 2 && request.HttpMethod == "POST")
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
            var method = controller.GetMethods()
                .FirstOrDefault(t => t.GetCustomAttributes(true)
                    .Any(attr => (attr.GetType().Name == $"Http{_httpContext.Request.HttpMethod}") && 
                                     ((t.GetParameters().Length - 1 == strParams.Length && request.HttpMethod == "POST") || (t.GetParameters().Length - 1 == strParams.Length - 1 && request.HttpMethod == "GET") || t.GetParameters().Length == 1 && request.HttpMethod == "GET") 
                                     && (controllerInfo.Length == 0 || (attr as HttpGET)?.MethodURI == controllerInfo[0] || t.Name.ToLower() == controllerInfo[^1] || controllerInfo[^1] == controllerName || request.HttpMethod == "POST")));

            if (method == null) return false;
            
            object? ret;

            if ((strParams.Length == method.GetParameters().Length - 1 && request.HttpMethod == "POST")  ||
                (strParams.Length - 1 == method.GetParameters().Length - 1 && request.HttpMethod == "GET") || method.GetParameters().Length == 1)
            {
                List<object> queryParams = new List<object>();
                queryParams.Add(_httpContext);
                if (request.HttpMethod == "GET") strParams = strParams.Skip(1).ToArray();
                bool BadRequest = false;
                try
                {
                    queryParams.AddRange(method.GetParameters().Skip(1)
                        .Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
                        .ToList());
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

            if (ret != null && ret.ToString().Split(' ')[0] == "Redirect:")
            {
                using (response)
                {
                    response.StatusCode = (int) HttpStatusCode.Redirect;
                    var location = GetLocation(ret.ToString().Split(' ')[1]);
                    response.Headers.Set("Location",location); 
                
                    return true;
                }
            }

            if (ret != null && ret.ToString().Split(':')[0] == "auth_cookie")
            {
                using (response)
                {
                    var userParams = ret.ToString().Split(':');
                    string session = SessionManager.CreateSession(Convert.ToInt32(userParams[1]), userParams[2], Convert.ToBoolean(userParams[3]));

                    var value = JsonSerializer.Serialize(new AuthCookie { Id = session});
                    var cookie = new Cookie("SessionId", value);
                    cookie.Path = "/";
                    cookie.Expires = Convert.ToBoolean(userParams[3])
                        ? DateTime.Now.AddMonths(1)
                        : DateTime.Now.AddMinutes(20);
                    response.Cookies.Add(cookie);
                    
                    response.StatusCode = (int) HttpStatusCode.Redirect;
                    var location = GetLocation("Redirect: success_login".Split(' ')[1]);
                    response.Headers.Set("Location",location); 

                    return true;
                }
            }
            
            if(method.GetCustomAttributes().Any(a => a.GetType().Name == "RequireAuth"))
            {
                if (!ValidAuthCookie())
                {
                    ret = "401 Unauthorized!";
                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                }
                
            }

            response.ContentType = "Application/json";
            
            if(ret != null && ret.ToString().StartsWith("<!DOCTYPE html>")) response.ContentType = "text/html";

            buffer = response.ContentType == "Application/json" ? Encoding.ASCII.GetBytes(JsonSerializer.Serialize(ret)) : Encoding.ASCII.GetBytes(ret.ToString());
            response.ContentLength64 = buffer.Length;

            output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
            
            return true;

            bool ValidAuthCookie()
            {
                if (request.Cookies["SessionId"] == null) return false;
                var cookieValue = request.Cookies["SessionId"].Value.Replace('.',',');
                var status = JsonSerializer.Deserialize<AuthCookie>(cookieValue);
                if (SessionManager.ValidateSession(status.Id)) return true;
                return false;
            }
        }
        
        private string GetExtension(string url)
        {
            string ext = Path.GetExtension(url);
            if (Directory.Exists(StaticSetting.Site + url)) ext = ".html";
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

        private string GetLocation(string redirectData)
        {
            switch (redirectData)
            {
                case "invalid_credentials":
                    return "/login/";
                case "not_owner":
                    return "/not_owner.html";
                case "already_on_sale":
                    return "/alreadyonsale.html";
                case "sell_success":
                    return "/marketplace/";
                case "nomoney":
                    return "/balanceerror.html";
                case "bought":
                    return "/profile/";
                case "unauthorized":
                    return "/login/";
                case "login_page":
                    return "/login/";
                case "success_login":
                    return "/profile/";
                case "invalid_data":
                    return "/login/fail.html";
                case "profile":
                    return "/profile/";
            }

            return "/404.html";
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