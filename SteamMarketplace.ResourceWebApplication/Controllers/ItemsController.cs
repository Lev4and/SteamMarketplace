using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/items")]
    [EnableCors("CorsPolicy")]
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly DefaultDataManager _dataManager;

        public ItemsController(DefaultDataManager dataManager, ILogger<ItemsController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("searchSuggestions")]
        public async Task<IActionResult> GetSearchSuggestions([FromQuery(Name = "q")] string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return BadRequest(new BaseResponseModel<List<string>>(new List<string>(), Statuses.InvalidData));
            }

            return Ok(new BaseResponseModel<List<string>>(await _dataManager.Items.GetSearchSuggestions(searchString)
                .ToListAsync(), Statuses.Success));
        }

        [HttpGet]
        [Route("{fullName}")]
        public IActionResult GetItemByFullName([Required][FromRoute(Name = "fullName")] string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            return Ok(new BaseResponseModel<Item>(_dataManager.Items.GetItemByFullName(fullName), Statuses.Success));
        }

        [HttpGet]
        [Route("{fullName}/extendedInfo")]
        public IActionResult GetExtendedInfoItemByFullName([Required][FromRoute(Name = "fullName")] string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            return Ok(new BaseResponseModel<ExtendedItem>(new ExtendedItem()
            {
                Count = _dataManager.Items.GetCountItems(fullName),
                CountOwners = _dataManager.Items.GetCountOwnersItems(fullName),
                Rarity = _dataManager.Items.GetRarityItem(fullName),
                AverageFloat = _dataManager.Items.GetAverageFloatItem(fullName),
                AddedAt = _dataManager.Items.GetMinAddedAtItem(fullName),
                Item = _dataManager.Items.GetItemByFullName(fullName)
            }, Statuses.Success));
        }

        [HttpPost]
        [Route("groupedItems")]
        public async Task<IActionResult> GetGroupedItems([FromBody] ItemsFilters filters)
        {
            if (filters == null)
            {
                _logger.LogWarning($"Validation failed. Invalid filters param.");

                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }

            var count = _dataManager.Items.GetCountGroupedItems(filters);
            var result = await _dataManager.Items.GetGroupedItems(filters).ToListAsync();

            return Ok(new PagedResponseModel<GroupedItem>(result, filters.Pagination.Page,
                filters.Pagination.Limit, count, Statuses.Success));
        }
    }
}
