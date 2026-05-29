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

        public FrmDriverAuth(IUsrSvc userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            InitializeComponent();
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


        private void lnkToLogin_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
            txtLoginPhone.Focus();
        }

        private void CmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            numCapacity.Enabled = cmbVehicleType.SelectedIndex != 0;
            if (cmbVehicleType.SelectedIndex == 0) numCapacity.Value = 2;
        }


        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string phone = txtLoginPhone.Text;
            string password = txtLoginPassword.Text;

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
                string name = txtRegName.Text;
                string phone = txtRegPhone.Text;
                string password = txtRegPassword.Text;

                // Thu thập thông tin phương tiện
                string plate = txtPlate.Text;
                string brand = txtBrand.Text;
                string model = txtModel.Text;
                string color = txtColor.Text;
                int capacity = (int)numCapacity.Value;
                VehicleType vt = cmbVehicleType.SelectedIndex == 0 ? VehicleType.Moto : VehicleType.Car;

                await _userService.RegisterDriverAsync(
                    name, phone, password, plate.Trim(), vt, plate.Trim(), brand.Trim(), model.Trim(), color.Trim(), (int)numCapacity.Value,
                    new Loc(new Coord(10.762, 106.660), new Addr("Vị trí mặc định", "", "", "Hồ Chí Minh", "Việt Nam"))
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

        private void btnToggleLoginPassword_Click(object sender, EventArgs e)
        {
            _regPasswordVisible = !_regPasswordVisible;
            btnToggleRegPassword.Text = _regPasswordVisible ? "👁️" : "🙈";
        }

        private void linkToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            txtRegName.Focus();
        }
    }
}
