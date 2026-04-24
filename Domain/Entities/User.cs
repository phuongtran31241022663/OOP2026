using Common.Utilities;
using System;
using Domain.SharedKernel;

namespace Domain.Entities
{
    public abstract class User : Entity
    {
        #region Fields
        private string _name;
        private string _phone;
        private string _password;
        #endregion
        #region Properties
        public string Name
        {
            get => _name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception(nameof(Name), new Exception("Tên không được để trống."));
                _name = value.Trim();
            }
        }

        public string Phone
        {
            get => _phone;
            protected set
            {
                // StringExtensions.IsValidPhoneNumber
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception(nameof(Phone), new Exception("Số điện thoại không hợp lệ."));
                _phone = value.Trim();
            }
        }

        public string Password => _password;
        #endregion
        #region Constructors
        // Constructor nghiệp vụ cho Register: Nhận pass chưa hash (tự động generate Id)
        protected User(string name, string phone, string rawPassword) : base(Guid.NewGuid())
        {
            Name = name;
            Phone = phone;

            if (string.IsNullOrWhiteSpace(rawPassword) || rawPassword.Length < 6)
                throw new ArgumentException("Mật khẩu phải từ 6 ký tự trở lên.", nameof(rawPassword));

            _password = PasswordHasher.HashPassword(rawPassword);
        }

        // Constructor nghiệp vụ cho persistence (đã có sẵn Id)
        protected User(Guid id, string name, string phone, string rawPassword) : base(id)
        {
            Name = name;
            Phone = phone;

            // Password từ DB đã là hash, nên gán trực tiếp
            _password = rawPassword;
        }

        // Constructor dành cho ORM/JSON serialization
        private User() : base(default)
        {
        }
        #endregion
        #region Methods

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void UpdatePhone(string newPhone)
        {
            Phone = newPhone;
        }

        public void ChangePassword(string oldRaw, string newRaw)
        {
            if (!VerifyPassword(oldRaw))
                throw new Exception(nameof(Password), new Exception("Sai mật khẩu cũ."));

            if (oldRaw == newRaw)
                throw new Exception(nameof(Password), new Exception("Mật khẩu mới không được trùng mật khẩu cũ."));

            if (string.IsNullOrWhiteSpace(oldRaw))
                throw new Exception(nameof(Password), new Exception("Mật khẩu cũ không được để trống."));

            if (string.IsNullOrWhiteSpace(newRaw) || newRaw.Length < 6)
                throw new Exception(nameof(Password), new Exception("Mật khẩu mới phải có ít nhất 6 ký tự."));

            _password = PasswordHasher.HashPassword(newRaw);
        }
        public bool VerifyPassword(string rawInput)
        {
            return PasswordHasher.VerifyPassword(rawInput, _password);
        }
        public virtual string GetInfo()
        {
            return $"ID: {Id.ToString().Substring(0, 8)} | T�n: {Name}";
        }
        #endregion
    }
}
