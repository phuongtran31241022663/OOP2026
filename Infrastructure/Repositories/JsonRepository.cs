using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.SharedKernel;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly string _filePath;
        protected List<T> _items;

        public JsonRepository(string fileName)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            _filePath = Path.Combine(folder, fileName);
            _items = new List<T>();
        }

        public async Task InitializeAsync()
        {
            _items = await FileStorage.LoadAsync<T>(_filePath);
        }

        public async Task SaveChangesAsync()
        {
            await FileStorage.SaveAsync(_filePath, _items);
        }

        // Async
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_items);
        }

        public async Task AddAsync(T entity)
        {
            _items.Add(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            var index = _items.FindIndex(e => e.Id == entity.Id);
            if (index >= 0)
                _items[index] = entity;
            else
                _items.Add(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            _items.RemoveAll(e => e.Id == id);
            await Task.CompletedTask;
        }

        // Sync
        public T GetById(Guid id)
        {
            return _items.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public void Add(T entity)
        {
            _items.Add(entity);
        }

        public void Update(T entity)
        {
            var index = _items.FindIndex(e => e.Id == entity.Id);
            if (index >= 0)
                _items[index] = entity;
            else
                _items.Add(entity);
        }

        public void Delete(Guid id)
        {
            _items.RemoveAll(e => e.Id == id);
        }
    }
}
