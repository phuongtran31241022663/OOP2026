using System.Text.RegularExpressions;
using static GMap.NET.Entity.OpenStreetMapGeocodeEntity;

namespace OOP2026
{
    public partial class FrmAuth : Form
    {
        private readonly IUsrSvc _usrSrv;
        private readonly IVehRepo _vehRepo;
        private readonly IUsrRepo _usrRepo;
        private readonly ITripRepo _tripRepo;
        private readonly IPolRepo _polRepo;
        private readonly IRevRepo _revRepo;
        private readonly ITripCmd _tripCmd;
        private readonly ITripQry _tripQry;
        private readonly IDrvCmd _drvCmd;
        private readonly IDrvQry _drvQry;
        private readonly IMapSvc _mapSvc;
        private readonly IFareSvc _fareSvc;
        private readonly IRevSvc _revSvc;
        private readonly IWalletSvc _walletSvc;
        private readonly IPsgSvc _psgSvc;
        private readonly IAdmSvc _admSvc;
        private readonly INotiSvc _notiSvc;

        public event EventHandler<Usr> LoginSucceeded;
        public event EventHandler<Usr> RegisterSucceeded;

        private bool _loginPasswordVisible;
        private bool _regPasswordVisible;

        public FrmAuth(
            IUsrSvc usrSrv,
            IVehRepo vehRepo,
            IUsrRepo usrRepo,
            ITripRepo tripRepo,
            IPolRepo polRepo,
            IRevRepo revRepo,
            ITripCmd tripCmd,
            ITripQry tripQry,
            IDrvCmd drvCmd,
            IDrvQry drvQry,
            IMapSvc mapSvc,
            IFareSvc fareSvc,
            IRevSvc revSvc,
            IWalletSvc walletSvc,
            IPsgSvc psgSvc,
            IAdmSvc admSvc,
            INotiSvc notiSvc)
        {
            _usrSrv = usrSrv ?? throw new ArgumentNullException(nameof(usrSrv));
            _vehRepo = vehRepo ?? throw new ArgumentNullException(nameof(vehRepo));
            _usrRepo = usrRepo ?? throw new ArgumentNullException(nameof(usrRepo));
            _tripRepo = tripRepo ?? throw new ArgumentNullException(nameof(tripRepo));
            _polRepo = polRepo ?? throw new ArgumentNullException(nameof(polRepo));
            _revRepo = revRepo ?? throw new ArgumentNullException(nameof(revRepo));
            _tripCmd = tripCmd ?? throw new ArgumentNullException(nameof(tripCmd));
            _tripQry = tripQry ?? throw new ArgumentNullException(nameof(tripQry));
            _drvCmd = drvCmd ?? throw new ArgumentNullException(nameof(drvCmd));
            _drvQry = drvQry ?? throw new ArgumentNullException(nameof(drvQry));
            _mapSvc = mapSvc ?? throw new ArgumentNullException(nameof(mapSvc));
            _fareSvc = fareSvc ?? throw new ArgumentNullException(nameof(fareSvc));
            _revSvc = revSvc ?? throw new ArgumentNullException(nameof(revSvc));
            _walletSvc = walletSvc ?? throw new ArgumentNullException(nameof(walletSvc));
            _psgSvc = psgSvc ?? throw new ArgumentNullException(nameof(psgSvc));
            _admSvc = admSvc ?? throw new ArgumentNullException(nameof(admSvc));
            _notiSvc = notiSvc ?? throw new ArgumentNullException(nameof(notiSvc));

            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnLogin.Click += async (s, e) => await OnLoginClicked();
            btnRegister.Click += async (s, e) => await OnRegisterClicked();

            // Demo buttons - Quick login (chỉ fill field)
            btnDemoPassenger.Click += (s, e) => DemoLogin("0911111111", "123456");
            btnDemoDriver.Click += (s, e) => DemoLogin("0900000000", "123456");
            btnDemoAdmin.Click += (s, e) => DemoLogin("0999999999", "admin123");

            txtLoginPhone.KeyDown += OnAuthKeyDown;
            txtLoginPassword.KeyDown += OnAuthKeyDown;
            txtRegName.KeyDown += OnAuthKeyDown;
            txtRegPhone.KeyDown += OnAuthKeyDown;
            txtRegPassword.KeyDown += OnAuthKeyDown;
        }

