using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nameless.Manga.Models;
using Nameless.Manga.ModelsDto;
using Nameless.MangaBI.Repositories.Implements;
using Nameless.WebApi.Controllers;

namespace Nameless.MangaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaLibraryController
        : GenericController<MangaLibrary, MangaLibraryDto, NewMangaVolume>
    {
        public MangaLibraryController(MangaLibraryRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}