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
    [HttpController("accounts")]
    public class Accounts
    {
        AccountRepository _repository = new AccountRepository();
        string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";

        [RequireAuth]
        [HttpGET("list")]
        public List<Account> GetAccounts(HttpListenerContext context)
        {
            return _repository.GetAll();
        }
        
        [HttpGET("id")]
        public Account GetAccountById(HttpListenerContext context, int id)
        {
            return _repository.GetById(id);
        }
        
        [HttpPOST("add")]
        public string SaveAccount(HttpListenerContext context, string login, string pass)
        {
            return _repository.Insert(new Account {Login = login, Password = pass});
        }

        [HttpPOST("login")]
        public string Login(HttpListenerContext context, string login, string pass)
        {
            var db = new Database(_connectionString);
            string query = $"SELECT * FROM Accounts WHERE Login='{login}' AND Password='{pass}'";
            var list = db.ExecuteQuery<Account>(query);
            if (list.Count() != 0) return $"auth_cookie:{list.ToList()[0].Id}";
            return "Not found";
        }

        [RequireAuth]
        [HttpGET("myaccount")]
        public Account MyAccount(HttpListenerContext context)
        {
            var cookie = JsonSerializer.Deserialize<AuthCookie>(context.Request.Cookies["SessionId"]?.Value.Replace('.',','));
            return GetAccountById(context, cookie.id);
        }
    }
}