using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nameless.WebApi.Models;
using Nameless.WebApi.Repositories;
using static Nameless.WebApi.Utils.ControllerUtils;

namespace Nameless.WebApi.Controllers
{
    /// <summary>
    /// Basic select controller for tables that are catalogues. 
    /// Tables must had an Id, and a Name field
    /// </summary>
    /// <typeparam name="T">The Model Type</typeparam>
    /// <typeparam name="D">The DTO Model Type</typeparam>
    public class CatalogueController<T, D> : Controller where T : class where D : BaseDto
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IMapper _mapper;

        /// <summary>
        /// Initialize a new instance for a catalogue controller
        /// </summary>
        /// <param name="repository">The database repository</param>
        /// <param name="mapper">The model mapper</param>
        public CatalogueController(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all instances of catalogue
        /// </summary>
        /// <returns>A list of the current objects</returns>
        /// <response code="200">The list of items</response>
        [HttpGet]
        [Route("[action]/")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();
            result = await result.OrderBy<T>("Name", OrderType.Asc);
            return Ok(result);
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
                return Ok(result);
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
        public async Task<IActionResult> Search(string? query)
        {
            try
            {
                IEnumerable<T> result = await this.Filter(query);
                result = await result.OrderBy<T>("Name", OrderType.Asc);
                return Ok(result);
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
                int totalRows = result.Count();
                return Ok(new Tuple<int, IEnumerable<T>>(totalRows, result));
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
        private async Task<IEnumerable<T>> Filter(string? query)
        {
            if (query != null && String.IsNullOrEmpty(query))
            {
                String filter = query.ToFilterString<T>();
                return await _repository.Search(filter);
            }
            return await _repository.GetAll();
        }
    }
}