using System;

namespace Domain.Entities.Users
{
    public class Admin : User
    {
        public Admin(
            string name,
            string phone,
            string password)
            : base(Guid.NewGuid(), name, phone, password)
        {
        }

        // Constructor cho persistence
        public Admin(
            Guid id,
            DateTime createdAt,
            string name,
            string phone,
            string password)
            : base(id, name, phone, password)
        {
        }

        public override string GetInfo()
        {
            return "TÀI KHOẢN QUẢN TRỊ VIÊN\n" + base.GetInfo();
        }
    }
}
