using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.WebApi.Models
{
    public class PageResult<D> where D : class
    {
        public int totalRows { get; set; }
        public IEnumerable<D> pageRows { get; set; }
    }
}

