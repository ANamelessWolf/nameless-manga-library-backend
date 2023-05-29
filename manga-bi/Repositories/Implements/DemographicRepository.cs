using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.WebApi.Repositories;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class DemographicRepository :
        GenericRepository<DemographicCatalogue>, IDemographicRepository, IContextAccesible
    {
        public MangaBIContext Context => (MangaBIContext)this._context;

        public DemographicRepository(MangaBIContext context) : base(context)
        {

        }

    }
}
