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
                UpdateVisualState();
                SelectionChanged?.Invoke(this, _selectedType);
            }
        }

        public ucFareSelector()
        {
            InitializeComponent();
            UpdateVisualState();
        }

        /// <summary>
        /// Cập nhật giá tiền hiển thị cho cả hai loại xe.
        /// </summary>
        public void SetPrices(decimal motorbikePrice, decimal carPrice)
        {
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

            // Màu nền của panel khi được chọn: xanh lá nhạt (LightGreen)
            pnlMotorbike.BackColor = isMotorbike ? Color.LightGreen : System.Drawing.Color.White;
            pnlCar.BackColor = !isMotorbike ? Color.LightGreen : System.Drawing.Color.White;

            // Màu chữ của icon/tên xe
            lblMotorbikeIcon.ForeColor = isMotorbike ? Color.DarkGreen : System.Drawing.Color.Black;
            lblCarIcon.ForeColor = !isMotorbike ? Color.DarkGreen : System.Drawing.Color.Black;

            // Làm mới giao diện
            pnlMotorbike.Invalidate();
            pnlCar.Invalidate();
        }

        // ========== EVENT HANDLERS ==========

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