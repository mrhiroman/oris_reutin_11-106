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
        AccountRepository repository = new AccountRepository();

        [HttpGET("list")]
        public List<Account> GetAccounts()
        {
            return repository.GetAll();
        }
        
        [HttpGET("id")]
        public Account GetAccountById(int id)
        {
            return repository.GetById(id);
        }
        
        [HttpPOST("add")]
        public string SaveAccount(string login, string pass)
        {
            return repository.Insert(new Account {Login = login, Password = pass});
        }
    }
}