using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class FrmPassengerAuth : Form
    {
        private readonly IUsrSvc _userService;
        private readonly IVehRepo _vehicleRepo;

        public event EventHandler<Usr>? LoginSucceeded;
        public event EventHandler<Usr>? RegisterSucceeded;

        public FrmPassengerAuth(IUsrSvc userService, IVehRepo vehicleRepo)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _vehicleRepo = vehicleRepo;

            InitializeComponent();
            ShowLoginPanel();
        }

        private void ShowLoginPanel()
        {
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
            btnTabLogin.BackColor = Colors.White;
            btnTabRegister.BackColor = Color.Transparent;
            txtLoginPhone.Focus();
        }

        private void ShowRegisterPanel()
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            btnTabLogin.BackColor = Color.Transparent;
            btnTabRegister.BackColor = Colors.White;
            txtRegName.Focus();
        }

        private void BtnTabLogin_Click(object sender, EventArgs e) => ShowLoginPanel();
        private void BtnTabRegister_Click(object sender, EventArgs e) => ShowRegisterPanel();
        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        private void PnlDemoAccount_Click(object sender, EventArgs e) => FillDemoAccount();

        private void FillDemoAccount()
        {
            txtLoginPhone.Text = "0911111111";
            txtLoginPassword.Text = "123456";
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            try
            {
                string phone = txtLoginPhone.Text.Trim();
                string password = txtLoginPassword.Text;

                Usr? user = await _userService.LoginAsync(phone, password);
                if (user == null)
                    throw new InvalidOperationException("Số điện thoại hoặc mật khẩu không chính xác.");

                LoginSucceeded?.Invoke(this, user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
            try
            {
                string name = txtRegName.Text.Trim();
                string phone = txtRegPhone.Text.Trim();
                string password = txtRegPassword.Text;
                    await _userService.RegisterPassengerAsync(name, phone, password);
             
                MessageBox.Show("Thành công", "Đăng ký tài khoản thành công!");

                Usr? user = await _userService.LoginAsync(phone, password);
                if (user != null)
                {
                    RegisterSucceeded?.Invoke(this, user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnRegister.Enabled = true;
            }
        }
    }
}
