using MongoDB.Driver;
using MongoDB.Bson;

namespace music_api.Model;

public static class DataBase
{
    private static string stringConnection { get; set; } = "";
    public static MongoClient mongoClient { get; private set; }

    public static void SetConnection ( string newStringConnection) 
    {
        stringConnection = newStringConnection;
    }
    public static void Connect () 
    {
        var settings = MongoClientSettings.FromConnectionString(stringConnection);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        mongoClient = new MongoClient(settings);

        mongoClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

        System.Console.WriteLine("Database Connected");
    }
    
    public static void SetDatabase ( string database ) 
    {
        mongoClient.GetDatabase(database);
    }
}