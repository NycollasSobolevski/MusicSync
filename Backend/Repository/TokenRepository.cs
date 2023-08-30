using System.Linq.Expressions;
using MongoDB.Driver;
using music_api.Model;

namespace music_api;

public class TokenRepository : IRepository<Token>
{
    private IMongoCollection<Token> context;
    public TokenRepository () 
    {
        string connString   = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        string databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
        
        DataBase.SetConnection(connString);
        DataBase.Connect();
        DataBase.mongoClient.GetDatabase("Token");
        this.context = DataBase.mongoClient
            .GetDatabase(databaseName)
            .GetCollection<Token>("Token");
    }

    public async Task add(Token obj)
    {
        await this.context.InsertOneAsync(obj);
    }

    public int Count(Expression<Func<Token, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Token obj)
    {
        System.Console.WriteLine(obj.ToString());
        await this.context.FindOneAndDeleteAsync<Token>(obj.Id);
    }

    public Task<bool> exists(Token obj)
    {
        throw new NotImplementedException();
    }

    public Task<List<Token>> Filter(Expression<Func<Token, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public Task<Token> FirstOrDefaultAsync(Expression<Func<User, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public Task<Token> Last(Token obj)
    {
        throw new NotImplementedException();
    }

    public async void Update(Token obj)
    {
        await this.context
            .UpdateOneAsync<Token>( 
                t => t.Id == obj.Id,
                Builders<Token>.Update.Set( 
                    t => t, obj
                )  
            );
    }
}