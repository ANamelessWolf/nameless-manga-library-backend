﻿using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.WebApi.Repositories;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class CurrencyRepository :
        GenericRepository<CurrencyCatalogue>, ICurrencyRepository, IContextAccesible
    {
        public MangaBIContext Context => (MangaBIContext)this._context;

        public CurrencyRepository(MangaBIContext context) : base(context)
        {

        }

    }
}
