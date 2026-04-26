using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
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

        public static object ToPassenger(object passenger)
        {
            return new { Id = "mock", Name = "mock" };
        }
    }
}
