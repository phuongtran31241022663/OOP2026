using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;
using Domain.Enums;
using Application.Interfaces;
using Presentation.Shells;
using Application.DTOs;

namespace Presentation.Screens.Driver
{
    public partial class TripNavigationForm : Form
    {
        // Dependencies
        private readonly DriverShell _shell;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly ISimulationService _simulationService;

        // State
        private TripDto _pendingTrip;
        private bool _isLoading;
        private HashSet<Guid> _notifiedIds;

        

        public TripNavigationForm(DriverShell shell, ITripService tripService,
            IUserService userService, ISimulationService simulationService)
        {
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));

            _notifiedIds = new HashSet<Guid>();

            InitializeComponent();
            InitializeUI();
            RefreshAsync();
        }

        private void InitializeUI()
        {
            this.Text = "Driver Navigation";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            // Create main layout
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Top bar
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Stats strip
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Content area
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150)); // Log area

            // Top bar
            InitializeTopBar();
            mainLayout.Controls.Add(_topBar, 0, 0);

            // Stats strip
            InitializeStatsStrip();
            mainLayout.Controls.Add(_statsStrip, 0, 1);

            // Content area
            InitializeContentArea();
            mainLayout.Controls.Add(_contentArea, 0, 2);

            // Log area
            InitializeLogArea();
            mainLayout.Controls.Add(_logListBox, 0, 3);

            this.Controls.Add(mainLayout);
        }

        private void InitializeTopBar()
        {
            _topBar = new Panel { Dock = DockStyle.Fill, BackColor = Color.LightBlue };

            _titleLabel = new Label
            {
                Text = "Điều phối chuyến",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            _refreshButton = new Button
            {
                Text = "Làm mới",
                Location = new Point(this.Width - 120, 10),
                Size = new Size(100, 35),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _refreshButton.Click += OnRefreshClicked;

            _topBar.Controls.AddRange(new Control[] { _titleLabel, _refreshButton });
        }

        private void InitializeStatsStrip()
        {
            _statsStrip = new Panel { Dock = DockStyle.Fill, BackColor = Color.LightGray };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            _ReviewLabel = new Label { Text = "Review: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _totalTripsLabel = new Label { Text = "Trips: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _incomeLabel = new Label { Text = "Income: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _walletLabel = new Label { Text = "Wallet: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _revenueTodayLabel = new Label { Text = "Today: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };

            layout.Controls.Add(_ReviewLabel, 0, 0);
            layout.Controls.Add(_totalTripsLabel, 1, 0);
            layout.Controls.Add(_incomeLabel, 2, 0);
            layout.Controls.Add(_walletLabel, 3, 0);
            layout.Controls.Add(_revenueTodayLabel, 4, 0);

            _statsStrip.Controls.Add(layout);
        }

        private void InitializeContentArea()
        {
            _contentArea = new Panel { Dock = DockStyle.Fill };

            // Empty panel
            InitializeEmptyPanel();

            // Request panel
            InitializeRequestPanel();

            // Active trip panel
            InitializeActiveTripPanel();

            _contentArea.Controls.AddRange(new Control[] { _emptyPanel, _requestPanel, _activeTripPanel });
        }

        private void InitializeEmptyPanel()
        {
            _emptyPanel = new Panel { Dock = DockStyle.Fill, Visible = false };

            _emptyMessageLabel = new Label
            {
                Text = "Không có chuyến nào",
                Font = new Font("Arial", 14, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            _emptyPanel.Controls.Add(_emptyMessageLabel);
        }

        private void InitializeRequestPanel()
        {
            _requestPanel = new Panel { Dock = DockStyle.Fill, Visible = false, BackColor = Color.LightYellow };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            _requestInfoLabel = new Label
            {
                Text = "Trip request info",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(_requestInfoLabel, 0, 0);

            var buttonPanel = new Panel { Dock = DockStyle.Fill };
            _acceptButton = new Button
            {
                Text = "Chấp nhận",
                Size = new Size(100, 35),
                Location = new Point(50, 5),
                BackColor = Color.LightGreen
            };
            _acceptButton.Click += OnAcceptClicked;

            _rejectButton = new Button
            {
                Text = "Từ chối",
                Size = new Size(100, 35),
                Location = new Point(160, 5),
                BackColor = Color.LightCoral
            };
            _rejectButton.Click += OnRejectClicked;

            buttonPanel.Controls.AddRange(new Control[] { _acceptButton, _rejectButton });
            layout.Controls.Add(buttonPanel, 0, 1);

            _requestPanel.Controls.Add(layout);
        }

        private void InitializeActiveTripPanel()
        {
            _activeTripPanel = new Panel { Dock = DockStyle.Fill, Visible = false };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Route info
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // Step bar
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Action button
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Spacer

            _routeInfoLabel = new Label
            {
                Text = "Route info",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(_routeInfoLabel, 0, 0);

            InitializeStepBar();
            layout.Controls.Add(_stepBar, 0, 1);

            _actionButton = new Button
            {
                Text = "Action",
                Dock = DockStyle.Fill,
                Height = 40,
                BackColor = Color.LightBlue
            };
            _actionButton.Click += OnActionClicked;
            layout.Controls.Add(_actionButton, 0, 2);

            _activeTripPanel.Controls.Add(layout);
        }

        private void InitializeStepBar()
        {
            _stepBar = new Panel { Dock = DockStyle.Fill };

            _stepDots = new PictureBox[4];
            _stepLabels = new Label[4];

            string[] stepTexts = { "Đã ghép đôi", "Đã đến điểm đón", "Bắt đầu chuyến", "Hoàn thành" };

            for (int i = 0; i < 4; i++)
            {
                _stepDots[i] = new PictureBox
                {
                    Size = new Size(20, 20),
                    Location = new Point(50 + i * 100, 10),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                _stepBar.Controls.Add(_stepDots[i]);

                _stepLabels[i] = new Label
                {
                    Text = stepTexts[i],
                    Location = new Point(30 + i * 100, 35),
                    AutoSize = true,
                    Font = new Font("Arial", 8)
                };
                _stepBar.Controls.Add(_stepLabels[i]);
            }
        }

        private void InitializeLogArea()
        {
            _logListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                ScrollAlwaysVisible = true
            };
        }

        private async void RefreshAsync()
        {
            if (_isLoading) return;

            _isLoading = true;
            AddLog("Đang đồng bộ...");

            try
            {
                // Step 1: Sync user profile
                var driverDto = await _userService.GetDriverById(_shell.Driver.Id);
                if (driverDto != null)
                {
                    // Update shell driver if needed
                    // _shell.Driver = driverDto; // Uncomment if shell needs updating
                }

                // Step 2: Check offline
                if (_shell.Driver.Status == DriverStatus.Offline)
                {
                    ShowEmpty("Tài xế offline");
                    UpdateStats();
                    return;
                }

                // Step 3: Recovery state error
                if (_shell.Driver.Status == DriverStatus.OnTrip && _shell.CurrentTrip == null)
                {
                    await _userService.ForceRecoverDriverStatus(_shell.Driver.Id);
                    AddLog("Khôi phục trạng thái tài xế");
                }

                // Step 4: Sync current trip
                // TODO: Get active trips for driver
                // var activeTrips = await _tripService.GetActiveTripsForDriverAsync(_shell.Driver.Id);
                // if (activeTrips.Any())
                // {
                //     _shell.SetCurrentTrip(activeTrips.First());
                // }

                // Step 5: Find new trips
                // TODO: var newTrips = await _tripService.GetActiveTripsForDriverAsync(_shell.Driver.Id);
                var newTrips = new List<TripDto>(); // Placeholder

                if (newTrips.Any())
                {
                    var newTrip = newTrips.First();
                    if (!_notifiedIds.Contains(newTrip.Id))
                    {
                        _pendingTrip = newTrip;
                        _notifiedIds.Add(newTrip.Id);
                        ShowRequestCard(newTrip);
                        PlayNotificationSound();
                        AddLog("Có chuyến mới!");
                    }
                }
                else if (_shell.CurrentTrip == null)
                {
                    ShowEmpty("Không có chuyến nào");
                }
                else
                {
                    ShowActiveTrip();
                }

                UpdateStats();
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi đồng bộ: {ex.Message}");
                ShowEmpty("Lỗi kết nối");
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void ShowEmpty(string message)
        {
            _emptyMessageLabel.Text = message;
            _emptyPanel.Visible = true;
            _requestPanel.Visible = false;
            _activeTripPanel.Visible = false;
        }

        private void ShowRequestCard(TripDto trip)
        {
            _requestInfoLabel.Text = $"{trip.Pickup?.Address ?? "Unknown"} → {trip.Destination?.Address ?? "Unknown"}\n" +
                                   $"Giá: {trip.Fare:N0} đ\n" +
                                   $"Thời gian: {trip.CreatedAt:dd/MM HH:mm}";

            _emptyPanel.Visible = false;
            _requestPanel.Visible = true;
            _activeTripPanel.Visible = false;
        }

        private void ShowActiveTrip()
        {
            if (_shell.CurrentTrip == null) return;

            var trip = _shell.CurrentTrip;
            _routeInfoLabel.Text = $"{trip.Pickup?.Address ?? "Unknown"} → {trip.Destination?.Address ?? "Unknown"}\n" +
                                 $"Giá: {trip.Fare:N0} đ";

            UpdateStepBar(trip.Status);

            _emptyPanel.Visible = false;
            _requestPanel.Visible = false;
            _activeTripPanel.Visible = true;
        }

        private void UpdateStepBar(TripStatus status)
        {
            // Map status to step progress
            int currentStep = status switch
            {
                TripStatus.Matched => 1,
                TripStatus.Started => 2,
                TripStatus.Completed => 3,
                _ => 0
            };

            for (int i = 0; i < 4; i++)
            {
                string state = i < currentStep ? "done" :
                              i == currentStep ? "active" : "todo";
                SetStepDot(i, state);
            }

            // Update action button
            UpdateActionButton(status);
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

            // Create colored circle bitmap
            var bmp = new Bitmap(20, 20);
            using (var g = Graphics.FromImage(bmp))
            {
                g.FillEllipse(new SolidBrush(color), 0, 0, 20, 20);
            }
            _stepDots[index].Image = bmp;
        }

        private void UpdateActionButton(TripStatus status)
        {
            switch (status)
            {
                case TripStatus.Matched:
                    _actionButton.Text = "Đã đến điểm đón";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Arrived:
                    _actionButton.Text = "Bắt đầu chuyến";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Started:
                    _actionButton.Text = "Hoàn thành chuyến";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Completed:
                    _actionButton.Text = "Hoàn thành";
                    _actionButton.Enabled = false;
                    break;
                default:
                    _actionButton.Text = "Chờ...";
                    _actionButton.Enabled = false;
                    break;
            }
        }

        private async void OnAcceptClicked(object sender, EventArgs e)
        {
            if (_pendingTrip == null || _shell.Driver.Status != DriverStatus.Available) return;

            try
            {
                // TODO: await _tripService.TryAssignDriverAsync(_pendingTrip.Id, _shell.Driver.Id);
                // var updatedTrip = await _tripService.GetTripAsync(_pendingTrip.Id);
                // _shell.SetCurrentTrip(updatedTrip);

                // For now, simulate
                _shell.SetCurrentTrip(_pendingTrip);

                _shell.OnTripAccepted(_pendingTrip);
                _pendingTrip = null;

                ShowActiveTrip();
                AddLog("Chấp nhận chuyến thành công");
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi chấp nhận: {ex.Message}");
                MessageBox.Show($"Không thể chấp nhận chuyến: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void OnRejectClicked(object sender, EventArgs e)
        {
            if (_pendingTrip == null) return;

            try
            {
                // TODO: await _tripService.RejectTripAsync(_pendingTrip.Id, _shell.Driver.Id);
                _pendingTrip = null;
                AddLog("Từ chối chuyến");
                await Task.Delay(500); // Small delay
                RefreshAsync();
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi từ chối: {ex.Message}");
            }
        }

        private async void OnActionClicked(object sender, EventArgs e)
        {
            if (_shell.CurrentTrip == null) return;

            try
            {
                var trip = _shell.CurrentTrip;
                switch (trip.Status)
                {
                    case TripStatus.Arrived:
                        // TODO: await _tripService.MarkArrivedAsync(trip.Id);
                        AddLog("Đã đến điểm đón");
                        break;
                    case TripStatus.Matched:
                        // TODO: await _tripService.StartTripAsync(trip.Id);
                        AddLog("Bắt đầu chuyến");
                        break;
                    case TripStatus.Started:
                        // TODO: await _tripService.CompleteTripAsync(trip.Id);
                        _shell.OnTripEnded();
                        AddLog("Hoàn thành chuyến");
                        break;
                }

                RefreshAsync();
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi hành động: {ex.Message}");
            }
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            RefreshAsync();
        }

        private void UpdateStats()
        {
            var driver = _shell.Driver;
            if (driver == null) return;

            _ReviewLabel.Text = $"Review: {driver.Review.ToString("F1")}";
            _totalTripsLabel.Text = $"Trips: {0}"; // TODO: Calculate from trip history
            _incomeLabel.Text = $"Income: {0:N0} đ"; // TODO: Calculate total earnings
            _walletLabel.Text = $"Wallet: {driver.WalletAmount:N0} đ";
            _revenueTodayLabel.Text = $"Today: {0:N0} đ"; // TODO: Calculate today's revenue
        }

        private void AddLog(string message)
        {
            string logEntry = $"[{DateTime.Now:HH:mm}] {message}";
            _logListBox.Items.Add(logEntry);

            // Limit to 200 entries
            while (_logListBox.Items.Count > 200)
            {
                _logListBox.Items.RemoveAt(0);
            }

            // Auto scroll to bottom
            _logListBox.SelectedIndex = _logListBox.Items.Count - 1;
            _logListBox.ClearSelected();
        }

        private void PlayNotificationSound()
        {
            try
            {
                SystemSounds.Beep.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }

        // Public methods for external control
        public void OnTripAccepted(Trip trip)
        {
            ShowActiveTrip();
        }

        public void OnTripEnded()
        {
            ShowEmpty("Chuyến đã kết thúc");
        }
    }
 
}
