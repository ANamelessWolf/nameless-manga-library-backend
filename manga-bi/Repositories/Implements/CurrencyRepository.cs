using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.WebApi.Repositories.Interfaces;

namespace Nameless.WebApi.Repositories.Implements
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
