using MongoDB.Driver;
using WRModel.Models;
using Microsoft.Extensions.Options;

namespace WSService.Services
{
    public class SearchHistoryService
    {
        private readonly IMongoCollection<SearchHistory> _historyCollection;

        public SearchHistoryService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _historyCollection = database.GetCollection<SearchHistory>("SearchHistory");
        }

        public async Task AddSearchAsync(SearchHistory search)
        {
            await _historyCollection.InsertOneAsync(search);
        }

        public async Task<List<SearchHistory>> GetUserHistoryAsync(string email)
        {
            return await _historyCollection
                .Find(x => x.Email == email)
                .SortByDescending(x => x.SearchedAt)
                .Limit(10)
                .ToListAsync();
        }
    }
}
