using System;
using System.Threading.Tasks;
using Xunit;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class ImportExchangeRateHttpClientTests
    {
        private readonly HttpContext _httpContext;
        private readonly Services.Authorization _authorization;

        public ImportExchangeRateHttpClientTests(HttpContext httpContext, Services.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;
        }

        [Fact]
        public async Task ImportAsync_WithParams_ReturnNotBeNullOrEmptyResponse()
        {
            _authorization.LoginByAdministrator();

            var date = new DateTime(2022, 1, 1);

            while (date <= DateTime.Now.Date)
            {
                var exchangeRateResponse = await _httpContext.CBR.Daily.GetDailyExchangeRateAsync(date);

                if (exchangeRateResponse != null && exchangeRateResponse.Valute != null)
                {
                    await _httpContext.ResourceAPI.ImportExchangeRate.ImportAsync(exchangeRateResponse);
                }

                date = date.AddDays(1);
            }
        }
    }
}
