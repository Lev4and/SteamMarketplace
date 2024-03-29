﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Extensions;

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
        [ProducesResponseType(typeof(PagedResponseModel<Sale>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
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

        [HttpGet]
        [Route("mySales/active/count")]
        [ProducesResponseType(typeof(BaseResponseModel<int>), 200)]
        public IActionResult GetCountActiveSales()
        {
            return Ok(new BaseResponseModel<int>(_dataManager.Sales.GetCountActiveSales(Guid.Parse(User.Claims.GetValue("id"))), 
                Statuses.Success));
        }

        [HttpPost]
        [Route("item")]
        [ProducesResponseType(typeof(PagedResponseModel<Sale>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
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
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        [ProducesResponseType(typeof(BaseResponseModel<List<PricesDynamic>>), 200)]
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
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        [ProducesResponseType(typeof(BaseResponseModel<List<ExposedSalesDynamic>>), 200)]
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
