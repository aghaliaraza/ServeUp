using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ServeUp.Data
{
    public class Class1
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public Class1()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("test");

            //InserAsync();
            //Query();
            QueryByTopLevelField();
            QueryByEmbeddedDocument();
            QueryByFieldInAnArray();
        }

        public async Task InsertAsync()
        {

            var document = new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "zipcode", "10075" },
                        { "building", "1480" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
                { "borough", "Manhattan" },
                { "cuisine", "Italian" },
                { "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "A" },
                            { "score", 11 }
                        },
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "B" },
                            { "score", 17 }
                        }
                    }
                },
                { "name", "Vella" },
                { "restaurant_id", "41704620" }
            };

            var collection = _database.GetCollection<BsonDocument>("restaurants");
            await collection.InsertOneAsync(document);
        }

        public async Task Query()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var count = 0;
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        // process document
                        count++;
                    }
                }
            }
        }

        public async Task QueryByTopLevelField()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var result = await collection.Find(filter).ToListAsync();
        }

        public async Task QueryByEmbeddedDocument()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("address.zipcode", "10075");
            var result = await collection.Find(filter).ToListAsync();
        }

        public async Task QueryByFieldInAnArray()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("grades.grade", "B");
            var result = await collection.Find(filter).ToListAsync();
        }

        public async Task QueryByGreaterThanOperator()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Gt("grades.score", 30);
            var result = await collection.Find(filter).ToListAsync();
        }

        public async Task QueryByLessThanOperator()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Lt("grades.score", 10);
            var result = await collection.Find(filter).ToListAsync();
        }
        public async Task QueryByLogicalAnd()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("cuisine", "Italian") & builder.Eq("address.zipcode", "10075");
            var result = await collection.Find(filter).ToListAsync();
        }

        public async Task QueryByLogicalOr()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("cuisine", "Italian") | builder.Eq("address.zipcode", "10075");
            var result = await collection.Find(filter).ToListAsync();
        }

        public async Task QuerySortResults()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var sort = Builders<BsonDocument>.Sort.Ascending("borough").Ascending("address.zipcode");
            var result = await collection.Find(filter).Sort(sort).ToListAsync();
        }


        public async Task UpdateTopLevelFields()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("name", "Juni");
            var update = Builders<BsonDocument>.Update
                .Set("cuisine", "American (New)")
                .CurrentDate("lastModified");
            var result = await collection.UpdateOneAsync(filter, update);
        }
        public async Task UpdateEmbeddedFields()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("restaurant_id", "41156888");
            var update = Builders<BsonDocument>.Update.Set("address.street", "East 31st Street");
            var result = await collection.UpdateOneAsync(filter, update);

            //can use assertion like below

            //result.MatchedCount.Should().Be(1);

            //or

            //if (result.IsModifiedCountAvailable)
            //{
            //    result.ModifiedCount.Should().Be(1);
            //}
        }
        public async Task UpdateMultipleDocuments()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("address.zipcode", "10016") & builder.Eq("cuisine", "Other");
            var update = Builders<BsonDocument>.Update
                .Set("cuisine", "Category To Be Determined")
                .CurrentDate("lastModified");
        }

        public async Task DeleteMatchCondition()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var result = await collection.DeleteManyAsync(filter);
        }

        public async Task DeleteAll()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var filter = new BsonDocument();
            var result = await collection.DeleteManyAsync(filter);
        }



        public async Task GroupDocumentByFieldAndCount()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var aggregate = collection.Aggregate().Group(new BsonDocument { { "_id", "$borough" }, { "count", new BsonDocument("$sum", 1) } });
            var results = await aggregate.ToListAsync();
        }

        public async Task GroupDocumentsByFilter()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var aggregate = collection.Aggregate()
                .Match(new BsonDocument { { "borough", "Queens" }, { "cuisine", "Brazilian" } })
                .Group(new BsonDocument { { "_id", "$address.zipcode" }, { "count", new BsonDocument("$sum", 1) } });
            var results = await aggregate.ToListAsync();
        }


        public async Task DropCollectionIncludingIndexis()
        {
            await _database.DropCollectionAsync("restaurants");
        }


        public async Task CreateSingleFieldIndex()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var keys = Builders<BsonDocument>.IndexKeys.Ascending("cuisine");
            await collection.Indexes.CreateOneAsync(keys);

            //assertion
            using (var cursor = await collection.Indexes.ListAsync())
            {
                var indexes = await cursor.ToListAsync();
                indexes.Find(index => index["name"] == "cuisine_1");
            }
        }

        public async Task CreateCompoundIndex()
        {
            var collection = _database.GetCollection<BsonDocument>("restaurants");
            var keys = Builders<BsonDocument>.IndexKeys.Ascending("cuisine").Ascending("address.zipcode");
            await collection.Indexes.CreateOneAsync(keys);

            //assertion
            using (var cursor = await collection.Indexes.ListAsync())
            {
                var indexes = await cursor.ToListAsync();
                indexes.Find(index => index["name"] == "cuisine_1_address.zipcode_1");
            }
        }
    }
}
