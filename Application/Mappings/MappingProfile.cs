using System.Collections.Generic;

namespace Application.Mappings
{
    /// <summary>
    /// Maps between Domain Entities and DTOs (Data Transfer Objects).
    /// This class documents all the mapping logic used in the application.
    /// 
    /// NOTE: The project uses manual mapping with static extension methods
    /// in the DTOs folder rather than AutoMapper for explicit control.
    /// 
    /// Mapping pattern: ToDto() and ToDomain() extension methods
    /// Location: Application/DTOs/*Mapper.cs files
    /// 
    /// Mapped Entities:
    /// - Driver → DriverDto (DriverMapper.cs)
    /// - Passenger → PassengerDto (PassengerMapper.cs)
    /// - Trip → TripDto (TripMapper.cs)
    /// - User → UserDto (no mapper yet - can use User directly)
    /// - Fare → FareDto
    /// 
    /// Custom builders:
    /// - TripRequestBuilder.cs - Builds trip requests from domain data
    /// </summary>
    public class MappingProfile
    {
        /// <summary>
        /// Gets a list of all mapper classes used in the application.
        /// Each mapper provides ToDto() and ToDomain() methods.
        /// </summary>
        public static IEnumerable<string> GetMappedEntities()
        {
            return new List<string>
            {
                "Driver (DriverMapper.cs)",
                "Passenger (PassengerMapper.cs)",
                "Trip (TripMapper.cs)",
                "Fare (FareDto.cs)"
            };
        }

        /// <summary>
        /// Describes the architecture of the mapping layer:
        /// 
        /// Advantages of manual mapping:
        /// - Type-safe and explicit
        /// - No reflection overhead
        /// - Easy to debug and maintain
        /// - Full control over transformation logic
        /// 
        /// Location of mappers: Application/DTOs/
        /// Pattern: Static extension methods or factory methods
        /// </summary>
        public static string GetMappingStrategy() => "Manual Mapping with Static Extensions";
    }
}
