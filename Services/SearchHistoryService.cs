using MongoDB.Driver;
using WRModel.Models;
using Microsoft.Extensions.Options;

namespace WSService.Services
{
    public class SearchHistoryService
    {
        private readonly IMongoCollection<SearchHistory> _searchHistoryCollection;

        public SearchHistoryService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _searchHistoryCollection = database.GetCollection<SearchHistory>("SearchHistory");
        }


        public async Task AddSearchAsync(SearchHistory search)
        {
            await _searchHistoryCollection.InsertOneAsync(search);
        }

        public async Task ClearSearchHistoryByEmailAsync(string email)
        {
            var filter = Builders<SearchHistory>.Filter.Eq(h => h.Email, email);
            await _searchHistoryCollection.DeleteManyAsync(filter);
        }


        public async Task<List<SearchHistory>> GetUserHistoryAsync(string email)
        {
            return await _searchHistoryCollection
                .Find(x => x.Email == email)
                .SortByDescending(x => x.SearchedAt)
                .Limit(10)
                .ToListAsync();
        }
    }
}
