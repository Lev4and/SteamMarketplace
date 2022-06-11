using Microsoft.Data.SqlClient;
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
            var query = $"UPDATE [Sales] " +
                    $"SET SoldAt = @SoldAt " +
                    $"WHERE Sales.Id = @Id";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@SoldAt", SqlDbType = SqlDbType.DateTime2, Value = DateTime.UtcNow },
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = saleId },
            };

            _context.ExecuteQuery(query, parameters);
        }

        public bool Contains(Guid itemId)
        {
            var query = $"SELECT TOP(1) * " +
                    $"FROM Sales " +
                    $"WHERE Sales.ItemId = @ItemId AND Sales.SoldAt IS NULL";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public List<RandomSale> GetRandomSales(Guid buyerId, int limit)
        {
            var query = $"SELECT TOP(@Limit) Sales.Id, Sales.SellerId, Sales.ItemId, Sales.Price, Sales.PriceUsd, " +
                $"Items.FullName as ItemFullName, ItemImages.SteamImg AS ItemSteamImage, " +
                $"AspNetUsers.UserName AS SellerName " +
                $"FROM Sales INNER JOIN " +
                $"Items ON Items.Id = Sales.ItemId INNER JOIN " +
                $"ItemImages ON ItemImages.ItemId = Sales.ItemId INNER JOIN " +
                $"AspNetUsers ON AspNetUsers.Id = Sales.SellerId " +
                $"WHERE Sales.SoldAt IS NULL AND Sales.CancelledAt IS NULL AND Sales.SellerId != @BuyerId " +
                $"ORDER BY NEWID()";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Limit", SqlDbType = SqlDbType.Int, Value = limit },
                new SqlParameter() { ParameterName = "@BuyerId", SqlDbType = SqlDbType.UniqueIdentifier, Value = buyerId },
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
            var query = $"SELECT TOP(1) Id " +
                    $"FROM Sales " +
                    $"WHERE Sales.ItemId = @ItemId AND Sales.SoldAt IS NULL";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId }
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

                var query = $"INSERT INTO [Sales] (Id, SellerId, ItemId, Price, PriceUsd, ExposedAt, SoldAt) VALUES " +
                    $"(@Id, @SellerId, @ItemId, @Price, @PriceUsd, @ExposedAt, @SoldAt)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@SellerId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.SellerId },
                    new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ItemId },
                    new SqlParameter() { ParameterName = "@Price", SqlDbType = SqlDbType.Decimal, Value = entity.Price },
                    new SqlParameter() { ParameterName = "@PriceUsd", SqlDbType = SqlDbType.Decimal, Value = entity.PriceUsd },
                    new SqlParameter() { ParameterName = "@ExposedAt", SqlDbType = SqlDbType.DateTime2, Value = entity.ExposedAt },
                    new SqlParameter() { ParameterName = "@SoldAt", SqlDbType = SqlDbType.DateTime2, Value = entity.SoldAt.GetDbValue() },
                    new SqlParameter() { ParameterName = "@CancelledAt", SqlDbType = SqlDbType.DateTime2, Value = entity.CancelledAt.GetDbValue() },
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
