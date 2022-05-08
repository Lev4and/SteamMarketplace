using Microsoft.Data.SqlClient;
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
            var query = $"SELECT TOP(1) Id " +
                $"FROM ExchangeRates " +
                $"WHERE ExchangeRates.DateTime = @DateTime AND ExchangeRates.CurrencyId = @CurrencyId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@DateTime", SqlDbType = SqlDbType.DateTime2, Value = dateTime },
                new SqlParameter() { ParameterName = "@CurrencyId", SqlDbType = SqlDbType.UniqueIdentifier, Value = currencyId }
            };

            var result = _context.ExecuteQuery(query, parameters).Rows;

            id = result.Count > 0 ? result[0].Field<Guid>("Id") : Guid.Empty;

            return result.Count > 0;
        }

        public Guid GetExchangeRateId(DateTime dateTime, Guid currencyId)
        {
            var query = $"SELECT TOP(1) Id " +
                $"FROM ExchangeRates " +
                $"WHERE ExchangeRates.DateTime = @DateTime AND ExchangeRates.CurrencyId = @CurrencyId";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@DateTime", SqlDbType = SqlDbType.DateTime2, Value = dateTime },
                new SqlParameter() { ParameterName = "@CurrencyId", SqlDbType = SqlDbType.UniqueIdentifier, Value = currencyId }
            };

            return _context.ExecuteQuery(query, parameters).Rows[0].Field<Guid>("Id");
        }

        public decimal GetRateCurrency(Guid currencyId)
        {
            var query = $"SELECT TOP(1) Rate " +
                $"FROM ExchangeRates " +
                $"WHERE ExchangeRates.CurrencyId = @CurrencyId " +
                $"ORDER BY ExchangeRates.DateTime DESC";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@CurrencyId", SqlDbType = SqlDbType.UniqueIdentifier, Value = currencyId }
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

                var query = $"INSERT INTO [ExchangeRates] (Id, CurrencyId, Rate, DateTime) VALUES (@Id, @CurrencyId, @Rate, @DateTime)";

                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.Id },
                    new SqlParameter() { ParameterName = "@CurrencyId", SqlDbType = SqlDbType.UniqueIdentifier, Value = entity.CurrencyId },
                    new SqlParameter() { ParameterName = "@Rate", SqlDbType = SqlDbType.Decimal, Value = entity.Rate },
                    new SqlParameter() { ParameterName = "@DateTime", SqlDbType = SqlDbType.DateTime2, Value = entity.DateTime },
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
