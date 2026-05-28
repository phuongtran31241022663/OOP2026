using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucHistory : UserControl
    {
        private Usr _user;
        private ITripQry _tripQuery;
        private IRevSvc _reviewService;

        public ucHistory()
        {
            InitializeComponent();
            this.Disposed += UcHistory_Disposed;

            // Xử lý sự kiện Resize để các Card co giãn theo đúng chiều rộng màn hình
            this.flpTrips.Resize += FlpTrips_Resize;
        }

        public void InitializePassenger(Psg passenger, ITripQry tripQuery, IRevSvc reviewService = null)
        {
            _user = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));
            _reviewService = reviewService;

            SafeLoadHistoryData();
        }

        public void InitializeDriver(Drv driver, ITripQry tripQuery, IRevSvc reviewService = null)
        {
            _user = driver ?? throw new ArgumentNullException(nameof(driver));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));
            _reviewService = reviewService;

            SafeLoadHistoryData();
        }

        private async void SafeLoadHistoryData()
        {
            await LoadHistoryDataAsync();
        }

        private async Task LoadHistoryDataAsync()
        {
            if (_user == null || _tripQuery == null) return;

            try
            {
                // Thay đổi tiêu đề tạm thời để thông báo trạng thái đang tải (UX)
                lblTitle.Text = "⌛ Đang tải lịch sử chuyến đi...";

                List<Trip> trips = null;

                if (_user is Psg)
                {
                    trips = await _tripQuery.GetTripsByPassengerAsync(_user.Id);
                }
                else if (_user is Drv)
                {
                    trips = await _tripQuery.GetTripsByDriverAsync(_user.Id);
                }

                if (trips == null)
                {
                    trips = new List<Trip>();
                }

                // Khử LINQ thành công
                trips.Sort((a, b) => b.CreatedAt.CompareTo(a.CreatedAt));

                UpdateHistoryUI(trips);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hệ thống không thể tải danh sách lịch sử: {ex.Message}",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lblTitle.Text = "📜 Lịch sử chuyến đi";
            }
        }

        private void UpdateHistoryUI(List<Trip> trips)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateHistoryUI(trips)));
                return;
            }

            ClearAndDisposeAllCards();

            if (trips == null || trips.Count == 0)
            {
                ShowEmptyMessage();
                return;
            }

            // Tạm dừng vẽ giao diện để tăng tốc độ Render (Tránh giật lag UI khi có nhiều Card)
            flpTrips.SuspendLayout();

            foreach (var trip in trips)
            {
                if (trip == null) continue;

                bool canReview = _reviewService != null &&
                                 _user is Psg &&
                                 trip.Status == TripStatus.Completed;

                Guid driverId = trip.DriverId ?? Guid.Empty;
                Guid passengerId = trip.PassengerId;

                var card = new ucHistoryCard();

                // FIX: Tính toán chiều rộng động dựa trên kích thước của FlowLayoutPanel (trừ khoảng trống thanh cuộn)
                card.Width = flpTrips.ClientSize.Width - 8;
                card.Margin = new Padding(0, 0, 0, 12);

                card.Bind(trip, _reviewService, driverId, passengerId, canReview);

                flpTrips.Controls.Add(card);
            }

            flpTrips.ResumeLayout(true);
        }

        private void ShowEmptyMessage()
        {
            Label lblEmpty = new Label
            {
                Text = "Bạn chưa có chuyến đi nào trong lịch sử.",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = Typography.Font9Regular,
                ForeColor = System.Drawing.Color.Gray
            };
            flpTrips.Controls.Add(lblEmpty);
        }

        public async Task RefreshAsync()
        {
            await LoadHistoryDataAsync();
        }

        private void FlpTrips_Resize(object sender, EventArgs e)
        {
            // Tự động căn chỉnh lại chiều rộng của toàn bộ Card khi co giãn cửa sổ phần mềm
            foreach (Control ctrl in flpTrips.Controls)
            {
                if (ctrl is ucHistoryCard card)
                {
                    card.Width = flpTrips.ClientSize.Width - 8;
                }
            }
        }

        private void ClearAndDisposeAllCards()
        {
            if (flpTrips == null) return;

            for (int i = flpTrips.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = flpTrips.Controls[i];

                if (ctrl is ucHistoryCard card)
                {
                    flpTrips.Controls.Remove(card);
                    card.Dispose();
                }
                else if (ctrl is Label)
                {
                    flpTrips.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }
            flpTrips.Controls.Clear();
        }

        private void UcHistory_Disposed(object sender, EventArgs e)
        {
            this.flpTrips.Resize -= FlpTrips_Resize; // Hủy đăng ký sự kiện tránh rò rỉ bộ nhớ
            ClearAndDisposeAllCards();
        }
    }
}
