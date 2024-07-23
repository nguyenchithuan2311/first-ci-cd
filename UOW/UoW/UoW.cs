using Microsoft.EntityFrameworkCore;
using UOW.Repository;

namespace UOW.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (_repositories.TryGetValue(type, out var repository))
            {
                return (IRepository<T>)repository;
            }

            var newRepository = new Repository<T>(_context);
            _repositories[type] = newRepository;
            return newRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}