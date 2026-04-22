namespace Domain.Enums
{
    /// <summary>
    /// Trạng thái làm việc của tài xế
    /// </summary>
    public enum DriverStatus
    {
        Offline = 0,     // Không online
        Available = 1,   // Có thể nhận công
        OnTrip = 2       // Đang chạy
    }
}
