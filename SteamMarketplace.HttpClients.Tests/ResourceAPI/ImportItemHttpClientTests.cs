using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.ResourceAPI
{
    public class ImportItemHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public ImportItemHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task ImportAsync_WithParams_StressTest()
        {
            var stopwatch = new Stopwatch();

            _httpContext.ResourceAPI.ImportItem.Login("Admin", "Admin");

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
