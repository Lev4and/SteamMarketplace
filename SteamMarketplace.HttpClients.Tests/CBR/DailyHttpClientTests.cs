using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SteamMarketplace.HttpClients.Tests.CBR
{
    public class DailyHttpClientTests
    {
        private readonly HttpContext _httpContext;

        public DailyHttpClientTests(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [Fact]
        public async Task GetDailyExchangeRateAsync_WithParams_ReturnNotBeNullOrEmptyResponse()
        {
            var response = await _httpContext.CBR.Daily.GetDailyExchangeRateAsync(DateTime.Now);

            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetDailyExchangeRateAsync_WithParams_StressTest()
        {
            var date = new DateTime(2022, 1, 1);

            while (date <= DateTime.Now.Date)
            {
                var response = await _httpContext.CBR.Daily.GetDailyExchangeRateAsync(date);

                date = date.AddDays(1);
            }
        }
    }
}
