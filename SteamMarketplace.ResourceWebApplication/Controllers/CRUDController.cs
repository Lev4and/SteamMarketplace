using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;
using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.ResourceWebApplication.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(Roles = "Администратор")]
    public class CRUDController<T, F> : ControllerBase where T : BaseEntity where F : Filters
    {
        private readonly ICRUDRepository<T> _cRUDRepository;
        private readonly IFilterableRepository<T, F> _filterableRepository;

        public CRUDController(ICRUDRepository<T> cRUDRepository, IFilterableRepository<T, F> filterableRepository)
        {
            _cRUDRepository = cRUDRepository;
            _filterableRepository = filterableRepository;
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 400)]
        public IActionResult Add([Required][FromBody] T entity)
        {
            try
            {
                if (!_cRUDRepository.Contains(entity))
                {
                    _cRUDRepository.Add(entity);

                    return Ok(new BaseResponseModel<bool>(true, Statuses.Success));
                }
                else
                {
                    return BadRequest(new BaseResponseModel<bool>(false, Statuses.ContainsData));
                }
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new BaseResponseModel<bool>(false, Statuses.InvalidData));
            }
            catch (ArgumentException)
            {
                return BadRequest(new BaseResponseModel<bool>(false, Statuses.InvalidData));
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 400)]
        public IActionResult Update([Required][FromBody] T entity)
        {
            try
            {
                var actualEntity = _cRUDRepository.GetById(entity.Id);

                if (actualEntity.Equals(entity) || (!actualEntity.Equals(entity) && !_cRUDRepository.Contains(entity)))
                {
                    _cRUDRepository.Update(entity);

                    return Ok(new BaseResponseModel<bool>(true, Statuses.Success));
                }
                else
                {
                    return BadRequest(new BaseResponseModel<bool>(false, Statuses.ContainsData));
                }
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new BaseResponseModel<bool>(false, Statuses.InvalidData));
            }
            catch (ArgumentException)
            {
                return BadRequest(new BaseResponseModel<bool>(false, Statuses.InvalidData));
            }
        }

        [HttpPost]
        [Route("allByFilters")]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 400)]
        public async Task<IActionResult> GetAllByFilters([Required][FromBody] F filters)
        {
            if (filters == null)
            {
                return BadRequest(new BaseResponseModel<bool>(false, Statuses.InvalidData));
            }

            var count = _filterableRepository.GetCount(filters);
            var result = await _filterableRepository.GetAllByFilters(filters).ToListAsync();

            return Ok(new PagedResponseModel<T>(result, filters.Pagination.Page,
                filters.Pagination.Limit, count, Statuses.Success));
        }

        [HttpDelete]
        [Route("{id}/delete")]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponseModel<bool>), 400)]
        public IActionResult Delete([Required][FromRoute(Name = "id")] Guid id)
        {
            try
            {
                _cRUDRepository.DeleteById(id);

                return Ok(new BaseResponseModel<bool>(true, Statuses.Success));
            }
            catch (ArgumentException)
            {
                return BadRequest(new BaseResponseModel<bool>(false, Statuses.InvalidData));
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BaseResponseModel<object?>), 400)]
        public IActionResult Get([Required][FromRoute(Name = "id")] Guid id)
        {
            try
            {
                return Ok(new BaseResponseModel<T>(_cRUDRepository.GetById(id), Statuses.Success));
            }
            catch (ArgumentException)
            {
                return BadRequest(new BaseResponseModel<object?>(null, Statuses.InvalidData));
            }
        }
    }
}
