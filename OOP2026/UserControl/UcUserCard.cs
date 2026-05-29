namespace OOP2026
{
    public partial class UcUserCard : UserControl
    {
        private Usr _user;

        public UcUserCard()
        {
            InitializeComponent();
        }

        public void SetUser(Usr user)
        {
            _user = user;
            lblId.Text = "ID: " + user.Id.ToString().Substring(0, 8);
            lblName.Text = user.Name;
            lblPhone.Text = user.Phone;
            
            if (user is Drv driver)
            {
                lblStatus.Text = driver.Status.ToString();
            }
            else
            {
                lblStatus.Text = "N/A";
            }
        }
    }
}