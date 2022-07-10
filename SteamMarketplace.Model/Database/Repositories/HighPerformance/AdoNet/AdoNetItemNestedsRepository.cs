using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
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
            var query = $"SELECT * " +
                    $"FROM \"ItemNesteds\" " +
                    $"WHERE \"ItemNesteds\".\"ItemId\" = @ItemId AND \"ItemNesteds\".\"ItemNestedId\" = @ItemNestedId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId },
                new NpgsqlParameter() { ParameterName = "@ItemNestedId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemNestedId }
            };

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public Guid GetItemNestedId(Guid itemId, Guid itemNestedId)
        {
            var query = $"SELECT \"Id\" " +
                    $"FROM \"ItemNesteds\" " +
                    $"WHERE \"ItemNesteds\".\"ItemId\" = @ItemId AND \"ItemNesteds\".\"ItemNestedId\" = @ItemNestedId " +
                    $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemId },
                new NpgsqlParameter() { ParameterName = "@ItemNestedId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = itemNestedId }
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

                var query = $"INSERT INTO \"ItemNesteds\" (\"Id\", \"ItemId\", \"ItemNestedId\") VALUES (@Id, @ItemId, @ItemNestedId)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@ItemId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.ItemId },
                    new NpgsqlParameter() { ParameterName = "@ItemNestedId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.ItemNestedId }
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
