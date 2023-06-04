using Nameless.WebApi.Models;

namespace Nameless.Manga.Models
{
    public class MangaLibrary : DbModel
    {
        /// <summary>
        /// Manga id
        /// </summary>
        public int MangaId { get; set; }
        /// <summary>
        /// Manga
        /// </summary>
        public MangaModel? Manga { get; set; }
        /// <summary>
        /// Manga volume index
        /// </summary>
        public int Volume { get; set; }
        /// <summary>
        /// Publisher id
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// Manga volume publisher
        /// </summary>
        public PublisherCatalogue? Publisher { get; set; }
        /// <summary>
        /// Manga volume price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Manga currency id
        /// </summary>
        public int CurrencyId { get; set; }
        /// <summary>
        /// Manga price currency
        /// </summary>
        public CurrencyCatalogue? Currency { get; set; }
        /// <summary>
        /// Manga language id
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// Manga volume writen language
        /// </summary>
        public LanguageCatalogue? Language { get; set; }
    }
}
