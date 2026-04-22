using System;

namespace Presentation.Contracts.Requests
{
    /// <summary>
    /// Base request validator interface.
    /// All API request DTOs should implement this for validation.
    /// 
    /// Giao diện trình xác thực yêu cầu cơ sở.
    /// Tất cả các DTO yêu cầu API phải triển khai điều này để xác thực.
    /// </summary>
    public interface IValidatedRequest
    {
        /// <summary>
        /// Validates the request data.
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        bool Validate(out List<string> errors);
    }

    /// <summary>
    /// Base class for all API request DTOs.
    /// </summary>
    public abstract class ApiRequest : IValidatedRequest
    {
        public virtual bool Validate(out List<string> errors)
        {
            errors = new List<string>();
            return true;
        }
    }

    /// <summary>
    /// Request for registering a new driver.
    /// </summary>
    public class CreateDriverRequest : ApiRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;

        public override bool Validate(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("Driver name is required");

            if (string.IsNullOrWhiteSpace(Email))
                errors.Add("Email is required");

            if (string.IsNullOrWhiteSpace(Phone))
                errors.Add("Phone number is required");

            if (string.IsNullOrWhiteSpace(LicensePlate))
                errors.Add("License plate is required");

            return errors.Count == 0;
        }
    }

    /// <summary>
    /// Request for registering a new passenger.
    /// </summary>
    public class CreatePassengerRequest : ApiRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public override bool Validate(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("Passenger name is required");

            if (string.IsNullOrWhiteSpace(Email))
                errors.Add("Email is required");

            if (string.IsNullOrWhiteSpace(Phone))
                errors.Add("Phone number is required");

            return errors.Count == 0;
        }
    }

    /// <summary>
    /// Request for creating a new trip.
    /// </summary>
    public class CreateTripRequest : ApiRequest
    {
        public Guid PassengerId { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DropoffLocation { get; set; } = null!;

        public override bool Validate(out List<string> errors)
        {
            errors = new List<string>();

            if (PassengerId == Guid.Empty)
                errors.Add("Passenger ID is required");

            if (string.IsNullOrWhiteSpace(PickupLocation))
                errors.Add("Pickup location is required");

            if (string.IsNullOrWhiteSpace(DropoffLocation))
                errors.Add("Dropoff location is required");

            return errors.Count == 0;
        }
    }

    /// <summary>
    /// Request for updating passenger information.
    /// </summary>
    public class UpdatePassengerRequest : ApiRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public override bool Validate(out List<string> errors)
        {
            errors = new List<string>();

            if (Id == Guid.Empty)
                errors.Add("Passenger ID is required");

            // At least one field must be provided for update
            if (string.IsNullOrWhiteSpace(Name) && 
                string.IsNullOrWhiteSpace(Email) && 
                string.IsNullOrWhiteSpace(Phone))
            {
                errors.Add("At least one field must be provided for update");
            }

            return errors.Count == 0;
        }
    }

    /// <summary>
    /// Request for updating driver status.
    /// </summary>
    public class UpdateDriverStatusRequest : ApiRequest
    {
        public Guid DriverId { get; set; }
        public string Status { get; set; } = null!;

        public override bool Validate(out List<string> errors)
        {
            errors = new List<string>();

            if (DriverId == Guid.Empty)
                errors.Add("Driver ID is required");

            if (string.IsNullOrWhiteSpace(Status))
                errors.Add("Status is required");

            var validStatuses = new[] { "Available", "Busy", "Offline" };
            if (!validStatuses.Contains(Status))
                errors.Add($"Status must be one of: {string.Join(", ", validStatuses)}");

            return errors.Count == 0;
        }
    }
}
