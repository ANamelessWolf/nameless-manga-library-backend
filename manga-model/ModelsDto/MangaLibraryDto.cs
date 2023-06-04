using Nameless.WebApi.Models;

namespace Nameless.Manga.ModelsDto
{
    public class MangaLibraryDto : BaseDto
    {
        /// <summary>
        /// Manga id
        /// </summary>
        public int MangaId { get; set; }
        /// <summary>
        /// Manga
        /// </summary>
        public string? Manga { get; set; }
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
        public string? Publisher { get; set; }
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
        public string? Currency { get; set; }
        /// <summary>
        /// Manga language id
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// Manga volume writen language
        /// </summary>
        public string? Language { get; set; }
    }
}
