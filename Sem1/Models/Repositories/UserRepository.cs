using System;
using System.Collections.Generic;
using System.Linq;
using HttpServer.Models;

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
        private string _connectionString = StaticSetting.ConnectionString;
        
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
            try
            {
                if (db.Insert(entity) != 0)
                {
                    db = new DatabaseAccessUnit(_connectionString);
                    return "Success";
                }
            }
            catch (Exception e)
            {
                return "Error! Account or email already exists!";
            }
            
            return "Error! Account or email already exists!";
        }

        public string Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Wallet GetWallet(int userId)
        {
            try
            {
                var db = new DatabaseAccessUnit(_connectionString);
                var wallets = db.ExecuteQuery<Wallet>($"SELECT * FROM Wallets WHERE UserId={userId}");
                return wallets.ToList().FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        public string UpdateWallets(int userId, string binance, string bybit, string bitcoin)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Wallets SET Binance='{binance}' WHERE UserId={userId}");
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Wallets SET Bybit='{bybit}' WHERE UserId={userId}");
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Wallets SET Bitcoin='{bitcoin}' WHERE UserId={userId}");
            return "Success";
        }
    }
}