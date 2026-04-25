using Domain.Repositories;
using Domain.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly string _filePath;
        private static readonly Mutex _fileMutex = new Mutex(false, "Global\\RideGo_JsonRepo_" + typeof(T).Name);
        private List<T> _items;

        public JsonRepository(string fileName)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, fileName);
            _items = new List<T>();
        }

        private async Task LoadFromFileAsync()
        {
            await Task.Run(() =>
            {
                _fileMutex.WaitOne();
                try
                {
                    if (File.Exists(_filePath))
                    {
                        string json = File.ReadAllText(_filePath);
                        _items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
                    }
                    else
                    {
                        _items = new List<T>();
                    }
                }
                finally
                {
                    _fileMutex.ReleaseMutex();
                }
            });
        }

        private async Task SaveToFileAsync()
        {
            await Task.Run(() =>
            {
                _fileMutex.WaitOne();
                try
                {
                    string json = JsonConvert.SerializeObject(_items, Formatting.Indented);
                    File.WriteAllText(_filePath, json);
                }
                finally
                {
                    _fileMutex.ReleaseMutex();
                }
            });
        }

        private async Task EnsureLoadedAsync()
        {
            if (_items == null || _items.Count == 0)
            {
                await LoadFromFileAsync();
            }
        }

        public async Task InitializeAsync()
        {
            await LoadFromFileAsync();
        }

        public async Task SaveChangesAsync()
        {
            await SaveToFileAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            await EnsureLoadedAsync();
            _fileMutex.WaitOne();
            try
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    T item = _items[i];
                    if (item.Id == id)
                        return item;
                }
                return null;
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            await EnsureLoadedAsync();
            _fileMutex.WaitOne();
            try
            {
                List<T> result = new List<T>();
                for (int i = 0; i < _items.Count; i++)
                {
                    result.Add(_items[i]);
                }
                return result;
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        public async Task AddAsync(T entity)
        {
            await EnsureLoadedAsync();
            _fileMutex.WaitOne();
            try
            {
                _items.Add(entity);
                await SaveToFileAsync();
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            await EnsureLoadedAsync();
            _fileMutex.WaitOne();
            try
            {
                bool found = false;
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Id == entity.Id)
                    {
                        _items[i] = entity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                    _items.Add(entity);
                await SaveToFileAsync();
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await EnsureLoadedAsync();
            _fileMutex.WaitOne();
            try
            {
                int indexToRemove = -1;
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Id == id)
                    {
                        indexToRemove = i;
                        break;
                    }
                }
                if (indexToRemove >= 0)
                {
                    _items.RemoveAt(indexToRemove);
                    await SaveToFileAsync();
                }
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }
    }
}