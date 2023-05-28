using Nameless.MangaBI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.MangaBI.Services.Interfaces
{
    public interface IExecuteSpService<T> where T : class
    {
        Task<IEnumerable<T>> ExecuteStoredProcedure(RequestStoredProcedure request);
    }
}
