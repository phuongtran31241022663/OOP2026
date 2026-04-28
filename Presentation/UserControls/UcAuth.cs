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

        private const string PlaceholderPhone = "So dien thoai";
        private const string PlaceholderPassword = "Mat khau";
        private const string PlaceholderName = "Ho va ten";

        public UcAuth(IUserService userService, IVehicleRepository vehicleRepository)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));

            InitializeComponent();
            SetupEvents();
            SetupPlaceholders();
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

            txtLoginPhone.KeyDown += OnAuthKeyDown;
            txtLoginPassword.KeyDown += OnAuthKeyDown;
            txtRegName.KeyDown += OnAuthKeyDown;
            txtRegPhone.KeyDown += OnAuthKeyDown;
            txtRegPassword.KeyDown += OnAuthKeyDown;

            txtLoginPhone.Enter += (s, e) => RemovePlaceholder(txtLoginPhone, PlaceholderPhone, false);
            txtLoginPhone.Leave += (s, e) => SetPlaceholder(txtLoginPhone, PlaceholderPhone, false);
            txtLoginPassword.Enter += (s, e) => RemovePlaceholder(txtLoginPassword, PlaceholderPassword, true);
            txtLoginPassword.Leave += (s, e) => SetPlaceholder(txtLoginPassword, PlaceholderPassword, true);
            txtRegName.Enter += (s, e) => RemovePlaceholder(txtRegName, PlaceholderName, false);
            txtRegName.Leave += (s, e) => SetPlaceholder(txtRegName, PlaceholderName, false);
            txtRegPhone.Enter += (s, e) => RemovePlaceholder(txtRegPhone, PlaceholderPhone, false);
            txtRegPhone.Leave += (s, e) => SetPlaceholder(txtRegPhone, PlaceholderPhone, false);
            txtRegPassword.Enter += (s, e) => RemovePlaceholder(txtRegPassword, PlaceholderPassword, true);
            txtRegPassword.Leave += (s, e) => SetPlaceholder(txtRegPassword, PlaceholderPassword, true);

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

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtLoginPhone, PlaceholderPhone, false);
            SetPlaceholder(txtLoginPassword, PlaceholderPassword, true);
            SetPlaceholder(txtRegName, PlaceholderName, false);
            SetPlaceholder(txtRegPhone, PlaceholderPhone, false);
            SetPlaceholder(txtRegPassword, PlaceholderPassword, true);
        }

        private static void SetPlaceholder(TextBox textBox, string placeholder, bool isPassword)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                return;
            }

            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            if (isPassword)
            {
                textBox.PasswordChar = '\0';
            }
        }

        private void RemovePlaceholder(TextBox textBox, string placeholder, bool isPassword)
        {
            if (textBox.Text != placeholder)
            {
                return;
            }

            textBox.Text = string.Empty;
            textBox.ForeColor = Color.Black;
            if (isPassword && !_loginPasswordVisible && textBox == txtLoginPassword)
            {
                textBox.PasswordChar = '*';
            }

            if (isPassword && !_regPasswordVisible && textBox == txtRegPassword)
            {
                textBox.PasswordChar = '*';
            }
        }

        private static string GetText(TextBox textBox, string placeholder)
        {
            return textBox.Text == placeholder ? string.Empty : textBox.Text.Trim();
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
            _validationErrorProvider?.Clear();
        }

        private void ShowRegisterPanel()
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            cmbRole.SelectedIndex = 0;
            OnRoleChanged();
            txtRegName.Focus();
            _validationErrorProvider?.Clear();
        }

        private void ToggleLoginPassword()
        {
            _loginPasswordVisible = !_loginPasswordVisible;
            txtLoginPassword.PasswordChar = _loginPasswordVisible ? '\0' : '*';
            btnToggleLoginPassword.Text = _loginPasswordVisible ? "An" : "Hien";
        }

        private void ToggleRegPassword()
        {
            _regPasswordVisible = !_regPasswordVisible;
            txtRegPassword.PasswordChar = _regPasswordVisible ? '\0' : '*';
            btnToggleRegPassword.Text = _regPasswordVisible ? "An" : "Hien";
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
            string phone = GetText(txtLoginPhone, PlaceholderPhone);
            string password = GetText(txtLoginPassword, PlaceholderPassword);

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                ShowWarning("Vui long nhap so dien thoai va mat khau.");
                return;
            }

            IsLoading = true;
            try
            {
                User user = await _userService.LoginAsync(phone, password);
                if (user == null)
                {
                    throw new InvalidOperationException("So dien thoai hoac mat khau khong dung.");
                }

                LoginSucceeded?.Invoke(this, user);
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Dang nhap");
                txtLoginPassword.Clear();
                txtLoginPassword.Focus();
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Dang nhap");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Dang nhap");
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
                string name = GetText(txtRegName, PlaceholderName);
                string phone = GetText(txtRegPhone, PlaceholderPhone);
                string password = GetText(txtRegPassword, PlaceholderPassword);
                bool isDriver = cmbRole.SelectedIndex == 1;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
                {
                    throw new FormatException("Vui long nhap day du thong tin dang ky.");
                }

                if (!Regex.IsMatch(phone, "^[0-9]{9,11}$"))
                {
                    throw new FormatException("So dien thoai khong hop le.");
                }

                if (password.Length < 6)
                {
                    throw new FormatException("Mat khau phai co it nhat 6 ky tu.");
                }

                if (isDriver)
                {
                    string plate = txtPlate.Text.Trim();
                    string brand = txtBrand.Text.Trim();
                    string model = txtModel.Text.Trim();
                    string color = txtColor.Text.Trim();
                    int capacity = (int)numCapacity.Value;

                    if (string.IsNullOrWhiteSpace(plate))
                    {
                        throw new FormatException("Bien so xe khong duoc de trong.");
                    }
                    if (string.IsNullOrWhiteSpace(brand))
                    {
                        throw new FormatException("Hang xe khong duoc de trong.");
                    }
                    if (string.IsNullOrWhiteSpace(model))
                    {
                        throw new FormatException("Mau xe khong duoc de trong.");
                    }
                    if (string.IsNullOrWhiteSpace(color))
                    {
                        throw new FormatException("Mau xe khong duoc de trong.");
                    }

                    Vehicle vehicle = cmbVehicleType.SelectedIndex == 0
                        ? (Vehicle)new Motorbike(null, plate, brand, model, color)
                        : new Car(null, plate, brand, model, color, capacity);

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
                    throw new InvalidOperationException("Khong the tu dong dang nhap sau khi dang ky.");
                }

                ShowInfo("Dang ky thanh cong!");
                RegisterSucceeded?.Invoke(this, user);
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Dang ky");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Dang ky");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Dang ky");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ValidateLoginFields()
        {
            string phone = GetText(txtLoginPhone, PlaceholderPhone);
            string password = GetText(txtLoginPassword, PlaceholderPassword);

            ValidateControl(txtLoginPhone, !string.IsNullOrWhiteSpace(phone), "Nhap so dien thoai.");
            ValidateControl(txtLoginPassword, !string.IsNullOrWhiteSpace(password), "Nhap mat khau.");
        }

        private void ValidateRegisterFields()
        {
            string name = GetText(txtRegName, PlaceholderName);
            string phone = GetText(txtRegPhone, PlaceholderPhone);
            string password = GetText(txtRegPassword, PlaceholderPassword);
            bool isDriver = cmbRole.SelectedIndex == 1;

            ValidateControl(txtRegName, !string.IsNullOrWhiteSpace(name), "Nhap ho va ten.");
            ValidateControl(txtRegPhone, Regex.IsMatch(phone, "^[0-9]{9,11}$"), "So dien thoai khong hop le.");
            ValidateControl(txtRegPassword, !string.IsNullOrWhiteSpace(password) && password.Length >= 6, "Mat khau toi thieu 6 ky tu.");

            if (!isDriver)
            {
                ValidateControl(txtPlate, true, string.Empty);
                ValidateControl(txtBrand, true, string.Empty);
                ValidateControl(txtModel, true, string.Empty);
                ValidateControl(txtColor, true, string.Empty);
                return;
            }

            ValidateControl(txtPlate, !string.IsNullOrWhiteSpace(txtPlate.Text), "Nhap bien so xe.");
            ValidateControl(txtBrand, !string.IsNullOrWhiteSpace(txtBrand.Text), "Nhap hang xe.");
            ValidateControl(txtModel, !string.IsNullOrWhiteSpace(txtModel.Text), "Nhap mau xe.");
            ValidateControl(txtColor, !string.IsNullOrWhiteSpace(txtColor.Text), "Nhap mau xe.");
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
