using MongoDB.Driver;
using MongoDB.Bson;

namespace music_api.Model;

public class DataBase
{
    private string stringConnection { get; set; } = "";
    public MongoClient mongoClient { get; private set; }
    public void SetConnection ( string newStringConnection) 
    {
        stringConnection = newStringConnection;
    }
    public void Connect () 
    {
        var settings = MongoClientSettings.FromConnectionString(stringConnection);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        mongoClient = new MongoClient(settings);

        mongoClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

        System.Console.WriteLine("Database Connected");
    }
    
    public void SetDatabase ( string database ) 
    {
        mongoClient.GetDatabase(database);
    }
}