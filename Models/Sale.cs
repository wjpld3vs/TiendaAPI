using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TiendaAPI.Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Customer")]
        public Customer Customer  { get; set; }

        [BsonElement("SaleDate")]
        public DateTime FechaVenta { get; set; }

        [BsonElement("Products")]
        public List<Product> Products { get; set; }

        [BsonElement("Total")]
        public decimal Total { get; set; }

    }
}
