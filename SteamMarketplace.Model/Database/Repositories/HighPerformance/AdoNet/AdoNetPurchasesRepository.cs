using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetPurchasesRepository : IPurchasesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetPurchasesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(Guid saleId)
        {
            var query = $"SELECT * " +
                    $"FROM \"Purchases\" " +
                    $"WHERE \"Purchases\".\"SaleId\" = @SaleId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@SaleId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = saleId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetPurchaseId(Guid saleId)
        {
            var query = $"SELECT \"Id\" " +
                    $"FROM \"Purchases\" " +
                    $"WHERE \"Purchases\".\"SaleId\" = @SaleId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@SaleId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = saleId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Purchase entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!checkOnUnique || !Contains(entity.SaleId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"Purchases\" (\"Id\", \"BuyerId\", \"SaleId\", \"Price\", \"PriceUsd\", \"PurchaseAt\") VALUES " +
                    $"(@Id, @BuyerId, @SaleId, @Price, @PriceUsd, @PurchaseAt)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@BuyerId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.BuyerId },
                    new NpgsqlParameter() { ParameterName = "@SaleId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.SaleId },
                    new NpgsqlParameter() { ParameterName = "@Price", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.Price },
                    new NpgsqlParameter() { ParameterName = "@PriceUsd", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.PriceUsd },
                    new NpgsqlParameter() { ParameterName = "@PurchaseAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.PurchaseAt }
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = GetPurchaseId(entity.SaleId);
            }

            return true;
        }
    }
}
