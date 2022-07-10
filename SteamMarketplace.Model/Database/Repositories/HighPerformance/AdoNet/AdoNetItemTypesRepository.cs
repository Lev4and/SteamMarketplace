using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetItemTypesRepository : IItemTypesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetItemTypesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(int cSMoneyId, out Guid id)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"ItemTypes\" " +
                $"WHERE \"ItemTypes\".\"CSMoneyId\" = @CSMoneyId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CSMoneyId", NpgsqlDbType = NpgsqlDbType.Integer, Value = cSMoneyId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetItemTypeId(int cSMoneyId)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"ItemTypes\" " +
                $"WHERE \"ItemTypes\".\"CSMoneyId\" = @CSMoneyId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CSMoneyId", NpgsqlDbType = NpgsqlDbType.Integer, Value = cSMoneyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(ItemType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!Contains(entity.CSMoneyId, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"ItemTypes\" (\"Id\", \"CSMoneyId\", \"Name\") VALUES (@Id, @CSMoneyId, @Name)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@CSMoneyId", NpgsqlDbType = NpgsqlDbType.Integer, Value = entity.CSMoneyId },
                    new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Name.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetItemTypeId(entity.CSMoneyId) : foundId;
            }

            return true;
        }
    }
}
