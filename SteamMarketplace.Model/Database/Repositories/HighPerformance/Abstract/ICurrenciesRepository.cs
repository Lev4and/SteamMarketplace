using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface ICurrenciesRepository
    {
        bool Contains(string cultureInfoName, out Guid id);

        bool Save(Currency entity, bool checkOnUnique = true);

        Guid GetCurrencyId(string cultureInfoName);

        Guid GetCurrencyIdByLiteral(string literal);
    }
}
