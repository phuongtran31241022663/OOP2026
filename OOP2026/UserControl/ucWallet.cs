using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucWallet : UserControl
    {
        private Drv _driver;
        private IWalletSvc _walletService;
        private IDrvQry _driverQuery;
        private ITripQry _tripQuery;

        // CỜ HIỆU: Ngăn chặn TextChanged lan truyền khi gán bằng code
        private bool _isUpdatingProgrammatically = false;

        public ucWallet()
        {
            InitializeComponent();
        }

        public void Initialize(Drv driver, IWalletSvc walletService, IDrvQry driverQuery, ITripQry tripQuery)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _driverQuery = driverQuery ?? throw new ArgumentNullException(nameof(driverQuery));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));

            txtCustomAmount.MaxLength = 9; // Tối đa 999.999.999 VNĐ
            _ = LoadWalletDataAsync();
        }

        // ─────────────────────────────────────────────────────
        //  EVENT HANDLERS
        // ─────────────────────────────────────────────────────

        private void btn50k_Click(object sender, EventArgs e) => SetQuickAmount(50_000, btn50k);
        private void btn100k_Click(object sender, EventArgs e) => SetQuickAmount(100_000, btn100k);
        private void btn200k_Click(object sender, EventArgs e) => SetQuickAmount(200_000, btn200k);

        private void txtCustomAmount_TextChanged(object sender, EventArgs e)
        {
            if (!_isUpdatingProgrammatically)
                ClearQuickAmountSelection();
        }

        private void txtCustomAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtCustomAmount_Enter(object sender, EventArgs e)
        {
            if (txtCustomAmount.Text == "0")
                txtCustomAmount.Clear();
        }

        private void txtCustomAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomAmount.Text))
                txtCustomAmount.Text = "0";
        }

        private async void btnTopup_Click(object sender, EventArgs e)
        {
            if (_driver == null || _walletService == null)
            {
                MessageBox.Show("Dịch vụ ví tiền chưa được liên kết.", "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtCustomAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền nạp hợp lệ (lớn hơn 0).", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (amount > 50_000_000)
            {
                MessageBox.Show("Số tiền nạp mỗi lần không được vượt quá 50.000.000đ.", "Quy định ví",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            amount = Math.Floor(amount);

            btnTopup.Enabled = false;
            string originalText = btnTopup.Text;
            btnTopup.Text = "⌛ Đang kết nối ngân hàng...";

            try
            {
                await _walletService.DepositAsync(_driver.Id, amount);

                MessageBox.Show(
                    $"✅ Đã nạp thành công {amount:N0}đ vào ví tài khoản!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _isUpdatingProgrammatically = true;
                txtCustomAmount.Text = "0";
                _isUpdatingProgrammatically = false;

                ClearQuickAmountSelection();
                await LoadWalletDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi giao dịch nạp tiền: {ex.Message}", "Lỗi cổng thanh toán",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTopup.Text = originalText;
                btnTopup.Enabled = true;
            }
        }

        // ─────────────────────────────────────────────────────
        //  PRIVATE HELPERS
        // ─────────────────────────────────────────────────────

        private void SetQuickAmount(decimal amount, Button clickedBtn)
        {
            _isUpdatingProgrammatically = true;
            txtCustomAmount.Text = amount.ToString();
            _isUpdatingProgrammatically = false;

            StyleQuickAmountButton(btn50k, false);
            StyleQuickAmountButton(btn100k, false);
            StyleQuickAmountButton(btn200k, false);
            StyleQuickAmountButton(clickedBtn, true);
        }

        private void ClearQuickAmountSelection()
        {
            StyleQuickAmountButton(btn50k, false);
            StyleQuickAmountButton(btn100k, false);
            StyleQuickAmountButton(btn200k, false);
        }

        private void StyleQuickAmountButton(Button btn, bool isSelected)
        {
            if (btn == null) return;
            btn.BackColor = isSelected ? Colors.Adm : Colors.White;
            btn.ForeColor = isSelected ? Colors.White : Colors.Black;
            btn.FlatAppearance.BorderColor = isSelected ? Colors.Adm : Colors.LightGray;
        }

        private async Task LoadWalletDataAsync()
        {
            if (_driver == null || _walletService == null || _driverQuery == null || _tripQuery == null)
                return;

            try
            {
                var driver = await _driverQuery.GetDriverByIdAsync(_driver.Id);
                int totalTrips = await _tripQuery.GetTotalTripsForDriverAsync(_driver.Id);

                if (driver == null) return;

                if (this.InvokeRequired)
                    this.Invoke(new Action(() => UpdateWalletLabels(driver, totalTrips)));
                else
                    UpdateWalletLabels(driver, totalTrips);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucWallet] Lỗi tải ví: {ex.Message}");
            }
        }

        private void UpdateWalletLabels(Drv driver, int totalTrips)
        {
            lblBalance.Text = $"{driver.Wallet:N0}đ";
            lblIncomeValue.Text = $"💰 Thu nhập: {driver.Income:N0}đ";
            lblTripsValue.Text = $"📊 Tổng chuyến: {totalTrips}";
        }

        public async Task RefreshWalletAsync()
        {
            await LoadWalletDataAsync();
        }
    }
}