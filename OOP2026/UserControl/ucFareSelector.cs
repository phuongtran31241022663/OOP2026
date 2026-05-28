using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace OOP2026
{
    public partial class ucFareSelector : UserControl
    {
        private VehicleType _selectedType = VehicleType.Moto;

        public event EventHandler<VehicleType>? SelectionChanged;

        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(VehicleType.Moto)]
        public VehicleType SelectedType
        {
            get => _selectedType;
            set
            {
                if (_selectedType == value) return;
                _selectedType = value;

                // Cập nhật lại trạng thái hiển thị của các Panel
                UpdateVisualState();

                // Kích hoạt sự kiện thông báo ra tầng cha
                SelectionChanged?.Invoke(this, _selectedType);
            }
        }

        public ucFareSelector()
        {
            InitializeComponent();
            // Tận dụng hàm cập nhật tập trung để thiết lập trạng thái ban đầu
            UpdateVisualState();
        }

        /// <summary>
        /// Cập nhật an toàn giá tiền từ luồng bất đồng bộ của Map/Fare Service
        /// </summary>
        public void SetPrices(decimal motorbikePrice, decimal carPrice)
        {
            // SỬA LỖI CRASH: Kiểm tra nếu hàm bị gọi từ một luồng ngầm không phải UI Thread
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetPrices(motorbikePrice, carPrice)));
                return;
            }

            lblMotorbikePrice.Text = $"{motorbikePrice:N0}đ";
            lblCarPrice.Text = $"{carPrice:N0}đ";
        }
        private void UpdateVisualState()
        {
            bool isMotorbike = (_selectedType == VehicleType.Moto);

            // Thay đổi màu nền tùy theo trạng thái được chọn
            pnlMotorbike.BackColor = isMotorbike ? Colors.LightGreen : System.Drawing.Color.White;
            pnlCar.BackColor = !isMotorbike ? Colors.LightGreen : System.Drawing.Color.White;

            // UX Tối ưu: Đổi màu chữ Icon/Title để tạo độ tương phản tốt hơn khi được Select
            lblMotorbikeIcon.ForeColor = isMotorbike ? System.Drawing.Color.DarkGreen : System.Drawing.Color.Black;
            lblCarIcon.ForeColor = !isMotorbike ? System.Drawing.Color.DarkGreen : System.Drawing.Color.Black;

            // Ép làm tươi đồ họa vùng chọn
            pnlMotorbike.Invalidate();
            pnlCar.Invalidate();
        }

        // ========== EVENT HANDLERS (GIAO DIỆN THUẦN TÚY) ==========

        private void OnMotorbikeClick(object sender, EventArgs e)
        {
            SelectedType = VehicleType.Moto;
        }

        private void OnCarClick(object sender, EventArgs e)
        {
            SelectedType = VehicleType.Car;
        }
    }
}
