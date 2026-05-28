namespace OOP2026
{
    public partial class FrmAdmin : Form
    {
        private readonly Adm _admin;
        private readonly IPolRepo _policyRepo;
        private readonly IAdmSvc _adminService;

        public FrmAdmin()
        {
            InitializeComponent();
        }

        public FrmAdmin(Adm admin, IUsrRepo userRepo, ITripRepo tripRepo, IPolRepo policyRepo, IRevRepo reviewRepo, IAdmSvc adminService)
        {
            _admin = admin;
            _policyRepo = policyRepo;
            _adminService = adminService;
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
        }

        private void BtnSavePolicy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Policy save UI is not connected yet.", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            flpCards.Controls.Clear();
            base.OnFormClosing(e);
        }

        private void BtnCloseAdmin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNavStats_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void BtnNavUsers_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void BtnNavTrips_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;
        }

        private void BtnNavFees_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
        }

    }       
}
