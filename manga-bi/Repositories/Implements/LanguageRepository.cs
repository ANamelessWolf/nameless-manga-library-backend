using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.WebApi.Repositories.Interfaces;

namespace Nameless.WebApi.Repositories.Implements
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
