using WRModel.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace WRModel.Models
{ 
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
     
        public string? Id { get; set; }
        
        [BsonElement("FullName")]
        public string FullName {get; set;} = string.Empty;

        [BsonElement("UserName")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Password")]
        public string Password { get; set; } = string.Empty;


        [BsonElement("FavoriteCities")]
        public List<string> FavoriteCities { get; set; } = new();
    }
}