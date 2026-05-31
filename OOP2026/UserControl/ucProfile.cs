using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucProfile : UserControl
    {
        private Usr _user;
        private IUsrSvc _userService;
        private bool _isEditMode = false;

        public ucProfile()
        {
            InitializeComponent();
        }

        public void Initialize(Usr user, IUsrSvc userService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            LoadUserProfile();
        }

        // ─────────────────────────────────────────────────────
        //  NẠP THÔNG TIN NGƯỜI DÙNG
        // ─────────────────────────────────────────────────────

        private void LoadUserProfile()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(LoadUserProfile));
                return;
            }

            if (_user == null)
            {
                System.Diagnostics.Debug.WriteLine("[ucProfile] _user is null");
                return;
            }

            lblName.Text = $"👤 Họ tên: {_user.Name}";
            lblPhone.Text = $"📞 Số điện thoại: {_user.Phone}";
            txtName.Text = _user.Name;
            txtPhone.Text = _user.Phone;

            // Đa hình: phân nhánh theo loại tài khoản
            if (_user is Psg p)
            {
                lblTrips.Text = $"📊 Tổng chuyến đi: {p.TotTrip} chuyến";
                lblLicense.Visible = false;
                lblRating.Visible = false;
                btnEdit.BackColor = Color.White;
            }
            else if (_user is Drv d)
            {
                lblTrips.Text = $"📊 Tổng chuyến đi: {d.TotTrip}";
                lblLicense.Text = $"🪪 Số GPLX: {d.LicNo}";
                lblLicense.Visible = true;
                lblRating.Text = $"⭐ Đánh giá: {d.AvgRat:F1} ({d.TotalReviews} đánh giá)";
                lblRating.Visible = true;
                btnEdit.BackColor = Color.White;
            }
            else
            {
                lblTrips.Text = "📊 Tổng chuyến đi: N/A";
                lblLicense.Visible = false;
                lblRating.Visible = false;
            }

            pnlAvatarBg?.Invalidate();
        }

        // ─────────────────────────────────────────────────────
        //  SỬA / LƯU THÔNG TIN
        // ─────────────────────────────────────────────────────

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_user == null || _userService == null) return;

            if (!_isEditMode)
            {
                _isEditMode = true;
                btnEdit.Text = "💾 Lưu thay đổi";
                btnEdit.BackColor = Color.LightGreen;
                txtName.Visible = true;
                txtPhone.Visible = true;
                lblName.Visible = false;
                lblPhone.Visible = false;
                return;
            }

            // ── Lưu ────────────────────────────────────────────
            string newName = txtName.Text.Trim();
            string newPhone = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newPhone))
            {
                MessageBox.Show("Tên và số điện thoại không được để trống.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnEdit.Enabled = false;
            btnEdit.Text = "⌛ Đang lưu...";

            try
            {
                await _userService.UpdateProfileAsync(_user.Id, newName, newPhone);
                await RefreshProfileAsync();

                _isEditMode = false;
                btnEdit.Text = "✏️ Chỉnh sửa thông tin";
                btnEdit.BackColor = Color.White;
                txtName.Visible = false;
                txtPhone.Visible = false;
                lblName.Visible = true;
                lblPhone.Visible = true;

                // ĐÃ SỬA: MessageBox.Show(text, caption)
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEdit.Enabled = true;
            }
        }

        // ─────────────────────────────────────────────────────
        //  ĐỔI MẬT KHẨU
        // ─────────────────────────────────────────────────────

        private async void btnChangePass_Click(object sender, EventArgs e)
        {
            if (_user == null || _userService == null) return;

            string oldPass = txtOldPass.Text;
            string newPass = txtNewPass.Text;

            if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu cũ và mật khẩu mới.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnChangePass.Enabled = false;
            string originalText = btnChangePass.Text;
            btnChangePass.Text = "⌛ Đang xử lý...";

            try
            {
                await _userService.ChangePasswordAsync(_user.Id, oldPass, newPass);

                // ĐÃ SỬA: MessageBox.Show(text, caption)
                MessageBox.Show("Đổi mật khẩu tài khoản thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi bảo mật",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtOldPass.Text = string.Empty;
                txtNewPass.Text = string.Empty;
                btnChangePass.Text = originalText;
                btnChangePass.Enabled = true;
            }
        }

        // ─────────────────────────────────────────────────────
        //  LÀM MỚI PROFILE
        // ─────────────────────────────────────────────────────

        public async Task RefreshProfileAsync()
        {
            if (_user == null || _userService == null) return;

            try
            {
                var freshUser = await _userService.RefreshUserAsync(_user.Id);
                if (freshUser != null)
                {
                    _user = freshUser;
                    LoadUserProfile();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucProfile] Lỗi làm mới profile: {ex.Message}");
            }
        }
    }
}