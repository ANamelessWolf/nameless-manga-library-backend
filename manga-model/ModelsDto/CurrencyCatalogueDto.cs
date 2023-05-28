using Nameless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Manga.ModelsDto
{
    public class CurrencyCatalogueDto : CatalogueDto
    {
        public decimal Value { get; set; }
        public string? Symbol { get; set; }
    }
}
