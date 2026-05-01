using Domain.Entities.Users;
using Domain.Entities;
using Domain.ValueObjects;
using Presentation;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiển thị thông tin giá cước chuyến đi.
    /// Bao gồm giá gốc, phụ phí, và tổng cộng.
    /// </summary>
    public partial class FarePanel : BaseUserControl
    {
        /// <summary>
        /// Giá cước hiện tại
        /// </summary>
        public Money CurrentFare { get; private set; }

        public FarePanel()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Set chi tiết giá cước
        /// </summary>
        public void SetFareDetails(decimal baseFare, decimal distanceFare, decimal commission, decimal totalFare)
        {
            _lblBaseFare.Text = $"Giá cơ bản: {baseFare:N0} đ";
            _lblDistanceFare.Text = $"Phí khoảng cách: {distanceFare:N0} đ";
            _lblCommission.Text = $"Hoa hồng: {commission:N0} đ";
            _lblTotalFare.Text = $"Tổng cộng: {totalFare:N0} đ";

            CurrentFare = new Money(totalFare, "VND");
        }

        /// <summary>
        /// Set giá ước tính cho booking
        /// </summary>
        public void SetFare(decimal Fare)
        {
            _lblBaseFare.Text = "Giá ước tính";
            _lblDistanceFare.Text = "";
            _lblCommission.Text = "";
            _lblTotalFare.Text = $"{Fare:N0} đ";

            _lblBreakdown.Text = "* Giá có thể thay đổi";
        }

        /// <summary>
        /// Reset panel về trạng thái trống
        /// </summary>
        public void ClearFare()
        {
            _lblBaseFare.Text = "Giá cơ bản: --";
            _lblDistanceFare.Text = "Phí khoảng cách: --";
            _lblCommission.Text = "Hoa hồng: --";
            _lblTotalFare.Text = "Tổng cộng: --";
            CurrentFare = null;
        }
    }
}

