using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiendaAPI.Models;
using TiendaAPI.Settings;

namespace TiendaAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly string _token;
        public UserService(ITiendaSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _users = database.GetCollection<User>("Users");
            _token = settings.Token;

            var indexKeys = Builders<User>.IndexKeys.Ascending(x => x.Name);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<User>(indexKeys, indexOptions);
            _users.Indexes.CreateOne(indexModel);

        }

        public async Task<User?> Validate(string username, string password)
        { 
            var user = await _users.FindAsync(
                new BsonDocument { { "Name", username } }).Result.FirstAsync();

            if (user != null)
                return user;

            return null;
        }

        public string GenerateToken(User user) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
{
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim("Role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
               issuer: "https://localhost",
               audience: "https://localhost",
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<List<User>> Get()
            => await _users.FindAsync(
                new BsonDocument()).Result.ToListAsync();

        public async Task<User> GetById(string id)
            => await _users.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task Create(User user)
            => await _users.InsertOneAsync(user);

        public async Task Update(User user)
        {
            var filter = Builders<User>
                .Filter
                .Eq(x => x.Id, user.Id);

            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task Delete(string id)
        {
            var filter = Builders<User>
                .Filter
                .Eq(x => x.Id, new ObjectId(id));

            await _users.DeleteOneAsync(filter);
        }
    }
}
