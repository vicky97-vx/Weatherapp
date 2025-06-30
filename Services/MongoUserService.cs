using MongoDB.Driver;
using System.Text.Json;
using WRModel.Models;
using Blazored.SessionStorage;
using Microsoft.Extensions.Configuration;

namespace WSService.Services
{
    public class MongoUserService
    {
        private readonly IMongoCollection<UserModel> _users;
        private readonly ISessionStorageService _sessionStorage;

        public MongoUserService(ISessionStorageService sessionStorage, IConfiguration configuration)
        {
            _sessionStorage = sessionStorage;

            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _users = database.GetCollection<UserModel>(configuration["MongoDbSettings:UserCollection"]);
        }

        public async Task<UserModel?> GetCurrentUserAsync()
        {
            var json = await _sessionStorage.GetItemAsync<string>("currentUser");
            return json == null ? null : JsonSerializer.Deserialize<UserModel>(json);
        }

        public async Task SigninUserAsync(UserModel user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<UserModel?> GetUserAsync(string email)
        {
            try
            {
                var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
                return user;
            }
            catch (TimeoutException tex)
            {
                Console.WriteLine("MongoDB Timeout: " + tex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MongoDB Error: " + ex.Message);
                return null;
            }
        }
    }
}
