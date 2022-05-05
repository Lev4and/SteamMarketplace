using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class ImportItemHttpClientTests
    {
        private readonly HttpContext _httpContext;
        private readonly Services.Authorization _authorization;

        public ImportItemHttpClientTests(HttpContext httpContext, Services.Authorization authorization)
        {
            _httpContext = httpContext;
            _authorization = authorization;
        }

        [Fact]
        public async Task ImportAsync_WithParams_StressTest()
        {
            var stopwatch = new Stopwatch();

            _authorization.LoginByAdministrator();

            for (var i = 0; i < 1000000; i += 50)
            {
                var response = await _httpContext.CSMoney.Store.GetInventoryAsync(50, i);

                if (response.Items == null) break;
                else
                {
                    foreach(var item in response.Items)
                    {
                        stopwatch.Restart();

                        var responce = await _httpContext.ResourceAPI.ImportItem.ImportAsync(item);

                        stopwatch.Stop();
                    }
                }
            }
        }
    }
}
