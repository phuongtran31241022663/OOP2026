using Application.Interfaces;
using Domain.Entities.Users;
using Domain.Entities;
using System;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Xem/sua ho so ca nhan + Nap vi.
    /// Hiển thị hồ sơ người dùng.
    /// </summary>
    public partial class UcProfile : BaseUserControl
    {
        private readonly User _user;
        private readonly IUserService _userService;

        public UcProfile(User user, IUserService userService)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            InitializeComponent();
            LoadProfile();
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnSave.Click += async (s, e) => await OnSaveClicked();
            btnTopUp.Click += async (s, e) => await OnTopUpClicked();
        }

        private void LoadProfile()
        {
            txtName.Text = _user.Name;
            txtPhone.Text = _user.Phone;

            if (_user is Driver driver)
            {
                lblWallet.Text = "Ví: " + (driver.Wallet?.Amount.ToString("N0") ?? "0") + "đ";
                pnlTopUp.Visible = true;
            }
            else
            {
                lblWallet.Visible = false;
                pnlTopUp.Visible = false;
            }
        }

        private async System.Threading.Tasks.Task OnSaveClicked()
        {
            IsLoading = true;
            await ExecuteWithHandlingAsync("Cập nhật hồ sơ cá nhân", async () =>
            {
                if (!string.IsNullOrWhiteSpace(txtName.Text) && txtName.Text != _user.Name)
                {
                    _user.UpdateName(txtName.Text.Trim());
                }

                if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    if (txtNewPassword.Text != txtConfirmPassword.Text)
                    {
                        throw new FormatException("Mật khẩu xác nhận không khớp.");
                    }
                    _user.ChangePassword(txtCurrentPassword.Text, txtNewPassword.Text);
                }

                await _userService.UpdateUserAsync(_user);

                ShowInfo("Cập nhật hồ sơ thành công!");
                var parent = ParentForm;
                if (parent != null)
                {
                    parent.DialogResult = DialogResult.OK;
                }
            }, () => IsLoading = false);
        }

        private async System.Threading.Tasks.Task OnTopUpClicked()
        {
            if (_user is Driver driver)
            {
                IsLoading = true;
                await ExecuteWithHandlingAsync("Nạp tiền vào ví", async () =>
                {
                    decimal amount;
                    if (!decimal.TryParse(txtTopUpAmount.Text, out amount) || amount <= 0)
                    {
                        throw new FormatException("Số tiền nạp không hợp lệ.");
                    }

                    driver.DepositToWallet(new Domain.ValueObjects.Money(amount, "VND"));
                    await _userService.UpdateUserAsync(_user);
                    lblWallet.Text = "Ví: " + driver.Wallet.Amount.ToString("N0") + "đ";
                    ShowInfo("Nạp tiền thành công!");
                }, () => IsLoading = false);
            }
        }
    }
}

