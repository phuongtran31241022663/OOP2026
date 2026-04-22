// Application/Features/Passengers/RegisterPassenger/RegisterPassengerValidator.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Passengers.RegisterPassenger
{
    public static class RegisterPassengerValidator
    {
        public static ValidationResult Validate(RegisterPassengerCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Tên không được để trống.");

            if (string.IsNullOrWhiteSpace(command.Phone))
                errors.Add("Số điện thoại không được để trống.");
            else if (!IsValidPhone(command.Phone))
                errors.Add("Số điện thoại phải là 10-11 chữ số.");

            if (string.IsNullOrWhiteSpace(command.Password))
                errors.Add("Mật khẩu không được để trống.");
            else if (command.Password.Length < 6)
                errors.Add("Mật khẩu phải ít nhất 6 ký tự.");

            return new ValidationResult(errors);
        }

        private static bool IsValidPhone(string phone)
        {
            return phone.Length >= 10 && phone.Length <= 11 && phone.All(char.IsDigit);
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