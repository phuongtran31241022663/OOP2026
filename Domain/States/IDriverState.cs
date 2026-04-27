using Domain.Entities.Users;

namespace Domain.States
{
    public interface IDriverState
    {
        void SetAvailable(Driver driver);
        void SetOnTrip(Driver driver);
        void SetOffline(Driver driver);
    }
}

