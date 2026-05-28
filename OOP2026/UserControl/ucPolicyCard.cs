using System;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucPolicyCard : UserControl
    {
        private Pol _policy;

        public ucPolicyCard()
        {
            InitializeComponent();
        }

        public void SetPolicy(Pol policy)
        {
            _policy = policy ?? throw new ArgumentNullException(nameof(policy));
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            bool isCar = _policy.VehicleType == VehicleType.Car;

            this.BackColor = isCar ? Colors.LightBlue : Colors.LightYellow;
            lblTitle.Text = isCar ? "🚗 Xe ô tô" : "🛵 Xe máy";
            lblBasePrice.Text = $"🔓 Mở cửa: {_policy.Base:N0}đ";
            lblKmPrice.Text = $"📏 Giá/km: {_policy.PriceKm:N0}đ";
            lblCommission.Text = $"💳 Hoa hồng: {_policy.CommRate}%";
        }
    }
}