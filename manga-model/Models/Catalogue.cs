using System;
using System.Collections.Generic;

namespace Nameless.Manga.Models
{
    /// <summary>
    /// Principal table of the employee
    /// </summary>
    public class Catalogue
    {
        /// <summary>
        /// Catalogue primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Catalogue item value
        /// </summary>
        public string? Name { get; set; }
    }
}