        private static string GetText(TextBox textBox)
        {
            return textBox.Text.Trim();
        }

        /// <summary>
        /// Đăng nhập nhanh - chỉ fill field, không auto-submit
        /// </summary>
        private void DemoLogin(string phone, string password)
        {
            txtLoginPhone.Text = phone;
            txtLoginPassword.Text = password;
            // Người dùng tự click nút Đăng nhập
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
        private async Task OnLoginClicked()
        {
            string phone = GetText(txtLoginPhone);
            string password = GetText(txtLoginPassword);

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại và mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            try
            {
                Usr usr = await _usrSrv.LoginAsync(phone, password);
                if (usr == null)
                {
                    throw new InvalidOperationException("Số điện thoại hoặc mật khẩu không đúng.");
                }

                LoginSucceeded?.Invoke(this, usr);
                NavigateToRoleForm(usr);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginPassword.Clear();
                txtLoginPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private async Task OnRegisterClicked()
        {
            btnRegister.Enabled = false;
            try
            {
                string name = GetText(txtRegName);
                string phone = GetText(txtRegPhone);
                string password = GetText(txtRegPassword);
                bool isDriver = cmbRole.SelectedIndex == 1;

                if (isDriver)
                {
                    string plate = txtPlate.Text.Trim();
                    string brand = txtBrand.Text.Trim();
                    string model = txtModel.Text.Trim();
                    string color = txtColor.Text.Trim();
                    int capacity = (int)numCapacity.Value;

                    Loc defaultLoc = new Loc(
                        new Coord(10.7769, 106.7009),
                        new Addr("District 1", string.Empty, "District 1", "Ho Chi Minh", "Vietnam"));

                    await _usrSrv.RegisterDriverAsync(
                        name,
                        phone,
                        password,
                        "GPLX-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                        cmbVehicleType.SelectedIndex == 0 ? VehicleType.Moto : VehicleType.Car,
                        plate,
                        brand,
                        model,
                        color,
                        capacity,
                        defaultLoc);
                }
                else
                {
                    await _usrSrv.RegisterPassengerAsync(name, phone, password);
                }

                Usr usr = await _usrSrv.LoginAsync(phone, password);
                if (usr == null)
                {
                    throw new InvalidOperationException("Không thể tự động đăng nhập sau khi đăng ký.");
                }

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegisterSucceeded?.Invoke(this, usr);
                NavigateToRoleForm(usr);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRegister.Enabled = true;
            }
        }

        private void NavigateToRoleForm(Usr usr)
        {
            Form nextForm = null;

            if (usr is Psg psg)
            {
                nextForm = new FrmPassenger(
                    psg, _usrSrv, _tripCmd, _tripQry,
                    _revSvc, _fareSvc, _mapSvc, _psgSvc, _notiSvc);
            }
            else if (usr is Drv drv)
            {
                nextForm = new FrmDriver(
                    drv, _usrSrv, _drvCmd, _drvQry, _tripQry, _tripCmd,
                    _walletSvc, _mapSvc, _usrRepo, _vehRepo, _notiSvc);
            }
            else if (usr is Adm adm)
            {
                nextForm = new FrmAdmin(
                    adm, _usrRepo, _tripRepo, _polRepo, _revRepo, _admSvc);
            }

            if (nextForm != null)
            {
                nextForm.Show();
                this.Hide();
                nextForm.FormClosed += (s, e) => this.Close();
            }
            else
            {
                MessageBox.Show("Vai trò người dùng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
            cmbRole.SelectedIndex = 0;
            txtRegName.Focus();
        }
        private void linkToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
            txtLoginPhone.Focus();
        }

        private void btnToggleLoginPassword_Click(object sender, EventArgs e)
        {
            _regPasswordVisible = !_regPasswordVisible;
            btnToggleRegPassword.Text = _regPasswordVisible ? "👁️" : "🙈";
        }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlVehicleInfo.Visible = true;
            cmbVehicleType.SelectedIndex = 0;
        }
        private void cmbVehicleType_SelectedIndexChanged (object sender, EventArgs e)
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
    }
}
