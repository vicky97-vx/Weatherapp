using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WRModel.Models;

namespace WRModel.Models
{
    public class SearchHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Email { get; set; } = "";
        public string City { get; set; } = "";
        public DateTime SearchedAt { get; set; } = DateTime.UtcNow;
        public string WeatherSummary { get; set; } 
    }
}
