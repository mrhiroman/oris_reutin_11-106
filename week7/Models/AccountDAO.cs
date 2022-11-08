using System;
using System.Collections.Generic;
using System.Linq;
using HttpServer.Models;
using MyORM;

namespace HttpServer
{
    public interface AccountDAO
    {
        List<Account> GetAll();
        Account GetById(int id);
        string Add(Account entity);
        string Delete(Account entity);
    }
    
    public class AccountDAOImpl : AccountDAO
    {
        string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";
        
        public List<Account> GetAll()
        {
            var db = new Database(_connectionString);
            return db.Select<Account>().ToList();
        }

        public Account GetById(int id)
        {
            var db = new Database(_connectionString);
            var acc = db.ExecuteQuery<Account>("SELECT * FROM Accounts WHERE Id={id}");
            if (acc.ToList()[0] != null)
            {
                return acc.ToList()[0];
            }

            return null;
        }

        public string Add(Account entity)
        {
            var db = new Database(_connectionString);
            db.Insert(entity);
            return "Steam_redirect";
        }

        public string Delete(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}