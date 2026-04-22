using System;
using Domain.Enums;
using Domain.SharedKernel;
using Domain.Vehicles;

namespace Domain.Trips.Events
{
 	public class TripMatchedEvent : DomainEvent
 	{
 	    public Guid TripId { get; }
 	    public Guid DriverId { get; }
        public VehicleType VehicleType { get; }

        public TripMatchedEvent(Guid tripId, Guid driverId, VehicleType vehicleType)
        {
 	        TripId = tripId;
 	        DriverId = driverId;
            VehicleType = vehicleType;
        }
 	}
}
