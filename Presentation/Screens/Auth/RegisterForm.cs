using Application.Interfaces;
using Domain.ValueObjects;
using System;
using System.Windows.Forms;

namespace Presentation.Screens.Auth
{
    public partial class RegisterForm : BaseForm
    {
        private readonly IUserService _userService;

        public RegisterForm()
        {
            InitializeComponent();
        }

        public RegisterForm(IUserService userService) : this()
        {
            _userService = userService;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (_userService == null)
                    throw new InvalidOperationException("User service not initialized.");

                if (cmbType.SelectedItem?.ToString() == "Driver")
                {
                    var defaultVehicle = new Motorbike(
                        Guid.Empty,
                        "59A1-NEW",
                        "Honda",
                        "Wave",
                        "Black");
                    var defaultLocation = new Location("HCM", "District 1", 10.7769, 106.7009);
                    await _userService.RegisterDriver(
                        txtName.Text,
                        txtPhone.Text,
                        txtPassword.Text,
                        defaultVehicle,
                        defaultLocation,
                        "GPLX-NEW");
                }
                else
                {
                    await _userService.RegisterPassenger(txtName.Text, txtPhone.Text, txtPassword.Text);
                }

                MessageBox.Show("Đăng ký thành công");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
