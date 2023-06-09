﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nameless.Manga.Models;
using Nameless.Manga.ModelsDto;
using Nameless.MangaBI.Repositories.Implements;
using Nameless.WebApi.Controllers;

namespace Nameless.MangaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController 
        : GenericController<MangaModel, MangaDto, NewManga>
    {
        public MangaController(MangaRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}