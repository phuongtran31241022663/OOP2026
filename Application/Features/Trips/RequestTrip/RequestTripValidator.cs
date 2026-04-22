using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Trips.RequestTrip
{
    public static class RequestTripValidator
    {
        public static ValidationResult Validate(RequestTripCommand command)
        {
            var errors = new List<string>();

            if (command.PassengerId == Guid.Empty)
                errors.Add("PassengerId không hợp lệ.");

            if (command.Pickup == null)
                errors.Add("Pickup location không được để trống.");

            if (command.Destination == null)
                errors.Add("Destination location không được để trống.");

            if (command.Pickup != null && command.Destination != null &&
                command.Pickup.Lat == command.Destination.Lat &&
                command.Pickup.Lng == command.Destination.Lng)
                errors.Add("Pickup và Destination không được giống nhau.");

            return new ValidationResult(errors);
        }
    }

    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public IEnumerable<string> Errors { get; }

        public ValidationResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
