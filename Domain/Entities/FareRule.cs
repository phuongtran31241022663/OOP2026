using Domain.Enums;
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
        /// Loại phương tiện áp dụng quy tắc cước này.
        /// </summary>
        public VehicleType VehicleType { get; }

        /// <summary>
        /// Giá cước mở cửa (giá cố định ban đầu).
        /// </summary>
        public Money BaseFare => _baseFare;

        /// <summary>
        /// Giá cước cho mỗi kilomet.
        /// </summary>
        public Money PricePerKm => _pricePerKm;

        /// <summary>
        /// Tỉ lệ hoa hồng (ví dụ: 0.2 cho 20%).
        /// </summary>
        public decimal CommissionRate => _commissionRate;

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
        /// <param name="vehicleType">Loại phương tiện.</param>
        /// <param name="baseFare">Giá mở cửa.</param>
        /// <param name="pricePerKm">Giá mỗi km.</param>
        /// <param name="commissionRate">Tỉ lệ hoa hồng.</param>
        public FareRule(
            VehicleType vehicleType,
            Money baseFare,
            Money pricePerKm,
            decimal commissionRate
        ) : base(Guid.NewGuid())
        {
            VehicleType = vehicleType;
            SetRule(baseFare, pricePerKm, commissionRate);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Cập nhật các thông số của quy tắc cước.
        /// </summary>
        /// <param name="baseFare">Giá mở cửa mới.</param>
        /// <param name="pricePerKm">Giá mỗi km mới.</param>
        /// <param name="commissionRate">Tỉ lệ hoa hồng mới.</param>
        public void UpdateRule(Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            SetRule(baseFare, pricePerKm, commissionRate);
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

            decimal commissionAmount = totalAmount * _commissionRate;

            Money total = new Money(totalAmount);
            Money commission = new Money(commissionAmount);

            return new Fare(total, commission);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Thiết lập và xác thực các giá trị của quy tắc cước.
        /// </summary>
        private void SetRule(Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            if (baseFare == null)
                throw new ArgumentNullException(nameof(baseFare));

            if (pricePerKm == null)
                throw new ArgumentNullException(nameof(pricePerKm));

            if (baseFare.Amount < 0)
                throw new ArgumentOutOfRangeException(nameof(baseFare), "Giá mở cửa phải lớn hơn hoặc bằng 0.");

            if (pricePerKm.Amount < 0)
                throw new ArgumentOutOfRangeException(nameof(pricePerKm), "Giá mỗi km phải lớn hơn hoặc bằng 0.");

            if (commissionRate < 0 || commissionRate > 1)
                throw new ArgumentOutOfRangeException(nameof(commissionRate), "Tỉ lệ hoa hồng phải nằm trong khoảng [0, 1].");

            _baseFare = baseFare;
            _pricePerKm = pricePerKm;
            _commissionRate = commissionRate;
            _updatedAt = DateTime.UtcNow;
        }

        #endregion
    }
}
