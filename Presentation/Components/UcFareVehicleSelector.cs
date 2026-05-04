using System;
using System.Windows.Forms;
using Domain.Enums;
using Presentation;

namespace Presentation.Components
{
    /// <summary>
    /// Control gộp fare panel + vehicle type combo box.
    /// </summary>
    public partial class UcFareVehicleSelector : BaseUserControl
    {
        public event Action<VehicleType> VehicleTypeChanged;

        public VehicleType SelectedVehicleType => cmbVehicleType.SelectedIndex == 0
            ? VehicleType.Motorbike
            : VehicleType.Car;

        public decimal CurrentFare { get; private set; }

        public UcFareVehicleSelector()
        {
            InitializeComponent();
        }

        public void SetFare(decimal amount)
        {
            CurrentFare = amount;
            lblFareAmount.Text = $"{amount:N0}đ";
        }

        public void ClearFare()
        {
            CurrentFare = 0;
            lblFareAmount.Text = "---đ";
        }

        private void cmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            VehicleTypeChanged?.Invoke(SelectedVehicleType);
        }

        // Enable manual vehicle type selection
        public void SetVehicleType(VehicleType vehicleType)
        {
            cmbVehicleType.SelectedIndex = vehicleType == VehicleType.Motorbike ? 0 : 1;
        }
    }
}