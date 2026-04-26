using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using Application.Interfaces;

using System;
using System.Windows.Forms;

namespace Presentation.Screens.Auth
{
    public partial class LoginForm : BaseForm
    {
        private readonly IUserService _userService;

        public User AuthenticatedUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(IUserService userService) : this()
        {
            _userService = userService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (_userService == null)
                    throw new InvalidOperationException("User service not initialized.");

                AuthenticatedUser = await _userService.LoginAsync(txtPhone.Text, txtPassword.Text);
                MessageBox.Show("ÄÄƒng nháº­p thÃ nh cÃ´ng");
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

