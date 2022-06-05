using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class ExchangeRateImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public ExchangeRateImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool Import(DateTime timestamp, Dictionary<string, decimal> rates)
        {
            if (rates == null)
            {
                throw new ArgumentNullException("rates", "The rates must not be empty.");
            }

            rates.Add("RUB", 1);

            foreach (var rate in rates)
            {
                var result = new Entities.ExchangeRate
                {
                    CurrencyId = _dataManager.Currencies.GetCurrencyIdByLiteral(rate.Key),
                    Rate = rate.Key != "USD" ? rate.Value / rates["USD"] : 1,
                    DateTime = timestamp
                };

                _dataManager.ExchangeRates.Save(result);
            }

            return true;
        }
    }
}
