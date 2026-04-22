// Application/Features/Trips/CompleteTrip/CompleteTripCommand.cs
using System;
using Domain.ValueObjects;

namespace Application.Features.Trips.CompleteTrip
{
    public class CompleteTripCommand
    {
        public Guid TripId { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public Money Fare { get; set; }
    }
}