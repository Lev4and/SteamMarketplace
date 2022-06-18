using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ITransactionTypesRepository : IFilterableRepository<TransactionType, TransactionTypesFilters>, ICRUDRepository<TransactionType>
    {
        IQueryable<TransactionType> GetAllTransactionTypes();
    }
}
