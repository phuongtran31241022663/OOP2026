using Domain.Entities;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
﻿// Infrastructure/Repositories/UserRepository.cs

namespace Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        public UserRepository() : base("users.json") { }

        public async Task<Driver> GetDriverByIdAsync(Guid id)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.OfType<Driver>().FirstOrDefault(d => d.Id == id);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.FirstOrDefault(u => u.Phone == phone);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Any(u => u.Phone == phone);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<List<User>> GetDriversAsync()
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.OfType<Driver>().Cast<User>().ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<List<User>> GetPassengersAsync()
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Where(u => u is Passenger).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.OfType<Driver>().Where(d => d.IsAvailable()).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}

