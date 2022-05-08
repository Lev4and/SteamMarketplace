using FluentAssertions;
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

            var exchangeRateResponse = await _httpContext.ResourceAPI.CBRExchangeRates.GetLatestExchangeRateAsync();

            if (exchangeRateResponse.Result != null)
            {
                await _httpContext.ResourceAPI.ImportExchangeRate.ImportAsync(exchangeRateResponse.Result);
            }
        }
    }
}
