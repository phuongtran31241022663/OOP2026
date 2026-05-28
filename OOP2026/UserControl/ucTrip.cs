using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucTrip : UserControl
    {
        private Trip _currentTrip;
        private Psg _passenger;

        // Chỉ giữ lại các dịch vụ Application Service để tương tác, cấm ngắt Repository
        private ITripCmd _tripCmd;
        private ITripQry _tripQuery;
        private IUsrSvc _userService;
        private INotificationSvc _notificationSvc;

        public event EventHandler? TripCancelled;
        public event EventHandler? TripCompleted;
        public event EventHandler? RequestNewTripRequired; // Sự kiện quay lại màn hình đặt xe

        public ucTrip()
        {
            InitializeComponent();
            ApplyStyles();

            // ĐỒNG BỘ: Đăng ký qua instance control để tránh leak bộ nhớ GDI/RAM
            this.ucTripStatus.CancelClicked += UcTripStatus_CancelClicked;
            this.ucTripStatus.RateClicked += UcTripStatus_RateClicked;
            this.ucTripStatus.RetryClicked += UcTripStatus_RetryClicked;
        }

        public void Initialize(Psg passenger,
                                ITripCmd tripCmd,
                                ITripQry tripQuery,
                                IUsrSvc userService,
                                INotificationSvc notificationSvc)
        {
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _tripCmd = tripCmd ?? throw new ArgumentNullException(nameof(tripCmd));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _notificationSvc = notificationSvc ?? throw new ArgumentNullException(nameof(notificationSvc));

            _tripCmd.TripStatusChanged += OnTripStatusChanged;
        }

        private void ApplyStyles()
        {
            this.BackColor = Colors.White;
            lblTripInfo.Font = Typography.Font14Bold;
            lblTripInfo.ForeColor = Colors.Black;
        }

        /// <summary>
        /// Làm mới dữ liệu chuyến đi an toàn không dính mã LINQ
        /// </summary>
        public async Task RefreshAsync()
        {
            if (_passenger == null || _tripQuery == null) return;

            var trips = await _tripQuery.GetTripsByPassengerAsync(_passenger.Id);

            if (trips != null && trips.Count > 0)
            {
                SetTrip(trips[0]);
            }
            else
            {
                SetTrip(null);
            }
        }

        /// <summary>
        /// Nạp dữ liệu cấu trúc chuyến đi lên giao diện điều khiển
        /// </summary>
        public void SetTrip(Trip trip)
        {
            _currentTrip = trip;
            if (trip == null)
            {
                pnlNoTrip.Visible = true;
                pnlTripDetails.Visible = false;
                return;
            }

            pnlNoTrip.Visible = false;
            pnlTripDetails.Visible = true;

            // Ép trạng thái máy xuống ô hiển thị đồ họa
            ucTripStatus.CurrentStatus = trip.Status;

            // TỐI ƯU UI: Chỉ hiển thị tên Addr rút gọn thay vì toàn bộ chuỗi địa chỉ dài
            lblPickup.Text = "      Từ: " + (trip.Pickup.Addr?.Name ?? "Điểm đón");
            lblDropoff.Text = "      Đến: " + (trip.Dropoff.Addr?.Name ?? "Điểm đến");

            lblFare.Text = $"Giá: {trip.TripFare.TotalAmount:N0}đ";

            if (trip.DriverId.HasValue)
            {
                _ = LoadDriverAndVehicleInfoAsync(trip.DriverId.Value);
            }
            else
            {
                lblDriverInfo.Text = "⌛ Hệ thống đang điều phối tài xế gần nhất...";
            }
        }

        private async Task LoadDriverAndVehicleInfoAsync(Guid driverId)
        {
            try
            {
                string driverDetail = await _userService.GetDriverVehicleSummaryAsync(driverId);

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => lblDriverInfo.Text = driverDetail));
                }
                else
                {
                    lblDriverInfo.Text = driverDetail;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi nạp thông tin tài xế: {ex.Message}");
                lblDriverInfo.Text = "Tài xế: Thất bại khi tải thông tin liên kết.";
            }
        }

        // ========== EVENT HANDLERS KẾT NỐI MÁY TRẠNG THÁI ==========

        private async void UcTripStatus_CancelClicked(object? sender, EventArgs e)
        {
            if (_currentTrip == null || _tripCmd == null) return;

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn hủy chuyến đi này không?",
                "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _tripCmd.CancelTripAsync(_currentTrip.Id, "Hành khách chủ động hủy trên UI");
                    TripCancelled?.Invoke(this, EventArgs.Empty);
                    await RefreshAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể hủy chuyến đi vào lúc này: {ex.Message}",
                        "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void UcTripStatus_RateClicked(object? sender, EventArgs e)
        {
            TripCompleted?.Invoke(this, EventArgs.Empty);
        }

        private async void UcTripStatus_RetryClicked(object? sender, EventArgs e)
        {
            RequestNewTripRequired?.Invoke(this, EventArgs.Empty);
            await RefreshAsync();
        }

        private async void OnTripStatusChanged(object? sender, TripStatusChangedEventArgs e)
        {
            if (_currentTrip == null || e.TripId != _currentTrip.Id) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnTripStatusChanged(sender, e)));
                return;
            }

            await RefreshAsync();
            
            string message = _notificationSvc.GetPassengerNotificationMessage(e);
            MessageBox.Show(message, "Thông báo chuyến đi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DisposeTripEvents()
        {
            if (_tripCmd != null)
            {
                _tripCmd.TripStatusChanged -= OnTripStatusChanged;
                System.Diagnostics.Debug.WriteLine($"[QA_LOG] ucTrip {this.Handle}: Unsubscribed from TripStatusChanged");
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[QA_LOG] ucTrip {this.Handle}: HandleDestroyed called");
            DisposeTripEvents();
            base.OnHandleDestroyed(e);
        }
    }
}
