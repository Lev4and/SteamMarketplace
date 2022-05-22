using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/exchangeRates")]
    [EnableCors("CorsPolicy")]
    public class ExchangeRatesController : Controller
    {
        private readonly ILogger<ExchangeRatesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public ExchangeRatesController(DefaultDataManager dataManager, ILogger<ExchangeRatesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{currencyId}")]
        public async Task<IActionResult> GetExchangeRates([Required][FromRoute(Name = "currencyId")] Guid currencyId)
        {
            return Ok(new BaseResponseModel<List<ExchangeRate>>(await _dataManager.ExchangeRates
                .GetExchangeRatesByCurrencyId(currencyId).ToListAsync(), Statuses.Success));
        }
    }
}
