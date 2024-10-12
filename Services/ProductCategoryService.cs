using MongoDB.Bson;
using MongoDB.Driver;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class ProductCategoryService
    {
        private readonly IMongoCollection<ProductCategory> _productCategory;
        public ProductCategoryService(ITiendaSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _productCategory = database.GetCollection<ProductCategory>("ProductCategory");
        }

        public async Task<List<ProductCategory>> Get()
            => await _productCategory.FindAsync(
        new BsonDocument()).Result.ToListAsync();

        public async Task<ProductCategory> GetById(string id)
            => await _productCategory.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task Create(ProductCategory productCategory)
            => await _productCategory.InsertOneAsync(productCategory);

        public async Task Update(ProductCategory productCategory)
        {
            var filter = Builders<ProductCategory>
                .Filter
                .Eq(x => x.Id, productCategory.Id);

            await _productCategory.ReplaceOneAsync(filter, productCategory);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<ProductCategory>
                .Filter
                .Eq(x => x.Id, new ObjectId(id));

            await _productCategory.DeleteOneAsync(filter);
        }

    }
}
