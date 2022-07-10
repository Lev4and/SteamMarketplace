using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetCurrenciesRepository : ICurrenciesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetCurrenciesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(string cultureInfoName, out Guid id)
        {
            if (string.IsNullOrEmpty(cultureInfoName))
            {
                throw new ArgumentNullException("cultureInfoName", "The culture info name must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"Currencies\" " +
                $"WHERE \"Currencies\".\"CultureInfoName\" = @CultureInfoName " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CultureInfoName", NpgsqlDbType = NpgsqlDbType.Text, Value = cultureInfoName }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetCurrencyId(string cultureInfoName)
        {
            if (string.IsNullOrEmpty(cultureInfoName))
            {
                throw new ArgumentNullException("cultureInfoName", "The culture info name must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"Currencies\" " +
                $"WHERE \"Currencies\".\"CultureInfoName\" = @CultureInfoName " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CultureInfoName", NpgsqlDbType = NpgsqlDbType.Text, Value = cultureInfoName }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetCurrencyIdByLiteral(string literal)
        {
            if (string.IsNullOrEmpty(literal))
            {
                throw new ArgumentNullException("literal", "The literal must not be empty.");
            }

            var query = $"SELECT \"Id\" " +
                $"FROM \"Currencies\" " +
                $"WHERE \"Currencies\".\"Literal\" = @Literal " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@Literal", NpgsqlDbType = NpgsqlDbType.Text, Value = literal }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetRandomCurrencyId()
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"Currencies\" " +
                $"ORDER BY random() " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>();

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public bool Save(Currency entity, bool checkOnUnique = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!checkOnUnique || !Contains(entity.CultureInfoName, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"Currencies\" (\"Id\", \"CultureInfoName\") VALUES (@Id, @CultureInfoName)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@CultureInfoName", NpgsqlDbType = NpgsqlDbType.Text, Value = entity.CultureInfoName }
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetCurrencyId(entity.CultureInfoName) : foundId;
            }

            return true;
        }
    }
}
