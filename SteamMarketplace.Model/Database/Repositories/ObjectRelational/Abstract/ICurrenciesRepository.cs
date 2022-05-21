﻿using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ICurrenciesRepository
    {
        IQueryable<Currency> GetAllCurrencies();
    }
}