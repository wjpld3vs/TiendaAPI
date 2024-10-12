using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TiendaAPI.Models
{
    public class ProductCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        
    }
}
