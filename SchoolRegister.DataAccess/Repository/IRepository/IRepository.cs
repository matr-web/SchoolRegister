using System.Linq.Expressions;

namespace SchoolRegister.DataAcces.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string includeProperties = null);
    Task<T> GetByAsync(Expression<Func<T, bool>> predicate, string includeProperties = null);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task Remove(T entity);
    Task RemoveRange(IEnumerable<T> entity);
}
