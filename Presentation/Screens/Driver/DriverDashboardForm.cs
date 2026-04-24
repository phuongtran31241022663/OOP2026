using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.Interfaces;
using Presentation.Shells;
using Presentation.Components;
using Application.DTOs;
using Domain.Users.Drivers;

using Presentation;

namespace Presentation.Screens.Driver
{
    public partial class DriverDashboardForm : BaseForm
    {
        // Dependencies
        private readonly DriverShell _shell;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareService;

        // State
        private TripDto _trip;
        private Driver _driver;
        private System.Timers.Timer _timer;
        private int _isRefreshing;
        private decimal _currentCommissionRate;
        private DateTime _lastDriverLoad;
        private const int DRIVER_CACHE_SECONDS = 15;

        // UI components
        private MapControl _mapControl;
        private Panel _infoPanel;
        private Label _statusLabel;
        private Panel _stepBar;
        private PictureBox[] _stepDots;
        private Label[] _stepLabels;
        private Button _actionButton;
        private Panel _paymentPanel;
        private Label _fareLabel;
        private Label _commissionLabel;
        private Label _netEarningsLabel;
        private Button _confirmPaymentButton;

        // Default constructor for designer and instantiation without dependencies
        public DriverDashboardForm()
        {
            InitializeComponent();
            // UI will be initialized when form is shown with actual data
        }

        public DriverDashboardForm(
            DriverShell shell,
            ITripService tripService,
            IUserService userService,
            ISimulationService simulationService,
            IFareService fareService)
        {
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));

            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Adjust splitter distance to 75%
            mainSplit.SplitterDistance = this.Width * 3 / 4;

            // Initialize step bar and payment panel
            InitializeStepBar();
            InitializePaymentPanel();

