using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBTDC.Model
{
    public class Person
    {
        public Person()
        {

        }

        [BsonElement("_id")]
        [BsonIgnoreIfNull]
        public ObjectId PersonId { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("nome")]
        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [BsonElement("data_criacao")]
        [BsonIgnoreIfNull]
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
