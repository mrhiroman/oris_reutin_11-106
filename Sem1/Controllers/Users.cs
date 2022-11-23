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
        public string AddUser(HttpListenerContext context, string login, string email, string pass)
        {
            string salt = HashManager.CreateSalt();
            string hashedPassword = HashManager.GetSHA256(salt + pass);
            return _repository.Insert(new User {Login = login, Email = email.Replace("%40","@"),
                Salt = salt, HashedPassword = hashedPassword, Balance = 50});
        }

        [HttpPOST("authorize")]
        public string Authorize(HttpListenerContext context, string login, string pass)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            
            string saltQuery = $"SELECT * FROM Users WHERE Login='{login}'";
            var saltLst = db.ExecuteQuery<User>(saltQuery).ToList();
            string salt = saltLst.Count != 0 ? saltLst[0].Salt : null;
            if (salt != null)
            {
                string hashedPassword = HashManager.GetSHA256(salt + pass);
                string query = $"SELECT * FROM Users WHERE Login='{login}' AND HashedPassword='{hashedPassword}'";
                db = new DatabaseAccessUnit(_connectionString);
                var list = db.ExecuteQuery<User>(query);
                if (list.Count() != 0) return $"auth_cookie:{list.ToList()[0].Id}:{list.ToList()[0].Login}";
            }
            
            return "Invalid Data"; //TODO add error message and redirect
        }

        [RequireAuth]
        [HttpGET("myaccount")]
        public User MyAccount(HttpListenerContext context)
        {
            var cookie = JsonSerializer.Deserialize<AuthCookie>(context.Request.Cookies["SessionId"]?.Value.Replace('.',','));
            var session = SessionManager.GetInformation(cookie.Id);
            return GetUserById(context, session.AccountId);
        }
    }
}