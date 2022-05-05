using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class CSMoneyStoreHttpClientTests
    {
        private readonly HttpContext _httpContext;
        private readonly Services.Authorization _authorization;

        public CSMoneyStoreHttpClientTests(HttpContext httpContext, Services.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;
        }

        [Fact]
        public async Task GetInventoryAsync_WithParams_ReturnNotBeNullOrEmptyResponse()
        {
            _authorization.LoginByAdministrator();

            var response = await _httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, 0);

            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetInventoryAsync_WithParams_StressTest()
        {
            _authorization.LoginByAdministrator();

            for (var i = 0; i < 1000000; i += 50)
            {
                var response = await _httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, i);

                if (response?.Result?.Items == null) break;
            }
        }
    }
}
