﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Extensions;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DefaultDataManager _dataManager;

        public UsersController(DefaultDataManager dataManager, ILogger<UsersController> logger)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("currentUser")]
        [ProducesResponseType(typeof(BaseResponseModel<ApplicationUser>), 200)]
        public IActionResult GetCurrentUser()
        {
            return Ok(new BaseResponseModel<ApplicationUser>(_dataManager.Users.GetUserById(Guid.Parse(User.Claims.GetValue("id"))), Statuses.Success));
        }
    }
}
