using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Trips.CancelTrip
{
    public static class CancelTripValidator
    {
        public static ValidationResult Validate(CancelTripCommand command)
        {
            var errors = new List<string>();

            if (command.TripId == Guid.Empty)
                errors.Add("TripId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(command.Reason))
                errors.Add("Lý do hủy không được để trống.");

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
