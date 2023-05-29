using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nameless.Manga.Models;
using Nameless.Manga.ModelsDto;
using Nameless.WebApi.Controllers;
using Nameless.MangaBI.Repositories.Implements;

namespace Nameless.MangaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController
        : GenericController<Author, AuthorDto>
    {
        public AuthorController(AuthorRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            
        }
    }
}
