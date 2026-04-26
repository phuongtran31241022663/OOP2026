using Domain.Entities.Users;
using Domain.Entities;
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

                if (cmbType.SelectedItem != null && cmbType.SelectedItem.ToString() == "Driver")
                {
                    Coordinate coordinate = new Coordinate(10.7769, 106.7009);
                    Address address = new Address("HCM", "", "District 1", "Ho Chi Minh", "Vietnam");
                    Location defaultLocation = new Location(coordinate, address);
                    await _userService.RegisterDriverAsync(
                        txtName.Text,
                        txtPhone.Text,
                        txtPassword.Text,
                        "GPLX-NEW",
                        Guid.NewGuid(),
                        defaultLocation);
                }
                else
                {
                    await _userService.RegisterPassengerAsync(txtName.Text, txtPhone.Text, txtPassword.Text);
                }

                MessageBox.Show("ÄÄƒng kÃ½ thÃ nh cÃ´ng");
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

