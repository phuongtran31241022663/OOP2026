using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    /// <summary>
    /// Base repository interface for common CRUD operations.
    /// Giao diện repository cơ sở cho các hoạt động CRUD chung.
    /// </summary>
    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        Task InitializeAsync();
        Task SaveChangesAsync();

        // Async operations
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);

        // Synchronous convenience operations (used by existing code)
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }

    /// <summary>
    /// Read-only repository interface for query operations.
    /// Giao diện repository chỉ đọc cho các hoạt động truy vấn.
    /// </summary>
    public interface IReadRepository<T> where T : class
    {
        // Async
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

        // Sync
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}
