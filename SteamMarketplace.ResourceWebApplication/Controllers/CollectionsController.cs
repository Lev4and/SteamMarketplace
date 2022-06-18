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
    [Route("api/collections")]
    [EnableCors("CorsPolicy")]
    public class CollectionsController : CRUDController<Collection, CollectionsFilters>
    {
        private readonly DefaultDataManager _dataManager;

        public CollectionsController(DefaultDataManager dataManager) 
            : base(dataManager.Collections, dataManager.Collections)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponseModel<List<Collection>>), 200)]
        public async Task<IActionResult> GetAllCollections()
        {
            return Ok(new BaseResponseModel<List<Collection>>(await _dataManager.Collections.GetAllCollections()
                .ToListAsync(), Statuses.Success));
        }
    }
}
