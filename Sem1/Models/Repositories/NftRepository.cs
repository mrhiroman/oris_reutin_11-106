using System.Collections.Generic;
using System.Linq;

namespace HttpServer.Models
{
    public class NftRepository : IRepository<Nft>
    {
        private string _connectionString = StaticSetting.ConnectionString;
        
        public Nft GetById(int id)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            var result = db.ExecuteQuery<Nft>($"SELECT * FROM Nfts WHERE Id={id}").ToList();
            if (result[0] != null)
            {
                return result[0];
            }

            return null;
        }

        public List<Nft> GetAllForUser(int ownerId)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.ExecuteQuery<Nft>($"SELECT * FROM Nfts WHERE OwnerId={ownerId}").ToList();
        }

        public List<Nft> GetAllForCollection(int collectionId)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.ExecuteQuery<Nft>($"SELECT * FROM Nfts WHERE CollectionId={collectionId}").ToList();
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
        
        public List<Nft> RetrieveSellList()
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.ExecuteQuery<Nft>($"SELECT * FROM Nfts WHERE CollectionId=2").ToList();
        }
    }
}