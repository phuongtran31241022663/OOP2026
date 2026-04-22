using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Domain.SharedKernel;

namespace Infrastructure.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        protected readonly string _filePath;
        protected List<T> _entities;

        public JsonRepository(string fileName)
        {
            // Xác định đường dẫn lưu file (ví dụ: data/students.json)
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            _filePath = Path.Combine(folder, fileName);
            _entities = new List<T>();
        }

        public async Task InitializeAsync()
        {
            if (File.Exists(_filePath))
            {
                string json = await Task.Run(() => File.ReadAllText(_filePath));
                // Deserialization: Chuyển chuỗi JSON từ file ngược thành List<T>
                _entities = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
        }

        public async Task SaveChangesAsync()
        {
            // Serialization: Chuyển List<T> thành chuỗi JSON để lưu xuống file
            string json = JsonConvert.SerializeObject(_entities, Formatting.Indented);
            await Task.Run(() => File.WriteAllText(_filePath, json));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            // Giả định T có thuộc tính Id. Trong thực tế, bạn nên dùng interface IEntity
            // Ở đây ta dùng 'dynamic' hoặc phản chiếu để minh họa nhanh
            return await Task.FromResult(_entities.FirstOrDefault(e => (Guid)((dynamic)e).Id == id));
        }

        public void Add(T entity) => _entities.Add(entity);

        public void Update(T entity)
        {
            // Với List trong bộ nhớ, đối tượng thường đã được cập nhật qua tham chiếu
            // nhưng ta có thể kiểm tra hoặc thay thế nếu cần.
        }

        public void Delete(T entity) => _entities.Remove(entity);
    }
}
