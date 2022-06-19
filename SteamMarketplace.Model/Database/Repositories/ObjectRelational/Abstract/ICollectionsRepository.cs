using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ICollectionsRepository : IFilterableRepository<Collection, CollectionsFilters>, ICRUDRepository<Collection>
    {
        IQueryable<Collection> GetAllCollections();
    }
}
