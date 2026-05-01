namespace Domain.Enums
{
    /// <summary>
    /// Represents the slot/role of a location on the map (pickup or destination).
    /// This enum is used by the Presentation layer to determine which location
    /// the user is currently selecting on the map.
    /// </summary>
    public enum MapSlot
    {
        /// <summary>
        /// No slot selected - user is not selecting a location.
        /// </summary>
        None = 0,

        /// <summary>
        /// Pickup location slot - user is selecting a pickup/ origin location.
        /// </summary>
        Pickup = 1,

        /// <summary>
        /// Destination location slot - user is selecting a destination location.
        /// </summary>
        Destination = 2
    }
}
