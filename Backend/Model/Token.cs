
using MongoDB.Bson.Serialization.Attributes;

namespace music_api.Model;

public partial class Token
{
    [BsonId]
    public string Id { get; set; }
    [BsonElement("User")]
    public string User { get; set; }
    [BsonElement("Streamer")]
    public string Streamer { get; set; }
    [BsonElement("Token")]
    public string StreamerToken { get; set; }
    [BsonElement("RefreshToken")]
    public string RefreshToken { get; set; }

}