using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.HttpClients.CBR.ResponseModels;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Importers.HighPerformance;

namespace SteamMarketplace.ResourceWebApplication.Areas.Import.Controllers
{
    [Authorize]
    [ApiController]
    [Area("Import")]
    [EnableCors("CorsPolicy")]
    [Route("api/import/exchangeRate")]
    public class ExchangeRateController : Controller
    {
        private readonly ILogger<ExchangeRateController> _logger;
        private readonly ExchangeRateImporter _importer;

        public ExchangeRateController(ILogger<ExchangeRateController> logger, ExchangeRateImporter importer)
        {
            _logger = logger;
            _importer = importer;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("import")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 401)]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        public IActionResult Import([FromBody] LatestExchangeRate latestExchangeRate)
        {
            if (latestExchangeRate == null)
            {
                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            return Ok(new BaseResponseModel<bool>(_importer.Import(latestExchangeRate.Timestamp, latestExchangeRate.Rates), 
                Statuses.Success));
        }
    }
}
