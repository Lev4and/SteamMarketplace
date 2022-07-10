using Microsoft.Data.SqlClient;
using Npgsql;
using NpgsqlTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;
using System.Data;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.AdoNet
{
    public class AdoNetExchangeRatesRepository : IExchangeRatesRepository
    {
        private readonly SteamMarketplaceDbContext _context;

        public AdoNetExchangeRatesRepository(SteamMarketplaceDbContext context)
        {
            _context = context;
        }

        public bool Contains(DateTime dateTime, Guid currencyId, out Guid id)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"ExchangeRates\" " +
                $"WHERE \"ExchangeRates\".\"DateTime\" = @DateTime AND \"ExchangeRates\".\"CurrencyId\" = @CurrencyId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@DateTime", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = dateTime },
                new NpgsqlParameter() { ParameterName = "@CurrencyId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = currencyId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetExchangeRateId(DateTime dateTime, Guid currencyId)
        {
            var query = $"SELECT \"Id\" " +
                $"FROM \"ExchangeRates\" " +
                $"WHERE \"ExchangeRates\".\"DateTime\" = @DateTime AND \"ExchangeRates\".\"CurrencyId\" = @CurrencyId " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@DateTime", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = dateTime },
                new NpgsqlParameter() { ParameterName = "@CurrencyId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = currencyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public decimal GetRateCurrency(Guid currencyId)
        {
            var query = $"SELECT \"Rate\" " +
                $"FROM \"ExchangeRates\" " +
                $"WHERE \"ExchangeRates\".\"CurrencyId\" = @CurrencyId " +
                $"ORDER BY \"ExchangeRates\".\"DateTime\" DESC " +
                $"LIMIT 1";

            var parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter() { ParameterName = "@CurrencyId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = currencyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<decimal>("Rate");
        }

        public bool Save(ExchangeRate entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The entity must not be empty.");
            }

            var foundId = Guid.Empty;

            if (!Contains(entity.DateTime, entity.CurrencyId, out foundId))
            {
                entity.Id = Guid.NewGuid();

                var query = $"INSERT INTO \"ExchangeRates\" (\"Id\", \"CurrencyId\", \"Rate\", \"DateTime\") VALUES (@Id, @CurrencyId, @Rate, @DateTime)";

                var parameters = new List<NpgsqlParameter>()
                {
                    new NpgsqlParameter() { ParameterName = "@Id", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.Id },
                    new NpgsqlParameter() { ParameterName = "@CurrencyId", NpgsqlDbType = NpgsqlDbType.Uuid, Value = entity.CurrencyId },
                    new NpgsqlParameter() { ParameterName = "@Rate", NpgsqlDbType = NpgsqlDbType.Numeric, Value = entity.Rate },
                    new NpgsqlParameter() { ParameterName = "@DateTime", NpgsqlDbType = NpgsqlDbType.TimestampTz, Value = entity.DateTime },
                };

                _context.ExecuteQuery(query, parameters);
            }
            else
            {
                entity.Id = Guid.Empty == foundId ? GetExchangeRateId(entity.DateTime, entity.CurrencyId) : foundId;
            }

            return true;
        }
    }
}
