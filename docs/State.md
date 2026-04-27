# State Pattern trong C#

## 1. Khái niệm và động lực

**State Pattern** là một mẫu thiết kế hành vi (Behavioral Pattern) cho phép một đối tượng thay đổi hành vi khi trạng thái bên trong của nó thay đổi. Nhìn từ bên ngoài, dường như đối tượng đó đã thay đổi lớp.

**Bài toán điển hình:** một đối tượng có một tập hữu hạn các trạng thái, mỗi trạng thái ứng xử khác nhau với cùng một sự kiện. Nếu dùng `switch-case` hay `if-else`, mã nguồn sẽ:

- Bị trùng lặp logic kiểm tra trạng thái ở nhiều phương thức.
- Vi phạm nguyên tắc **Open/Closed** — mỗi khi thêm trạng thái mới phải sửa tất cả các `switch`.
- Khó kiểm thử từng trạng thái độc lập.

State Pattern giải quyết bằng cách đóng gói hành vi của mỗi trạng thái vào một lớp riêng, và ủy thác hành vi từ context hiện tại sang đối tượng trạng thái.

---

## 2. Cấu trúc cổ điển trong C#

Mẫu gồm ba thành phần chính:

- **Context**: Lớp client quan tâm, duy trì một tham chiếu đến State hiện tại. Gọi `state.Handle(this)`.
- **State**: Abstract class hoặc interface khai báo các hành vi phụ thuộc trạng thái.
- **ConcreteState**: Các lớp cụ thể triển khai từng trạng thái. Tự quyết định chuyển đổi bằng `context.State = new OtherState()`.

### Ví dụ: Máy bán vé tự động

```csharp
// Interface trạng thái
public interface IState
{
    void InsertCoin();
    void SelectTicket();
    void Cancel();
}

// Context
public class TicketMachine
{
    private IState _currentState;

    public TicketMachine()
    {
        _currentState = new WaitingForCoinState(this);
    }

    public IState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public void InsertCoin()    => _currentState.InsertCoin();
    public void SelectTicket()  => _currentState.SelectTicket();
    public void Cancel()        => _currentState.Cancel();
}

// ConcreteState: Chờ đồng xu
public class WaitingForCoinState : IState
{
    private readonly TicketMachine _machine;

    public WaitingForCoinState(TicketMachine machine) => _machine = machine;

    public void InsertCoin()
    {
        Console.WriteLine("Coin accepted.");
        _machine.CurrentState = new TicketSelectedState(_machine);
    }

    public void SelectTicket() => Console.WriteLine("Insert a coin first.");
    public void Cancel()       => Console.WriteLine("No coin to return.");
}

// ConcreteState: Đã có đồng xu
public class TicketSelectedState : IState
{
    private readonly TicketMachine _machine;

    public TicketSelectedState(TicketMachine machine) => _machine = machine;

    public void InsertCoin() => Console.WriteLine("Coin already inserted.");

    public void SelectTicket()
    {
        Console.WriteLine("Printing ticket...");
        _machine.CurrentState = new WaitingForCoinState(_machine);
    }

    public void Cancel()
    {
        Console.WriteLine("Refunding coin...");
        _machine.CurrentState = new WaitingForCoinState(_machine);
    }
}
```

> **Kết quả:** Logic `switch(state)` biến mất khỏi `TicketMachine`. Mỗi trạng thái đóng gói hành vi của nó và tự quản lý chuyển đổi.

---

## 3. Các biến thể nâng cao

### 3.1 State Machine phân cấp (Hierarchical State)

Khi trạng thái có các trạng thái con (ví dụ: "Đang hoạt động" bao gồm "Chạy", "Đi bộ"), áp dụng **Composite + State**. Có thể dùng thư viện `Stateless`:

```csharp
var machine = new StateMachine<State, Trigger>(State.Idle);

machine.Configure(State.Idle)
    .Permit(Trigger.Start, State.Active);

machine.Configure(State.Active)
    .Permit(Trigger.Stop, State.Idle)
    .OnEntry(() => Console.WriteLine("Machine active"));
```

### 3.2 State và Dependency Injection

Khi các state cần service bên ngoài (logging, repository), khởi tạo `new ConcreteState()` trực tiếp phá vỡ DI. Giải pháp:

- Dùng `IStateFactory` inject vào context.
- Đăng ký states là `Transient`, context nhận `IServiceProvider` để resolve khi chuyển trạng thái.
- **Event-driven state**: context phát sự kiện, các state tự đăng ký.

### 3.3 Thread Safety

Chuyển đổi state không nguyên tử có thể gây race condition. Giải pháp:

- `Interlocked.Exchange` nếu state là reference type.
- `ImmutableState` + context bất biến (với `record`).
- `lock` — cẩn thận với deadlock và hiệu năng.

---

## 4. So sánh Strategy vs State

| Tiêu chí | Strategy | State |
|---|---|---|
| Ai quyết định thay đổi? | Client (bên ngoài) | Bản thân trạng thái (bên trong) |
| Các lớp có biết nhau? | Không | ConcreteState biết về State khác |
| Mục đích | Hoán đổi thuật toán | Quản lý vòng đời và hành vi theo trạng thái |
| Triển khai C# | Thường dùng `delegate` / `Action` | Luôn dùng lớp riêng biệt |

---

## 5. Serialization State

Khi serialize context, cần cả trạng thái hiện tại. Dùng `[JsonDerivedType]` cho State abstract để deserialize đúng kiểu:

```csharp
[JsonDerivedType(typeof(WaitingForCoinState), "waiting")]
[JsonDerivedType(typeof(TicketSelectedState), "selected")]
public abstract class StateBase { }
```

---

## 6. Ứng dụng thực tế

| Hệ thống | Trạng thái |
|---|---|
| Vòng đời đơn hàng | New → Confirmed → Shipped → Delivered → Cancelled |
| Trình phát nhạc | Playing → Paused → Stopped |
| UI Navigation / Wizard | Các bước đăng ký, thanh toán |
| Game | Trạng thái nhân vật: Idle, Running, Attacking, Dead |
| C# `async/await` compiler | `IAsyncStateMachine` — State Pattern được dùng trong ngôn ngữ |
| **Đời sống chuyến đi (Trip)** | Requested → Searching → Matched → Arrived → Started → Completed / Cancelled / Timeout |
| **Trạng thái tài xế (Driver)** | Offline → Available → OnTrip → Offline/Available |

---

## 7. Lưu ý khi triển khai

- **Phạm vi áp dụng**: Nếu chỉ có 2–3 trạng thái và ít hành vi, `enum + switch` tránh quá tải kỹ thuật. State Pattern phát huy khi số lượng trạng thái và hành vi tăng.
- **Quản lý chuyển đổi tập trung**: Nên tập trung logic chuyển đổi vào context hoặc một transition table thay vì để mỗi state tự gán `context.State` — tránh coupling chằng chịt.
- **Kiểm thử**: Mỗi `ConcreteState` dễ dàng unit test độc lập. Có thể mock context hoặc chỉ kiểm tra hành vi khi nhận sự kiện.

---

## 8. Kết luận

State Pattern là công cụ mạnh mẽ để thay thế các khối điều kiện phức tạp bằng một tập các lớp có trách nhiệm rõ ràng, tuân thủ **Single Responsibility** và **Open/Closed**. Trong C#, pattern này hòa quyện với `record`, pattern matching và default interface methods để tạo ra code sạch, bất biến, an toàn luồng và dễ kiểm thử.