            // Setup map event
            _mapControl.MapClicked += OnMapRightClick;
        }

        private void InitializeMap()
        {
            _mapControl.MapClicked += OnMapRightClick; // Right-click for location update
        }

        private void InitializeInfoPanel()
        {
            // Status label setup
            _statusLabel.Text = "Loading...";
            _statusLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            _statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            _statusLabel.Dock = DockStyle.Fill;

            // Action button setup
            _actionButton.Text = "Action";
            _actionButton.Dock = DockStyle.Fill;
            _actionButton.Height = 40;
            _actionButton.BackColor = Color.LightBlue;
            _actionButton.Click += OnActionClicked;

            // Initialize step bar and payment panel (they use existing controls)
            InitializeStepBar();
            InitializePaymentPanel();
        }

        private void InitializeStepBar()
        {
            _stepBar.Dock = DockStyle.Fill;

            _stepDots = new PictureBox[4];
            _stepLabels = new Label[4];

            string[] stepTexts = { "Nhận", "Đến đón", "Bắt đầu", "Xong" };

            for (int i = 0; i < 4; i++)
            {
                _stepDots[i] = new PictureBox
                {
                    Size = new Size(20, 20),
                    Location = new Point(50 + i * 80, 10),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                _stepBar.Controls.Add(_stepDots[i]);

                _stepLabels[i] = new Label
                {
                    Text = stepTexts[i],
                    Location = new Point(30 + i * 80, 35),
                    AutoSize = true,
                    Font = new Font("Arial", 8)
                };
                _stepBar.Controls.Add(_stepLabels[i]);
            }
        }

        private void InitializePaymentPanel()
        {
            _paymentPanel.Dock = DockStyle.Fill;
            _paymentPanel.Visible = false;

            _fareLabel.Text = "Tổng tiền: -- đ";
            _fareLabel.Dock = DockStyle.Top;

            _commissionLabel.Text = "Hoa hồng: -- đ";
            _commissionLabel.Dock = DockStyle.Top;

            _netEarningsLabel.Text = "Thu nhập: -- đ";
            _netEarningsLabel.Dock = DockStyle.Top;

            _confirmPaymentButton.Text = "Xác nhận thanh toán";
            _confirmPaymentButton.Dock = DockStyle.Bottom;
            _confirmPaymentButton.Height = 30;
            _confirmPaymentButton.BackColor = Color.LightGreen;
            _confirmPaymentButton.Click += OnConfirmPaymentClicked;
        }

        // Lifecycle methods
        public void OnNavigatedTo(TripDto trip = null)
        {
            _trip = trip ?? _shell.CurrentTrip;

            if (_trip == null)
            {
                ShowEmptyState();
                return;
            }

            // Load commission rate
            LoadCommissionRate();

            // Start timer
            StartTimer();

            // Initial refresh
            _ = RefreshAsync();

            // Start simulation if matched
            if (_trip.Status == TripStatus.Matched)
            {
                // TODO: Start simulation to pickup location
            }
        }

        public void OnNavigatingFrom()
        {
            StopTimer();
        }

        private void StartTimer()
        {
            _timer = new System.Timers.Timer(5000); // 5 seconds
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        private async void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Simulation tick
            await _simulationService.Tick();

            // Refresh UI
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            if (Interlocked.Exchange(ref _isRefreshing, 1) == 1)
            {
                return;
            }

            try
            {
                // Step 1: Sync trip
                // TODO: _trip = await _tripService.GetTripAsync(_trip.Id);
                _shell.SetCurrentTrip(_trip);

                // Step 2: Load driver (with cache)
                if (_driver == null || (DateTime.Now - _lastDriverLoad).TotalSeconds > DRIVER_CACHE_SECONDS)
                {
                    // TODO: _driver = await Task.Run(() => _userService.GetDriverById(_trip.DriverId.Value));
                    _lastDriverLoad = DateTime.Now;
                }

                // Step 3: Apply UI
                ApplyTripToUI();

                // Step 4: Map update
                RefreshMap();
            }
            catch (Exception ex)
            {
                // Handle errors
                Console.WriteLine($"Refresh error: {ex.Message}");
            }
            finally
            {
                Interlocked.Exchange(ref _isRefreshing, 0);
            }
        }

        private void ApplyTripToUI()
        {
            if (_trip == null) return;

            // Update status
            UpdateStatusDisplay();

            // Update step bar
            UpdateStepBar();

            // Update action button
            UpdateActionButton();

            // Show/hide payment panel
            bool isCompleted = _trip.Status == TripStatus.Completed || _trip.Status == TripStatus.Cancelled;
            _paymentPanel.Visible = isCompleted;
            _actionButton.Visible = !isCompleted;

            if (isCompleted)
            {
                CalculatePayment();
            }
        }

        private void UpdateStatusDisplay()
        {
            string statusText = _trip.Status switch
            {
                TripStatus.Matched => "Chờ khách",
                TripStatus.Started => "Đang chạy",
                TripStatus.Completed => "Kết thúc",
                TripStatus.Cancelled => "Hủy",
                _ => "Unknown"
            };

            _statusLabel.Text = statusText;
        }

        private void UpdateStepBar()
        {
            int activeStep = _trip.Status switch
            {
                TripStatus.Matched => 2,
                TripStatus.Started => 3,
                TripStatus.Completed => 4,
                _ => 0
            };

            for (int i = 0; i < 4; i++)
            {
                string state = i < activeStep - 1 ? "done" :
                              i == activeStep - 1 ? "active" : "todo";
                SetStepDot(i, state);
            }
        }

        private void SetStepDot(int index, string state)
        {
            Color color = state switch
            {
                "done" => Color.Green,
                "active" => Color.Blue,
                "todo" => Color.Gray,
                _ => Color.Gray
            };

            var bmp = new Bitmap(20, 20);
            using (var g = Graphics.FromImage(bmp))
            {
                g.FillEllipse(new SolidBrush(color), 0, 0, 20, 20);
            }
            _stepDots[index].Image = bmp;
        }

        private void UpdateActionButton()
        {
            (_actionButton.Text, _actionButton.Enabled) = _trip.Status switch
            {
                TripStatus.Matched => ("Đã đến điểm đón", true),
                TripStatus.Started => ("Hoàn thành chuyến", true),
                _ => ("Chờ...", false)
            };
        }

        private async void OnActionClicked(object sender, EventArgs e)
        {
            if (_trip == null) return;

            try
            {
                switch (_trip.Status)
                {
                    case TripStatus.Matched:
                    case TripStatus.Arrived:
                        // TODO: await _tripService.MarkArrivedAsync(_trip.Id);
                        break;
                    case TripStatus.Started:
                        // TODO: await _tripService.CompleteTripAsync(_trip.Id);
                        await _simulationService.SimulateTripProgress(_trip.Id);
                        break;
                }

                await RefreshAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculatePayment()
        {
            decimal fare = _trip.Fare.Amount;
            decimal commission = fare * _currentCommissionRate;
            decimal netEarnings = fare - commission;

            _fareLabel.Text = $"Tổng tiền: {fare:N0} đ";
            _commissionLabel.Text = $"Hoa hồng ({_currentCommissionRate:P0}): {commission:N0} đ";
            _netEarningsLabel.Text = $"Thu nhập: {netEarnings:N0} đ";
        }

        private async void LoadCommissionRate()
        {
            try
            {
                // TODO: var fareRule = await _fareService.GetFareRuleAsync(_trip.VehicleType ?? VehicleType.Motorbike);
                // _currentCommissionRate = fareRule.CommissionRate;
                _currentCommissionRate = 0.1m; // Default 10%
            }
            catch
            {
                _currentCommissionRate = 0.1m; // Fallback
            }
        }

        private void RefreshMap()
        {
            if (_driver == null || _trip == null) return;

            // Clear existing markers
            _mapControl.ClearMarkers();

            // Add markers
            _mapControl.AddDriverMarker(_driver.Position ?? new Location("Driver", "", 0, 0));
            _mapControl.AddPickupMarker(_trip.Pickup);
            _mapControl.AddDestinationMarker(_trip.Destination);

            // Set camera to follow driver
            _mapControl.SetCamera(_driver.Position ?? _trip.Pickup);

            // Draw routes
            DrawRoutes();
        }

        private void DrawRoutes()
        {
            if (_trip.Status == TripStatus.Started)
            {
                // For active trip: show full route with progress
                // TODO: Implement route splitting logic
                // Find closest point, split into traveled (gray) and remaining (blue)
            }
            else
            {
                // For en route to pickup: driver to pickup route
                if (_driver?.Position != null)
                {
                    _mapControl.DrawRoute(_driver.Position, _trip.Pickup, Color.Blue);
                }
            }
        }

        private void OnMapRightClick(object sender, Location location)
        {
            // Right-click to update driver location manually
            if (_driver != null)
            {
                // TODO: await _userService.UpdateDriverLocationAsync(_driver.Id, location);
                _driver.UpdateLocation(location);
                RefreshMap();
            }
        }

        private async void OnConfirmPaymentClicked(object sender, EventArgs e)
        {
            try
            {
                // TODO: await _tripService.ConfirmPaymentAsync(_trip.Id, _trip.Fare);
                _shell.OnTripEnded();
                _shell.NavigateTo("Dashboard"); // Navigate to dashboard, don't close
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xác nhận thanh toán: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyState()
        {
            _statusLabel.Text = "Không có chuyến nào";
            _actionButton.Visible = false;
            _paymentPanel.Visible = false;
        }
    }
}
