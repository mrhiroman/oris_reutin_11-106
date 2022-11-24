using System.Collections.Generic;
using System.Linq;
using MyORM;

namespace HttpServer.Models.Repositories
{
    public class DealRepository : IRepository<Deal>
    {
        string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NftDB;Integrated Security=True";
        
        public Deal GetById(int id)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            var result = db.ExecuteQuery<Deal>($"SELECT * FROM Deals WHERE Id={id}").ToList();
            if (result[0] != null)
            {
                return result[0];
            }

            return null;
        }

        public List<Deal> GetAll()
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.Select<Deal>().ToList();
        }

        public string Insert(Deal entity)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.Insert(entity).ToString();
        }

        public string Delete(Deal entity)
        {
            throw new System.NotImplementedException();
        }

        public string AddToSellList(Deal entity)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            var possibleEntity =
                db.ExecuteQuery<Deal>($"SELECT * FROM Deals WHERE NftId={entity.NftId} AND status='active'").ToList();
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Nfts SET CollectionId=2 WHERE Id={entity.NftId}");
            if (possibleEntity.Count == 0)
            {
                db = new DatabaseAccessUnit(_connectionString);
                db.Insert(entity);
                return "Redirect: sell_success";
            }
            else
            {
                return "Redirect: already_on_sale";
            }
           
        }

        public string SetToUser(Deal entity, User user)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Users SET Balance=Balance-{entity.Cost} WHERE Id={user.Id}");
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Users SET Balance=Balance+{entity.Cost} WHERE Id={entity.SellerId}");
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Nfts SET OwnerId={user.Id} WHERE Id={entity.NftId}");
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Nfts SET CollectionId=1 WHERE Id={entity.NftId}");
            db = new DatabaseAccessUnit(_connectionString);
            db.ExecuteNonQuery($"UPDATE Deals SET Status='sold' WHERE Id={entity.Id}");
            return "Success!";
        }
        
        public List<Deal> RetrieveSellList()
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.ExecuteQuery<Deal>($"SELECT * FROM Deals WHERE CollectionId=2 AND Status='active'").ToList();
        }

        public List<Deal> RetrieveCollection(int collectionId)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.ExecuteQuery<Deal>($"SELECT * FROM Deals WHERE CollectionId={collectionId} AND Status='active'").ToList();
        }
    }
}