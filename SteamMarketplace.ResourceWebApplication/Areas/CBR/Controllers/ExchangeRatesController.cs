using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.HttpClients.CBR.ResponseModels;
using SteamMarketplace.Model.Common;

namespace SteamMarketplace.ResourceWebApplication.Areas.CBR.Controllers
{
    [Authorize]
    [Area("CBR")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/cBR/exchangeRates")]
    public class ExchangeRatesController : Controller
    {
        private readonly HttpClients.HttpContext _httpContext;

        public ExchangeRatesController(HttpClients.HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("latest")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 401)]
        [ProducesResponseType(typeof(BaseResponseModel<LatestExchangeRate>), 200)]
        public async Task<IActionResult> GetLatest()
        {
            return Ok(new BaseResponseModel<LatestExchangeRate>(await _httpContext.CBR.Latest.GetLatestExchangeRateAsync(), 
                Statuses.Success));
        }
    }
}
