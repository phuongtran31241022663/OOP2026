using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class UcAdminUser : UserControl
    {
        public UcAdminUser()
        {
            InitializeComponent();
        }

        public void SetData(int driverCount, int onlineDrivers, int passengerCount, int adminCount)
        {
            lblDriverCount.Text = driverCount.ToString();
            lblDriverSub.Text = $"{onlineDrivers} đang online";

            lblPassCount.Text = passengerCount.ToString();
            lblPassSub.Text = "đã đăng ký";

            lblAdminCount.Text = adminCount.ToString();
            lblAdminSub.Text = "quản trị viên";
        }
    }
}