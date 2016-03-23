using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ServeUp.Data
{
    /// <summary>
    /// A MongoDB repository. Maps to a collection with the same name
    /// as type TEntity.
    /// </summary>
    /// <typeparam name="T">Entity type for this repository</typeparam>
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected IMongoDatabase database;
        protected IMongoCollection<TEntity> collection;

        public MongoRepository()
        {
            var credential = MongoCredential.CreateMongoCRCredential(ConfigurationManager.AppSettings.Get("DatabaseName").ToString(), ConfigurationManager.AppSettings.Get("DatabaseUser").ToString(), ConfigurationManager.AppSettings.Get("DatabasePassword").ToString());
            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress(ConfigurationManager.AppSettings.Get("DatabaseIP").ToString(), int.Parse(ConfigurationManager.AppSettings.Get("DatabasePort").ToString())),
                ConnectTimeout = new TimeSpan(0, 1, 5),
                SocketTimeout = new TimeSpan(0, 1, 5),
                MaxConnectionPoolSize = 1500,
                WaitQueueSize = 1500,
                WaitQueueTimeout = new TimeSpan(0, 1, 0)
            };

            var client = new MongoClient(settings);
            database = client.GetDatabase(ConfigurationManager.AppSettings.Get("DatabaseName").ToString());
            collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            collection.InsertOne(entity);
            //await collection.InsertOneAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            await collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            await collection.DeleteOneAsync(filter);
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();
        }

        public IList<TEntity> GetAll()
        {
            var filter = new BsonDocument();
            return collection. FindSync<TEntity>(filter).ToList();
        }

        public TEntity GetById(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            return collection.FindSync<TEntity>(filter).FirstOrDefault();
        }                
    }
}
