using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.CBR
{
    public class LatestHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public LatestHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task GetLatestExchangeRateAsync_WithoutParams_ReturnNotBeNullOrEmptyResponse()
        {
            var response = await _httpContext.CBR.Latest.GetLatestExchangeRateAsync();

            response.Should().NotBeNull();
        }
    }
}
