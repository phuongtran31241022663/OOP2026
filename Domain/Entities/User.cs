using Common.Utilities;
using System;
using Domain.SharedKernel;

namespace Domain.Entities
{
    /// <summary>
    /// Lớp trừu tượng cơ sở cho tất cả các loại người dùng trong hệ thống.
    /// </summary>
    /// <remarks>
    /// Chứa các thông tin chung như Tên, Số điện thoại và Mật khẩu đã được hash.
    /// Cung cấp các phương thức để cập nhật thông tin và xác thực mật khẩu.
    /// </remarks>
    public abstract class User : Entity
    {
        #region Fields

        private string _name;
        private string _phone;
        private string _password;

        #endregion

        #region Properties

        /// <summary>
        /// Tên của người dùng.
        /// </summary>
        /// <exception cref="ArgumentException">Ném ra khi tên là rỗng hoặc khoảng trắng.</exception>
        public string Name
        {
            get => _name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên không được để trống.", nameof(Name));
                _name = value.Trim();
            }
        }

        /// <summary>
        /// Số điện thoại của người dùng.
        /// </summary>
        /// <exception cref="ArgumentException">Ném ra khi số điện thoại không hợp lệ.</exception>
        public string Phone
        {
            get => _phone;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Số điện thoại không hợp lệ.", nameof(Phone));
                _phone = value.Trim();
            }
        }

        /// <summary>
        /// Mật khẩu đã được hash của người dùng.
        /// </summary>
        public string Password => _password;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor để tạo mới người dùng (ví dụ: khi đăng ký).
        /// Tự động tạo Id mới và hash mật khẩu.
        /// </summary>
        /// <param name="name">Tên người dùng.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="rawPassword">Mật khẩu thô (chưa hash), phải có ít nhất 6 ký tự.</param>
        /// <exception cref="ArgumentException">Ném ra khi mật khẩu không hợp lệ.</exception>
        protected User(string name, string phone, string rawPassword) : base(Guid.NewGuid())
        {
            Name = name;
            Phone = phone;

            if (string.IsNullOrWhiteSpace(rawPassword) || rawPassword.Length < 6)
                throw new ArgumentException("Mật khẩu phải từ 6 ký tự trở lên.", nameof(rawPassword));

            _password = PasswordHasher.HashPassword(rawPassword);
        }

        /// <summary>
        /// Constructor để tái tạo đối tượng người dùng từ cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">Id đã tồn tại của người dùng.</param>
        /// <param name="name">Tên người dùng.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="hashedPassword">Mật khẩu đã được hash từ CSDL.</param>
        protected User(Guid id, string name, string phone, string hashedPassword) : base(id)
        {
            Name = name;
            Phone = phone;
            _password = hashedPassword;
        }

        /// <summary>
        /// Constructor không tham số cho các công cụ ORM hoặc serialization.
        /// </summary>
        protected User() : base(default)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Cập nhật tên của người dùng.
        /// </summary>
        /// <param name="newName">Tên mới.</param>
        public void UpdateName(string newName)
        {
            Name = newName;
        }

        /// <summary>
        /// Cập nhật số điện thoại của người dùng.
        /// </summary>
        /// <param name="newPhone">Số điện thoại mới.</param>
        public void UpdatePhone(string newPhone)
        {
            Phone = newPhone;
        }

        /// <summary>
        /// Thay đổi mật khẩu của người dùng.
        /// </summary>
        /// <param name="oldRaw">Mật khẩu cũ (dạng thô) để xác thực.</param>
        /// <param name="newRaw">Mật khẩu mới (dạng thô), phải có ít nhất 6 ký tự và khác mật khẩu cũ.</param>
        /// <exception cref="ArgumentException">Ném ra khi mật khẩu cũ/mới không hợp lệ.</exception>
        public void ChangePassword(string oldRaw, string newRaw)
        {
            if (string.IsNullOrWhiteSpace(oldRaw))
                throw new ArgumentException("Mật khẩu cũ không được để trống.", nameof(oldRaw));

            if (!VerifyPassword(oldRaw))
                throw new ArgumentException("Sai mật khẩu cũ.", nameof(oldRaw));
            
            if (oldRaw == newRaw)
                throw new ArgumentException("Mật khẩu mới không được trùng mật khẩu cũ.", nameof(newRaw));

            if (string.IsNullOrWhiteSpace(newRaw) || newRaw.Length < 6)
                throw new ArgumentException("Mật khẩu mới phải có ít nhất 6 ký tự.", nameof(newRaw));

            _password = PasswordHasher.HashPassword(newRaw);
        }

        /// <summary>
        /// Xác thực mật khẩu thô với mật khẩu đã được hash.
        /// </summary>
        /// <param name="rawInput">Mật khẩu thô cần kiểm tra.</param>
        /// <returns><c>true</c> nếu mật khẩu khớp, ngược lại <c>false</c>.</returns>
        public bool VerifyPassword(string rawInput)
        {
            return PasswordHasher.VerifyPassword(rawInput, _password);
        }

        /// <summary>
        /// Lấy thông tin cơ bản của người dùng dưới dạng chuỗi.
        /// </summary>
        /// <returns>Chuỗi chứa Id và Tên người dùng.</returns>
        public virtual string GetInfo()
        {
            return $"ID: {Id.ToString().Substring(0, 8)} | Tên: {Name}";
        }

        #endregion
    }
}
