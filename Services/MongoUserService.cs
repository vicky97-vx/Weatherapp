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
            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("No session data found.");
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<UserModel>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialization failed: " + ex.Message);
                return null;
            }
        }

        public async Task SigninUserAsync(UserModel user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task <UserModel?> LoginAsync(string email, string password)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task AddFavoriteAsync(string email, string city)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var update = Builders<UserModel>.Update.AddToSet(u => u.FavoriteCities, city);
            await _users.UpdateOneAsync(filter, update);
        }

        public async Task RemoveFavoriteCityAsync(string email, string city)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var update = Builders<UserModel>.Update.Pull(u => u.FavoriteCities, city);
            await _users.UpdateOneAsync(filter, update);
        }

        public async Task UpdateUserAsync(UserModel updatedUser)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, updatedUser.Email);
            await _users.ReplaceOneAsync(filter, updatedUser);
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




// using MongoDB.Driver;
// using Microsoft.Extensions.Configuration;
// using WRModel.Models;
// using System.Threading.Tasks;

// namespace WSService.Services
// {
//     public class MongoUserService
//     {
//         private readonly IMongoCollection<UserModel> _users;

//         public MongoUserService(IConfiguration config)
//         {
//             var client = new MongoClient(config.GetConnectionString("mongodb"));
//             var database = client.GetDatabase("WeatherAppDb"); 
//             _users = database.GetCollection<UserModel>("Users");
//         }

        
//         public async Task<UserModel?> GetUserAsync(string email)
//         {
//             return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
//         }

        
//         public async Task AddUserAsync(UserModel user)
//         {
//             await _users.InsertOneAsync(user);
//         }

        
//         public async Task<UserModel?> LoginAsync(string email, string password)
//         {
//             var user = await GetUserAsync(email);
//             if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
//             {
//                 return user;
//             }
//             return null;
//         }

        
//         public async Task UpdateUserAsync(UserModel user)
//         {
//             var filter = Builders<UserModel>.Filter.Eq(u => u.Email, user.Email);
//             await _users.ReplaceOneAsync(filter, user);
//         }

//         public async Task AddFavoriteAsync(string email, string city)
//         {
//             var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
//             var update = Builders<UserModel>.Update.AddToSet(u => u.FavoriteCities, city);
//             await _users.UpdateOneAsync(filter, update);
//         }


        
//         public async Task RemoveFavoriteCityAsync(string email, string city)
//         {
//             var update = Builders<UserModel>.Update.Pull(u => u.FavoriteCities, city);
//             await _users.UpdateOneAsync(u => u.Email == email, update);
//         }
//     }
// }
