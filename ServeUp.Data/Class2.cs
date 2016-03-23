using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ServeUp.Data
{
    public class Class2
    {
        //protected static IMongoClient _client;
        //protected static IMongoDatabase _database;

        protected readonly IRepository<Customer> IR;

        public Class2()
        {
            IR = new MongoRepository<Customer>();
            IR.Insert(new Customer { Name = "abc", Phone = "123" });

            var a = IR.SearchFor(x => x.Name == "abc");

            //var credential = MongoCredential.CreateMongoCRCredential("admin", "imran", "P@$$W0RD");
            //var settings = new MongoClientSettings
            //{
            //    Credentials = new[] { credential },
            //    Server = new MongoServerAddress("1.0.0.27", 27017),
            //    ConnectTimeout = new TimeSpan(0, 1, 5),
            //    SocketTimeout = new TimeSpan(0, 1, 5),
            //    MaxConnectionPoolSize = 1500,
            //    WaitQueueSize = 1500,
            //    WaitQueueTimeout = new TimeSpan(0, 1, 0)
            //};

            //_client = new MongoClient(settings);

            //_database = _client.GetDatabase("admin");

            //_database.ListCollections();
        }
    }

    public class Customer: EntityBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
