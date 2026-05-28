using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class FrmDriverAuth : Form
    {
        public event EventHandler<Usr>? LoginSucceeded;
        public event EventHandler<Usr>? RegisterSucceeded;

        private readonly IUsrSvc _userService;
        private bool _loginPasswordVisible;
        private bool _regPasswordVisible;

        // ĐÃ KHỬ: Vứt bỏ hoàn toàn IVehRepo khởi tạo UI
        public FrmDriverAuth(IUsrSvc userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            InitializeComponent();
            ShowLoginPanel();
        }

        private static string GetText(TextBox textBox) => textBox.Text.Trim();

        private void ShowLoginPanel()
        {
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
            txtLoginPhone.Focus();
        }

        private void ShowRegisterPanel()
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            txtRegName.Focus();
        }

        private void ClearRegisterForm()
        {
            txtRegName.Clear();
            txtRegPhone.Clear();
            txtRegPassword.Clear();
            txtPlate.Clear();
            txtBrand.Clear();
            txtModel.Clear();
            txtColor.Clear();
            numCapacity.Value = 4;
        }

        // ========== EVENT HANDLERS (GIAO DIỆN THUẦN TÚY) ==========

        private void BtnToggleLoginPassword_Click(object sender, EventArgs e)
        {
            _loginPasswordVisible = !_loginPasswordVisible;
            txtLoginPassword.PasswordChar = _loginPasswordVisible ? '\0' : '*';
            btnToggleLoginPassword.Text = _loginPasswordVisible ? "👁️" : "🙈";
        }

        private void BtnToggleRegPassword_Click(object sender, EventArgs e)
        {
            _regPasswordVisible = !_regPasswordVisible;
            txtRegPassword.PasswordChar = _regPasswordVisible ? '\0' : '*';
            btnToggleRegPassword.Text = _regPasswordVisible ? "👁️" : "🙈";
        }

        private void OnAuthKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;

            if (pnlLogin.Visible)
                btnLogin.PerformClick();
            else
                btnRegister.PerformClick();
        }

        private void LinkToRegister_Click(object sender, EventArgs e) => ShowRegisterPanel();

        private void lnkToLogin_Click(object sender, EventArgs e) => ShowLoginPanel();

        private void CmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tối ưu UI: Nếu chọn Xe máy (SelectedIndex == 0) thì ẩn hoặc vô hiệu hóa ô nhập số chỗ ngồi
            numCapacity.Enabled = cmbVehicleType.SelectedIndex != 0;
            if (cmbVehicleType.SelectedIndex == 0) numCapacity.Value = 2;
        }

        // ========== XỬ LÝ NGHIỆP VỤ (ĐẨY XUỐNG SERVICE VÀ TRY-CATCH) ==========

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string phone = GetText(txtLoginPhone);
            string password = GetText(txtLoginPassword);

            btnLogin.Enabled = false;
            try
            {
                Usr? user = await _userService.LoginAsync(phone, password);
                if (user == null)
                    throw new InvalidOperationException("Số điện thoại hoặc mật khẩu không chính xác.");

                LoginSucceeded?.Invoke(this, user);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoginPassword.Focus();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginPassword.Clear();
                txtLoginPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi đăng nhập: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
            try
            {
                // Thu thập thông tin cá nhân tài xế
                string name = GetText(txtRegName);
                string phone = GetText(txtRegPhone);
                string password = GetText(txtRegPassword);

                // Thu thập thông tin phương tiện
                string plate = GetText(txtPlate);
                string brand = GetText(txtBrand);
                string model = GetText(txtModel);
                string color = GetText(txtColor);
                int capacity = (int)numCapacity.Value;
                VehicleType vt = cmbVehicleType.SelectedIndex == 0 ? VehicleType.Moto : VehicleType.Car;

                // CHUẨN DDD: Đẩy toàn bộ thông tin thô xuống UsrSvc. 
                // UsrSvc sẽ tự lo việc kiểm tra dữ liệu trùng, gọi VehicleFactory, tạo Veh và Drv trong 1 luồng an toàn.
                await _userService.RegisterDriverAsync(
                    name, phone, password, plate.Trim(), plate.Trim(), brand.Trim(), model.Trim(), color.Trim(), (int)numCapacity.Value, vt, new Loc(new Coord(10.762, 106.660), new Addr("Vị trí mặc định", "", "", "Hồ Chí Minh", "Việt Nam"))
                );

                MessageBox.Show("Thành công", "Đăng ký tài xế thành công!");
                ClearRegisterForm();

                // Tự động chuyển vùng vào thẳng ứng dụng
                Usr? user = await _userService.LoginAsync(phone, password);
                if (user != null)
                {
                    RegisterSucceeded?.Invoke(this, user);
                }
            }
            catch (ArgumentException ex) // Bắt các lỗi rỗng thông tin, sai định dạng biển số/SĐT từ Domain quăng lên
            {
                MessageBox.Show(ex.Message, "Thông báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex) // Lỗi nghiệp vụ trùng SĐT hoặc trùng Biển số xe trong DB
            {
                MessageBox.Show(ex.Message, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đăng ký thất bại: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRegister.Enabled = true;
            }
        }
    }
}
