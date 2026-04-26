using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
// Presentation/Helpers/EventHelper.cs
using Application.Interfaces;
using System;

namespace Presentation.Helpers
{
    public static class EventHelper
    {
        public static void SubscribeToNotifications(INotificationService notificationService, Action<string> onMessage)
        {
            notificationService.OnPassengerNotified += (id, msg) => onMessage($"Passenger {id}: {msg}");
            notificationService.OnDriverNotified += (id, msg) => onMessage($"Driver {id}: {msg}");
            notificationService.OnTripUpdated += (id, msg) => onMessage($"Trip {id}: {msg}");
        }

        // Note: Unsubscribe manually in UI code
    }
}
