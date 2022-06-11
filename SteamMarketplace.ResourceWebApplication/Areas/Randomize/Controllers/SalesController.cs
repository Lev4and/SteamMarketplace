using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Services.Randomizers;

namespace SteamMarketplace.ResourceWebApplication.Areas.Randomize.Controllers
{
    [Authorize]
    [ApiController]
    [Area("Randomize")]
    [EnableCors("CorsPolicy")]
    [Route("api/randomize/sales")]
    public class SalesController : Controller
    {
        private readonly SaleRandomizer _saleRandomizer;

        public SalesController(SaleRandomizer saleRandomizer)
        {
            _saleRandomizer = saleRandomizer;
        }

        [HttpGet]
        [Route("exposeItemsOnSale")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 401)]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        public async Task<IActionResult> ExposeItemsOnSale()
        {
            await _saleRandomizer.ExposeItemsOnSaleAsync();

            return Ok(new BaseResponseModel<object?>(null, Statuses.Success));
        }
    }
}
