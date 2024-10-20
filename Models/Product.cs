using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TiendaAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Price")]
        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }

        [BsonElement("Stock")]
        [BsonRepresentation(BsonType.Int32)]
        public int Stock { get; set; }

        [BsonElement("MinStock")]
        [BsonRepresentation(BsonType.Int32)]
        public int MinStock { get; set; }

        [BsonElement("Supplier")]
        public Supplier Supplier { get; set; }

        [BsonElement("Category")]
        public ProductCategory Category { get; set; }
    }
}
