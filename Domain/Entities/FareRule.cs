using Domain.ValueObjects;
using System;
using Domain.SharedKernel;

namespace Domain.Entities
{
    /// <summary>
    /// Định nghĩa quy tắc tính cước cho một loại phương tiện cụ thể.
    /// </summary>
    /// <remarks>
    /// Lớp này là một Entity, có định danh duy nhất và vòng đời riêng.
    /// Nó chứa các thông số để tính toán giá cước, bao gồm giá mở cửa, giá mỗi km và tỉ lệ hoa hồng.
    /// </remarks>
    public class FareRule : Entity
    {
        #region Fields

        private Money _baseFare;
        private Money _pricePerKm;
        private decimal _commissionRate;
        private DateTime _updatedAt;

        #endregion

        #region Properties

        /// <summary>
        /// Loại phương tiện áp dụng quy tắc cước này (ví dụ: "Car", "Motorbike").
        /// </summary>
        public string VehicleType { get; }

        /// <summary>
        /// Giá cước mở cửa (giá cố định ban đầu).
        /// </summary>
        public Money BaseFare
        {
            get => _baseFare;
            private set
            {
                if (value.Amount < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Giá mở cửa phải lớn hơn hoặc bằng 0.");
                _baseFare = value;
            }
        }

        /// <summary>
        /// Giá cước cho mỗi kilomet.
        /// </summary>
        public Money PricePerKm
        {
            get => _pricePerKm;
            private set
            {
                if (value.Amount < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Giá mỗi km phải lớn hơn hoặc bằng 0.");
                _pricePerKm = value;
            }
        }

        /// <summary>
        /// Tỉ lệ hoa hồng
        /// </summary>
        public decimal CommissionRate
        {
            get => _commissionRate;
            private set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(CommissionRate),
                        "Tỉ lệ hoa hồng phải nằm trong khoảng [0, 1].");
                _commissionRate = value;
            }
        }

        /// <summary>
        /// Thời điểm quy tắc được cập nhật lần cuối.
        /// </summary>
        public DateTime UpdatedAt => _updatedAt;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor cho ORM.
        /// </summary>
        protected FareRule() : base(Guid.Empty) { }

        /// <summary>
        /// Tạo một quy tắc tính cước mới.
        /// </summary>
        /// <param name="vehicleType">Loại phương tiện (ví dụ: "Car", "Motorbike").</param>
        /// <param name="baseFare">Giá mở cửa.</param>
        /// <param name="pricePerKm">Giá mỗi km.</param>
        /// <param name="commissionRate">Tỉ lệ hoa hồng.</param>
        public FareRule(
            string vehicleType,
            Money baseFare,
            Money pricePerKm,
            decimal commissionRate
        ) : base(Guid.NewGuid())
        {
            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new ArgumentException("Loại phương tiện không được để trống.", nameof(vehicleType));

            VehicleType = vehicleType;
            BaseFare = baseFare;
            PricePerKm = pricePerKm;
            CommissionRate = commissionRate;
            _updatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Cập nhật các thông số của quy tắc cước.
        /// </summary>
        /// <param name="vehicleType">Loại phương tiện (phải khớp với loại hiện tại).</param>
        /// <param name="baseFare">Giá mở cửa mới.</param>
        /// <param name="pricePerKm">Giá mỗi km mới.</param>
        /// <param name="commissionRate">Tỉ lệ hoa hồng mới.</param>
        public void UpdateRule(string vehicleType, Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            if (!string.Equals(VehicleType, vehicleType, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Không thể thay đổi loại phương tiện của quy tắc cước hiện có.");

            BaseFare = baseFare;
            PricePerKm = pricePerKm;
            CommissionRate = commissionRate;
            _updatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Tính toán tổng cước và hoa hồng cho một quãng đường di chuyển.
        /// </summary>
        /// <param name="distanceKm">Quãng đường di chuyển (tính bằng kilomet).</param>
        /// <returns>Đối tượng <see cref="Fare"/> chứa tổng cước và tiền hoa hồng.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra khi quãng đường nhỏ hơn 0.</exception>
        public Fare CalculateFare(double distanceKm)
        {
            if (distanceKm < 0)
                throw new ArgumentOutOfRangeException(nameof(distanceKm), "Quãng đường phải lớn hơn hoặc bằng 0.");

            decimal totalAmount =
                BaseFare.Amount +
                ((decimal)distanceKm * PricePerKm.Amount);

            // Làm tròn đến 1000đ: >= 500 lên, < 500 xuống
            totalAmount = Math.Round(totalAmount / 1000m, MidpointRounding.AwayFromZero) * 1000m;

            decimal commissionAmount = totalAmount * _commissionRate;

            Money total = new Money(totalAmount);
            Money commission = new Money(commissionAmount);

            return new Fare(total, commission);
        }

        #endregion
    }
}
