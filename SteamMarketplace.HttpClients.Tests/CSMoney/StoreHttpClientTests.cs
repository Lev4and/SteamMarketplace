using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.CSMoney
{
    public class StoreHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public StoreHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task GetInventoryAsync_WithParams_ReturnNotBeNullOrEmptyResponse()
        {
            var response = await _httpContext.CSMoney.Store.GetInventoryAsync(50, 0);

            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetInventoryAsync_WithParams_StressTest()
        {
            for(var i = 0; i < 1000000; i += 50)
            {
                var response = await _httpContext.CSMoney.Store.GetInventoryAsync(50, i);

                if (response.Items == null) break;
            }
        }
    }
}
