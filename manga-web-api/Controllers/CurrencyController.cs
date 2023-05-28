﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nameless.Manga.Models;
using Nameless.Manga.ModelsDto;
using Nameless.WebApi.Controllers;
using Nameless.WebApi.Repositories.Implements;

namespace manga_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController
        : BasicGenericController<CurrencyCatalogue, CurrencyCatalogueDto>
    {
        public CurrencyController(CurrencyRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
