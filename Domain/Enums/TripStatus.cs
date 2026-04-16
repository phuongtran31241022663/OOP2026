namespace Domain.Enums
{
    /// <summary>
    /// Trạng thái của một chuyến đi (Trip)
    /// </summary>
    public enum TripStatus
    {
        Requested = 0,   // Đã yêu cầu (Khách hàng vừa bấm đặt xe)
        Searching = 1,   // Đang tìm tài xế (Hệ thống đang quét tìm xe gần đây)
        Matched = 2,     // Đã tìm thấy tài xế (Tài xế đã chấp nhận chuyến)
        Arrived = 3,     // Tài xế đã đến (Đã có mặt tại điểm đón)
        Started = 4,     // Đã bắt đầu (Khách đã lên xe, chuyến đi đang diễn ra)
        Completed = 5,   // Đã hoàn thành (Kết thúc hành trình an toàn)
        Cancelled = 6,   // Đã hủy (Chuyến đi bị hủy bởi khách hoặc tài xế)
        Timeout = 7     // Hết thời gian chờ (Không tìm được tài xế trong thời gian quy định)
    }
}
