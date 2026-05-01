# Observer Pattern trong C#

## 1. Khái niệm và mục đích

**Observer Pattern** (mẫu quan sát) là một mẫu thiết kế hành vi, thiết lập cơ chế phụ thuộc **một-nhiều** giữa các đối tượng: khi một đối tượng (**Subject / Publisher**) thay đổi trạng thái, tất cả các đối tượng phụ thuộc vào nó (**Observer / Subscriber**) tự động được thông báo và cập nhật.

Trong C#, Observer Pattern được hỗ trợ qua hai cơ chế cốt lõi:

- `delegate` và `event` (C# 1.0).
- `IObserver<T>` / `IObservable<T>` (.NET Framework 4.0, Reactive Extensions).

---

## 2. Triển khai Observer cổ điển (GoF)

```csharp
public interface IObserver
{
    void Update(string message);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

public class NewsPublisher : ISubject
{
    private readonly List<IObserver> _observers = new();
    private string _latestNews;

    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var obs in _observers)
            obs.Update(_latestNews);
    }

    public void Publish(string news)
    {
        _latestNews = news;
        Notify();
    }
}

public class Subscriber : IObserver
{
    public string Name { get; set; }
    public void Update(string message) => Console.WriteLine($"{Name} received: {message}");
}
```

---

## 3. `delegate` và `event` — Cơ chế Observer tích hợp sẵn

### 3.1 Nguyên lý

- **Delegate**: đại diện cho một hoặc nhiều phương thức có cùng chữ ký.
- **Event**: thành viên của lớp, che giấu delegate bên trong, chỉ cho phép `+=` / `-=` từ bên ngoài.

### Ví dụ với `EventHandler<T>`

```csharp
public class Thermometer
{
    private decimal _temperature;

    public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

    public decimal Temperature
    {
        get => _temperature;
        set
        {
            if (_temperature != value)
            {
                _temperature = value;
                OnTemperatureChanged(new TemperatureChangedEventArgs(_temperature));
            }
        }
    }

    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e); // Thread-safe invocation
    }
}

public class TemperatureChangedEventArgs : EventArgs
{
    public decimal NewTemperature { get; }
    public TemperatureChangedEventArgs(decimal temp) => NewTemperature = temp;
}
```

```csharp
// Subscriber
var sensor = new Thermometer();
sensor.TemperatureChanged += (sender, args) =>
    Console.WriteLine($"Temp is now: {args.NewTemperature}°C");
```

### 3.2 Multicast delegate

Một event có thể có nhiều handler. Khi invoke, tất cả được gọi tuần tự theo thứ tự đăng ký. Nếu một handler ném ngoại lệ, các handler sau không được gọi. Dùng `Delegate.GetInvocationList()` để xử lý riêng từng handler.

### 3.3 Memory Leaks — Weak Event Pattern

> **Nguy hiểm:** Subject giữ tham chiếu mạnh đến Observer. Nếu quên `unsubscribe`, Observer sống mãi dù không cần nữa.

Giải pháp:
- Luôn `-=` trong `Dispose()` hoặc khi không cần.
- `WeakReference<Action>` để Subject không giữ Observer cứng.
- `CompositeDisposable` (Reactive Extensions) để hủy đăng ký hàng loạt.

---

## 4. `IObserver<T>` và `IObservable<T>` — Observer chuẩn hóa cho luồng dữ liệu

```csharp
public interface IObservable<T>
{
    IDisposable Subscribe(IObserver<T> observer);
}

public interface IObserver<T>
{
    void OnNext(T value);           // Dữ liệu tiếp theo
    void OnError(Exception error);  // Lỗi xảy ra
    void OnCompleted();             // Không còn dữ liệu
}
```

> `IObservable<T>` không phải sự kiện đơn lẻ — nó là **luồng các giá trị theo thời gian** (sequence), có thể hoàn thành hoặc gặp lỗi.

### 4.1 Triển khai thủ công

```csharp
public class DataStream : IObservable<int>
{
    private readonly List<IObserver<int>> _observers = new();

    public IDisposable Subscribe(IObserver<int> observer)
    {
        _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }

    public void Publish(int value)
    {
        foreach (var obs in _observers)
            obs.OnNext(value);
    }

    public void End()
    {
        foreach (var obs in _observers)
            obs.OnCompleted();
        _observers.Clear();
    }
}
```

### 4.2 `Subject<T>` từ Rx.NET

```csharp
var subject = new Subject<int>();

subject.Subscribe(
    onNext:      x  => Console.WriteLine($"Received {x}"),
    onError:     ex => Console.WriteLine($"Error: {ex.Message}"),
    onCompleted: () => Console.WriteLine("Done")
);

subject.OnNext(1);
subject.OnNext(2);
subject.OnCompleted();
```

### 4.3 Event vs `IObservable<T>`

| Tiêu chí | `event` (C# keyword) | `IObservable<T>` |
|---|---|---|
| Mục đích | Sự kiện rời rạc, không có kết thúc | Luồng dữ liệu tuần tự, có thể hoàn thành/lỗi |
| Ngữ nghĩa lỗi | Không có cơ chế tự nhiên | `OnError` rõ ràng |
| Hủy đăng ký | `-=` | `Dispose` trả về từ `Subscribe` |
| Biến đổi dữ liệu | LINQ không áp dụng trực tiếp | LINQ operators (Rx): filter, map, merge... |
| Bất đồng bộ | Phụ thuộc handler | Hỗ trợ scheduling, async |

---

## 5. Các biến thể nâng cao

### 5.1 Event Aggregator / Event Bus

Khi có nhiều publisher và subscriber phân tán, dùng trung tâm điều phối:

```csharp
public interface IEventAggregator
{
    void Subscribe<T>(Action<T> handler);
    void Publish<T>(T eventArgs);
}
```

### 5.2 Observer với async/await

Event handler thường là `void`, không hỗ trợ `await`. Dùng Rx:

```csharp
subject
    .SelectMany(async x =>
    {
        await Task.Delay(100);
        return x * 2;
    })
    .Subscribe(result => Console.WriteLine(result));
```

---

## 6. Ứng dụng thực tế trong .NET

| Tình huống | Cơ chế |
|---|---|
| WinForms button click | `event` + `EventHandler` |
| WPF data binding | `INotifyPropertyChanged` |
| File system monitoring | `FileSystemWatcher` |
| Real-time web | SignalR — Hub phát, Client là Observer |
| Reactive UI | ReactiveUI, Blazor `EventCallback` |

---

## 7. Lựa chọn triển khai phù hợp

| Kịch bản | Nên dùng |
|---|---|
| Nhiều handler biết sự kiện đơn giản | `event` + Delegate |
| Cần hủy đăng ký an toàn, tránh rò rỉ | `IObservable` + Rx hoặc WeakEvent |
| Chuỗi sự kiện cần lọc, kết hợp | Rx.NET |
| Giao tiếp module độc lập | Event Aggregator |
| Data binding WPF/MAUI | `INotifyPropertyChanged` |

---

## 8. Best Practices

1. Luôn dùng `?.Invoke` để tránh null check không an toàn với luồng.
2. Không quên `-=` trong `Dispose()` khi Observer có vòng đời ngắn hơn Subject.
3. Dùng `EventArgs` kế thừa `EventArgs` chuẩn và `EventHandler<T>` thay vì delegate tùy chỉnh.
4. `event` không hỗ trợ `async` trực tiếp — bọc `async void` và log ngoại lệ bên trong.
5. Khi số lượng Observer lớn, nghĩ đến Rx hoặc Mediator để phân tải.
6. Nếu event được raise từ background thread, cần `SynchronizationContext` hoặc `Dispatcher` để cập nhật UI an toàn.
7. Dùng `IObservable` cho stream có kết thúc — tài nguyên được giải phóng khi `OnCompleted`.
8. Chỉ dùng event cho "fire-and-forget" — không dùng cho request-response.

---

## 9. Ánh xạ vào RideGo System

| Thành phần RideGo | Pattern áp dụng | Ghi chú |
|---|---|---|
| `ITripService.TripStatusChanged` | Observer via Event | `EventHandler<TripStatusChangedEventArgs>` — UI Forms (PassengerShell, DriverShell) subscribe để nhận cập nhật real-time khi trip chuyển trạng thái. |
| `Trip` Domain Events | Observer (Publish-Subscribe) | `TripRequestedEvent`, `TripMatchedEvent`, etc. — Domain layer phát sự kiện, Infrastructure/BackgroundJobs subscribe xử lý. |
| `Driver` Status Events | Observer | `DriverStatusChangedEvent`, `DriverLocationUpdatedEvent` — UI cập nhật driver status real-time. |

**Ví dụ thực tế trong codebase:**
```csharp
// Application/Interfaces/ITripService.cs
event EventHandler<TripStatusChangedEventArgs> TripStatusChanged;

// Presentation/Shells/DriverShell.cs — Subscribe trong constructor
_tripService.TripStatusChanged += OnTripStatusChanged;

private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
{
    // Update UI when trip status changes
}
```

---

## 10. Kết luận

Observer Pattern trong C# đã được nâng cấp từ mẫu thủ công thành một phần tự nhiên của ngôn ngữ. Với `delegate/event`, gắn kết các thành phần một cách lỏng lẻo và hiệu quả. Khi cần luồng dữ liệu phức tạp, `IObservable<T>` và Reactive Extensions mở rộng mạnh mẽ, cho phép lập trình phản ứng với operators chuẩn hóa và kiểm soát lỗi tinh tế.
