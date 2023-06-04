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
    public class AuthorRoleController
        : CatalogueController<AuthorRoleCatalogue, AuthorRoleCatalogueDto>
    {
        public AuthorRoleController(AuthorRoleRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
