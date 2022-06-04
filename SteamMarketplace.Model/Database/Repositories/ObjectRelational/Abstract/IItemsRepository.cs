﻿using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IItemsRepository
    {
        int GetCountItems();

        int GetCountItems(string fullName);

        int GetCountOwnersItems(string fullName);

        int GetCountGroupedItems(ItemsFilters filters);

        double GetRarityItem(string fullName);

        float? GetAverageFloatItem(string fullName);

        DateTime GetMinAddedAtItem(string fullName);

        Item GetItemByFullName(string fullName);

        IQueryable<string> GetSearchSuggestions(string searchString);

        IQueryable<GroupedItem> GetGroupedItems(ItemsFilters filters);
    }
}
