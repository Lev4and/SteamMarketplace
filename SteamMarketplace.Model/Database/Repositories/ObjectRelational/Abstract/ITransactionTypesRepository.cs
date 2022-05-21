using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ITransactionTypesRepository
    {
        IQueryable<TransactionType> GetAllTransactionTypes();
    }
}
