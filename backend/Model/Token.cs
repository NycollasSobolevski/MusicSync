
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace music_api.Model;

public partial class Token
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("User")]
    public string User { get; set; }
    [BsonElement("Service")]
    public string Service { get; set; }
    [BsonElement("Token")]
    public string ServiceToken { get; set; }
    [BsonElement("RefreshToken")]
    public string RefreshToken { get; set; }
    [BsonElement("ExpiresIn")]
    public int ExpiresIn { get; set; }
    [BsonElement("LastUpdate")]
    public DateTime LastUpdate { get; set; }
}