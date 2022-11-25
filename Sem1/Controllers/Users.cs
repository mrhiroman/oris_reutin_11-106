using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.Json;
using HttpServer.Attributes;
using HttpServer.Models;
using MyORM;

namespace HttpServer.Controllers
{
    [HttpController("users")]
    public class Users
    {
        UserRepository _repository = new UserRepository();
        string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NftDB;Integrated Security=True";

        [RequireAuth]
        [HttpGET("list")]
        public List<User> GetUsers(HttpListenerContext context)
        {
            return _repository.GetAll();
        }
        
        [HttpGET("id")]
        public User GetUserById(HttpListenerContext context, int id)
        {
            return _repository.GetById(id);
        }
        
        [HttpPOST("add")]
        public string AddUser(HttpListenerContext context, string login, string email, string pass, string pass2)
        {
            if (pass != pass2) return "Redirect: login_page";
            string salt = HashManager.CreateSalt();
            string hashedPassword = HashManager.GetSHA256(salt + pass);
            var result = _repository.Insert(new User {Login = login, Email = email.Replace("%40","@"),
                Salt = salt, HashedPassword = hashedPassword, Balance = 50});

            var db = new DatabaseAccessUnit(_connectionString);
            string query = $"SELECT * FROM Users WHERE Login='{login}'";
            var list = db.ExecuteQuery<User>(query).ToList();

            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"INSERT INTO Wallets VALUES({list.ToList()[0].Id},'empty','empty','empty')");
            
            if (result == "Success")
            {
                return $"auth_cookie:{list.ToList()[0].Id}:{list.ToList()[0].Login}:false";
            }

            return "Redirect: invalid_data";
        }

        [HttpPOST("authorize")]
        public string Authorize(HttpListenerContext context, string login, string pass, string rememberMe)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            bool isLongSession = rememberMe == "on";
                
            string saltQuery = $"SELECT * FROM Users WHERE Login='{login}'";
            var lst = db.ExecuteQuery<User>(saltQuery).ToList();
            
            string salt = lst.Count != 0 ? lst[0].Salt : null;
            if (salt != null)
            {
                string hashedPassword = HashManager.GetSHA256(salt + pass);
                if (hashedPassword == lst[0].HashedPassword)
                {
                    return $"auth_cookie:{lst[0].Id}:{lst[0].Login}:{isLongSession}";
                }
            }
            
            return "Redirect: invalid_data";
        }

        [RequireAuth]
        [HttpGET("myaccount")]
        public User MyAccount(HttpListenerContext context)
        {
            var cookie = context.Request.Cookies["SessionId"]?.Value != null ? JsonSerializer.Deserialize<AuthCookie>(context.Request.Cookies["SessionId"]?.Value) : null;
            if(cookie == null) return null;
            var session = SessionManager.GetInformation(cookie.Id);
            return GetUserById(context, session.AccountId);
        }

        [RequireAuth]
        [HttpGET("logout")]
        public string Logout(HttpListenerContext context)
        {
            var cookie = context.Request.Cookies["SessionId"]?.Value != null ? JsonSerializer.Deserialize<AuthCookie>(context.Request.Cookies["SessionId"]?.Value) : null;
            if(cookie == null) return "Redirect: login_page";
            if (SessionManager.ValidateSession(cookie.Id))
            {
                var session = SessionManager.GetInformation(cookie.Id);
                SessionManager.ExpireSession(session);
            }
            return "Redirect: login_page";
        }
        
        [RequireAuth]
        [HttpPOST("updatewallets")]
        public string UpdateWallets(HttpListenerContext context, string binance, string bybit, string bitcoin, int fill1, int fill2)
        {
            var user = MyAccount(context);
            _repository.UpdateWallets(user.Id, binance, bybit, bitcoin);
            return "Redirect: bought";
        }
    }
}