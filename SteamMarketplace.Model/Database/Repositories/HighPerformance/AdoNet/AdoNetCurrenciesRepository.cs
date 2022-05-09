using Microsoft.Data.SqlClient;
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

            var query = $"SELECT TOP(1) Id " +
                $"FROM Currencies " +
                $"WHERE Currencies.CultureInfoName = @CultureInfoName";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@CultureInfoName", SqlDbType = SqlDbType.NVarChar, Value = cultureInfoName }
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

            var query = $"SELECT TOP(1) Id " +
                $"FROM Currencies " +
                $"WHERE Currencies.CultureInfoName = @CultureInfoName";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@CultureInfoName", SqlDbType = SqlDbType.NVarChar, Value = cultureInfoName }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetCurrencyIdByLiteral(string literal)
        {
            if (string.IsNullOrEmpty(literal))
            {
                throw new ArgumentNullException("literal", "The literal must not be empty.");
            }

            var query = $"SELECT TOP(1) Id " +
                $"FROM Currencies " +
                $"WHERE Currencies.Literal = @Literal";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Literal", SqlDbType = SqlDbType.NVarChar, Value = literal }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public Guid GetRandomCurrencyId()
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM Currencies " +
                $"ORDER BY NEWID()";

            var parameters = new List<SqlParameter>();

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

                var query = $"INSERT INTO [Currencies] (Id, CultureInfoName) VALUES (@Id, @CultureInfoName)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@CultureInfoName", SqlDbType = SqlDbType.NVarChar, Value = entity.CultureInfoName }
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
