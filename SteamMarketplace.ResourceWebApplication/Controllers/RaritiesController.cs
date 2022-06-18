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
    [Route("api/rarities")]
    [EnableCors("CorsPolicy")]
    public class RaritiesController : CRUDController<Rarity, RaritiesFilters>
    {
        private readonly ILogger<RaritiesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public RaritiesController(DefaultDataManager dataManager, ILogger<RaritiesController> logger)
            : base(dataManager.Rarities, dataManager.Rarities)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponseModel<List<Rarity>>), 200)]
        public async Task<IActionResult> GetAllRarities()
        {
            return Ok(new BaseResponseModel<List<Rarity>>(await _dataManager.Rarities.GetAllRarities()
                .ToListAsync(), Statuses.Success));
        }
    }
}
