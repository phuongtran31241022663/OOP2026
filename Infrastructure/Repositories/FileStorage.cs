using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
    public static class FileStorage
    {
        private static readonly object _lock = new object();

        public static Task<List<T>> LoadAsync<T>(string filePath)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    if (!File.Exists(filePath))
                        return new List<T>();

                    var json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
                }
            });
        }

        public static Task SaveAsync<T>(string filePath, List<T> data)
        {
            return Task.Run(() =>
            {
                lock (_lock)
                {
                    var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                }
            });
        }
    }
}