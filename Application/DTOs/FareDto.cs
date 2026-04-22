namespace Application.DTOs
{
    internal class FareDto
    {
        public decimal TotalAmount { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal Commission { get; set; }
        public decimal DriverIncome { get; set; }
        public string Currency { get; set; } = "VND";
    }
}
