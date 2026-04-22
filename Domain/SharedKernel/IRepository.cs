using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.SharedKernel
{
    /// <summary>
    /// Base repository interface for common CRUD operations.
    /// Giao diện repository cơ sở cho các hoạt động CRUD chung.
    /// </summary>
    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        Task InitializeAsync();
        Task SaveChangesAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    /// <summary>
    /// Read-only repository interface for query operations.
    /// Giao diện repository chỉ đọc cho các hoạt động truy vấn.
    /// </summary>
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
