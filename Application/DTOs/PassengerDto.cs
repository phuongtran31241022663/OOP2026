namespace Application.DTOs
{
    public class PassengerDto
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int TotalTrips { get; set; }
    }
}