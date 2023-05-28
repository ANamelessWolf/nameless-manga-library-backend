using Nameless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Manga.ModelsDto
{
    public class CatalogueDto : BaseDto
    {
        /// <summary>
        /// Catalogue item value
        /// </summary>
        public string? Name { get; set; }
    }
}
