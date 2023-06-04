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
    public class CatalogueController<T, D> :
        BasicGenericController<T, D> where T : class where D : BaseDto
    {
        /// <summary>
        /// Initialize a new instance for a catalogue controller
        /// </summary>
        /// <param name="repository">The database repository</param>
        /// <param name="mapper">The model mapper</param>
        public CatalogueController(IGenericRepository<T> repository, IMapper mapper)
            : base(repository, mapper)
        {

        }

        /// <summary>
        /// Get all items, can be sorted by fields
        /// </summary>
        /// <param name="orderField">The field name used to sort the fields</param>
        /// <param name="orderType">The order type, ASC or DSC</param>
        /// <returns>A list of the current objects</returns>
        /// <response code="200">The list of items</response>
        [HttpGet]
        public override async Task<IActionResult> GetAll([FromQuery] string? orderField = "Name", [FromQuery] OrderType? orderType = OrderType.Asc)
        {
            return await base.GetAll(orderField, orderType);
        }

        /// <summary>
        /// Search for a catalogue item by a search query
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns>A list of the current objects</returns>
        /// <response code="200">The list of items</response>
        [HttpGet]
        [Route("[action]/")]
        public override async Task<IActionResult> Search(string? query, string? orderField = "Name")
        {
            return await base.Search(query, orderField);
        }

        /// <summary>
        /// Maps the selected result to the DTO Model
        /// </summary>
        /// <param name="result">The mapped result</param>
        /// <returns>The list of mapped selected objects</returns>
        protected override OkObjectResult Map(IEnumerable<T> result)
        {
            return Ok(result);
        }
        /// <summary>
        /// Maps the selected result to the DTO Model
        /// </summary>
        /// <param name="result">The mapped result</param>
        /// <returns>The list of mapped selected objects</returns>
        protected override OkObjectResult MapPageResult(IEnumerable<T> result)
        {
            int totalRows = result.Count();
            PageResult<T> pageResult = new PageResult<T>()
            {
                TotalRows = totalRows,
                Items = result
            };
            return Ok(pageResult);
        }
        /// <summary>
        /// Maps the selected result to the DTO Model
        /// </summary>
        /// <param name="result">The mapped result</param>
        /// <returns>The list of mapped selected objects</returns>
        protected override OkObjectResult Map(T result)
        {
            return Ok(result);
        }
    }
}