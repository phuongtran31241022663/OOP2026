using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Shells;
using System;

namespace Presentation.UserControls
{
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

    /// <summary>
    /// Vong doi dat xe: Dat -> Theo doi -> Thanh toan.
    /// SplitContainer: Ban do trai, bang dieu khien dong phai.
    /// </summary>
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

        private Trip _currentTrip;
        private Location _pickup;
        private Location _destination;
        private VehicleType _selectedVehicle = VehicleType.Motorbike;

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
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapService = mapService;
            _fareService = fareService;
            _simulationService = simulationService;
            _matchingService = matchingService;
            _reviewService = reviewService;

            InitializeComponent();
            SetupEvents();
            ShowStage(TripStage.Idle);
            
            // Initialize location pickers with passenger data for recent locations
            InitializeLocationPickers();
        }

        private void SetupEvents()
        {
            btnHistory.Click += (s, e) => ShowStage(TripStage.History);
            btnProfile.Click += (s, e) => RequestShowProfile?.Invoke(this, _passenger);
            btnLogout.Click += (s, e) => RequestLogout?.Invoke(this, e);

            btnBook.Click += async (s, e) => await OnBookClicked();
            btnCancelSearch.Click += async (s, e) => await OnCancelSearchClicked();
            btnCancelTrip.Click += async (s, e) => await OnCancelTripClicked();
            btnConfirmPayment.Click += async (s, e) => await OnConfirmPaymentClicked();
            btnRateDriver.Click += (s, e) => OnRateDriverClicked();

            cmbVehicleType.SelectedIndexChanged += (s, e) =>
            {
                _selectedVehicle = cmbVehicleType.SelectedIndex == 1 ? VehicleType.Car : VehicleType.Motorbike;
                UpdateFareEstimate();
                ValidateBooking(); // Re-validate when vehicle type changes
            };

            // Location picker events
            pickupPicker.SelectedIndexChanged += (s, e) => OnPickupSelected();
            destinationPicker.SelectedIndexChanged += (s, e) => OnDestinationSelected();

            _tripService.TripStatusChanged += OnTripStatusChanged;
            Disposed += (s, e) => _tripService.TripStatusChanged -= OnTripStatusChanged;

            // Keyboard support
            KeyDown += UcPassenger_KeyDown;

            // Setup hover effects for buttons
            SetupButtonHoverEffects();
        }

        #region Location Picker Handlers

        private void OnPickupSelected()
        {
            // Use the public SelectedLocation property from LocationPickerControl
            _pickup = pickupPicker.SelectedLocation;
            if (_pickup != null)
            {
                // Update display text
                pickupPicker.Text = $"A: {FormatLocationForDisplay(_pickup)}";
                ValidateBooking();
            }
        }

        private void OnDestinationSelected()
        {
            // Use the public SelectedLocation property from LocationPickerControl
            _destination = destinationPicker.SelectedLocation;
            if (_destination != null)
            {
                // Update display text
                destinationPicker.Text = $"B: {FormatLocationForDisplay(_destination)}";
                
                // Check for duplicate locations
                CheckForDuplicateLocations();
                
                ValidateBooking();
            }
        }

        private void CheckForDuplicateLocations()
        {
            if (_pickup == null || _destination == null)
                return;

            // Check if coordinates are too close (duplicate)
            bool isDuplicate = Math.Abs(_pickup.Coordinate.Latitude - _destination.Coordinate.Latitude) < LocationPickerControl.CoordinateTolerance &&
                              Math.Abs(_pickup.Coordinate.Longitude - _destination.Coordinate.Longitude) < LocationPickerControl.CoordinateTolerance;

            if (isDuplicate)
            {
                _validationErrorProvider.SetError(destinationPicker, "Điểm đến không được trùng điểm đón");
                btnBook.Enabled = false;
            }
            else
            {
                _validationErrorProvider.SetError(destinationPicker, string.Empty);
                // Don't set enabled here - let ValidateBooking decide based on all validations
            }
        }

        #endregion

        #region Validation Methods

        private void ValidateBooking()
        {
            bool pickupValid = _pickup != null;
            bool destinationValid = _destination != null;
            bool vehicleValid = cmbVehicleType.SelectedIndex >= 0;
            bool noDuplicate = true;

            // Check for duplicate locations
            if (_pickup != null && _destination != null)
            {
                bool isDuplicate = Math.Abs(_pickup.Coordinate.Latitude - _destination.Coordinate.Latitude) < LocationPickerControl.CoordinateTolerance &&
                                  Math.Abs(_pickup.Coordinate.Longitude - _destination.Coordinate.Longitude) < LocationPickerControl.CoordinateTolerance;
                noDuplicate = !isDuplicate;
            }

            bool isValid = pickupValid && destinationValid && vehicleValid && noDuplicate;

            // Set error messages via ErrorProvider from BaseUserControl
            if (pickupValid)
                _validationErrorProvider.SetError(pickupPicker, string.Empty);
            else
                _validationErrorProvider.SetError(pickupPicker, "Vui lòng chọn điểm đón");

            if (destinationValid && noDuplicate)
                _validationErrorProvider.SetError(destinationPicker, string.Empty);
            else
                _validationErrorProvider.SetError(destinationPicker, noDuplicate ? "Vui lòng chọn điểm đến" : "Điểm đến không được trùng điểm đón");

            if (vehicleValid)
                _validationErrorProvider.SetError(cmbVehicleType, string.Empty);
            else
                _validationErrorProvider.SetError(cmbVehicleType, "Vui lòng chọn loại xe");

            btnBook.Enabled = isValid;
        }

        #endregion

        #region Keyboard Support

        private void UcPassenger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Prevent beep

                // If focus is on a ComboBox, let the default behavior happen (open dropdown)
                if (ActiveControl is ComboBox)
                    return;

                // If booking button is enabled, click it
                if (btnBook.Enabled)
                {
                    btnBook.PerformClick();
                    return;
                }

                // Otherwise, try to move focus to next control
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        #endregion

        #region Hover Effects

        private void SetupButtonHoverEffects()
        {
            // Book button hover effects
            btnBook.MouseEnter += (s, e) => 
            {
                btnBook.BackColor = UiConstants.PrimaryHover;
            };
            btnBook.MouseLeave += (s, e) => 
            {
                btnBook.BackColor = UiConstants.PrimaryNormal;
            };

            // Cancel search button hover effects
            btnCancelSearch.MouseEnter += (s, e) => 
            {
                btnCancelSearch.BackColor = UiConstants.DangerHover;
            };
            btnCancelSearch.MouseLeave += (s, e) => 
            {
                btnCancelSearch.BackColor = UiConstants.DangerNormal;
            };

            // Cancel trip button hover effects
            btnCancelTrip.MouseEnter += (s, e) => 
            {
                btnCancelTrip.BackColor = UiConstants.DangerHover;
            };
            btnCancelTrip.MouseLeave += (s, e) => 
            {
                btnCancelTrip.BackColor = UiConstants.DangerNormal;
            };

            // Confirm payment button hover effects
            btnConfirmPayment.MouseEnter += (s, e) => 
            {
                btnConfirmPayment.BackColor = UiConstants.SuccessHover;
            };
            btnConfirmPayment.MouseLeave += (s, e) => 
            {
                btnConfirmPayment.BackColor = UiConstants.SuccessNormal;
            };

            // Rate driver button hover effects
            btnRateDriver.MouseEnter += (s, e) => 
            {
                btnRateDriver.BackColor = UiConstants.PrimaryHover;
            };
            btnRateDriver.MouseLeave += (s, e) => 
            {
                btnRateDriver.BackColor = UiConstants.PrimaryNormal;
            };
        }

        #endregion

        #region Helper Methods

        private string FormatLocationForDisplay(Location location)
        {
            if (location == null || location.Address == null)
                return string.Empty;

            var address = location.Address;
            // Format: District, City
            if (!string.IsNullOrWhiteSpace(address.District) && !string.IsNullOrWhiteSpace(address.City))
                return $"{address.District}, {address.City}";
            else if (!string.IsNullOrWhiteSpace(address.City))
                return address.City;
            else if (!string.IsNullOrWhiteSpace(address.District))
                return address.District;
            else
                return string.Empty;
        }

        private void InitializeLocationPickers()
        {
            // Set up the labels for pickup and destination (these would typically be external labels)
            // For now, we'll just initialize the pickers with empty state
            pickupPicker.SelectedLocation = null;
            destinationPicker.SelectedLocation = null;
            
            // Populate dropdowns with recent locations based on passenger ID
            string userIdentifier = _passenger?.Id.ToString();
            pickupPicker.PopulateDropdown(userIdentifier);
            destinationPicker.PopulateDropdown(userIdentifier);
        }

        #endregion

        private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            RunOnUI(() =>
            {
                if (_currentTrip != null && _currentTrip.Id == e.TripId)
                {
                    lblStatus.Text = "Trang thai: " + e.NewStatus;
                    UpdateStageFromStatus(e.NewStatus);
                }
            });
        }

        private void UpdateStageFromStatus(string status)
        {
            switch (status)
            {
                case "Searching": ShowStage(TripStage.Searching); break;
                case "Matched": ShowStage(TripStage.Tracking); break;
                case "Arrived": ShowStage(TripStage.Tracking); break;
                case "Started": ShowStage(TripStage.Tracking); break;
                case "Completed": ShowStage(TripStage.Payment); break;
                case "Cancelled": ShowStage(TripStage.Idle); break;
            }
        }

        private void ShowStage(TripStage stage)
        {
            pnlBooking.Visible = false;
            pnlSearching.Visible = false;
            pnlTracking.Visible = false;
            pnlPayment.Visible = false;
            pnlHistory.Visible = false;

            switch (stage)
            {
                case TripStage.Idle:
                    pnlBooking.Visible = true;
                    break;
                case TripStage.Searching:
                    pnlSearching.Visible = true;
                    break;
                case TripStage.Tracking:
                    pnlTracking.Visible = true;
                    break;
                case TripStage.Payment:
                    pnlPayment.Visible = true;
                    break;
                case TripStage.History:
                    pnlHistory.Visible = true;
                    LoadHistory();
                    break;
            }
        }

        private void UpdateFareEstimate()
        {
            if (_pickup != null && _destination != null && _fareService != null)
            {
                // Async estimate could be added here
            }
        }

        private async System.Threading.Tasks.Task OnBookClicked()
        {
            if (_pickup == null || _destination == null)
            {
                ShowWarning("Vui long chon diem don va diem den.");
                return;
            }

            ShowStage(TripStage.Searching);
            IsLoading = true;

            try
            {
                Route route = null;
                if (_mapService != null)
                {
                    route = await _mapService.GetRouteAsync(_pickup, _destination);
                }

                if (route == null)
                {
                    route = new Route(_pickup, _destination, 5.0, TimeSpan.FromMinutes(15), "");
                }

                Fare fare = null;
                if (_fareService != null)
                {
                    fare = await _fareService.CalculateFareAsync(_selectedVehicle, route.Distance);
                }
                else
                {
                    var total = _selectedVehicle == VehicleType.Car ? 25000m + (decimal)(route.Distance * 12000) : 12000m + (decimal)(route.Distance * 4000);
                    fare = new Fare(new Money(total, "VND"), new Money(total * 0.2m, "VND"));
                }

                _currentTrip = await _tripService.CreateTripAsync(_passenger.Id, route, fare, _selectedVehicle);
                _currentTrip.SetSearching();
                ShowStage(TripStage.Searching);

                // Simulate matching
                if (_matchingService != null)
                {
                    bool matched = await _matchingService.MatchDriverToTripAsync(_currentTrip.Id, Guid.Empty);
                    if (!matched)
                    {
                        ShowWarning("Khong tim thay tai xe phu hop. Vui long thu lai.");
                        ShowStage(TripStage.Idle);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Dat chuyen di");
                ShowStage(TripStage.Idle);
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Dat chuyen di");
                ShowStage(TripStage.Idle);
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Dat chuyen di");
                ShowStage(TripStage.Idle);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnCancelSearchClicked()
        {
            IsLoading = true;
            try
            {
                if (_currentTrip != null)
                {
                    await _tripService.CancelTripAsync(_currentTrip.Id, "Hanh khach huy tim kiem");
                    _currentTrip = null;
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Huy tim kiem tai xe");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Huy tim kiem tai xe");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Huy tim kiem tai xe");
            }
            finally
            {
                IsLoading = false;
            }

            ShowStage(TripStage.Idle);
        }

        private async System.Threading.Tasks.Task OnCancelTripClicked()
        {
            if (_currentTrip == null) return;

            IsLoading = true;
            try
            {
                bool canCancel = await _tripService.CanTripBeCancelledAsync(_currentTrip.Id);
                if (!canCancel)
                {
                    ShowWarning("Khong the huy chuyen o trang thai nay.");
                    return;
                }

                if (Confirm("Ban co chac muon huy chuyen?"))
                {
                    await _tripService.CancelTripAsync(_currentTrip.Id, "Hanh khach huy");
                    _currentTrip = null;
                    ShowStage(TripStage.Idle);
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Huy chuyen di");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Huy chuyen di");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Huy chuyen di");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnConfirmPaymentClicked()
        {
            if (_currentTrip == null) return;

            IsLoading = true;
            try
            {
                await _tripService.ConfirmPaymentAsync(_currentTrip.Id);
                ShowInfo("Thanh toan thanh cong!");
                ShowStage(TripStage.Idle);
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Xac nhan thanh toan");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Xac nhan thanh toan");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Xac nhan thanh toan");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnRateDriverClicked()
        {
            if (_currentTrip == null)
            {
                return;
            }

            try
            {
                if (_reviewService == null)
                {
                    throw new InvalidOperationException("Dich vu danh gia chua duoc khoi tao.");
                }

                var ucRating = new UcRating(_reviewService, _currentTrip);
                FrmModal.ShowModal(this, ucRating, "Danh gia tai xe");
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Mo man hinh danh gia tai xe");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Mo man hinh danh gia tai xe");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Mo man hinh danh gia tai xe");
            }
        }

        private async void LoadHistory()
        {
            IsLoading = true;
            try
            {
                var trips = await _tripService.GetTripsByPassengerAsync(_passenger.Id);
                dgvHistory.Rows.Clear();
                foreach (var trip in trips)
                {
                    dgvHistory.Rows.Add(
                        trip.Id.ToString().Substring(0, 8),
                        trip.Status,
                        trip.TripFare?.TotalAmount.Amount.ToString("N0") + "d",
                        trip.RequestAt.ToString("dd/MM HH:mm"));
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Tai lich su chuyen di");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Tai lich su chuyen di");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Tai lich su chuyen di");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private enum TripStage { Idle, Searching, Tracking, Payment, History }
    }
}

