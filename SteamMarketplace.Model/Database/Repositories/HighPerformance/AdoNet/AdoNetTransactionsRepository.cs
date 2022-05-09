using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetTransactionsRepository : ITransactionsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetTransactionsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Save(Transaction entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            entity.Id = Guid.NewGuid();

            var query = $"INSERT INTO [Transactions] (Id, UserId, TypeId, PurchaseId, Value, HappenedAt) VALUES " +
                $"(@Id, @UserId, @TypeId, @PurchaseId, @Value, @HappenedAt)";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.UserId },
                new SqlParameter() { ParameterName = "@TypeId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.TypeId },
                new SqlParameter() { ParameterName = "@PurchaseId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.PurchaseId.GetDbValue() },
                new SqlParameter() { ParameterName = "@Value", SqlDbType = SqlDbType.Decimal, Value = entity.Value },
                new SqlParameter() { ParameterName = "@HappenedAt", SqlDbType = SqlDbType.DateTime2, Value = entity.HappenedAt },
            };

            _context.ExecuteQuery(query, parameters);

            return true;
        }
    }
}
