﻿using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IRaritiesRepository
    {
        IQueryable<Rarity> GetAllRarities();
    }
}