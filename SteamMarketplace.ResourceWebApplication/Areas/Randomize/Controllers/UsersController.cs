﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Services.Randomizers;

namespace SteamMarketplace.ResourceWebApplication.Areas.Randomize.Controllers
{
    [Authorize]
    [ApiController]
    [Area("Randomize")]
    [EnableCors("CorsPolicy")]
    [Route("api/randomize/users")]
    public class UsersController : Controller
    {
        private readonly UserRandomizer _userRandomizer;

        public UsersController(UserRandomizer userRandomizer)
        {
            _userRandomizer = userRandomizer;
        }

        [HttpGet]
        [Route("create")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 200)]
        public async Task<IActionResult> Create()
        {
            await _userRandomizer.CreateUserAsync();

            return Ok(new BaseResponseModel<object?>(null, Statuses.Success));
        }
    }
}
