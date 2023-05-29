using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.WebApi.Repositories;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class PublisherRepository :
        GenericRepository<PublisherCatalogue>, IPublisherRepository, IContextAccesible
    {
        public MangaBIContext Context => (MangaBIContext)this._context;

        public PublisherRepository(MangaBIContext context) : base(context)
        {

        }

    }
}
