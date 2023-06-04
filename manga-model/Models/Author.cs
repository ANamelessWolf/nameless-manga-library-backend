using Nameless.WebApi.Models;

namespace Nameless.Manga.Models
{
    /// <summary>
    /// Author table model class
    /// </summary>
    public class Author: DbModel
    {
        /// <summary>
        /// Author full name
        /// </summary>
        public string? Name { get; set; }

        public int RoleId { get; set; }

        /// <summary>
        /// Author role
        /// </summary>
        public AuthorRoleCatalogue? Role { get; set; }
    }
}
