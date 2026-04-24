using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Simple JSON file storage with in-memory list. Used by Presentation for DI.
    /// </summary>
    public class JsonStorage<T>
    {
        private readonly string _filePath;
        private readonly object _lock = new object();
        private List<T> _items = new List<T>();

        public JsonStorage(string fileName)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Loads data from JSON file into memory. Key selector currently unused.
        /// </summary>
        public Task InitializeAsync(Func<T, Guid> keySelector = null)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    if (File.Exists(_filePath))
                    {
                        var json = File.ReadAllText(_filePath);
                        _items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
                    }
                    else
                    {
                        _items = new List<T>();
                    }
                }
            });
        }

        /// <summary>
        /// Saves current in-memory list to JSON file.
        /// </summary>
        public Task SaveAsync()
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    var json = JsonConvert.SerializeObject(_items, Formatting.Indented);
                    File.WriteAllText(_filePath, json);
                }
            });
        }

        public List<T> Items => _items;
    }
}
