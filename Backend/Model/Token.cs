
using MongoDB.Bson.Serialization.Attributes;

namespace music_api.Model;

public partial class Token
{
    [BsonId]
    public string Id { get; set; }
    [BsonElement("User")]
    public string User { get; set; }
    [BsonElement("SpotifyToken")]
    public string SpotifyToken { get; set; }
    [BsonElement("YoutubeToken")]
    public string YoutubeToken { get; set; }
    [BsonElement("DeezerToken")]
    public string DeezerToken { get; set; }

}