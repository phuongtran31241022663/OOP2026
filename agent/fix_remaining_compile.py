from pathlib import Path

def replace(path, old, new):
    p = Path(path)
    data = p.read_text(encoding="utf-8-sig")
    if old not in data:
        print("MISS", path, old[:60].replace("\n", "\\n"))
        return
    p.write_text(data.replace(old, new), encoding="utf-8-sig")
    print("OK", path)

replace("OOP2026/Form/FrmDriver.cs",
"""            map.SetPickup += OnMapLocationSelected;
""",
"""            map.LocationSelected += OnMapLocationSelected;
""")

replace("OOP2026/UserControl/ucBooking.cs", "e.Location", "e.Loc")

replace("OOP2026/Service.cs", "p.Locationality", "p.Locality")

replace("OOP2026/UserControl/ucReview.cs",
"""                Loc = new Point(startX + index * starSize, (pnlStars.Height - starSize) / 2),
""",
"""                Location = new Point(startX + index * starSize, (pnlStars.Height - starSize) / 2),
""")

replace("OOP2026/UserControl/ucReview.cs",
"""        public event EventHandler<EventArgs>? ReviewSubmitted;
""",
"""        public event EventHandler<ReviewSubmittedEventArgs> ReviewSubmitted;
""")

replace("OOP2026/UserControl/ucReview.cs",
"""                ReviewSubmitted?.Invoke(this, new EventArgs(_selectedStars, commentText));
""",
"""                ReviewSubmitted?.Invoke(this, new ReviewSubmittedEventArgs(_selectedStars, commentText));
""")

review_path = Path("OOP2026/UserControl/ucReview.cs")
review = review_path.read_text(encoding="utf-8-sig")
marker = "\n    }\n}"
if "class ReviewSubmittedEventArgs" not in review:
    review = review.replace(marker, """
    public class ReviewSubmittedEventArgs : EventArgs
    {
        public int Stars { get; private set; }
        public string Comment { get; private set; }

        public ReviewSubmittedEventArgs(int stars, string comment)
        {
            Stars = stars;
            Comment = comment;
        }
    }
}
""")
    review_path.write_text(review, encoding="utf-8-sig")
    print("OK OOP2026/UserControl/ucReview.cs event args")

admin_path = Path("OOP2026/Form/FrmAdmin.cs")
admin = admin_path.read_text(encoding="utf-8-sig")
if "public FrmAdmin(" not in admin:
    admin = admin.replace("""        private readonly IAdmSvc _adminService;

""", """        private readonly IAdmSvc _adminService;

        public FrmAdmin(Adm admin, IUsrRepo userRepo, ITripRepo tripRepo, IPolRepo policyRepo, IRevRepo reviewRepo, IAdmSvc adminService)
        {
            _admin = admin;
            _policyRepo = policyRepo;
            _adminService = adminService;
            InitializeComponent();
        }

""")
if "private void FrmAdmin_Load" not in admin:
    admin = admin.replace("""        private void BtnCloseAdmin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

""", """        private void BtnCloseAdmin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
        }

        private void BtnSavePolicy_Click(object sender, EventArgs e)
        {
        }

""")
    admin_path.write_text(admin, encoding="utf-8-sig")
    print("OK OOP2026/Form/FrmAdmin.cs")

replace("OOP2026/UserControl/ucDriverHome.cs", "lblStatus.Checked", "chkStatus.Checked")
replace("OOP2026/UserControl/ucDriverHome.cs", "lblStatus_CheckedChanged", "chkStatus_CheckedChanged")