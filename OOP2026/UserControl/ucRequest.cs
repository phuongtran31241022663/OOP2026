using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucRequest : UserControl
    {
        private Drv _driver;
        private IDrvCmd _driverCmd;
        private ITripCmd _tripCmd;
        private ITripQry _tripQuery;
        private Trip _currentTrip;
        private INotiSvc _notificationSvc;

        public event EventHandler<Trip> TripAccepted;
        public event EventHandler<Trip> TripRejected;

        public ucRequest()
        {
            InitializeComponent();
            this.Disposed += UcRequest_Disposed;
        }

        public void Initialize(Drv driver, IDrvCmd driverCmd, ITripCmd tripCmd,
                               ITripQry tripQuery, INotiSvc notificationSvc)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _driverCmd = driverCmd ?? throw new ArgumentNullException(nameof(driverCmd));
            _tripCmd = tripCmd ?? throw new ArgumentNullException(nameof(tripCmd));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));
            _notificationSvc = notificationSvc ?? throw new ArgumentNullException(nameof(notificationSvc));

            _driverCmd.DriverStatusChanged += OnDriverStatusChanged;
            _tripCmd.TripStatusChanged += OnTripStatusChanged;
        }

        // ─────────────────────────────────────────────────────
        //  HIỂN THỊ DANH SÁCH CHUYẾN CHỜ
        // ─────────────────────────────────────────────────────

        public void UpdateRequests(IEnumerable<Trip> pendingTrips)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateRequests(pendingTrips)));
                return;
            }

            ClearAndDisposeRequestsPanel();
            if (pendingTrips == null) return;

            int count = 0;
            foreach (var trip in pendingTrips)
            {
                if (trip == null) continue;

                var card = new ucTripCard();
                card.Trip = trip;
                card.Tag = trip;
                card.TripAccepted += OnCardTripAccepted;
                card.TripRejected += OnCardTripRejected;
                flpRequests.Controls.Add(card);
                count++;
            }

            lblTitle.Text = $"Chuyến được đề nghị ({count})";
            lblTitle.Visible = true;
            flpRequests.Visible = true;
            tlpActiveTrip.Visible = false;
        }

        // ─────────────────────────────────────────────────────
        //  XỬ LÝ SỰ KIỆN THẺ CHUYẾN ĐI
        // ─────────────────────────────────────────────────────

        private async void OnCardTripAccepted(object sender, EventArgs e)
        {
            if (!(sender is ucTripCard card) || !(card.Tag is Trip trip)) return;

            try
            {
                await _driverCmd.AcceptTripAsync(_driver.Id, trip.Id);
                MessageBox.Show(
                    _notificationSvc.GetDriverAcceptedMessage(),
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadActiveTripFromService();
                TripAccepted?.Invoke(this, trip);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nhận chuyến: {ex.Message}", "Hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void OnCardTripRejected(object sender, EventArgs e)
        {
            if (!(sender is ucTripCard card) || !(card.Tag is Trip trip)) return;

            try
            {
                await _driverCmd.RejectTripAsync(_driver.Id, trip.Id);
                TripRejected?.Invoke(this, trip);

                card.TripAccepted -= OnCardTripAccepted;
                card.TripRejected -= OnCardTripRejected;
                flpRequests.Controls.Remove(card);
                card.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi từ chối chuyến: {ex.Message}", "Hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────
        //  HIỂN THỊ CHUYẾN ĐI ĐANG CHẠY
        // ─────────────────────────────────────────────────────

        public void ShowActiveTrip(Trip trip)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowActiveTrip(trip)));
                return;
            }

            _currentTrip = trip;
            lblTitle.Visible = false;
            flpRequests.Visible = false;
            tlpActiveTrip.Visible = true;

            if (trip == null) return;

            string pickup = trip.TripRoute?.Pickup?.Addr?.Name ?? "Đang cập nhật...";
            string dropoff = trip.TripRoute?.Dropoff?.Addr?.Name ?? "Đang cập nhật...";

            lblRouteInfo.Text = $"📍 Điểm đón: {pickup}\n\n🏁 Điểm trả: {dropoff}";
            lblTripSummary.Text = $"{trip.TripRoute?.Distance:F1} km  •  {trip.TripFare?.TotalAmount:N0}đ";

            // Ánh xạ State Machine → nút bấm
            btnArrived.Visible = trip.IsMatched();
            btnStart.Visible = trip.IsArrived();
            btnComplete.Visible = trip.IsStarted() || trip.IsDropOff();
        }

        public async Task LoadActiveTripFromService()
        {
            if (_driver == null || _tripQuery == null) return;

            var activeTrip = await _tripQuery.GetActiveTripForDriverAsync(_driver.Id);
            if (activeTrip != null)
            {
                ShowActiveTrip(activeTrip);
            }
            else
            {
                _currentTrip = null;
                lblTitle.Visible = true;
                flpRequests.Visible = true;
                tlpActiveTrip.Visible = false;
            }
        }

        // ─────────────────────────────────────────────────────
        //  ĐIỀU HƯỚNG TRẠNG THÁI CHUYẾN ĐI
        // ─────────────────────────────────────────────────────

        private async void BtnArrived_Click(object sender, EventArgs e)
        {
            if (_currentTrip == null || _tripCmd == null) return;
            try
            {
                await _tripCmd.ArrivedPickupAtPickupAsync(_currentTrip.Id);
                await _driverCmd.UpdateLocationAsync(_driver.Id, _currentTrip.TripRoute.Pickup);
                MessageBox.Show(
                    _notificationSvc.GetArrivedPickupPickupMessage(),
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadActiveTripFromService();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            if (_currentTrip == null || _tripCmd == null) return;
            try
            {
                await _tripCmd.StartTripAsync(_currentTrip.Id);
                MessageBox.Show(
                    _notificationSvc.GetDriverStartTripMessage(),
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadActiveTripFromService();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnComplete_Click(object sender, EventArgs e)
        {
            if (_currentTrip == null || _tripCmd == null) return;
            try
            {
                if (_currentTrip.IsStarted())
                {
                    await _tripCmd.ArrivedPickupAtDropoffAsync(_currentTrip.Id);
                    _currentTrip = await _tripQuery.GetActiveTripForDriverAsync(_driver.Id);
                }

                if (_currentTrip != null && _currentTrip.IsDropOff())
                {
                    await _tripCmd.CompleteTripAsync(_currentTrip.Id);
                    MessageBox.Show(
                        _notificationSvc.GetDriverCompleteTripMessage(),
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Chưa thể hoàn thành chuyến. Vui lòng kiểm tra lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                await LoadActiveTripFromService();
            }
        }

        public async Task RefreshAsync()
        {
            await LoadActiveTripFromService();
        }

        // ─────────────────────────────────────────────────────
        //  OBSERVER: DriverStatusChanged
        //  ĐÃ SỬA: Không dùng await bên trong Invoke synchronous.
        //  Chỉ marshal UI update về UI thread, rồi return.
        // ─────────────────────────────────────────────────────

        private void OnDriverStatusChanged(object sender, DriverStatusChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => OnDriverStatusChanged(sender, e)));
                return;
            }

            string message = _notificationSvc.GetDriverNotificationMessage(e);
            MessageBox.Show(message, "Thông báo tài xế",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─────────────────────────────────────────────────────
        //  OBSERVER: TripStatusChanged
        //  ĐÃ SỬA: Tách await ra ngoài Invoke, dùng BeginInvoke
        //  để không block UI thread.
        // ─────────────────────────────────────────────────────

        private async void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            if (_currentTrip == null || e.TripId != _currentTrip.Id) return;

            // Xử lý thông báo riêng cho tài xế khi khách hủy
            if (e.NewStatus == TripStatus.Cancelled)
            {
                string cancelMsg = _notificationSvc.GetDriverCancelledMessage();
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                        MessageBox.Show(cancelMsg, "Chuyến đi bị hủy",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)));
                }
                else
                {
                    MessageBox.Show(cancelMsg, "Chuyến đi bị hủy",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // Reload data trên background thread trước
            await LoadActiveTripFromService();

            // Các trạng thái khác vẫn hiện thông báo chung
            if (e.NewStatus != TripStatus.Cancelled)
            {
                string message = _notificationSvc.GetPassengerNotificationMessage(e);
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                        MessageBox.Show(message, "Thông báo chuyến đi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)));
                }
                else
                {
                    MessageBox.Show(message, "Thông báo chuyến đi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ─────────────────────────────────────────────────────
        //  MEMORY MANAGEMENT
        // ─────────────────────────────────────────────────────

        private void ClearAndDisposeRequestsPanel()
        {
            for (int i = flpRequests.Controls.Count - 1; i >= 0; i--)
            {
                if (flpRequests.Controls[i] is ucTripCard card)
                {
                    card.TripAccepted -= OnCardTripAccepted;
                    card.TripRejected -= OnCardTripRejected;
                    flpRequests.Controls.RemoveAt(i);
                    card.Dispose();
                }
            }
            flpRequests.Controls.Clear();
        }

        private void UcRequest_Disposed(object sender, EventArgs e)
        {
            ClearAndDisposeRequestsPanel();
            if (_driverCmd != null) _driverCmd.DriverStatusChanged -= OnDriverStatusChanged;
            if (_tripCmd != null) _tripCmd.TripStatusChanged -= OnTripStatusChanged;
        }
    }
}