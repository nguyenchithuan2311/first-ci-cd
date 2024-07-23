using Microsoft.EntityFrameworkCore;

namespace UOW.Repository;

public class Repository<T> : IRepository<T>
    where T : class
{
    private static DbContext Context{ get; set; }
    private readonly DbSet<T> _dbSet;
    public Repository(DbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    } 
    

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity, CancellationToken token)
    {
        await _dbSet.AddAsync(entity, token);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}