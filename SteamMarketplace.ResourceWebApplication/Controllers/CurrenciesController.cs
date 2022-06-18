using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/currencies")]
    [EnableCors("CorsPolicy")]
    public class CurrenciesController : CRUDController<Currency, CurrenciesFilters>
    {
        private readonly ILogger<CurrenciesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public CurrenciesController(DefaultDataManager dataManager, ILogger<CurrenciesController> logger)
            : base(dataManager.Currencies, dataManager.Currencies)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponseModel<List<Currency>>), 200)]
        public async Task<IActionResult> GetAllCurrencies()
        {
            return Ok(new BaseResponseModel<List<Currency>>(await _dataManager.Currencies.GetAllCurrencies()
                .ToListAsync(), Statuses.Success));
        }
    }
}
