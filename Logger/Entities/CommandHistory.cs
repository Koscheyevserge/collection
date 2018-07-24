using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Entities
{
    public class CommandHistory
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CommandUri { get; set; }
        [BsonRepresentation(BsonType.Document)]
        public string BeforeState { get; set; }
        [BsonRepresentation(BsonType.Document)]
        public string AfterState { get; set; }
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}
