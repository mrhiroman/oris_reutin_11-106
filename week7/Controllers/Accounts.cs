using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        [HttpGET("list")]
        public List<Account> GetAccounts()
        {
            return _repository.GetAll();
        }
        
        [HttpGET("id")]
        public Account GetAccountById(int id)
        {
            return _repository.GetById(id);
        }
        
        [HttpPOST("add")]
        public string SaveAccount(string login, string pass)
        {
            return _repository.Insert(new Account {Login = login, Password = pass});
        }

        [HttpPOST("login")]
        public bool Login(string login, string pass)
        {
            var db = new Database(_connectionString);
            string query = $"SELECT * FROM Accounts WHERE Login='{login}' AND Password='{pass}'";
            var list = db.ExecuteQuery<Account>(query);
            if (list.Count() == 0) return false;
            return true;
        }
    }
}