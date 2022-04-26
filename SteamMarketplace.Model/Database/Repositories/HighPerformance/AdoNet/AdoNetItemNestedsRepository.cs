using Microsoft.Data.SqlClient;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetItemNestedsRepository : IItemNestedsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetItemNestedsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(Guid itemId, Guid itemNestedId)
        {
            var query = $"SELECT TOP(1) * " +
                    $"FROM ItemNesteds " +
                    $"WHERE ItemNesteds.ItemId = @ItemId AND ItemNesteds.ItemNestedId = @ItemNestedId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId },
                new SqlParameter() { ParameterName = "@ItemNestedId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemNestedId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetItemNestedId(Guid itemId, Guid itemNestedId)
        {
            var query = $"SELECT TOP(1) Id " +
                    $"FROM ItemNesteds " +
                    $"WHERE ItemNesteds.ItemId = @ItemId AND ItemNesteds.ItemNestedId = @ItemNestedId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemId },
                new SqlParameter() { ParameterName = "@ItemNestedId", SqlDbType = SqlDbType.UniqueIdentifier, Value = itemNestedId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(ItemNested entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            if (!checkOnUnique || !Contains(entity.ItemId, entity.ItemNestedId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO [ItemNesteds] (Id, ItemId, ItemNestedId) VALUES (@Id, @ItemId, @ItemNestedId)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@ItemId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ItemId },
                    new SqlParameter() { ParameterName = "@ItemNestedId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.ItemNestedId }
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = GetItemNestedId(entity.ItemId, entity.ItemNestedId);
            }

            return true;
        }
    }
}
