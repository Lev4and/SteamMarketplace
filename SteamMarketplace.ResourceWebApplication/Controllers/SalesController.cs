using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/sales")]
    [EnableCors("CorsPolicy")]
    public class SalesController : Controller
    {
        private readonly ILogger<SalesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public SalesController(DefaultDataManager dataManager, ILogger<SalesController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpPost]
        [Route("mySales")]
        public async Task<IActionResult> GetMySales([FromBody] SalesFilters filters)
        {
            if (filters == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            var count = _dataManager.Sales.GetCountSales(filters.UserId);
            var result = await _dataManager.Sales.GetSales(filters).ToListAsync();

            return Ok(new PagedResponseModel<Sale>(result, filters.Pagination.Page,
                filters.Pagination.Limit, count, Statuses.Success));
        }

        [HttpPost]
        [Route("item")]
        public async Task<IActionResult> GetSalesItem([FromBody] SalesItemFilters filters)
        {
            if (filters == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            var count = _dataManager.Sales.GetCountSalesItem(filters.FullName);
            var result = await _dataManager.Sales.GetSalesItem(filters).ToListAsync();

            return Ok(new PagedResponseModel<Sale>(result, filters.Pagination.Page,
                filters.Pagination.Limit, count, Statuses.Success));
        }

        [HttpGet]
        [Route("pricesDynamics")]
        public async Task<IActionResult> GetPricesDynamicsItem([FromQuery(Name = "name")] string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                _logger.LogWarning($"Validation failed. Invalid fullName param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            var result = await _dataManager.Sales.GetPricesDynamicsItem(fullName).ToListAsync();

            return Ok(new BaseResponseModel<List<PricesDynamic>>(result, Statuses.Success));
        }

        [HttpGet]
        [Route("exposedSalesDynamics")]
        public async Task<IActionResult> GetExposedSalesDynamicsItem([FromQuery(Name = "name")] string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                _logger.LogWarning($"Validation failed. Invalid fullName param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            var result = await _dataManager.Sales.GetExposedSalesDynamicsItem(fullName).ToListAsync();

            return Ok(new BaseResponseModel<List<ExposedSalesDynamic>>(result, Statuses.Success));
        }
    }
}
