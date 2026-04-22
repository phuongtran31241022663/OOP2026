using Application.Interfaces;
using Domain.Trips;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RouteService : IRouteService
    {
        public Task<Route> GetRouteAsync(Location pickup, Location destination)
        {
          
        }
    }
}

