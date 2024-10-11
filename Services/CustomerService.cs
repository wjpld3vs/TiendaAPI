using MongoDB.Bson;
using MongoDB.Driver;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;
        public CustomerService(ITiendaSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _customers = database.GetCollection<Customer>("Customers");
        }

        public async Task<List<Customer>> Get()
            => await _customers.FindAsync(
                new BsonDocument()).Result.ToListAsync();

        public async Task<Customer> GetById(string id)
            => await _customers.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task Create(Customer customer)
            => await _customers.InsertOneAsync(customer);

        public async Task Update(Customer customer)
        {
            var filter = Builders<Customer>
                .Filter
                .Eq(x => x.Id, customer.Id);

            await _customers.ReplaceOneAsync(filter, customer);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<Customer>
                .Filter
                .Eq(x => x.Id, new ObjectId(id));

            await _customers.DeleteOneAsync(filter);
        }
    }
}
