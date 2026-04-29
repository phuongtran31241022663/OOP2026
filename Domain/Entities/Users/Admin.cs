using System;

namespace Domain.Entities.Users
{
    /// <summary>
    /// Đại diện cho người dùng có vai trò Quản trị viên.
    /// </summary>
    public class Admin : User
    {
        #region Constructors

        /// <summary>
        /// Constructor để tạo mới một Quản trị viên.
        /// </summary>
        /// <param name="name">Tên Quản trị viên.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="password">Mật khẩu (dạng thô).</param>
        public Admin(
            string name,
            string phone,
            string password)
            : base(name, phone, password) // Gọi constructor tạo mới của lớp User
        {
        }

        /// <summary>
        /// Constructor để tái tạo đối tượng Quản trị viên từ cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">ID đã tồn tại.</param>
        /// <param name="name">Tên Quản trị viên.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="hashedPassword">Mật khẩu đã được hash.</param>
        public Admin(
            Guid id,
            string name,
            string phone,
            string hashedPassword)
            : base(id, name, phone, hashedPassword) // Gọi constructor persistence của lớp User
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Ghi đè phương thức để cung cấp thông tin nhận dạng cho Quản trị viên.
        /// </summary>
        /// <returns>Chuỗi thông tin bao gồm vai trò và thông tin cơ bản.</returns>
        public override string GetInfo()
        {
            return "TÀI KHOẢN QUẢN TRỊ VIÊN\n" + base.GetInfo();
        }

        #endregion
    }
}
