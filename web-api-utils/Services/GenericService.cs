using Nameless.WebApi.Repositories;
using Nameless.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.WebApi.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;

        public GenericService(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task Delete(int id)
        {
            await _genericRepository.Delete(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _genericRepository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _genericRepository.GetById(id);
        }

        public async Task<T> Insert(T entity)
        {
            return await _genericRepository.Insert(entity);
        }

        public async Task<T> Update(T entity)
        {
            return await _genericRepository.Update(entity);
        }

        public async Task<IEnumerable<T>> Filter(string filter)
        {
            return await _genericRepository.Filter(filter);
        }
        public async Task<Tuple<int, IEnumerable<T>>> GetPage(int pageIndex, int pageSize, string filter = null)
        {
            return await _genericRepository.GetPage(pageIndex, pageSize, filter);
        }

    }
}