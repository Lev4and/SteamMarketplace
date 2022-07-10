using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetApplicationsRepository : IApplicationsRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetApplicationsRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(int steamId, out Guid id)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"Applications\" " +
                $"WHERE \"Applications\".\"SteamId\" = @SteamId";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@SteamId", NpgsqlDbType = NpgsqlDbType.Integer, Value = steamId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetApplicationId(int steamId)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"Applications\" " +
                $"WHERE \"Applications\".\"SteamId\" = @SteamId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@SteamId", NpgsqlDbType = NpgsqlDbType.Integer, Value = steamId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Application entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!checkOnUnique || !Contains(entity.SteamId, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"Applications\" (\"Id\", \"SteamId\", \"Name\") VALUES (@Id, @SteamId, @Name)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@SteamId", NpgsqlDbType = NpgsqlDbType.Integer, Value = entity.SteamId },
                    new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Name.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetApplicationId(entity.SteamId) : foundId;
            }

            return true;
        }
    }
}
