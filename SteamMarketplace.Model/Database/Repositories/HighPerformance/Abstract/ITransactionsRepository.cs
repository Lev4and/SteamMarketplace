using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface ITransactionsRepository
    {
        bool Save(Transaction entity);
    }
}
