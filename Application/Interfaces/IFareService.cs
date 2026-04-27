using Domain.Enums;
using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFareService
    {
        Task<Fare> CalculateFareAsync(VehicleType vehicleType, double distanceKM);
    }
}
