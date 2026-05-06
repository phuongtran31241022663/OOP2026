using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.Interfaces;
using Domain.ValueObjects;
using Presentation.Base;

namespace Presentation.Components
{
    /// <summary>
    /// Merged UcVehicleFareSelector: Compact mode (combo+fare) + Cards mode (visual options with icons, dist/time, auto-fare calc).
    /// Integrates UcVehicleOptionsPanel features into UcFareVehicleSelector base.
    /// </summary>
public partial class UcVehicleFareSelector : BaseUserControl
    {
        private IFareService _fareService;
        private IMapService _mapService;
        private string _selectedVehicleType;
        private bool _cardMode = false; // Default compact

        // Compact UI (from original Designer)
        public string SelectedVehicleType => cmbVehicleType.SelectedIndex == 0 ? "Motorbike" : "Car";
        public decimal CurrentFare { get; private set; }

        // Dynamic card elements (from UcVehicleOptionsPanel)
        private Panel pnlCardsContainer;
        private Label lblMotoIcon, lblMotoName, lblMotoDistTime, lblMotoFare;
        private Button btnMotoSelect;
        private Label lblCarIcon, lblCarName, lblCarDistTime, lblCarFare;
        private Button btnCarSelect;
        private Button btnToggleMode; // Toggle compact/cards

        public event Action<string> VehicleTypeChanged; // Original
        public event EventHandler<string> VehicleSelected; // From options panel

        public UcVehicleFareSelector()
        {
            InitializeComponent();
            InitializeDynamicControls();
        }

        private void InitializeDynamicControls()
        {
            // Toggle button (compact <-> cards)
            btnToggleMode = new Button 
            { 
                Text = "Cards", 
                Dock = DockStyle.Top, 
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                Font = Presentation.Constants.UiConstants.Typography.Small,
                BackColor = Presentation.Constants.UiConstants.Colors.SurfaceLight
            };
            btnToggleMode.FlatAppearance.BorderSize = 0;
            btnToggleMode.Click += BtnToggleMode_Click;
            Controls.Add(btnToggleMode);
            btnToggleMode.BringToFront();

            // Cards container (initially hidden)
            pnlCardsContainer = new Panel { Dock = DockStyle.Fill, Visible = false, Padding = new Padding(10) };
            CreateCardControls();
            Controls.Add(pnlCardsContainer);
            pnlCardsContainer.SendToBack();

            Height = 300; // Cards height
            _cardMode = true; // Start with cards mode
            UpdateMode();
        }

        private void CreateCardControls()
        {
            TableLayoutPanel tblCards = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
            };
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlCardsContainer.Controls.Add(tblCards);

            // Motorbike card
            var pnlMoto = CreateVehicleCard("Motorbike", "🏍️", "Xe máy", out lblMotoDistTime, out lblMotoFare, out btnMotoSelect);
            btnMotoSelect.Click += BtnMotoSelect_Click;
            tblCards.Controls.Add(pnlMoto, 0, 0);

            // Car card
            var pnlCar = CreateVehicleCard("Car", "🚗", "Ô tô", out lblCarDistTime, out lblCarFare, out btnCarSelect);
            btnCarSelect.Click += BtnCarSelect_Click;
            tblCards.Controls.Add(pnlCar, 1, 0);
        }

        private Panel CreateVehicleCard(string type, string icon, string name, out Label lblDistTime, out Label lblFare, out Button btnSelect)
        {
            var pnl = new Panel 
            { 
                BackColor = Presentation.Constants.UiConstants.Colors.SurfaceWhite, 
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Padding = new Padding(10)
            };
            // Thêm hiệu ứng border bằng Paint
            pnl.Paint += (s, e) => {
                var p = (Panel)s;
                Color borderColor = (string)p.Tag == _selectedVehicleType ? Presentation.Constants.UiConstants.Colors.Primary : Presentation.Constants.UiConstants.Colors.BorderSubtle;
                int borderWidth = (string)p.Tag == _selectedVehicleType ? 2 : 1;
                using (var pen = new Pen(borderColor, borderWidth)) {
                    e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
                }
            };
            pnl.Tag = type;

            var lblIcon = new Label { Text = icon, Font = new Font("Segoe UI Emoji", 28), Dock = DockStyle.Top, Height = 50, TextAlign = ContentAlignment.MiddleCenter };
            var lblName = new Label { Text = name, Font = Presentation.Constants.UiConstants.Typography.Header, Dock = DockStyle.Top, Height = 30, TextAlign = ContentAlignment.MiddleCenter };
            lblDistTime = new Label { Text = "---", Font = Presentation.Constants.UiConstants.Typography.Tiny, Dock = DockStyle.Top, Height = 20, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted };
            lblFare = new Label { Text = "--- đ", Font = Presentation.Constants.UiConstants.Typography.Header, Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Presentation.Constants.UiConstants.Colors.Primary };
            
            btnSelect = new Button 
            { 
                Text = "Chọn", 
                Dock = DockStyle.Bottom, 
                Height = 35, 
                FlatStyle = FlatStyle.Flat,
                BackColor = Presentation.Constants.UiConstants.Colors.SurfaceLight,
                Font = Presentation.Constants.UiConstants.Typography.Small
            };
            btnSelect.FlatAppearance.BorderSize = 0;

            pnl.Controls.AddRange(new Control[] { btnSelect, lblFare, lblDistTime, lblName, lblIcon });
            
            // Click anywhere to select
            Control[] all = { pnl, lblIcon, lblName, lblDistTime, lblFare };
            foreach(var c in all) c.Cursor = Cursors.Hand;
            pnl.Click += (s, e) => OnVehicleSelect(type);
            lblIcon.Click += (s, e) => OnVehicleSelect(type);
            lblName.Click += (s, e) => OnVehicleSelect(type);
            lblFare.Click += (s, e) => OnVehicleSelect(type);

            return pnl;
        }

