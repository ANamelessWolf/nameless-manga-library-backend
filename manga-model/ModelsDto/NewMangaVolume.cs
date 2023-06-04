using Nameless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Manga.ModelsDto
{
    public class NewMangaVolume 
    {
        /// <summary>
        /// Manga id
        /// </summary>
        public int MangaId { get; set; }
        /// <summary>
        /// Manga volume index
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// Publisher id
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// Manga volume price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Manga currency id
        /// </summary>
        public int CurrencyId { get; set; }
        /// <summary>
        /// Manga language id
        /// </summary>
        public int LanguageId { get; set; }
    }
}
