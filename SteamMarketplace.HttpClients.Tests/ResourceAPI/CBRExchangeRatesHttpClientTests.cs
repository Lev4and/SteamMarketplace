using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class CBRExchangeRatesHttpClientTests
    {
        private readonly HttpContext _httpContext;
        private readonly Services.Authorization _authorization;

        public CBRExchangeRatesHttpClientTests(HttpContext httpContext,
            Services.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;
        }

        [Fact]
        public async Task GetLatestExchangeRateAsync_WithoutParams_ReturnNotBeNullOrEmptyResponse()
        {
            _authorization.LoginByAdministrator();

            var response = await _httpContext.ResourceAPI.CBRExchangeRates.GetLatestExchangeRateAsync();

            response.Should().NotBeNull();
        }
    }
}
