// Application/Features/Trips/AssignDriver/AssignDriverValidator.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Trips.AssignDriver
{
    public static class AssignDriverValidator
    {
        public static ValidationResult Validate(AssignDriverCommand command)
        {
            var errors = new List<string>();

            if (command.TripId == Guid.Empty)
                errors.Add("TripId không hợp lệ.");

            if (command.DriverId == Guid.Empty)
                errors.Add("DriverId không hợp lệ.");

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