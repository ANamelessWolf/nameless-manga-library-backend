using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Manga.ModelsDto
{
    public class NewAuthor
    {
        /// <summary>
        /// Author full name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Author role id
        /// </summary>
        public int RoleId { get; set; }
    }
}
