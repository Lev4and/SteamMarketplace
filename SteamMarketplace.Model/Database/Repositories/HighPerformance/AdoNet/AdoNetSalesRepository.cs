using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetSalesRepository : ISalesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetSalesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public void CloseSale(Guid saleId)
        {
            var query = $"UPDATE \"Sales\" " +
                    $"SET \"SoldAt\" = @SoldAt " +
                    $"WHERE \"Sales\".\"Id\" = @Id";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@SoldAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = DateTime.UtcNow },
                new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = saleId },
            };

            _context.ExecuteQuery(query, parameters);
        }

        public bool Contains(Guid itemId)
        {
            var query = $"SELECT * " +
                    $"FROM \"Sales\" " +
                    $"WHERE \"Sales\".\"ItemId\" = @ItemId AND \"Sales\".\"SoldAt\" IS NULL " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public List<RandomSale> GetRandomSales(Guid buyerId, int limit)
        {
            var query = $"SELECT \"Sales\".\"Id\", \"Sales\".\"SellerId\", \"Sales\".\"ItemId\", \"Sales\".\"Price\", \"Sales\".\"PriceUsd\", " +
                $"\"Items\".\"FullName\" as \"ItemFullName\", \"ItemImages\".\"SteamImg\" AS \"ItemSteamImage\", " +
                $"\"AspNetUsers\".\"UserName\" AS \"SellerName\" " +
                $"FROM \"Sales\" INNER JOIN " +
                $"\"Items\" ON \"Items\".\"Id\" = \"Sales\".\"ItemId\" INNER JOIN " +
                $"\"ItemImages\" ON \"ItemImages\".\"ItemId\" = \"Sales\".\"ItemId\" INNER JOIN " +
                $"\"AspNetUsers\" ON \"AspNetUsers\".\"Id\" = \"Sales\".\"SellerId\" " +
                $"WHERE \"Sales\".\"SoldAt\" IS NULL AND \"Sales\".\"CancelledAt\" IS NULL AND \"Sales\".\"SellerId\" != @BuyerId " +
                $"ORDER BY random() " +
                $"LIMIT @Limit";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Limit", NpgsqlDbType = NpgsqlDbType.Integer, Value = limit },
                new NpgsqlParameter() { ParameterName = "@BuyerId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = buyerId },
            };

            var result = new List<RandomSale>();

            foreach (DataRow item in _context.ExecuteQuery(query, parameters).Rows)
            {
                result.Add(item.ToObject<RandomSale>());
            }

            return result;
        }

        public Guid GetSaleId(Guid itemId)
        {
            var query = $"SELECT \"Id\" " +
                    $"FROM \"Sales\" " +
                    $"WHERE \"Sales\".\"ItemId\" = @ItemId AND \"Sales\".\"SoldAt\" IS NULL " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Sale entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!checkOnUnique || !Contains(entity.ItemId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"Sales\" (\"Id\", \"SellerId\", \"ItemId\", \"Price\", \"PriceUsd\", \"ExposedAt\", \"SoldAt\") VALUES " +
                    $"(@Id, @SellerId, @ItemId, @Price, @PriceUsd, @ExposedAt, @SoldAt)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@SellerId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.SellerId },
                    new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.ItemId },
                    new NpgsqlParameter() { ParameterName = "@Price", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.Price },
                    new NpgsqlParameter() { ParameterName = "@PriceUsd", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.PriceUsd },
                    new NpgsqlParameter() { ParameterName = "@ExposedAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.ExposedAt },
                    new NpgsqlParameter() { ParameterName = "@SoldAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.SoldAt.GetDbValue() },
                    new NpgsqlParameter() { ParameterName = "@CancelledAt", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.CancelledAt.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = GetSaleId(entity.ItemId);
            }

            return true;
        }
    }
}
