using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.WebApi.Repositories;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class LanguageRepository :
        GenericRepository<LanguageCatalogue>, ILanguageRepository, IContextAccesible
    {
        public MangaBIContext Context => (MangaBIContext)this._context;

        public LanguageRepository(MangaBIContext context) : base(context)
        {

        }

    }
}
