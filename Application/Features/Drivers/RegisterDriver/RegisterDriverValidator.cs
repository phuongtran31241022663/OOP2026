using System.Collections.Generic;
using System.Linq;

namespace Application.Features.Drivers.RegisterDriver
{
    public static class RegisterDriverValidator
    {
        public static ValidationResult Validate(RegisterDriverCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(command.Name))
                errors.Add("Tên không được để trống.");

            if (string.IsNullOrWhiteSpace(command.Phone) || !IsValidPhone(command.Phone))
                errors.Add("Số điện thoại không hợp lệ.");

            if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < 6)
                errors.Add("Mật khẩu phải ít nhất 6 ký tự.");

            if (command.VehicleType == null)
                errors.Add("Vehicle type không được để trống.");

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
