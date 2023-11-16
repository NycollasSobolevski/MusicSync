using System.Linq.Expressions;
using music_api.Model;

namespace music_api;

public interface IRepository<T>
{
    Task<List<T>> Filter(Expression<Func<T, bool>> exp);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp);
    Task<T> add(T obj);
    Task Delete(T obj);
    Task Update(T obj);
    Task<T> Last(T obj);
    Task<bool> exists(T obj);
    int Count(Expression<Func<T, bool>> exp);
}