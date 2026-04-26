using Application.Interfaces;
using Domain.Entities.Users;
using System;
using System.Windows.Forms;

namespace Presentation.Screens.AdminScreen
{
    public partial class AdminDashboardForm : BaseForm
    {
        private readonly Admin _admin;
        private readonly IAdminService _adminService;

        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        public AdminDashboardForm(Admin admin, IAdminService adminService)
            : this()
        {
            _admin = admin;
            _adminService = adminService;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_lblSectionTitle != null)
            {
                _lblSectionTitle.Text = "Admin Dashboard";
            }
        }
    }
}
