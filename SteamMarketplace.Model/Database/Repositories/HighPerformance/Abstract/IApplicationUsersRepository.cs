using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IApplicationUsersRepository
    {
        bool Contains(string username, out Guid id);

        bool Save(ApplicationUser entity, bool checkOnUnique = true);

        Guid GetRandomUserId();

        Guid GetUserId(string username);

        Guid GetCurrencyId(Guid userId);

        decimal GetWalletBalance(Guid userId);

        void TopUpWalletBalance(Guid userId, decimal value);

        void ReduceWalletBalance(Guid userId, decimal value);
    }
}
