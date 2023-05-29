using Nameless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Manga.ModelsDto
{
    public class AuthorDto : BaseDto
    {
        /// <summary>
        /// Catalogue item value
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Author Role Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Author role
        /// </summary>
        public AuthorRoleCatalogueDto? Role { get; set; }


    }
}
