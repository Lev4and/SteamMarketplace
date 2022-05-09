namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface ITransactionTypesRepository
    {
        Guid GetTransactionTypeIdByRuName(string ruName);
    }
}
