using Microsoft.Data.SqlClient;
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
            var query = $"SELECT TOP(1) * " +
                    $"FROM Purchases " +
                    $"WHERE Purchases.SaleId = @SaleId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@SaleId", SqlDbType = SqlDbType.UniqueIdentifier, Value = saleId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetPurchaseId(Guid saleId)
        {
            var query = $"SELECT TOP(1) Id " +
                    $"FROM Purchases " +
                    $"WHERE Purchases.SaleId = @SaleId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@SaleId", SqlDbType = SqlDbType.UniqueIdentifier, Value = saleId }
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

                var query = $"INSERT INTO [Purchases] (Id, BuyerId, SaleId, Price, PriceUsd, PurchaseAt) VALUES " +
                    $"(@Id, @BuyerId, @SaleId, @Price, @PriceUsd, @PurchaseAt)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@BuyerId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.BuyerId },
                    new SqlParameter() { ParameterName = "@SaleId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.SaleId },
                    new SqlParameter() { ParameterName = "@Price", SqlDbType = SqlDbType.Decimal, Value = entity.Price },
                    new SqlParameter() { ParameterName = "@PriceUsd", SqlDbType = SqlDbType.Decimal, Value = entity.PriceUsd },
                    new SqlParameter() { ParameterName = "@PurchaseAt", SqlDbType = SqlDbType.DateTime2, Value = entity.PurchaseAt }
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
