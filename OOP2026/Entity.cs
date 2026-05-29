using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace OOP2026
{
    #region Usr & các lớp con
    [JsonDerivedType(typeof(Drv), typeDiscriminator: "driver")]
    [JsonDerivedType(typeof(Psg), typeDiscriminator: "passenger")]
    [JsonDerivedType(typeof(Adm), typeDiscriminator: "admin")]
    public abstract class Usr
    {
        private Guid _id;
        private string _name = string.Empty;
        private string _phone = string.Empty;
        private string _password = string.Empty;

        public Guid Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tên không được để trống.", nameof(value));
                }
                _name = value.Trim();
            }
        }
        public string Phone
        {
            get { return _phone; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Số điện thoại không được để trống.", nameof(value));
                }
                string trimmed = value.Trim();
                if (Regex.IsMatch(trimmed, @"^0[0-9]{9}$") == false)
                {
                    throw new ArgumentException("Số điện thoại không đúng định dạng (10 số, bắt đầu bằng 0).", nameof(value));
                }
                _phone = trimmed;
            }
        }
        public string Pwd
        {
            get { return _password; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Mật khẩu không được để trống.", nameof(value));
                }
                if (value.Length < 6)
                {
                    throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự.", nameof(value));
                }
                _password = value;
            }
        }

        // Constructor không tham số
        protected Usr()
        {
            _id = Guid.NewGuid();
        }

        protected Usr(string name, string phone, string password)
        {
            _id = Guid.NewGuid();
            Name = name;
            Phone = phone;
            Pwd = password;
        }

        [JsonConstructor]
        protected Usr(Guid id, string name, string phone, string password)
        {
            _id = id;
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (phone == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            _name = name;
            _phone = phone;
            _password = password;
        }

        public void ChangePassword(string oldRaw, string newRaw)
        {
            if (string.IsNullOrWhiteSpace(oldRaw))
                throw new ArgumentException("Mật khẩu cũ không được để trống.", nameof(oldRaw));
            if (!VerifyPassword(oldRaw))
                throw new ArgumentException("Sai mật khẩu cũ.", nameof(oldRaw));
            if (oldRaw == newRaw)
                throw new ArgumentException("Mật khẩu mới không được trùng mật khẩu cũ.", nameof(newRaw));
            Pwd = newRaw;
        }

        public virtual void UpdateProfile(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public bool VerifyPassword(string rawInput)
        {
            return rawInput == _password;
        }
    }
    public class Drv : Usr
    {
        [field: JsonIgnore]
        public event EventHandler<DriverStatusChangedEventArgs>? StatusChanged;

        private readonly object _stateLock = new object();
        private IDriverState _currentState;
        private DriverStatus _status;

        // ---------- Các field dữ liệu ----------
        private string _licenseNumber = string.Empty;
        private Guid _vehicleId;
        private Loc _position;
        private decimal _wallet;
        private decimal _income;
        private int _totalTrips;
        private int _ratingSum;
        private int _totalReviews;

        public string LicNo
        {
            get { return _licenseNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Số GPLX không được để trống.", nameof(value));
                _licenseNumber = value.Trim();
            }
        }

        public Guid VehId
        {
            get { return _vehicleId; }
            private set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("ID phương tiện không hợp lệ.", nameof(value));
                _vehicleId = value;
            }
        }

        public Loc Position
        {
            get { return _position; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _position = value;
            }
        }

        public decimal Wallet
        {
            get { return _wallet; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _wallet = value;
            }
        }

        public decimal Income
        {
            get { return _income; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _income = value;
            }
        }

        public int TotTrip
        {
            get { return _totalTrips; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _totalTrips = value;
            }
        }

        public int RatingSum
        {
            get { return _ratingSum; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _ratingSum = value;
            }
        }

        public int TotalReviews
        {
            get { return _totalReviews; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _totalReviews = value;
            }
        }

        [JsonIgnore]
        public decimal AvgRat
        {
            get
            {
                if (TotalReviews == 0)
                {
                    return 0;
                }
                return Math.Round((decimal)RatingSum / TotalReviews, 2);
            }
        }

        public DriverStatus Status
        {
            get
            {
                lock (_stateLock)
                {
                    return _status;
                }
            }
        }

        [JsonIgnore]
        public IDriverState State
        {
            get
            {
                lock (_stateLock) return _currentState;
            }
            private set
            {
                lock (_stateLock)
                {
                    _currentState = value;
                    _status = value.Status;
                }
            }
        }

        // Constructor không tham số
        public Drv() : base()
        {
            _vehicleId = Guid.Empty;
            _position = new Loc();
            _wallet = 0;
            _income = 0;
            _totalTrips = 0;
            _ratingSum = 0;
            _totalReviews = 0;
            _currentState = DriverStateFactory.Create(DriverStatus.Offline);
            _status = _currentState.Status;
        }

        public Drv(string name, string phone, string password, string licenseNumber, Guid vehicleId, Loc position)
            : base(name, phone, password)
        {
            if (licenseNumber == null)
            {
                throw new ArgumentNullException(nameof(licenseNumber));
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            _licenseNumber = licenseNumber.Trim();
            _vehicleId = vehicleId;
            _position = position;
            _wallet = 0;
            _income = 0;
            _totalTrips = 0;
            _ratingSum = 0;
            _totalReviews = 0;
            _currentState = DriverStateFactory.Create(DriverStatus.Offline);
            _status = _currentState.Status;
        }

        [JsonConstructor]
        public Drv(Guid id, string name, string phone, string pwd, string licNo,
            Guid vehId, DriverStatus status, Loc position, decimal wallet, decimal income,
            int totTrip, int ratingSum, int totalReviews)
            : base(id, name, phone, pwd)
        {
            if (licNo == null)
            {
                throw new ArgumentNullException(nameof(licNo));
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            _licenseNumber = licNo;
            _vehicleId = vehId;
            _position = position;
            _wallet = wallet;
            _income = income;
            _totalTrips = totTrip;
            _ratingSum = ratingSum;
            _totalReviews = totalReviews;

            _currentState = DriverStateFactory.Create(status);
            _status = status;
        }

        // ---------- State Pattern: Ủy thác cho state hiện tại ----------
        public void SetOnline() { State.SetOnline(this); }
        public void SetOnTrip() { State.SetOnTrip(this); }
        public void SetOffline() { State.SetOffline(this); }

        public bool IsOnline() { return Status == DriverStatus.Online; }
        public bool IsOffline() { return Status == DriverStatus.Offline; }
        public bool IsOnTrip() { return Status == DriverStatus.OnTrip; }

        public void PayCommission(Fare fare)
        {
            if (fare is null) throw new ArgumentNullException(nameof(fare));
            if (Wallet < fare.Commission) throw new InvalidOperationException("Số dư tài khoản không đủ để khấu trừ chiết khấu chuyến đi.");
            Wallet -= fare.Commission;
        }

        public void UpdatePosition(Loc newPosition)
        {
            Position = newPosition ?? throw new ArgumentNullException(nameof(newPosition));
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Số tiền nạp phải lớn hơn 0.", nameof(amount));
            Wallet += amount;
        }

        public void AddIncome(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Thu nhập cộng thêm phải lớn hơn 0.", nameof(amount));
            Income += amount;
            Wallet += amount;
        }

        public void AddReview(int rating)
        {
            if (rating < 1 || rating > 5) throw new ArgumentException("Đánh giá phải nằm trong khoảng từ 1 đến 5 sao.", nameof(rating));
            RatingSum += rating;
            TotalReviews++;
        }

        public void IncrementTotalTrips() { TotTrip++; }

        internal void TransitionTo(IDriverState newState)
        {
            if (newState == null) throw new ArgumentNullException(nameof(newState));
            DriverStatus oldStatus;
            DriverStatus currentNewStatus;

            lock (_stateLock)
            {
                oldStatus = _status;
                _currentState = newState;
                _status = newState.Status;
                currentNewStatus = _status;
            }

            if (StatusChanged != null)
            {
                StatusChanged.Invoke(this, new DriverStatusChangedEventArgs(Id, oldStatus, currentNewStatus));
            }
        }
    }
    public class Adm : Usr
    {
        public Adm() : base() { }
        public Adm(string name, string phone, string password) : base(name, phone, password) { }

        [JsonConstructor]
        public Adm(Guid id, string name, string phone, string pwd) : base(id, name, phone, pwd) { }
    }
    public class Psg : Usr
    {
        private int _totalTrips;

        public int TotTrip
        {
            get { return _totalTrips; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _totalTrips = value;
            }
        }
        public Psg(string name, string phone, string password) : base(name, phone, password)
        {
            TotTrip = 0;
        }

        [JsonConstructor]
        public Psg(Guid id, string name, string phone, string pwd, int totTrip)
            : base(id, name, phone, pwd)
        {
            TotTrip = totTrip;
        }

        public void IncrementTotalTrips() { TotTrip++; }
    }
    #endregion

    #region Veh & các lớp con
    [JsonDerivedType(typeof(Car), typeDiscriminator: "car")]
    [JsonDerivedType(typeof(Moto), typeDiscriminator: "motorbike")]
    public abstract class Veh
    {
        private Guid _id;
        private Guid _driverId;
        private string _plateNumber = string.Empty;
        private string _brand = string.Empty;
        private string _model = string.Empty;
        private string _color = string.Empty;
        private int _capacity = 1;

        private static readonly Regex PlateRegex = new Regex(@"^[0-9]{2}[A-Z0-9\-\.\s]{3,12}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Guid Id
        {
            get { return _id; }
        }
        public Guid DriverId
        {
            get { return _driverId; }
        }
        public string Plate
        {
            get { return _plateNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Biển số xe không được để trống.", nameof(value));
                }
                string trimmed = value.Trim();
                if (PlateRegex.IsMatch(trimmed) == false)
                {
                    throw new ArgumentException("Biển số xe không đúng định dạng (Ví dụ: 59X3-12345 hoặc 29A-123.45).", nameof(value));
                }
                _plateNumber = trimmed;
            }
        }
        public string Brand
        {
            get { return _brand; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Hãng xe không được để trống.", nameof(value));
                _brand = value.Trim();
            }
        }

        public string Model
        {
            get { return _model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mẫu xe không được để trống.", nameof(value));
                _model = value.Trim();
            }
        }

        public string Color
        {
            get { return _color; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Màu sắc không được để trống.", nameof(value));
                _color = value.Trim();
            }
        }
        public int Capacity
        {
            get { return _capacity; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Sức chứa phải lớn hơn 0.");

                if (this is Car && (value < 4 || value > 7))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Xe ô tô bắt buộc phải cấu hình từ 4 đến 7 chỗ ngồi.");
                }
                else if (this is Moto && value > 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Xe máy chỉ cho phép tối đa 2 chỗ ngồi.");
                }

                _capacity = value;
            }
        }
        public abstract VehicleType Type { get; }

        // Constructor không tham số
        protected Veh()
        {
            _id = Guid.NewGuid();
            _driverId = Guid.Empty;
            _plateNumber = string.Empty;
            _brand = string.Empty;
            _model = string.Empty;
            _color = string.Empty;
            _capacity = this is Car ? 4 : 1;
        }

        protected Veh(string plateNumber, string brand, string model, string color, int capacity)
        {
            _id = Guid.NewGuid();
            _driverId = Guid.Empty;
            Plate = plateNumber;
            Brand = brand;
            Model = model;
            Color = color;
            Capacity = capacity;
        }

        [JsonConstructor]
        protected Veh(Guid id, Guid driverId, string plateNumber, string brand, string model,
                   string color, int capacity, VehicleType type)
        {
            _id = id;
            _driverId = driverId;
            if (plateNumber == null)
            {
                throw new ArgumentNullException(nameof(plateNumber));
            }
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (color == null)
            {
                throw new ArgumentNullException(nameof(color));
            }
            _plateNumber = plateNumber;
            _brand = brand;
            _model = model;
            _color = color;
            Capacity = capacity;
        }
        public void LinkDriver(Guid driverId)
        {
            if (driverId == Guid.Empty)
                throw new ArgumentException("Id tài xế để liên kết không hợp lệ.", nameof(driverId));
            _driverId = driverId;
        }
        public void UnlinkDriver()
        {
            _driverId = Guid.Empty;
        }
    }
    public class Car : Veh
    {
        public override VehicleType Type
        {
            get { return VehicleType.Car; }
        }

        public Car() : base() { }

        public Car(string plateNumber, string brand, string model, string color, int capacity)
            : base(plateNumber, brand, model, color, capacity) { }

        [JsonConstructor]
        public Car(Guid id, Guid driverId, string plate, string brand, string model,
            string color, int capacity, VehicleType type)
            : base(id, driverId, plate, brand, model, color, capacity, type) { }
    }

    public class Moto : Veh
    {
        public override VehicleType Type
        {
            get { return VehicleType.Moto; }
        }

        public Moto() : base() { }

        public Moto(string plateNumber, string brand, string model, string color, int capacity = 1)
            : base(plateNumber, brand, model, color, capacity) { }

        [JsonConstructor]
        public Moto(Guid id, Guid driverId, string plate, string brand, string model,
            string color, int capacity, VehicleType type)
            : base(id, driverId, plate, brand, model, color, capacity, type) { }
    }
    #endregion

    #region Pol
    public class Pol
    {
        private Guid _id;
        private VehicleType _vehicleType;
        private decimal _baseFare;
        private decimal _pricePerKm;
        private decimal _commissionRate;
        private DateTime _createdAt;

        public Guid Id
        {
            get { return _id; }
        }

        public VehicleType VehicleType
        {
            get { return _vehicleType; }
            private set
            {
                if (!Enum.IsDefined(typeof(VehicleType), value))
                    throw new ArgumentException("Loại xe áp dụng chính sách không hợp lệ.", nameof(value));
                _vehicleType = value;
            }
        }

        public decimal Base
        {
            get { return _baseFare; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Giá mở cửa không được âm.");
                _baseFare = value;
            }
        }

        public decimal PriceKm
        {
            get { return _pricePerKm; }
            private set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Giá mỗi km không được âm.");
                _pricePerKm = value;
            }
        }

        public decimal CommRate
        {
            get { return _commissionRate; }
            private set
            {
                if (value < 0 || value > 1) throw new ArgumentOutOfRangeException(nameof(value), "Tỷ lệ chiết khấu phải nằm trong khoảng từ 0.0 đến 1.0 (0% - 100%).");
                _commissionRate = value;
            }
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
        }

        public Pol()
        {
            _id = Guid.NewGuid();
            _vehicleType = VehicleType.Moto;
            _baseFare = 0;
            _pricePerKm = 0;
            _commissionRate = 0;
            _createdAt = DateTime.UtcNow;
        }

        public Pol(VehicleType vehicleType, decimal baseFare, decimal pricePerKm, decimal commissionRate)
        {
            _id = Guid.NewGuid();
            VehicleType = vehicleType;
            Base = baseFare;
            PriceKm = pricePerKm;
            CommRate = commissionRate;
            _createdAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Pol(Guid id, VehicleType vehicleType, decimal Base, decimal PriceKm,
            decimal CommRate, DateTime CreatedAt)
        {
            _id = id;
            _vehicleType = vehicleType;
            _baseFare = Base;
            _pricePerKm = PriceKm;
            _commissionRate = CommRate;
            _createdAt = CreatedAt;
        }
        public Fare CalculateFare(decimal distance)
        {
            if (distance < 0) throw new ArgumentException("Khoảng cách di chuyển không được âm.", nameof(distance));

            decimal fareAmount = Base + (PriceKm * distance);
            fareAmount = Math.Round(fareAmount, 0, MidpointRounding.AwayFromZero);

            decimal commissionAmount = Math.Round(fareAmount * CommRate, 0, MidpointRounding.AwayFromZero);

            return new Fare(fareAmount, commissionAmount);
        }
    }
    #endregion

    #region Rev
    public sealed class Rev
    {
        private Guid _id;
        private Guid _driverId;
        private Guid _passengerId;
        private Guid _tripId;
        private int _star;
        private string _comment = string.Empty;
        private DateTime _createdAt;

        public Guid Id
        {
            get { return _id; }
        }
        public Guid DriverId
        {
            get { return _driverId; }
        }
        public Guid PassengerId
        {
            get { return _passengerId; }
        }
        public Guid TripId
        {
            get { return _tripId; }
        }
        public int Star
        {
            get { return _star; }
            private set
            {
                if (value < 1 || value > 5) throw new ArgumentOutOfRangeException(nameof(value), "Điểm đánh giá phải từ 1 đến 5 sao.");
                _star = value;
            }
        }
        public string Comment
        {
            get { return _comment; }
            private set
            {
                if (value == null)
                {
                    _comment = string.Empty;
                }
                else
                {
                    _comment = value;
                }
            }
        }
        public DateTime CreatedAt
        {
            get { return _createdAt; }
        }

        public Rev()
        {
            _id = Guid.NewGuid();
            _driverId = Guid.Empty;
            _passengerId = Guid.Empty;
            _tripId = Guid.Empty;
            _star = 1;
            _comment = string.Empty;
            _createdAt = DateTime.UtcNow;
        }

        public Rev(Guid driverId, Guid passengerId, Guid tripId, int star, string comment)
        {
            if (driverId == Guid.Empty) throw new ArgumentException("Id tài xế không hợp lệ.", nameof(driverId));
            if (passengerId == Guid.Empty) throw new ArgumentException("Id hành khách không hợp lệ.", nameof(passengerId));
            if (tripId == Guid.Empty) throw new ArgumentException("Id chuyến đi không hợp lệ.", nameof(tripId));

            _id = Guid.NewGuid();
            _driverId = driverId;
            _passengerId = passengerId;
            _tripId = tripId;
            Star = star;
            Comment = comment;
            _createdAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Rev(Guid id, Guid driverId, Guid passengerId, Guid tripId, int star, string comment, DateTime createdAt)
        {
            _id = id;
            _driverId = driverId;
            _passengerId = passengerId;
            _tripId = tripId;
            _star = star;
            _comment = comment;
            _createdAt = createdAt;
        }

        public void UpdateReview(int star, string comment)
        {
            Star = star;
            Comment = comment;
        }
    }
    #endregion

    #region Trip
    public class Trip
    {
        [field: JsonIgnore]
        public event EventHandler<TripStatusChangedEventArgs>? StatusChanged;
        private readonly object _stateLock = new object();
        private ITripState _currentState;
        private TripStatus _status;
        private Guid _id, _passengerId;
        private VehicleType _tripVehicleType;
        private Fare _tripFare = new Fare();
        private Route _tripRoute = new Route();
        private DateTime _createAt;
        private Guid? _driverId;
        private bool _isPaid, _isReviewed;
        public Guid Id
        {
            get { return _id; }
        }
        public Guid PassengerId
        {
            get { return _passengerId; }
        }
        public VehicleType TripVehicleType
        {
            get { return _tripVehicleType; }
        }
        public Fare TripFare
        {
            get { return _tripFare; }
        }
        public Route TripRoute
        {
            get { return _tripRoute; }
        }
        public DateTime CreatedAt
        {
            get { return _createAt; }
        }
        public Loc Pickup
        {
            get { return _tripRoute.Pickup; }
        }
        public Loc Dropoff
        {
            get { return _tripRoute.Dropoff; }
        }
        public Guid? DriverId
        {
            get
            {
                lock (_stateLock) return _driverId;
            }
            private set
            {
                if (value.HasValue && value.Value == Guid.Empty)
                    throw new ArgumentException("Id tài xế không hợp lệ.", nameof(value));
                _driverId = value;
            }
        }
        public bool IsPaid
        {
            get { lock (_stateLock) return _isPaid; }
            private set { _isPaid = value; }
        }
        public bool IsReviewed
        {
            get { lock (_stateLock) return _isReviewed; }
            private set { _isReviewed = value; }
        }
        public TripStatus Status
        {
            get
            {
                lock (_stateLock)
                {
                    if (_currentState != null)
                    {
                        return _currentState.Status;
                    }
                    return _status;
                }
            }
        }
        public Trip()
        {
            _id = Guid.NewGuid();
            _passengerId = Guid.Empty;
            _tripVehicleType = VehicleType.Moto;
            _tripFare = new Fare();
            _tripRoute = new Route();
            _createAt = DateTime.UtcNow;
            _isPaid = false;
            _isReviewed = false;
            _status = TripStatus.Pending;
            _currentState = TripStateFactory.Create(TripStatus.Pending);
        }
        public Trip(Guid passengerId, Route route, Fare fare, VehicleType vehicleType)
        {
            if (passengerId == Guid.Empty) throw new ArgumentException("Id hành khách không hợp lệ.", nameof(passengerId));
            _id = Guid.NewGuid();
            _passengerId = passengerId;
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (fare == null) throw new ArgumentNullException(nameof(fare));
            _tripRoute = route;
            _tripFare = fare;
            if (!Enum.IsDefined(typeof(VehicleType), vehicleType)) throw new ArgumentException("Loại xe yêu cầu không hợp lệ.", nameof(vehicleType));
            _tripVehicleType = vehicleType;
            _createAt = DateTime.UtcNow;
            _isPaid = false;
            _isReviewed = false;
            _status = TripStatus.Pending;
            _currentState = TripStateFactory.Create(TripStatus.Pending);
        }
        [JsonConstructor]
        public Trip(Guid id, Guid passengerId, VehicleType tripVehicleType, Fare tripFare, Route tripRoute,
            DateTime createdAt, Guid? driverId, bool isPaid, bool isReviewed, TripStatus status)
        {
            _id = id;
            _passengerId = passengerId;
            _tripVehicleType = tripVehicleType;
            if (tripFare == null) throw new ArgumentNullException(nameof(tripFare));
            if (tripRoute == null) throw new ArgumentNullException(nameof(tripRoute));
            _tripFare = tripFare;
            _tripRoute = tripRoute;
            _createAt = createdAt;
            _driverId = driverId;
            _isPaid = isPaid;
            _isReviewed = isReviewed;
            _status = status;
            _currentState = TripStateFactory.Create(status);
        }
        public void StartSearching() { GetCurrentStateSafe().StartSearching(this); }
        public void AssignDriver(Guid driverId) { GetCurrentStateSafe().AssignDriver(this, driverId); }
        public void ArrivedPickup() { GetCurrentStateSafe().ArrivedPickup(this); }
        public void BeginTrip() { GetCurrentStateSafe().BeginTrip(this); }
        public void ArrivedDropoff() { GetCurrentStateSafe().ArrivedDropoff(this); }
        public void Cancel(string reason) { GetCurrentStateSafe().Cancel(this, reason); }
        public void Timeout() { GetCurrentStateSafe().Timeout(this); }
        public void ConfirmPayment() { GetCurrentStateSafe().ConfirmPayment(this); }

        public bool IsSearching() { return Status == TripStatus.Searching; }
        public bool IsMatched() { return Status == TripStatus.Matched; }
        public bool IsArrived() { return Status == TripStatus.Arrived; }
        public bool IsStarted() { return Status == TripStatus.Started; }
        public bool IsDropOff() { return Status == TripStatus.DropOff; }
        public bool IsCompleted() { return Status == TripStatus.Completed; }
        public bool IsCancelled() { return Status == TripStatus.Cancelled; }
        public bool IsTimeout() { return Status == TripStatus.Timeout; }
        public bool IsTerminal() { return IsCompleted() || IsCancelled() || IsTimeout(); }

        private ITripState GetCurrentStateSafe()
        {
            lock (_stateLock) return _currentState;
        }
        public void MarkAsReviewed()
        {
            lock (_stateLock)
            {
                if (_isReviewed) throw new InvalidOperationException("Chuyến đi này đã được gửi đánh giá trước đó.");
                if (!IsCompleted()) throw new InvalidOperationException("Chỉ có thể đánh giá những chuyến đi đã hoàn thành xuất sắc.");
                _isReviewed = true;
            }
        }
        internal void TransitionTo(ITripState newState)
        {
            if (newState == null) throw new ArgumentNullException(nameof(newState));
            TripStatus oldStatus;
            TripStatus currentNewStatus;
            lock (_stateLock)
            {
                oldStatus = _currentState.Status;
                _currentState = newState;
                _status = newState.Status;
                currentNewStatus = _status;
            }
            if (StatusChanged != null)
            {
                StatusChanged.Invoke(this, new TripStatusChangedEventArgs(Id, oldStatus, currentNewStatus));
            }
        }
        internal void SetDriverId(Guid driverId)
        {
            if (driverId == Guid.Empty) throw new ArgumentException("Id tài xế không hợp lệ.", nameof(driverId));

            lock (_stateLock)
            {
                if (_driverId.HasValue && _driverId.Value != driverId)
                    throw new InvalidOperationException("Chuyến đi đã có tài xế tiếp nhận, không thể thay đổi.");
                _driverId = driverId;
            }
        }
        internal void MarkPaymentAsSettled()
        {
            lock (_stateLock)
            {
                _isPaid = true;
            }
        }
    }

    #endregion
}
