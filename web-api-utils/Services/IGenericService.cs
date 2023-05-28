using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.WebApi.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> Filter(string filter);
    }
}
