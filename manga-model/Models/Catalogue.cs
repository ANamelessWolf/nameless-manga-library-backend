using Nameless.WebApi.Models;

namespace Nameless.Manga.Models
{
    /// <summary>
    /// Principal table of the employee
    /// </summary>
    public class Catalogue : DbModel
    {
        /// <summary>
        /// Catalogue item value
        /// </summary>
        public string? Name { get; set; }
    }
}
