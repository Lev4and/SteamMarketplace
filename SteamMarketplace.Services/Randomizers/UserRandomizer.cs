﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using System.Text;

namespace SteamMarketplace.Services.Randomizers
{
    public class UserRandomizer
    {
        private readonly Random _random;
        private readonly ILogger<UserRandomizer> _logger;
        private readonly HighPerformanceDataManager _dataManager;

        public UserRandomizer(HighPerformanceDataManager dataManager, ILogger<UserRandomizer> logger)
        {
            _logger = logger;
            _random = new Random();
            _dataManager = dataManager;
        }

        private Guid GetRoleId()
        {
            return _dataManager.ApplicatonRoles.GetRoleIdByName("Игрок");
        }

        private Guid GetCurrencyId()
        {
            return _dataManager.Currencies.GetRandomCurrencyId();
        }

        private Guid GetTransactionTypeId()
        {
            return _dataManager.TransactionTypes.GetTransactionTypeIdByRuName("Пополнение");
        }

        private decimal GetRateCurrency(Guid currencyId)
        {
            return _dataManager.ExchangeRates.GetRateCurrency(currencyId);
        }

        private string GetUserName()
        {
            var chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZ0123456789_-";

            return new string(Enumerable.Repeat(chars, 12).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public void CreateUser()
        {
            var userName = GetUserName();

            var user = new ApplicationUser()
            {
                CurrencyId = GetCurrencyId(),
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = $"{userName}@gmail.com",
                NormalizedEmail = $"{userName.ToUpper()}@GMAIL.COM",
                RegisteredAt = DateTime.UtcNow,
                WalletBalance = 0,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, userName),
                SecurityStamp = string.Empty
            };

            _dataManager.ApplicationUsers.Save(user);

            _dataManager.UserRoles.Save(new IdentityUserRole<Guid> 
            { 
                UserId = user.Id,
                RoleId = GetRoleId()
            });

            var walletBalance = _random.Next(0, 1000001) * GetRateCurrency(user.CurrencyId);

            _dataManager.Transactions.Save(new Transaction
            {
                UserId = user.Id,
                TypeId = GetTransactionTypeId(),
                Value = walletBalance,
                HappenedAt = DateTime.UtcNow
            });

            _dataManager.ApplicationUsers.TopUpWalletBalance(user.Id, walletBalance);

            _logger.LogDebug($"The user {user.UserName} has joined to SteamMarketplace");
        }
    }
}
