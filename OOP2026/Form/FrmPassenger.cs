using System;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class FrmPassenger : Form
    {
        private readonly Psg _passenger;

        private readonly IUsrSvc _userService;
        private readonly ITripCmd _tripCmdService;
        private readonly ITripQry _tripQueryService;
        private readonly IRevSvc _reviewService;
        private readonly IFareSvc _fareService;
        private readonly IMapSvc _mapService;
        private readonly IPsgSvc _passengerService;
        private readonly INotiSvc _notificationSvc;

        public FrmPassenger(
            Psg passenger,
            IUsrSvc userService,
            ITripCmd tripCommandService,
            ITripQry tripQueryService,
            IRevSvc reviewService,
            IFareSvc fareService,
            IMapSvc mapService,
            IPsgSvc passengerService,
            INotiSvc notificationSvc)
        {
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripCmdService = tripCommandService ?? throw new ArgumentNullException(nameof(tripCommandService));
            _tripQueryService = tripQueryService ?? throw new ArgumentNullException(nameof(tripQueryService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            _notificationSvc = notificationSvc ?? throw new ArgumentNullException(nameof(notificationSvc));

            InitializeComponent();
            InitializeDependencies();
        }
        private void InitializeDependencies()
        {
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
    }
}
