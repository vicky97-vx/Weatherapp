using WRModel.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace WRModel.Models
{ 
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string FullName {get; set;} = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty; 

       

        [BsonElement("FavoriteCities")]
        public List<string> FavoriteCities { get; set; } = new();
    }
}
