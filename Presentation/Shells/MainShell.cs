using Application.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

using Presentation;

namespace Presentation.Shells
{
    public partial class MainShell : BaseShell
    {
        private readonly Func<Form> _loginFormFactory;
        private readonly Func<Form> _registerFormFactory;
        private readonly Func<Form> _passengerShellFactory;
        private readonly Func<Form> _driverShellFactory;
        private readonly IUserService _userService;





        public MainShell(
            Func<Form> loginFormFactory,
            Func<Form> registerFormFactory,
            Func<Form> passengerShellFactory,
            Func<Form> driverShellFactory,
            IUserService userService)
        {
            _loginFormFactory = loginFormFactory ?? throw new ArgumentNullException(nameof(loginFormFactory));
            _registerFormFactory = registerFormFactory ?? throw new ArgumentNullException(nameof(registerFormFactory));
            _passengerShellFactory = passengerShellFactory ?? throw new ArgumentNullException(nameof(passengerShellFactory));
            _driverShellFactory = driverShellFactory ?? throw new ArgumentNullException(nameof(driverShellFactory));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            InitializeComponent();
            InitForm();
            SetupEvents();
        }

        private void InitForm()
        {
            Text = "OOP Ride-Hailing";
            Size = new Size(540, 620);
            MinimumSize = new Size(420, 540);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10.5f);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            BackColor = Color.LightGray; // AppTheme.PageBg
        }

        private void SetupEvents()
        {
            ButtonLogin.Click += (s, e) => OnLoginClicked();
            ButtonRegister.Click += (s, e) => OnRegisterClicked();
            ButtonDual.Click += async (s, e) => await OnOpenDualClicked();
            ButtonExit.Click += (s, e) => OnExitClicked();
        }

        // Event handlers
        private void OnLoginClicked()
        {
            using (var loginForm = _loginFormFactory())
            {
                Hide();
                var result = loginForm.ShowDialog();
                Show();
                if (result == DialogResult.OK)
                    MessageBox.Show("Authenticated successfully.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OnRegisterClicked()
        {
            using (var regForm = _registerFormFactory())
            {
                Hide();
                regForm.ShowDialog();
                Show();
            }
        }

        private void OnExitClicked() => this.Close();

        private async System.Threading.Tasks.Task OnOpenDualClicked()
        {
            var passengerForm = _passengerShellFactory();
            var driverForm = _driverShellFactory();

            passengerForm.Show();
            driverForm.Show();
        }
    }
}
