using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace music_api.Model;

public partial class User
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
    [BsonElement("Birth")]
    public DateTime Birth { get; set; }
    [BsonElement("Email")]
    public string Email { get; set; }
    [BsonElement("Password")]
    public string Password { get; set; }
    [BsonElement("Salt")]
    public string Salt { get; set; }
    [BsonElement("EmailConfirmed")]
    public bool EmailConfirmed { get; set; }
    [BsonElement("IsActive")]
    public bool IsActive { get; set; }
}