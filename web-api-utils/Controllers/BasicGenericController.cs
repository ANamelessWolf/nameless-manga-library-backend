using AutoMapper;
using AutoMapper.Internal;
using Nameless.WebApi.Models;
using Nameless.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static Nameless.WebApi.Utils.ControllerUtils;

namespace Nameless.WebApi.Controllers
{
    public class BasicGenericController<T, D> : Controller where T : class where D : BaseDto
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IMapper _mapper;

        public BasicGenericController(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all items, can be sorted by fields
        /// </summary>
        /// <param name="orderField">The field name used to sort the fields</param>
        /// <param name="orderType">The order type, ASC or DSC</param>
        /// <returns>A list of the current objects</returns>
        /// <response code="200">The list of items</response>
        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] string? orderField, [FromQuery] OrderType? orderType)
        {
            try
            {
                IEnumerable<T> result = await _repository.GetAll();
                var sort = typeof(T).GetOrderRequest(orderField, orderType);
                result = await result.OrderBy<T>(sort.orderField, sort.orderType);
                return Map(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get a catalogue item by id
        /// </summary>
        /// <param name="id">The item id</param>
        /// <returns>The catalogue item</returns>
        /// <response code="200">The catalogue item</response>
        [HttpGet("{id}", Name = "[controller]/GetById")]
        public async Task<ActionResult<D>> GetById(int id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null)
                    return NotFound();
                return Map(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Search for a catalogue item by a search query
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns>A list of the current objects</returns>
        /// <response code="200">The list of items</response>
        [HttpGet]
        [Route("[action]/")]
        public virtual async Task<IActionResult> Search(string? query, string? orderField)
        {
            try
            {
                IEnumerable<T> result = await this.Filter(query);
                var sort = typeof(T).GetOrderRequest(orderField, OrderType.Asc);
                result = await result.OrderBy<T>(sort.orderField, sort.orderType);
                return Map(result);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets the items filtering, by query and limiting its results by a pageSize, and the given index
        /// </summary>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="query">The filtering query</param>
        /// <param name="sortAsc">True if values are sort ascending</param>
        /// <returns>A list of the current objects</returns>
        /// <response code="200">The list of items</response>
        [HttpGet]
        [Route("[action]/")]
        public async Task<IActionResult> GetByPage([FromQuery] int pageIndex, [FromQuery] int pageSize, string? query, Boolean sortAsc = true)
        {
            try
            {
                IEnumerable<T> result = await this.Filter(query);
                result = await result.OrderBy<T>("Name", OrderType.Asc);
                result = result.Limit(pageIndex, pageSize);
                return MapPageResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Filter the catalogue by a query
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns>The list of selected items</returns>
        protected async Task<IEnumerable<T>> Filter(string? query)
        {
            if (query != null && String.IsNullOrEmpty(query))
            {
                String filter = query.ToFilterString<T>();
                return await _repository.Search(filter);
            }
            return await _repository.GetAll();
        }

        /// <summary>
        /// Maps the selected result to the DTO Model
        /// </summary>
        /// <param name="result">The mapped result</param>
        /// <returns>The list of mapped selected objects</returns>
        protected virtual OkObjectResult Map(T result)
        {
            return Ok(_mapper.Map<D>(result));
        }

        /// <summary>
        /// Maps the selected result to the DTO Model
        /// </summary>
        /// <param name="result">The mapped result</param>
        /// <returns>The list of mapped selected objects</returns>
        protected virtual OkObjectResult Map(IEnumerable<T> result)
        {
            return Ok(_mapper.Map<List<D>>(result));
        }

        /// <summary>
        /// Maps the selected result to the DTO Model
        /// </summary>
        /// <param name="result">The mapped result</param>
        /// <returns>The list of mapped selected objects</returns>
        protected virtual OkObjectResult MapPageResult(IEnumerable<T> result)
        {
            int totalRows = result.Count();
            PageResult<D> pageResult = new PageResult<D>()
            {
                TotalRows = totalRows,
                Items = _mapper.Map<List<D>>(result)
            };
            return Ok(pageResult);
        }

    }
}