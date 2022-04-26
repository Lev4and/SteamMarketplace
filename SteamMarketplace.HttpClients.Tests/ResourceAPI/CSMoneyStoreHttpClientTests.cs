using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class CSMoneyStoreHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public CSMoneyStoreHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task GetInventoryAsync_WithParams_ReturnNotBeNullOrEmptyResponse()
        {
            var response = await _httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, 0);

            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetInventoryAsync_WithParams_StressTest()
        {
            _httpContext.ResourceAPI.CSMoneyStore.Login("Admin", "Admin");

            for (var i = 0; i < 1000000; i += 50)
            {
                var response = await _httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, i);

                if (response?.Result?.Items == null) break;
            }
        }
    }
}
