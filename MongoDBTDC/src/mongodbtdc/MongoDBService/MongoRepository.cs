using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBTDC.MongoDBService
{
    public class MongoRepository<T> : IDisposable where T : class
    {
        protected static IMongoDatabase _database;
        private MongoClient _mongoClient;

        public MongoRepository(string dataBaseName = null, bool isReplicaSet = true)
        {
            var mongoSettings = new MongoClientSettings();
            List<MongoServerAddress> servers = new List<MongoServerAddress>();

            if (isReplicaSet)
            {
                servers.Add(new MongoServerAddress("xxx.xxx.x.xxx", 27017));
                servers.Add(new MongoServerAddress("xxx.xxx.x.xxx", 27017));
                servers.Add(new MongoServerAddress("xxx.xxx.x.xxx", 27017));
                

                mongoSettings.WriteConcern =
                  new WriteConcern(1, TimeSpan.FromMilliseconds(1000), null, true);
                
                mongoSettings.Servers = servers;
                mongoSettings.ConnectionMode = ConnectionMode.ReplicaSet;
                mongoSettings.ReplicaSetName = "rsTDC";
                _mongoClient = new MongoClient(mongoSettings);

            }

            else
            {
                servers.Add(new MongoServerAddress("xxx.xxx.xxx.xxx", 27017));
                mongoSettings.Servers = servers;
                mongoSettings.ConnectionMode = ConnectionMode.Automatic;
                _mongoClient = new MongoClient(mongoSettings);
            }

            _database = _mongoClient.GetDatabase(dataBaseName);

        }


        public T GetById(ObjectId id)
        {
            var collection = _database.GetCollection<BsonDocument>(typeof(T).Name.ToLower());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var doc = collection.Find(filter).FirstOrDefaultAsync().Result;
            if (doc != null)
            {
                return BsonSerializer.Deserialize<T>(doc);
            }
            return null;
        }


        public void Add<T>(T item) where T : class, new()
        {
            var objectSave = _database.GetCollection<T>(typeof(T).Name.ToLower());
            objectSave.InsertOneAsync(item).Wait();
        }

        public List<T> All<T>() where T : class, new()
        {
            var collection =  _database.GetCollection<T>(typeof(T).Name.ToLower());
            return collection.Find(Builders<T>.Filter.Empty).ToListAsync().Result;
        }

        public void Dispose()
        {

        }
    }
}
