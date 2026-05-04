using System;
using Domain.Entities.Users;

namespace Presentation.Base
{
    // cái interface gì đây, vô nghĩa
    public interface IUserControl
    {
        event EventHandler RequestLogout;
        event EventHandler<User> RequestShowProfile;
    }
}