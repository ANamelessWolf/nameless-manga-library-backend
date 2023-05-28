using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nameless.MangaBI;

namespace Nameless.MangaBI.Services.Interfaces
{
    public interface IContextAccesible
    {
        public MangaBIContext Context { get; }
    }
}
