namespace Nameless.Manga.Models
{
    public class CurrencyCatalogue : Catalogue
    {
        /// <summary>
        /// Currency value
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Currency symbol
        /// </summary>
        public string? Symbol { get; set; }
    }
}