        public void SetServices(IMapService mapService, IFareService fareService)
        {
            _mapService = mapService;
            _fareService = fareService;
        }

        // Rich update from locations (from UcVehicleOptionsPanel)
        public async Task UpdateFaresAsync(Location pickup, Location destination)
        {
            if (_mapService == null || _fareService == null || pickup == null || destination == null) return;

            try
            {
                Route route = await _mapService.GetRouteAsync(pickup, destination);
                string distTime = $"{route.Distance:F1} km • {route.Duration.TotalMinutes:F0} phút";

                // Motorbike
                Fare motoFareResult = await _fareService.CalculateFareAsync("Motorbike", route.Distance);
                lblMotoDistTime.Text = distTime;
                lblMotoFare.Text = motoFareResult.TotalAmount.Amount.ToString("N0") + " đ";

                // Car
                Fare carFareResult = await _fareService.CalculateFareAsync("Car", route.Distance);
                lblCarDistTime.Text = distTime;
                lblCarFare.Text = carFareResult.TotalAmount.Amount.ToString("N0") + " đ";

                CurrentFare = motoFareResult.TotalAmount.Amount; // Default to moto
                lblFareAmount.Text = CurrentFare.ToString("N0") + "đ";
                ClearSelection();
            }
            catch (Exception ex)
            {
                lblMotoFare.Text = lblCarFare.Text = "Lỗi tính giá";
                lblMotoDistTime.Text = lblCarDistTime.Text = ex.Message;
                lblFareAmount.Text = "Lỗi";
            }
        }

        // Original methods
        public void SetFare(decimal amount)
        {
            CurrentFare = amount;
            lblFareAmount.Text = $"{amount:N0}đ";
        }

        public void ClearFare()
        {
            CurrentFare = 0;
            lblFareAmount.Text = string.Empty;
        }

        public void SetVehicleType(string vehicleType)
        {
            _selectedVehicleType = vehicleType;
            cmbVehicleType.SelectedIndex = vehicleType.Equals("Motorbike", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
            if (_cardMode)
            {
                HighlightSelected(vehicleType);
            }
        }

        private void cmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedVehicleType = SelectedVehicleType;
            VehicleTypeChanged?.Invoke(_selectedVehicleType);
            VehicleSelected?.Invoke(this, _selectedVehicleType);
        }

        private void OnVehicleSelect(string vehicleType)
        {
            _selectedVehicleType = vehicleType;
            HighlightSelected(vehicleType);
            VehicleTypeChanged?.Invoke(vehicleType);
            VehicleSelected?.Invoke(this, vehicleType);
        }

        private void ClearSelection()
        {
            _selectedVehicleType = null;
            btnMotoSelect.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceLight;
            btnMotoSelect.ForeColor = Presentation.Constants.UiConstants.Colors.TextPrimary;
            btnMotoSelect.Text = "Chọn";

            btnCarSelect.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceLight;
            btnCarSelect.ForeColor = Presentation.Constants.UiConstants.Colors.TextPrimary;
            btnCarSelect.Text = "Chọn";

            cmbVehicleType.BackColor = Color.White;
            pnlCardsContainer.Invalidate(true);
        }

        private void HighlightSelected(string vehicleType)
        {
            _selectedVehicleType = vehicleType;
            if (vehicleType == "Motorbike")
            {
                btnMotoSelect.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
                btnMotoSelect.ForeColor = Color.White;
                btnMotoSelect.Text = "Đang chọn";
            }
            else if (vehicleType == "Car")
            {
                btnCarSelect.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
                btnCarSelect.ForeColor = Color.White;
                btnCarSelect.Text = "Đang chọn";
            }
            pnlCardsContainer.Invalidate(true);
        }

        private void ToggleMode()
        {
            _cardMode = !_cardMode;
            UpdateMode();
        }

        private void UpdateMode()
        {
            pnlCardsContainer.Visible = _cardMode;
            cmbVehicleType.Visible = lblVehicleLabel.Visible = lblFareLabel.Visible = lblFareAmount.Visible = !_cardMode;
            btnToggleMode.Text = _cardMode ? "Compact" : "Cards";
            Height = _cardMode ? 300 : 80;

            // Update parent panel height if it's inside UcPassenger booking panel
            if (Parent != null && Parent.Name == "pnlBooking")
            {
                Height = _cardMode ? 300 : 80;
            }

            Invalidate();
        }

        private void BtnToggleMode_Click(object sender, EventArgs e)
        {
            ToggleMode();
        }

        private void BtnMotoSelect_Click(object sender, EventArgs e)
        {
            OnVehicleSelect("Motorbike");
        }

        private void BtnCarSelect_Click(object sender, EventArgs e)
        {
            OnVehicleSelect("Car");
        }
    }
}

