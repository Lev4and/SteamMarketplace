﻿using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class DefaultDataManager
    {
        public ICollectionsRepository Collections { get; }

        public ICurrenciesRepository Currencies { get; }

        public IItemsRepository Items { get; }

        public IPurchasesRepository Purchases { get; }

        public ISalesRepository Sales { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public IUsersRepository Users { get; }

        public DefaultDataManager(ICollectionsRepository collections, ICurrenciesRepository currencies,
            IItemsRepository items, 
            IPurchasesRepository purchases, ISalesRepository sales, 
            IUserInventoriesRepository userInventories, IUsersRepository users)
        {
            Collections = collections;
            Currencies = currencies;
            Items = items;
            Purchases = purchases;
            Sales = sales;
            UserInventories = userInventories;
            Users = users;
        }
    }
}
