using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Vehicles;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    public partial class UcAuth : BaseUserControl
    {
        private readonly IUserService _userService;
        private readonly IVehicleRepository _vehicleRepository;

        public event EventHandler<User> LoginSucceeded;
        public event EventHandler<User> RegisterSucceeded;

        private bool _loginPasswordVisible;
        private bool _regPasswordVisible;

        public UcAuth(IUserService userService, IVehicleRepository vehicleRepository)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));

            InitializeComponent();
            SetupEvents();
            ShowLoginPanel();
        }

        private void SetupEvents()
        {
            btnLogin.Click += async (s, e) => await OnLoginClicked();
            btnRegister.Click += async (s, e) => await OnRegisterClicked();
            linkToRegister.Click += (s, e) => ShowRegisterPanel();
            linkToLogin.Click += (s, e) => ShowLoginPanel();

            btnToggleLoginPassword.Click += (s, e) => ToggleLoginPassword();
            btnToggleRegPassword.Click += (s, e) => ToggleRegPassword();

            cmbRole.SelectedIndexChanged += (s, e) => OnRoleChanged();
            cmbVehicleType.SelectedIndexChanged += (s, e) => OnVehicleTypeChanged();

            // Demo buttons - Quick login (chỉ fill field)
            btnDemoPassenger.Click += (s, e) => DemoLogin("0911111111", "123456");
            btnDemoDriver.Click += (s, e) => DemoLogin("0900000000", "123456");
            btnDemoAdmin.Click += (s, e) => DemoLogin("admin", "admin");

            txtLoginPhone.KeyDown += OnAuthKeyDown;
            txtLoginPassword.KeyDown += OnAuthKeyDown;
            txtRegName.KeyDown += OnAuthKeyDown;
            txtRegPhone.KeyDown += OnAuthKeyDown;
            txtRegPassword.KeyDown += OnAuthKeyDown;

            txtLoginPhone.TextChanged += (s, e) => ValidateLoginFields();
            txtLoginPassword.TextChanged += (s, e) => ValidateLoginFields();
            txtRegName.TextChanged += (s, e) => ValidateRegisterFields();
            txtRegPhone.TextChanged += (s, e) => ValidateRegisterFields();
            txtRegPassword.TextChanged += (s, e) => ValidateRegisterFields();
            txtPlate.TextChanged += (s, e) => ValidateRegisterFields();
            txtBrand.TextChanged += (s, e) => ValidateRegisterFields();
            txtModel.TextChanged += (s, e) => ValidateRegisterFields();
            txtColor.TextChanged += (s, e) => ValidateRegisterFields();
            numCapacity.ValueChanged += (s, e) => ValidateRegisterFields();

            SetupButtonHoverEffects();
        }

        private static string GetText(TextBox textBox)
        {
            return textBox.Text.Trim();
        }

        /// <summary>
        /// Đăng nhập nhanh - chỉ fill field, không auto-submit
        /// </summary>
        private void DemoLogin(string phone, string password)
        {
            txtLoginPhone.Text = phone;
            txtLoginPassword.Text = password;
            // Người dùng tự click nút Đăng nhập
        }

        private void OnAuthKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
            if (pnlLogin.Visible)
            {
                btnLogin.PerformClick();
            }
            else
            {
                btnRegister.PerformClick();
            }
        }

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
            cmbRole.SelectedIndex = 0;
            OnRoleChanged();
            txtRegName.Focus();
        }

        private void ToggleLoginPassword()
        {
            _loginPasswordVisible = !_loginPasswordVisible;
            txtLoginPassword.PasswordChar = _loginPasswordVisible ? '\0' : '*';
            btnToggleLoginPassword.Text = _loginPasswordVisible ? "Ẩn" : "Hiện";
        }

        private void ToggleRegPassword()
        {
            _regPasswordVisible = !_regPasswordVisible;
            txtRegPassword.PasswordChar = _regPasswordVisible ? '\0' : '*';
            btnToggleRegPassword.Text = _regPasswordVisible ? "Ẩn" : "Hiện";
        }

        private void OnRoleChanged()
        {
            bool isDriver = cmbRole.SelectedIndex == 1;
            pnlVehicleInfo.Visible = isDriver;

            if (!isDriver)
            {
                return;
            }

            cmbVehicleType.SelectedIndex = 0;
            OnVehicleTypeChanged();
        }

        private void OnVehicleTypeChanged()
        {
            if (cmbVehicleType.SelectedIndex == 0)
            {
                numCapacity.Value = 2;
                numCapacity.Enabled = false;
                return;
            }

            numCapacity.Minimum = 4;
            numCapacity.Maximum = 7;
            numCapacity.Value = 4;
            numCapacity.Enabled = true;
        }

        private async Task OnLoginClicked()
        {
            string phone = GetText(txtLoginPhone);
            string password = GetText(txtLoginPassword);

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                ShowWarning("Vui lòng nhập số điện thoại và mật khẩu.");
                return;
            }

            IsLoading = true;
            try
            {
                User user = await _userService.LoginAsync(phone, password);
                if (user == null)
                {
                    throw new InvalidOperationException("Số điện thoại hoặc mật khẩu không đúng.");
                }

                LoginSucceeded?.Invoke(this, user);
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message);
                txtLoginPassword.Clear();
                txtLoginPassword.Focus();
            }
            catch (Exception ex)
            {
                ShowError("Có lỗi xảy ra khi đăng nhập: " + ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task OnRegisterClicked()
        {
            IsLoading = true;
            try
            {
                string name = GetText(txtRegName);
                string phone = GetText(txtRegPhone);
                string password = GetText(txtRegPassword);
                bool isDriver = cmbRole.SelectedIndex == 1;

                if (isDriver)
                {
                    string plate = txtPlate.Text.Trim();
                    string brand = txtBrand.Text.Trim();
                    string model = txtModel.Text.Trim();
                    string color = txtColor.Text.Trim();
                    int capacity = (int)numCapacity.Value;

                    Vehicle vehicle = null;

                    try
                    {
                        vehicle = cmbVehicleType.SelectedIndex == 0
                            ? (Vehicle)new Motorbike(null, plate, brand, model, color)
                            : new Car(null, plate, brand, model, color, capacity);
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException("Thông tin xe không hợp lệ: " + ex.Message);
                    }

                    await _vehicleRepository.AddAsync(vehicle);
                    await _vehicleRepository.SaveChangesAsync();

                    var defaultLocation = new Location(
                        new Coordinate(10.7769, 106.7009),
                        new Address("District 1", string.Empty, "District 1", "Ho Chi Minh", "Vietnam"));

                    await _userService.RegisterDriverAsync(
                        name,
                        phone,
                        password,
                        "GPLX-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                        vehicle.Id,
                        defaultLocation);
                }
                else
                {
                    await _userService.RegisterPassengerAsync(name, phone, password);
                }

                User user = await _userService.LoginAsync(phone, password);
                if (user == null)
                {
                    throw new InvalidOperationException("Không thể tự động đăng nhập sau khi đăng ký.");
                }

                ShowInfo("Đăng ký thành công!");
                RegisterSucceeded?.Invoke(this, user);
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message);
            }
            catch (FormatException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("Có lỗi xảy ra khi đăng ký: " + ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ValidateLoginFields()
        {
            string phone = GetText(txtLoginPhone);
            string password = GetText(txtLoginPassword);

            ValidateControl(txtLoginPhone, !string.IsNullOrWhiteSpace(phone), "Nhập số điện thoại.");
            ValidateControl(txtLoginPassword, !string.IsNullOrWhiteSpace(password), "Nhập mật khẩu.");
        }

        private void ValidateRegisterFields()
        {
            string name = GetText(txtRegName);
            string phone = GetText(txtRegPhone);
            string password = GetText(txtRegPassword);
            bool isDriver = cmbRole.SelectedIndex == 1;

            ValidateControl(txtRegName, !string.IsNullOrWhiteSpace(name), "Nhập họ và tên.");
            ValidateControl(txtRegPhone, Regex.IsMatch(phone, "^[0-9]{9,11}$"), "Số điện thoại không hợp lệ.");
            ValidateControl(txtRegPassword, !string.IsNullOrWhiteSpace(password) && password.Length >= 6, "Mật khẩu tối thiểu 6 ký tự.");

            if (!isDriver)
            {
                ValidateControl(txtPlate, true, string.Empty);
                ValidateControl(txtBrand, true, string.Empty);
                ValidateControl(txtModel, true, string.Empty);
                ValidateControl(txtColor, true, string.Empty);
                return;
            }

            ValidateControl(txtPlate, !string.IsNullOrWhiteSpace(txtPlate.Text), "Nhập biển số xe.");
            ValidateControl(txtBrand, !string.IsNullOrWhiteSpace(txtBrand.Text), "Nhập hãng xe.");
            ValidateControl(txtModel, !string.IsNullOrWhiteSpace(txtModel.Text), "Nhập mẫu xe.");
            ValidateControl(txtColor, !string.IsNullOrWhiteSpace(txtColor.Text), "Nhập màu xe.");
        }

        private void SetupButtonHoverEffects()
        {
            SetupButtonHover(btnLogin, Color.FromArgb(0, 123, 255));
            SetupButtonHover(btnRegister, Color.FromArgb(40, 167, 69));
        }

        private static void SetupButtonHover(Button button, Color hoverColor)
        {
            if (button == null)
            {
                return;
            }

            Color normal = button.BackColor;
            button.MouseEnter += (s, e) => button.BackColor = hoverColor;
            button.MouseLeave += (s, e) => button.BackColor = normal;
        }
    }
}
