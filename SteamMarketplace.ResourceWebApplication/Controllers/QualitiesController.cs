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
    [Route("api/qualities")]
    [EnableCors("CorsPolicy")]
    public class QualitiesController : CRUDController<Quality, QualitiesFilters>
    {
        private readonly ILogger<QualitiesController> _logger;
        private readonly DefaultDataManager _dataManager;

        public QualitiesController(DefaultDataManager dataManager, ILogger<QualitiesController> logger)
            : base(dataManager.Qualities, dataManager.Qualities)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponseModel<List<Quality>>), 200)]
        public async Task<IActionResult> GetAllQualities()
        {
            return Ok(new BaseResponseModel<List<Quality>>(await _dataManager.Qualities.GetAllQualities()
                .ToListAsync(), Statuses.Success));
        }
    }
}
