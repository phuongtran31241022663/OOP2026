using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
    }

    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        Task InitializeAsync();
        Task SaveChangesAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}