using Application.Interfaces;
using Application.Events;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Components;
using Presentation.Constants;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    public partial class UcPassenger : BaseUserControl
    {
        private readonly Passenger _passenger;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly IMapService _mapService;
        private readonly IFareService _fareService;
        private readonly ISimulationService _simulationService;
        private readonly IMatchingService _matchingService;
        private readonly IReviewService _reviewService;

        public event EventHandler RequestLogout;
        public event EventHandler<User> RequestShowProfile;

        public UcPassenger(
            Passenger passenger,
            ITripService tripService,
            IUserService userService,
            IMapService mapService,
            IFareService fareService,
            ISimulationService simulationService,
            IMatchingService matchingService,
            IReviewService reviewService)
        {
            _passenger = passenger;
            _tripService = tripService;
            _userService = userService;
            _mapService = mapService;
            _fareService = fareService;
            _simulationService = simulationService;
            _matchingService = matchingService;
            _reviewService = reviewService;
            InitializeComponent();

            // Map integration: Toggle ActiveSlot on picker click
            pickupPicker.Click += PickupPicker_Click;
            destinationPicker.Click += DestinationPicker_Click;
            mapControl.MapClicked += MapControl_MapClicked;
        }

        private void PickupPicker_Click(object sender, EventArgs e)
        {
            mapControl.ActiveSlot = Presentation.Components.MapSlot.Pickup;
            lblStatus.Text = "Nhấp vào bản đồ chọn điểm đón";
        }

        private void DestinationPicker_Click(object sender, EventArgs e)
        {
            mapControl.ActiveSlot = Presentation.Components.MapSlot.Destination;
            lblStatus.Text = "Nhấp vào bản đồ chọn điểm trả";
        }

        private void MapControl_MapClicked(Presentation.Components.MapControl map, Domain.ValueObjects.Location location)
        {
            if (map.ActiveSlot == Presentation.Components.MapSlot.Pickup)
            {
                pickupPicker.SelectedLocation = location;
            }
            else if (map.ActiveSlot == Presentation.Components.MapSlot.Destination)
            {
                destinationPicker.SelectedLocation = location;
            }
            map.ActiveSlot = Presentation.Components.MapSlot.None;
            lblStatus.Text = "Đã chọn vị trí";
        }

        // Original event handlers (stubbed)
        private void btnLogout_Click(object sender, EventArgs e) => RequestLogout?.Invoke(this, EventArgs.Empty);
        private void btnProfile_Click(object sender, EventArgs e) => RequestShowProfile?.Invoke(this, _passenger);
        private void btnHistory_Click(object sender, EventArgs e) { /* Show history */ }
        private void btnBook_Click(object sender, EventArgs e) { /* Book trip */ }

        // Rest of implementation...
    }
}

