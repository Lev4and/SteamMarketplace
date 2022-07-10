using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Extensions;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetRaritiesRepository : IRaritiesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetRaritiesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(string name, out Guid id)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The name must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"Rarities\" " +
                $"WHERE \"Rarities\".\"Name\" = @Name " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = name }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetRarityId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The name must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"Rarities\" " +
                $"WHERE \"Rarities\".\"Name\" = @Name " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = name }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Rarity entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!checkOnUnique || !Contains(entity.Name, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"Rarities\" (\"Id\", \"Name\", \"RuName\") VALUES (@Id, @Name, @RuName)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@Name", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.Name },
                    new NpgsqlParameter() { ParameterName = "@RuName", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.RuName.GetDbValue() },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetRarityId(entity.Name) : foundId;
            }

            return true;
        }
    }
}
