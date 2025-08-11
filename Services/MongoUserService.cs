using MongoDB.Driver;
using System.Text.Json;
using WRModel.Models;
using Blazored.SessionStorage;


namespace WSService.Services
{
    public class MongoUserService
    {
        private readonly IMongoCollection<UserModel> _usersCollection;
        private readonly ISessionStorageService _sessionStorage;

        public MongoUserService(ISessionStorageService sessionStorage, IConfiguration configuration)
        {
            _sessionStorage = sessionStorage;

            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _usersCollection = database.GetCollection<UserModel>(configuration["MongoDbSettings:UserCollection"]);
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
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task <UserModel?> LoginAsync(string email, string password)
        {
            return await _usersCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task AddFavoriteAsync(string email, string city)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var update = Builders<UserModel>.Update.AddToSet(u => u.FavoriteCities, city);
            var result = await _usersCollection.UpdateOneAsync(filter, update); 
        }


        //   public async Task RemoveFavoriteAsync(string email, string city)
        // {
        //     city = city.Trim().ToLower();
        //     var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
        //     var update = Builders<UserModel>.Update.Pull(u => u.FavoriteCities, city);
        //     var result = await _usersCollection.UpdateOneAsync(filter, update);

        // }

        public async Task<List<string>> GetFavoriteCitiesAsync(string email)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var user = await _usersCollection.Find(filter).FirstOrDefaultAsync();
            return user?.FavoriteCities ?? new List<string>();
        }
        public async Task<bool> RemoveFavoriteCityAsync(string email, string city)
        {
            // city = city.Trim().ToLower();
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var update = Builders<UserModel>.Update.Pull(u => u.FavoriteCities, city);
            var result = await _usersCollection.UpdateOneAsync(filter, update);
            Console.WriteLine($"Tried removing: {city}, ModifiedCount: {result.ModifiedCount}");
    
            return result.ModifiedCount > 0;
        }


      
        
        public async Task<bool> ChangePasswordAsync(string email, string newPassword)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var update = Builders<UserModel>.Update.Set(u => u.Password, newPassword);
            var result = await _usersCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateUserAsync(UserModel User)
        {
            Console.WriteLine($"[DEBUG] Updating user: {User.FullName}, Username: {User.UserName}, Email: {User.Email}");
            
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, User.Email);
            var update = Builders<UserModel>.Update
                .Set(u => u.FullName, User.FullName)
                .Set(u => u.UserName, User.UserName)
                .Set(u => u.Email, User.Email);

            var result = await _usersCollection.UpdateOneAsync(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }


        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _usersCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
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