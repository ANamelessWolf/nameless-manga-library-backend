﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nameless.WebApi.Repositories;
using Nameless.WebApi.Models;
using AutoMapper.Internal;

namespace Nameless.WebApi.Controllers
{
    public class GenericController<T, D, I> : BasicGenericController<T, D> where T : DbModel where D : BaseDto where I : class
    {

        public GenericController(IGenericRepository<T> repository, IMapper mapper) :
            base(repository, mapper)
        {

        }

        /// <summary>
        /// Método para eliminar una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <returns>No regresa información</returns>
        /// <response code="204">No regresa información</response>
        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Método para agregar una entidad
        /// </summary>
        /// <param name="entity">Objeto con la información de la entidad</param>
        /// <returns>Regresa un objeto con los datos de la entidad agregada</returns>
        /// <response code="201">Regresa un objeto con los datos de la entidad agregada</response>
        //[Authorize]
        [HttpPost]
        public ActionResult<T> Insert(I entity)
        {
            var result = _repository.Insert(_mapper.Map<T>(entity)).Result;
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        /// <summary>
        /// Método para modificar los datos de una entidad
        /// </summary>
        /// <param name="entity">Objeto con la información de la entidad</param>
        /// <returns>Regresa un objeto con los datos de la entidad modificada</returns>
        /// <response code="200">Regresa un objeto con los datos de la entidad modificada</response>
        //[Authorize]
        [HttpPut]
        public async Task<D> Update(D entity)
        {
            var result = await _repository.Update(_mapper.Map<T>(entity));
            return _mapper.Map<D>(result);
        }
    }
}