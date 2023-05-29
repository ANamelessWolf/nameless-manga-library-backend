using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nameless.Manga.Models
{
    /// <summary>
    /// Author table model class
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
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
