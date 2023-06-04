using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Manga.ModelsDto
{
    public class NewManga
    {
        /// <summary>
        /// Manga name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Manga english name
        /// </summary>
        public string? NameEn { get; set; }
        /// <summary>
        /// Manga japanese name
        /// </summary>
        public string? NameJp { get; set; }
        /// <summary>
        /// Manga total volumes
        /// </summary>
        public int Volumes { get; set; }
        /// <summary>
        /// Manga total chapters
        /// </summary>
        public int Chapters { get; set; }
        /// <summary>
        /// A flag that indicates a chapter is finished
        /// </summary>
        public Boolean IsCompleted { get; set; }
        /// <summary>
        /// The demographic id
        /// </summary>
        public int DemographicId { get; set; }
        /// <summary>
        /// Manga author writer id
        /// </summary>
        public int WriterId { get; set; }
        /// <summary>
        /// Manga author illustrator id
        /// </summary>
        public int IllustratorId { get; set; }
    }
}
