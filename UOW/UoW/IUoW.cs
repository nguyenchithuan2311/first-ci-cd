using UOW.Repository;

namespace UOW.UoW;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> CompleteAsync();
    }
