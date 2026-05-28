using System;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucDriverCard : UserControl
    {
        // Sự kiện thông báo ra bên ngoài (Form cha) để kích hoạt tính năng gọi điện
        public event EventHandler<string> CallClicked;

        private Drv _driver;
        private Veh _vehicle;

        /// <summary>
        /// Thuộc tính nhận dữ liệu Tài xế và tự động cập nhật giao diện (Không dùng Lambda)
        /// </summary>
        public Drv Drv
        {
            get
            {
                return _driver;
            }
            set
            {
                // Chỉ cập nhật và vẽ lại giao diện nếu đối tượng truyền vào thực sự thay đổi
                if (_driver != value)
                {
                    _driver = value;
                    UpdateCardUI();
                }
            }
        }

        /// <summary>
        /// Thuộc tính nhận dữ liệu Phương tiện đi kèm của tài xế (Không dùng Lambda)
        /// </summary>
        public Veh Veh
        {
            get
            {
                return _vehicle;
            }
            set
            {
                // Chỉ cập nhật nếu phương tiện có sự thay đổi
                if (_vehicle != value)
                {
                    _vehicle = value;
                    UpdateCardUI();
                }
            }
        }

        public ucDriverCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// GIẢI PHÁP TỐI ƯU HIỆU NĂNG: Hàm nạp đồng thời cả Drv và Veh, 
        /// giúp UI chỉ cần tính toán và render vẽ lại duy nhất một lần (Chống giật khung hình).
        /// </summary>
        public void Initialize(Drv driver, Veh vehicle)
        {
            _driver = driver;
            _vehicle = vehicle;
            UpdateCardUI();
        }

        /// <summary>
        /// Hàm nội bộ chịu trách nhiệm đẩy dữ liệu từ Backend Domain lên các nhãn hiển thị (UI)
        /// </summary>
        private void UpdateCardUI()
        {
            // Nếu chưa được nạp dữ liệu tài xế, giữ giao diện trống hoặc ẩn đi
            if (_driver == null)
            {
                return;
            }

            lblName.Text = _driver.Name;
            lblPhone.Text = _driver.Phone;
            lblRating.Text = "⭐ " + _driver.AvgRat.ToString("F1") + " (" + _driver.TotTrip.ToString() + " chuyến)";

            if (_vehicle != null)
            {
                lblVehicle.Text = _vehicle.Brand + " " + _vehicle.Model + " - " + _vehicle.Plate;
            }
            else
            {
                lblVehicle.Text = "Không có phương tiện";
            }

            // Ép toàn bộ Card vẽ lại cấu trúc nếu có sự thay đổi layout
            this.Invalidate(true);
        }

        // ========== EVENT HANDLERS (GIAO DIỆN THUẦN TÚY) ==========

        private void BtnCall_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu ngay tại chỗ thông qua đặc tính an toàn
            if (_driver != null && !string.IsNullOrEmpty(_driver.Phone))
            {
                // Kiểm tra xem có Form cha nào đang ký lắng nghe sự kiện này không
                if (CallClicked != null)
                {
                    // Bắn số điện thoại ra ngoài cho hệ thống viễn thông giả lập xử lý
                    CallClicked.Invoke(this, _driver.Phone);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin số điện thoại của tài xế này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
