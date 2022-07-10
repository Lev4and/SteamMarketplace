using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
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

            var query = $"INSERT INTO \"Transactions\" (\"Id\", \"UserId\", \"TypeId\", \"PurchaseId\", \"Value\", \"HappenedAt\") VALUES " +
                $"(@Id, @UserId, @TypeId, @PurchaseId, @Value, @HappenedAt)";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                new NpgsqlParameter() { ParameterName = "@UserId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.UserId },
                new NpgsqlParameter() { ParameterName = "@TypeId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.TypeId },
                new NpgsqlParameter() { ParameterName = "@PurchaseId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.PurchaseId.GetDbValue() },
                new NpgsqlParameter() { ParameterName = "@Value", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.Value },
                new NpgsqlParameter() { ParameterName = "@HappenedAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.HappenedAt },
            };

            _context.ExecuteQuery(query, parameters);

            return true;
        }
    }
}
