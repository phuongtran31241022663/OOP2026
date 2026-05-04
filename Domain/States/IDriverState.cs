using Domain.Entities.Users;

namespace Domain.States
{
    /// <summary>
    /// Interface for Driver state machine.
    /// Each concrete state encapsulates the behavior and state transitions for a specific Driver state.
    /// For serialization, use TypeNameHandling.Auto in JsonSerializerSettings.
    /// </summary>
    public interface IDriverState
    {
        void SetAvailable(Driver driver);
        void SetOnTrip(Driver driver);
        void SetOffline(Driver driver);
    }
}
