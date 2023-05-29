using AutoMapper;
using AutoMapper.Internal;
using Nameless.WebApi.Models;
using Nameless.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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
        /// Método para obtener la información de todas las entidades
        /// </summary>
        /// <returns>Regresa un listado de objetos con los datos de cada entidad</returns>
        /// <response code="200">Regresa un listado de objetos con los datos de cada entidad</response>
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();

            return Ok(_mapper.Map<List<D>>(result));
        }

        /// <summary>
        /// Método para obtener la información de una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <returns>Regresa un objeto con los datos de la entidad</returns>
        /// <response code="200">Regresa la información asociada a una entidad</response>
        //[Authorize]
        [HttpGet("{id}", Name = "[controller]/GetById")]
        public async Task<ActionResult<D>> GetById(int id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null)
                    return NotFound();
                return Ok(_mapper.Map<D>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Método para obtener la información de las entidades que cubran con el filtro especificado
        /// </summary>
        /// <returns>Regresa un listado de objetos con los datos de cada entidad que cubran con el filtro especificado</returns>
        /// <response code="200">Regresa un listado de objetos con los datos de cada entidad que cubran con el filtro especificado</response>
        //[Authorize]
        [HttpGet]
        [Route("[action]/")]
        public async Task<IActionResult> Search([FromQuery] D entity)
        {
            try
            {
                String filter;
                filter = ChangeModelToFilterString(_mapper.Map<T>(entity));
                var result = String.IsNullOrEmpty(filter) ? await _repository.GetAll() : await _repository.Filter(filter);
                return Ok(_mapper.Map<List<D>>(result));
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

        protected string ChangeModelToFilterString(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            var typeEntity = typeof(T);
            String strAnd = String.Empty;
            String strCondition = String.Empty;
            StringBuilder filter = new StringBuilder();

            foreach (PropertyInfo property in typeEntity.GetProperties())
            {
                int caso = 0;
                var value = typeEntity.GetProperty(property.Name).GetValue(entity, null);
                var defaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : String.Empty;
                if (value != null && value.ToString() != defaultValue.ToString())
                {
                    if (property.PropertyType == typeof(string))
                    {
                        String? strValue = value.ToString();
                        if (strValue != null && strValue.StartsWith("*"))
                        {
                            caso += 1;
                            strValue = strValue.Substring(1);
                        }
                        if (strValue != null && strValue.EndsWith("*"))
                        {
                            caso += 2;
                            strValue = strValue.Substring(0, strValue.Length - 1);
                        }

                        value = String.Format("\"{0}\"", strValue);
                    }
                    switch (caso)
                    {
                        case 1:
                            strCondition = String.Format("{0} {1}.EndsWith({2}, System.StringComparison.OrdinalIgnoreCase)", strAnd, property.Name, value);
                            break;
                        case 2:
                            strCondition = String.Format("{0} {1}.StartsWith({2}, System.StringComparison.OrdinalIgnoreCase)", strAnd, property.Name, value);
                            break;
                        case 3:
                            strCondition = String.Format("{0} {1}.Contains({2}, System.StringComparison.OrdinalIgnoreCase)", strAnd, property.Name, value);
                            break;
                        default:
                            strCondition = String.Format("{0} {1} == {2}", strAnd, property.Name, value);
                            break;
                    }
                    filter.Append(strCondition);
                    strAnd = " &&";
                }
            }
            return filter.ToString();
        }


        /// <summary>
        /// Método para obtener la información de las entidades que cubran con el filtro especificado
        /// </summary>
        /// <returns>Regresa un listado de objetos con los datos de cada entidad que cubran con el filtro especificado</returns>
        /// <response code="200">Regresa un listado de objetos con los datos de cada entidad que cubran con el filtro especificado</response>
        //[Authorize]
        [HttpGet]
        [Route("[action]/{filter}")]
        public async Task<IActionResult> Filter(string filter)
        {
            try
            {
                var result = await _repository.Filter(FilterDtoToFilterContext(filter));
                return Ok(_mapper.Map<List<D>>(result));
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
        protected String FilterDtoToFilterContext(string filter)
        {
            PropertyMap map;
            StringBuilder sb = new StringBuilder();
            TypeMap typeMap = _mapper.ConfigurationProvider.Internal().FindTypeMapFor(typeof(T), typeof(D));
            if (typeMap == null)
                return filter;

            Regex rx = new Regex(@"(\w+|\"".*\"")");
            int index = 0;
            foreach (Match match in rx.Matches(filter, index))
            {
                map = typeMap.PropertyMaps.FirstOrDefault(p => p.DestinationName == match.Value);
                if (map != null)
                {
                    sb.Append(filter.Substring(index, match.Index - index) + map.SourceMember.Name);
                }
                else
                    sb.Append(filter.Substring(index, match.Index - index + match.Value.Length));
                index = match.Index + match.Length;
            }
            if (index < filter.Length)
                sb.Append(filter.Substring(index, filter.Length - (index)));

            return sb.ToString();
        }

        protected String OrderDtoToOrderContext(string orderField)
        {
            PropertyMap map;
            StringBuilder sb = new StringBuilder();
            TypeMap typeMap = _mapper.ConfigurationProvider.Internal().FindTypeMapFor(typeof(T), typeof(D));
            if (typeMap == null)
                return orderField;

            map = typeMap.PropertyMaps.FirstOrDefault(p => p.DestinationName.ToLower() == orderField.ToLower());
            if (map == null)
                throw new Exception("Field to order not exists");

            return map.SourceMember.Name;
        }


        /// <summary>
        /// Método para obtener la información de la entidad de forma paginada y filtrada (si el filtro se especifica)
        /// </summary>
        /// <returns>Regresa un listado de objetos, de la página especificada, con los datos de cada entidad</returns>
        /// <response code="200">Regresa un listado de objetos con los datos de cada entidad, de acuerdo a la página especificada</response>
        /// 
        //[Authorize]
        [HttpGet]
        [Route("[action]/")]
        public async Task<IActionResult> GetPage(int pageIndex, int pageSize, [FromQuery] D entity, [FromQuery] OrderRequest orderRequest = null)
        {
            try
            {

                String filter;
                OrderRequest order = null;
                filter = ChangeModelToFilterString(_mapper.Map<T>(entity));
                if (orderRequest != null && !string.IsNullOrEmpty(orderRequest.orderField))
                {
                    orderRequest.orderField = OrderDtoToOrderContext(orderRequest.orderField);
                    order = orderRequest;
                }
                var result = await _repository.GetPage(pageIndex, pageSize, filter, order);
                return Ok(new PageResult<D> { totalRows = result.Item1, pageRows = _mapper.Map<List<D>>(result.Item2) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}