using Domain.Repositories;
using Domain.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
﻿﻿﻿﻿// Infrastructure/Repositories/JsonRepository.cs

namespace Infrastructure.Repositories
{
    // tạm thời, hiện tại thì lưu trữ hoạt động tốt
    public class JsonRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly string _filePath;
        protected readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);
        protected List<T> _items;
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonRepository(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Tên file không được để trống.", nameof(fileName));

            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    System.Diagnostics.Debug.WriteLine($"[JsonRepository] Created directory: {folder}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[JsonRepository] ERROR creating directory {folder}: {ex.Message}");
                throw;
            }

            _filePath = Path.Combine(folder, fileName);
            _items = new List<T>();
            _serializerSettings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TypeNameHandling = TypeNameHandling.Auto
            };

            System.Diagnostics.Debug.WriteLine($"[JsonRepository] Initialized for file: {_filePath}");
        }


        private async Task LoadFromFileAsync()
        {
            await _fileLock.WaitAsync();
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = await Task.Run(() => File.ReadAllText(_filePath));
                    _items = JsonConvert.DeserializeObject<List<T>>(json, _serializerSettings) ?? new List<T>();



                }
                else
                {
                    _items = new List<T>();
                }
            }
            finally
            {
                _fileLock.Release();
            }
        }

        private async Task SaveToFileAsync()
        {
            await _fileLock.WaitAsync();
            try
            {
                string json = JsonConvert.SerializeObject(_items, Formatting.Indented, _serializerSettings);
                await Task.Run(() => File.WriteAllText(_filePath, json));
                System.Diagnostics.Debug.WriteLine($"[JsonRepository] Saved {_items.Count} items to {_filePath}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[JsonRepository] ERROR saving to {_filePath}: {ex.Message}");
                throw;
            }
            finally
            {
                _fileLock.Release();
            }
        }

        private bool _hasLoadedOnce = false;

        protected virtual async Task EnsureLoadedAsync()
        {
            if (!_hasLoadedOnce)
            {
                await LoadFromFileAsync();
                _hasLoadedOnce = true;
            }
        }

        public async Task InitializeAsync()
        {
            await LoadFromFileAsync();
            System.Diagnostics.Debug.WriteLine($"[JsonRepository] Initialized with {_items.Count} items from {_filePath}");
        }

        public async Task SaveChangesAsync()
        {
            await SaveToFileAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
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
                _fileLock.Release();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return new List<T>(_items);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await EnsureLoadedAsync();

            await _fileLock.WaitAsync();
            try
            {
                _items.Add(entity);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await EnsureLoadedAsync();

            await _fileLock.WaitAsync();
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
                {
                    _items.Add(entity);
                }
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id không hợp lệ.", nameof(id));

            await EnsureLoadedAsync();

            await _fileLock.WaitAsync();
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
                }
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}

