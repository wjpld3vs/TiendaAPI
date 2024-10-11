using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;
        public ProductService(ITiendaSettings settings) 
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> Get()
            => await _products.FindAsync(
                new BsonDocument()).Result.ToListAsync();

        public async Task<Product> GetById(string id)
            => await _products.FindAsync(
                new BsonDocument {{ "_id", new ObjectId(id) }}).Result.FirstAsync();

        public async Task Create(Product product)
            => await _products.InsertOneAsync(product);

        public async Task Update(Product product)
        {
            var filter = Builders<Product>
                .Filter
                .Eq(x => x.Id, product.Id);

            await _products.ReplaceOneAsync(filter, product);
        }

        public async Task Delete(string id)
        { 
            var filter = Builders<Product>
                .Filter
                .Eq(x => x.Id, new ObjectId(id));

            await _products.DeleteOneAsync(filter);
        }

    }
}