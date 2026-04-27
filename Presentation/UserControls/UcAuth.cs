using Application.Interfaces;
using Domain.Entities.Users;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Man hinh xac thuc tap trung: Login + Register.
    /// Su dung hai Panel an/hien luan phien thay vi TabControl.
    /// </summary>    
    public partial class UcAuth : BaseUserControl
    {
        private readonly IUserService _userService;

        public event EventHandler<User> LoginSucceeded;
        public event EventHandler<User> RegisterSucceeded;

        public UcAuth(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

            txtLoginPhone.KeyDown += OnAuthKeyDown;
            txtLoginPassword.KeyDown += OnAuthKeyDown;
            txtRegName.KeyDown += OnAuthKeyDown;
            txtRegPhone.KeyDown += OnAuthKeyDown;
            txtRegPassword.KeyDown += OnAuthKeyDown;
        }

        private void OnAuthKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                if (pnlLogin.Visible)
                    btnLogin.PerformClick();
                else
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
            txtRegName.Focus();
        }

        private async System.Threading.Tasks.Task OnLoginClicked()
        {
            string phone = txtLoginPhone.Text.Trim();
            string password = txtLoginPassword.Text;

            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                ShowWarning("Vui long nhap so dien thoai va mat khau.");
                return;
            }

            IsLoading = true;
            try
            {
                User user = await _userService.LoginAsync(phone, password);
                if (user != null)
                {
                    LoginSucceeded?.Invoke(this, user);
                }
                else
                {
                    ShowError("So dien thoai hoac mat khau khong dung.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Dang nhap that bai: " + ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnRegisterClicked()
        {
            string name = txtRegName.Text.Trim();
            string phone = txtRegPhone.Text.Trim();
            string password = txtRegPassword.Text;
            string role = cmbRole.SelectedItem?.ToString() ?? "Passenger";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                ShowWarning("Vui long nhap day du thong tin.");
                return;
            }

            IsLoading = true;
            try
            {
                User user;
                if (role == "Driver")
                {
                    var defaultLocation = new Domain.ValueObjects.Location(
                        new Domain.ValueObjects.Coordinate(10.7769, 106.7009),
                        new Domain.ValueObjects.Address("District 1", "", "District 1", "Ho Chi Minh", "Vietnam"));
                    user = await _userService.RegisterDriverAsync(name, phone, password, "GPLX-" + Guid.NewGuid().ToString().Substring(0, 8), Guid.NewGuid(), defaultLocation);
                }
                else
                {
                    user = await _userService.RegisterPassengerAsync(name, phone, password);
                }

                if (user != null)
                {
                    ShowInfo("Dang ky thanh cong!");
                    RegisterSucceeded?.Invoke(this, user);
                }
            }
            catch (Exception ex)
            {
                ShowError("Dang ky that bai: " + ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

