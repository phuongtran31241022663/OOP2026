using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRouteService
    {
        Task<Route> GetRouteAsync(Location pickup, Location destination);
    }
}
