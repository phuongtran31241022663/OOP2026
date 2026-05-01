﻿using Application.Events;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TripService : ITripService
    {
        public event EventHandler<TripStatusChangedEventArgs> TripStatusChanged;

        private static readonly SemaphoreSlim _matchLock = new SemaphoreSlim(1, 1);

        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFareService _fareService;
        private readonly IMapService _mapService;

        public TripService(
            ITripRepository tripRepository,
            IDriverRepository driverRepository,
            IPassengerRepository passengerRepository,
            IFareService fareService,
            IMapService mapService)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
            _fareService = fareService;
            _mapService = mapService;
        }

        protected virtual void OnTripStatusChanged(TripStatusChangedEventArgs e)
        {
            EventHandler<TripStatusChangedEventArgs> handler = this.TripStatusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // ----- Commands (async) -----
        public async Task<Trip> RequestTripAsync(Guid passengerId, Location pickupLocation, Location destinationLocation, VehicleType vehicleType)
        {
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
            {
                throw new InvalidOperationException("Hành khách không tồn tại.");
            }

            // Lấy thông tin route từ MapService
            Route route = await _mapService.GetRouteAsync(pickupLocation, destinationLocation);
            if (route == null)
            {
                throw new InvalidOperationException("Không thể tìm thấy đường đi.");
            }

            // Tính toán Fare
            Fare fare = await _fareService.CalculateFareAsync(vehicleType, route.Distance);

            Trip trip = new Trip(passengerId, route, fare, vehicleType);
            trip.SetSearching();
            await _tripRepository.AddAsync(trip);
            await _tripRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(trip.Id, trip.Status, null));
            return trip;
        }

        public async Task<Trip> CreateTripAsync(Guid passengerId, Route route, Fare fare, VehicleType vehicleType)
        {
            Trip trip = new Trip(passengerId, route, fare, vehicleType);
            trip.SetSearching();
            await _tripRepository.AddAsync(trip);
            await _tripRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(trip.Id, trip.Status, null));
            return trip;
        }

        public async Task MatchDriverAsync(Guid tripId, Guid driverId)
        {
            await _matchLock.WaitAsync();
            try
            {
                Trip trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null)
                {
                    throw new InvalidOperationException("Không tìm thấy chuyến.");
                }

                Driver driver = await _driverRepository.GetByIdAsync(driverId);
                if (driver == null)
                {
                    throw new InvalidOperationException("Không tìm thấy tài xế.");
                }

                // Kiểm tra ví tài xế có đủ tiền trả hoa hồng không
                if (driver.Wallet.Amount < trip.TripFare.Commission.Amount)
                {
                    throw new InvalidOperationException("Số dư ví không đủ để trả hoa hồng.");
                }

                trip.MatchDriver(driverId);
                driver.SetOnTrip();

                await _tripRepository.UpdateAsync(trip);
                await _driverRepository.UpdateAsync(driver);
                await _tripRepository.SaveChangesAsync();
                await _driverRepository.SaveChangesAsync();
                OnTripStatusChanged(new TripStatusChangedEventArgs(tripId, trip.Status, driverId));
            }
            finally
            {
                _matchLock.Release();
            }
        }

        public async Task MarkAsArrivedAsync(Guid tripId)
        {
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new InvalidOperationException("Không tìm thấy chuyến.");
            }
            trip.MarkAsArrived();
            await _tripRepository.UpdateAsync(trip);
            await _tripRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(tripId, trip.Status, trip.DriverId));
        }

        public async Task StartTripAsync(Guid tripId)
        {
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new InvalidOperationException("Không tìm thấy chuyến.");
            }
            trip.StartTrip();
            await _tripRepository.UpdateAsync(trip);
            await _tripRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(tripId, trip.Status, trip.DriverId));
        }

        public async Task CompleteTripAsync(Guid tripId)
        {
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new InvalidOperationException("Không tìm thấy chuyến.");
            }

            trip.CompleteTrip();
            trip.ConfirmPayment();

            if (trip.DriverId.HasValue)
            {
                Driver driver = await _driverRepository.GetByIdAsync(trip.DriverId.Value);
                if (driver != null)
                {
                    driver.SetAvailable();
                    driver.AddTrip();
                    await _driverRepository.UpdateAsync(driver);
                }
            }

            Passenger passenger = await _passengerRepository.GetByIdAsync(trip.PassengerId);
            if (passenger != null)
            {
                passenger.AddTrip();
                await _passengerRepository.UpdateAsync(passenger);
            }

            await _tripRepository.UpdateAsync(trip);
            await _tripRepository.SaveChangesAsync();
            await _driverRepository.SaveChangesAsync();
            await _passengerRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(tripId, trip.Status, trip.DriverId));
        }

        public async Task CancelTripAsync(Guid tripId, string reason)
        {
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new InvalidOperationException("Không tìm thấy chuyến.");
            }

            trip.Cancel(reason);

            if (trip.DriverId.HasValue)
            {
                Driver driver = await _driverRepository.GetByIdAsync(trip.DriverId.Value);
                if (driver != null)
                {
                    driver.SetAvailable();
                    await _driverRepository.UpdateAsync(driver);
                }
            }

            await _tripRepository.UpdateAsync(trip);
            await _tripRepository.SaveChangesAsync();
            await _driverRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(tripId, trip.Status, trip.DriverId));
        }

        public async Task ConfirmPaymentAsync(Guid tripId)
        {
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new InvalidOperationException("Không tìm thấy chuyến.");
            }
            trip.ConfirmPayment();
            await _tripRepository.UpdateAsync(trip);
            await _tripRepository.SaveChangesAsync();
            OnTripStatusChanged(new TripStatusChangedEventArgs(tripId, trip.Status, trip.DriverId));
        }

        // ----- Queries (async) -----
        public async Task<Trip> GetTripAsync(Guid tripId)
        {
            return await _tripRepository.GetByIdAsync(tripId);
        }

        public async Task<Trip> GetTripByIdAsync(Guid id)
        {
            return await _tripRepository.GetByIdAsync(id);
        }

        public async Task<Trip> GetActiveTripForDriverAsync(Guid driverId)
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            for (int i = 0; i < allTrips.Count; i++)
            {
                Trip trip = allTrips[i];
                if (trip.DriverId == driverId)
                {
                    if (!trip.IsTerminal())
                    {
                        return trip;
                    }
                }
            }
            return null;
        }

        public async Task<Trip> GetActiveTripForPassengerAsync(Guid passengerId)
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            for (int i = 0; i < allTrips.Count; i++)
            {
                Trip trip = allTrips[i];
                if (trip.PassengerId == passengerId)
                {
                    if (!trip.IsTerminal())
                    {
                        return trip;
                    }
                }
            }
            return null;
        }

        public async Task<List<Trip>> GetPendingTripsAsync()
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            List<Trip> pending = new List<Trip>();
            for (int i = 0; i < allTrips.Count; i++)
            {
                Trip trip = allTrips[i];
                if (trip.IsSearching())
                {
                    pending.Add(trip);
                }
            }
            return pending;
        }

        public async Task<List<Trip>> GetTripsByDriverAsync(Guid driverId)
        {
            return await _tripRepository.GetByDriverIdAsync(driverId);
        }

        public async Task<List<Trip>> GetTripsByPassengerAsync(Guid passengerId)
        {
            return await _tripRepository.GetByPassengerIdAsync(passengerId);
        }

        public async Task<bool> CanTripBeCancelledAsync(Guid tripId)
        {
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
            {
                return false;
            }
            return (trip.IsSearching() || trip.IsMatched());
        }
    }
}
