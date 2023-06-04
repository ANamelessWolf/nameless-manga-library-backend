using Nameless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nameless.Manga.Models
{
    /// <summary>
    /// Manga table model class
    /// </summary>
    public class MangaModel : DbModel
    {
        /// <summary>
        /// Manga name
        /// </summary>
        public string? Name { get; set; }
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
        /// Manga demographic
        /// </summary>
        public DemographicCatalogue Demographic{ get; set; }
        /// <summary>
        /// Manga author writer id
        /// </summary>
        public int WriterId { get; set; }
        /// <summary>
        /// Author role
        /// </summary>
        public Author? Writer { get; set; }
        /// <summary>
        /// Author role
        /// </summary>
        public Author? Illustrator { get; set; }
        /// <summary>
        /// Manga author illustrator id
        /// </summary>
        public int IllustratorId { get; set; }
    }
}
