using Application.Events;
using Application.Interfaces;
using System;

namespace Presentation.Helpers
{
    public static class EventHelper
    {
        public static void SubscribeToTripEvents(ITripService tripService, Action<string> onMessage)
        {
            if (tripService == null || onMessage == null)
            {
                return;
            }

            tripService.TripStatusChanged += delegate (object sender, TripStatusChangedEventArgs e)
            {
                onMessage(string.Format("Trip {0}: {1}", e.TripId, e.NewStatus));
            };
        }
    }
}
