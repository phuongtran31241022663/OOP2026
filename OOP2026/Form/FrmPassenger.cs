using System;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class FrmPassenger : Form
    {
        // Gộp các field thừa, chỉ giữ lại những gì thực sự cần thiết
        private readonly Psg _passenger;
        // Các service được giữ lại để phục vụ việc khởi tạo Panel
        private readonly IUsrSvc _userService;
        private readonly ITripCmd _tripCmdService;
        private readonly ITripQry _tripQueryService;
        private readonly IUsrRepo _userRepo;
        private readonly IVehRepo _vehicleRepo;
        private readonly IRevSvc _reviewService;
        private readonly IFareSvc _fareService;
        private readonly IMapSvc _mapService;
        private readonly IPsgSvc _passengerService;
        private readonly INotificationSvc _notificationSvc;

        public FrmPassenger(
            Psg passenger,
            IUsrSvc userService,
            ITripCmd tripCommandService,
            ITripQry tripQueryService,
            IUsrRepo userRepository,
            IVehRepo vehicleRepository,
            IRevSvc reviewService,
            IFareSvc fareService,
            IMapSvc mapService,
            IPsgSvc passengerService,
            INotificationSvc notificationSvc)
        {
            // Gán dữ liệu cơ bản
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripCmdService = tripCommandService ?? throw new ArgumentNullException(nameof(tripCommandService));
            _tripQueryService = tripQueryService ?? throw new ArgumentNullException(nameof(tripQueryService));
            _userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _vehicleRepo = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            _notificationSvc = notificationSvc ?? throw new ArgumentNullException(nameof(notificationSvc));

            InitializeComponent();

            // Tối ưu hóa UI
            this.DoubleBuffered = true;
            InitializeDependencies();
        }
        private void InitializeDependencies()
        {
            // Khởi tạo Panel với đầy đủ các Service cần thiết
            home.Initialize(
                _passenger,
                _userService,
                _tripCmdService,
                _tripQueryService,
                _reviewService,
                _fareService,
                _mapService,
                _passengerService,
                _notificationSvc
            );

            home.SetMap(map);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
}
