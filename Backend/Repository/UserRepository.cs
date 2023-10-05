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
    private IMongoCollection<User> context;
    public UserRepository () 
    {
        string connString   = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        string databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
        DataBase.SetConnection(connString);
        DataBase.Connect();
        DataBase.mongoClient.GetDatabase("User");
        this.context = DataBase.mongoClient
            .GetDatabase(databaseName)
            .GetCollection<User>("User");
    }

    public async Task<User> add(User obj)
    {
        await this.context.InsertOneAsync(obj);
        return obj;
    }

    public int Count(Expression<Func<User, bool>> exp)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(User obj)
    {
        System.Console.WriteLine(obj.ToString());
        await this.context.FindOneAndDeleteAsync<User>(obj.Name);
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
        var bsonUser = await context.FindAsync(exp);
        var list = bsonUser.ToList();
        return list.FirstOrDefault();
    }

    public Task<User> Last(User obj)
    {
        throw new NotImplementedException();
    }

    public async Task Update(User obj)
    {
        var updated = await context.ReplaceOneAsync(
            Builders<User>.Filter.Eq(u => u.Id, obj.Id),
            obj
        );
    }
}