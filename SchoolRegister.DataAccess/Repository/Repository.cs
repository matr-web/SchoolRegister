using Microsoft.EntityFrameworkCore;
using SchoolRegister.DataAccess;
using System.Linq.Expressions;

namespace SchoolRegister.DataAcces.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly SchoolRegisterContext context;
    private DbSet<T> dbSet; 

    public Repository(SchoolRegisterContext context)
    {
        this.context = context;
        this.dbSet = this.context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties != null)
        {
            foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate, string includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties != null)
        {
            foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Task.FromResult(dbSet.AddAsync(entity));
    }

    public async Task UpdateAsync(T entity)
    {
        await Task.FromResult(context.Entry(entity).State = EntityState.Modified);
    }

    public Task Remove(T entity)
    {
        dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
        return Task.CompletedTask;
    }
}
