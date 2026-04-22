using Application.Interfaces;
using Application.DTOs;
using System;
using System.Windows.Forms;

namespace Presentation.Screens.Auth
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;

        public UserDto AuthenticatedUser { get; private set; }

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

                AuthenticatedUser = await _userService.Login(txtPhone.Text, txtPassword.Text);
                MessageBox.Show("Đăng nhập thành công");
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
