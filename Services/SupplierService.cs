using MongoDB.Bson;
using MongoDB.Driver;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class SupplierService
    {
        private readonly IMongoCollection<Supplier> _suppliers;
        public SupplierService(ITiendaSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _suppliers = database.GetCollection<Supplier>("Suppliers");
        }

        public async Task<List<Supplier>> Get()
            => await _suppliers.FindAsync(
                new BsonDocument()).Result.ToListAsync();

        public async Task<Supplier> GetById(string id)
            => await _suppliers.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task Create(Supplier supplier)
            => await _suppliers.InsertOneAsync(supplier);

        public async Task Update(Supplier supplier)
        {
            var filter = Builders<Supplier>
                .Filter
                .Eq(x => x.Id, supplier.Id);

            await _suppliers.ReplaceOneAsync(filter, supplier);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<Supplier>
                .Filter
                .Eq(x => x.Id, new ObjectId(id));

            await _suppliers.DeleteOneAsync(filter);
        }

    }
}
