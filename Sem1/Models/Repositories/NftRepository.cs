using System.Collections.Generic;
using System.Linq;
using MyORM;

namespace HttpServer.Models
{
    public class NftRepository : IRepository<Nft>
    {
        string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NftDB;Integrated Security=True";
        
        public Nft GetById(int id)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            var result = db.ExecuteQuery<Nft>($"SELECT * FROM Nfts WHERE Id={id}");
            if (result.ToList()[0] != null)
            {
                return result.ToList()[0];
            }

            return null;
        }

        public List<Nft> GetAll()
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.Select<Nft>().ToList();
        }

        public string Insert(Nft entity)
        {
            throw new System.NotImplementedException();
        }

        public string Delete(Nft entity)
        {
            throw new System.NotImplementedException();
        }
    }
}