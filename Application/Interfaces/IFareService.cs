using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFareService
    {
        Task<Fare> CalculateFareAsync(string vehicleType, double distanceKm);
    }
}

