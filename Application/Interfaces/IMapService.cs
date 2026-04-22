using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMapService
    {
        // Trả về khoảng cách (mét) và thời gian (giây) giữa 2 điểm
        Task<(double distance, double duration)> GetDistanceAsync(Location origin, Location destination);
    }
}

