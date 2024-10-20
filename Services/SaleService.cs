using MongoDB.Bson;
using MongoDB.Driver;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class SaleService
    {
        private readonly IMongoCollection<Sale> _sales;
        public SaleService(ITiendaSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _sales = database.GetCollection<Sale>("Sales");
        }

        public async Task<List<Sale>> Get()
            => await _sales.FindAsync(
                new BsonDocument()).Result.ToListAsync();

        public async Task<Sale> GetById(string id)
            => await _sales.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task Create(Sale sale)
            => await _sales.InsertOneAsync(sale);

        public async Task Update(Sale sale)
        {
            var filter = Builders<Sale>
                .Filter
                .Eq(x => x.Id, sale.Id);

            await _sales.ReplaceOneAsync(filter, sale);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<Sale>
                .Filter
                .Eq(x => x.Id, new ObjectId(id));

            await _sales.DeleteOneAsync(filter);
        }
    }
}
