using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TiendaAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Role")]
        public string Role { get; set; }
    }
}
