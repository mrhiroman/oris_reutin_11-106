using System;
using System.Collections.Generic;
using System.Linq;
using HttpServer.Models;
using MyORM;

namespace HttpServer
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        string Insert(TEntity entity);
        string Delete(TEntity entity);
    }

    public class UserRepository : IRepository<User>
    {
        string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NftDB;Integrated Security=True";

        
        public User GetById(int id)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            var acc = db.ExecuteQuery<User>($"SELECT * FROM Users WHERE Id={id}");
            if (acc.ToList().Count != 0)
            {
                return acc.ToList()[0];
            }

            return null;
        }

        public List<User> GetAll()
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.Select<User>().ToList();
        }

        public string Insert(User entity)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            if (db.Insert(entity) != 0) return "Success!";
            return "Error! Account or email already exists!";
        }

        public string Delete(User entity)
        {
            throw new NotImplementedException();
        }
    }
}