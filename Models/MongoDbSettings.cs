namespace WRModel.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = "";
        public string DatabaseName { get; set; } = "";
        public string UserCollection { get; set; } = "";
        public string SearchHistoryCollection { get; set; } = "";
    }
}