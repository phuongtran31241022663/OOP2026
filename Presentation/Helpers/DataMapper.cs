// Presentation/Helpers/DataMapper.cs
// Simplified to avoid Domain dependency

namespace Presentation.Helpers
{
    public static class DataMapper
    {
        public static object ToTrip(object trip)
        {
            return new { Id = "mock", Status = "mock" };
        }

        public static object ToDriver(object driver)
        {
            return new { Id = "mock", Name = "mock" };
        }

        public static object ToPassengerDto(object passenger)
        {
            return new { Id = "mock", Name = "mock" };
        }
    }
}