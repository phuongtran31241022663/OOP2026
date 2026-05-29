using System.Reflection;
using System.Text;
using System.Text.Json;

namespace OOP2026
{
    /// <summary>
    /// Repository Pattern – Generic Repository lưu trữ dữ liệu dưới dạng JSON.
    /// Đảm bảo an toàn luồng tuyệt đối (Thread-safe) và bất đồng bộ không chặn UI.
    /// </summary>
    public class JsonRepository<T> : IJsonRepository<T>, IDisposable where T : class
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        protected readonly string _filePath;
        protected readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);
        protected List<T> _items = new List<T>();
        private bool _disposed;
        private bool _isLoaded;

        // Lưu bộ nhớ đệm PropertyInfo của Id để tránh quét Reflection lặp đi lặp lại qua mỗi lượt gọi hàm
        private static readonly PropertyInfo? IdPropertyCache = typeof(T).GetProperties()
            .FirstOrDefault(prop => prop.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                    prop.Name.Equals(typeof(T).Name + "Id", StringComparison.OrdinalIgnoreCase));

        public JsonRepository(string fileName)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Đảm bảo dữ liệu được nạp từ file lên bộ nhớ một lần duy nhất (Dành cho mọi phiên bản .NET)
        /// </summary>
        protected async Task EnsureLoadedAsync()
        {
            if (_isLoaded) return;

            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_isLoaded) return;

                if (File.Exists(_filePath))
                {
                    string json;
                    using (StreamReader reader = new StreamReader(_filePath, Encoding.UTF8))
                    {
                        json = await reader.ReadToEndAsync().ConfigureAwait(false);
                    }

                    _items = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
                }
                _isLoaded = true;
            }
            finally
            {
                _fileLock.Release();
            }
        }
        /// <summary>
        /// Ghi file bất đồng bộ an toàn không gây nghẽn UI WinForms
        /// </summary>
        private async Task SaveInternalAsync()
        {
            string json = JsonSerializer.Serialize(_items, _jsonOptions);

            // Giải pháp tương thích ngược cho .NET Framework bằng StreamWriter
            using (StreamWriter writer = new StreamWriter(_filePath, false, Encoding.UTF8))
            {
                await writer.WriteAsync(json).ConfigureAwait(false);
            }
        }
        public async Task<List<T>> ReadAsync()
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                List<T> copy = new List<T>();
                foreach (T item in _items)
                {
                    if (item != null)
                    {
                        copy.Add(item);
                    }
                }
                return copy;
            }
            finally { _fileLock.Release(); }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            if (IdPropertyCache == null)
                throw new InvalidOperationException("Thực thể " + typeof(T).Name + " không có thuộc tính định danh Id hợp lệ.");

            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                foreach (T item in _items)
                {
                    if (item != null)
                    {
                        object? val = IdPropertyCache.GetValue(item);
                        if (val is Guid && (Guid)val == id)
                        {
                            return item;
                        }
                    }
                }
                return null;
            }
            finally { _fileLock.Release(); }
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                _items.Add(entity);
                await SaveInternalAsync().ConfigureAwait(false);
            }
            finally { _fileLock.Release(); }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (IdPropertyCache == null)
                throw new InvalidOperationException("Không tìm thấy cấu hình Id của " + typeof(T).Name);

            object? entityIdObj = IdPropertyCache.GetValue(entity);
            if (entityIdObj is Guid == false)
                throw new InvalidOperationException("Giá trị Id của thực thể truyền vào không hợp lệ.");
            Guid entityId = (Guid)entityIdObj;

            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                int index = -1;
                for (int i = 0; i < _items.Count; i++)
                {
                    T item = _items[i];
                    if (item != null)
                    {
                        object? itemIdObj = IdPropertyCache.GetValue(item);
                        if (itemIdObj is Guid && (Guid)itemIdObj == entityId)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                if (index < 0)
                    throw new KeyNotFoundException("Thực thể " + typeof(T).Name + " với Id " + entityId + " không tồn tại trong hệ thống.");

                _items[index] = entity;
                await SaveInternalAsync().ConfigureAwait(false);
            }
            finally { _fileLock.Release(); }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (IdPropertyCache == null)
                throw new InvalidOperationException("Không tìm thấy cấu hình Id của " + typeof(T).Name);

            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                bool removed = false;
                for (int i = _items.Count - 1; i >= 0; i--)
                {
                    T item = _items[i];
                    if (item != null)
                    {
                        object? val = IdPropertyCache.GetValue(item);
                        if (val is Guid && (Guid)val == id)
                        {
                            _items.RemoveAt(i);
                            removed = true;
                        }
                    }
                }

                if (removed)
                {
                    await SaveInternalAsync().ConfigureAwait(false);
                }
            }
            finally { _fileLock.Release(); }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _fileLock?.Dispose();
            _items?.Clear();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~JsonRepository() { Dispose(); }
    }

    #region Các Lớp Con Kế Thợp Lệ
    public class PolRepo : IPolRepo
    {
        private readonly JsonRepository<Pol> _inner;

        public PolRepo()
        {
            _inner = new JsonRepository<Pol>("policy.json");
        }

        public Task<List<Pol>> ReadAsync() { return _inner.ReadAsync(); }
        public Task<Pol?> GetByIdAsync(Guid id) { return _inner.GetByIdAsync(id); }

        public async Task<Pol?> GetLatestByVehicleTypeAsync(VehicleType vehicleType)
        {
            List<Pol> all = await _inner.ReadAsync().ConfigureAwait(false);
            // Do yêu cầu đồ án không dùng LINQ nâng cao, viết cụ thể kiểm tra điều kiện dữ liệu
            Pol? latest = null;
            foreach (Pol p in all)
            {
                if (p.VehicleType == vehicleType)
                {
                    if (latest == null)
                    {
                        latest = p;
                    }
                    else if (p.CreatedAt > latest.CreatedAt)
                    {
                        latest = p;
                    }
                }
            }
            return latest;
        }

        public Task CreateAsync(Pol entity) { return _inner.CreateAsync(entity); }
    }

    public class RevRepo : JsonRepository<Rev>, IRevRepo
    {
        public RevRepo() : base("reviews.json") { }

        public async Task<List<Rev>> GetByDriverIdAsync(Guid driverId)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                List<Rev> result = new List<Rev>();
                foreach (Rev r in _items)
                {
                    if (r != null)
                    {
                        if (r.DriverId == driverId)
                        {
                            result.Add(r);
                        }
                    }
                }
                return result;
            }
            finally { _fileLock.Release(); }
        }

        public async Task<List<Rev>> GetByTripIdAsync(Guid tripId)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                List<Rev> result = new List<Rev>();
                foreach (Rev r in _items)
                {
                    if (r != null)
                    {
                        if (r.TripId == tripId)
                        {
                            result.Add(r);
                        }
                    }
                }
                return result;
            }
            finally { _fileLock.Release(); }
        }

        public async Task<List<Rev>> GetByPassengerIdAsync(Guid passengerId)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                List<Rev> result = new List<Rev>();
                foreach (Rev r in _items)
                {
                    if (r != null)
                    {
                        if (r.PassengerId == passengerId)
                        {
                            result.Add(r);
                        }
                    }
                }
                return result;
            }
            finally { _fileLock.Release(); }
        }
    }

    public class TripRepo : JsonRepository<Trip>, ITripRepo
    {
        public TripRepo() : base("trips.json") { }

        public async Task<List<Trip>> GetByDriverIdAsync(Guid driverId)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                List<Trip> result = new List<Trip>();
                foreach (Trip t in _items)
                {
                    if (t != null)
                    {
                        if (t.DriverId == driverId)
                        {
                            result.Add(t);
                        }
                    }
                }
                return result;
            }
            finally { _fileLock.Release(); }
        }

        public async Task<List<Trip>> GetByPassengerIdAsync(Guid passengerId)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                List<Trip> result = new List<Trip>();
                foreach (Trip t in _items)
                {
                    if (t != null)
                    {
                        if (t.PassengerId == passengerId)
                        {
                            result.Add(t);
                        }
                    }
                }
                return result;
            }
            finally { _fileLock.Release(); }
        }
    }

    public class UsrRepo : JsonRepository<Usr>, IUsrRepo
    {
        public UsrRepo() : base("users.json") { }

        public async Task<Usr?> GetByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                foreach (Usr u in _items)
                {
                    if (u != null)
                    {
                        if (u.Phone == phone)
                        {
                            return u;
                        }
                    }
                }
                return null;
            }
            finally { _fileLock.Release(); }
        }

        public async Task<Psg?> GetPassengerByIdAsync(Guid id)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                foreach (Usr u in _items)
                {
                    Psg? p = u as Psg;
                    if (p != null)
                    {
                        if (p.Id == id)
                        {
                            return p;
                        }
                    }
                }
                return null;
            }
            finally { _fileLock.Release(); }
        }

        public async Task<Drv?> GetDriverByIdAsync(Guid id)
        {
            await EnsureLoadedAsync().ConfigureAwait(false);
            await _fileLock.WaitAsync().ConfigureAwait(false);
            try
            {
                foreach (Usr u in _items)
                {
                    Drv? d = u as Drv;
                    if (d != null)
                    {
                        if (d.Id == id)
                        {
                            return d;
                        }
                    }
                }
                return null;
            }
            finally { _fileLock.Release(); }
        }
    }

    public class VehRepo : IVehRepo
    {
        private readonly JsonRepository<Veh> _inner;

        public VehRepo()
        {
            _inner = new JsonRepository<Veh>("vehicles.json");
        }

        public Task<List<Veh>> ReadAsync() { return _inner.ReadAsync(); }
        public Task<Veh?> GetByIdAsync(Guid id) { return _inner.GetByIdAsync(id); }
        public Task CreateAsync(Veh entity) { return _inner.CreateAsync(entity); }
        public Task UpdateAsync(Veh entity) { return _inner.UpdateAsync(entity); }
        public Task DeleteAsync(Guid id) { return _inner.DeleteAsync(id); }
    }
    #endregion
}
