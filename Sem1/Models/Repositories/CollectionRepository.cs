using System;
using System.Collections.Generic;
using System.Linq;
using MyORM;

namespace HttpServer.Models.Repositories
{
    public class CollectionRepository : IRepository<Collection>
    {
        private string _connectionString = StaticSetting.ConnectionString;
        
        public Collection GetById(int id)
        {
            var db = new DatabaseAccessUnit(_connectionString);
            var result = db.ExecuteQuery<Collection>($"SELECT * FROM Collections WHERE Id={id}").ToList();
            if (result[0] != null)
            {
                return result[0];
            }

            return null;
        }

        public List<Collection> GetAll()
        {
            var db = new DatabaseAccessUnit(_connectionString);
            return db.Select<Collection>().ToList();
        }

        public string Insert(Collection entity)
        {
            throw new NotImplementedException();
        }

        public string Delete(Collection entity)
        {
            throw new System.NotImplementedException();
        }
    }
}