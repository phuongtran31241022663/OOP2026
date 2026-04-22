using Domain.Enums;
using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFareRuleService
    {
        Task<Fare> CalculateFare(VehicleType vehicleType);
    }
}
