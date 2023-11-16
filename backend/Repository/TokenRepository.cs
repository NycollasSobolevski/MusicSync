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
        try
        {
            
            DataBase.SetConnection(connString);
            DataBase.Connect();
            DataBase.mongoClient.GetDatabase("Token");
            this.context = DataBase.mongoClient
                .GetDatabase(databaseName)
                .GetCollection<Token>("Token");
        }
        catch (Exception exp)
        {
            System.Console.WriteLine(exp);
        }
    }

    public async Task<Token> add(Token obj)
    {
        System.Console.WriteLine("Add token");
        await this.context.InsertOneAsync(obj);
        return obj;
    }

    public int Count(Expression<Func<Token, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Token obj)
    {
        System.Console.WriteLine(obj.ToString());
        await this.context.FindOneAndDeleteAsync<Token>(t => 
            t.Id == obj.Id
        );
    }
    

    public Task<bool> exists(Token obj)
    {
        throw new NotImplementedException();
    }

    public Task<List<Token>> Filter(Expression<Func<Token, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public async Task<Token> FirstOrDefaultAsync(Expression<Func<Token, bool>> exp)
    {
        var token = await context.FindAsync(exp);
        var list = token.ToList();
        return list.FirstOrDefault();
    }

    public Task<Token> Last(Token obj)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Token obj)
    {
        await this.context
            .UpdateOneAsync<Token>( 
                t => t.Id == obj.Id,
                Builders<Token>.Update
                    .Set(t => t.ServiceToken, obj.ServiceToken)
                    .Set(t => t.RefreshToken, obj.RefreshToken)
                    .Set(t => t.LastUpdate, obj.LastUpdate)
                    .Set(t => t.ExpiresIn, obj.ExpiresIn)
            );
    }
}