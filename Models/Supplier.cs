using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TiendaAPI.Models
{
    public class Supplier
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("Country")]
        [BsonRepresentation(BsonType.String)]
        public string Country { get; set; }

    }
}
