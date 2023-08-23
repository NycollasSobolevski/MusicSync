using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace music_api;

using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using music_api.Model;

public class UserRepository : IRepository<User>
{
    private DataBase dataBase;
    private IMongoCollection<BsonDocument> context;
    public UserRepository () 
    {
        string connString   = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        string databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
        this.dataBase.SetConnection(connString);
        this.dataBase.Connect();
        this.dataBase.SetDatabase("User");
        this.context = dataBase.mongoClient
            .GetDatabase(databaseName)
            .GetCollection<BsonDocument>("User");
    }

    public async Task add(User obj)
    {
        await this.context.InsertOneAsync(new BsonDocument{
            {"Name", obj.Name},
            {"Birth", obj.Birth},
            {"Email", obj.Email},
            {"Password", obj.Password},
            {"Salt", obj.Salt}
        });
    }

    public int Count(Expression<Func<User, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public void Delete(User obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> exists(User obj)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> Filter(Expression<Func<User, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> exp)
    {
        //TODO: 
        throw new NotImplementedException();
    }

    public Task<User> Last(User obj)
    {
        throw new NotImplementedException();
    }

    public void Update(User obj)
    {
        throw new NotImplementedException();
    }
}