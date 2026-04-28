using Application.Interfaces;
using Domain.Entities.Users;
using Domain.Entities;
using System;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Xem/sua ho so ca nhan + Nap vi.
    /// Mo trong FrmModal.
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
                lblWallet.Text = "Vi: " + (driver.Wallet?.Amount.ToString("N0") ?? "0") + "d";
                btnTopUp.Visible = true;
            }
            else
            {
                lblWallet.Visible = false;
                btnTopUp.Visible = false;
            }
        }

        private async System.Threading.Tasks.Task OnSaveClicked()
        {
            IsLoading = true;
            await ExecuteWithHandlingAsync("Cap nhat ho so ca nhan", async () =>
            {
                if (!string.IsNullOrWhiteSpace(txtName.Text) && txtName.Text != _user.Name)
                {
                    _user.UpdateName(txtName.Text.Trim());
                }

                if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    if (txtNewPassword.Text != txtConfirmPassword.Text)
                    {
                        throw new FormatException("Mat khau xac nhan khong khop.");
                    }
                    _user.ChangePassword(txtCurrentPassword.Text, txtNewPassword.Text);
                }

                ShowInfo("Cap nhat ho so thanh cong!");
                var parent = ParentForm;
                if (parent != null)
                {
                    parent.DialogResult = DialogResult.OK;
                }

                await System.Threading.Tasks.Task.CompletedTask;
            }, () => IsLoading = false);
        }

        private async System.Threading.Tasks.Task OnTopUpClicked()
        {
            if (_user is Driver driver)
            {
                IsLoading = true;
                await ExecuteWithHandlingAsync("Nap tien vao vi", async () =>
                {
                    decimal amount;
                    if (!decimal.TryParse(txtTopUpAmount.Text, out amount) || amount <= 0)
                    {
                        throw new FormatException("So tien nap khong hop le.");
                    }

                    driver.DepositToWallet(new Domain.ValueObjects.Money(amount, "VND"));
                    lblWallet.Text = "Vi: " + driver.Wallet.Amount.ToString("N0") + "d";
                    ShowInfo("Nap tien thanh cong!");
                    await System.Threading.Tasks.Task.CompletedTask;
                }, () => IsLoading = false);
            }
        }
    }
}

